using System.Collections.Generic;

namespace SkyDesert.Localization.Parsing
{
    public class LocaleParsingException : LocalizationException
    {
        public LocaleParsingException(LocaleParsingDiagnosticDescriptor descriptor, LocaleParsingLocation location) : base("Locale parsing error.")
        {
            Descriptor = descriptor;
            Location = location;
        }

        public LocaleParsingDiagnosticDescriptor Descriptor { get; }
        public LocaleParsingLocation Location { get; }

        public override string Message => Descriptor.Message;

        protected override IEnumerable<Detail> Details
        {
            get
            {
                yield return new Detail(nameof(Location), Location.ToString());
            }
        }
    }
}
