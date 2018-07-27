using System;
using System.Collections.Generic;
using System.Web;
using System.Threading;
using System.Globalization;
using CRMTree.Model.Common;

namespace ShInfoTech.Common
{
    /// <summary>
    /// 国际化语言的处理
    /// </summary>
    public class Language
    {
        /// <summary>
        /// 获取当前的语言 zh-cn 中文，en-us 英文 
        /// </summary>
        /// <returns></returns>
        public static string GetLang1()
        {
            string _lang = Security.GetCookie("language");
            if (string.IsNullOrEmpty(_lang))
            {
                _lang = WebConfig.GetAppSettingString("DefaultLanguage");
            }
            if (string.IsNullOrEmpty(_lang))
            {
                _lang = "zh-cn";
            }
            SetLang_Cookies(_lang);
            return _lang;
        }
        public static EM_Language GetLang2()
        {
            string _lang = GetLang1();
            if (_lang == "zh-cn" || string.IsNullOrEmpty(_lang))
                return EM_Language.zh_cn;
            else
                return EM_Language.en_us;
        }
        /// <summary>
        /// 设置本地化语言
        /// </summary>
        public static void SetLang()
        {
            string language = GetLang1();
            if (!String.IsNullOrEmpty(language))
            {
                //UICulture - 决定了采用哪一种本地化资源，也就是使用哪种语言
                //Culture - 决定各种数据类型是如何组织，如数字与日期
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(language);
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(language);
            }
        }
        /// <summary>
        /// 将本地语言设置到Cookies
        /// </summary>
        /// <param name="_lan"></param>
        public static void SetLang_Cookies(string _lan)
        {
            string LangKey = "language";
            HttpContext.Current.Response.Cookies[LangKey].Value = _lan;
            HttpContext.Current.Response.Cookies[LangKey].Expires = DateTime.Now.AddDays(1);
        }
        /// <summary>
        /// 将本地语言设置到Cookies
        /// </summary>
        /// <param name="_lan"></param>
        public static void SetLang_Cookies(EM_Language _lan)
        {
            if (_lan == EM_Language.zh_cn)
            {
                /* 中文*/
                SetLang_Cookies("zh-cn");               
            }
            else if (_lan == EM_Language.en_us)
            {
                /* 英文*/
                SetLang_Cookies("en-us");
            }
        }
    }
}