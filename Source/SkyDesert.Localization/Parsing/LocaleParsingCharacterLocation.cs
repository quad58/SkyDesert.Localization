namespace SkyDesert.Localization.Parsing
{
    public class LocaleParsingCharacterLocation : LocaleParsingLocation
    {
        public LocaleParsingCharacterLocation(int character)
        {
            Character = character;
        }

        public int Character { get; }

        public override string ToString()
        {
            return $"Character {Character + 1}";
        }
    }
}
