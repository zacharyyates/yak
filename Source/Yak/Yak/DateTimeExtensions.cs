
namespace System
{
    public static class DateTimeExtensions
    {
        public static TimeSpan FromNow(this DateTime dateTime)
        {
            return DateTime.Now - dateTime;
        }
    }
}