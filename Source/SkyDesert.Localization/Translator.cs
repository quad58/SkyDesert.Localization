using SkyDesert.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace SkyDesert.Localization
{
    public sealed class Translator : ITranslator<TranslationOptions>
    {
        public Translator(Locale locale)
        {
            TranslationSource = locale ?? throw new ArgumentNullException(nameof(locale));
        }

        private ITranslationSource TranslationSource { get; }

        public bool TryTranslate(string key, out string translation, TranslationOptions options)
        {
            if (!TryTranslateCore(key, out translation, options))
            {
                return false;
            }

            translation = Unescape(translation);
            return true;
        }

        public bool TryTranslate(string key, out string translation)
        {
            return TryTranslate(key, out translation, new TranslationOptions());
        }

        private string Unescape(string input)
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

        private bool TryTranslateCore(string key, out string translation, TranslationOptions options)
        {
            if (key == null) throw new ArgumentNullException(nameof(key));
            if (string.IsNullOrWhiteSpace(key)) throw new ArgumentException("Value cannot be empty or white space.", nameof(key));

            int openBracketIndex = -1;
            if (options.ParseArguments)
            {
                openBracketIndex = key.IndexOf('(', true);
            }

            if (openBracketIndex == -1)
            {
                if (!TranslationSource.TryGetTranslation(key, out translation))
                {
                    return false;
                }
                translation = TranslateRecursive(translation, options, options.MaxRecursionDepth);
                return true;
            }

            int closeBracketIndex = key.IndexOf(')', true);
            if (closeBracketIndex != key.Length - 1)
            {
                if (closeBracketIndex != -1)
                {
                    throw new TranslationException(TranslationErrorCode.TextAfterBracket, key);
                }
                throw new TranslationException(TranslationErrorCode.UnclosedBracket, key);
            }

            string keyName = key.Substring(0, openBracketIndex).Trim();

            if (string.IsNullOrEmpty(keyName))
            {
                throw new TranslationException(TranslationErrorCode.ArgumentsWithoutKeyName, key);
            }

            if (!TranslationSource.TryGetTranslation(keyName, out translation))
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

                translation = TranslateRecursive(translation, options, options.MaxRecursionDepth);
                return true;
            }
            catch (TranslationInvalidArgumentsException translationArgumentsParsingException)
            {
                throw new TranslationException(translationArgumentsParsingException, key);
            }
        }

        private Dictionary<string, string> ParseArguments(string input)
        {
            string[] arguments = input.Split(',', true);
            Dictionary<string, string> argumentPairs = new Dictionary<string, string>();

            foreach (string argument in arguments)
            {
                int equalsIndex = argument.IndexOf('=', true);
                if (equalsIndex == -1)
                {
                    throw new TranslationInvalidArgumentsException(TranslationInvalidArgumentsErrorCode.ArgumentWithoutValue, argument);
                }

                string argumentName = argument.Substring(0, equalsIndex).Trim();
                string argumentValue = argument.Substring(equalsIndex + 1, argument.Length - equalsIndex - 1).Trim();

                if (!argumentPairs.TryAdd(argumentName, argumentValue))
                {
                    throw new TranslationInvalidArgumentsException(TranslationInvalidArgumentsErrorCode.ArgumentDuplicate, argument);
                }
            }

            return argumentPairs;
        }

        private string TranslatePlaceholdersCore(string input, TranslationOptions options)
        {
            if (input.IndexOf('{', true) == -1)
            {
                // input has no placeholders
                return input;
            }
            return input.ReplacePlaceholders(placeholderName =>
            {
                if (TryTranslateCore(placeholderName, out string translation, options))
                {
                    return translation;
                }
                return '{' + placeholderName + '}';
            }, true);
        }

        public string TranslatePlaceholders(string input, TranslationOptions options)
        {
            return Unescape(TranslatePlaceholdersCore(input, options));
        }

        public string TranslatePlaceholders(string input)
        {
            return TranslatePlaceholders(input, new TranslationOptions());
        }

        private string TranslateRecursive(string input, TranslationOptions options, int currentDepth)
        {
            if (currentDepth <= 0)
                return input;

            return TranslatePlaceholdersCore(input, new TranslationOptions(options.ParseArguments, options.MaxRecursionDepth - 1));
        }
    }
}
