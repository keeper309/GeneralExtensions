using GameCore.LoggerService;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore.GeneralExtensions
{
    public abstract class SafeActionBase<TAction, TArgs> : IDisposable
        where TAction : Delegate
    {
        private List<TAction> _list;
        private int _gapsCount;
        private int _lastCompactFrameId;
        private int _callStack;

        public bool IsEmpty => _list == null || _list.Count == 0;

        protected abstract void Call(TAction action, TArgs args);

        public void Dispose()
        {
            _list.Clear();
            _gapsCount = 0;
        }

        public void Add(TAction value)
        {
            _list ??= new List<TAction>();

#if UNITY_EDITOR || DEBUG
            Log.Assert(!_list.Contains(value), "already contains such action!");
#endif
            _list.Add(value);
        }

        public void Remove(TAction value)
        {
            if (_list.IsNullOrEmpty())
                return;

            int id = _list.IndexOf(value);

            if (id < 0)
                return;

            _list[id] = null;
            ++_gapsCount;
        }

        protected void InvokeInner(TArgs args)
        {
            if (IsEmpty)
                return;

            CallStack(ECallStackCommand.Increase);

            ErrorDelegateExtractor<TAction> extractor = new();
            int count = _list.Count;
            for (int i = 0; i < count; i++)
            {
                TAction action = _list[i];

                if (action == null)
                    continue;

                try
                {
                    Call(action, args);
                }
                catch (Exception e)
                {
                    Log.Exception(e);
                    extractor.PushError(action);
                }
            }

            extractor.Cleanup(_list);
            CallStack(ECallStackCommand.Decrease);
        }

        private void CallStack(ECallStackCommand command)
        {
            if (command == ECallStackCommand.Increase)
            {
                ++_callStack;
            }
            else
            {
                --_callStack;
                TryCompact();
            }
        }

        private void TryCompact()
        {
            if (_callStack != 0 ||
                _lastCompactFrameId == Time.frameCount)
            {
                return;
            }

            if (_gapsCount > 16 &&
                _gapsCount * 2 > _list.Count)
            {
                List<TAction> newList = new(_list.Count - _gapsCount + 4);
                foreach (TAction action in _list)
                {
                    if (action != null)
                    {
                        newList.Add(action);
                    }
                }

                _list = newList;
                _gapsCount = 0;
                _lastCompactFrameId = Time.frameCount;
            }
        }

        private enum ECallStackCommand : byte
        {
            Increase,
            Decrease
        }
    }
}
