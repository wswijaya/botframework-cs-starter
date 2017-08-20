namespace StarterBot.Base
{
    using System;
    public static class DateTimeExtensions
    {
        public static DateTime GetSinsgaporeDateTime(this DateTime dt)
        {
            if (dt == null) dt = DateTime.UtcNow;

            if (dt.Kind == DateTimeKind.Utc)
            {
                TimeZoneInfo sgTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Singapore Standard Time");
                return TimeZoneInfo.ConvertTimeFromUtc(dt, sgTimeZone);
            }
            else
            {
                return dt;
            }
        }

        public static DateTime FromUnixTime(this long unixTime)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return epoch.AddSeconds(unixTime);
        }

        public static long ToUnixTime(this DateTime date)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return Convert.ToInt64((date - epoch).TotalSeconds);
        }
    }
}