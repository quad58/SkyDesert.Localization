using SkyDesert.Localization.Core;
using System.Collections.Generic;

namespace SkyDesert.Localization.Parsing
{
    public sealed class LocaleParsingResult
    {
        public LocaleParsingResult(bool success, ILocale locale, IReadOnlyList<LocaleParsingDiagnostic> diagnostics)
        {
            Success = success;
            Locale = locale;
            Diagnostics = diagnostics;
        }

        public bool Success { get; }
        public ILocale Locale { get; }
        public IReadOnlyList<LocaleParsingDiagnostic> Diagnostics { get; }
    }
}
