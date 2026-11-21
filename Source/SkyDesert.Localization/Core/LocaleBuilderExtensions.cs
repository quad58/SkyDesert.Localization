namespace SkyDesert.Localization.Core
{
    public static class LocaleBuilderExtensions
    {
        public static void AddOrUpdateTranslationsFromLocale(this ILocaleBuilder localeBuilder, Locale locale)
        {
            localeBuilder.AddOrUpdateTranslations(locale.Translations);
        }
    }
}
