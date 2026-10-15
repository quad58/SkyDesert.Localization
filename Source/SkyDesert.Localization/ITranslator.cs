namespace SkyDesert.Localization
{
    public interface ITranslator
    {
        public bool TryTranslate(string key, out string translation);
    }
}
