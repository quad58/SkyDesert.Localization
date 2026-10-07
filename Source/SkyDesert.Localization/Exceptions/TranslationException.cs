using System;

namespace SkyDesert.Localization
{
    public class TranslationException : Exception
    {
        public TranslationException(TranslationErrorCode errorCode, string key) : base(GetMessage(errorCode, key))
        {
            ErrorCode = errorCode;
            Key = key;
        }

        public TranslationException(TranslationArgumentsParsingException innerException, string key) :
            base(GetMessage(TranslationErrorCode.InvalidArguments, key), innerException)
        {
            ErrorCode = TranslationErrorCode.InvalidArguments;
            Key = key;
        }

        public TranslationErrorCode ErrorCode { get; }
        public string Key { get; }

        protected static string GetMessage(TranslationErrorCode errorCode, string key)
        {
            string message = $" Key: \"{key}\"";
            switch (errorCode)
            {
                case TranslationErrorCode.UnclosedBracket:
                    return "Unclosed bracket or text after bracket." + message;
                case TranslationErrorCode.ArgumentsWithoutKeyName:
                    return "Arguments provided without a key name." + message;
            }
            return "Unknown error." + message;
        }
    }
}
