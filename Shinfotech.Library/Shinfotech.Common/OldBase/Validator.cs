using System;
using System.IO;
using System.Xml;
using System.Text;
using System.Text.RegularExpressions;
namespace ShInfoTech.Common
{
    /// <summary>
    /// ��֤��
    /// </summary>
    public class Validator
    {
        /// <summary>
        /// �Ƿ�Ϊ����
        /// </summary>
        /// <param name="lstr"></param>
        /// <returns></returns>
        public static bool IsChinese(string lstr)
        {
            return Regex.IsMatch(lstr, @"[\u4e00-\u9fa5]");
        }

        /// <summary>
        /// �Ƿ�������ʱ������
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
        /// �Ƿ�ΪEmail
        /// </summary>
        /// <param name="_value">��֤���ַ�</param>
        /// <returns>�Ƿ�Ϸ���boolֵ</returns>
        public static bool IsEmail(string _value)
        {
            Regex regex = new Regex(@"^\w+([-+.]\w+)*@(\w+([-.]\w+)*\.)+([a-zA-Z]+)+$", RegexOptions.IgnoreCase);
            return regex.Match(_value).Success;
        }

        /// <summary>
        /// �Ƿ�ΪINT����
        /// </summary>
        /// <param name="_value">��֤���ַ�</param>
        /// <returns>�Ƿ�Ϸ���boolֵ</returns>
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
        /// �Ƿ�ΪIP
        /// </summary>
        /// <param name="_value">��֤���ַ�</param>
        /// <returns>�Ƿ�Ϸ���boolֵ</returns>
        public static bool IsIP(string _value)
        {
            Regex regex = new Regex("^(((2[0-4]{1}[0-9]{1})|(25[0-5]{1}))|(1[0-9]{2})|([1-9]{1}[0-9]{1})|([0-9]{1})).(((2[0-4]{1}[0-9]{1})|(25[0-5]{1}))|(1[0-9]{2})|([1-9]{1}[0-9]{1})|([0-9]{1})).(((2[0-4]{1}[0-9]{1})|(25[0-5]{1}))|(1[0-9]{2})|([1-9]{1}[0-9]{1})|([0-9]{1})).(((2[0-4]{1}[0-9]{1})|(25[0-5]{1}))|(1[0-9]{2})|([1-9]{1}[0-9]{1})|([0-9]{1}))$", RegexOptions.IgnoreCase);
            return regex.Match(_value).Success;
        }

        /// <summary>
        /// �Ƿ��Ǵ���ĸ������
        /// </summary>
        /// <param name="_value">��֤���ַ�</param>
        /// <returns>�Ƿ�Ϸ���boolֵ</returns>
        public static bool IsLetterOrNumber(string _value)
        {
            return QuickValidate("^[a-zA-Z0-9_]*$", _value);
        }

        /// <summary>
        /// �Ƿ�Ϊ�ֻ�����
        /// </summary>
        /// <param name="_value">��֤���ַ�</param>
        /// <returns>�Ƿ�Ϸ���boolֵ</returns>
        public static bool IsMobileNum(string _value)
        {
            Regex regex = new Regex(@"^(13|15|18)\d{9}$", RegexOptions.IgnoreCase);
            return regex.Match(_value).Success;
        }

        /// <summary>
        /// �ж��Ƿ������֣�����С��������
        /// </summary>
        /// <param name="_value">��֤���ַ�</param>
        /// <returns>�Ƿ�Ϸ���boolֵ</returns>
        public static bool IsNumber(string _value)
        {
            return QuickValidate("^(0|([1-9]+[0-9]*))(.[0-9]+)?$", _value);
        }

        /// <summary>
        /// �Ƿ��Ǵ�����
        /// </summary>
        /// <param name="_value">��֤���ַ�</param>
        /// <returns>�Ƿ�Ϸ���boolֵ</returns>
        public static bool IsNumeric(string _value)
        {
            return QuickValidate("^[1-9]*[0-9]*$", _value);
        }


