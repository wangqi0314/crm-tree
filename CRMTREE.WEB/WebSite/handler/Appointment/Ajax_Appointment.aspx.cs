using CRMTree.Public;
using Shinfotech.Tools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class handler_Appointment_Ajax_Appointment : BasePage
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
                    //Add_Customers_Targeted();//Customers Targeted 下拉框自定义添加
                    break;
                case "list_car":
                    List();//car 列表
                    break;
                case "add_car":
                    //Add_car();//添加 car
                    break;
                case "up_car":
                    //Update_car();//修改 car
                    break;
                case "delete_car":
                    //carDelete();//删除 car
                    break;
                case "del_file":
                    //Delete_File();//删除 upload File 物理文件
                    break;
            }
        }
        else
            Response.Write("-1");//action 参数不正确，没权限
    }
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
            strSortField = "AP_Time";
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
        StringBuilder sbWhere = new StringBuilder(" where AP_AU_Code=");
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
        if (strSortField.Equals("AP_Code"))
        {
            sbList.Append("class=\"w40 taxisCurrent\" onclick=\"changeOrder(this,'AP_Code')\">ID<span class=\"");
            sbList.Append(orderClass);
            sbList.Append("\" title=\"Sort by the column \"></span></th>");
        }
        else
        {
            sbList.Append("class=\"w40\" onclick=\"changeOrder(this,'AP_Code')\">ID<span class=\"taxis\" title=\"Sort by the column \"></span></th>");
        }
        sbList.Append("<th>CI_Code</th>");
        sbList.Append("<th>CS_Style_EN</th>");
        sbList.Append("<th>AD_Name_EN</th>");
        sbList.Append("<th>DE_ID</th>");
        sbList.Append("<th style=\"cursor:pointer;\" ");
        if (strSortField.Equals("AP_Time"))
        {
            sbList.Append("class=\"taxisCurrent\" onclick=\"changeOrder(this,'AP_Time')\">AP_Time<span class=\"");
            sbList.Append(orderClass);
            sbList.Append("\" title=\"Sort by the column \"></span></th>");
        }
        else
        {
            sbList.Append(" onclick=\"changeOrder(this,'AP_Time')\">AP_Time<span class=\"taxis\" title=\"Sort by the column \"></span></th>");
        }
        sbList.Append("<th class=\"w100\">Operate</th>");
        //初始化变量
        int intRowsCount = 0;//总行数
        int intPageCount = 0;//总页数  
        int intCurrentPage = RequestClass.GetInt("page", 1);//当前页
        int intPageSize = RequestClass.GetInt("pagesize", 15); ;//每页条数

        //得到数据表
        //var dt = DbHelperSQL.ProcedurePagerByProc("[App_getList]", "AP_Code", "*", strSortField, strSortRule, sbWhere.ToString(), intCurrentPage, intPageSize, out intPageCount, out intRowsCount).Tables[0];
        var dt = new DataTable();
        if (null != dt && dt.Rows.Count > 0)
        {
            //生成分页html
            var strPager = new StringBuilder();
            if (1 < intPageCount) strPager = Pager.JavascriptPagination(true,intCurrentPage, intPageCount, intRowsCount);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                var intID = dt.Rows[i][0];
                var intRows = i + 1;
                sbList.Append("<tr><td>");
                sbList.Append(((i + 1) + (intCurrentPage - 1) * intPageSize));
                sbList.Append("</td><td>");
                sbList.Append(intID);
                sbList.Append("</td><td>");
                sbList.Append(dt.Rows[i][1]);
                sbList.Append("</td><td>");
                sbList.Append(dt.Rows[i][2]);
                sbList.Append("</td><td>");
                sbList.Append(dt.Rows[i][3]);
                sbList.Append("</td><td>");
                sbList.Append(dt.Rows[i][4]);
                sbList.Append("</td><td>");
                sbList.Append(Convert.ToDateTime(dt.Rows[i][5].ToString()).ToString("MM/dd/yyyy HH:mm"));
                sbList.Append("</td><td>");
                sbList.Append("<a href=\"/manage/Appointment/Appointment.aspx?id=");
                sbList.Append(intID);
                sbList.Append("\"><i class=\"btnModify\" title=\"edit\"></i></a>");
                sbList.Append("<a href=\"javascript:;\"><i class=\"btnDelete\" title=\"Delete\" onclick=\"Delete(");
                sbList.Append(intID);
                sbList.Append(")\"></i></a>");
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
    [WebMethod]
    public static int DeleteApp(string AP_Code)
    {
        CRMTree.BLL.BL_Appt_Service App = new CRMTree.BLL.BL_Appt_Service();
        int i = App.DeleteAppointmens(int.Parse(AP_Code));
        return i;
    }
}