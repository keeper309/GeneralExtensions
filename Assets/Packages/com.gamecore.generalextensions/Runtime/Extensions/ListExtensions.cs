using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace GameCore.GeneralExtensions
{
    public static class ListExtensions
    {
        public enum ECompareEmptyMode
        {
            StrictEqual,
            LogicallyEqual
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Dequeue<T>(this List<T> list)
        {
            T temp = list[0];
            list.RemoveAt(0);

            return temp;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Enqueue<T>(this List<T> list, T value)
        {
            list.Add(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Peek<T>(this IReadOnlyList<T> list)
        {
            return list[0];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNullOrEmpty<T>(this IReadOnlyList<T> list)
        {
            return list == null || list.Count == 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Last<T>(this IReadOnlyList<T> list)
        {
            return list.IsNullOrEmpty() ? default : list[^1];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T First<T>(this IReadOnlyList<T> list)
        {
            return list.IsNullOrEmpty() ? default : list[0];
        }

        public static void RemoveBySwapLast<T>(this List<T> list, int index)
        {
            if (list.IsNullOrEmpty())
            {
                throw new NullReferenceException($"list is empty to use {nameof(RemoveBySwapLast)}");
            }

            int lastId = list.Count - 1;

            if (index >= list.Count || index < 0)
            {
                throw new IndexOutOfRangeException($"cant {nameof(RemoveBySwapLast)} with id: {index}");
            }

            if (index == lastId)
            {
                list.RemoveAt(index);

                return;
            }

            list[index] = list[lastId];
            list.RemoveAt(lastId);
        }

        public static bool TryRemoveLast<T>(this List<T> list, out T lastItem)
        {
            if (list.IsNullOrEmpty())
            {
                lastItem = default;

                return false;
            }

            int id = list.Count - 1;

            lastItem = list[id];
            list.RemoveAt(id);

            return true;
        }

        public static string ToStringNew<T>(this List<T> list)
        {
            if (list == null)
                return "null";

            if (list.Count == 0)
                return "empty";

            StringBuilder sb = new(list.Count * 8 + 32);

            sb.Append("Count: ");
            sb.Append(list.Count);
            sb.Append(" ... ");

            foreach (T item in list)
            {
                sb.Append(item);
                sb.Append(", ");
            }

            return sb.ToString();
        }

        public static bool EqualsCollection<T>(
            this List<T> list0,
            List<T> list1,
            ECompareEmptyMode compareMode = ECompareEmptyMode.StrictEqual,
            Func<T, T, bool> itemComparator = null
        )
            where T : IEquatable<T>
        {
            if (ReferenceEquals(list0, list1))
                return true;

            switch (compareMode)
            {
                case ECompareEmptyMode.StrictEqual when list0 == null || list1 == null: return false;
                case ECompareEmptyMode.LogicallyEqual when list0.IsNullOrEmpty() == list1.IsNullOrEmpty(): return true;
            }

            if (list0.Count != list1.Count)
                return false;

            itemComparator ??= DefaultComparator;

            int count = list1.Count;
            for (int i = 0; i < count; i++)
            {
                if (!itemComparator(list0[i], list1[i]))
                    return false;
            }

            return true;
        }

        private static bool DefaultComparator<T>(T t0, T t1) where T : IEquatable<T>
        {
            return t0.Equals(t1);
        }
    }
}
