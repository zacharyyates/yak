using System.Collections.Generic;
using System.Linq;

namespace System
{
    public static class Extensions
    {
        public static bool IsNull<T>(this T obj)
        {
            return obj == null;
        }
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> enumerable)
        {
            if (enumerable == null) 
                return true;

            // ICollection<T>.Count is O(1), this is faster if enumerable is a list
            var collection = enumerable as ICollection<T>;
            if (collection != null)
                return collection.Count < 1;

            // IEnumerable<T>.Count is O(N), this is slower
            return !collection.Any();
        }

        #region Strings

        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        public static bool IsNullOrWhiteSpace(this string str)
        {
            return string.IsNullOrWhiteSpace(str);
        }

        public static string FormatWith(this string format, params object[] args)
        {
            return string.Format(format, args);
        }
        
        // TODO: add an overload that uses culture info

        #endregion

        #region Throw Helpers

        public static void ThrowIfNull<T>(this T obj)
        {
            if (obj.IsNull()) throw new ArgumentNullException();
        }
        public static void ThrowIfNull<T>(this T obj, string paramName)
        {
            if (obj.IsNull()) throw new ArgumentNullException(paramName);
        }

        public static void ThrowIfNullOrEmpty(this string str)
        {
            if (str.IsNullOrEmpty()) throw new ArgumentException();
        }
        public static void ThrowIfNullOrEmpty(this string str, string paramName)
        {
            if (str.IsNullOrEmpty())       
                throw new ArgumentException("{0} is null or empty.".FormatWith(paramName), paramName);
        }

        public static void ThrowIfNullOrWhiteSpace(this string str)
        {
            if (str.IsNullOrWhiteSpace()) throw new ArgumentException();
        }
        public static void ThrowIfNullOrWhiteSpace(this string str, string paramName)
        {
            if (str.IsNullOrWhiteSpace())
                throw new ArgumentException("{0} is null or whitespace.".FormatWith(paramName), paramName);
        }

        #endregion
    }
}