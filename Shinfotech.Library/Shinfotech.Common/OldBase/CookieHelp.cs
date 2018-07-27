using System;
using System.Collections;
using System.Web;
using System.Collections.Generic;

namespace ShInfoTech.Common
{
    /// <summary>
    /// COOKIE������
    /// </summary>
    public class CookieHelp
    {

        /// <summary>
        ///  ɾ��COOKIE����
        /// </summary>
        /// <param name="strCookieName">Cookie��������</param>
        public static void DelCookie(string strCookieName)
        {
            DelCookie(strCookieName, "");
        }

        /// <summary>
        /// ɾ��COOKIE����
        /// </summary>
        /// <param name="strCookieName">Cookie��������</param>
        /// <param name="strDomain">������,���������;����</param>
        public static void DelCookie(string strCookieName, string strDomain)
        {
            HttpCookie objCookie = new HttpCookie(strCookieName.Trim());
            if (strDomain.Length > 0)
            {
                objCookie.Domain = strDomain;
            }
            objCookie.Expires = DateTime.Now.AddYears(-1);
            HttpContext.Current.Response.Cookies.Add(objCookie);
        }

        /// <summary>
        /// ɾ��ĳ��COOKIE����ĳ��Key�Ӽ��������ɹ������ַ���"success"��������󱾾Ͳ����ڣ��򷵻��ַ���null
        /// </summary>
        /// <param name="strCookieName">Cookie��������</param>
        /// <param name="strKeyName">>Key����</param>
        /// <param name="iExpires">COOKIE������Чʱ�䣨��������1��ʾ������Ч��0�͸�������ʾ������Чʱ�䣬���ڵ���2��ʾ������Ч������31536000��=1��=(60*60*24*365)��ע�⣺�����޸Ĺ��ܣ�ʵ���ؽ����ǣ�����ʱ��ҲҪ���裬��Ϊû�취��þɵ���Ч��</param>
        /// <returns>������󱾾Ͳ����ڣ��򷵻��ַ���null����������ɹ������ַ���"success"��</returns>
        public static string DelCookieKey(string strCookieName, string strKeyName, int iExpires)
        {
            if (HttpContext.Current.Request.Cookies[strCookieName] == null)
            {
                return null;
            }
            HttpCookie objCookie = HttpContext.Current.Request.Cookies[strCookieName];
            objCookie.Values.Remove(strKeyName);
            if (iExpires > 0)
            {
                if (iExpires == 1)
                {
                    objCookie.Expires = DateTime.MaxValue;
                }
                else
                {
                    objCookie.Expires = DateTime.Now.AddSeconds((double)iExpires);
                }
            }
            HttpContext.Current.Response.Cookies.Add(objCookie);
            return "success";
        }

        /// <summary>
        ///  �޸�ĳ��COOKIE����ĳ��Key���ļ�ֵ �� ��ĳ��COOKIE�������Key�� �����ñ������������ɹ������ַ���"success"��������󱾾Ͳ����ڣ��򷵻��ַ���null��
        /// </summary>
        /// <param name="strCookieName">Cookie��������</param>
        /// <param name="strKeyName">Key����</param>
        /// <param name="KeyValue">Key��ֵ</param>
        /// <param name="iExpires">COOKIE������Чʱ�䣨��������1��ʾ������Ч��0�͸�������ʾ������Чʱ�䣬���ڵ���2��ʾ������Ч������31536000��=1��=(60*60*24*365)��ע�⣺�����޸Ĺ��ܣ�ʵ���ؽ����ǣ�����ʱ��ҲҪ���裬��Ϊû�취��þɵ���Ч��</param>
        /// <returns>������󱾾Ͳ����ڣ��򷵻��ַ���null����������ɹ������ַ���"success"��</returns>
        public static string EditCookie(string strCookieName, string strKeyName, string KeyValue, int iExpires)
        {
            return EditCookie(strCookieName, strKeyName, KeyValue, iExpires, "");
        }


        /// <summary>
        ///  �޸�ĳ��COOKIE����ĳ��Key���ļ�ֵ �� ��ĳ��COOKIE�������Key�� �����ñ������������ɹ������ַ���"success"��������󱾾Ͳ����ڣ��򷵻��ַ���null��
        /// </summary>
        /// <param name="strCookieName">Cookie��������</param>
        /// <param name="strKeyName">Key����</param>
        /// <param name="KeyValue">Key��ֵ</param>
        /// <param name="iExpires">COOKIE������Чʱ�䣨��������1��ʾ������Ч��0�͸�������ʾ������Чʱ�䣬���ڵ���2��ʾ������Ч������31536000��=1��=(60*60*24*365)��ע�⣺�����޸Ĺ��ܣ�ʵ���ؽ����ǣ�����ʱ��ҲҪ���裬��Ϊû�취��þɵ���Ч��</param>
        /// <param name="strDomains">������,���������;����</param>
        /// <param name="strPath">����·��</param>
        /// <returns>������󱾾Ͳ����ڣ��򷵻��ַ���null����������ɹ������ַ���"success"��</returns>
        public static string EditCookie(string strCookieName, string strKeyName, string KeyValue, int iExpires, string strDomain)
        {
            if (HttpContext.Current.Request.Cookies[strCookieName] == null)
            {
                return null;
            }
            HttpCookie objCookie = HttpContext.Current.Request.Cookies[strCookieName];
            objCookie[strKeyName] = HttpUtility.UrlEncode(KeyValue.Trim());
            if (iExpires > 0)
            {
                if (iExpires == 1)
                {
                    objCookie.Expires = DateTime.MaxValue;
                }
                else
                {
                    objCookie.Expires = DateTime.Now.AddSeconds((double)iExpires);
                }
            }
            HttpContext.Current.Response.Cookies.Add(objCookie);
            return "success";
        }


