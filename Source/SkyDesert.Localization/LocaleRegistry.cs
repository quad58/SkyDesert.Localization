using System;
using System.Collections.Generic;
using System.Linq;

namespace SkyDesert.Localization
{
    public sealed class LocaleRegistry : ILocaleProvider, ILocaleChangeNotifier
    {
        public LocaleRegistry()
        {
            _Locales = new Dictionary<string, Locale>();
        }

        public LocaleRegistry(IEnumerable<Locale> locales)
        {
            _Locales = locales.ToDictionary((locale) => locale.Code);
        }

        public Locale Current { get; private set; }

        public string CurrentLanguage
        {
            get => Current.Code;
            set
            {
                if (Current != null && Current.Code == value) return;
                Current = Locales[value];
                OnLocaleChange?.Invoke(Current);
            }
        }

        public event Action<Locale> OnLocaleChange;

        private Dictionary<string, Locale> _Locales;
        public IReadOnlyDictionary<string, Locale> Locales => _Locales;

        public void AddLocale(Locale locale)
        {
            _Locales.Add(locale.Code, locale);
        }

        public void AddLocales(IEnumerable<Locale> locales)
        {
            foreach (Locale locale in locales)
            {
                AddLocale(locale);
            }
        }

        public bool RemoveLocale(string code)
        {
            return _Locales.Remove(code);
        }

        public bool RemoveLocale(Locale locale)
        {
            return _Locales.Remove(locale.Code);
        }
    }
}
