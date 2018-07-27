using CRMTree.Model;
using CRMTree.Model.Common;
using CRMTree.Model.Reports;
using CRMTree.Model.User;
using ShInfoTech.Common;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CRMTree.BLL
{
    /// <summary>
    /// 本类用来对Report报表的Name，Desc，Query，用ParamList进行各种替换；
    /// </summary>
    public class BL_ReportReplace
    {
        #region 报表参数替换
        /// <summary>
        /// 针对报表的各类参数进行替换
        /// </summary>
        /// <param name="Report"></param>
        /// <returns></returns>
        public static MD_ReportList ReportParamReplace(MD_ReportList Report)
        {
            return ReportParamReplace(Report, EM_ParameterMode.Background, null);
        }
        /// <summary>
        /// 针对报表的各类参数进行替换
        /// </summary>
        /// <param name="o"></param>
        /// <param name="Mode"></param>
        /// <param name="Paramterslist"></param>
        /// <returns></returns>
        public static MD_ReportList ReportParamReplace(MD_ReportList o, EM_ParameterMode Mode, string Paramterslist,bool isEn=true)
        {
            if (o == null)
            {
                return null;
            }
            o = oValCopyDefault(o);
            if (Mode == EM_ParameterMode.Page)
            {
                return NameDescReplace_Page(o, Paramterslist,isEn);
            }
            else if (Mode == EM_ParameterMode.Background)
            {
                return NameDescReplace_Background(o,isEn);
            }
            else
            {
                return o;
            }
        }
        #endregion

        #region 私有方法
        /// <summary>
        /// 用后台参数替换报表
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        private static MD_ReportList NameDescReplace_Background(MD_ReportList o,bool isEn = true)
        {
            for (int i = 0; i < o.CT_Reports_List.Count; i++)
            {
                if (isNull(o.CT_Reports_List[i].PL_Tag))
                {
                    continue;
                }
                if (i != 0 && o.CT_Reports_List[i].RP_Code == o.CT_Reports_List[i - 1].RP_Code)
                {
                    o = oRowRemove(o, i);
                    i = i - 1;
                }
                if (o.CT_Reports_List[i].PL_Type == 10)
                {
                    string _v = BL_MyCar.getMake_Model_Style(1, int.Parse(o.CT_Reports_List[i].PL_Default), isEn);
                    o = oReplace(o, i, _v);
                }
                else if (o.CT_Reports_List[i].PL_Type == 11)
                {
                    string _v = BL_MyCar.getMake_Model_Style(2, int.Parse(o.CT_Reports_List[i].PL_Default), isEn);
                    o = oReplace(o, i, _v);
                }
                else if (o.CT_Reports_List[i].PL_Type == 12)
                {
                    string _v = BL_MyCar.getMake_Model_Style(3, int.Parse(o.CT_Reports_List[i].PL_Default), isEn);
                    o = oReplace(o, i, _v);
                }
                else if (o.CT_Reports_List[i].PL_Type == 14)
                {
                    string _v = new BL_Appt_Service().GetAdviserName(o.CT_Reports_List[i].PL_Default);
                    o = oReplace(o, i, _v);
                }
                else if (o.CT_Reports_List[i].PL_Type == 20)
                {
                    string _v = new BL_Appt_Service().Get_PeriodicalsText(o.CT_Reports_List[i].PL_Default);
                    o = oReplace(o, i, _v);
                }
                else
                {
                    o = oReplace(o, i);
                }
            }
            return o;
        }

        /// <summary>
        /// 用前台参数替换报表
        /// </summary>
        /// <param name="o"></param>
        /// <param name="Paramterslist"></param>
        /// <returns></returns>
        private static MD_ReportList NameDescReplace_Page(MD_ReportList o, string Paramterslist,bool isEn=true)
        {
            string[] ParamtersArray = Paramterslist.Split(',');
            int Pa = 0;
            if (ParamtersArray.Length <= 0)
            {
                return null;
            }
            for (int i = 0; i < o.CT_Reports_List.Count; i++)
            {
                if (isNull(o.CT_Reports_List[i].PL_Tag))
                {
                    Pa++;
                    continue;
                }
                if (i != 0 && o.CT_Reports_List[i].RP_Code == o.CT_Reports_List[i - 1].RP_Code)
                {
                    o = oRowRemove(o, i);
                    i = i - 1;
                }
                if (o.CT_Reports_List[i].PL_Type == 10)
                {
                    string _v = BL_MyCar.getMake_Model_Style(1, int.Parse(ParamtersArray[Pa].ToString()),isEn);
                    o = oReplace(o, i, _v);
                }
                else if (o.CT_Reports_List[i].PL_Type == 11)
                {
                    string _v = BL_MyCar.getMake_Model_Style(2, int.Parse(ParamtersArray[Pa].ToString()), isEn);
                    o = oReplace(o, i, _v);
                }
                else if (o.CT_Reports_List[i].PL_Type == 12)
                {
                    string _v = BL_MyCar.getMake_Model_Style(3, int.Parse(ParamtersArray[Pa].ToString()), isEn);
                    o = oReplace(o, i, _v);
                }
                else if (o.CT_Reports_List[i].PL_Type == 14)
                {
                    string _v = new BL_Appt_Service().GetAdviserName(ParamtersArray[Pa].ToString());
                    o = oReplace(o, i, _v);
                }
                else if (o.CT_Reports_List[i].PL_Type == 20)
                {
                    string _v = new BL_Appt_Service().Get_PeriodicalsText(ParamtersArray[Pa].ToString());
                    o = oReplace(o, i, _v);
                }
                else
                {
                    o = oReplace(o, i);
                }
                Pa++;
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
        /// 对象，某一位置，某一值的替换
        /// </summary>
        /// <param name="o"></param>
        /// <param name="i"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private static string Replace(MD_ReportList o, int i, string key, string value)
        {
            if (o == null || o.CT_Reports_List[i] == null)
            {
                return key;
            }
            if (value == null)
            {
                value = o.CT_Reports_List[i].PL_Default;
            }
            key = key.Replace(o.CT_Reports_List[i].PL_Tag, value);
            return key;
        }
        /// <summary>
        /// 用默认值替换对象i行的对应参数
        /// </summary>
        /// <param name="o"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        private static MD_ReportList oReplace(MD_ReportList o, int i)
        {
            return oReplace(o, i, null);
        }
        /// <summary>
        /// 用给定值替换对象i行的对应参数
        /// </summary>
        /// <param name="o"></param>
        /// <param name="i"></param>
        /// <param name="_v"></param>
        /// <returns></returns>
        private static MD_ReportList oReplace(MD_ReportList o, int i, string _v)
        {
            if (o == null)
                return new MD_ReportList();
            if (!isNull(o.CT_Reports_List[i].RP_Name_EN))
            {
                o.CT_Reports_List[i].RP_Name_EN = Replace(o, i, o.CT_Reports_List[i].RP_Name_EN, _v);
            }
            if (!isNull(o.CT_Reports_List[i].RP_Desc_EN))
            {
                o.CT_Reports_List[i].RP_Desc_EN = Replace(o, i, o.CT_Reports_List[i].RP_Desc_EN, _v);
            }
            if (!isNull(o.CT_Reports_List[i].RP_Query))
            {
                o.CT_Reports_List[i].RP_Query = Replace(o, i, o.CT_Reports_List[i].RP_Query, _v);
            }
            return o;
        }
        /// <summary>
        /// 对象列表替换成功后，删除列表的i-1行
        /// </summary>
        /// <param name="o"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        private static MD_ReportList oRowRemove(MD_ReportList o, int i)
        {
            if (o == null || o.CT_Reports_List[i] == null || o.CT_Reports_List[i - 1] == null)
            {
                return new MD_ReportList();
            }
            o.CT_Reports_List[i].RP_Name_EN = o.CT_Reports_List[i - 1].RP_Name_EN;
            o.CT_Reports_List[i].RP_Desc_EN = o.CT_Reports_List[i - 1].RP_Desc_EN;
            o.CT_Reports_List[i].RP_Query = o.CT_Reports_List[i - 1].RP_Query;
            o.CT_Reports_List.Remove(o.CT_Reports_List[i - 1]);
            return o;
        }
        /// <summary>
        /// 如果获取的value不为空就替换默认的Default的值
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        private static MD_ReportList oValCopyDefault(MD_ReportList o)
        {
            if (o == null)
            {
                return new MD_ReportList();
            }
            for (int i = 0; i < o.CT_Reports_List.Count; i++)
            {
                if (!isNull(o.CT_Reports_List[i].PV_Val))
                {
                    o.CT_Reports_List[i].PL_Default = o.CT_Reports_List[i].PV_Val;
                }
            }
            return o;
        }
        #endregion
        #endregion
    }
}
