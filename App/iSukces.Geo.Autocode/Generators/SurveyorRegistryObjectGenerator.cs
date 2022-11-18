using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using iSukces.Code;
using iSukces.Code.AutoCode;

namespace iSukces.Geo.Autocode.Generators
{
    public class SurveyorRegistryObjectGenerator : BaseGenerator<SurveyorRegistryObject>
    {
        public SurveyorRegistryObjectGenerator(SlnAssemblyBaseDirectoryProvider sln)
        {
            _docDir = new DirectoryInfo(Path.Combine(sln.SolutionDir.FullName, "..", "Docs"));
            _docDir = new DirectoryInfo(_docDir.FullName);
        }

        private static string Construct(SurveyorRegistryObject src)
        {
            var ll = new CsArgumentsBuilder()
                .AddValue(src.Code)
                .AddValue(src.Group)
                .AddValue(src.Description);

            var code = ll.CallMethod("new " + nameof(SurveyorRegistryObject), false);
            return code;
        }


        protected override void GenerateInternal()
        {
            if (Type != typeof(SurveyorRegistryObject)) return;
            MyGenerateInternal();
        }

        private void MyGenerateInternal()
        {
            var list = new List<SurveyorRegistryObject>();

            var    t    = Path.Combine(_docDir.FullName, "Geodezyjna_ewidencja_sieci_uzbrojenia_terenu.txt");
            var    l    = File.ReadLines(t);
            string objGroup = "";
            foreach (var i in l)
            {
                if (i.Trim().StartsWith("#") || string.IsNullOrWhiteSpace(i))
                    continue;
                var ll = StartsWithNumberRegex.Match(i);
                if (ll.Success)
                {
                    var a  = ll.Groups[2].Value.Trim();
                    var aa = a.Split('\t');
                    list.Add(new SurveyorRegistryObject(aa[1], objGroup, aa[0]));
                }
                else
                {
                    var aa = i.Trim().Split('#');
                    objGroup = aa[0];
                }
            }

            var body = Make1(list, Construct);

            AddGetKnownCodesMethod(body, "https://sip.lex.pl/akty-prawne/dzu-dziennik-ustaw/baza-danych-obiektow-topograficznych-oraz-mapa-zasadnicza-19135412");
        }

        #region Fields

        const string StartsWithNumberFilter = @"^\s*(\d+)(.*)$";
        static readonly Regex StartsWithNumberRegex = new Regex(StartsWithNumberFilter, RegexOptions.IgnoreCase | RegexOptions.Compiled);

        private readonly DirectoryInfo _docDir;

        #endregion
    }
}
