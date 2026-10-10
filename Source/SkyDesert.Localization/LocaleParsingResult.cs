using System.Collections.Generic;

namespace SkyDesert.Localization
{
    public class LocaleParsingResult
    {
        public LocaleParsingResult(Locale locale, IReadOnlyList<LocaleParsingDiagnostic> diagnostics)
        {
            Success = diagnostics.Count > 0;
            Locale = locale;
            Diagnostics = diagnostics;
        }

        public bool Success { get; }
        public Locale Locale { get; }
        public IReadOnlyList<LocaleParsingDiagnostic> Diagnostics { get; }
    }
}
