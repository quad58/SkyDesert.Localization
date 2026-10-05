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

        public string Translate(string key, string fallback, bool formatArguments = true, int maxRecursionDepth = 32)
        {
            if (fallback == null) throw new ArgumentNullException(nameof(fallback));

            if (TryTranslate(key, out string translation, formatArguments, maxRecursionDepth))
            {
                return translation;
            }

            return fallback;
        }

        public bool TryTranslate(string key, out string translation, bool formatArguments = true, int maxRecursionDepth = 32)
        {
            try
            {
                translation = Translate(key, formatArguments, maxRecursionDepth);
                return true;
            }
            catch (ArgumentException)
            {
                throw;
            }
            catch
            {
                translation = null;
                return false;
            }
        }

        public string Translate(string key, bool formatArguments = true, int maxRecursionDepth = 32)
        {
            string translation = TranslateCore(key, formatArguments, maxRecursionDepth);
            translation = translation.Replace(@"\(", "(");
            translation = translation.Replace(@"\)", ")");
            translation = translation.Replace(@"\=", "=");
            translation = translation.Replace(@"\,", ",");
            translation = translation.Replace(@"\{", "{");
            translation = translation.Replace(@"\}", "}");
            translation = translation.Replace(@"\\", "\\");
            return translation;
        }

        // TODO: Exceptions
        protected virtual string TranslateCore(string key, bool formatArguments, int maxRecursionDepth) // <- rename to TranslateStep
        {
            if (key == null) throw new ArgumentNullException(nameof(key));
            if (string.IsNullOrWhiteSpace(key)) throw new ArgumentException("Value cannot be empty or white space.", nameof(key));

            string translation;

            if (!formatArguments)
            {
                if (!Locale.Translations.TryGetValue(key, out translation))
                {
                    throw new TranslationException(TranslationErrorCode.KeyNotFound);
                }
                return TranslateRecursive(translation, formatArguments, maxRecursionDepth);
            }

            int openBracketIndex = key.IndexOf('(', true);

            if (openBracketIndex == -1)
            {
                if (!Locale.Translations.TryGetValue(key, out translation))
                {
                    throw new TranslationException(TranslationErrorCode.KeyNotFound);
                }
                return TranslateRecursive(translation, formatArguments, maxRecursionDepth);
            }

            if (key.IndexOf(')', true) != key.Length - 1)
            {
                throw new TranslationException(TranslationErrorCode.UclosedBracket);
            }

            string keyName = key.Substring(0, openBracketIndex).Trim();

            if (string.IsNullOrEmpty(keyName))
            {
                throw new TranslationException(TranslationErrorCode.EmptyKey);
            }

            if (!Locale.Translations.TryGetValue(keyName, out translation))
            {
                throw new TranslationException(TranslationErrorCode.KeyNotFound);
            }

            string[] rawArguments = key.Substring(openBracketIndex + 1, key.Length - openBracketIndex - 2).Split(',', true);
            Dictionary<string, string> arguments = new Dictionary<string, string>();

            foreach (string argument in rawArguments)
            {
                int equalsIndex = argument.IndexOf('=', true);
                if (equalsIndex == -1)
                {
                    throw new TranslationException(TranslationErrorCode.ArgumentWithoutValue);
                }

                string argumentName = argument.Substring(0, equalsIndex).Trim();
                string argumentValue = argument.Substring(equalsIndex + 1, argument.Length - equalsIndex - 1).Trim();

                if (!arguments.TryAdd(argumentName, argumentValue))
                {
                    throw new TranslationException(TranslationErrorCode.ArgumentDuplicate);
                }   
            }

            translation = translation.ReplacePlaceholders(placeholderName =>
            {
                if (arguments.TryGetValue(placeholderName, out string argumentValue))
                {
                    return argumentValue;
                }
                return '{' + placeholderName + '}';
            }, true);

            return TranslateRecursive(translation, formatArguments, maxRecursionDepth);
        }

        public string TranslateString(string input, bool formatArguments = true, int maxRecursionDepth = 32)
        {
            if (input.IndexOf('{', true) == -1)
            {
                // input has no placeholders
                return input;
            }
            return input.ReplacePlaceholders(placeholderName =>
            {
                return TranslateCore(placeholderName, formatArguments, maxRecursionDepth);
            }, true);
        }

        protected string TranslateRecursive(string input, bool formatArguments, int currentDepth)
        {
            if (currentDepth <= 0)
                return input;

            return TranslateString(input, formatArguments, currentDepth - 1);
        }
    }
}
