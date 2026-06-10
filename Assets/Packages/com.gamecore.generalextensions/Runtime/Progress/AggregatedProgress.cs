using System;
using System.Collections.Generic;
using System.Linq;

namespace GameCore.GeneralExtensions
{
    public class AggregatedProgress : IProgressSender
    {
        public event Action<float> OnProgressUpdated;
        public event Action<string> OnMessageUpdated;

        private IProgressSender[] _sessions;
        private List<IProgressReceiver> _progressListeners = new List<IProgressReceiver>();

        public float ProgressValue { get; private set; }

        public AggregatedProgress(params IProgressSender[] sessions)
        {
            _sessions = sessions;

            foreach (IProgressSender session in _sessions)
            {
                session.OnProgressUpdated += HandleProgressUpdated;
                session.OnMessageUpdated += HandleDescriptionUpdated;
            }
        }

        public void Dispose()
        {
            if (_sessions != null)
            {
                foreach (IProgressSender session in _sessions)
                {
                    session.OnProgressUpdated -= HandleProgressUpdated;
                    session.OnMessageUpdated -= HandleDescriptionUpdated;
                }
            }

            OnProgressUpdated = null;
            OnMessageUpdated = null;

            _progressListeners = null;
            _sessions = null;
            ProgressValue = -1;
        }

        public void AddListeners(IEnumerable<IProgressReceiver> listeners)
        {
            if (listeners != null)
                _progressListeners.AddRange(listeners);
        }

        public void AddListeners(params IProgressReceiver[] listeners)
        {
            if (listeners != null && listeners.Length > 0)
                _progressListeners.AddRange(listeners);
        }

        private void HandleDescriptionUpdated(string message)
        {
            _progressListeners.ForEach(progress => progress.Report(message));
            OnMessageUpdated?.Invoke(message);
        }

        private void HandleProgressUpdated(float value)
        {
            ProgressValue = _sessions.Aggregate(0.0f, (factor, sessionWithProgress) => factor + sessionWithProgress.ProgressValue);
            float averageValue = ProgressValue / _sessions.Length;

            _progressListeners.ForEach(progress => progress.Report(averageValue));
            OnProgressUpdated?.Invoke(averageValue);
        }
    }
}
