
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

        public static string ShortenDate(string dateTimeString)
        {
            return DateTime.Parse(dateTimeString).ToShortDateString();
        }

        public static int CalculateAge(string dateTimeString)
        {
            return CalculateAge(DateTime.Parse(dateTimeString));
        }

        public static int CalculateAge(DateTime birthDate)
        {
            return CalculateAge(birthDate, DateTime.Now);
        }

        public static int CalculateAge(DateTime birthDate, DateTime targetDate)
        {
            return (targetDate.Year - birthDate.Year) - ((birthDate.Month > targetDate.Month || birthDate.Day > targetDate.Day) ? 1 : 0);
        }
        
        public static bool IsInThePast(DateTime targetDateTime)
        {
            return !IsInTheFuture(targetDateTime);
        }

        public static bool IsInTheFuture(DateTime targetDateTime)
        {
            return PresentLinuxTime < ToLinuxDateTime(targetDateTime);
        }

        public static int ToLinuxDateTime(DateTime targetDateTime)
        {
            return (int) targetDateTime.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
        }
    }
}
