using System.Collections.Generic;
using System.Linq;

namespace SkyDesert.Localization.Translation
{
    public abstract class TranslationArgumentException : TranslationException
    {
        public TranslationArgumentException(string message, string key, string argument) : base(message, key)
        {
            Argument = argument;
        }

        public string Argument { get; }

        protected override IEnumerable<Detail> Details => base.Details.Append(new Detail(nameof(Argument), Argument));
    }
}
