using System;

namespace SkyDesert.Localization
{
    public class TranslationException : Exception
    {
        public TranslationException(TranslationErrorCode errorCode) : base(GetMessage(errorCode))
        {
            ErrorCode = errorCode;
        }

        public readonly TranslationErrorCode ErrorCode;

        protected static string GetMessage(TranslationErrorCode errorCode)
        {
            throw new NotImplementedException();
        }
    }
}
