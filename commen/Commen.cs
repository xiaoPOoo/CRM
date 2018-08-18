using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace commen
{
    public class Commen
    {
        public static string GetTimeSpan(TimeSpan time)
        {
            if (time.TotalDays > 365)
            {
                return Math.Floor(time.TotalDays / 365) + "年前";
            }
            else if (time.TotalDays > 30)
            {
                return Math.Floor(time.TotalDays / 30) + "月前";
            }
            else if (time.TotalHours > 24)
            {
                return Math.Floor(time.TotalDays) + "天前";
            }
            else if (time.TotalHours > 1)
            {
                return Math.Floor(time.TotalHours) + "小时前";
            }
            else if (time.TotalMinutes > 1)
            {
                return Math.Floor(time.TotalMinutes) + "分钟前";
            }
            else
            {
                return "刚刚";
            }
        }
    }
}
