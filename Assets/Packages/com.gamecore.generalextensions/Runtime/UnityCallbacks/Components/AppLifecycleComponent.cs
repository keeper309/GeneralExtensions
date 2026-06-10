using GameCore.LoggerService;
using System;

namespace GameCore.GeneralExtensions
{
    public class AppLifecycleComponent : DisposableMonoBehaviour, IAppLifecycleProvider
    {
        event Action<bool> IAppLifecycleProvider.OnAppPause
        {
            add => _onAppPause += value;
            remove => _onAppPause -= value;
        }

        event Action<bool> IAppLifecycleProvider.OnAppFocus
        {
            add => _onAppFocus += value;
            remove => _onAppFocus -= value;
        }

        event Action IAppLifecycleProvider.OnAppQuit
        {
            add => _onAppQuit += value;
            remove => _onAppQuit -= value;
        }

        private Action<bool> _onAppPause;
        private Action<bool> _onAppFocus;
        private Action _onAppQuit;

        public ILogger Logger { get; set; }

        protected override void OnDestroy()
        {
            _onAppPause = null;
            _onAppFocus = null;
            _onAppQuit = null;

            base.OnDestroy();
        }

        private void OnApplicationPause(bool pauseStatus)
        {
            Warning($"{nameof(OnApplicationPause)}  {nameof(pauseStatus)}: {pauseStatus}");

            _onAppPause?.Invoke(pauseStatus);
        }

        private void OnApplicationFocus(bool hasFocus)
        {
            Warning($"{nameof(OnApplicationFocus)}  {nameof(hasFocus)}: {hasFocus}");

            _onAppFocus?.Invoke(hasFocus);
        }

        private void OnApplicationQuit()
        {
            Warning(nameof(OnApplicationQuit));

            _onAppQuit?.Invoke();
        }

        private void Warning(string message)
        {
            if (Logger == null)
            {
                Log.Warning(message);
            }
            else
            {
                Logger.Warning(message);
            }
        }
    }
}
