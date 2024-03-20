// ----------------------------------------------------------------------
// <copyright file="DateTimeExtensions.cs" company="Felsökning">
//      Copyright © Felsökning. All rights reserved.
// </copyright>
// <author>John Bailey</author>
// ----------------------------------------------------------------------
namespace Felsökning
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="DateTimeExtensions"/> class.
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        ///     Extends the <see cref="DateTime"/> object to return whether the day of the week is a weekday, as opposed to the weekend.
        /// </summary>
        /// <param name="dateTime">The current <see cref="DateTime"/> object in question.</param>
        /// <returns>A boolean indicating if the day is a weekday.</returns>
        public static bool IsWeekDay(this DateTime dateTime)
        {
            return dateTime.DayOfWeek == DayOfWeek.Monday
                || dateTime.DayOfWeek == DayOfWeek.Tuesday
                || dateTime.DayOfWeek == DayOfWeek.Wednesday
                || dateTime.DayOfWeek == DayOfWeek.Thursday
                || dateTime.DayOfWeek == DayOfWeek.Friday;
        }

        /// <summary>
        ///     Extends the <see cref="DateTime"/> object to return a standardised string based on the passed culture.
        /// </summary>
        /// <param name="dateTime">The current <see cref="DateTime"/> object in question.</param>
        /// <param name="culture">The target culture to convert the string into.</param>
        /// <returns>A cultured string representation of the DateTime object.</returns>
        public static string ToCulturedString(this DateTime dateTime, string culture)
        {
            CultureInfo cultureInfo = new CultureInfo(name: culture);
            return dateTime.ToString(cultureInfo);
        }

        /// <summary>
        ///     Extends the <see cref="DateTime"/> object to return a standardised string based on ISO:8601.
        /// </summary>
        /// <param name="dateTime">The current <see cref="DateTime"/> object in question.</param>
        /// <returns>A string in the ISO:8601 defined style.</returns>
        public static string ToIso8601UtcString(this DateTime dateTime)
        {
            return dateTime.ToUniversalTime().ToString("o");
        }

        /// <summary>
        ///     Extends the <see cref="DateTime"/> object to return a POSIX time structure.
        /// </summary>
        /// <param name="dateTime">The current <see cref="DateTime"/> object in question.</param>
        /// <returns>A long representing the Unix Epoc time.</returns>
        public static long ToPosixTime(this DateTime dateTime)
        {
            DateTime whenDinosaursRoamedTheEarth = DateTime.UnixEpoch;
            long currentDateTimeTicks = dateTime.Ticks;
            long whenDinosaursRoamedTheEarthTicks = whenDinosaursRoamedTheEarth.Ticks;
            return (currentDateTimeTicks - whenDinosaursRoamedTheEarthTicks) / 10000000;
        }

        /// <summary>
        ///     Extends the <see cref="DateTime"/> object to return an RFC:1123-compliant string.
        ///     The string will be returned as 'ddd, dd MMM yyyy HH:mm:ss GMT'.
        /// </summary>
        /// <param name="dateTime">The current <see cref="DateTime"/> object in question.</param>
        /// <returns>A string representing the RFC:1123-compliant time.</returns>
        public static string ToRfc1123String(this DateTime dateTime)
        {
            return dateTime.ToString(format: "r");
        }

        /// <summary>
        ///     Extends the <see cref="DateTime"/> object to return a standardised string based on the "sv-se" culture info object.
        ///     The string will be returned as'yyyy-mm-dd HH:MM:ss'.
        /// </summary>
        /// <param name="dateTime">The current <see cref="DateTime"/> object in question.</param>
        /// <returns>A string representation of the DateTime object.</returns>
        public static string ToSwedishString(this DateTime dateTime)
        {
            CultureInfo svSe = new CultureInfo(name: "sv-se");
            return dateTime.ToString(svSe);
        }

        /// <summary>
        ///     Extends the <see cref="DateTime"/> object to include a method to return the week number.
        /// </summary>
        /// <param name="dateTime">The current <see cref="DateTime"/> object in question.</param>
        /// <returns>An integer signifying the current week number of the year.</returns>
        public static int ToWeekNumber(this DateTime dateTime)
        {
            // Thanks to Peter Saverman for this idea!
            Calendar calendar = CultureInfo.InvariantCulture.Calendar;
            DayOfWeek dayOfWeek = calendar.GetDayOfWeek(dateTime);
            if (dayOfWeek <= DayOfWeek.Monday && dayOfWeek <= DayOfWeek.Wednesday)
            {
                dateTime = dateTime.AddDays(3);
            }

            // We must used Monday for the first day of the week.
            // See: https://en.wikipedia.org/wiki/ISO_week_date#Calculating_the_week_number_of_a_given_date
            return calendar.GetWeekOfYear(dateTime, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }
    }
}