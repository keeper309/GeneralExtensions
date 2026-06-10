using System;

namespace GameCore.GeneralExtensions
{
    public interface IAppLifecycleProvider
    {
        event Action<bool> OnAppPause;
        event Action<bool> OnAppFocus;
        event Action OnAppQuit;
    }
}