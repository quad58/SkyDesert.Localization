using System.Collections.Generic;
using System.Linq;

namespace SkyDesert.Localization.Parsing
{
    public sealed class LocaleParsingException : LocalizationException
    {
        public LocaleParsingException(LocaleParsingDiagnosticDescriptor descriptor, LocaleParsingLocation location) : base(descriptor.Message)
        {
            Descriptor = descriptor;
            Location = location;
        }

        public LocaleParsingDiagnosticDescriptor Descriptor { get; }
        public LocaleParsingLocation Location { get; }

        protected override IEnumerable<Detail> Details => base.Details.Append(new Detail(nameof(Location), Location.ToString()));
    }
}
