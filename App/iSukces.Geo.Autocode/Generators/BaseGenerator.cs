using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using iSukces.Code;
using iSukces.Code.Interfaces;

namespace iSukces.Geo.Autocode.Generators;

public abstract class BaseGenerator<TElement> : Code.AutoCode.Generators.SingleClassGenerator
{
    protected static MasterMapObject? Make(string name, string objectGeometry, string code, string markGeometry,
        string cartographicSign)
    {
        if (string.IsNullOrWhiteSpace(code))
            return null;
        if (name == "Nazwa obiektu bazy danych")
            return null;
        return new MasterMapObject(code, name, Parse(objectGeometry), Parse(markGeometry), cartographicSign);
    }

    protected static MasterMapObject? Make(IReadOnlyList<string> l)
    {
        return Make(l[1], l[2], l[3], l[4], l[5]);
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

    public static void WriteList(CsCodeWriter code, IReadOnlyList<string> list)
    {
        for (var index = 0; index < list.Count; index++)
        {
            var i = list[index];
            if (index < list.Count - 1)
                i += ",";
            code.WriteLine(i);
        }
    }

    protected void AddGetKnownCodesMethod(CsCodeWriter sb, string source)
    {
        var m = Class.AddMethod("GetKnownCodes", (CsType)$"System.Collections.Generic.IReadOnlyList<{typeof(TElement).Name}>")
            .WithStatic()
            .WithVisibility(Visibilities.Private)
            .WithBody(sb);
        m.AddComment("Sporządzone na podstawie " + source);
    }

    protected virtual void Fix(Dictionary<TableCellKey, string> values)
    {
    }

    public async Task<List<T>> GetListFromFile<T>(string s, Func<string[], T?> make)
        where T : class
    {
        var fi = new FileInfo(s);
        if (!fi.Exists)
            throw new FileNotFoundException("File not found", fi.FullName);
        var result = new List<T>();
        var html   = await File.ReadAllTextAsync(fi.FullName);
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

            var so = make(l);

            if (so is null)
                continue;
            result.Add(so);
        }

        return result;
    }

    protected CsCodeWriter Make1<T>(List<T> list, Func<T, string> callConstructor)
    {
        var body = new CsCodeWriter();
        body.WriteLine("return new []");
        body.WriteLine("{");
        body.IncIndent();
        WriteList(body, list.Select(callConstructor).ToArray());
        body.DecIndent();
        body.WriteLine("};");
        return body;
    }

    protected async Task<Dictionary<TableCellKey, string>> ParseHtmlTable(string html)
    {
        var       config  = Configuration.Default;
        using var context = BrowsingContext.New(config);
        using var doc     = await context.OpenAsync(req => req.Content(html));

        var table = doc.QuerySelectorAll("table").First();
        var rows  = table.QuerySelectorAll("tr").ToArray();

        Dictionary<TableCellKey, string> values = new();

        for (var rowIdx = 0; rowIdx < rows.Length; rowIdx++)
        {
            var row   = rows[rowIdx];
            var cells = row.QuerySelectorAll("td").Cast<IHtmlTableDataCellElement>().ToArray();

            int columnIdx = 0;
            for (int i = 0; i < cells.Length; i++)
            {
                var cell  = cells[i];
                var value = GetCellValue(cell) ?? "";

                var rs         = cell.RowSpan;
                var columnSpan = cell.ColumnSpan;

                var key = new TableCellKey(rowIdx, columnIdx);
                while (true)
                {
                    if (!values.ContainsKey(key))
                        break;
                    key = new TableCellKey(rowIdx, ++columnIdx);
                }

                void AddRows()
                {
                    if (rs == 1)
                    {
                        key = new TableCellKey(rowIdx, columnIdx);
                        values.Add(key, value);
                        return;
                    }

                    for (var rowPlus = 0; rowPlus < rs; rowPlus++)
                    {
                        key = new TableCellKey(rowIdx + rowPlus, columnIdx);
                        values.Add(key, value);
                    }
                }

                if (columnSpan > 1)
                {
                    for (var colPlus = 0; colPlus < columnSpan; colPlus++)
                    {
                        AddRows();
                        columnIdx++;
                    }

                    continue;
                }

                AddRows();
                columnIdx++;
            }
        }

        Fix(values);

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
            if (a == "")
                Debug.WriteLine("");
            return a;
        }

        return values;
    }

    const string MoreSpacesFilter = @"[\s]{2,}";
    static readonly Regex MoreSpacesRegex = new Regex(MoreSpacesFilter, RegexOptions.Compiled);
}