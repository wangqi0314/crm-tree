using System;
using System.Web.UI;
namespace ShInfoTech.Common
{
    /// <summary>
    /// Javascript������
    /// </summary>
    public class Javascript
    {
        /// <summary>
        /// ��ʾ��Ϣ���ҷ�����һҳ
        /// </summary>
        /// <param name="pMessage">��ʾ����Ϣ</param>
        /// <param name="pKey">Ψһ�ļ�ֵ</param>
        /// <param name="pPage">ע��ű���page����</param>
        public static void RegisterAlertAndBackScript(string pMessage, string pKey, Page pPage)
        {
            string script = "<script language='javascript' defer>alert('" + pMessage + "');history.back();</script>";
            pPage.ClientScript.RegisterStartupScript(pPage.GetType(), pPage.UniqueID + pKey, script, false);
        }

        /// <summary>
        /// ��ʾ��Ϣ��ת��������ַ
        /// </summary>
        ///  <param name="pMessage">��Ϣ����</param>
        ///  <param name="pNavigateTo">��ʾ��Ϣ��</param>
        ///  <param name="pKey">Ψһ�ļ�ֵ</param>
        /// <param name="pPage">Ҫע��ű���page����</param>
        public static void RegisterAlertScript(string pMessage, string pNavigateTo, string pKey, Page pPage)
        {
            string script = "<script language='javascript' defer>alert('" + pMessage + "');document.location.href='" + pNavigateTo + "';</script>";
            pPage.ClientScript.RegisterStartupScript(pPage.GetType(), pPage.UniqueID + pKey, script, false);
        }

        /// <summary>
        /// ��ʾ��Ϣ,Ȼ�����û�ѡ��,ѡ����/��Ļ�,��ת��ͬ����ַ
        /// </summary>
        /// <param name="pMessage">��Ϣ����</param>
        /// <param name="pYesNavigateTo">ת�����ַ</param>
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

