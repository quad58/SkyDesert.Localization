namespace SkyDesert.Localization
{
    public interface ITranslationSource
    {
        public bool TryGetTranslation(string key, out string translation);

        public string GetTranslation(string key);
    }
}
