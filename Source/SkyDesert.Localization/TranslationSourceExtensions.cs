namespace SkyDesert.Localization
{
    public static class TranslationSourceExtensions
    {
        public static bool ContainsTranslation(this ITranslationSource translationSource, string key)
        {
            return translationSource.TryGetTranslation(key, out _);
        }
    }
}
