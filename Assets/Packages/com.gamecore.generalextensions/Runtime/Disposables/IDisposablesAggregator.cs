using System;

namespace GameCore.GeneralExtensions
{
    public interface IDisposablesAggregator : IAddOnlyDisposablesAggregator, IDisposable
    {
        bool Remove(IDisposable disposable);
    }
}
