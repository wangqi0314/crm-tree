using System;
using System.Collections.Generic;
using System.Text;

namespace Shinfotech.Tools
{
    public class TimeParser
    {

        #region 把秒转换成分钟
        /// <summary>
        /// 把秒转换成分钟
        /// </summary>
        /// <returns></returns>
        public static int SecondToMinute(int Second)
        {
            decimal mm = (decimal)((decimal)Second / (decimal)60);
            return Convert.ToInt32(Math.Ceiling(mm));
        }
        #endregion

        #region 返回某年某月最后一天
        /// <summary>
        /// 返回某年某月最后一天
        /// </summary>
        /// <param name="year">年份</param>
        /// <param name="month">月份</param>
        /// <returns>日</returns>
        public static int GetMonthLastDate(int year, int month)
        {
            DateTime lastDay = new DateTime(year, month, new System.Globalization.GregorianCalendar().GetDaysInMonth(year, month));
            int Day = lastDay.Day;
            return Day;
        }
        #endregion

        #region 返回时间差
        public static string DateDiff(DateTime DateTime1, DateTime DateTime2)
        {
            string dateDiff = null;
            try
            {
                //TimeSpan ts1 = new TimeSpan(DateTime1.Ticks);
                //TimeSpan ts2 = new TimeSpan(DateTime2.Ticks);
                //TimeSpan ts = ts1.Subtract(ts2).Duration();
                TimeSpan ts = DateTime2 - DateTime1;
                if (ts.Days >= 1)
                {
                    dateDiff = DateTime1.Month.ToString() + "月" + DateTime1.Day.ToString() + "日";
                }
                else
                {
                    if (ts.Hours > 1)
                    {
                        dateDiff = ts.Hours.ToString() + "小时前";
                    }
                    else
                    {
                        dateDiff = ts.Minutes.ToString() + "分钟前";
                    }
                }
            }
            catch
            { }
            return dateDiff;
        }
        #endregion

        #region String的值转换DateTime
        /// <summary>
        /// String的值转换DateTime
        /// </summary>
        /// <param name="strName"></param>
        /// <returns></returns>
        public static DateTime GetDateTime(string strTime)
        {
            DateTime time = Convert.ToDateTime("1900-1-1 " + DateTime.Now.ToString("HH:mm:ss"));
            try
            {
                return time = Convert.ToDateTime(strTime);
            }
            catch
            {
                return time;
            }

        }
        #endregion

        #region 字符串日期格式转换
        /// <summary>
        /// 字符串日期格式转换
        /// Creater Michael Wang
        /// Create Date 2011-01-01 15:50
        /// </summary>
        /// <param name="strDate"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string TimeToString(string strDate, string format)
        {
            if (0 < strDate.Length)
            {
                try
                {
                    DateTime date = Convert.ToDateTime(strDate);
                    return date.ToString(format);
                }
                catch
                {
                    return strDate;
                }
            }
            else
            {
                return "";
            }
        }
        #endregion

        #region 返回今天之前的三个日期:昨天,前天 今天(不显示) +private string showDay(DateTime date)

        /// <summary>
        /// 返回今天之前的三个日期:昨天,前天 今天(不显示)
        /// createUser:szLive
        /// createDate:2012-3-16 
        /// 比如: 昨天是:2012-3-15 16:40:39  今天是:2012-3-16 那返回是: "昨天 16:40"
        ///       前天是:2012-3-14 16:40:39  今天是:2012-3-16 那返回是: "前天 16:40"
        /// </summary>
        /// <param name="date">需要比较的日期</param>
        /// <returns></returns>
        public static string showDay(DateTime date)
        {
            string strDay = "";
            try
            {
                int days = DateTime.Now.Date.Day - date.Date.Day;
                switch (days)
                {
                    case 0:
                        strDay = " 今天 ";//今天不用显示出来
                        break;
                    case 1:
                        strDay = " 昨天 ";
                        break;
                    case 2:
                        strDay = " 前天 ";
                        break;
                    default:
                        strDay = date.ToString("yyyy-MM-dd");
                        break;
                }
            }
            catch
            {
                return date.ToString();
            }
            return strDay + " " + date.ToShortTimeString().ToString();
        }

        #endregion
        #region
        /// <summary>
        /// 字符串日期格式转换
        /// Creater Michael Wang
        /// Create Date 20110101 15:50
        /// </summary>
        /// <param name="strDate"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string TimeToFormString(string strDate, string format)
        {
            if (0 < strDate.Length)
            {
                try
                {
                    DateTime date = Convert.ToDateTime(strDate);
                    return date.ToString(format);
                }
                catch
                {
                    return strDate;
                }
            }
            else
            {
                return "";
            }
        }
        #endregion

        

        #region 动态墙时间展示
        ///// <summary>
        ///// 动态墙时间展示
        ///// createUser:Duyalin
        ///// createDate:2012-08-07 
        ///// <param name="string">日期</param>
        ///// <returns></returns>
        ///// <summary>
        //public static string TrendsDay(string date)
        //{
        //    string strDay = "";
        //    try
        //    {
        //        DateTime sTime = GetDateTime(date);
        //        TimeSpan ts = DateTime.Now - sTime;
        //        if (ts.Days < 1)
        //        {
        //            if (ts.Hours < 1)
        //            {
        //                if (ts.Minutes < 1)
        //                {
        //                    if (ts.Seconds > 0)
        //                    {
        //                        strDay = ts.Seconds + "秒前 ";
        //                    }
        //                    else
        //                    {
        //                        strDay = (ts.Seconds + 60) + "秒前 ";
        //                    }
        //                }
        //                else
        //                {
        //                    strDay = ts.Minutes + "分钟前 ";

        //                }
        //            }
        //            else
        //            {
        //                strDay = "今天 " + sTime.ToString("HH:mm:ss");
        //            }
        //        }
        //        else if (ts.Days < 2)
        //        {
        //            strDay = "昨天 " + sTime.ToString("HH:mm:ss");
        //        }
        //        else
        //        {
        //            strDay = sTime.Month + "月" + sTime.Day + "日 " + sTime.ToString("HH:mm:ss");
        //        }
        //    }
        //    catch
        //    {
        //        return date.ToString();
        //    }
        //    return strDay;
        //}

        #endregion
    }
}
