using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SkyDesert.Localization
{
    [CreateAssetMenu(menuName = "Localization/Locale Registry", fileName = "LocaleRegistry")]
    public class LocaleRegistry : ScriptableObject, ILocaleProvider
    {
        public LocaleRegistry(IEnumerable<Locale> locales)
        {
            Locales = locales.ToDictionary((locale) => locale.Code);
        }

        public Locale CurrentLocale { get; private set; }

        public string CurrentLanguage
        {
            get => CurrentLocale.Code;
            set
            {
                Locale newLocale = Locales[value];
                if (CurrentLocale.Code != value)
                {
                    OnLocaleChange.Invoke(newLocale);
                }
                CurrentLocale = newLocale;
            }
        }

        public event Action<Locale> OnLocaleChange;

        private Dictionary<string, Locale> Locales;
    }
}
