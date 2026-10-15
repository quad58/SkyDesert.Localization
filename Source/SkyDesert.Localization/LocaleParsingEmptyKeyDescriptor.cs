namespace SkyDesert.Localization
{
    public sealed class LocaleParsingEmptyKeyDescriptor : LocaleParsingDiagnosticDescriptor
    {
        public override string Message => "Value does not have a key (nothing before equals symbol).";
    }
}
