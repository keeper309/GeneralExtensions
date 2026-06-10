using System;
using System.Runtime.CompilerServices;

namespace GameCore.GeneralExtensions
{
    public static class TimeExtensions
    {
        /// <summary>
        ///     based on DateTime.UtcNow.
        /// </summary>
        /// <returns>
        ///     Milliseconds from origin
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static long GlobalMillis()
        {
            return DateTime.UtcNow.Ticks / TimeSpan.TicksPerMillisecond;
        }
    }
}
