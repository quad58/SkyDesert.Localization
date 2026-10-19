namespace SkyDesert.Localization.Translation
{
    public sealed class TranslationUnclosedBracketException : TranslationException
    {
        public TranslationUnclosedBracketException(string key) : base("Unclosed bracket.", key)
        {

        }
    }
}
