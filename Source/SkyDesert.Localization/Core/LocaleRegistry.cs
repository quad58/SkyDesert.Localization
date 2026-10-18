using System;
using System.Collections.Generic;
using System.Linq;

namespace SkyDesert.Localization.Core
{
    public sealed class LocaleRegistry : ILocaleProvider, ILocaleChangeNotifier
    {
        public LocaleRegistry()
        {
            _Locales = new Dictionary<string, Locale>();
        }

        public LocaleRegistry(IEnumerable<Locale> locales)
        {
            _Locales = locales.ToDictionary((locale) => locale.LanguageCode);
        }

        public Locale Current { get; private set; }

        public string CurrentLanguage
        {
            get => Current.LanguageCode;
            set
            {
                if (Current != null && Current.LanguageCode == value) return;
                if (!_Locales.TryGetValue(value, out Locale locale))
                {
                    throw new KeyNotFoundException($"Locale with code {value} not found.");
                }
                Current = locale;
                OnLocaleChange?.Invoke(Current);
            }
        }

        public event Action<Locale> OnLocaleChange;

        private Dictionary<string, Locale> _Locales;
        public IReadOnlyDictionary<string, Locale> Locales => _Locales;

        public void AddLocale(Locale locale)
        {
            _Locales.Add(locale.LanguageCode, locale);
        }

        public void AddLocales(IEnumerable<Locale> locales)
        {
            foreach (Locale locale in locales)
            {
                AddLocale(locale);
            }
        }

        public bool ContainsLocale(string code)
        {
            return _Locales.ContainsKey(code);
        }

        public bool ContainsLocale(Locale locale)
        {
            return _Locales.ContainsKey(locale.LanguageCode);
        }

        public bool RemoveLocale(string code)
        {
            return _Locales.Remove(code);
        }

        public bool RemoveLocale(Locale locale)
        {
            return _Locales.Remove(locale.LanguageCode);
        }
    }
}
