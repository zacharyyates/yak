
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace Yak {
    public static class EnumerableExtensions {
        /// <summary>
        /// Indicates whether an <see cref="IEnumerable"/> is null or contains no elements.
        /// This method calls <see cref="Any"/>, which may have unintended consequences with forward-only IEnumerable implementations.
        /// </summary>
        public static bool IsNullOrEmpty(this IEnumerable enumerable) {
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
        /// Indicates whether an <see cref="IEnumerable{T}"/> is null or contains no elements.
        /// </summary>
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> enumerable) {
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
        /// Indicates whether an <see cref="ICollection{T}"/> is null or contains no elements.
        /// </summary>
        public static bool IsNullOrEmpty<T>(this ICollection<T> collection) {
            if (collection == null)
                return true;

            return collection.Count < 1;
        }

        /// <summary>
        /// Gets the number of elements contained in the <see cref="IEnumerable"/>.
        /// This method iterates through the elements to perform the count, do not use on forward-only IEnumerable implementations or performance intensive scenarios.
        /// </summary>
        public static int Count(this IEnumerable enumerable) {
            Trace.TraceWarning("Using EnumerableExtensions.Count(IEnumerable) is not recommended.");

            enumerable.ThrowIfNull("enumerable");

            var count = 0;
            var enumerator = enumerable.GetEnumerator();
            while (enumerator.MoveNext())
                count++;

            return count;
        }

        /// <summary>
        /// Determines whether a sequence contains any elements.
        /// This method calls <see cref="Count"/>, which may have unintended consequences with forward-only IEnumerable implementations.
        /// </summary>
        public static bool Any(this IEnumerable enumerable) {
            enumerable.ThrowIfNull("enumerable");

            return enumerable.Count() > 0;
        }
    }
}