using CRMTree.Public;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using Shinfotech.Tools;
using ShInfoTech.Common;

public partial class handler_ajax_car : BasePage
{
    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        string strAction = RequestClass.GetString("action").ToLower();//操作动作 
        if ("add_targeted" == strAction || "list_car" == strAction || "add_car" == strAction || "delete_car" == strAction || "up_car" == strAction || "del_file" == strAction)
        {
            switch (strAction)
            {
                case "add_targeted":
                    Add_Customers_Targeted();//Customers Targeted 下拉框自定义添加
                    break;
                case "list_car":
                    List();//car 列表
                    break;
                case "add_car":
                    Add_car();//添加 car
                    break;
                case "up_car":
                    Update_car();//修改 car
                    break;
                case "delete_car":
                    carDelete();//删除 car
                    break;
                case "del_file":
                    Delete_File();//删除 upload File 物理文件
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
            strSortField = "CI_Update_dt";
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
        StringBuilder sbWhere = new StringBuilder(" CI_AU_CODE=");
        sbWhere.Append(UserSession.User.AU_Code);
        StringBuilder sbList = new StringBuilder();
        //if (0 < strKeyword.Length)
        //{
        //    sbWhere.Append(" and (CG_Title like '%");
        //    sbWhere.Append(strKeyword);
        //    sbWhere.Append("%' or CG_Desc like '%");
        //    sbWhere.Append(strKeyword);
        //    sbWhere.Append("%')");
        //}
        sbList.Append("<table class=\"t-bd\"><tr>");
        sbList.Append("<th class=\"w40\">Sequence </th>");
        sbList.Append("<th style=\"cursor:pointer;\" ");
        if (strSortField.Equals("ci_code"))
        {
            sbList.Append("class=\"w40 taxisCurrent\" onclick=\"changeOrder(this,'ci_code')\">ID<span class=\"");
            sbList.Append(orderClass);
            sbList.Append("\" title=\"Sort by the column \"></span></th>");
        }
        else
        {
            sbList.Append("class=\"w40\" onclick=\"changeOrder(this,'ci_code')\">ID<span class=\"taxis\" title=\"Sort by the column \"></span></th>");
        }
        sbList.Append("<th>Make</th>");
        sbList.Append("<th>Model</th>");
        sbList.Append("<th>Style</th>");
        sbList.Append("<th>VIN</th>");
        sbList.Append("<th>Mileage</th>");
        sbList.Append("<th>Years</th>");
        sbList.Append("<th style=\"cursor:pointer;\" ");
        if (strSortField.Equals("CI_Update_dt"))
        {
            sbList.Append("class=\"taxisCurrent\" onclick=\"changeOrder(this,'CI_Update_dt')\">CI_Update_dt<span class=\"");
            sbList.Append(orderClass);
            sbList.Append("\" title=\"Sort by the column \"></span></th>");
        }
        else
        {
            sbList.Append(" onclick=\"changeOrder(this,'CI_Update_dt')\">CI_Update_dt<span class=\"taxis\" title=\"Sort by the column \"></span></th>");
        }
        sbList.Append("<th class=\"w100\">Operate</th>");
        //初始化变量
        int intRowsCount = 0;//总行数
        int intPageCount = 0;//总页数  
        int intCurrentPage = RequestClass.GetInt("page", 1);//当前页
        int intPageSize = RequestClass.GetInt("pagesize", 15); ;//每页条数

        //得到数据表
        //var dt = DbHelperSQL.ProcedurePager("CT_cars", "ci_code", "*", strSortRule, strSortField, intCurrentPage, intPageSize, sbWhere.ToString(), out intPageCount, out intRowsCount).Tables[0];
        //var dt = DbHelperSQL.ProcedurePagerByProc("[Car_getList]", "ci_code", "*", strSortField, strSortRule, sbWhere.ToString(), intCurrentPage, intPageSize, out intPageCount, out intRowsCount).Tables[0];
        var dt = new DataTable();
        if (null != dt && dt.Rows.Count > 0)
        {
            //生成分页html
            var strPager = new StringBuilder();
            if (1 < intPageCount) strPager = Pager.JavascriptPagination(true,intCurrentPage, intPageCount, intRowsCount);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                var intID = dt.Rows[i]["ci_code"];
                var YEID = dt.Rows[i]["CI_YR_Code"];
                var ColorI = dt.Rows[i]["CI_Color_I"];
                var ColorE = dt.Rows[i]["CI_Color_E"];
                var VIN = dt.Rows[i]["CI_VIN"];
                var LC = dt.Rows[i]["CI_Mileage"];
                var MKID = dt.Rows[i]["MK_Code"];
                var MDID = dt.Rows[i]["CM_Code"];
                var CSID = dt.Rows[i]["CS_Code"];
                var intRows = i + 1;
                sbList.Append("<tr><td>");
                sbList.Append(((i + 1) + (intCurrentPage - 1) * intPageSize));
                sbList.Append("</td><td>");
                sbList.Append(dt.Rows[i]["ci_code"]);
                sbList.Append("</td><td>");
                sbList.Append(dt.Rows[i]["MK_Make_EN"]);
                sbList.Append("</td><td>");
                sbList.Append(dt.Rows[i]["CM_Model_EN"]);
                sbList.Append("</td><td>");
                sbList.Append(dt.Rows[i]["CS_Style_EN"]);
                sbList.Append("</td><td>");
                sbList.Append(dt.Rows[i]["CI_VIN"]);
                sbList.Append("</td><td>");
                sbList.Append(dt.Rows[i]["CI_Mileage"]);
                sbList.Append("</td><td>");
                sbList.Append(dt.Rows[i]["YR_Year"]);
                sbList.Append("</td><td>");
                sbList.Append(Convert.ToDateTime(dt.Rows[i]["CI_Update_dt"].ToString()).ToString("MM/dd/yyyy HH:mm:ss"));
                sbList.Append("</td><td>");
                sbList.Append("<a href=\"/manage/car/edit_car.aspx?id=");
                sbList.Append(intID);
                sbList.Append("&YEID=");
                sbList.Append(YEID);
                sbList.Append("&ColorI=");
                sbList.Append(ColorI);
                sbList.Append("&ColorE=");
                sbList.Append(ColorE);
                sbList.Append("&VIN=");
                sbList.Append(VIN);
                sbList.Append("&LC=");
                sbList.Append(LC);
                sbList.Append("&MKID=");
                sbList.Append(MKID);
                sbList.Append("&MDID=");
                sbList.Append(MDID);
                sbList.Append("&CSID=");
                sbList.Append(CSID);
                sbList.Append("\"><i class=\"btnModify\" title=\"edit\"></i></a>");
                sbList.Append("<a href=\"javascript:;\"><i class=\"btnDelete\" title=\"Delete\" onclick=\"Delete(");
                sbList.Append(intID);
                sbList.Append(",'");
                sbList.Append(dt.Rows[i]["CI_VIN"]);
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

    #region 添加 car
    private void Add_car()
    {
        int Styleid = RequestClass.GetInt("Styleid", 0);
        int Yearsid = RequestClass.GetInt("Yearsid", 0);
        int COLOR_Eid = RequestClass.GetInt("COLOR_Eid", 0);
        int COLOR_Iid = RequestClass.GetInt("COLOR_Iid", 0);
        long UserCode = UserSession.User.AU_Code;
        string strFilename = Microsoft.JScript.GlobalObject.unescape(RequestClass.GetString("hdFileNmae")).Replace("'", "").Trim();
        string vin = Microsoft.JScript.GlobalObject.unescape(RequestClass.GetString("vin")).Replace("'", "").Trim();
        int mileage = RequestClass.GetInt("mileage", 0);
        string Lic = Microsoft.JScript.GlobalObject.unescape(RequestClass.GetString("Lic")).Replace("'", "").Trim();

        if (Styleid == 0 || Yearsid == 0)
        {
            Response.Write("-2");
        }

        else
        {
            int rowsAffected = 0;
            SqlParameter[] parameters = {
                    new SqlParameter("@results", SqlDbType.Int,4),    
                    new SqlParameter("@Styleid", SqlDbType.Int,50),
                    new SqlParameter("@Yearsid", SqlDbType.Int,50),
                    new SqlParameter("@COLOR_Eid", SqlDbType.Int,50),
                    new SqlParameter("@COLOR_Iid", SqlDbType.Int,50),
                    new SqlParameter("@vin", SqlDbType.NVarChar,50),
                    new SqlParameter("@Lic", SqlDbType.NVarChar,50),
                    new SqlParameter("@mileage", SqlDbType.Int,100),
                    new SqlParameter("@strFilename", SqlDbType.NVarChar,100),
                    new SqlParameter("@UserCode", SqlDbType.BigInt),
					};
            parameters[0].Direction = ParameterDirection.Output;
            parameters[1].Value = Styleid;
            parameters[2].Value = Yearsid;
            parameters[3].Value = COLOR_Eid;
            parameters[4].Value = COLOR_Iid;
            parameters[5].Value = vin;
            parameters[6].Value = Lic;
            parameters[7].Value = mileage;
            parameters[8].Value = strFilename;
            parameters[9].Value = UserCode;
            SqlHelper.ExecuteNonQuery(Tools.GetConnString(), CommandType.StoredProcedure, "CT_Car_Inventory_Add", parameters);
            Response.Write(parameters[0].Value.ToString());
        }
    }
    #endregion

    #region 修改 car
    private void Update_car()
    {        
        int tid = RequestClass.GetInt("tid", 0);
        int Styleid = RequestClass.GetInt("Styleid", 0);
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
					new SqlParameter("@Styleid", SqlDbType.Int,50),
					new SqlParameter("@Yearsid", SqlDbType.Int,50),
                    new SqlParameter("@COLOR_Eid", SqlDbType.TinyInt,100),
                    new SqlParameter("@COLOR_Iid", SqlDbType.TinyInt,50),                      
                    new SqlParameter("@vin",SqlDbType.NVarChar,100),
                    new SqlParameter("@Lic",SqlDbType.NVarChar,100),
                   new SqlParameter("@mileage",SqlDbType.Int,50),                 
                                         };
            parameters[0].Direction = ParameterDirection.Output;
            parameters[1].Value = tid;
            parameters[2].Value = Styleid;
            parameters[3].Value = Yearsid;
            parameters[4].Value = COLOR_Eid;
            parameters[5].Value = COLOR_Iid;
            parameters[6].Value = vin;
            parameters[7].Value = Lic;
            parameters[8].Value = mileage;
            SqlHelper.ExecuteNonQuery(Tools.GetConnString(), CommandType.StoredProcedure, "CT_cars_Update", parameters);
            Response.Write(parameters[0].Value.ToString());  
        }

    }
    #endregion

    #region 删除 car
    private void carDelete()
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
            SqlHelper.ExecuteNonQuery(Tools.GetConnString(), CommandType.StoredProcedure, "CT_cars_Delete", parameters);
            Response.Write(parameters[0].Value);
        }
    }
    #endregion 删除 car

    #region 删除 upload File 物理文件
    private void Delete_File()
    {
        string strFileName = RequestClass.GetString("fullname");
        string strPath = System.Configuration.ConfigurationManager.AppSettings["FileUploadPath"];
        if (1 < strFileName.Length)
        {
            string[] ArrFileName = strFileName.Split(',');
            for (int i = 0; i < ArrFileName.Length; i++)
            {
                string strFname = ArrFileName[i].ToString().Trim();
                if (0 < strFname.Length)
                {
                    try
                    {
                        //判断文件是不是存在
                        if (File.Exists(strPath + strFileName))
                        {
                            //如果存在则删除
                            File.Delete(strPath + strFileName);
                        }
                    }
                    catch (Exception)
                    {
                    }
                }

            }

        }
    }
    #endregion
}