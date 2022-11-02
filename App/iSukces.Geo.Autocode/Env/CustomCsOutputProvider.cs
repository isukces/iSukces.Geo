using System;
using System.IO;
using iSukces.Code.AutoCode;

namespace iSukces.Geo.Autocode.Env
{
    public sealed class CustomCsOutputProvider : ICsOutputProvider
    {
        public CustomCsOutputProvider(SlnAssemblyBaseDirectoryProvider sln)
        {
            _sln = sln;
        }

        public CsOutputFileInfo GeOutputFileInfo(Type type)
        {
            var projFolder = _sln.GetBaseDirectory(type.Assembly);
            var fileName   = Path.Combine(projFolder.FullName, type.Name + ".cs");
            return new CsOutputFileInfo(fileName, true);
        }

        #region Fields

        private const string Name = "GetCodeFilePath";
        private readonly SlnAssemblyBaseDirectoryProvider _sln;

        #endregion
    }
}