        /// <summary>
        /// ��ȡCookieĳ�������Valueֵ������Valueֵ��������󱾾Ͳ����ڣ��򷵻��ַ���null
        /// </summary>
        /// <param name="strCookieName">Cookie��������</param>
        /// <returns>Valueֵ��������󱾾Ͳ����ڣ��򷵻��ַ���null</returns>
        public static string GetCookie(string strCookieName)
        {
            if (HttpContext.Current.Request.Cookies[strCookieName] == null)
            {
                return null;
            }
            return HttpUtility.UrlDecode(HttpContext.Current.Request.Cookies[strCookieName].Value);
        }

        /// <summary>
        /// ��ȡCookieĳ�������ĳ��Key���ļ�ֵ������Key��ֵ��������󱾾Ͳ����ڣ��򷵻��ַ���null�����Key�������ڣ��򷵻��ַ���"KeyNonexistence"
        /// </summary>
        /// <param name="strCookieName">Cookie��������</param>
        /// <param name="strKeyName">Key����</param>
        /// <returns>Key��ֵ��������󱾾Ͳ����ڣ��򷵻��ַ���null�����Key�������ڣ��򷵻��ַ���"KeyNonexistence"</returns>
        public static string GetCookie(string strCookieName, string strKeyName)
        {
            if (HttpContext.Current.Request.Cookies[strCookieName] == null)
            {
                return null;
            }
            string _value = HttpContext.Current.Request.Cookies[strCookieName][strKeyName];
            return HttpUtility.UrlDecode(_value);
        }

        /// <summary>
        /// ����COOKIE���󲢸�Valueֵ���޸�COOKIE��ValueֵҲ�ô˷�������Ϊ��COOKIE�޸ı���������Expires
        /// </summary>
        /// <param name="strCookieName">COOKIE������</param>
        /// <param name="strValue">COOKIE����Valueֵ</param>
        public static void SetCookie(string strCookieName, string strValue)
        {
            SetCookie(strCookieName, 1, strValue, "");
        }

        /// <summary>
        ///  ����COOKIE���󲢸�Valueֵ���޸�COOKIE��ValueֵҲ�ô˷�������Ϊ��COOKIE�޸ı���������Expires
        /// </summary>
        /// <param name="strCookieName">COOKIE������</param>
        /// <param name="iExpires">COOKIE������Чʱ�䣨��������1��ʾ������Ч��0�͸�������ʾ������Чʱ�䣬���ڵ���2��ʾ������Ч������31536000��=1��=(60*60*24*365)</param>
        /// <param name="strValue">COOKIE����Valueֵ</param>
        public static void SetCookie(string strCookieName, int iExpires, string strValue)
        {
            SetCookie(strCookieName, iExpires, strValue, "");
        }

        /// <summary>
        /// ����COOKIE���󲢸�Valueֵ���޸�COOKIE��ValueֵҲ�ô˷�������Ϊ��COOKIE�޸ı���������Expires
        /// </summary>
        /// <param name="strCookieName">COOKIE������</param>
        /// <param name="iExpires">COOKIE������Чʱ�䣨��������1��ʾ������Ч��0�͸�������ʾ������Чʱ�䣬���ڵ���2��ʾ������Ч������31536000��=1��=(60*60*24*365)</param>
        /// <param name="dc">COOKIE����Dictionary string, string ����</param>
        /// <param name="strDomain">������</param>
        public static void SetCookie(string strCookieName, int iExpires, Dictionary<string, string> dc, string strDomain)
        {
            HttpCookie objCookie = new HttpCookie(strCookieName.Trim());
            foreach (KeyValuePair<string, string> kv in dc)
            {
                objCookie[kv.Key] = HttpUtility.UrlEncode(kv.Value.Trim());
            }
            if (strDomain.Length > 0)
            {
                objCookie.Domain = strDomain;
            }
            if (iExpires > 0)
            {
                if (iExpires == 1)
                {
                    objCookie.Expires = DateTime.MaxValue;
                }
                else
                {
                    objCookie.Expires = DateTime.Now.AddSeconds((double)iExpires);
                }
            }
            HttpContext.Current.Response.Cookies.Add(objCookie);
        }

        /// <summary>
        /// ����COOKIE���󲢸�Valueֵ���޸�COOKIE��ValueֵҲ�ô˷�������Ϊ��COOKIE�޸ı���������Expires
        /// </summary>
        /// <param name="strCookieName">COOKIE������</param>
        /// <param name="iExpires">COOKIE������Чʱ�䣨��������1��ʾ������Ч��0�͸�������ʾ������Чʱ�䣬���ڵ���2��ʾ������Ч������31536000��=1��=(60*60*24*365)</param>
        /// <param name="strValue">COOKIE����Valueֵ</param>
        /// <param name="strDomain">������</param>
        public static void SetCookie(string strCookieName, int iExpires, string strValue, string strDomain)
        {
            HttpCookie objCookie = new HttpCookie(strCookieName.Trim());
            objCookie.Value = HttpUtility.UrlEncode(strValue.Trim());
            if (strDomain.Length > 0)
            {
                objCookie.Domain = strDomain;
            }
            if (iExpires > 0)
            {
                if (iExpires == 1)
                {
                    objCookie.Expires = DateTime.MaxValue;
                }
                else
                {
                    objCookie.Expires = DateTime.Now.AddSeconds((double)iExpires);
                }
            }
            HttpContext.Current.Response.Cookies.Add(objCookie);
        }
    }
}