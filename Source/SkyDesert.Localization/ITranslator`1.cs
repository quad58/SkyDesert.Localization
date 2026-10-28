namespace SkyDesert.Localization
{
    public interface ITranslator<TOptions> : ITranslator
    {
        bool TryTranslate(string key, out string translation, TOptions options);
    }
}
