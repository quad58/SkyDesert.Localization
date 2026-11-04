namespace SkyDesert.Localization.Core
{
    public sealed class CannotRemoveCurrentLocaleException : LocaleRegistryException
    {
        public CannotRemoveCurrentLocaleException(string localeIdentifier) : base("Cannot remove current locale.", localeIdentifier)
        {

        }
    }
}
