using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CRMTree.Public;
using Shinfotech.Tools;
using ShInfoTech.Common;
using System.Web.Services;

public partial class manage_profile_List_Profile : BasePage
{
    static long userCode = -1;
    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        if (!IsPostBack)
        {
            this.top1.UserID = UserSession.User.AU_Code;
            userCode = UserSession.User.AU_Code;
        }
    }
    [WebMethod]
    public static int PasswordChang(string OPW, string NPW)
    {
        string strPwdNew = ShInfoTech.Common.Security.Md5(NPW);
        string strPwdOld = ShInfoTech.Common.Security.Md5(OPW);
        SqlParameter[] parameters = {
                    new SqlParameter("@results", SqlDbType.Int,4),
                    new SqlParameter("@AU_Code",SqlDbType.BigInt,4),
					new SqlParameter("@AU_Password_Old", SqlDbType.NVarChar,50),
					new SqlParameter("@AU_Password_New", SqlDbType.NVarChar,50)
					};
        parameters[0].Direction = ParameterDirection.Output;
        parameters[1].Value = userCode;
        parameters[2].Value = strPwdOld;
        parameters[3].Value = strPwdNew;
        int i = SqlHelper.ExecuteNonQuery(Tools.GetConnString(), CommandType.StoredProcedure, "CT_All_Users_Password", parameters);
        int n=Convert.ToInt32(parameters[0].Value);
        return n;
    }
}