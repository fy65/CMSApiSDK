using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fy.Common
{
    public class TimeHelper
    {

        #region ########时间戳 相关###############

        /// <summary>
        /// 取当前时间点的时间戳，高并发情况下会有重复。想要解决这问题请使用sleep线程睡眠1毫秒。
        /// </summary>
        /// <param name="AccurateToMilliseconds">精确到毫秒级别</param>
        /// <returns>返回一个长整数时间戳</returns>
        public static long GetNowTimeStamp(bool AccurateToMilliseconds = false)
        {
            //备注：DateTime.Now.ToUniversalTime不能缩写成DateTime.Now.Ticks，会有好几个小时的误差。
            //621355968000000000计算方法 long ticks = (new DateTime(1970, 1, 1, 8, 0, 0)).ToUniversalTime().Ticks;
            if (AccurateToMilliseconds)
            {
                return (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000;
            }
            else
            {
                //上面是精确到毫秒，需要在最后除去（10000），这里只精确到秒，只要在10000后面加三个0即可（1秒等于1000毫米）。
                return (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000000;
            }
        }


        /// <summary>
        /// UTC时间 转 时间戳 (UTC时间 UTC时间 UTC时间 ,否则误差几个小时) 
        ///提示：转换UTC的方式 DateTime.Now.ToUniversalTime
        /// </summary>
        /// <param name="utcTime">UTC时间，UTC时间，UTC时间</param>
        /// <param name="AccurateToMilliseconds">精确到毫秒级别 默认false</param>
        /// <returns></returns>

        public static long ConvertToTimeStamp(DateTime utcTime, bool AccurateToMilliseconds = false)
        {
            if (AccurateToMilliseconds)
            {
                return (utcTime.Ticks - 621355968000000000) / 10000;
            }
            else
            {
                return (utcTime.ToUniversalTime().Ticks - 621355968000000000) / 10000000;
            }
        }

        /// <summary>
        /// 时间戳反转为时间 （当地时区）
        /// </summary>
        /// <param name="TimeStamp">时间戳</param>
        /// <param name="AccurateToMilliseconds">是否精确到毫秒</param>
        /// <returns>返回一个日期时间</returns>
        public static DateTime ConvertToDateTime(long TimeStamp, bool AccurateToMilliseconds = false)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1)); // 当地时区
            if (AccurateToMilliseconds)
            {
                return startTime.AddTicks(TimeStamp * 10000);
            }
            else
            {
                return startTime.AddTicks(TimeStamp * 10000000);
            }
        }

        #endregion

        #region 时间差 返回秒
        /// <summary>
        /// time1-time2
        /// </summary>
        /// <param name="time1"></param>
        /// <param name="time2"></param>
        /// <returns></returns>
        public static double GetTimeDiff(DateTime time1, DateTime time2)
        {
            double dateDiff = 0;
            try
            {
                TimeSpan ts = time1 - time2;
                var s = ts.TotalSeconds;
                dateDiff = s;
            }
            catch
            { }
            return dateDiff;
        }

        #endregion
    }
}
