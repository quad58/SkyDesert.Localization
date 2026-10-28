namespace SkyDesert.Localization
{
    public interface ITranslator
    {
        bool TryTranslate(string key, out string translation);
    }
}
