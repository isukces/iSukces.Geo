using System;
using System.IO;
using iSukces.Code;
using iSukces.Code.AutoCode;
using iSukces.Geo.Autocode.Env;
using iSukces.Geo.Autocode.Generators;

namespace iSukces.Geo.Autocode;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Creating classes...");
        var sln = SlnAssemblyBaseDirectoryProvider.Make<Program>("iSukces.Geo.sln");
        var fn  = new SimpleAssemblyFilenameProvider(sln, "AutoCode.cs");
        var gen = new MyAutoCodeGenerator(fn);
        
        CsFileFactory.Instance.CreateCsFile += (a, b) =>
        {
            if (!string.IsNullOrEmpty(b.File.SuggestedFileName))
                Console.WriteLine("Creating file " + Path.GetFileName(b.File.SuggestedFileName));
            b.File.FileScopeNamespace = FileScopeNamespaceConfiguration.AssumeDefined("iSukces.Geo");
            b.File.Nullable           = FileNullableOption.GlobalEnabled;
        };
        
        gen.TypeBasedOutputProvider = new CustomCsOutputProvider(sln);
        gen.WithGenerator(new SipGeoInfoGenerator(sln));
        gen.WithGenerator(new GesutGenerator(sln));
        gen.WithGenerator(new SurveyorRegistryObjectGenerator(sln));
        gen.Make<MasterMapObject>();
        
        
        Console.WriteLine("Done");
    }
}
