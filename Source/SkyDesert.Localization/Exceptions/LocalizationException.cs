// TODO: Rewrite on StringBuilder

using System;
using System.Collections.Generic;

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

        protected virtual IEnumerable<Detail> Details { get; }

        public override string ToString()
        {
            string output = GetType().FullName;
            if (!string.IsNullOrEmpty(Message))
            {
                output += $": {Message}";
            }

            using IEnumerator<Detail> enumerator = Details.GetEnumerator();
            if (enumerator.MoveNext())
            {
                output += " (";
                output += enumerator.Current.ToString();

                while (enumerator.MoveNext())
                {
                    output += ", ";
                    output += enumerator.Current.ToString();
                }
                output += ")";
            }

            if (InnerException != null)
            {
                output += " ---> " + InnerException.ToString();
                output += Environment.NewLine + "   --- End of inner exception stack trace ---";
            }

            output += Environment.NewLine + StackTrace;

            return output;
        }

        protected struct Detail
        {
            public Detail(string name, string value)
            {
                Name = name;
                Value = value;
            }

            public string Name { get; }
            public string Value { get; }

            public override string ToString()
            {
                return $"{Name}: {Value}";
            }
        }
    }
}
