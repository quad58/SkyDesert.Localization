namespace SkyDesert.Localization
{
    public class TranslationException : LocalizationException
    {
        public TranslationException(TranslationErrorCode errorCode, string key) : base(DefaultMessage)
        {
            ErrorCode = errorCode;
            Key = key;
        }

        public TranslationException(TranslationArgumentsParsingException innerException, string key) : base(DefaultMessage, innerException)
        {
            ErrorCode = TranslationErrorCode.InvalidArguments;
            Key = key;
        }

        private const string DefaultMessage = "Translation error.";

        public TranslationErrorCode ErrorCode { get; }
        public string Key { get; }

        public override string Message {
            get
            {
                string additionalInfo = $" Key: \"{Key}\"";
                switch (ErrorCode)
                {
                    case TranslationErrorCode.UnclosedBracket:
                        return "Unclosed bracket or text after bracket." + additionalInfo;
                    case TranslationErrorCode.ArgumentsWithoutKeyName:
                        return "Arguments provided without a key name." + additionalInfo;
                }
                return "Unknown error." + additionalInfo;
            }
        }
    }
}
