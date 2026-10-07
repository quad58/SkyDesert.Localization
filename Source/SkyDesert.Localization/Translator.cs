using SkyDesert.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace SkyDesert.Localization
{
    public class Translator
    {
        public Translator(Locale locale)
        {
            Locale = locale ?? throw new ArgumentNullException(nameof(locale));
        }

        protected Locale Locale { get; }

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
            if (!TryTranslateCore(key, out translation, parseArguments, maxRecursionDepth))
            {
                return false;
            }
            translation = Unescape(translation);
            return true;
        }

        public string Translate(string key, bool parseArguments = true, int maxRecursionDepth = 32)
        {
            if (!TryTranslateCore(key, out string translation, parseArguments, maxRecursionDepth))
            {
                throw new TranslationKeyNotFoundException(key, Locale);
            }
            return Unescape(translation);
        }

        protected string Unescape(string input)
        {
            if (input == null) throw new ArgumentNullException(nameof(input));

            StringBuilder stringBuilder = new StringBuilder(input);
            stringBuilder.Replace(@"\(", "(");
            stringBuilder.Replace(@"\)", ")");
            stringBuilder.Replace(@"\=", "=");
            stringBuilder.Replace(@"\,", ",");
            stringBuilder.Replace(@"\{", "{");
            stringBuilder.Replace(@"\}", "}");
            stringBuilder.Replace(@"\\", "\\");
            return stringBuilder.ToString();
        }

        protected virtual bool TryTranslateCore(string key, out string translation, bool parseArguments, int maxRecursionDepth)
        {
            if (key == null) throw new ArgumentNullException(nameof(key));
            if (string.IsNullOrWhiteSpace(key)) throw new ArgumentException("Value cannot be empty or white space.", nameof(key));

            int openBracketIndex = -1;
            if (parseArguments)
            {
                openBracketIndex = key.IndexOf('(', true);
            }

            if (openBracketIndex == -1)
            {
                if (!TryGetTranslation(key, out translation))
                {
                    return false;
                }
                translation = TranslateRecursive(translation, parseArguments, maxRecursionDepth);
                return true;
            }

            if (key.IndexOf(')', true) != key.Length - 1)
            {
                throw new TranslationException(TranslationErrorCode.UnclosedBracket, key);
            }

            string keyName = key.Substring(0, openBracketIndex).Trim();

            if (string.IsNullOrEmpty(keyName))
            {
                throw new TranslationException(TranslationErrorCode.ArgumentsWithoutKeyName, key);
            }

            if (!TryGetTranslation(keyName, out translation))
            {
                return false;
            }

            try
            {
                Dictionary<string, string> arguments = ParseArguments(key.Substring(openBracketIndex + 1, key.Length - openBracketIndex - 2));

                translation = translation.ReplacePlaceholders(placeholderName =>
                {
                    if (arguments.TryGetValue(placeholderName, out string argumentValue))
                    {
                        return argumentValue;
                    }
                    return '{' + placeholderName + '}';
                }, true);

                translation = TranslateRecursive(translation, parseArguments, maxRecursionDepth);
                return true;
            }
            catch (TranslationArgumentsParsingException translationArgumentsParsingException)
            {
                throw new TranslationException(translationArgumentsParsingException, key);
            }
        }

        protected bool TryGetTranslation(string key, out string translation)
        {
            if (Locale.Translations.TryGetValue(key, out translation))
            {
                return true;
            }
            return false;
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
                    throw new TranslationArgumentsParsingException(TranslationArgumentsParsingErrorCode.ArgumentWithoutValue, argument);
                }

                string argumentName = argument.Substring(0, equalsIndex).Trim();
                string argumentValue = argument.Substring(equalsIndex + 1, argument.Length - equalsIndex - 1).Trim();

                if (!argumentPairs.TryAdd(argumentName, argumentValue))
                {
                    throw new TranslationArgumentsParsingException(TranslationArgumentsParsingErrorCode.ArgumentDuplicate, argument);
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
                if (TryTranslateCore(placeholderName, out string translation, parseArguments, maxRecursionDepth))
                {
                    return translation;
                }
                return '{' + placeholderName + '}';
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
