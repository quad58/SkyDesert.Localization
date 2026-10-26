using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace SkyDesert.Localization.Core
{
    public sealed class LocaleRegistry : ILocaleProvider, ILocaleChangeNotifier
    {
        public LocaleRegistry(IEnumerable<Locale> locales, string defaultLanguageCode)
        {
            _Locales = new ConcurrentDictionary<string, Locale>(locales.ToDictionary((locale) => locale.LanguageCode));
            SetLocale(defaultLanguageCode);
        }

        public Locale CurrentLocale { get; private set; }

        public event Action<Locale> OnLocaleChange;

        private ConcurrentDictionary<string, Locale> _Locales;
        public IEnumerable<Locale> Locales => _Locales.Values.ToList();

        public void SetLocale(string languageCode)
        {
            if (CurrentLocale != null && CurrentLocale.LanguageCode == languageCode) return;
            CurrentLocale = GetLocale(languageCode);
            OnLocaleChange?.Invoke(CurrentLocale);
        }

        public bool TryAddLocale(Locale locale)
        {
            if (locale == null) throw new ArgumentNullException(nameof(locale));

            return _Locales.TryAdd(locale.LanguageCode, locale);
        }

        public void AddLocale(Locale locale)
        {
            if (!TryAddLocale(locale)) throw new LocaleAlreadyExistsException(locale.LanguageCode);
        }

        public void AddLocales(IEnumerable<Locale> locales)
        {
            foreach (Locale locale in locales)
            {
                AddLocale(locale);
            }
        }

        public bool ContainsLocale(string languageCode)
        {
            return _Locales.ContainsKey(languageCode);
        }

        public bool ContainsLocale(Locale locale)
        {
            if (locale == null) throw new ArgumentNullException(nameof(locale));

            return ContainsLocale(locale.LanguageCode);
        }

        public bool TryGetLocale(string languageCode, out Locale locale)
        {
            return _Locales.TryGetValue(languageCode, out locale);
        }

        public Locale GetLocale(string languageCode)
        {
            if (!TryGetLocale(languageCode, out Locale locale)) throw new LocaleNotFoundException(languageCode);

            return locale;
        }

        public bool TryRemoveLocale(string languageCode)
        {
            if (CurrentLocale.LanguageCode == languageCode) return false;

            return _Locales.TryRemove(languageCode, out _);
        }

        public bool TryRemoveLocale(Locale locale)
        {
            if (locale == null) throw new ArgumentNullException(nameof(locale));

            return TryRemoveLocale(locale.LanguageCode);
        }

        public void RemoveLocale(string languageCode)
        {
            if (CurrentLocale.LanguageCode == languageCode) throw new CannotRemoveCurrentLocaleException(languageCode);

            if (!TryRemoveLocale(languageCode)) throw new LocaleNotFoundException(languageCode);
        }

        public void RemoveLocale(Locale locale)
        {
            if (locale == null) throw new ArgumentNullException(nameof(locale));

            RemoveLocale(locale.LanguageCode);
        }
    }
}
