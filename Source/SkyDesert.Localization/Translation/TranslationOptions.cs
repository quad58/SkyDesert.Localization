using System;

namespace SkyDesert.Localization.Translation
{
    public readonly struct TranslationOptions
    {
        public TranslationOptions(bool parseArguments, int maxRecursionDepth)
        {
            if (maxRecursionDepth < 0) throw new ArgumentOutOfRangeException(nameof(maxRecursionDepth));

            ParseArguments = parseArguments;
            MaxRecursionDepth = maxRecursionDepth;
        }

        public bool ParseArguments { get; }
        public int MaxRecursionDepth { get; }
    }
}
