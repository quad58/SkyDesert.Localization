using SkyDesert.Localization.Parsing;
using System;
using Xunit;

namespace SkyDesert.Localization.Tests.Parsing
{
    public class LocaleParserParseLineTests
    {
        [Theory]
        [InlineData("key=translation")]
        [InlineData("   key=translation")]
        [InlineData("	key=translation   ")]
        public void ParseLine_KeyEqualsTranslationInput_ReturnsKeyAndTranslation(string input)
        {
            LocaleParser parser = new LocaleParser();

            parser.ParseLine(input, out string key, out string translation);

            Assert.Equal("key", key);
            Assert.Equal("translation", translation);
        }

        [Fact]
        public void ParseLine_NullInput_ThrowsArgumentNullException()
        {
            LocaleParser parser = new LocaleParser();

            Assert.Throws<ArgumentNullException>(() => parser.ParseLine(null, out string key, out string translation));
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        public void ParseLine_WhiteSpaceInput_ThrowsArgumentException(string input)
        {
            LocaleParser parser = new LocaleParser();

            Assert.Throws<ArgumentException>(() => parser.ParseLine(input, out string key, out string translation));
        }

        [Theory]
        [InlineData("key", 3)]
        [InlineData("  key", 5)]
        [InlineData("  key  ", 5)]
        public void ParseLine_KeyWithoutValueInput_ThrowsLocaleParsingExceptionWithKeyWithoutValueDescriptor(string input, int character)
        {
            LocaleParser parser = new LocaleParser();

            LocaleParsingException exception = Assert.Throws<LocaleParsingException>(() => parser.ParseLine(input, out string key, out string translation));

            TestException<LocaleParsingKeyWithoutValueDescriptor>(exception, character);
        }

        [Theory]
        [InlineData("=translation", 0)]
        [InlineData("  =translation", 2)]
        public void ParseLine_EmptyKeyInput_ThrowsLocaleParsingExceptionWithEmptyKeyDescriptor(string input, int character)
        {
            LocaleParser parser = new LocaleParser();

            LocaleParsingException exception = Assert.Throws<LocaleParsingException>(() => parser.ParseLine(input, out string key, out string translation));

            TestException<LocaleParsingEmptyKeyDescriptor>(exception, character);
        }

        private void TestException<TDescriptor>(LocaleParsingException exception, int character)
        {
            Assert.IsType<TDescriptor>(exception.Descriptor);
            LocaleParsingCharacterLocation location = Assert.IsType<LocaleParsingCharacterLocation>(exception.Location);

            Assert.Equal(character, location.Character);
        }
    }
}
