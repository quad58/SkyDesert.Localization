namespace SkyDesert.Localization
{
    public class LocaleParsingDiagnostic
    {
        public LocaleParsingDiagnostic(LocaleParsingErrorCode errorCode, string message, LocaleParsingLocation location)
        {
            ErrorCode = errorCode;
            Message = message;
            Location = location;
        }

        public LocaleParsingErrorCode ErrorCode { get; }
        public LocaleParsingLocation Location { get; }
        public string Message { get; }

        public override string ToString()
        {
            return $"{Location}: {Message}";
        }
    }
}
