using System.Collections.Generic;

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

        protected override IEnumerable<Detail> Details
        {
            get
            {
                yield return new Detail(nameof(Argument), Argument.ToString());
            }
        }
    }
}
