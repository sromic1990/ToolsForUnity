using System;
using System.Globalization;

namespace Sourav.Utilities.Scripts.TimeRelated
{
    public static class DateTimeUtility
    {
        public static int EvaluateSeconds(string dateTime)
        {
            DateTime now = DateTime.Now;

            DateTime last = DateTime.Parse(dateTime, CultureInfo.InvariantCulture);

            TimeSpan span = now - last;

            // D.Log("span seconds = "+span.Seconds);
            // D.Log("span minutes = "+span.Minutes);
            // D.Log("span hours = "+span.Hours);
            // D.Log("span days = "+span.Days);

            int totalSeconds = span.Seconds + (span.Minutes * 60) + (span.Hours * 60 * 60) + (span.Days * 24 * 60 * 60);
            
            return totalSeconds;
        }
    }
}
