using CRMTree.Model.Common;
using CRMTree.Model.Reports;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShInfoTech.Common
{
    /// <summary>
    /// 本类用于对Report 的各类参数的替换
    /// </summary>
    public class Parameter_Replace
    {
        public static DataTable getUser_value(DataTable dt,string User_Value)
        {
            if (dt == null)
            {
                return null;
            }
            DataTable NewDt = new DataTable();
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                NewDt.Columns.Add(dt.Columns[i].Caption);
            }
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["AU_Code"].ToString() == User_Value)
                {
                    NewDt.Rows.Add(dt.Rows[i].ItemArray);
                }
            }
            return NewDt;
        }

        public static string ReplaceContents(string Content, DataTable tb1, DataTable tb2)
        {
            if (tb1 == null || tb2 == null)
            {
                return Content;
            }
            for (int i = 0; i < tb1.Rows.Count; i++)
            {
                if (tb1.Rows[i][2] != null && tb1.Rows[i][2].ToString() != "")
                {
                    if (tb2.Columns.Contains(tb1.Rows[i][2].ToString()))
                    {
                        Content = Content.Replace("{{" + (i+1) + "}}", tb2.Rows[0][tb1.Rows[i][2].ToString()].ToString());
                    }
                }
            }
            for (int i = 0; i < tb1.Rows.Count; i++)
            {
                Content = Content.Replace("{{" + (i + 1) + "}}", "");
            }
            return Content;
        }
    }
}
