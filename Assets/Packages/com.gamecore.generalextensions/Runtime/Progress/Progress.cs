using System;

namespace GameCore.GeneralExtensions
{
    public class Progress : IProgressReceiver, IProgressSender
    {
        public event Action<float> OnProgressUpdated;
        public event Action<string> OnMessageUpdated;

        public float ProgressValue { get; private set; }

        public void Report(float value)
        {
            ProgressValue = value;
            OnProgressUpdated?.Invoke(value);
        }

        public void Report(string message)
        {
            OnMessageUpdated?.Invoke(message);
        }

        public void Report(float value, string message)
        {
            ProgressValue = value;
            OnProgressUpdated?.Invoke(value);
            OnMessageUpdated?.Invoke(message);
        }

        public void Dispose() { }
    }
}
