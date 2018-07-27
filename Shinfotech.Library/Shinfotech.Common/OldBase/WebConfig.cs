using System;
using System.Web.Configuration;

namespace ShInfoTech.Common
{
    /// <summary>
    /// WebConfig�����ļ�������
    /// </summary>
    public static class WebConfig
    {
        /// <summary>
        /// ��Web.Config��ȡAppSettingString
        /// </summary>
        /// <param name="sAppSettingStringName"></param>
        /// <returns></returns>
        public static string GetAppSettingString(string sAppSettingStringName)
        {
            string str = string.Empty;
            try
            {
                for (int i = 0; i < WebConfigurationManager.AppSettings.Count; i++)
                {
                    if (sAppSettingStringName == WebConfigurationManager.AppSettings.Keys[i])
                    {
                        return WebConfigurationManager.AppSettings[sAppSettingStringName];
                    }
                }
            }
            catch
            {
                return str;
            }
            return str;
        }
        /// <summary>
        /// ��Web.Config��ȡConnectionString
        /// </summary>
        /// <param name="AConnectionStringName"></param>
        /// <returns></returns>
        public static string GetConnectionString(string AConnectionStringName)
        {
            try
            {
                return WebConfigurationManager.ConnectionStrings[AConnectionStringName].ConnectionString;
            }
            catch
            {
                return "";
            }
        }
    }
}

