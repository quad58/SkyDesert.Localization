using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SkyDesert.Localization.Core
{
    public sealed class Locale : ITranslationSource
    {
        public Locale(string identifier)
        {
            Identifier = identifier;
            _Translations = new ConcurrentDictionary<string, string>();
        }

        public Locale(string identifier, IEnumerable<KeyValuePair<string, string>> translations)
        {
            Identifier = identifier;
            _Translations = new ConcurrentDictionary<string, string>(translations);
        }

        public string Identifier { get; }

        private ConcurrentDictionary<string, string> _Translations;

        public IReadOnlyDictionary<string, string> GetTranslationsSnapshot()
        {
            return new ReadOnlyDictionary<string, string>(new Dictionary<string, string>(_Translations));
        }

        public bool TryAddTranslation(string key, string translation)
        {
            return _Translations.TryAdd(key, translation);
        }

        public void AddOrUpdateTranslation(string key, string translation)
        {
            _Translations.AddOrUpdate(key, translation, (_key, oldTranslation) => translation);
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
            return _Translations.TryRemove(key, out _);
        }

        public bool TryGetTranslation(string key, out string translation)
        {
            return _Translations.TryGetValue(key, out translation);
        }

        public override string ToString()
        {
            return Identifier;
        }
    }
}
