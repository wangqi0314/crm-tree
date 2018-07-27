using CRMTree.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMTREE.BasePage
{
    /// <summary>
    /// 此类适用于各类数据的组合配置
    /// </summary>
    public class DataConfiguration
    {
        #region 新修复
        /// <summary>
        /// 此方法用于，对表格的某一字段的行集合用“,”链接为字符串
        /// </summary>
        /// <param name="RP_Query"></param>
        /// <param name="Query_Field"></param>
        /// <returns></returns>
        public static string GetString_Code(DataTable dt, string Query_Field)
        {
            string str_Query = string.Empty;
            if (dt == null || dt.Rows.Count <= 0)
            {
                return null;
            }
            if (dt.Columns[Query_Field].ColumnName != Query_Field)
            {
                return null;
            }
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                str_Query += dt.Rows[i][Query_Field].ToString() + ",";
            }
            if (str_Query.Length > 1)
            {
                str_Query = str_Query.Substring(0, str_Query.Length - 1);
            }
            return str_Query;
        }
        /// <summary>
        /// 根据Method方法，获取Ch_Utype的类型
        /// </summary>
        /// <param name="Method"></param>
        /// <returns></returns>
        public static string[] Get_CH_UType(string Method)
        {
            string[] a_M = Method.Split(',');
            string _CH_UType = string.Empty;
            #region 赋值
            for (int i = 0; i < a_M.Length; i++)
            {
                if (a_M[i] == "1")
                {
                    _CH_UType += "1,";
                }
                else if (a_M[i] == "2")
                {
                    _CH_UType += "3,";
                }
                else if (a_M[i] == "3")
                {
                    _CH_UType += "21,";
                }
                else if (a_M[i] == "4")
                {
                    _CH_UType += "23,";
                }
                else if (a_M[i] == "5")
                {
                    _CH_UType += "43,";
                }
                else if (a_M[i] == "6")
                {
                    _CH_UType += "61,";
                }
            }
            #endregion
            if (_CH_UType.Length > 1)
            {
                _CH_UType = _CH_UType.Substring(0, _CH_UType.Length - 1);
            }
            return _CH_UType.Split(',');
        }
        /// <summary>
        /// 获取His PTY
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static int GetPTY(int type)
        {
            int PTY = 10;
            if (type == 1)
            {
                PTY = 12;
            }
            return PTY;
        }
        #endregion
    }
}
