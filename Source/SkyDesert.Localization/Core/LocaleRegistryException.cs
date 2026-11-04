using System.Collections.Generic;
using System.Linq;

namespace SkyDesert.Localization.Core
{
    public class LocaleRegistryException : LocalizationException
    {
        public LocaleRegistryException(string message, string localeIdentifier) : base(message)
        {
            LocaleIdentifier = localeIdentifier;
        }

        public string LocaleIdentifier { get; }

        protected override IEnumerable<Detail> Details => base.Details.Append(new Detail(nameof(LocaleIdentifier), LocaleIdentifier));
    }
}
