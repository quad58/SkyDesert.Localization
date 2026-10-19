using System.Collections.Generic;
using System.Linq;

namespace SkyDesert.Localization.Translation
{
    public abstract class TranslationException : LocalizationException
    {
        public TranslationException(string message, string key) : base(message)
        {
            Key = key;
        }

        public string Key { get; }

        protected override IEnumerable<Detail> Details => base.Details.Append(new Detail(nameof(Key), Key));
    }
}
