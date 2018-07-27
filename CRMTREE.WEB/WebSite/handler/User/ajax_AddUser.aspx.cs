using CRMTree.Public;
using Shinfotech.Tools;
using ShInfoTech.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class handler_ajax_AddUser : BasePage
{
    protected override void OnLoad(EventArgs e)
    {
         base.OnLoad(e);
         string strAction = RequestClass.GetString("action").ToLower();//操作动作 
         if ("add_targeted" == strAction || "list_user" == strAction || "add_user" == strAction || "delete_user" == strAction || "up_user" == strAction || "del_file" == strAction)
        {
            switch (strAction)
            {
                case "add_targeted":
                    Add_Customers_Targeted();//Customers Targeted 下拉框自定义添加
                    break;
                case "list_user":
                    List();//user 列表
                    break;
                case "add_user":
                    Add_User();//添加 user
                    break;
                case "up_user":
                    Update_user();//修改 user
                    break;
                case "delete_user":
                    userDelete();//删除 user
                    break;
            
            }
        }
        else
            Response.Write("-1");//action 参数不正确，没权限
    }

    #region Customers Targeted 下拉框自定义添加
    private void Add_Customers_Targeted()
    {
        int intCoede = RequestClass.GetInt("intCoede", 0);
        int intCG_Parameter = RequestClass.GetInt("targeted", 0);
        if (0 < intCoede && 0 < intCG_Parameter)
        {

        }
        else
        {
            Response.Write("-4");//参数不正确或未填写内容
        }
    }
    #endregion

    #region 查询
    private void List()
    {
        string strKeyword = Microsoft.JScript.GlobalObject.unescape(RequestClass.GetString("keyword")).Trim().Replace("'", "");
        string strDTstartDate = RequestClass.GetString("dtstartDate").Trim().Replace("'", "");
        string strDTendDate = RequestClass.GetString("dtendDate").Trim().Replace("'", "");
        string strSortRule = RequestClass.GetString("sortrule").Trim().Replace("'", "");
        string strSortField = RequestClass.GetString("sortfield").Trim().Replace("'", "");
        string orderClass = String.Empty;
        if (string.IsNullOrEmpty(strSortField))
        {
            strSortField = "AU_Update_dt";
            strSortRule = "desc";
            orderClass = "taxisDown";
        }
        else
        {
            if (strSortRule.Equals("asc"))
                orderClass = "taxisUp";
            else
                orderClass = "taxisDown";
        }
         
        StringBuilder sbWhere = new StringBuilder(" AU_CODE=");
        sbWhere.Append(UserSession.User.AU_Code);
        StringBuilder sbList = new StringBuilder();
        sbList.Append("<table class=\"t-bd\"><tr>");
        sbList.Append("<th class=\"w40\">Sequence </th>");
        sbList.Append("<th style=\"cursor:pointer;\" ");
        if (strSortField.Equals("AU_CODE"))
        {
            sbList.Append("class=\"w40 taxisCurrent\" onclick=\"changeOrder(this,'AU_CODE')\">ID<span class=\"");
            sbList.Append(orderClass);
            sbList.Append("\" title=\"Sort by the column \"></span></th>");
        }
        else
        {
            sbList.Append("class=\"w40\" onclick=\"changeOrder(this,'AU_CODE')\">ID<span class=\"taxis\" title=\"Sort by the column \"></span></th>");
        }
        sbList.Append("<th>AU_Name</th>");
        sbList.Append("<th>AU_Username</th>");
        sbList.Append("<th>AU_Password</th>");
        sbList.Append("<th>AU_Gender</th>");
        sbList.Append("<th>AU_Active_tag</th>");
        sbList.Append("<th style=\"cursor:pointer;\" ");
        if (strSortField.Equals("AU_Update_dt"))
        {
            sbList.Append("class=\"taxisCurrent\" onclick=\"changeOrder(this,'AU_Update_dt')\">AU_Update_dt<span class=\"");
            sbList.Append(orderClass);
            sbList.Append("\" title=\"Sort by the column \"></span></th>");
        }
        else
        {
            sbList.Append(" onclick=\"changeOrder(this,'AU_Update_dt')\">AU_Update_dt<span class=\"taxis\" title=\"Sort by the column \"></span></th>");
        }
        sbList.Append("<th class=\"w100\">Operate</th>");
        //初始化变量
        int intRowsCount = 0;//总行数
        int intPageCount = 0;//总页数  
        int intCurrentPage = RequestClass.GetInt("page", 1);//当前页
        int intPageSize = RequestClass.GetInt("pagesize", 15); ;//每页条数
        //var dt = DbHelperSQL.ProcedurePagerByProc("[dbo].[User_Lists]", "AU_CODE", "*", strSortField, strSortRule, sbWhere.ToString(), intCurrentPage, intPageSize, out intPageCount, out intRowsCount).Tables[0];
        var dt = new DataTable();
        if (null != dt && dt.Rows.Count > 0)
        {
            //生成分页html
            var strPager = new StringBuilder();
            if (1 < intPageCount) strPager = Pager.JavascriptPagination(true,intCurrentPage, intPageCount, intRowsCount);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                var a="";
                var b = "";
                var intID = dt.Rows[i]["AU_CODE"];
                var ss = Convert.ToInt32(dt.Rows[i]["AU_Gender"]);
                if (ss==1)
                {
                    a = "male";
                }
                else if (ss==0) 
                {
                    a = "female";
                }
                var cc = Convert.ToInt32(dt.Rows[i]["AU_Active_tag"]);
                if (cc == 1)
                {
                    b = "To enable the";
                }
                else if (cc == 0)
                {
                    b = "Is not enabled";
                }
                var intRows = i + 1;
                sbList.Append("<tr><td>");
                sbList.Append(((i + 1) + (intCurrentPage - 1) * intPageSize));
                sbList.Append("</td><td>");
                sbList.Append(dt.Rows[i]["AU_Code"]);
                sbList.Append("</td><td>");
                sbList.Append(dt.Rows[i]["AU_Name"]);
                sbList.Append("</td><td>");
                sbList.Append(dt.Rows[i]["AU_Username"]);
                sbList.Append("</td><td>");
                sbList.Append(dt.Rows[i]["AU_Password"]);
                sbList.Append("</td><td>");
                sbList.Append(a);
                sbList.Append("</td><td>");
                sbList.Append(b);
                sbList.Append("</td><td>");
                sbList.Append(dt.Rows[i]["AU_Update_dt"]);
                sbList.Append("</td><td>");
                sbList.Append("<a href=\"/manage/User/edit_User.aspx?id=");
                sbList.Append(intID);
                sbList.Append("\"><i class=\"btnModify\" title=\"edit\"></i></a>");
                sbList.Append("<a href=\"javascript:;\"><i class=\"btnDelete\" title=\"Delete\" onclick=\"Delete(");
                sbList.Append(intID);
                sbList.Append("')\"></i></a>");
                sbList.Append("</td></tr>");
                
            }
            sbList.Append("</table>");
            sbList.Append(strPager);
        }
        else
        {
            sbList.Append("<tr ><td colspan=\"10\"><div class=\"noData\">No matching data！</div></td></tr>");
            sbList.Append("</tbody>");
            sbList.Append("</table>");
        }
        Response.Write(sbList);
    }
    #endregion

    #region 添加 user
    private void Add_User()
    {
        int Gend = RequestClass.GetInt("Gend", 0);
        int IndusTry = RequestClass.GetInt("IndusTry", 0);
        int Occupation = RequestClass.GetInt("Occupation", 0);
        int Education = RequestClass.GetInt("Education", 0);
        int IDType = RequestClass.GetInt("IDType", 0);
        int Deraler = RequestClass.GetInt("Deraler", 0);
        int Group = RequestClass.GetInt("Group", 0);
        long UserCode = UserSession.User.AU_Code;
        int UGCode = UserSession.User.AU_UG_Code;
        string UserName = Microsoft.JScript.GlobalObject.unescape(RequestClass.GetString("UserName")).Replace("'", "").Trim();
        string Password = Microsoft.JScript.GlobalObject.unescape(RequestClass.GetString("Password")).Replace("'", "").Trim();
        string BirthdayData = Microsoft.JScript.GlobalObject.unescape(RequestClass.GetString("BirthdayData")).Replace("'", "").Trim();
        string IDCode = Microsoft.JScript.GlobalObject.unescape(RequestClass.GetString("IDCode")).Replace("'", "").Trim();
      

        if (Gend == 0 || IndusTry == 0 )
        {
            Response.Write("-2");
        }

        else
        {
            int rowsAffected = 0;
            SqlParameter[] parameters = {
                    new SqlParameter("@results", SqlDbType.Int,4),                    
					new SqlParameter("@Gend", SqlDbType.Int,50),
					new SqlParameter("@IndusTry", SqlDbType.Int,50),
                    new SqlParameter("@Occupation", SqlDbType.Int,50),
                    new SqlParameter("@Education", SqlDbType.Int,50),
                    new SqlParameter("@IDType", SqlDbType.Int,50),
                    new SqlParameter("@Deraler", SqlDbType.Int,50),
                    new SqlParameter("@Group", SqlDbType.Int,50),
                    new SqlParameter("@UserName", SqlDbType.NVarChar,50),
                    new SqlParameter("@Password", SqlDbType.NVarChar,50),
                    new SqlParameter("@BirthdayData", SqlDbType.NVarChar,100),
                    new SqlParameter("@IDCode",SqlDbType.NVarChar,100),
                    new SqlParameter("@UserCode", SqlDbType.BigInt,50),
					};
            parameters[0].Direction = ParameterDirection.Output;
            parameters[1].Value = Gend;
            parameters[2].Value = IndusTry;
            parameters[3].Value = Occupation;
            parameters[4].Value = Education;
            parameters[5].Value = IDType;
            parameters[6].Value = Deraler;
            parameters[7].Value = Group;
            parameters[8].Value = UserName;
            parameters[9].Value = Password;
            parameters[10].Value = BirthdayData;
            parameters[11].Value = IDCode;
            parameters[12].Value = UserCode;
            SqlHelper.ExecuteNonQuery(Tools.GetConnString(), CommandType.StoredProcedure, "CT_user_Inventory_Add", parameters);
            Response.Write(parameters[0].Value.ToString());
        }
    }
    #endregion

    #region 修改 user
    private void Update_user()
    {
        int tid = RequestClass.GetInt("tid", 0);
        int Makeid = RequestClass.GetInt("Makeid", 0);
        int Modeid = RequestClass.GetInt("Modeid", 0);
        int Styleid = RequestClass.GetInt("Styleid", 0);
        int typeid = RequestClass.GetInt("typeid", 0);
        int Yearsid = RequestClass.GetInt("Yearsid", 0);
        int COLOR_Eid = RequestClass.GetInt("COLOR_Eid", 0);
        int COLOR_Iid = RequestClass.GetInt("COLOR_Iid", 0);
        string vin = Microsoft.JScript.GlobalObject.unescape(RequestClass.GetString("vin")).Replace("'", "").Trim();
        string Lic = Microsoft.JScript.GlobalObject.unescape(RequestClass.GetString("Lic")).Replace("'", "").Trim();
        int mileage = RequestClass.GetInt("mileage", 0);
        if (0 >= tid)
        {
            Response.Write("-2");//参数 没有全填写  
        }
        else
        {

            int rowsAffected = 0;
            SqlParameter[] parameters = {
                    new SqlParameter("@results", SqlDbType.Int,4),
                    new SqlParameter("@tid",SqlDbType.Int,50),
					new SqlParameter("@Makeid", SqlDbType.Int,50),
					new SqlParameter("@Modeid", SqlDbType.Int,250),
					new SqlParameter("@Styleid", SqlDbType.Int,50),
                    new SqlParameter("@typeid", SqlDbType.Int,50),
					new SqlParameter("@Yearsid", SqlDbType.Int,50),
                    new SqlParameter("@COLOR_Eid", SqlDbType.TinyInt,100),
                    new SqlParameter("@COLOR_Iid", SqlDbType.TinyInt,50),                      
                    new SqlParameter("@vin",SqlDbType.NVarChar,100),
                    new SqlParameter("@Lic",SqlDbType.NVarChar,100),
                    new SqlParameter("@mileage",SqlDbType.Int,50),                 
                                        };
            parameters[0].Direction = ParameterDirection.Output;
            parameters[1].Value = tid;
            parameters[2].Value = Makeid;
            parameters[3].Value = Modeid;
            parameters[4].Value = Styleid;
            parameters[5].Value = typeid;
            parameters[6].Value = Yearsid;
            parameters[7].Value = COLOR_Eid;
            parameters[8].Value = COLOR_Iid;
            parameters[9].Value = vin;
            parameters[10].Value = Lic;
            parameters[11].Value = mileage;
            SqlHelper.ExecuteNonQuery(Tools.GetConnString(), CommandType.StoredProcedure, "CT_users_Update", parameters);
            Response.Write(parameters[0].Value.ToString());
        }

    }
    #endregion

    #region 删除 user
    private void userDelete()
    {
        int intci_code = RequestClass.GetInt("id", 0);
        if (0 >= intci_code) Response.Write("-2");//参数 没有全填写
        else
        {
            int rowsAffected = 0;
            SqlParameter[] parameters = {
                    new SqlParameter("@results", SqlDbType.Int,4),
					new SqlParameter("@ci_code", SqlDbType.Int,4) };
            parameters[0].Direction = ParameterDirection.Output;
            parameters[1].Value = intci_code;
            SqlHelper.ExecuteNonQuery(Tools.GetConnString(), CommandType.StoredProcedure, "CT_users_Delete", parameters);
            Response.Write(parameters[0].Value);
        }
    }
    #endregion 删除 user

}