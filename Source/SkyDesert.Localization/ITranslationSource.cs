namespace SkyDesert.Localization
{
    public interface ITranslationSource
    {
        bool TryGetTranslation(string key, out string translation);
    }
}
