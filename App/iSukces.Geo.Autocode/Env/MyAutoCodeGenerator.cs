using System.Collections.Generic;
using System.Reflection;
using iSukces.Code.AutoCode;
using iSukces.Code.CodeWrite;

namespace iSukces.Geo.Autocode.Env
{
    internal sealed class MyAutoCodeGenerator : AutoCodeGenerator
    {
        public MyAutoCodeGenerator(IAssemblyFilenameProvider prov2)
            : base(prov2)
        {
        }

        protected override IFinalizableAutoCodeGeneratorContext CreateAutoCodeGeneratorContext(CsFile file,
            Assembly assembly)
        {
            var context = new AutoCodeGeneratorContext(file, file.GetOrCreateClass);
            return context;
        }

        #region Fields

        private readonly HashSet<Assembly> _alreadyMade = new HashSet<Assembly>();

        #endregion
    }
}
