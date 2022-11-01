using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using iSukces.Code;
using iSukces.Code.AutoCode;
using iSukces.Code.Interfaces;
using OfficeOpenXml;

namespace iSukces.Geo.Autocode.Generators
{
    public class SipGeoInfoGenerator : Code.AutoCode.Generators.SingleClassGenerator
    {
        public SipGeoInfoGenerator(SlnAssemblyBaseDirectoryProvider sln)
        {
            _docDir = new DirectoryInfo(Path.Combine(sln.SolutionDir.FullName, "..", "Docs"));
            _docDir = new DirectoryInfo(_docDir.FullName);
        }

        private static MasterMapObject Make(string name, string objectGeometry, string code, string markGeometry,
            string cartographicSign)
        {
            if (string.IsNullOrWhiteSpace(code))
                return null;
            if (name =="Nazwa obiektu bazy danych")
                return null;
            return new MasterMapObject(code, name, Parse(objectGeometry), Parse(markGeometry), cartographicSign);
        }

        private static MasterMapObject Make(IReadOnlyList<string> strings)
        {
            return Make(strings[1], strings[2], strings[3], strings[4], strings[5]);
        }

        private static GeometryKind Parse(string t)
        {
            return t switch
            {
                "punkt" => GeometryKind.Point,
                "powierzchnia" => GeometryKind.Area,
                "multipowierzchnia" => GeometryKind.MultiArea,
                "linia" => GeometryKind.Line,
                "tekst" => GeometryKind.Text,
                "brak" => GeometryKind.None,
                _ => throw new NotImplementedException()
            };
        }

        private static async Task<Dictionary<TableCellKey, string>> ParseHtmlTable(string html)
        {
            var       config  = Configuration.Default;
            using var context = BrowsingContext.New(config);
            using var doc     = await context.OpenAsync(req => req.Content(html));
            Console.WriteLine(doc.Title);
            Console.WriteLine(doc.Body.InnerHtml.Trim());
            Console.WriteLine(doc.FirstChild.NodeName.ToLower());
            Console.WriteLine(doc.LastChild.NodeName.ToLower());
            var table = doc.QuerySelectorAll("table").First();
            var rows  = table.QuerySelectorAll("tr").ToArray();
            
            Dictionary<TableCellKey, string> values    = new();

            
            for (var rowIdx = 0; rowIdx < rows.Length; rowIdx++)
            {
                var row   = rows[rowIdx];
                var cells = row.QuerySelectorAll("td").Cast<IHtmlTableDataCellElement>().ToArray();

                int columnIdx = 0;
                for (int i = 0; i < cells.Length; i++)
                {
                   
                    var cell  = cells[i];
                    var value = GetCellValue(cell);

                   
                    
                    var rs = cell.RowSpan;
                    var cs = cell.ColumnSpan;
                    if (cs > 1)
                    {
                        throw new NotImplementedException();
                    }

                    for (int ii = 0; ii < rs; ii++)
                    {
                        var key = new TableCellKey(rowIdx + ii, columnIdx);
                        if (ii == 0)
                        {
                            while (true)
                            {
                                if (!values.ContainsKey(key))
                                    break;
                                key = new TableCellKey(rowIdx + ii, ++columnIdx);
                            }
                        }

                        values.Add(key, value ?? "");
                    }

                    columnIdx++;
                }
            }

            {

                Dictionary<string, string> col1                                                       = new Dictionary<string, string>();
                for (int i = 325; i <= 364; i++) 
                    col1[i.ToString(CultureInfo.InvariantCulture) + "."] = "rura ochronna";

                col1["127."] = "inna budowla";
                col1["247."] = "przewód gazowy podwyższonego średniego ciśnienia";
                
                var keys = values.Keys.Where(a => a.Col == 0).ToArray();
                foreach (var key in keys)
                {
                    var v = values[key];
                    if (col1.TryGetValue(v, out var text))
                    {
                        var key2 = key.WithColumn(1);
                        values[key2] = text;
                    }
                    if (v is "36." or "37.")
                    {
                        var key2 = key.WithColumn(5);
                        values[key2] = "podpora związana z budynkiem";
                    }
                    
                    if (v is "24." or "23.")
                    {
                        var key2 = key.WithColumn(5);
                        values[key2] = "budynek projektowany";
                    }
                }
                

            }

            string GetCellValue(IHtmlTableDataCellElement cell)
            {
                var sb = new StringBuilder();

                void Scan(INode el)
                {
                    sb.Append(" ");
                    if (el is IText tn)
                    {
                        sb.Append(tn.Text);
                        return;
                    }

                    if (el is IElement element)
                    {
                        Scan2(element.ChildNodes);
                        return;
                    }

                    throw new NotImplementedException();
                }

                void Scan2(INodeList el)
                {
                    foreach (var i in el)
                    {
                        Scan(i);
                    }
                }

                Scan2(cell.ChildNodes);
                var a = sb.ToString().Replace("\n", " ").Trim();
                a = MoreSpacesRegex.Replace(a, m =>
                {
                    return " ";
                });
                if (a=="")
                    Debug.WriteLine("");
                return a;
            }

            return values;
        }
        
