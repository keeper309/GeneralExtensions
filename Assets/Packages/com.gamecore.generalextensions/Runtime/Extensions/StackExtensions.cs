using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace GameCore.GeneralExtensions
{
    public static class StackExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsEmpty<T>(this Stack<T> stack)
        {
            return stack.Count == 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNullOrEmpty<T>(this Stack<T> stack)
        {
            return stack == null || stack.Count == 0;
        }
    }
}
