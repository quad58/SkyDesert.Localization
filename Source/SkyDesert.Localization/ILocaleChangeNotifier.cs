using SkyDesert.Localization.Core;
using System;

namespace SkyDesert.Localization
{
    public interface ILocaleChangeNotifier
    {
        public event Action<Locale> OnLocaleChange;
    }
}
