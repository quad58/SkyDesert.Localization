namespace SkyDesert.Localization.Translation
{
    public sealed class TranslationArgumentsWithoutKeyNameException : TranslationException
    {
        public TranslationArgumentsWithoutKeyNameException(string key) : base("Arguments provided without a key name.", key)
        {

        }
    }
}
