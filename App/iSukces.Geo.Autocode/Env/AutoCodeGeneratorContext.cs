using System;
using iSukces.Code;
using iSukces.Code.AutoCode;

namespace iSukces.Geo.Autocode.Env;

public sealed class AutoCodeGeneratorContext : AutoCodeGenerator.SimpleAutoCodeGeneratorContext
{
    /*public AutoCodeGeneratorContext(Func<TypeProvider, CsClass> getOrCreateClassFunc,
        Action<string> addNamespaceAction, Func<string, CsNamespace> getOrCreateNamespaceFunc)
        : base(getOrCreateClassFunc, addNamespaceAction, getOrCreateNamespaceFunc)
    {
    }*/

    public AutoCodeGeneratorContext(CsFile file, Func<TypeProvider, CsClass> getOrCreateClassFunc)
        : base(file, getOrCreateClassFunc)
    {
    }
}
