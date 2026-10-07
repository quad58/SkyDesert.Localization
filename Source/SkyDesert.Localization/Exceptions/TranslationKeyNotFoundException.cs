using System;

namespace SkyDesert.Localization
{
    public class TranslationKeyNotFoundException : Exception
    {
        public TranslationKeyNotFoundException(string key, Locale locale) : base("Translation not found.")
        {
            Key = key;
            Locale = locale;
        }

        public string Key { get; }
        public Locale Locale { get; }

        public override string Message => $"Translation with key \"{Key}\" not found in \"{Locale}\" locale.";
    }
}
