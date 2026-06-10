using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace GameCore.GeneralExtensions
{
    public static class DictionaryExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetValueSafe<TKey, TValue>(
            this IReadOnlyDictionary<TKey, TValue> dictionary,
            TKey key,
            out TValue result
        )
        {
            if (dictionary == null)
            {
                result = default;

                return false;
            }

            return dictionary.TryGetValue(key, out result);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNullOrEmpty<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> dictionary)
        {
            return dictionary == null || dictionary.Count == 0;
        }
    }
}
