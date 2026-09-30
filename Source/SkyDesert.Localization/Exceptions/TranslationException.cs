using System;

namespace SkyDesert.Localization
{
    public class TranslationException : Exception
    {
        public enum ErrorCode
        {
            KeyNotFound
        }
    }
}
