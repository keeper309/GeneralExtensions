using System;
using UnityEngine;

namespace GameCore.GeneralExtensions
{
    public class UpdateComponent : DisposableMonoBehaviour, ITimeWatcher, IUpdateSender, IPauseableUpdateSender, IPauseable
    {
        event Action<float> IPauseableUpdateSender.OnFixedUpdate
        {
            add => _onPauseableFixedUpdate += value;
            remove => _onPauseableFixedUpdate -= value;
        }

        event Action<float> IPauseableUpdateSender.OnLateUpdate
        {
            add => _onPauseableLateUpdate += value;
            remove => _onPauseableLateUpdate -= value;
        }

        event Action<float> IPauseableUpdateSender.OnUpdate
        {
            add => _onPauseableUpdate += value;
            remove => _onPauseableUpdate -= value;
        }

        event Action<float> IUpdateSender.OnUpdate
        {
            add => _onUpdate += value;
            remove => _onUpdate -= value;
        }

        event Action<float> IUpdateSender.OnFixedUpdate
        {
            add => _onFixedUpdate += value;
            remove => _onFixedUpdate -= value;
        }

        event Action<float> IUpdateSender.OnLateUpdate
        {
            add => _onLateUpdate += value;
            remove => _onLateUpdate -= value;
        }

        private Action<float> _onUpdate;
        private Action<float> _onLateUpdate;
        private Action<float> _onFixedUpdate;

        private Action<float> _onPauseableUpdate;
        private Action<float> _onPauseableLateUpdate;
        private Action<float> _onPauseableFixedUpdate;

        public bool IsPaused { get; set; }

        public float UnscaledTimeWithoutPause { get; private set; }
        public float ScaledTimeWithoutPause { get; private set; }

        protected override void OnDestroy()
        {
            _onUpdate = null;
            _onLateUpdate = null;
            _onFixedUpdate = null;
            _onPauseableUpdate = null;
            _onPauseableLateUpdate = null;
            _onPauseableFixedUpdate = null;

            base.OnDestroy();
        }

        private void Update()
        {
            UnscaledTimeWithoutPause += Time.unscaledDeltaTime;
            ScaledTimeWithoutPause += Time.deltaTime;

            if (!IsPaused)
            {
                _onPauseableUpdate?.Invoke(Time.deltaTime);
            }

            _onUpdate?.Invoke(Time.deltaTime);
        }

        private void FixedUpdate()
        {
            if (!IsPaused)
            {
                _onPauseableFixedUpdate?.Invoke(Time.fixedDeltaTime);
            }

            _onFixedUpdate?.Invoke(Time.fixedDeltaTime);
        }

        private void LateUpdate()
        {
            if (!IsPaused)
            {
                _onPauseableLateUpdate?.Invoke(Time.deltaTime);
            }

            _onLateUpdate?.Invoke(Time.deltaTime);
        }
    }
}
