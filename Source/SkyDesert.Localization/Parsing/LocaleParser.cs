using SkyDesert.Localization.Core;
using SkyDesert.Localization.Extensions;
using System;
using System.Collections.Generic;

namespace SkyDesert.Localization.Parsing
{
    public class LocaleParser : ILocaleParser, ILocaleValidator
    {
        private delegate void LineResolver(string key, string transltion);
        private delegate void ExceptionResolver(LocaleParsingException exception, int line);

        public LocaleParsingResult Parse(string input, string localeIdentifier)
        {
            if (localeIdentifier == null) throw new ArgumentNullException(nameof(localeIdentifier));

            Locale locale = new Locale(localeIdentifier);
            List<LocaleParsingDiagnostic> diagnostics = new List<LocaleParsingDiagnostic>();
            bool success = true;

            ParseLines(input, (key, translation) =>
            {
                locale.AddOrUpdateTranslation(key, translation);
            }, (exception, line) =>
            {
                diagnostics.Add(CreateDiagnostic(exception, line));
                success = false;
            });

            return new LocaleParsingResult(success, locale, diagnostics);
        }

        public IReadOnlyList<LocaleParsingDiagnostic> Validate(string input)
        {
            List<LocaleParsingDiagnostic> diagnostics = new List<LocaleParsingDiagnostic>();

            ParseLines(input, (key, transltion) => { }, (exception, line) =>
            {
                diagnostics.Add(CreateDiagnostic(exception, line));
            });

            return diagnostics;
        }

        private LocaleParsingDiagnostic CreateDiagnostic(LocaleParsingException exception, int line)
        {
            if (exception.Location is LocaleParsingCharacterLocation characterLocation)
            {
                return new LocaleParsingDiagnostic(exception.Descriptor, new LocaleParsingTextLocation(line, characterLocation.Character));
            }
            return new LocaleParsingDiagnostic(exception.Descriptor, exception.Location);
        }

        private void ParseLines(string input, LineResolver resolveLine, ExceptionResolver resolveException)
        {
            if (input == null) throw new ArgumentNullException(nameof(input));

            string[] lines = input.Split(new string[] { "\n", "\r\n" }, StringSplitOptions.None);
            for (int i = 0; i < lines.Length; i++)
            {
                string line = RemoveComment(lines[i]);
                if (string.IsNullOrWhiteSpace(line)) continue;

                try
                {
                    ParseLine(line, out string key, out string translation);
                    resolveLine(key, translation);
                }
                catch (LocaleParsingException exception)
                {
                    resolveException(exception, i);
                }
            }
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
                throw new LocaleParsingException(new LocaleParsingKeyWithoutValueDescriptor(), new LocaleParsingCharacterLocation(input.Length));
            }

            key = input.Substring(0, equalsIndex).Trim();
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new LocaleParsingException(new LocaleParsingEmptyKeyDescriptor(), new LocaleParsingCharacterLocation(equalsIndex));
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
