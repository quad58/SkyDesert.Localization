namespace SkyDesert.Localization
{
    public class LocaleParsingException : LocalizationException
    {
        public LocaleParsingException(LocaleParsingErrorCode errorCode, int column) : base("Locale parsing error.")
        {
            ErrorCode = errorCode;
            Column = column;
        }

        public LocaleParsingErrorCode ErrorCode { get; }
        public int Column { get; }

        public override string Message
        {
            get
            {
                switch (ErrorCode)
                {
                    case LocaleParsingErrorCode.MissingEqualsSymbol:
                        return "";
                    case LocaleParsingErrorCode.MissingKey:
                        return "";
                }
                return "Unknown error.";
            }
        }
    }
}
