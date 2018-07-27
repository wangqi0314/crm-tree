using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMTree.Model.Common
{
    public class IN_Language
    {
        /// <summary>
        /// Campaign标题 
        /// </summary>
        /// <param name="In"></param>
        /// <returns></returns>
        public static string CGL_Title(EM_Language In)
        {
            if (EM_Language.en_us == In) { return "Title"; }
            else if (EM_Language.zh_cn == In) { return "活动标题"; }
            return null;
        }
        /// <summary>
        /// Campaign描述 
        /// </summary>
        /// <param name="In"></param>
        /// <returns></returns>
        public static string CGL_Description(EM_Language In)
        {
            if (EM_Language.en_us == In) { return "Description"; }
            else if (EM_Language.zh_cn == In) { return "活动描述"; }
            return null;
        }
        /// <summary>
        /// Campaign 举办者  Active
        /// </summary>
        /// <param name="In"></param>
        /// <returns></returns>
        public static string CGL_Whom(EM_Language In)
        {
            if (EM_Language.en_us == In) { return "Whom"; }
            else if (EM_Language.zh_cn == In) { return "举办者"; }
            return null;
        }
        /// <summary>
        /// Campaign 状态
        /// </summary>
        /// <param name="In"></param>
        /// <returns></returns>
        public static string CGL_Active(EM_Language In)
        {
            if (EM_Language.en_us == In) { return "Active"; }
            else if (EM_Language.zh_cn == In) { return "活动状态"; }
            return null;
        }
        public static string CGL_Status(EM_Language In)
        {
            if (EM_Language.en_us == In) { return "Status"; }
            else if (EM_Language.zh_cn == In) { return "活动状态"; }
            return null;
        }
        /// <summary>
        /// Campaign 更新时间
        /// </summary>
        /// <param name="In"></param>
        /// <returns></returns>
        public static string CGL_Update_dt(EM_Language In)
        {
            if (EM_Language.en_us == In) { return "Update_dt"; }
            else if (EM_Language.zh_cn == In) { return "更新时间"; }
            return null;
        }
        /// <summary>
        /// Campaign 操作
        /// </summary>
        /// <param name="In"></param>
        /// <returns></returns>
        public static string CGL_Operate(EM_Language In)
        {
            if (EM_Language.en_us == In) { return "Operate"; }
            else if (EM_Language.zh_cn == In) { return "操作"; }
            return null;
        }
        public static string RP_Name(EM_Language In)
        {
            if (EM_Language.en_us == In) { return "Report Name"; }
            else if (EM_Language.zh_cn == In) { return "报表名称"; }
            return null;
        }
        public static string RP_UpdateDate(EM_Language In)
        {
            if (EM_Language.en_us == In) { return "Last Updated"; }
            else if (EM_Language.zh_cn == In) { return "上次更新时间"; }
            return null;
        }
        public static string RP_Description(EM_Language In)
        {
            if (EM_Language.en_us == In) { return "Description"; }
            else if (EM_Language.zh_cn == In) { return "报表描述"; }
            return null;
        }
    }
}
