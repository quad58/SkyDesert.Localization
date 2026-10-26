namespace SkyDesert.Localization.Core
{
    public sealed class LocaleNotFoundException : LocaleRegistryException
    {
        public LocaleNotFoundException(string languageCode) : base("Locale not found.", languageCode)
        {

        }
    }
}
