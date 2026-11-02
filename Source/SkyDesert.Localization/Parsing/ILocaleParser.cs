namespace SkyDesert.Localization.Parsing
{
    public interface ILocaleParser
    {
        LocaleParsingResult Parse(string input, string localeCode);
    }
}
