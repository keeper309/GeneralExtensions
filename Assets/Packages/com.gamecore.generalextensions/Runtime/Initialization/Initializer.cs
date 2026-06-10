using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;

namespace GameCore.GeneralExtensions
{
    public class Initializer
    {
        private readonly List<IProgressReceiver> _progressReceivers = new List<IProgressReceiver>();
        private readonly List<Unit> _units = new List<Unit>();

        public void Add(IProgressReceiver progressReceiver)
        {
            _progressReceivers.Add(progressReceiver);
        }

        public void Add(IInitializable initializable)
        {
            if (_units.FindIndex(x => x.Initializable == initializable) >= 0)
            {
                throw new Exception($"Can't add same instance of {initializable.GetType().FullName} twice.");
            }

            _units.Add(new Unit(initializable));
        }

        public async UniTask Initialize()
        {
            IProgressSender[] progressSenders = _units
                .Select(p => p.Progress)
                .Cast<IProgressSender>()
                .ToArray();

            AggregatedProgress aggregatedSession = new AggregatedProgress(progressSenders);
            aggregatedSession.AddListeners(_progressReceivers);

            try
            {
                foreach (Unit initializableProgress in _units)
                {
                    await initializableProgress.Initializable.Initialize(initializableProgress.Progress);
                    _progressReceivers.ForEach(progress => progress.Report(string.Empty));
                }
            }
            catch (Exception e)
            {
                _progressReceivers.ForEach(progress => progress.Report(1.0f));
                aggregatedSession.Dispose();

                throw new Exception("Initialization process failed. For reason see inner exception.", e);
            }

            _progressReceivers.ForEach(progress => progress.Report(1.0f, string.Empty));
            aggregatedSession.Dispose();
        }

        private readonly struct Unit
        {
            public readonly IInitializable Initializable;
            public readonly Progress Progress;

            public Unit(IInitializable initializable)
            {
                Initializable = initializable;
                Progress = new Progress();
            }
        }
    }
}
