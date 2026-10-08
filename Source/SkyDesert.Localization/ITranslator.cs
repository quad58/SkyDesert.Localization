namespace SkyDesert.Localization
{
    public interface ITranslator
    {
        public bool TryTranslate(string key, out string translation);

        public string Translate(string key);

        public string Translate(string key, string fallback);

        public string TranslatePlaceholders(string input);
    }
}
