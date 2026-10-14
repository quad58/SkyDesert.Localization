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
                    case LocaleParsingErrorCode.KeyWithoutValue:
                        return "Key has no value (missing equals symbol).";
                    case LocaleParsingErrorCode.EmptyKey:
                        return "Value does not have a key (nothing before equals symbol).";
                }
                return "Unknown error.";
            }
        }
    }
}
