using SkyDesert.Localization.Parsing;
using System;
using Xunit;

namespace SkyDesert.Localization.Tests.Parsing
{
    public class LocaleParserParseTests
    {
        [Fact]
        public void Parse_SimpleMultilineTranslationsInput_ReturnsFullResult()
        {
            LocaleParser parser = new LocaleParser();

            LocaleParsingResult result = parser.Parse("key1=translation1\nkey2=translation2\rkey3=translation3\r\nkey4=translation4", "en-US");

            Assert.True(result.Success);
            Assert.Equal("en-US", result.Locale.Identifier);
            Assert.Equal(4, result.Locale.GetTranslationsSnapshot().Count);
            Assert.Equal("translation1", result.Locale.GetTranslation("key1"));
            Assert.Equal("translation2", result.Locale.GetTranslation("key2"));
            Assert.Equal("translation3", result.Locale.GetTranslation("key3"));
            Assert.Equal("translation4", result.Locale.GetTranslation("key4"));
        }

        [Fact]
        public void Parse_ComplexMultilineTranslationsInput_ReturnsFullResult()
        {
            LocaleParser parser = new LocaleParser();

            LocaleParsingResult result = parser.Parse("   #comment\n   \nkey1=translation1\nkey2=translation2   #comment", "en-US");

            Assert.True(result.Success);
            Assert.Equal("en-US", result.Locale.Identifier);
            Assert.Equal(2, result.Locale.GetTranslationsSnapshot().Count);
            Assert.Equal("translation1", result.Locale.GetTranslation("key1"));
            Assert.Equal("translation2", result.Locale.GetTranslation("key2"));
        }

        [Fact]
        public void Parse_InvalidMultilineTranslationsInput_ReturnsFullDiagnosics()
        {
            LocaleParser parser = new LocaleParser();

            LocaleParsingResult result = parser.Parse("key\n  key\n  key  \n=translation\n  =translation", "en-US");

            Assert.False(result.Success);
            Assert.Equal(5, result.Diagnostics.Count);

            TestDiagnostic<LocaleParsingKeyWithoutValueDescriptor>(result.Diagnostics[0], 0, 3);
            TestDiagnostic<LocaleParsingKeyWithoutValueDescriptor>(result.Diagnostics[1], 1, 5);
            TestDiagnostic<LocaleParsingKeyWithoutValueDescriptor>(result.Diagnostics[2], 2, 5);
            TestDiagnostic<LocaleParsingEmptyKeyDescriptor>(result.Diagnostics[3], 3, 0);
            TestDiagnostic<LocaleParsingEmptyKeyDescriptor>(result.Diagnostics[4], 4, 2);
        }

        private void TestDiagnostic<TDescriptor>(LocaleParsingDiagnostic diagnostic, int line, int character)
        {
            Assert.IsType<TDescriptor>(diagnostic.Descriptor);
            LocaleParsingTextLocation location = Assert.IsType<LocaleParsingTextLocation>(diagnostic.Location);

            Assert.Equal(line, location.Line);
            Assert.Equal(character, location.Character);
        }

        [Fact]
        public void Parse_NullInput_ThrowsArgumentNullException()
        {
            LocaleParser parser = new LocaleParser();

            Assert.Throws<ArgumentNullException>(() => parser.Parse(null, "en-US"));
        }

        [Fact]
        public void Parse_NullLocaleIdentifier_ThrowsArgumentNullException()
        {
            LocaleParser parser = new LocaleParser();

            Assert.Throws<ArgumentNullException>(() => parser.Parse("key=translation", null));
        }
    }
}
