namespace SkyDesert.Localization.Core
{
    public sealed class LocaleAlreadyExistsException : LocaleRegistryException
    {
        public LocaleAlreadyExistsException(string languageCode) : base("Locale already exists.", languageCode)
        {

        }
    }
}
