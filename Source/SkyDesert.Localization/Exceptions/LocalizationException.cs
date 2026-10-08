using System;

namespace SkyDesert.Localization
{
    public class LocalizationException : Exception
    {
        public LocalizationException() : base(DefaultMessage)
        {

        }

        public LocalizationException(string message) : base(message)
        {

        }

        public LocalizationException(string message, Exception innerException) : base(message, innerException)
        {

        }

        private const string DefaultMessage = "Localization error.";

        public override string Message => DefaultMessage;
    }
}
