using CRMTree.Model;
using ShInfoTech.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMTree.DAL
{
    public class DL_TabLinkList
    {
        public MD_TabLinks getTabLinkList(long UserCode)
        {
            StringBuilder strSql = new StringBuilder();
            DataSet ds = new DataSet();
            if (UserCode == 0)
            {
                strSql.Append("select TL_Code,TL_UG_Code,TL_Parent,TL_Level,TL_Order,TL_TagCD,TL_Text_EN,TL_Text_CN,TL_Link,TL_Children from CT_Tab_Links TL where TL_UG_Code=0 order by TL_Parent,TL_Order");
                ds = SqlHelper.ExecuteDataset(strSql.ToString());
            }
            else
            {
                strSql.Append("select TL_Code,TL_UG_Code,TL_Parent,TL_Level,TL_Order,TL_TagCD,TL_Text_EN,TL_Text_CN,TL_Link,TL_Children from CT_Tab_Links TL inner join CT_All_Users AU on TL.TL_UG_Code=AU.AU_UG_Code where AU.AU_Code=@AU_Code order by TL_Parent,TL_Order");
                SqlParameter[] parameters = { new SqlParameter("@AU_Code", SqlDbType.Int) };
                parameters[0].Value = UserCode;
                ds = SqlHelper.ExecuteDataset(CommandType.Text, strSql.ToString(), parameters);
            }
            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count <= 0)
            { return null; }
            MD_TabLinks myTabLinkList = new MD_TabLinks();
            myTabLinkList.CT_Tab_LinkList = new List<Model.CT_Tab_Links>();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                myTabLinkList.CT_Tab_LinkList.Add(new Model.CT_Tab_Links
                {
                    TL_Code = int.Parse(ds.Tables[0].Rows[i]["TL_Code"].ToString()),
                    TL_UG_Code = int.Parse(ds.Tables[0].Rows[i]["TL_UG_Code"].ToString()),
                    TL_Parent = int.Parse(ds.Tables[0].Rows[i]["TL_Parent"].ToString()),
                    TL_Level = int.Parse(ds.Tables[0].Rows[i]["TL_Level"].ToString()),
                    TL_Order = int.Parse(ds.Tables[0].Rows[i]["TL_Order"].ToString()),
                    TL_TagCD = ds.Tables[0].Rows[i]["TL_TagCD"].ToString(),
                    TL_Text_EN = ds.Tables[0].Rows[i]["TL_Text_EN"].ToString(),
                    TL_Text_CN = ds.Tables[0].Rows[i]["TL_Text_CN"].ToString(),
                    TL_Link = ds.Tables[0].Rows[i]["TL_Link"].ToString(),
                    TL_Children = int.Parse(ds.Tables[0].Rows[i]["TL_Children"].ToString())
                });
            }
            return myTabLinkList;
        }
        public string getTabName(string url, long au_Code)
        {
            if (string.IsNullOrEmpty(url))
                return string.Empty;
            else
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select TL_Text_EN,TL_Text_CN from CT_Tab_Links TL inner join CT_All_Users AU on TL.TL_UG_Code=AU.AU_UG_Code where AU.AU_Code=@Au_Code and TL_Link=@Url");
                SqlParameter[] parameters = { new SqlParameter("@Au_Code", SqlDbType.Int), new SqlParameter("@Url", SqlDbType.NVarChar) };
                parameters[0].Value = au_Code;
                parameters[1].Value = url.Trim();

                Object result = SqlHelper.ExecuteScalar( CommandType.Text, strSql.ToString(), parameters);

                if (result != null)
                    return result.ToString();
                else
                    return string.Empty;

            }
        }

        /// <summary>
        /// 根据某一Url 获取他的树形层级
        /// </summary>
        /// <param name="UG_Code"></param>
        /// <param name="LinkUrl"></param>
        /// <returns></returns>
        public MD_TabLinks GetTabLevelList(int UG_Code, string LinkUrl)
        {
            SqlParameter[] parameters = { new SqlParameter("@UG_Code", SqlDbType.Int),
                                           new SqlParameter("@Link", SqlDbType.NVarChar)};
            parameters[0].Value = UG_Code;
            parameters[1].Value = LinkUrl;
            DataSet ds = SqlHelper.ExecuteDataset( CommandType.StoredProcedure, "09_LK_GetCurrentPage", parameters);
            MD_TabLinks _o = new MD_TabLinks();
            _o.CT_Tab_LinkList = DataHelper.ConvertToList<CT_Tab_Links>(ds);
            return _o;
        }
    }
}
