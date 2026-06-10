using System;
using Cysharp.Threading.Tasks;
using System.Collections;
using System.Threading;
using UnityEngine;
using GameCore.LoggerService;

namespace GameCore.GeneralExtensions
{
    public static class UniTaskExtensions
    {
        public static UniTask<T> AsUniTask<T>(Action<Action<T>> method)
        {
            UniTaskCompletionSource<T> source = new UniTaskCompletionSource<T>();
            try
            {
                method(t => source.TrySetResult(t));
            }
            catch (Exception ex)
            {
                source.TrySetException(ex);
            }

            return source.Task;
        }

        public static async UniTask<T> AsUniTaskAsync<T>(bool switchToMainThread, Action<Action<T>> method)
        {
            T result = await AsUniTask<T>(method);
            if (switchToMainThread && !ThreadHelper.IsOnMainThread)
            {
                await UniTask.SwitchToMainThread();
            }

            return result;
        }

        public static UniTask ToUniTask(
            this AsyncOperation asyncOperation,
            IProgressReceiver progressReceiver,
            string message,
            PlayerLoopTiming timing = PlayerLoopTiming.Update,
            CancellationToken cancellationToken = default
        )
        {
            IProgress<float> progressWrap = new Progress<float>(x => progressReceiver.Report(x, message));

            return asyncOperation.ToUniTask(progressWrap, timing, cancellationToken);
        }

        public static IEnumerator WaitToEnumerate(this UniTask task, int delayMs = 0)
        {
            while (!task.GetAwaiter().IsCompleted)
            {
                yield return delayMs <= 0
                    ? null
                    : new WaitForSeconds(delayMs / 1000f);
            }
        }
    }
}
