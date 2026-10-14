using System;

namespace SkyDesert.Localization
{
    public class LocalizationException : Exception
    {
        public LocalizationException() : base("Localization error.")
        {

        }

        public LocalizationException(string message) : base(message)
        {

        }

        public LocalizationException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
