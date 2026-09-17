using System.Collections.Generic;
using System.Text;

namespace SkyDesert.Localization
{
    public class Translator
    {
        public Translator(Locale locale)
        {
            Locale = locale;
        }

        protected readonly Locale Locale;

        public bool TryTranslate(string key, bool formatArguments, bool recursive, out string translation)
        {
            translation = Translate(key, null, formatArguments, recursive);
            return translation != null;
        }

        public bool TryTranslate(string key, out string translation)
        {
            return TryTranslate(key, true, true, out translation);
        }

        // Below is an old realisation from Sky Desert and it is probably not safe and performative at all

        public virtual string Translate(string key, string fallback, bool formatArguments, bool recursive)
        {
            string translation;
            if (formatArguments)
            {
                string[] keyArgumentPair = key.Split("(");
                keyArgumentPair[keyArgumentPair.Length - 1] = keyArgumentPair[keyArgumentPair.Length - 1].Replace(")", "");
                if (keyArgumentPair.Length > 1)
                {
                    string[] arguments = keyArgumentPair[1].Split(",");
                    key = keyArgumentPair[0];
                    if (!Locale.Translations.TryGetValue(key, out translation))
                    {
                        translation = fallback;
                    }
                    if (keyArgumentPair[1] != "")
                    {
                        foreach (string argument in arguments)
                        {
                            string[] nameValuePair = argument.Split("=");
                            string argumentName = nameValuePair[0];
                            string argumentValue = nameValuePair[1];
                            translation = translation.Replace("{" + argumentName + "}", argumentValue);
                        }
                    }
                }
                else
                {
                    if (!Locale.Translations.TryGetValue(key, out translation))
                    {
                        translation = fallback;
                    }
                }
            }
            else
            {
                if (!Locale.Translations.TryGetValue(key, out translation))
                {
                    translation = fallback;
                }
            }

            if (recursive)
            {
                if (GetKeys(translation).Count != 0)
                {
                    translation = TranslateString(translation, formatArguments, true);
                }
            }
            return translation;
        }

        public string Translate(string key, string fallback)
        {
            return Translate(key, fallback, true, true);
        }

        private List<string> GetKeys(string source)
        {
            List<string> keys = new List<string>();

            StringBuilder stringBuilder = new StringBuilder();
            bool keyStarted = false;
            foreach (char c in source)
            {
                if (c == '{')
                {
                    stringBuilder.Clear();
                    keyStarted = true;
                }
                else if (c == '}' && keyStarted)
                {
                    keys.Add(stringBuilder.ToString());
                    stringBuilder.Clear();
                    keyStarted = false;
                }
                else
                {
                    stringBuilder.Append(c);
                }
            }
            return keys;
        }

        private string TranslateString(string source, bool formatArguments, bool recursive)
        {
            List<string> keys = GetKeys(source);
            foreach (string key in keys)
            {
                source = source.Replace("{" + key + "}", Translate(key, "Error: Key \"" + key.Split("(")[0] + "\" not found", formatArguments, recursive));
            }

            if (recursive)
            {
                if (GetKeys(source).Count != 0)
                {
                    source = TranslateString(source, formatArguments, true);
                }
            }

            return source;
        }
    }
}
