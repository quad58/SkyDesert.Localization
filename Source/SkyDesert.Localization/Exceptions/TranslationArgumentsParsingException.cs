using System;

namespace SkyDesert.Localization
{
    public class TranslationArgumentsParsingException : Exception
    {
        public TranslationArgumentsParsingException(TranslationArgumentsParsingErrorCode errorCode, string argument) : base(GetMessage(errorCode, argument))
        {
            ErrorCode = errorCode;
            Argument = argument;
        }

        public TranslationArgumentsParsingErrorCode ErrorCode { get; }
        public string Argument { get; }

        protected static string GetMessage(TranslationArgumentsParsingErrorCode errorCode, string argument)
        {
            string message = $" Argument: \"{argument}\".";
            switch (errorCode)
            {
                case TranslationArgumentsParsingErrorCode.ArgumentWithoutValue:
                    return "Argument without a value." + message;
                case TranslationArgumentsParsingErrorCode.ArgumentDuplicate:
                    return "Argument duplicate." + message;
            }
            return "Unknown error." + message;
        }
    }
}
