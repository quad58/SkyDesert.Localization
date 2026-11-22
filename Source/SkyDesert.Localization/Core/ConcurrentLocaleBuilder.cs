using System.Collections.Concurrent;
using System.Collections.Generic;

namespace SkyDesert.Localization.Core
{
    public class ConcurrentLocaleBuilder : ILocaleBuilder
    {
        public ConcurrentLocaleBuilder()
        {

        }

        public ConcurrentLocaleBuilder(IEnumerable<KeyValuePair<string, string>> translations)
        {
            AddOrUpdateTranslations(translations);
        }

        private ConcurrentDictionary<string, string> Translations = new ConcurrentDictionary<string, string>();

        public bool TryAddTranslation(string key, string translation)
        {
            return Translations.TryAdd(key, translation);
        }

        public void AddOrUpdateTranslation(string key, string translation)
        {
            Translations.AddOrUpdate(key, translation, (_key, oldTranslation) => translation);
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
            return Translations.TryRemove(key, out _);
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
