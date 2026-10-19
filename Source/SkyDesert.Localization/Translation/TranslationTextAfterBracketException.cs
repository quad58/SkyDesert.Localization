namespace SkyDesert.Localization.Translation
{
    public sealed class TranslationTextAfterBracketException : TranslationException
    {
        public TranslationTextAfterBracketException(string key) : base("Text after bracket.", key)
        {

        }
    }
}
