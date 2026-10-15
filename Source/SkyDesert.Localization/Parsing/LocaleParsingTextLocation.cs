namespace SkyDesert.Localization.Parsing
{
    public class LocaleParsingTextLocation : LocaleParsingLocation
    {
        public LocaleParsingTextLocation(int line, int character)
        {
            Line = line;
            Character = character;
        }

        public int Line { get; }
        public int Character { get; }

        public override string ToString()
        {
            return $"Line {Line + 1}, Character {Character + 1}";
        }
    }
}
