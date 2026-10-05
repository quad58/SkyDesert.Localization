using SkyDesert.Extensions;
using System;
using System.Collections.Generic;

namespace SkyDesert.Localization
{
    public class Translator
    {
        public Translator(Locale locale)
        {
            Locale = locale ?? throw new ArgumentNullException(nameof(locale));
        }

        protected readonly Locale Locale;

        public string Translate(string key, string fallback, bool parseArguments = true, int maxRecursionDepth = 32)
        {
            if (fallback == null) throw new ArgumentNullException(nameof(fallback));

            if (TryTranslate(key, out string translation, parseArguments, maxRecursionDepth))
            {
                return translation;
            }

            return fallback;
        }

        public bool TryTranslate(string key, out string translation, bool parseArguments = true, int maxRecursionDepth = 32)
        {
            try
            {
                translation = Translate(key, parseArguments, maxRecursionDepth);
                return true;
            }
            catch (TranslationException)
            {
                translation = null;
                return false;
            }
        }

        public string Translate(string key, bool parseArguments = true, int maxRecursionDepth = 32)
        {
            string translation = TranslateCore(key, parseArguments, maxRecursionDepth);
            translation = translation.Replace(@"\(", "(");
            translation = translation.Replace(@"\)", ")");
            translation = translation.Replace(@"\=", "=");
            translation = translation.Replace(@"\,", ",");
            translation = translation.Replace(@"\{", "{");
            translation = translation.Replace(@"\}", "}");
            translation = translation.Replace(@"\\", "\\");
            return translation;
        }

        protected virtual string TranslateCore(string key, bool parseArguments, int maxRecursionDepth)
        {
            if (key == null) throw new ArgumentNullException(nameof(key));
            if (string.IsNullOrWhiteSpace(key)) throw new ArgumentException("Value cannot be empty or white space.", nameof(key));

            string translation;

            if (!parseArguments)
            {
                return TranslateRecursive(GetTranslation(key), parseArguments, maxRecursionDepth);
            }

            int openBracketIndex = key.IndexOf('(', true);

            if (openBracketIndex == -1)
            {
                return TranslateRecursive(GetTranslation(key), parseArguments, maxRecursionDepth);
            }

            if (key.IndexOf(')', true) != key.Length - 1)
            {
                throw new TranslationException(TranslationErrorCode.UnclosedBracket);
            }

            string keyName = key.Substring(0, openBracketIndex).Trim();

            if (string.IsNullOrEmpty(keyName))
            {
                throw new TranslationException(TranslationErrorCode.EmptyKey);
            }

            translation = GetTranslation(keyName);

            Dictionary<string, string> arguments = ParseArguments(key.Substring(openBracketIndex + 1, key.Length - openBracketIndex - 2));

            translation = translation.ReplacePlaceholders(placeholderName =>
            {
                if (arguments.TryGetValue(placeholderName, out string argumentValue))
                {
                    return argumentValue;
                }
                return '{' + placeholderName + '}';
            }, true);

            return TranslateRecursive(translation, parseArguments, maxRecursionDepth);
        }

        protected string GetTranslation(string key)
        {
            if (!Locale.Translations.TryGetValue(key, out string translation))
            {
                throw new TranslationException(TranslationErrorCode.KeyNotFound);
            }
            return translation;
        }

        protected virtual Dictionary<string, string> ParseArguments(string input)
        {
            string[] arguments = input.Split(',', true);
            Dictionary<string, string> argumentPairs = new Dictionary<string, string>();

            foreach (string argument in arguments)
            {
                int equalsIndex = argument.IndexOf('=', true);
                if (equalsIndex == -1)
                {
                    throw new TranslationException(TranslationErrorCode.ArgumentWithoutValue);
                }

                string argumentName = argument.Substring(0, equalsIndex).Trim();
                string argumentValue = argument.Substring(equalsIndex + 1, argument.Length - equalsIndex - 1).Trim();

                if (!argumentPairs.TryAdd(argumentName, argumentValue))
                {
                    throw new TranslationException(TranslationErrorCode.ArgumentDuplicate);
                }
            }

            return argumentPairs;
        }

        public string TranslatePlaceholders(string input, bool parseArguments = true, int maxRecursionDepth = 32)
        {
            if (input.IndexOf('{', true) == -1)
            {
                // input has no placeholders
                return input;
            }
            return input.ReplacePlaceholders(placeholderName =>
            {
                return TranslateCore(placeholderName, parseArguments, maxRecursionDepth);
            }, true);
        }

        protected string TranslateRecursive(string input, bool parseArguments, int currentDepth)
        {
            if (currentDepth <= 0)
                return input;

            return TranslatePlaceholders(input, parseArguments, currentDepth - 1);
        }
    }
}
