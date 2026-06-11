using System;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;

namespace GameCore.GeneralExtensions
{
    public static class StringExtension
    {
        public static string ReplaceLastOccurrence(this string source, string find, string replace)
        {
            string result = source;
            int place = source.LastIndexOf(find, StringComparison.InvariantCulture);

            if (place >= 0)
            {
                result = source.Remove(place, find.Length).Insert(place, replace);
            }

            return result;
        }

        public static float ParseFloat(this string str, float defaultValue = 0f)
        {
            float result;
            if (!float.TryParse(str, out result))
            {
                result = defaultValue;
            }

            return result;
        }

        public static int ParseInt(this string str, int defaultValue = 0)
        {
            int result;
            if (!int.TryParse(str, out result))
            {
                result = defaultValue;
            }

            return result;
        }

        public static ushort ParseUShort(this string str, ushort defaultValue = 0)
        {
            ushort result;
            if (!ushort.TryParse(str, out result))
            {
                result = defaultValue;
            }

            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNullOrEmpty(this string s)
        {
            return string.IsNullOrEmpty(s);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNullOrWhiteSpace(this string s)
        {
            return string.IsNullOrWhiteSpace(s);
        }

        public static int LastIndexOf(this string s, Func<char, bool> callback)
        {
            for (int i = s.Length - 1; i >= 0; i--)
            {
                if (callback(s[i]))
                {
                    return i;
                }
            }

            return -1;
        }

        public static int IndexOf(this string s, Func<char, bool> callback)
        {
            for (int i = 0; i < s.Length; i++)
            {
                if (callback(s[i]))
                {
                    return i;
                }
            }

            return -1;
        }

        private static readonly Regex NonAlphanumericRegex = new("[^a-z0-9_]", RegexOptions.Compiled);

        /// <summary>
        /// Converts a display name to a safe lowercase identifier:
        /// spaces to underscores, non-alphanumeric characters removed.
        /// Returns null when the result would be empty.
        /// </summary>
        public static string SanitizeAsIdentifier(this string s)
        {
            if (string.IsNullOrEmpty(s))
                return null;

            string sanitized = NonAlphanumericRegex.Replace(s.ToLower().Replace(' ', '_'), "");
            return sanitized.Length > 0 ? sanitized : null;
        }

        public static bool IsFirstLetterNumber(this string source)
        {
            return !source.IsNullOrEmpty() && char.IsNumber(source[0]);
        }

        public static string ToPascalCase(this string source)
        {
            return ToPascalCase(source, new[] { '_', '-', ' ' });
        }

        public static string ToPascalCase(this string source, char[] delimiters)
        {
            string[] words = source.Split(delimiters);

            StringBuilder sb = new StringBuilder();

            foreach (string word in words)
            {
                sb.Append(ToUpperFirstLetter(word));
            }

            return sb.ToString();
        }

        public static string ToUpperFirstLetter(this string source)
        {
            if (source.IsNullOrEmpty())
                return string.Empty;

            char[] letters = source.ToCharArray();
            letters[0] = char.ToUpper(letters[0]);

            return new string(letters);
        }
    }
}
