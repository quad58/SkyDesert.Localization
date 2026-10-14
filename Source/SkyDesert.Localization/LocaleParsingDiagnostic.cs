namespace SkyDesert.Localization
{
    public class LocaleParsingDiagnostic
    {
        public LocaleParsingDiagnostic(LocaleParsingErrorCode errorCode, string message, int line, int column)
        {
            ErrorCode = errorCode;
            Message = message;
            Line = line;
            Column = column;
        }


        public LocaleParsingDiagnostic(LocaleParsingException localeParsingException, int line) :
            this(localeParsingException.ErrorCode, localeParsingException.Message, line, localeParsingException.Column)
        {
            Line = line;
        }

        public LocaleParsingErrorCode ErrorCode { get; }
        public int Line { get; }
        public int Column { get; }
        public string Message { get; }
    }
}
