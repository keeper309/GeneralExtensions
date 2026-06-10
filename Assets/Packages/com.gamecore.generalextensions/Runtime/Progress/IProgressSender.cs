using System;

namespace GameCore.GeneralExtensions
{
    public interface IProgressSender : IDisposable
    {
        event Action<float> OnProgressUpdated;
        event Action<string> OnMessageUpdated;
        float ProgressValue { get; }
    }
}
