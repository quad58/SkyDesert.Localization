using System.Collections.Generic;

namespace SkyDesert.Localization.Core
{
    public interface ILocaleBuilder
    {
        bool TryAddTranslation(string key, string translation);
        void AddOrUpdateTranslation(string key, string translation);
        void AddOrUpdateTranslations(IEnumerable<KeyValuePair<string, string>> translations);
        bool RemoveTranslation(string key);
        void RemoveTranslations(IEnumerable<string> keys);
        ILocale Build(string identifier);
    }
}
