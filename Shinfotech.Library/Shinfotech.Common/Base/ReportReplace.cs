using CRMTree.Model;
using CRMTree.Model.Common;
using CRMTree.Model.Reports;
using CRMTree.Model.User;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ShInfoTech.Common
{
    /// <summary>
    /// 本类用来对Report报表的Name，Desc，Query，用Session，QueryString进行各种替换；
    /// 同时针对国际化的替换
    /// </summary>
    public class ReportReplace
    {
        /// <summary>
        /// 根据Session和QueryString替换参数
        /// </summary>
        /// <param name="Query"></param>
        /// <returns></returns>
        public static string ReportParamReplace(string Query)
        {
            if (isNull(Query))
            {
                return Query;
            }
            Query = SessionReplace(Query);
            Query = QuerryStringReplace(Query);
            return Query;
        }

        /// <summary>
        /// 针对国际化的Name替换
        /// </summary>
        /// <param name="Report"></param>
        /// <returns></returns>
        public static MD_ReportList NameReplace(MD_ReportList o)
        {
            if (o == null || o.CT_Reports_List == null)
            {
                return null;
            }
            for (int i = 0; i < o.CT_Reports_List.Count; i++)
            {
                o.CT_Reports_List[i].RP_Name_EN = o.CT_Reports_List[i].RP_Name_CN;
                o.CT_Reports_List[i].RP_Desc_EN = o.CT_Reports_List[i].RP_Desc_CN;
                o.CT_Reports_List[i].PL_Prompt_En = o.CT_Reports_List[i].PL_Prompt_Ch;
            }
            return o;
        }

        #region 内部处理方法
        /// <summary>
        /// 判断字符串是否为空
        /// </summary>
        /// <param name="_o"></param>
        /// <returns></returns>
        private static bool isNull(string _o)
        {
            return string.IsNullOrEmpty(_o);
        }
        /// <summary>
        /// 用Session值替换参数
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        private static string SessionReplace(string o)
        {
            if (null == HttpContext.Current || HttpContext.Current.Session["PublicUser"] == null)
            {
                return o;
            }
            if (Language.GetLang2() == EM_Language.en_us)
            {
                o = o.Replace("{{Lng}}", "101");
                o = o.Replace("{{Lng2}}", "_en");
            }
            else
            {
                o = o.Replace("{{Lng}}", "111");
                o = o.Replace("{{Lng2}}", "_cn");
            }

            MD_UserEntity UE = (MD_UserEntity)HttpContext.Current.Session["PublicUser"];
            if (UE.User.UG_UType == (int)UserIdentity.Generic)
            {
                o = o.Replace("{{User}}", UE.User.AU_Code.ToString());
                o = o.Replace("{{Empl}}", "-1");
                o = o.Replace("{{Dlr}}", "-1");
                o = o.Replace("{{UG}}", "-1");
                o = o.Replace("{{OEM}}", "-1");
            }
            else if (UE.User.UG_UType == (int)UserIdentity.Dealer)
            {
                o = o.Replace("{{User}}", UE.User.AU_Code.ToString());
                o = o.Replace("{{Empl}}", UE.DealerEmpl.DE_Code.ToString());
                o = o.Replace("{{Dlr}}", UE.Dealer.AD_Code.ToString());
                o = o.Replace("{{DG}}", UE.Dealer.AD_DG_Code == 0 ? "-1" : UE.Dealer.AD_DG_Code.ToString());
                o = o.Replace("{{UG}}", UE.User.AU_UG_Code == 0 ? "-1" : UE.User.AU_UG_Code.ToString());
                o = o.Replace("{{OEM}}", UE.Dealer.AD_OM_Code == 0 ? "-1" : UE.Dealer.AD_OM_Code.ToString());
            }
            else if (UE.User.UG_UType == (int)UserIdentity.DealerGroup)
            {
                o = o.Replace("{{User}}", UE.User.AU_Code.ToString());
                o = o.Replace("{{Empl}}", UE.DealerEmpl.DE_Code.ToString());
                o = o.Replace("{{Dlr}}", "-1");
                o = o.Replace("{{UG}}", UE.DealerGroup.DG_Code.ToString());
                o = o.Replace("{{OEM}}", "-1");
            }
            else if (UE.User.UG_UType == (int)UserIdentity.OEM)
            {
                o = o.Replace("{{User}}", UE.User.AU_Code.ToString());
                o = o.Replace("{{Empl}}", UE.DealerEmpl.DE_Code.ToString());
                o = o.Replace("{{Dlr}}", "-1");
                o = o.Replace("{{UG}}", "-1");
                o = o.Replace("{{OEM}}", UE.OEM.OM_Code.ToString());
            }
            
            return o;
        }
        /// <summary>
        /// 用QuerryString值替换参数
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        private static string QuerryStringReplace(string o)
        {
            if (null == HttpContext.Current || HttpContext.Current.Session["Paramater"] == null)
            {
                return o;
            }
            NameValueCollection con = (NameValueCollection)HttpContext.Current.Session["Paramater"];
            string Cust = con["Cust"];
            string Dlr = con["Dlr"];
            string DG = con["DG"];
            string oem = con["oem"];
            string UG = con["UG"];
            if (!isNull(Cust))
            {
                o = o.Replace("{{S_Cust}}", Cust);
            }
            if (!isNull(Dlr))
            {
                o = o.Replace("{{S_Dlr}}", Dlr);
            }
            if (!isNull(DG))
            {
                o = o.Replace("{{S_DG}}", DG);
            }
            if (!isNull(oem))
            {
                o = o.Replace("{{S_OEM}}", oem);
            }
            if (!isNull(UG))
            {
                o = o.Replace("{{S_UG}}", UG);
            }
            return o;
        }
        #endregion
    }
}
