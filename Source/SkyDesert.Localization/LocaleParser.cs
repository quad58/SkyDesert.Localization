// TODO: Exceptions

using SkyDesert.Extensions;
using System;

namespace SkyDesert.Localization
{
    public sealed class LocaleParser
    {
        public Locale Parse(string source)
        {
            Locale locale = new Locale();

            string[] lines = source.Split(new string[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string line in lines)
            {
                if (TryParseLine(line, out string key, out string translation))
                {
                    locale.AddOrUpdateTranslation(key, translation);
                }
            }

            return locale;
        }

        private bool TryParseLine(string line, out string key, out string translation)
        {
            int sharpIndex = line.IndexOf('#', true);
            if (sharpIndex != -1)
            {
                line = line.Substring(0, sharpIndex + 1);
            }
            if (string.IsNullOrWhiteSpace(line))
            {
                key = null;
                translation = null;
                return false;
            }

            int equalsIndex = line.IndexOf('=', true);
            if (equalsIndex == -1)
            {
                throw new LocaleParsingException();
            }
            key = line.Substring(0, equalsIndex);
            translation = line.Substring(equalsIndex + 1);
            return true;
        }
    }
}
