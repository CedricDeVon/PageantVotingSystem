
using System;

namespace PageantVotingSystem.Sources.Miscellaneous
{
    public class DateParser
    {
        public static int PresentLinuxTime
        {
            get { return ToLinuxDateTime(DateTime.Now); }

            private set { }
        }

        public static string ShortenDate(DateTime dateTime)
        {
            return dateTime.ToShortDateString();
        }

        public static int CalculateAge(string dateTimeString)
        {
            if (string.IsNullOrEmpty(dateTimeString))
            {
                throw new Exception("'DateParser' - 'dateTimeString' cannot be null or empty");
            }

            try
            {
                return CalculateAge(DateTime.Parse(dateTimeString));
            }
            catch
            {
                throw new Exception("'DateParser' - 'dateTimeString' must represent this format 'YYYY-MM-DD'");
            }
        }

        public static bool IsInThePast(DateTime targetDateTime)
        {
            return PresentLinuxTime > ToLinuxDateTime(targetDateTime);
        }

        public static bool IsInTheFuture(DateTime targetDateTime)
        {
            return PresentLinuxTime < ToLinuxDateTime(targetDateTime);
        }

        private static int ToLinuxDateTime(DateTime targetDateTime)
        {
            return (int) targetDateTime.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
        }

        private static int CalculateAge(DateTime birthDate)
        {
            return CalculateAge(birthDate, DateTime.Now);
        }

        private static int CalculateAge(DateTime birthDate, DateTime targetDate)
        {
            return (targetDate.Year - birthDate.Year) - ((birthDate.Month > targetDate.Month || birthDate.Day > targetDate.Day) ? 1 : 0);
        }

    }
}
