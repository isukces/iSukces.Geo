namespace iSukces.Geo
{
    /// <summary>
    ///     Wykaz obiektów stanowiących treść mapy zasadniczej
    ///     Element tabeli https://www.prawo.pl/akty/dz-u-2015-2028,18246588.html
    /// </summary>
    public sealed class MasterMapObject
    {
        public MasterMapObject(string code, string name, GeometryKind objectGeometry, GeometryKind markGeometry,
            string cartographicSign)
        {
            Name             = name;
            ObjectGeometry   = objectGeometry;
            Code             = code;
            MarkGeometry     = markGeometry;
            CartographicSign = cartographicSign;
        }

        #region properties

        public string       Name             { get; }
        public GeometryKind ObjectGeometry   { get; }
        public string       Code             { get; }
        public GeometryKind MarkGeometry     { get; }
        public string       CartographicSign { get; }

        #endregion
    }
}
