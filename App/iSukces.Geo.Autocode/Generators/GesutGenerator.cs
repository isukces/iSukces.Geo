using System.IO;
using iSukces.Code;
using iSukces.Code.AutoCode;

namespace iSukces.Geo.Autocode.Generators;

public class GesutGenerator : BaseGenerator<GesutObject>
{
    public GesutGenerator(SlnAssemblyBaseDirectoryProvider sln)
    {
        _docDir = new DirectoryInfo(Path.Combine(sln.SolutionDir.FullName, "..", "Docs"));
        _docDir = new DirectoryInfo(_docDir.FullName);
    }

      
    protected override void GenerateInternal()
    {
        if (Type != typeof(GesutObject)) return;
        MyGenerateInternal();
    }

    private void MyGenerateInternal()
    {
        var t    = Path.Combine(_docDir.FullName, "Gesut.html");
        var list = GetListFromFile(t, ParseGesutObject).GetAwaiter().GetResult();

        var body = Make1(list, Construct);
            
        AddGetKnownCodesMethod(body, "https://sip.lex.pl/akty-prawne/dzu-dziennik-ustaw/baza-danych-geodezyjnej-ewidencji-sieci-uzbrojenia-terenu-baza-danych-17969785");
    }

    private static string Construct(GesutObject src)
    {
        var ll = new CsArgumentsBuilder()
            .AddValue(src.ClassCode)
            .AddValue(src.ObjectCode)
            .AddValue(src.Class)
            .AddValue(src.Name);

        var code = ll.CallMethod("new " + nameof(GesutObject), false);
        return code;
    }

    private static GesutObject ParseGesutObject(string[] l)
    {
        if (l[1] == "POZIOM 1" || l[1] == "KOD")
            return null;
        return new GesutObject(l[3], l[5], l[4], l[6]);
        // return Make(l[1], l[2], l[3], l[4], l[5]);
    }

    #region Fields

    private readonly DirectoryInfo _docDir;

    #endregion
}