using System;

namespace SkyDesert.Localization.Translation
{
    public static class TranslatorExtensions
    {
        // NOTE: Duplicated logic can not be extracted because methods use different interfaces.

        public static string Translate(this ITranslator translator, string key)
        {
            if (!translator.TryTranslate(key, out string translation))
            {
                throw new TranslationNotFoundException(key);
            }

            return translation;
        }

        public static string Translate(this ITranslator translator, string key, string fallback)
        {
            if (fallback == null) throw new ArgumentNullException(nameof(fallback));

            if (translator.TryTranslate(key, out string translation))
            {
                return translation;
            }

            return fallback;
        }

        public static string Translate<TOptions>(this ITranslator<TOptions> translator, string key, TOptions options)
        {
            if (!translator.TryTranslate(key, out string translation, options))
            {
                throw new TranslationNotFoundException(key);
            }

            return translation;
        }

        public static string Translate<TOptions>(this ITranslator<TOptions> translator, string key, string fallback, TOptions options)
        {
            if (fallback == null) throw new ArgumentNullException(nameof(fallback));

            if (translator.TryTranslate(key, out string translation, options))
            {
                return translation;
            }

            return fallback;
        }
    }
}
