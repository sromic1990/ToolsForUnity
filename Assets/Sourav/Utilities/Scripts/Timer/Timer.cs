using System;

namespace Sourav.Utilities.Scripts.Timer
{
    public static class Timer
    {
        #region CURRENT TIMESTAMP RELATED
        public static string GetCurrentTimeStampString()
        {
            return DateTime.Now.ToBinary().ToString();
            
        }
        public static DateTime GetCurrentTimeStamp()
        {
            return DateTime.Now;
        }
        #endregion

        #region TIME PASSED IN VARIOUS FORMATS
        public static TimeSpan GetDifference(string inputTime)
        {
            long temp = Convert.ToInt64(inputTime);
            DateTime oldTime = DateTime.FromBinary(temp);
            TimeSpan difference = GetDifference(GetCurrentTimeStamp(), oldTime);
            return difference;
        }

        public static TimeSpan GetDifference(DateTime time1, DateTime time2)
        {
            return time1.Subtract(time2);
        }

        public static int GetDaysPassed(string inputTime)
        {
            return GetDaysPassed(GetDifference(inputTime));
        }
        public static int GetDaysPassed(TimeSpan inputTime)
        {
            return inputTime.Days;
        }
        
        public static int GetHoursPassed(string inputTime)
        {
            return GetHoursPassed(GetDifference(inputTime));
        }
        public static int GetHoursPassed(TimeSpan inputTime)
        {
            int totalHours = (GetDaysPassed(inputTime) * 24) + inputTime.Hours;
            return totalHours;
        }

        public static int GetMinutesPassed(string inputTime)
        {
            return GetMinutesPassed(GetDifference(inputTime));
        }
        public static int GetMinutesPassed(TimeSpan inputTime)
        {
            int totalMins = (GetHoursPassed(inputTime) * 60) + inputTime.Minutes;
            return totalMins;
        }
        
        public static int GetSecondsPassed(string inputTime)
        {
            TimeSpan difference = GetDifference(inputTime);
            return GetSecondsPassed(difference);
        }
        public static int GetSecondsPassed(TimeSpan inputTime)
        {
            int totalSeconds = inputTime.Days * 24 * 60 * 60 + inputTime.Hours * 60 * 60 +
                               inputTime.Minutes * 60 + inputTime.Seconds;
            return totalSeconds;
        }
        #endregion
        
        #region TIME IN TIME FORMAT RELATED
        public static string GetTimeInFormat(TimeSpan time, Format format)
        {
            string timeInString = "";

            switch (format)
            {
                case Format.ddhhmmss:
                    timeInString = GetCorrectNumber(time.Days) + ":" + GetCorrectNumber(time.Hours) + ":" + GetCorrectNumber(time.Minutes) + ":" +
                                   GetCorrectNumber(time.Seconds);
                    break;
                
                case Format.hhmmss:
                    int hours = (time.Days * 24) + time.Hours;
                    timeInString = GetCorrectNumber(hours) + ":" + GetCorrectNumber(time.Minutes) + ":" +
                                   GetCorrectNumber(time.Seconds);
                    break;
                
                case Format.mmss:
                    int minutes = (((time.Days * 24) + time.Hours)* 60) + time.Minutes;
                    timeInString = GetCorrectNumber(minutes) + ":" +
                                   GetCorrectNumber(time.Seconds);
                    break;
                
                case Format.ss:
                    int seconds = ((((time.Days * 24) + time.Hours)* 60) + time.Minutes) * 60 + time.Seconds;
                    timeInString = GetCorrectNumber(seconds);
                    break;
            }
            return timeInString;
        }
        #endregion

        #region TIMESPAN TYPE RELATED
        public static TimeSpanType CheckTimespanType(TimeSpan timeSpan)
        {
            if (timeSpan.CompareTo(TimeSpan.Zero) < 0)
            {
                return TimeSpanType.Negative;
            }
            else
            {
                return TimeSpanType.Positive;
            }
        }
        #endregion
        
        #region UTILITIES
        private static string GetCorrectNumber(int number)
        {
            if (number < 10)
            {
                return "0" + number.ToString();
            }
            else
            {
                return number.ToString();
            }
        }
        #endregion
    }

    public enum Format
    {
        ddhhmmss,
        hhmmss,
        mmss,
        ss,
    }

    public enum TimeSpanType
    {
        Positive,
        Negative,
    }
}
