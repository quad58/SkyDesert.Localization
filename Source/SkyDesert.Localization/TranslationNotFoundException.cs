using System.Collections.Generic;

namespace SkyDesert.Localization
{
    public sealed class TranslationNotFoundException : LocalizationException
    {
        public TranslationNotFoundException(string key) : base("Translation not found.")
        {
            Key = key;
        }

        public string Key { get; }

        public override string Message => "Translation not found.";

        protected override IEnumerable<Detail> Details
        {
            get
            {
                yield return new Detail(nameof(Key), Key.ToString());
            }
        }
    }
}
