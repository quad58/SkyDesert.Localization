using SkyDesert.Extensions;
using System.Collections.Generic;

namespace SkyDesert.Localization
{
    public class Locale : ITranslationSource
    {
        public Locale(string code)
        {
            LanguageCode = code;
            _Translations = new Dictionary<string, string>();
        }

        public Locale(string code, IEnumerable<KeyValuePair<string, string>> translations)
        {
            LanguageCode = code;
            _Translations = new Dictionary<string, string>(translations);
        }

        public string LanguageCode { get; } // Should be an IETF BCP 47 language tag 

        protected Dictionary<string, string> _Translations;
        public IReadOnlyDictionary<string, string> Translations => _Translations;

        public bool TryAddTranslation(string key, string translation)
        {
            return _Translations.TryAdd(key, translation);
        }

        public bool AddOrUpdateTranslation(string key, string translation)
        {
            return _Translations.AddOrUpdate(key, translation);
        }

        public void AddOrUpdateTranslations(IEnumerable<KeyValuePair<string, string>> translations)
        {
            foreach (KeyValuePair<string, string> translation in translations)
            {
                _Translations.AddOrUpdate(translation.Key, translation.Value);
            }
        }

        public bool RemoveTranslation(string key)
        {
            return _Translations.Remove(key);
        }

        public bool TryGetTranslation(string key, out string translation)
        {
            if (Translations.TryGetValue(key, out translation))
            {
                return true;
            }
            return false;
        }

        public string GetTranslation(string key)
        {
            if (!Translations.TryGetValue(key, out string translation))
            {
                throw new TranslationNotFoundException(key);
            }
            return translation;
        }

        public override string ToString()
        {
            return LanguageCode;
        }
    }
}
