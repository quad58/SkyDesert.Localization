using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace SkyDesert.Localization.Core
{
    public sealed class Locale : ILocale
    {
        public Locale(string identifier, IEnumerable<KeyValuePair<string, string>> translations)
        {
            Identifier = identifier;
            Translations = new ReadOnlyDictionary<string, string>(translations.ToDictionary(kvp => kvp.Key, kvp => kvp.Value));
        }

        public string Identifier { get; }

        public IReadOnlyDictionary<string, string> Translations { get; }

        public bool TryGetTranslation(string key, out string translation)
        {
            return Translations.TryGetValue(key, out translation);
        }

        public override string ToString()
        {
            return Identifier;
        }
    }
}
