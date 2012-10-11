﻿using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace System
{
    public static class Extensions
    {
        /// <summary>
        /// Indicates whether an <see cref="System.Object"/> is null.
        /// </summary>
        public static bool IsNull<T>(this T obj)
        {
            return obj == null;
        }

        #region Enumerable

        /// <summary>
        /// Indicates whether an <see cref="System.Collections.IEnumerable"/> is null or contains no elements.
        /// This method calls <see cref="Extensions.Any(IEnumerable)"/>, which may have unintended consequences with forward-only IEnumerable implementations.
        /// </summary>
        public static bool IsNullOrEmpty(this IEnumerable enumerable)
        {
            if (enumerable == null)
                return true;

            // ICollection.Count is O(1), this is faster if enumerable is a list
            var collection = enumerable as ICollection;
            if (collection != null)
                return collection.Count < 1;

            // IEnumerable.Count is O(N), this is slower and may have unintended consequences with forward-only IEnumerable implementations
            return !enumerable.Any();
        }

        /// <summary>
        /// Indicates whether an <see cref="System.Collections.Generic.IEnumerable<out T>"/> is null or contains no elements.
        /// </summary>
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

        /// <summary>
        /// Indicates whether an <see cref="System.Collections.Generic.ICollection<T>"/> is null or contains no elements.
        /// </summary>
        public static bool IsNullOrEmpty<T>(this ICollection<T> collection)
        {
            if (collection == null)
                return true;

            return collection.Count < 1;
        }

        /// <summary>
        /// Gets the number of elements contained in the <see cref="System.Collections.IEnumerable"/>.
        /// This method iterates through the elements to perform the count, do not use on forward-only IEnumerable implementations or performance intensive scenarios.
        /// </summary>
        public static int Count(this IEnumerable enumerable)
        {
            Trace.TraceWarning("Using Extensions.Count(IEnumerable) is not recommended.");

            enumerable.ThrowIfNull("enumerable");
            
            var count = 0;
            var enumerator = enumerable.GetEnumerator();
            while (enumerator.MoveNext()) 
                count++;
            
            return count;
        }

        /// <summary>
        /// Determines whether a sequence contains any elements.
        /// This method calls <see cref="Extensions.Count(IEnumerable)"/>, which may have unintended consequences with forward-only IEnumerable implementations.
        /// </summary>
        public static bool Any(this IEnumerable enumerable)
        {
            enumerable.ThrowIfNull("enumerable");

            return enumerable.Count() > 0;
        }

        #endregion

        #region Strings

        /// <summary>
        /// Indicates whether the specified string is null or an <see cref="System.String.Empty"/> string.
        /// </summary>
        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }

        /// <summary>
        /// Indicates whether the string is null, empty, or consists of only whitespace characters.
        /// </summary>
        public static bool IsNullOrWhiteSpace(this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }

        public static string FormatWith(this string format, params object[] args)
        {
            return string.Format(format, args);
        }
        
        // TODO: add an overload that uses CultureInfo

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

        public static void ThrowIfNullOrEmpty(this string value)
        {
            if (value.IsNullOrEmpty()) throw new ArgumentException();
        }
        public static void ThrowIfNullOrEmpty(this string value, string paramName)
        {
            if (value.IsNullOrEmpty())       
                throw new ArgumentException("{0} is null or empty.".FormatWith(paramName), paramName);
        }

        public static void ThrowIfNullOrWhiteSpace(this string value)
        {
            if (value.IsNullOrWhiteSpace()) throw new ArgumentException();
        }
        public static void ThrowIfNullOrWhiteSpace(this string value, string paramName)
        {
            if (value.IsNullOrWhiteSpace())
                throw new ArgumentException("{0} is null or whitespace.".FormatWith(paramName), paramName);
        }

        #endregion
    }
}