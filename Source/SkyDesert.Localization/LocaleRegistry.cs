using System;
using System.Collections.Generic;
using UnityEngine;

namespace SkyDesert.Localization
{
    [CreateAssetMenu(menuName = "Localization/Locale Registry", fileName = "LocaleRegistry")]
    public sealed class LocaleRegistry : ScriptableObject, ILocaleProvider
    {
        public Locale CurrentLocale { get; private set; }

        public string CurrentLanguage
        {
            get => CurrentLocale.Code;
            set
            {
                Locale newLocale = Locales[value];
                if (CurrentLocale == null || CurrentLocale.Code != value)
                {
                    OnLocaleChange?.Invoke(newLocale);
                }
                CurrentLocale = newLocale;
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
