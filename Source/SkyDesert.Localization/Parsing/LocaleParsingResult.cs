using SkyDesert.Localization.Core;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace SkyDesert.Localization.Parsing
{
    public sealed class LocaleParsingResult
    {
        public LocaleParsingResult(bool success, ILocale locale, IEnumerable<LocaleParsingDiagnostic> diagnostics)
        {
            Success = success;
            Locale = locale;
            Diagnostics = new ReadOnlyCollection<LocaleParsingDiagnostic>(diagnostics.ToList());
        }

        public bool Success { get; }
        public ILocale Locale { get; }
        public IReadOnlyList<LocaleParsingDiagnostic> Diagnostics { get; }
    }
}
