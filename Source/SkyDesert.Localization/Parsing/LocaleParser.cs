using SkyDesert.Localization.Core;
using SkyDesert.Localization.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SkyDesert.Localization.Parsing
{
    public class LocaleParser : ILocaleParser, ILocaleValidator
    {
        private delegate void LineResolver(string input, int line);
        protected delegate void DiagnosticDescriptorHandler(LocaleParsingDiagnosticDescriptor descriptor, int character);
        private delegate void DiagnosticHandler(LocaleParsingDiagnostic diagnostic);

        public LocaleParsingResult Parse(string input, string localeIdentifier)
        {
            if (localeIdentifier == null) throw new ArgumentNullException(nameof(localeIdentifier));

            Dictionary<string, string> translations = new Dictionary<string, string>();
            List<LocaleParsingDiagnostic> diagnostics = new List<LocaleParsingDiagnostic>();
            bool success = true;

            ResolveLines(input, (lineInput, line) =>
            {
                ParseLine(lineInput, out string key, out string translation, (descriptor, character) =>
                {
                    diagnostics.Add(new LocaleParsingDiagnostic(descriptor, new LocaleParsingTextLocation(line, character)));
                    success = false;
                });
                translations[key] = translation;
            });

            Locale locale = new Locale(localeIdentifier, translations);
            return new LocaleParsingResult(success, locale, diagnostics);
        }

        public IReadOnlyList<LocaleParsingDiagnostic> Parse(string input, ILocaleBuilder localeBuilder)
        {
            List<LocaleParsingDiagnostic> diagnostics = new List<LocaleParsingDiagnostic>();

            ResolveLines(input, (lineInput, line) =>
            {
                ParseLine(lineInput, out string key, out string translation, (descriptor, character) =>
                {
                    diagnostics.Add(new LocaleParsingDiagnostic(descriptor, new LocaleParsingTextLocation(line, character)));
                });
                localeBuilder.AddOrUpdateTranslation(key, translation);
            });

            return new ReadOnlyCollection<LocaleParsingDiagnostic>(diagnostics);
        }

        public IReadOnlyList<LocaleParsingDiagnostic> Validate(string input)
        {
            List<LocaleParsingDiagnostic> diagnostics = new List<LocaleParsingDiagnostic>();

            ResolveLines(input, (lineInput, line) =>
            {
                ParseLine(lineInput, out string key, out string translation, (descriptor, character) =>
                {
                    diagnostics.Add(new LocaleParsingDiagnostic(descriptor, new LocaleParsingTextLocation(line, character)));
                });
            });

            return new ReadOnlyCollection<LocaleParsingDiagnostic>(diagnostics);
        }

        private void ResolveLines(string input, LineResolver resolveLine)
        {
            if (input == null) throw new ArgumentNullException(nameof(input));

            string[] lines = input.Split(new string[] { "\n", "\r", "\r\n" }, StringSplitOptions.None);
            for (int i = 0; i < lines.Length; i++)
            {
                string line = RemoveComment(lines[i]);
                if (string.IsNullOrWhiteSpace(line)) continue;
                resolveLine(line, i);
            }
        }

        public void ParseLine(string input, out string key, out string translation)
        {
            ParseLine(input, out key, out translation, (descriptor, character) => throw new LocaleParsingException(descriptor, new LocaleParsingCharacterLocation(character)));
        }

        public bool TryParseLine(string input, out string key, out string translation)
        {
            bool success = true;
            ParseLine(input, out key, out translation, (descriptor, character) => success = false);
            return success;
        }

        protected virtual void ParseLine(string input, out string key, out string translation, DiagnosticDescriptorHandler handleDiagnosticDescriptor)
        {
            if (input == null) throw new ArgumentNullException(nameof(input));
            if (string.IsNullOrWhiteSpace(input)) throw new ArgumentException("Value cannot be empty or white space.", nameof(input));

            key = null;
            translation = null;

            int equalsIndex = input.IndexOf('=', true);
            if (equalsIndex == -1)
            {
                handleDiagnosticDescriptor(new LocaleParsingKeyWithoutValueDescriptor(), input.TrimEnd().Length);
                return;
            }

            key = input.Substring(0, equalsIndex).TrimStart();
            if (string.IsNullOrWhiteSpace(key))
            {
                handleDiagnosticDescriptor(new LocaleParsingEmptyKeyDescriptor(), equalsIndex);
                return;
            }

            translation = input.Substring(equalsIndex + 1).TrimEnd();
            return;
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
