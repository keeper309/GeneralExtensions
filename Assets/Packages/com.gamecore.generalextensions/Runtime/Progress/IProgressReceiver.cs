using System;

namespace GameCore.GeneralExtensions
{
    public interface IProgressReceiver : IProgress<float>
    {
        void Report(string message);
        void Report(float value, string message);
    }
}
