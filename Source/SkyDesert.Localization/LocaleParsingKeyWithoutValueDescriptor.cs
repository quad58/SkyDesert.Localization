namespace SkyDesert.Localization
{
    public sealed class LocaleParsingKeyWithoutValueDescriptor : LocaleParsingDiagnosticDescriptor
    {
        public override string Message => "Key has no value (missing equals symbol).";
    }
}
