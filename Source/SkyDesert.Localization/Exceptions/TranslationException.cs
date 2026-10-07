using System;

namespace SkyDesert.Localization
{
    public class TranslationException : Exception
    {
        public TranslationException(TranslationErrorCode errorCode) : base()
        {
            ErrorCode = errorCode;
        }

        public TranslationException(TranslationErrorCode errorCode, string key = null, Locale locale = null) :
            base(GetMessage(new TranslationExceptionContext(errorCode, key, locale)))
        {
            ErrorCode = errorCode;
            Key = key;
            Locale = locale;
        }

        public readonly TranslationErrorCode ErrorCode;
        public readonly string Key;
        public readonly Locale Locale;

        protected static string GetMessage(TranslationExceptionContext context)
        {
            switch (context.ErrorCode)
            {
                case TranslationErrorCode.KeyNotFound:
                    return $"Translation with key \"{context.Key}\" not found in \"{context.Locale}\" locale.";
                case TranslationErrorCode.UnclosedBracket:
                    return $"Unclosed bracket in key \"{context.Key}\".";
                case TranslationErrorCode.ArgumentsWithoutKeyName:
                    return $"Arguments provided without a key name in key \"{context.Key}\".";
                case TranslationErrorCode.ArgumentWithoutValue:
                    return $"Argument without a value in key \"{context.Key}\".";
                case TranslationErrorCode.ArgumentDuplicate:
                    return $"Argument duplicate in key \"{context.Key}\".";
            }
            return null;
        }
    }
}
