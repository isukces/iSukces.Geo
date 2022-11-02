using System.Collections.Generic;

namespace iSukces.Geo
{
    public partial class GesutObjects
    {
        #region properties

        public static IReadOnlyList<GesutObject> KnownObjects { get; } = GetKnownCodes();

        #endregion
    }
}