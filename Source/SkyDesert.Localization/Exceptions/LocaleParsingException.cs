using System;

namespace SkyDesert.Localization
{
    public class LocaleParsingException : Exception
    {
        public LocaleParsingException(LocaleParsingErrorCode errorCode, int column) : base(GetMessage(errorCode, column))
        {
            ErrorCode = errorCode;
            Column = column;
        }

        public readonly LocaleParsingErrorCode ErrorCode;
        public readonly int Column;

        protected static string GetMessage(LocaleParsingErrorCode errorCode, int column)
        {
            switch (errorCode)
            {
                case LocaleParsingErrorCode.MissingEqualsSymbol:
                    return "";
                case LocaleParsingErrorCode.MissingKey:
                    return "";
            }
            return null;
        }
    }
}
