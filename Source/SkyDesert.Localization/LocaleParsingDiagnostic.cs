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

        public LocaleParsingDiagnostic(LocaleParsingErrorCode errorCode, string message, int line, int column) :
            this(errorCode, message, new LocaleParsingStringLocation(line, column))
        {

        }

        public LocaleParsingDiagnostic(LocaleParsingException localeParsingException, int line) :
            this(localeParsingException.ErrorCode, localeParsingException.Message, line, localeParsingException.Column)
        {

        }

        public LocaleParsingErrorCode ErrorCode { get; }
        public LocaleParsingLocation Location;
        public string Message { get; }
    }
}
