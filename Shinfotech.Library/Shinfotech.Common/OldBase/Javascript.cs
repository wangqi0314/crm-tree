using System;
using System.Web.UI;
namespace ShInfoTech.Common
{
    /// <summary>
    /// Javascript处理类
    /// </summary>
    public class Javascript
    {
        /// <summary>
        /// 提示消息并且返回上一页
        /// </summary>
        /// <param name="pMessage">提示的消息</param>
        /// <param name="pKey">唯一的键值</param>
        /// <param name="pPage">注册脚本的page对象</param>
        public static void RegisterAlertAndBackScript(string pMessage, string pKey, Page pPage)
        {
            string script = "<script language='javascript' defer>alert('" + pMessage + "');history.back();</script>";
            pPage.ClientScript.RegisterStartupScript(pPage.GetType(), pPage.UniqueID + pKey, script, false);
        }

        /// <summary>
        /// 提示消息后转向其他网址
        /// </summary>
        ///  <param name="pMessage">消息内容</param>
        ///  <param name="pNavigateTo">提示消息后</param>
        ///  <param name="pKey">唯一的键值</param>
        /// <param name="pPage">要注册脚本的page对象</param>
        public static void RegisterAlertScript(string pMessage, string pNavigateTo, string pKey, Page pPage)
        {
            string script = "<script language='javascript' defer>alert('" + pMessage + "');document.location.href='" + pNavigateTo + "';</script>";
            pPage.ClientScript.RegisterStartupScript(pPage.GetType(), pPage.UniqueID + pKey, script, false);
        }

        /// <summary>
        /// 提示消息,然后让用户选择,选择是/否的话,就转向不同的网址
        /// </summary>
        /// <param name="pMessage">消息内容</param>
        /// <param name="pYesNavigateTo">转向的网址</param>
        /// <param name="pNoNavigateTo"></param>
        /// <param name="pKey"></param>
        /// <param name="pPage"></param>
        public static void RegisterConfirmScript(string pMessage, string pYesNavigateTo, string pNoNavigateTo, string pKey, Page pPage)
        {
            string script = "<script language='javascript' defer>if(confirm('" + pMessage + "'))\r\n                      document.location.href='" + pYesNavigateTo + "';\r\n                    else\r\n                      document.location.href='" + pNoNavigateTo + "'</script>";
            pPage.ClientScript.RegisterStartupScript(pPage.GetType(), pPage.UniqueID + pKey, script, false);
        }
    }
}

