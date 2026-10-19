namespace SkyDesert.Localization.Translation
{
    public sealed class TranslationArgumentWithoutValueException : TranslationArgumentException
    {
        public TranslationArgumentWithoutValueException(string key, string argument) : base("Argument has no value (missing equals symbol).", key, argument)
        {

        }
    }
}
