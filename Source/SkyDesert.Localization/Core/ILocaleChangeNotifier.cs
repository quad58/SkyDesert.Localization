using System;

namespace SkyDesert.Localization.Core
{
    public interface ILocaleChangeNotifier
    {
        public event Action<Locale> OnLocaleChange;
    }
}
