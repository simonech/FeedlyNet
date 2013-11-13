using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Feedly.NET.JsonSerialization
{
    public static class DateTimeUtils
    {
        public static DateTime ConvertFromUnixTimestampSec(this long timestamp)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return origin.AddSeconds(timestamp);
        }

        public static long ConvertToUnixTimestampSec(this DateTime date)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            TimeSpan diff = date - origin;
            return (long)Math.Floor(diff.TotalSeconds);
        }

        public static DateTime ConvertFromUnixTimestampMilliSec(this long timestamp)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return origin.AddMilliseconds(timestamp);
        }

        public static long ConvertToUnixTimestampMilliSec(this DateTime date)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            TimeSpan diff = date - origin;
            return (long)Math.Floor(diff.TotalMilliseconds);
        }
    }
}
