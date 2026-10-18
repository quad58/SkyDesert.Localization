using System.Collections.Generic;

namespace SkyDesert.Localization.Parsing
{
    public interface ILocaleParser
    {
        public LocaleParsingResult Parse(string input, string localeCode);
        public IReadOnlyList<LocaleParsingDiagnostic> Validate(string input);
    }
}
