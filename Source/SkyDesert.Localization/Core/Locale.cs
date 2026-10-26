using SkyDesert.Localization.Extensions;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SkyDesert.Localization.Core
{
    public sealed class Locale : ITranslationSource
    {
        public Locale(string code)
        {
            LanguageCode = code;
            _Translations = new ConcurrentDictionary<string, string>();
        }

        public Locale(string code, IEnumerable<KeyValuePair<string, string>> translations)
        {
            LanguageCode = code;
            _Translations = new ConcurrentDictionary<string, string>(translations);
        }

        public string LanguageCode { get; } // Should be an IETF BCP 47 language tag 

        private ConcurrentDictionary<string, string> _Translations;
        public IReadOnlyDictionary<string, string> Translations => new ReadOnlyDictionary<string, string>(new Dictionary<string, string>(_Translations));

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
            return _Translations.TryRemove(key, out _);
        }

        public bool TryGetTranslation(string key, out string translation)
        {
            if (_Translations.TryGetValue(key, out translation))
            {
                return true;
            }
            return false;
        }

        public override string ToString()
        {
            return LanguageCode;
        }
    }
}
