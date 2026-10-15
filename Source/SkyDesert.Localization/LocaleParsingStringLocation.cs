namespace SkyDesert.Localization
{
    public class LocaleParsingStringLocation : LocaleParsingLocation
    {
        public LocaleParsingStringLocation(int line, int column)
        {
            Line = line;
            Column = column;
        }

        public int Line { get; }
        public int Column { get; }

        public override string ToString()
        {
            return $"({Line + 1}, {Column + 1})";
        }
    }
}
