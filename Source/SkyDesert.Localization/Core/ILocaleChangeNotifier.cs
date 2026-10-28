using System;

namespace SkyDesert.Localization.Core
{
    public interface ILocaleChangeNotifier
    {
        event Action<Locale> OnLocaleChange;
    }
}
