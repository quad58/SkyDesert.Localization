namespace SkyDesert.Localization
{
    public class TranslationArgumentsParsingException : LocalizationException
    {
        public TranslationArgumentsParsingException(TranslationArgumentsParsingErrorCode errorCode, string argument) : base("Translation arguments parsing error.")
        {
            ErrorCode = errorCode;
            Argument = argument;
        }

        public TranslationArgumentsParsingErrorCode ErrorCode { get; }
        public string Argument { get; }

        public override string Message
        {
            get
            {
                string additionalInfo = $" Argument: \"{Argument}\".";
                switch (ErrorCode)
                {
                    case TranslationArgumentsParsingErrorCode.ArgumentWithoutValue:
                        return "Argument without a value." + additionalInfo;
                    case TranslationArgumentsParsingErrorCode.ArgumentDuplicate:
                        return "Argument duplicate." + additionalInfo;
                }
                return "Unknown error." + additionalInfo;
            }
        }
    }
}
