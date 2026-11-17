using SkyDesert.Localization.Parsing;
using System.Collections.Generic;
using Xunit;

namespace SkyDesert.Localization.Tests.Parsing
{
    public class LocaleParserValidateTests
    {
        [Fact]
        public void Validate_ValidMultilineTranslationsInput_ReturnsFullResult()
        {
            LocaleParser parser = new LocaleParser();

            IReadOnlyList<LocaleParsingDiagnostic> diagnostics = parser.Validate("key1=translation1\nkey2=translation2\rkey3=translation3\r\nkey4=translation4");

            Assert.Equal(0, diagnostics.Count);
        }

        [Fact]
        public void Parse_InvalidMultilineTranslationsInput_ReturnsFullDiagnosics()
        {
            LocaleParser parser = new LocaleParser();

            IReadOnlyList<LocaleParsingDiagnostic> diagnostics = parser.Validate("key\n  key\n  key  \n=translation\n  =translation");

            Assert.Equal(5, diagnostics.Count);

            TestDiagnostic<LocaleParsingKeyWithoutValueDescriptor>(diagnostics[0], 0, 3);
            TestDiagnostic<LocaleParsingKeyWithoutValueDescriptor>(diagnostics[1], 1, 5);
            TestDiagnostic<LocaleParsingKeyWithoutValueDescriptor>(diagnostics[2], 2, 5);
            TestDiagnostic<LocaleParsingEmptyKeyDescriptor>(diagnostics[3], 3, 0);
            TestDiagnostic<LocaleParsingEmptyKeyDescriptor>(diagnostics[4], 4, 2);
        }

        private void TestDiagnostic<TDescriptor>(LocaleParsingDiagnostic diagnostic, int line, int character)
        {
            Assert.IsType<TDescriptor>(diagnostic.Descriptor);
            LocaleParsingTextLocation location = Assert.IsType<LocaleParsingTextLocation>(diagnostic.Location);

            Assert.Equal(line, location.Line);
            Assert.Equal(character, location.Character);
        }
    }
}
