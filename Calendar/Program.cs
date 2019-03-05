using System;
using System.Globalization;
using System.Threading;

namespace Calendar
{
    public static class Program
    {
        public static DateTime TruncToBeginningOfMonth(this DateTime time)
        {
            return new DateTime(time.Year, time.Month, 1);
        }

        public static bool IsHolday(this DayOfWeek dayOfweek)
        {
            return dayOfweek.Equals(DayOfWeek.Saturday) || dayOfweek.Equals(DayOfWeek.Sunday);
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Pls enter date");
            Thread.CurrentThread.CurrentCulture = new CultureInfo("ru-RU");
            Thread.CurrentThread.CurrentCulture.DateTimeFormat.FirstDayOfWeek = DayOfWeek.Sunday;
            DateTime time;
            while (!DateTime.TryParse(Console.ReadLine(), out time))
            {
                Console.WriteLine("Enter valid date");
            }
            Console.WriteLine(time);
            DateTime startOfMonth = time.TruncToBeginningOfMonth();
            PrintHeader();
            SkipEmptyWeekDays(startOfMonth);

            PrintAllDaysInMonth(startOfMonth);

            Console.ReadKey();
        }

        private static void PrintAllDaysInMonth(DateTime startOfMonth)
        {
            int holidays = 0;
            int workDays = 0;
            for (DateTime i = startOfMonth; i.Month.Equals(startOfMonth.Month); i = i.AddDays(1))
            {
                if (i.DayOfWeek.IsHolday())
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    holidays++;
                }
                else
                {
                    Console.ResetColor();
                    workDays++;
                }
                Console.Write($"{i.Day,-15}");
                if (i.DayOfWeek.Equals(DayOfWeek.Saturday))
                {
                    Console.WriteLine();
                }
            }
            Console.WriteLine();
            Console.ResetColor();
            Console.WriteLine($"workdays: {workDays}; holidays: {holidays}");
        }

        private static void SkipEmptyWeekDays(DateTime startOfMonth)
        {
            foreach (var weekDay in Enum.GetValues(typeof(DayOfWeek)))
            {
                if (weekDay.Equals(startOfMonth.DayOfWeek))
                {
                    break;
                }
                else
                {
                    Console.Write("{0,-15}", "");
                }
            }
        }

        private static void PrintHeader()
        {
            foreach (var day in CultureInfo.CurrentCulture.DateTimeFormat.DayNames)
            {
                Console.Write("{0,-15}",day);
            }
            Console.WriteLine();
        }
    }
}
