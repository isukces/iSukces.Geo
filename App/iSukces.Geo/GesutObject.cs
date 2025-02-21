using System.Collections.Generic;

namespace iSukces.Geo;

/// <summary>
///     Opisuje sieci uzbrojenia terenu
/// </summary>
public partial class GesutObject
{
    public GesutObject(string classCode, string objectCode, string @class, string name)
    {
        Class      = @class;
        Name       = name;
        ClassCode  = classCode;
        ObjectCode = objectCode;
    }

    public static IReadOnlyList<GesutObject> KnownObjects { get; } = GetKnownCodes();

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
}

// -----===== autogenerated code =====-----
// ReSharper disable All
partial class GesutObject
{
    // Sporządzone na podstawie https://sip.lex.pl/akty-prawne/dzu-dziennik-ustaw/baza-danych-geodezyjnej-ewidencji-sieci-uzbrojenia-terenu-baza-danych-17969785
    private static System.Collections.Generic.IReadOnlyList<GesutObject> GetKnownCodes()
    {
        return new []
        {
            new GesutObject("SUPB", "SUPB01", "przewód benzynowy", "przewód benzynowy"),
            new GesutObject("SUPC", "SUPC01", "przewód ciepłowniczy", "przewód ciepłowniczy o wysokim parametrze - wodny"),
            new GesutObject("SUPC", "SUPC02", "przewód ciepłowniczy", "przewód ciepłowniczy o niskim parametrze - wodny"),
            new GesutObject("SUPC", "SUPC03", "przewód ciepłowniczy", "przewód ciepłowniczy dwuprzewodowy - parowy"),
            new GesutObject("SUPC", "SUPC04", "przewód ciepłowniczy", "przewód ciepłowniczy jednoprzewodowy - parowy"),
            new GesutObject("SUPC", "SUPC05", "przewód ciepłowniczy", "przewód ciepłowniczy"),
            new GesutObject("SUPE", "SUPE01", "przewód elektroenergetyczny", "przewód elektroenergetyczny najwyższego napięcia"),
            new GesutObject("SUPE", "SUPE02", "przewód elektroenergetyczny", "przewód elektroenergetyczny wysokiego napięcia"),
            new GesutObject("SUPE", "SUPE03", "przewód elektroenergetyczny", "przewód elektroenergetyczny średniego napięcia"),
            new GesutObject("SUPE", "SUPE04", "przewód elektroenergetyczny", "przewód elektroenergetyczny niskiego napięcia"),
            new GesutObject("SUPE", "SUPE05", "przewód elektroenergetyczny", "przewód elektroenergetyczny oświetleniowy"),
            new GesutObject("SUPE", "SUPE06", "przewód elektroenergetyczny", "przewód elektroenergetyczny"),
            new GesutObject("SUPG", "SUPG01", "przewód gazowy", "przewód gazowy wysokiego ciśnienia"),
            new GesutObject("SUPG", "SUPG02", "przewód gazowy", "przewód gazowy podwyższonego średniego ciśnienia"),
            new GesutObject("SUPG", "SUPG03", "przewód gazowy", "przewód gazowy średniego ciśnienia"),
            new GesutObject("SUPG", "SUPG04", "przewód gazowy", "przewód gazowy niskiego ciśnienia"),
            new GesutObject("SUPG", "SUPG05", "przewód gazowy", "przewód gazowy"),
            new GesutObject("SUPK", "SUPK01", "przewód kanalizacyjny", "przewód kanalizacyjny deszczowy"),
            new GesutObject("SUPK", "SUPK02", "przewód kanalizacyjny", "przewód kanalizacyjny lokalny"),
            new GesutObject("SUPK", "SUPK03", "przewód kanalizacyjny", "przewód kanalizacyjny ogólnospławny"),
            new GesutObject("SUPK", "SUPK04", "przewód kanalizacyjny", "przewód kanalizacyjny przemysłowy"),
            new GesutObject("SUPK", "SUPK05", "przewód kanalizacyjny", "przewód kanalizacyjny sanitarny"),
            new GesutObject("SUPK", "SUPK06", "przewód kanalizacyjny", "przewód kanalizacyjny"),
            new GesutObject("SUPN", "SUPN01", "przewód naftowy", "przewód naftowy"),
            new GesutObject("SUPT", "SUPT01", "przewód telekomunikacyjny", "przewód telekomunikacyjny"),
            new GesutObject("SUPW", "SUPW01", "przewód wodociągowy", "przewód wodociągowy ogólny"),
            new GesutObject("SUPW", "SUPW02", "przewód wodociągowy", "przewód wodociągowy lokalny"),
            new GesutObject("SUPW", "SUPW03", "przewód wodociągowy", "przewód wodociągowy"),
            new GesutObject("SUPZ", "SUPZ01", "przewód niezidentyfikowany", "przewód niezidentyfikowany"),
            new GesutObject("SUPI", "SUPI01", "przewód inny", "przewód inny"),
            new GesutObject("SUOP", "SUOP01", "obudowa przewodu", "kanalizacja kablowa"),
            new GesutObject("SUOP", "SUOP02", "obudowa przewodu", "kanał ciepłowniczy"),
            new GesutObject("SUOP", "SUOP03", "obudowa przewodu", "rura ochronna"),
            new GesutObject("SUOP", "SUOP04", "obudowa przewodu", "inna obudowa przewodu"),
            new GesutObject("SUBP", "SUBP01", "budowla podziemna", "kanał technologiczny"),
            new GesutObject("SUBP", "SUBP02", "budowla podziemna", "komora podziemna"),
            new GesutObject("SUBP", "SUBP03", "budowla podziemna", "osadnik piaskowy"),
            new GesutObject("SUBP", "SUBP04", "budowla podziemna", "parking lub garaż"),
            new GesutObject("SUBP", "SUBP05", "budowla podziemna", "przejście podziemne"),
            new GesutObject("SUBP", "SUBP06", "budowla podziemna", "tunel drogowy"),
            new GesutObject("SUBP", "SUBP07", "budowla podziemna", "tunel kolejowy"),
            new GesutObject("SUBP", "SUBP08", "budowla podziemna", "tunel metra"),
            new GesutObject("SUBP", "SUBP09", "budowla podziemna", "tunel tramwajowy"),
            new GesutObject("SUBP", "SUBP10", "budowla podziemna", "schron lub bunkier"),
            new GesutObject("SUBP", "SUBP11", "budowla podziemna", "inna budowla podziemna"),
            new GesutObject("SUUS", "SUUS01", "urządzenie techniczne związane z siecią", "dystrybutor paliw"),
            new GesutObject("SUUS", "SUUS02", "urządzenie techniczne związane z siecią", "hydrant"),
            new GesutObject("SUUS", "SUUS03", "urządzenie techniczne związane z siecią", "hydrofornia"),
            new GesutObject("SUUS", "SUUS04", "urządzenie techniczne związane z siecią", "kontener telekomunikacyjny"),
            new GesutObject("SUUS", "SUUS05", "urządzenie techniczne związane z siecią", "kratka ściekowa"),
            new GesutObject("SUUS", "SUUS06", "urządzenie techniczne związane z siecią", "odwodnienie liniowe"),
            new GesutObject("SUUS", "SUUS07", "urządzenie techniczne związane z siecią", "osadnik kanalizacji lokalnej (dół Chambeau)"),
            new GesutObject("SUUS", "SUUS08", "urządzenie techniczne związane z siecią", "przepompownia"),
            new GesutObject("SUUS", "SUUS09", "urządzenie techniczne związane z siecią", "słupek telekomunikacyjny"),
            new GesutObject("SUUS", "SUUS10", "urządzenie techniczne związane z siecią", "słupowa stacja transformatorowa"),
            new GesutObject("SUUS", "SUUS11", "urządzenie techniczne związane z siecią", "stacja gazowa"),
            new GesutObject("SUUS", "SUUS12", "urządzenie techniczne związane z siecią", "stacja transformatorowa"),
            new GesutObject("SUUS", "SUUS13", "urządzenie techniczne związane z siecią", "studnia"),
            new GesutObject("SUUS", "SUUS14", "urządzenie techniczne związane z siecią", "studnia głębinowa"),
            new GesutObject("SUUS", "SUUS15", "urządzenie techniczne związane z siecią", "studzienka"),
            new GesutObject("SUUS", "SUUS16", "urządzenie techniczne związane z siecią", "sygnalizator świetlny"),
            new GesutObject("SUUS", "SUUS17", "urządzenie techniczne związane z siecią", "szafa kablowa"),
            new GesutObject("SUUS", "SUUS18", "urządzenie techniczne związane z siecią", "szafa oświetleniowa"),
            new GesutObject("SUUS", "SUUS19", "urządzenie techniczne związane z siecią", "szafa sterownicza"),
            new GesutObject("SUUS", "SUUS20", "urządzenie techniczne związane z siecią", "szafka gazowa"),
            new GesutObject("SUUS", "SUUS21", "urządzenie techniczne związane z siecią", "trójnik"),
            new GesutObject("SUUS", "SUUS22", "urządzenie techniczne związane z siecią", "właz"),
            new GesutObject("SUUS", "SUUS23", "urządzenie techniczne związane z siecią", "wylot kanału"),
            new GesutObject("SUUS", "SUUS24", "urządzenie techniczne związane z siecią", "wywietrznik"),
            new GesutObject("SUUS", "SUUS25", "urządzenie techniczne związane z siecią", "zasuwa"),
            new GesutObject("SUUS", "SUUS26", "urządzenie techniczne związane z siecią", "zawór"),
            new GesutObject("SUUS", "SUUS27", "urządzenie techniczne związane z siecią", "zbiornik"),
            new GesutObject("SUUS", "SUUS28", "urządzenie techniczne związane z siecią", "zdrój uliczny"),
            new GesutObject("SUUS", "SUUS29", "urządzenie techniczne związane z siecią", "złącze kablowe"),
            new GesutObject("SUUS", "SUUS30", "urządzenie techniczne związane z siecią", "niezidentyfikowane urządzenie techniczne"),
            new GesutObject("SUUS", "SUUS31", "urządzenie techniczne związane z siecią", "inne urządzenie techniczne"),
            new GesutObject("SUPS", "SUPS01", "punkt o określonej wysokości", "punkt o określonej wysokości"),
            new GesutObject("SUSM", "SUSM01", "słup i maszt", "latarnia"),
            new GesutObject("SUSM", "SUSM02", "słup i maszt", "maszt oświetleniowy"),
            new GesutObject("SUSM", "SUSM03", "słup i maszt", "maszt telekomunikacyjny"),
            new GesutObject("SUSM", "SUSM04", "słup i maszt", "słup"),
            new GesutObject("SUSM", "SUSM05", "słup i maszt", "słup łączony"),
            new GesutObject("SUSM", "SUSM06", "słup i maszt", "słup kratowy"),
            new GesutObject("SUSM", "SUSM07", "słup i maszt", "słup trakcji kolejowej"),
            new GesutObject("SUSM", "SUSM08", "słup i maszt", "słup trakcji tramwajowej"),
            new GesutObject("SUSM", "SUSM09", "słup i maszt", "słup trakcji trolejbusowej"),
            new GesutObject("SUSM", "SUSM10", "słup i maszt", "turbina wiatrowa"),
            new GesutObject("SUSM", "SUSM11", "słup i maszt", "wieża telekomunikacyjna"),
            new GesutObject("SUSM", "SUSM12", "słup i maszt", "inny słup lub maszt"),
            new GesutObject("SUKP", "SUKP01", "korytarz przesyłowy", "korytarz przesyłowy")
        };
    }

}
