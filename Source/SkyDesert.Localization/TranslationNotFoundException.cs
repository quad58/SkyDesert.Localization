using System.Collections.Generic;
using System.Linq;

namespace SkyDesert.Localization
{
    public sealed class TranslationNotFoundException : LocalizationException
    {
        public TranslationNotFoundException(string key) : base("Translation not found.")
        {
            Key = key;
        }

        public string Key { get; }

        protected override IEnumerable<Detail> Details => base.Details.Append(new Detail(nameof(Key), Key));
    }
}
