using System;
using System.Collections.Generic;
using System.Text;

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
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.Append(GetType().FullName);
            if (!string.IsNullOrEmpty(Message))
            {
                stringBuilder.Append($": {Message}");
            }

            using (IEnumerator<Detail> enumerator = Details.GetEnumerator())
            {
                if (enumerator.MoveNext())
                {
                    stringBuilder.Append(" (");
                    stringBuilder.Append(enumerator.Current.ToString());

                    while (enumerator.MoveNext())
                    {
                        stringBuilder.Append(", ");
                        stringBuilder.Append(enumerator.Current.ToString());
                    }
                    stringBuilder.Append(")");
                }
            }

            if (InnerException != null)
            {
                stringBuilder.Append(" ---> ");
                stringBuilder.Append(InnerException.ToString());
                stringBuilder.Append(Environment.NewLine);
                stringBuilder.Append("   --- End of inner exception stack trace ---");
            }

            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append(StackTrace);

            return stringBuilder.ToString();
        }

        protected readonly struct Detail
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
