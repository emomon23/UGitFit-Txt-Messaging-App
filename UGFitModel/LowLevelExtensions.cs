// -----------------------------------------------------------------------
// <copyright file="LowLevelExtensions.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace UGitFit.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public static class LowLevelExtensions
    {
        public static  bool isNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        public static bool IsNumeric(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return false;

            double d = 0;
            long l = 0;
            bool result;

            result = double.TryParse(str, out d);
            if (result)
                return result;

            return long.TryParse(str, out l);
        }

        public static int ToInt(this string str, int resultIfNotAString)
        {
            try {
                return int.Parse(str);
            }catch {
                return resultIfNotAString;
            }
        }

        public static bool IsDate(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return false;

            try
            {
                DateTime.Parse(str);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static DateTime ToDate(this string str)
        {
            return DateTime.Parse(str);
        }

        public static bool IsGreaterThanDays(this DateTime date, int days)
        {
            TimeSpan ts = DateTime.Now - date;

            return ts.Days > days;
        }

        public static DateTime ToDate(this string str, string dateToReturnIfNull)
        {
            try
            {
                if (string.IsNullOrEmpty(str))
                    return DateTime.Parse(dateToReturnIfNull);

                return DateTime.Parse(str);
            }
            catch
            {
                return DateTime.Parse(dateToReturnIfNull);
            }
        }

        public static  bool IsGuid(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return false;

            try
            {
                Guid.Parse(str);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static Guid ToGuid(this string str)
        {
            try
            {
                return Guid.Parse(str);
            }
            catch
            {
                return Guid.NewGuid();
            }
        }

        public static bool IsPhoneNumber(this string str)
        {
            string temp = str.Replace(" ", "").Replace("-", "").Replace("+", "").Replace("(", "").Replace(")", "");
            return IsNumeric(str);
        }

        public static DateTime ToMidnight(this DateTime date)
        {
            return DateTime.Parse(date.ToShortDateString() + " 12:00:00 am");
        }

        public static DateTime SetTimeOfDay(this DateTime date, string timeOfDay)
        {
            return DateTime.Parse(date.ToShortDateString() + " " + timeOfDay);
        }
    }
}
