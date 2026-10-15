using SkyDesert.Extensions;
using System;
using System.Collections.Generic;

namespace SkyDesert.Localization
{
    public class LocaleParser
    {
        private delegate void LineResolver(string key, string transltion);
        private delegate void ExceptionResolver(LocaleParsingException exception, int line);

        public LocaleParsingResult Parse(string input, string localeCode)
        {
            if (localeCode == null) throw new ArgumentNullException(nameof(localeCode));

            Locale locale = new Locale(localeCode);
            List<LocaleParsingDiagnostic> diagnostics = new List<LocaleParsingDiagnostic>();
            bool success = true;

            ParseLines(input, (key, translation) =>
            {
                locale.AddOrUpdateTranslation(key, translation);
            }, (exception, line) =>
            {
                diagnostics.Add(CreateStringDiagnostic(exception, line));
                success = false;
            });

            return new LocaleParsingResult(success, locale, diagnostics);
        }

        public IReadOnlyList<LocaleParsingDiagnostic> Validate(string input)
        {
            List<LocaleParsingDiagnostic> diagnostics = new List<LocaleParsingDiagnostic>();

            ParseLines(input, (_, _) => { }, (exception, line) =>
            {
                diagnostics.Add(CreateStringDiagnostic(exception, line));
            });

            return diagnostics;
        }

        private LocaleParsingDiagnostic CreateStringDiagnostic(LocaleParsingException exception, int line)
        {
            return new LocaleParsingDiagnostic(exception.Descriptor, new LocaleParsingStringLocation(line, exception.Character));
        }

        private void ParseLines(string input, LineResolver resolveLine, ExceptionResolver resolveException)
        {
            if (input == null) throw new ArgumentNullException(nameof(input));

            string[] lines = input.Split(new string[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < lines.Length; i++)
            {
                string line = RemoveComment(lines[i]);
                if (string.IsNullOrEmpty(line)) continue;

                try
                {
                    ParseLine(line, out string key, out string translation);
                    resolveLine(key, translation);
                }
                catch (LocaleParsingException exception)
                {
                    resolveException(exception, i + 1);
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
                throw new LocaleParsingException(new LocaleParsingKeyWithoutValueDescriptor(), input.Length - 1);
            }

            key = input.Substring(0, equalsIndex);
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new LocaleParsingException(new LocaleParsingEmptyKeyDescriptor(), 0);
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