        const string MoreSpacesFilter = @"[\s]{2,}";
        static Regex MoreSpacesRegex = new Regex(MoreSpacesFilter, RegexOptions.Compiled);
        
        private static void WriteList(CsCodeWriter code, IReadOnlyList<string> list)
        {
            for (var index = 0; index < list.Count; index++)
            {
                var i = list[index];
                if (index < list.Count - 1)
                    i += ",";
                code.WriteLine(i);
            }
        }

        protected override void GenerateInternal()
        {
            if (Type != typeof(MasterMapObjects)) return;
            MyGenerateInternal();
        }

        private async Task<List<MasterMapObject>> GetListFromFile()
        {
            var fi = new FileInfo(Path.Combine(_docDir.FullName, "WykazObiektówStanowiącychTreśćMapyZasadniczej.html"));
            if (!fi.Exists)
                throw new FileNotFoundException("File not found", fi.FullName);
            var result = new List<MasterMapObject>();
            var html   = File.ReadAllText(fi.FullName);
            var values = await ParseHtmlTable(html);

            var keysByRow = values.Keys.GroupBy(a => a.Row).OrderBy(a => a.Key).ToArray();
            foreach (var i in keysByRow)
            {
                var maxCol = i.Select(a => a.Col).Max();
                var l      = new string[maxCol + 1];
                for (int col = 0; col <= maxCol; col++)
                {
                    var key = new TableCellKey(i.Key, col);
                    if (values.TryGetValue(key, out var value))
                        l[col] = value;
                }

                var so = Make(l);

                if (so is null)
                    continue;
                result.Add(so);
            }

            return result;
        }

        private void MyGenerateInternal()
        {
            var list = GetListFromFile().GetAwaiter().GetResult();

            var sb = new CsCodeWriter();

            sb.WriteLine("return new []");
            sb.WriteLine("{");
            sb.IncIndent();

            var names = list.Select(a => a.CartographicSign)
                .Where(a => a is not null)
                .Distinct()
                .OrderBy(a => a)
                .ToArray();

            {
                var code = new CsCodeWriter();
                code.Open("return new[]");
                WriteList(code, names.Select(a => a.CsEncode()).ToArray());
                code.Close("};");
                Class.AddMethod("GetCartographicSigns", Class.GetTypeName<IReadOnlyList<string>>())
                    .WithStatic()
                    .WithVisibility(Visibilities.Private)
                    .WithBody(code);
            }

            WriteList(sb, list.Select(o =>
            {
                var ll = new CsArgumentsBuilder();
                ll.AddValue(o.Code)
                    .AddValue(o.Name)
                    .AddCode(nameof(GeometryKind) + "." + o.ObjectGeometry)
                    .AddCode(nameof(GeometryKind) + "." + o.MarkGeometry)
                    .AddValue(o.CartographicSign);

                var code = ll.CallMethod("new " + nameof(MasterMapObject), false);
                return code;
            }).ToArray());

            sb.DecIndent();
            sb.WriteLine("};");

            var m = Class.AddMethod("GetKnownCodes", $"System.Collections.Generic.IReadOnlyList<{nameof(MasterMapObject)}>")
                .WithStatic()
                .WithVisibility(Visibilities.Private)
                .WithBody(sb);
            m.AddComment("Sporządzone na podstawie https://www.prawo.pl/akty/dz-u-2015-2028,18246588.html");
        }

        #region Fields

        private readonly DirectoryInfo _docDir;

        #endregion

        
    }
}
