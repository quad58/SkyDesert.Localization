using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace SkyDesert.Localization.Core
{
    public sealed class LocaleRegistry : ILocaleProvider, ILocaleChangeNotifier
    {
        public LocaleRegistry(IEnumerable<ILocale> locales, string defaultLocale)
        {
            _Locales = new ConcurrentDictionary<string, ILocale>(locales.ToDictionary((locale) => locale.Identifier));
            SetLocale(defaultLocale);
        }

        public ILocale CurrentLocale { get; private set; }

        public event Action<ILocale> OnLocaleChange;

        private ConcurrentDictionary<string, ILocale> _Locales;
        public IEnumerable<ILocale> Locales => _Locales.Values.ToList();

        public void SetLocale(string localeIdentifier)
        {
            if (CurrentLocale != null && CurrentLocale.Identifier == localeIdentifier) return;
            CurrentLocale = GetLocale(localeIdentifier);
            OnLocaleChange?.Invoke(CurrentLocale);
        }

        public bool TryAddLocale(ILocale locale)
        {
            if (locale == null) throw new ArgumentNullException(nameof(locale));

            return _Locales.TryAdd(locale.Identifier, locale);
        }

        public void AddLocale(ILocale locale)
        {
            if (!TryAddLocale(locale)) throw new LocaleAlreadyExistsException(locale.Identifier);
        }

        public void AddLocales(IEnumerable<ILocale> locales)
        {
            foreach (ILocale locale in locales)
            {
                AddLocale(locale);
            }
        }

        public bool ContainsLocale(string localeIdentifier)
        {
            return _Locales.ContainsKey(localeIdentifier);
        }

        public bool ContainsLocale(ILocale locale)
        {
            if (locale == null) throw new ArgumentNullException(nameof(locale));

            return ContainsLocale(locale.Identifier);
        }

        public bool TryGetLocale(string localeIdentifier, out ILocale locale)
        {
            return _Locales.TryGetValue(localeIdentifier, out locale);
        }

        public ILocale GetLocale(string localeIdentifier)
        {
            if (!TryGetLocale(localeIdentifier, out ILocale locale)) throw new LocaleNotFoundException(localeIdentifier);

            return locale;
        }

        public bool TryRemoveLocale(string localeIdentifier)
        {
            if (CurrentLocale.Identifier == localeIdentifier) return false;

            return _Locales.TryRemove(localeIdentifier, out _);
        }

        public bool TryRemoveLocale(ILocale locale)
        {
            if (locale == null) throw new ArgumentNullException(nameof(locale));

            return TryRemoveLocale(locale.Identifier);
        }

        public void RemoveLocale(string localeIdentifier)
        {
            if (CurrentLocale.Identifier == localeIdentifier) throw new CannotRemoveCurrentLocaleException(localeIdentifier);

            if (!TryRemoveLocale(localeIdentifier)) throw new LocaleNotFoundException(localeIdentifier);
        }

        public void RemoveLocale(ILocale locale)
        {
            if (locale == null) throw new ArgumentNullException(nameof(locale));

            RemoveLocale(locale.Identifier);
        }
    }
}
