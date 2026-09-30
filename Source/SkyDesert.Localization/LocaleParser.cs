// TODO: Exceptions

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
            if (string.IsNullOrWhiteSpace(line) || line[0] == '#')
            {
                key = null;
                translation = null;
                return false;
            }
            else
            {
                int equalsIndex = line.IndexOf('=');
                key = line.Substring(0, equalsIndex);
                translation = line.Substring(equalsIndex + 1);
                return true;
            }
        }
    }
}
