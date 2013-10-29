using System;
using System.Collections.Generic;

namespace Yak {
    public static class ExceptionExtensions {
        const string ExceptionMessageNullOrEmptyFormat = "Parameter: {0} cannot be null or empty.";
        const string ExceptionMessageNullOrWhiteSpaceFormat = "Parameter: {0} cannot be null or whitespace.";

        public static void ThrowIfNull<T>(this T obj) where T : class {
            if (obj.IsNull()) throw new ArgumentNullException();
        }

        public static void ThrowIfNull<T>(this T obj, string paramName) where T : class {
            if (obj.IsNull()) throw new ArgumentNullException(paramName);
        }

        public static void ThrowIfNullOrEmpty(this string value) {
            if (value.IsNullOrEmpty()) throw new ArgumentException();
        }

        public static void ThrowIfNullOrEmpty(this string value, string paramName) {
            if (value.IsNullOrEmpty())
                throw new ArgumentException(ExceptionMessageNullOrEmptyFormat.Fmt(paramName), paramName);
        }

        public static void ThrowIfNullOrEmpty<T>(this IEnumerable<T> enumerable) {
            if (enumerable.IsNullOrEmpty()) throw new ArgumentException();
        }

        public static void ThrowIfNullOrEmpty<T>(this IEnumerable<T> enumerable, string paramName) {
            if (enumerable.IsNullOrEmpty())
                throw new ArgumentException(ExceptionMessageNullOrEmptyFormat.Fmt(paramName), paramName);
        }

        public static void ThrowIfNullOrWhiteSpace(this string value) {
            if (value.IsNullOrWhiteSpace()) throw new ArgumentException();
        }

        public static void ThrowIfNullOrWhiteSpace(this string value, string paramName) {
            if (value.IsNullOrWhiteSpace())
                throw new ArgumentException(ExceptionMessageNullOrWhiteSpaceFormat.Fmt(paramName), paramName);
        }

        public static Exception Innermost(this Exception exception) {
            exception.ThrowIfNull("exception"); // haha: exception-inception

            var inner = exception;
            while (inner.InnerException != null)
                inner = inner.InnerException;

            return inner;
        }
    }
}