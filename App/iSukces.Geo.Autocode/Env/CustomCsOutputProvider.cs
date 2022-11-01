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
            var fileName   = Path.Combine(projFolder.FullName, type.Name + ".Auto.cs");
            return new CsOutputFileInfo(fileName, false);
            /*var at     = type.GetCustomAttribute<AutocodeCustomOutputMethod>(false);
            var name   = at?.MethodName ?? Name;
            var method = type.GetMethod(name, GeneratorsHelper.AllStatic);
            if (method is null)
                return null;
            if (method.Invoke(null, null) is string fileName)
                return new CsOutputFileInfo(fileName, true);
            return null;*/
        }

        #region Fields

        private const string Name = "GetCodeFilePath";
        private readonly SlnAssemblyBaseDirectoryProvider _sln;

        #endregion
    }
}
