using SkyDesert.Extensions;
using System.Collections.Generic;

namespace SkyDesert.Localization
{
    public class Locale
    {
        public Locale()
        {
            _Translations = new Dictionary<string, string>();
        }

        public Locale(string code)
        {
            Code = code;
            _Translations = new Dictionary<string, string>();
        }

        public Locale(string code, IEnumerable<KeyValuePair<string, string>> translations)
        {
            Code = code;
            _Translations = new Dictionary<string, string>(translations);
        }

        public string Code { get; protected set; } // Should be an IETF BCP 47 language tag 

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
    }
}
