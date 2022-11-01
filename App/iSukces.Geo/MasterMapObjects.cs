using System.Collections.Generic;

namespace iSukces.Geo
{
    public partial class MasterMapObjects
    {
        #region properties

        public static IReadOnlyList<MasterMapObject> KnownObjects { get; } = GetKnownCodes();

        #endregion
    }
}
