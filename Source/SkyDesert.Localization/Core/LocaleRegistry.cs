using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace SkyDesert.Localization.Core
{
    public sealed class LocaleRegistry : ILocaleProvider, ILocaleChangeNotifier
    {
        public LocaleRegistry(IEnumerable<Locale> locales, string defaultLocale)
        {
            _Locales = new ConcurrentDictionary<string, Locale>(locales.ToDictionary((locale) => locale.Identifier));
            SetLocale(defaultLocale);
        }

        public Locale CurrentLocale { get; private set; }

        public event Action<Locale> OnLocaleChange;

        private ConcurrentDictionary<string, Locale> _Locales;
        public IEnumerable<Locale> Locales => _Locales.Values.ToList();

        public void SetLocale(string localeIdentifier)
        {
            if (CurrentLocale != null && CurrentLocale.Identifier == localeIdentifier) return;
            CurrentLocale = GetLocale(localeIdentifier);
            OnLocaleChange?.Invoke(CurrentLocale);
        }

        public bool TryAddLocale(Locale locale)
        {
            if (locale == null) throw new ArgumentNullException(nameof(locale));

            return _Locales.TryAdd(locale.Identifier, locale);
        }

        public void AddLocale(Locale locale)
        {
            if (!TryAddLocale(locale)) throw new LocaleAlreadyExistsException(locale.Identifier);
        }

        public void AddLocales(IEnumerable<Locale> locales)
        {
            foreach (Locale locale in locales)
            {
                AddLocale(locale);
            }
        }

        public bool ContainsLocale(string localeIdentifier)
        {
            return _Locales.ContainsKey(localeIdentifier);
        }

        public bool ContainsLocale(Locale locale)
        {
            if (locale == null) throw new ArgumentNullException(nameof(locale));

            return ContainsLocale(locale.Identifier);
        }

        public bool TryGetLocale(string localeIdentifier, out Locale locale)
        {
            return _Locales.TryGetValue(localeIdentifier, out locale);
        }

        public Locale GetLocale(string localeIdentifier)
        {
            if (!TryGetLocale(localeIdentifier, out Locale locale)) throw new LocaleNotFoundException(localeIdentifier);

            return locale;
        }

        public bool TryRemoveLocale(string localeIdentifier)
        {
            if (CurrentLocale.Identifier == localeIdentifier) return false;

            return _Locales.TryRemove(localeIdentifier, out _);
        }

        public bool TryRemoveLocale(Locale locale)
        {
            if (locale == null) throw new ArgumentNullException(nameof(locale));

            return TryRemoveLocale(locale.Identifier);
        }

        public void RemoveLocale(string localeIdentifier)
        {
            if (CurrentLocale.Identifier == localeIdentifier) throw new CannotRemoveCurrentLocaleException(localeIdentifier);

            if (!TryRemoveLocale(localeIdentifier)) throw new LocaleNotFoundException(localeIdentifier);
        }

        public void RemoveLocale(Locale locale)
        {
            if (locale == null) throw new ArgumentNullException(nameof(locale));

            RemoveLocale(locale.Identifier);
        }
    }
}
