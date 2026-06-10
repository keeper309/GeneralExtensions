using System;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace GameCore.GeneralExtensions
{
    public struct TransitionFunctorWithCancellation : IDisposable
    {
        private CancellationTokenSource _cancellationTokenSource;

        public bool IsInTransition { get; private set; }

        public void Dispose()
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource?.Dispose();
            _cancellationTokenSource = null;
        }

        public void Cancel()
        {
            if (IsInTransition && _cancellationTokenSource != null)
            {
                _cancellationTokenSource.Cancel();
                _cancellationTokenSource.Dispose();
                _cancellationTokenSource = new CancellationTokenSource();
            }
        }

        public async UniTask Execute(Func<CancellationToken, UniTask> function)
        {
            Cancel();

            _cancellationTokenSource = _cancellationTokenSource ?? new CancellationTokenSource();

            IsInTransition = true;

            try
            {
                await function(_cancellationTokenSource.Token);
            }
            finally
            {
                IsInTransition = false;
            }
        }
    }
}
