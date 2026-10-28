using System.Collections.Generic;

namespace SkyDesert.Localization.Parsing
{
    public interface ILocaleParser
    {
        LocaleParsingResult Parse(string input, string localeCode);
        IReadOnlyList<LocaleParsingDiagnostic> Validate(string input);
    }
}
