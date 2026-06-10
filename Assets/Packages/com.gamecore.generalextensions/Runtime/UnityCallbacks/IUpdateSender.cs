using System;

namespace GameCore.GeneralExtensions
{
    public interface IUpdateSender
    {
        event Action<float> OnUpdate;
        event Action<float> OnFixedUpdate;
        event Action<float> OnLateUpdate;
    }
}