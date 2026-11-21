using System.Collections.Generic;

namespace SkyDesert.Localization.Core
{
    public class LocaleBuilder : ILocaleBuilder
    {
        public LocaleBuilder()
        {

        }

        public LocaleBuilder(IEnumerable<KeyValuePair<string, string>> translations)
        {
            AddOrUpdateTranslations(translations);
        }

        private Dictionary<string, string> Translations = new Dictionary<string, string>();

        public bool TryAddTranslation(string key, string translation)
        {
            if (Translations.ContainsKey(key))
            {
                return false;
            }
            Translations.Add(key, translation);
            return true;
        }

        public void AddOrUpdateTranslation(string key, string translation)
        {
            Translations[key] = translation;
        }

        public void AddOrUpdateTranslations(IEnumerable<KeyValuePair<string, string>> translations)
        {
            foreach (KeyValuePair<string, string> translation in translations)
            {
                AddOrUpdateTranslation(translation.Key, translation.Value);
            }
        }

        public bool RemoveTranslation(string key)
        {
            return Translations.Remove(key);
        }

        public void RemoveTranslations(IEnumerable<string> keys)
        {
            foreach (string key in keys)
            {
                RemoveTranslation(key);
            }
        }

        public ILocale Build(string identifier)
        {
            return new Locale(identifier, Translations);
        }
    }
}
