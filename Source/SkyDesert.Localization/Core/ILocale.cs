namespace SkyDesert.Localization.Core
{
    public interface ILocale : ITranslationSource
    {
        string Identifier { get; }
    }
}
