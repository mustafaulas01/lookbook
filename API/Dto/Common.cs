using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dto
{
    public class Common
    {
        public static DateTime ConvertIstanbulDateTime(DateTime dateTime)
        {
            var timeZones = TimeZoneInfo.GetSystemTimeZones();
            var istanbulTimeZone = timeZones.FirstOrDefault(x => x.Id.Contains("Turkey") || x.Id.Contains("Istanbul"));
            var localTime = TimeZoneInfo.ConvertTime(new DateTime(dateTime.Ticks, DateTimeKind.Local),
                TimeZoneInfo.Local,
                istanbulTimeZone!);
            return localTime;
        }
    }
}