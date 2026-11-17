using SkyDesert.Localization.Parsing;
using Xunit;

namespace SkyDesert.Localization.Tests.Parsing
{
    public class LocaleParserTryParseLineTests
    {
        [Fact]
        public void TryParseLine_KeyEqualsTranslationInput_ReturnsTrue()
        {
            LocaleParser parser = new LocaleParser();

            bool result = parser.TryParseLine("key=translation", out string key, out string translation);

            Assert.True(result);
        }

        [Fact]
        public void TryParseLine_KeyWithoutValueInput_ReturnsFalse()
        {
            LocaleParser parser = new LocaleParser();

            bool result = parser.TryParseLine("key", out string key, out string translation);

            Assert.False(result);
        }

        [Fact]
        public void TryParseLine_EmptyKeyInput_ReturnsFalse()
        {
            LocaleParser parser = new LocaleParser();

            bool result = parser.TryParseLine("=translation", out string key, out string translation);

            Assert.False(result);
        }
    }
}
