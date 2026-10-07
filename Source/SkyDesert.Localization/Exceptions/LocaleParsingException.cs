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

        public LocaleParsingErrorCode ErrorCode { get; }
        public int Column { get; }

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
