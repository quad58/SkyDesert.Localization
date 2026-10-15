using System.Collections.Generic;

namespace SkyDesert.Localization
{
    public class TranslationException : LocalizationException
    {
        public TranslationException(TranslationErrorCode errorCode, string key) : base(DefaultMessage)
        {
            ErrorCode = errorCode;
            Key = key;
        }

        public TranslationException(TranslationInvalidArgumentsException innerException, string key) : base(DefaultMessage, innerException)
        {
            ErrorCode = TranslationErrorCode.InvalidArguments;
            Key = key;
        }

        private const string DefaultMessage = "Translation error.";

        public TranslationErrorCode ErrorCode { get; }
        public string Key { get; }

        public override string Message {
            get
            {
                switch (ErrorCode)
                {
                    case TranslationErrorCode.UnclosedBracket:
                        return "Unclosed bracket.";
                    case TranslationErrorCode.TextAfterBracket:
                        return "Text after bracket.";
                    case TranslationErrorCode.ArgumentsWithoutKeyName:
                        return "Arguments provided without a key name.";
                    case TranslationErrorCode.InvalidArguments:
                        return "Arguments are invalid.";
                }
                return "Unknown error.";
            }
        }

        protected override IEnumerable<Detail> Details
        {
            get
            {
                yield return new Detail(nameof(Key), Key.ToString());
            }
        }
    }
}
