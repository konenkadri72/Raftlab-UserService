using System;
using System.Collections.Generic;
using System.Text;

namespace Raftlab.Core.HelperServiceAndExtensions.DateTimeExtension
{
    public static class DateTimeExtensions
    {
        /// <summary>
        /// This Method Will Specify UTC Kind
        /// </summary>
        /// <param name="input">Date</param>
        /// <returns></returns>
        public static DateTime SpecifyUTCKind(this DateTime input)
        {
            return DateTime.SpecifyKind(input, DateTimeKind.Utc);
        }

        /// <summary>
        /// This Method Will Specify UTC Kind (Nullable)
        /// </summary>
        /// <param name="input">Date (Nullable)</param>
        /// <returns></returns>
        public static DateTime? SpecifyUTCKind(this DateTime? input)
        {
            if (input.HasValue)
                return DateTime.SpecifyKind(input.Value, DateTimeKind.Utc);
            else
                return null;
        }

        /// <summary>
        /// Convert Date From Source TimeZone To Destination TimeZone
        /// </summary>
        /// <param name="date">UTC Date</param>
        /// <param name="sourceZone">From TimeZone</param>
        /// <param name="destinationZone">To TimeZone</param>
        /// <returns></returns>
        public static DateTime GetNewTimezoneDate(this System.DateTime date, TimeZoneInfo sourceZone, TimeZoneInfo destinationZone)
        {
            if (date == null)
            {
                return date;
            }
            var convertedDate = DateTime.SpecifyKind(date.GetConvertedDate(sourceZone), DateTimeKind.Unspecified);
            return DateTime.SpecifyKind(TimeZoneInfo.ConvertTimeToUtc(convertedDate, destinationZone), DateTimeKind.Utc);
        }

        /// <summary>
        /// Convert Date To Specified TimeZone
        /// </summary>
        /// <param name="date">UTC Date</param>
        /// <param name="zoneInfo">To TimeZone</param>
        /// <returns></returns>
        public static DateTime GetConvertedDate(this System.DateTime date, TimeZoneInfo zoneInfo)
        {
            return TimeZoneInfo.ConvertTimeFromUtc(date, zoneInfo);
        }

        /// <summary>
        /// This Method will Return SpecifiedZone CurrentDate
        /// </summary>
        /// <param name="timeZone"></param>
        /// <returns></returns>
        public static DateTime GetZoneCurrentDateTime(TimeZoneInfo timeZone)
        {
            return TimeZoneInfo.ConvertTimeToUtc(TimeZoneInfo.ConvertTime(DateTimeOffset.UtcNow, timeZone).DateTime, timeZone);
        }

        /// <summary>
        /// This Method will Add Day on Date into Specified TimeZone
        /// </summary>
        /// <param name="date">UTC Date</param>
        /// <param name="dayCount">Day Count</param>
        /// <param name="zoneInfo">TimeZone of Given Date</param>
        /// <returns></returns>
        public static DateTime ConvertAndAddDays(this System.DateTime date, int dayCount, TimeZoneInfo zoneInfo)
        {
            return TimeZoneInfo.ConvertTimeToUtc(date.GetConvertedDate(zoneInfo).AddDays(dayCount), zoneInfo);
        }

        /// <summary>
        /// This Method will Add Month on Date into Specified TimeZone
        /// </summary>
        /// <param name="date">UTC Date</param>
        /// <param name="monthCount">Month Count</param>
        /// <param name="zoneInfo">TimeZone of Given Date</param>
        /// <returns></returns>
        public static DateTime ConvertAndAddMonths(this System.DateTime date, int monthCount, TimeZoneInfo zoneInfo)
        {
            return TimeZoneInfo.ConvertTimeToUtc(date.GetConvertedDate(zoneInfo).AddMonths(monthCount), zoneInfo);
        }
    }
}
