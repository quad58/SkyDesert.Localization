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

        public LocaleParsingDiagnostic(LocaleParsingErrorCode errorCode, string message, int line, int character) :
            this(errorCode, message, new LocaleParsingStringLocation(line, character))
        {

        }

        public LocaleParsingDiagnostic(LocaleParsingException localeParsingException, int line) :
            this(localeParsingException.ErrorCode, localeParsingException.Message, line, localeParsingException.Character)
        {

        }

        public LocaleParsingErrorCode ErrorCode { get; }
        public LocaleParsingLocation Location;
        public string Message { get; }
    }
}
