namespace SkyDesert.Localization.Core
{
    public sealed class LocaleAlreadyExistsException : LocaleRegistryException
    {
        public LocaleAlreadyExistsException(string localeIdentifier) : base("Locale already exists.", localeIdentifier)
        {

        }
    }
}
