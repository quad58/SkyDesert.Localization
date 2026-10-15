namespace SkyDesert.Localization
{
    public class LocaleParsingDiagnostic
    {
        public LocaleParsingDiagnostic(LocaleParsingDiagnosticDescriptor descriptor, LocaleParsingLocation location)
        {
            Descriptor = descriptor;
            Location = location;
        }

        public LocaleParsingDiagnosticDescriptor Descriptor { get; }
        public LocaleParsingLocation Location { get; }

        public override string ToString()
        {
            return $"{Location}: {Descriptor}";
        }
    }
}
