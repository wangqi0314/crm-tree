using System;
using System.IO;
using System.Xml;
using System.Text;
using System.Text.RegularExpressions;
namespace ShInfoTech.Common
{
    /// <summary>
    /// 验证类
    /// </summary>
    public class Validator
    {
        /// <summary>
        /// 是否为中文
        /// </summary>
        /// <param name="lstr"></param>
        /// <returns></returns>
        public static bool IsChinese(string lstr)
        {
            return Regex.IsMatch(lstr, @"[\u4e00-\u9fa5]");
        }

        /// <summary>
        /// 是否是日期时间类型
        /// </summary>
        /// <param name="strTime"></param>
        /// <returns></returns>
        public static bool IsDateTime(string strTime)
        {
            try
            {
                Convert.ToDateTime(strTime);
                return true;
            }
            catch
            {
                return false;
            }
        }


        /// <summary>
        /// 是否为Email
        /// </summary>
        /// <param name="_value">验证的字符</param>
        /// <returns>是否合法的bool值</returns>
        public static bool IsEmail(string _value)
        {
            Regex regex = new Regex(@"^\w+([-+.]\w+)*@(\w+([-.]\w+)*\.)+([a-zA-Z]+)+$", RegexOptions.IgnoreCase);
            return regex.Match(_value).Success;
        }

        /// <summary>
        /// 是否为INT整型
        /// </summary>
        /// <param name="_value">验证的字符</param>
        /// <returns>是否合法的bool值</returns>
        public static bool IsInt(string _value)
        {
            Regex regex = new Regex(@"^(-){0,1}\d+$");
            if (regex.Match(_value).Success)
            {
                if ((long.Parse(_value) > 0x7fffffffL) || (long.Parse(_value) < -2147483648L))
                {
                    return false;
                }
                return true;
            }
            return false;
        }

        /// <summary>
        /// 是否为IP
        /// </summary>
        /// <param name="_value">验证的字符</param>
        /// <returns>是否合法的bool值</returns>
        public static bool IsIP(string _value)
        {
            Regex regex = new Regex("^(((2[0-4]{1}[0-9]{1})|(25[0-5]{1}))|(1[0-9]{2})|([1-9]{1}[0-9]{1})|([0-9]{1})).(((2[0-4]{1}[0-9]{1})|(25[0-5]{1}))|(1[0-9]{2})|([1-9]{1}[0-9]{1})|([0-9]{1})).(((2[0-4]{1}[0-9]{1})|(25[0-5]{1}))|(1[0-9]{2})|([1-9]{1}[0-9]{1})|([0-9]{1})).(((2[0-4]{1}[0-9]{1})|(25[0-5]{1}))|(1[0-9]{2})|([1-9]{1}[0-9]{1})|([0-9]{1}))$", RegexOptions.IgnoreCase);
            return regex.Match(_value).Success;
        }

        /// <summary>
        /// 是否是纯字母和数字
        /// </summary>
        /// <param name="_value">验证的字符</param>
        /// <returns>是否合法的bool值</returns>
        public static bool IsLetterOrNumber(string _value)
        {
            return QuickValidate("^[a-zA-Z0-9_]*$", _value);
        }

        /// <summary>
        /// 是否为手机号码
        /// </summary>
        /// <param name="_value">验证的字符</param>
        /// <returns>是否合法的bool值</returns>
        public static bool IsMobileNum(string _value)
        {
            Regex regex = new Regex(@"^(13|15|18)\d{9}$", RegexOptions.IgnoreCase);
            return regex.Match(_value).Success;
        }

        /// <summary>
        /// 判断是否是数字，包括小数和整数
        /// </summary>
        /// <param name="_value">验证的字符</param>
        /// <returns>是否合法的bool值</returns>
        public static bool IsNumber(string _value)
        {
            return QuickValidate("^(0|([1-9]+[0-9]*))(.[0-9]+)?$", _value);
        }

        /// <summary>
        /// 是否是纯数字
        /// </summary>
        /// <param name="_value">验证的字符</param>
        /// <returns>是否合法的bool值</returns>
        public static bool IsNumeric(string _value)
        {
            return QuickValidate("^[1-9]*[0-9]*$", _value);
        }


