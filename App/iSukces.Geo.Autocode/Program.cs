using System;
using iSukces.Code.AutoCode;
using iSukces.Geo.Autocode.Env;
using iSukces.Geo.Autocode.Generators;

namespace iSukces.Geo.Autocode
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var sln = SlnAssemblyBaseDirectoryProvider.Make<Program>("iSukces.Geo.sln");
            var fn  = new SimpleAssemblyFilenameProvider(sln, "AutoCode.cs");
            var gen = new MyAutoCodeGenerator(fn);
            gen.TypeBasedOutputProvider = new CustomCsOutputProvider(sln);
            gen.WithGenerator(new SipGeoInfoGenerator(sln));
            gen.WithGenerator(new GesutGenerator(sln));
            gen.WithGenerator(new SurveyorRegistryObjectGenerator(sln));
            gen.Make<MasterMapObject>();
        }
    }
}
