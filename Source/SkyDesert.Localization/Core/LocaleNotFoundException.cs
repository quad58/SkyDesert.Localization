namespace SkyDesert.Localization.Core
{
    public sealed class LocaleNotFoundException : LocaleRegistryException
    {
        public LocaleNotFoundException(string localeIdentifier) : base("Locale not found.", localeIdentifier)
        {

        }
    }
}
