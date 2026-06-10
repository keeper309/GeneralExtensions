using System;
using System.Collections.Generic;

public static class EnumExtensions
{
    public static bool HasAllFlags<TEnum>(this TEnum flags, TEnum filter) where TEnum : Enum
    {
        long flagsValue = Convert.ToInt64(flags);
        long filterValue = Convert.ToInt64(filter);

        return (flagsValue & filterValue) == filterValue;
    }

    public static bool HasAnyFlags<TEnum>(this TEnum flags, TEnum filter) where TEnum : Enum
    {
        long flagsValue = Convert.ToInt64(flags);
        long filterValue = Convert.ToInt64(filter);

        return (flagsValue & filterValue) != 0;
    }

    public static List<TEnum> ToFlagList<TEnum>(this TEnum flags) where TEnum : Enum
    {
        List<TEnum> result = new List<TEnum>();
        foreach (TEnum value in Enum.GetValues(typeof(TEnum)))
        {
            if (flags.HasAnyFlags(value) && Convert.ToInt64(value) != 0)
            {
                result.Add(value);
            }
        }

        return result;
    }

    public static TEnum SetFlag<TEnum>(this TEnum flags, TEnum flagToSet) where TEnum : Enum
    {
        long result = Convert.ToInt64(flags) | Convert.ToInt64(flagToSet);

        return (TEnum)Enum.ToObject(typeof(TEnum), result);
    }


    public static TEnum ClearFlags<TEnum>(this TEnum flags, TEnum flagToClear) where TEnum : Enum
    {
        long result = Convert.ToInt64(flags) & ~Convert.ToInt64(flagToClear);

        return (TEnum)Enum.ToObject(typeof(TEnum), result);
    }

    public static string ToBitString<TEnum>(this TEnum flags) where TEnum : Enum
    {
        long value = Convert.ToInt64(flags);

        return Convert.ToString(value, 2).PadLeft(64, '0');
    }
}