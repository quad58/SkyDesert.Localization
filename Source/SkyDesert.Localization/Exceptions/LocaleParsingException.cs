using System.Collections.Generic;

namespace SkyDesert.Localization
{
    public class LocaleParsingException : LocalizationException
    {
        public LocaleParsingException(LocaleParsingDiagnosticDescriptor descriptor, int character) : base("Locale parsing error.")
        {
            Descriptor = descriptor;
            Character = character;
        }

        public LocaleParsingDiagnosticDescriptor Descriptor { get; }
        public int Character { get; }

        public override string Message => Descriptor.Message;

        protected override IEnumerable<Detail> Details
        {
            get
            {
                yield return new Detail(nameof(Character), Character.ToString());
            }
        }
    }
}
