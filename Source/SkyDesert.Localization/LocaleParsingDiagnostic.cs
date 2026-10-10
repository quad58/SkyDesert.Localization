namespace SkyDesert.Localization
{
    public class LocaleParsingDiagnostic
    {
        public LocaleParsingErrorCode ErrorCode { get; }
        public int Line { get; }
        public int Column { get; }
    }
}
