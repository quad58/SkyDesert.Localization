namespace SkyDesert.Localization
{
    public class TranslationExceptionContext
    {
        public TranslationExceptionContext(TranslationErrorCode errorCode, string key, Locale locale = null)
        {
            ErrorCode = errorCode;
            Key = key;
            Locale = locale;
        }

        public readonly TranslationErrorCode ErrorCode;
        public readonly string Key;
        public readonly Locale Locale;
    }
}
