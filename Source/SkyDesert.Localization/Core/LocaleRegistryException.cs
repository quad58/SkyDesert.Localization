using System.Collections.Generic;
using System.Linq;

namespace SkyDesert.Localization.Core
{
    public class LocaleRegistryException : LocalizationException
    {
        public LocaleRegistryException(string message, string languageCode) : base(message)
        {
            LanguageCode = languageCode;
        }

        public string LanguageCode { get; }

        protected override IEnumerable<Detail> Details => base.Details.Append(new Detail(nameof(LanguageCode), LanguageCode));
    }
}
