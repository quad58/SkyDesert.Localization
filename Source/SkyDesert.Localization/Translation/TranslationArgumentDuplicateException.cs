namespace SkyDesert.Localization.Translation
{
    public sealed class TranslationArgumentDuplicateException : TranslationArgumentException
    {
        public TranslationArgumentDuplicateException(string key, string argument) : base("Argument has duplicate.", key, argument)
        {

        }
    }
}
