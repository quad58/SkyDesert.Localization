namespace SkyDesert.Localization.Parsing
{
    public abstract class LocaleParsingDiagnosticDescriptor
    {
        public abstract string Message { get; }

        public override string ToString()
        {
            return Message;
        }
    }
}
