using SkyDesert.Extensions;
using System;

namespace SkyDesert.Localization
{
    public class LocaleParser
    {
        public Locale Parse(string input, string localeCode)
        {
            if (input == null) throw new ArgumentNullException(nameof(input));
            if (localeCode == null) throw new ArgumentNullException(nameof(localeCode));

            Locale locale = new Locale(localeCode);

            string[] lines = input.Split(new string[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < lines.Length; i++)
            {
                string line = RemoveComment(lines[i]);
                if (!string.IsNullOrEmpty(line) && TryParseLine(line, out string key, out string translation))
                {
                    locale.AddOrUpdateTranslation(key, translation);
                }
            }

            return locale;
        }

        public bool TryParseLine(string input, out string key, out string translation)
        {
            try
            {
                ParseLine(input, out key, out translation);
                return true;
            }
            catch (LocaleParsingException)
            {
                key = null;
                translation = null;
                return false;
            }
        }

        public virtual void ParseLine(string input, out string key, out string translation)
        {
            if (input == null) throw new ArgumentNullException(nameof(input));
            if (string.IsNullOrWhiteSpace(input)) throw new ArgumentException("Value cannot be empty or white space.", nameof(input));

            int equalsIndex = input.IndexOf('=', true);
            if (equalsIndex == -1)
            {
                throw new LocaleParsingException(LocaleParsingErrorCode.MissingEqualsSymbol, input.Length - 1);
            }

            key = input.Substring(0, equalsIndex);
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new LocaleParsingException(LocaleParsingErrorCode.MissingKey, 0);
            }

            translation = input.Substring(equalsIndex + 1);
        }

        protected virtual string RemoveComment(string input)
        {
            int sharpIndex = input.IndexOf('#', true);
            if (sharpIndex != -1)
            {
                input = input.Substring(0, sharpIndex);
            }
            return input;
        }
    }
}
