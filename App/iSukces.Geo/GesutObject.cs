namespace iSukces.Geo
{
    /// <summary>
    ///     Opisuje sieci uzbrojenia terenu
    /// </summary>
    public class GesutObject
    {
        public GesutObject(string classCode, string objectCode, string @class, string name)
        {
            Class      = @class;
            Name       = name;
            ClassCode  = classCode;
            ObjectCode = objectCode;
        }

        #region properties

        /// <summary>
        ///     Nazwa klasy obiektów
        /// </summary>
        public string Class { get; }

        /// <summary>
        ///     Nazwa obiektu
        /// </summary>
        public string Name { get; }

        /// <summary>
        ///     Kod poziomu 2, dla nazwa klasy obiektów
        /// </summary>
        public string ClassCode { get; }


        /// <summary>
        ///     Kod poziomu 3, dla nazwa obiektu
        /// </summary>
        public string ObjectCode { get; }

        #endregion
    }
}
