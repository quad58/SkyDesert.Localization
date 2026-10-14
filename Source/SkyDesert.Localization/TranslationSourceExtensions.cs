namespace SkyDesert.Localization
{
    public static class TranslationSourceExtensions
    {
        public static string GetTranslation(this ITranslationSource translationSource, string key)
        {
            if (!translationSource.TryGetTranslation(key, out string translation))
            {
                throw new TranslationNotFoundException(key);
            }
            return translation;
        }
    }
}