        /// <summary>
        /// ������֤һ���ַ����Ƿ����ָ����������ʽ
        /// </summary>
        /// <param name="_express">������ʽ������</param>
        /// <param name="_value">����֤���ַ���</param>
        /// <returns>�Ƿ�Ϸ���boolֵ</returns>
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
        /// �ж�һ���ַ���
        /// </summary>
        /// <param name="_value">����֤���ַ���</param>
        /// <returns>�Ƿ�Ϸ���boolֵ</returns>
        public static bool IsPhoneNum(string _value)
        {
            Regex regex = new Regex(@"^(86)?(-)?(0\d{2,3})?(-)?(\d{7,8})(-)?(\d{3,5})?$", RegexOptions.IgnoreCase);
            return regex.Match(_value).Success;
        }

        /// <summary>
        /// �Ƿ����ת��Ϊ����
        /// </summary>
        /// <param name="_value">����֤���ַ���</param>
        /// <returns>�Ƿ�Ϸ���boolֵ</returns>
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
        /// �ж�һ���ַ����Ƿ�Ϊ��ַ
        /// </summary>
        /// <param name="_value">����֤���ַ���</param>
        /// <returns>�Ƿ�Ϸ���boolֵ</returns>
        public static bool IsUrl(string _value)
        {
            Regex regex = new Regex(@"(http://)?([\w-]+\.)*[\w-]+(/[\w- ./?%&=]*)?", RegexOptions.IgnoreCase);
            return regex.Match(_value).Success;
        }

        /// <summary>
        /// �ж�һ���ַ����Ƿ�Ϊ��ĸ������
        /// </summary>
        /// <param name="_value">����֤���ַ���</param>
        /// <returns>�Ƿ�Ϸ���boolֵ</returns>
        public static bool IsWordAndNum(string _value)
        {
            Regex regex = new Regex("[a-zA-Z0-9]?");
            return regex.Match(_value).Success;
        }

        /// <summary>
        /// ��֤��������
        /// </summary>
        /// <param name="code">��������</param>
        /// <returns></returns>
        public static bool IsZipCode(string code)
        {
            Regex regex = new Regex("^[a-zA-Z0-9 ]{3,12}$");
            return regex.Match(code).Success;
        }

        /// <summary>
        /// ���ַ���ת������
        /// </summary>
        /// <param name="_value">�ַ���</param>
        /// <param name="_defaultValue">Ĭ��ֵ</param>
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
        /// ������֤��6-16λ�ַ�,����Ӣ�ġ����ּ���_������-����ɣ�
        /// </summary>
        /// <param name="pwd">����</param>
        /// <returns></returns>
        public static bool ValidatePwd(string pwd)
        {
            Regex regex = new Regex(@"^([A-Z]|[a-z]|[\d]|[-]|[_]){6,16}$");
            return regex.Match(pwd).Success;
        }

        /// <summary>
        /// �û�����֤��4-16 ���ַ��������ġ�Ӣ�ĺ����֣�
        /// </summary>
        /// <param name="userName">�û���</param>
        /// <returns></returns>
        public static bool ValidateUserName(string userName)
        {
            Regex regex = new Regex(@"^[\.\w\u4e00-\u9fa5\uF900-\uFA2D]{2,16}$");
            return regex.Match(userName).Success;
        }

        /// <summary>
        /// �ж������Ƿ����
        /// </summary>
        /// <param name="myDate">��Ҫ�жϵ�����</param>
        /// <returns></returns>
        public static bool ValidDate(string myDate)
        {
            return (!IsStringDate(myDate) || CompareDate(myDate, DateTime.Now.ToShortDateString(), 0));
        }

        /// <summary>
        /// ���ڱȽ�
        /// </summary>
        /// <param name="today">����ĳ������</param>
        /// <param name="writeDate">��������</param>
        /// <param name="n">�Ƚ�����</param>
        /// <returns>������������true��С�ڷ���false</returns>
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