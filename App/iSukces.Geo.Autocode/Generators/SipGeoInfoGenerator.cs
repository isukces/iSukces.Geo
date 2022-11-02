using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using iSukces.Code;
using iSukces.Code.AutoCode;

namespace iSukces.Geo.Autocode.Generators
{
    public class SipGeoInfoGenerator : BaseGenerator<MasterMapObject>
    {
        public SipGeoInfoGenerator(SlnAssemblyBaseDirectoryProvider sln)
        {
            _docDir = new DirectoryInfo(Path.Combine(sln.SolutionDir.FullName, "..", "Docs"));
            _docDir = new DirectoryInfo(_docDir.FullName);
        }

        protected override void Fix(Dictionary<TableCellKey, string> values)
        {
            base.Fix(values);
            var col1 = new Dictionary<string, string>();
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

        protected override void GenerateInternal()
        {
            if (Type != typeof(MasterMapObjects)) return;
            MyGenerateInternal();
        }

        private void MyGenerateInternal()
        {
            var t = Path.Combine(_docDir.FullName, "WykazObiektówStanowiącychTreśćMapyZasadniczej.html");
            var list = GetListFromFile(t, ParseMasterMapObject).GetAwaiter().GetResult();

            /*
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
            */
            /*var body = new CsCodeWriter();
            body.WriteLine("return new []");
            body.WriteLine("{");
            body.IncIndent();

            WriteList(body, list.Select(o =>
            {
               
            }).ToArray());

            body.DecIndent();
            body.WriteLine("};");*/
            var body = Make1(list, Construct);

            AddGetKnownCodesMethod(body, "Sporządzone na podstawie https://www.prawo.pl/akty/dz-u-2015-2028,18246588.html");
        }

        private static string Construct(MasterMapObject o)
        {
            var ll = new CsArgumentsBuilder();
            ll.AddValue(o.Code)
                .AddValue(o.Name)
                .AddCode(nameof(GeometryKind) + "." + o.ObjectGeometry)
                .AddCode(nameof(GeometryKind) + "." + o.MarkGeometry)
                .AddValue(o.CartographicSign);

            var code = ll.CallMethod("new " + nameof(MasterMapObject), false);
            return code;
        }

        private static MasterMapObject ParseMasterMapObject(string[] l)
        {
            return Make(l[1], l[2], l[3], l[4], l[5]);
        }

        #region Fields

        private readonly DirectoryInfo _docDir;

        #endregion
    }
}
