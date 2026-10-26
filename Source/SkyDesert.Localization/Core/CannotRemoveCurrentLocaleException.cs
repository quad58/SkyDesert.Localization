namespace SkyDesert.Localization.Core
{
    public sealed class CannotRemoveCurrentLocaleException : LocaleRegistryException
    {
        public CannotRemoveCurrentLocaleException(string languageCode) : base("Cannot remove current locale.", languageCode)
        {

        }
    }
}
