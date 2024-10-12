using Microsoft.VisualBasic;
using System.Globalization;

internal class Program
{
    public List<string> calendars = new List<string> { "gregory", "persian", "hijri" };
    private static void Main(string[] args)
    {
        Console.Write("Enter date time: (yyyy-mm-dd, yyyy/mm/dd, ...): ");
        var input = Console.ReadLine();

        Console.Write("Enter calendar type (gregory, persian, hijri): ");
        var inputCalendar = Console.ReadLine();

        inputCalendar = !String.IsNullOrWhiteSpace(inputCalendar) ? inputCalendar : "gregory";
        Calendar calendar = inputCalendar switch
        {
            "gregory" => new GregorianCalendar(),
            "persian" => new PersianCalendar(),
            "hijri" => new HijriCalendar(),
            _ => new GregorianCalendar(),
        };

        if (!String.IsNullOrWhiteSpace(input))
        {
            try
            {
                var baseDate = DateTime.Parse(input);
                var date = new DateTime(
                    baseDate.Year,
                    baseDate.Month,
                    baseDate.Day,
                    baseDate.Hour,
                    baseDate.Minute,
                    baseDate.Second,
                    calendar
                );
                printDate(baseDate, calendar);
            }
            catch (Exception ex)
            {
                if (ex is FormatException)
                {
                    Console.WriteLine("Date format is invalid");
                }
                Console.WriteLine("Error in creating datetime object with specified calendar");
            }
        }
    }
    public static void printDate(DateTime date, Calendar calendar)
    {
        switch (calendar)
        {
            case GregorianCalendar:
                printPersian(date);
                printHijri(date);
                break;
            case PersianCalendar:
                printGregorian(date);
                printHijri(date);
                break;
            case HijriCalendar:
                printPersian(date);
                printGregorian(date);
                break;
        }
    }
    public static void printPersian(DateTime date)
    {
        var calendar = new PersianCalendar();
        Console.WriteLine("Jalali: {0}, {1}-{2}-{3} {4}:{5}:{6}\n",
        calendar.GetDayOfWeek(date),
        calendar.GetYear(date),
        calendar.GetMonth(date),
        calendar.GetDayOfMonth(date),
        calendar.GetHour(date),
        calendar.GetMinute(date),
        calendar.GetSecond(date));
    }
    public static void printGregorian(DateTime date)
    {
        var calendar = new GregorianCalendar();
        Console.WriteLine("Gregory: {0}, {1}-{2}-{3} {4}:{5}:{6}\n",
        calendar.GetDayOfWeek(date),
        calendar.GetYear(date),
        calendar.GetMonth(date),
        calendar.GetDayOfMonth(date),
        calendar.GetHour(date),
        calendar.GetMinute(date),
        calendar.GetSecond(date));
    }
    public static void printHijri(DateTime date)
    {
        var calendar = new HijriCalendar();
        Console.WriteLine("Hijri: {0}, {1}-{2}-{3} {4}:{5}:{6}\n",
        calendar.GetDayOfWeek(date),
        calendar.GetYear(date),
        calendar.GetMonth(date),
        calendar.GetDayOfMonth(date),
        calendar.GetHour(date),
        calendar.GetMinute(date),
        calendar.GetSecond(date));
    }
}