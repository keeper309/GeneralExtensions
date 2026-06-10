using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace GameCore.GeneralExtensions
{
    public struct ErrorDelegateExtractor<TDelegate> where TDelegate : Delegate
    {
        private LinkedList<TDelegate> _errors;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void PushError(TDelegate @delegate)
        {
            _errors ??= new LinkedList<TDelegate>();
            _errors.AddFirst(@delegate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Cleanup(List<TDelegate> list)
        {
            if (_errors == null)
                return;

            foreach (TDelegate @delegate in _errors)
            {
                list.Remove(@delegate);
            }
            _errors.Clear();
        }
    }
}