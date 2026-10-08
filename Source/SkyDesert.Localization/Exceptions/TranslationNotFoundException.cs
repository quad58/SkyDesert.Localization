namespace SkyDesert.Localization
{
    public class TranslationNotFoundException : LocalizationException
    {
        public TranslationNotFoundException(string key) : base("Translation not found.")
        {
            Key = key;
        }

        public string Key { get; }

        public override string Message => $"Translation with key \"{Key}\" not found.";
    }
}
