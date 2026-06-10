using System;

namespace GameCore.GeneralExtensions
{
    public interface IAddOnlyDisposablesAggregator
    {
        void Add(IDisposable disposable);

        /// <summary>
        ///   will return false => disposable == null or already contains such disposable
        /// </summary>
        bool AddSafe(IDisposable disposable);
    }
}
