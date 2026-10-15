using SkyDesert.Localization.Core;
using System.Collections.Generic;

namespace SkyDesert.Localization.Parsing
{
    public class LocaleParsingResult
    {
        public LocaleParsingResult(bool success, Locale locale, IReadOnlyList<LocaleParsingDiagnostic> diagnostics)
        {
            Success = success;
            Locale = locale;
            Diagnostics = diagnostics;
        }

        public bool Success { get; }
        public Locale Locale { get; }
        public IReadOnlyList<LocaleParsingDiagnostic> Diagnostics { get; }
    }
}
