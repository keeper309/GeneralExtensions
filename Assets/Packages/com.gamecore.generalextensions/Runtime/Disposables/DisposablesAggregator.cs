using GameCore.LoggerService;
using System;
using System.Collections.Generic;

namespace GameCore.GeneralExtensions
{
    public class DisposablesAggregator : IDisposablesAggregator
    {
        private List<IDisposable> _disposables;

        public void Dispose()
        {
            if (_disposables != null)
            {
                foreach (IDisposable disposable in _disposables)
                {
                    disposable.Dispose();
                }

                _disposables = null;
            }
        }

        public void Add(IDisposable disposable)
        {
            if (_disposables == null)
                _disposables = new List<IDisposable>();

#if UNITY_EDITOR
            Log.Assert(!_disposables.Contains(disposable), "already contains such disposable");
#endif

            _disposables.Add(disposable);
        }

        public bool AddSafe(IDisposable disposable)
        {
            if (disposable == null)
                return false;

            if (_disposables == null)
                _disposables = new List<IDisposable>();

            if (_disposables.Contains(disposable))
                return false;

            _disposables.Add(disposable);

            return true;
        }

        public bool Remove(IDisposable disposable)
        {
            if (_disposables == null)
            {
                return false;
            }

            for (int i = 0; i < _disposables.Count; i++)
            {
                if (ReferenceEquals(disposable, _disposables[i]))
                {
                    _disposables.RemoveBySwapLast(i);

                    return true;
                }
            }

            return false;
        }
    }
}
