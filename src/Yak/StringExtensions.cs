using System;
using System.Text;

namespace Yak {
    public static class StringExtensions {
        /// <summary>
        /// Indicates whether the specified string is null or an <see cref="string.Empty"/> string.
        /// </summary>
        public static bool IsNullOrEmpty(this string value) {
            return string.IsNullOrEmpty(value);
        }

        /// <summary>
        /// Indicates whether the string is null, empty, or consists of only whitespace characters.
        /// </summary>
        public static bool IsNullOrWhiteSpace(this string value) {
            return IsEmpty(value);
        }
        public static bool IsEmpty(this string value) {
            return string.IsNullOrWhiteSpace(value);
        }

        public static string FormatWith(this string format, params object[] args) {
            return Fmt(format, args);
        }
        public static string Fmt(this string format, params object[] args) {
            return string.Format(format, args);
        }


        public static string Repeat(this char c, int count) {
            return new string(c, count);
        }
        public static string Repeat(this string str, int count) {
            var output = new StringBuilder();
            for (var i = 0; i < count; i++)
                output.Append(str);
            return output.ToString();
        }

        public static string[] Split(this string str, StringSplitOptions options, params char[] separator) {
            str.ThrowIfNull("str");
            return str.Split(separator, options);
        }
        public static string[] Split(this string str, StringSplitOptions options, params string[] separator) {
            str.ThrowIfNull("str");
            return str.Split(separator, options);
        }
        public static string[] SplitRemoveEmpty(this string str, params char[] separator) {
            str.ThrowIfNull("str");
            return str.Split(separator, StringSplitOptions.RemoveEmptyEntries);
        }
        public static string[] SplitRemoveEmpty(this string str, params string[] separator) {
            str.ThrowIfNull("str");
            return str.Split(separator, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}