namespace System
{
    public static class ExceptionExtensions
    {
        public static Exception Innermost(this Exception exception)
        {
            exception.ThrowIfNull("exception");

            var inner = exception;
            while (inner.InnerException != null)
                inner = inner.InnerException;

            return inner;
        }
    }
}