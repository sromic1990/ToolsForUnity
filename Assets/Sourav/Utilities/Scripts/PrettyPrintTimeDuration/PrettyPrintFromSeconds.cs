namespace Sourav.Utilities.Scripts.PrettyPrintTimeDuration
{
    public static class PrettyPrintFromSeconds
    {
        private const int secondsInMinutes = 60;
        private const int secondsInHours = 3600;
        private const int secondsInDay = 86400;
        private const int secondsInMonth = 2592000;
        private const int secondsInYear = 31104000;

        private const int hoursThreshold = 12;
        private const int minsThreshold = 15;
        private const int daysThreshold = 2;
        private const int monthsThreshold = 1;

        public static string GetPrettyString(int seconds, TypeOfPrint type)
        {
            string print = "";

            switch (type)
            {
                case TypeOfPrint.LongHand:
                    print = GetLongHandString(seconds);
                    break;
                
                case TypeOfPrint.ShortHand:
                    print = GetShortHandPrint(seconds);
                    break;
            }
            
            return print;
        }

        private static string GetLongHandString(int totalSeconds)
        {
            string longHand = "";

            int years = totalSeconds / secondsInYear;
            int rem = totalSeconds % secondsInYear;
            int months = rem / secondsInMonth;
            rem = rem % secondsInMonth;
            int days = rem / secondsInDay;
            rem = rem % secondsInDay;
            int hours = rem / secondsInHours;
            rem = rem % secondsInHours;
            int minutes = rem / secondsInMinutes;
            int seconds = rem % secondsInMinutes;

            longHand = "";
            if (years > 0)
            {
                longHand += years;
                if (years > 1)
                {
                    longHand += " yrs ";
                }
                else
                {
                    longHand += " yr ";
                }

                if (months > monthsThreshold)
                {
                    if (months > 0)
                    {
                        longHand += months;
                        if (months > 1)
                        {
                            longHand += " months ";
                        }
                        else
                        {
                            longHand += " month ";
                        }
                    }
                }
            }
            else
            {
                if (months > 0)
                {
                    longHand += months;
                    if (months > 1)
                    {
                        longHand += " months ";
                    }
                    else
                    {
                        longHand += " month ";
                    }

                    if (days > daysThreshold)
                    {
                        if (days > 0)
                        {
                            longHand += days;
                            if (days > 1)
                            {
                                longHand += " days ";
                            }
                            else
                            {
                                longHand += " day ";
                            }
                        }
                    }
                }
                else
                {
                    if (days > 0)
                    {
                        longHand += days;
                        if (days > 1)
                        {
                            longHand += " days ";
                        }
                        else
                        {
                            longHand += " day ";
                        }
                        
                        if (hours > hoursThreshold)
                        {
                            longHand += hours;
                            if (hours > 1)
                            {
                                longHand += " hrs ";
                            }
                            else
                            {
                                longHand += " hr ";
                            }
                        }
                    }
                    else
                    {
                        if (hours > 0)
                        {
                            longHand += hours;
                            if (hours > 1)
                            {
                                longHand += " hrs ";
                            }
                            else
                            {
                                longHand += " hr ";
                            }
                            
                            if (minutes > minsThreshold)
                            {
                                longHand += minutes + " min ";
                            }
                        }
                        else
                        {
                            if (minutes > 0)
                            {
                                longHand += minutes + " min ";
                            }
                            else
                            {
                                longHand += seconds + " sec. ";
                            }
                        }
                    }
                }
            }
            
            // D.Log($"longHand = {longHand}");
            return longHand;
        }
        
        private static string GetShortHandPrint(int secondsTotal)
        {
            string shortHand = "";

            int hours = secondsTotal / secondsInHours;
            int rem = secondsTotal % secondsInHours;
            int mins = rem / secondsInMinutes;
            rem = rem % secondsInMinutes;
            int seconds = rem;

            if (hours < 10)
            {
                shortHand += "0" + hours + ":";
            }
            else
            {
                shortHand += hours + ":";
            }

            if (mins < 10)
            {
                shortHand += "0" + mins + ":";
            }
            else
            {
                shortHand += mins + ":";
            }

            if (seconds < 10)
            {
                shortHand += "0" + seconds;
            }
            else
            {
                shortHand += seconds;
            }
            
            // shortHand = hours + ":" + mins + ":" + rem;
            // D.Log($"shortHand = {shortHand}");
            return shortHand;
        }
    }

    public enum TypeOfPrint
    {
        ShortHand,
        LongHand,
    }
}
