using System.Collections.Generic;

namespace SkyDesert.Localization.Parsing
{
    public interface ILocaleValidator
    {
        IReadOnlyList<LocaleParsingDiagnostic> Validate(string input);
    }
}