        /// <summary>
        /// 快速验证一个字符串是否符合指定的正则表达式
        /// </summary>
        /// <param name="_express">正则表达式的内容</param>
        /// <param name="_value">需验证的字符串</param>
        /// <returns>是否合法的bool值</returns>
        public static bool QuickValidate(string _express, string _value)
        {
            Regex myRegex = new Regex(_express);
            if (_value.Length == 0)
            {
                return false;
            }
            return myRegex.IsMatch(_value);
        }

        /// <summary>
        /// 判断一个字符串
        /// </summary>
        /// <param name="_value">需验证的字符串</param>
        /// <returns>是否合法的bool值</returns>
        public static bool IsPhoneNum(string _value)
        {
            Regex regex = new Regex(@"^(86)?(-)?(0\d{2,3})?(-)?(\d{7,8})(-)?(\d{3,5})?$", RegexOptions.IgnoreCase);
            return regex.Match(_value).Success;
        }

        /// <summary>
        /// 是否可以转化为日期
        /// </summary>
        /// <param name="_value">需验证的字符串</param>
        /// <returns>是否合法的bool值</returns>
        public static bool IsStringDate(string _value)
        {
            try
            {
                DateTime dTime = DateTime.Parse(_value);
            }
            catch (FormatException)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 判断一个字符串是否为网址
        /// </summary>
        /// <param name="_value">需验证的字符串</param>
        /// <returns>是否合法的bool值</returns>
        public static bool IsUrl(string _value)
        {
            Regex regex = new Regex(@"(http://)?([\w-]+\.)*[\w-]+(/[\w- ./?%&=]*)?", RegexOptions.IgnoreCase);
            return regex.Match(_value).Success;
        }

        /// <summary>
        /// 判断一个字符串是否为字母加数字
        /// </summary>
        /// <param name="_value">需验证的字符串</param>
        /// <returns>是否合法的bool值</returns>
        public static bool IsWordAndNum(string _value)
        {
            Regex regex = new Regex("[a-zA-Z0-9]?");
            return regex.Match(_value).Success;
        }

        /// <summary>
        /// 验证邮政编码
        /// </summary>
        /// <param name="code">邮政编码</param>
        /// <returns></returns>
        public static bool IsZipCode(string code)
        {
            Regex regex = new Regex("^[a-zA-Z0-9 ]{3,12}$");
            return regex.Match(code).Success;
        }

        /// <summary>
        /// 把字符串转成日期
        /// </summary>
        /// <param name="_value">字符串</param>
        /// <param name="_defaultValue">默认值</param>
        /// <returns></returns>
        public static DateTime StrToDate(string _value, DateTime _defaultValue)
        {
            if (IsStringDate(_value))
            {
                return Convert.ToDateTime(_value);
            }
            return _defaultValue;
        }

        /// <summary>
        /// 密码验证（6-16位字符,可由英文、数字及“_”、“-”组成）
        /// </summary>
        /// <param name="pwd">密码</param>
        /// <returns></returns>
        public static bool ValidatePwd(string pwd)
        {
            Regex regex = new Regex(@"^([A-Z]|[a-z]|[\d]|[-]|[_]){6,16}$");
            return regex.Match(pwd).Success;
        }

        /// <summary>
        /// 用户名验证（4-16 个字符，用中文、英文和数字）
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns></returns>
        public static bool ValidateUserName(string userName)
        {
            Regex regex = new Regex(@"^[\.\w\u4e00-\u9fa5\uF900-\uFA2D]{2,16}$");
            return regex.Match(userName).Success;
        }

        /// <summary>
        /// 判断日期是否过期
        /// </summary>
        /// <param name="myDate">所要判断的日期</param>
        /// <returns></returns>
        public static bool ValidDate(string myDate)
        {
            return (!IsStringDate(myDate) || CompareDate(myDate, DateTime.Now.ToShortDateString(), 0));
        }

        /// <summary>
        /// 日期比较
        /// </summary>
        /// <param name="today">距离某个日期</param>
        /// <param name="writeDate">输入日期</param>
        /// <param name="n">比较天数</param>
        /// <returns>大于天数返回true，小于返回false</returns>
        public static bool CompareDate(string today, string writeDate, int n)
        {
            DateTime Today = Convert.ToDateTime(today);
            DateTime WriteDate = Convert.ToDateTime(writeDate).AddDays((double)n);
            if (Today >= WriteDate)
            {
                return false;
            }
            return true;
        }
    }
}