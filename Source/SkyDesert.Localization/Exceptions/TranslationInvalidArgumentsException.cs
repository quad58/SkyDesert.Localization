namespace SkyDesert.Localization
{
    public class TranslationInvalidArgumentsException : LocalizationException
    {
        public TranslationInvalidArgumentsException(TranslationInvalidArgumentsErrorCode errorCode, string argument) : base("Translation arguments are invalid.")
        {
            ErrorCode = errorCode;
            Argument = argument;
        }

        public TranslationInvalidArgumentsErrorCode ErrorCode { get; }
        public string Argument { get; }

        public override string Message
        {
            get
            {
                switch (ErrorCode)
                {
                    case TranslationInvalidArgumentsErrorCode.ArgumentWithoutValue:
                        return "Argument has no value (missing equals symbol).";
                    case TranslationInvalidArgumentsErrorCode.ArgumentDuplicate:
                        return "Argument has duplicate.";
                }
                return "Unknown error.";
            }
        }
    }
}
