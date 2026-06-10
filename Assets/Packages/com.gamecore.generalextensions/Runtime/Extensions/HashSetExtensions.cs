using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace GameCore.GeneralExtensions
{
    public static class HashSetExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNullOrEmpty<T>(this HashSet<T> set)
        {
            return set == null || set.Count == 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddRange<T>(this HashSet<T> set, IEnumerable<T> range)
        {
            foreach (T item in range)
            {
                set.Add(item);
            }
        }

        public static T ItemAtIndexLinear<T>(this HashSet<T> set, int index)
        {
            if (set.IsNullOrEmpty() || index >= set.Count)
            {
                return default;
            }

            T result = default;
            int seeker = 0;
            set.ForEach(
                item =>
                {
                    if (seeker == index)
                    {
                        result = item;

                        return true;
                    }

                    seeker++;

                    return false;
                }
            );

            return result;
        }
    }
}
