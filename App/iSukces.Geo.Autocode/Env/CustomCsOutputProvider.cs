using System;
using System.IO;
using iSukces.Code.AutoCode;

namespace iSukces.Geo.Autocode.Env;

public sealed class CustomCsOutputProvider : ICsOutputProvider
{
    public CustomCsOutputProvider(SlnAssemblyBaseDirectoryProvider sln)
    {
        _sln = sln;
    }
    
    private readonly SlnAssemblyBaseDirectoryProvider _sln;

    public CsOutputFileInfo GetOutputFileInfo(Type type)
    {
        var projFolder = _sln.GetBaseDirectory(type.Assembly);
        var fileName   = Path.Combine(projFolder.FullName, type.Name + ".cs");
        return new CsOutputFileInfo(fileName, true);
    }
}
