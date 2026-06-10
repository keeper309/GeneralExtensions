using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GameCore.GeneralExtensions
{
    public static class MathExtension
    {
        public const float Inch2Cm = 2.54f;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int SecondsToMillis(this float seconds)
        {
            return Mathf.FloorToInt(seconds * 1000);
        }

        /// <summary>
        /// Returns true with the given probability.
        /// </summary>
        /// <param name="chance">Probability from 0 (never) to 1 (always).</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Chance(float chance)
        {
            return Random.Range(0.0f, 1.0f) <= chance;
        }

        public static long PositivePow(long value, long power)
        {
            if (value <= 1 || power == 1)
                return value;

            long result = 1;
            while (power > 0)
            {
                result *= value;
                --power;
            }

            return result;
        }

        //interpolate value of f(x).
        // F0 = f(x0), F1 = f(x1)  =>  Lerp = F(x);
        public static float Lerp(float f0, float f1, float x0, float x1, float x)
        {
            float delta = x1 - x0;

            if (delta < 0)
            {
                Swap(ref x0, ref x1);
                Swap(ref f0, ref f1);
            }
            else if (delta < 0.0001f)
            {
                return (f0 + f1) * 0.5f;
            }

            return f0 + (f1 - f0) / (x1 - x0) * (x - x0);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Swap(ref float f1, ref float f2)
        {
            (f1, f2) = (f2, f1);
        }

        //interpolate value of f(x).
        // F0 = f(x0), F1 = f(x1)  =>  Lerp = F(x);
        public static float LerpClamp(float f0, float f1, float x0, float x1, float x)
        {
            float delta = x1 - x0;

            if (delta < 0)
            {
                Swap(ref x0, ref x1);
                Swap(ref f0, ref f1);
            }
            else if (Mathf.Abs(delta) < 0.0001f)
            {
                return (f0 + f1) * 0.5f;
            }

            if (x >= x1)
                return f1;

            if (x <= x0)
                return f0;

            return f0 + (f1 - f0) / (x1 - x0) * (x - x0);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Sqr(this float value)
        {
            return value * value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Sqr(this int value)
        {
            return value * value;
        }

        public static T Clamp<T>(this T value, T min, T max) where T : IComparable<T>
        {
            T result;

            if (value.CompareTo(min) < 0)
            {
                result = min;
            }
            else if (value.CompareTo(max) > 0)
            {
                result = max;
            }
            else
            {
                result = value;
            }

            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsEven(this int value)
        {
            return value % 2 == 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsOdd(this int value)
        {
            return !IsEven(value);
        }

        public static Vector2 GetGameScreenSizeCm(float defaultDpi = 160)
        {
            float dpi = Screen.dpi;
            if (dpi <= 0)
                dpi = defaultDpi;

            Vector2 screenSize = new Vector2(Screen.width, Screen.height);

#if UNITY_EDITOR
            screenSize = UnityEditor.Handles.GetMainGameViewSize();
#endif
            return new Vector2(screenSize.x / dpi * Inch2Cm, screenSize.y / dpi * Inch2Cm);
        }
    }
}
