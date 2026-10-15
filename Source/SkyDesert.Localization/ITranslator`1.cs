namespace SkyDesert.Localization
{
    public interface ITranslator<TOptions> : ITranslator
    {
        public bool TryTranslate(string key, out string translation, TOptions options);
    }
}
