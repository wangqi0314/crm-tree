using CRMTree.BLL;
using CRMTree.Model.MyCar;
using CRMTree.Model.User;
using CRMTree.Public;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class manage_AddUser_edit_User : BasePage
{
    static long userCode = -1;
    static bool Intern = true;
    public string strFilename = String.Empty;
    protected override void OnLoad(EventArgs e)
    {

        base.OnLoad(e);
        if (!IsPostBack)
        {
            Intern = Interna;
            userCode = UserSession.User.AU_Code;
            this.top1.UserID = UserSession.User.AU_Code;
            int UG_Code = UserSession.DealerEmpl.DE_AD_OM_Code;
        }


    }
    [WebMethod]
    public static Object DealerList()
    {
        BL_Dealers user = new CRMTree.BLL.BL_Dealers();
        MD_DealersList myCarModel = user.DealerList();
        return myCarModel;
    }
    [WebMethod]
    public static Object getGroupsList(int UG_UType)
    {
        BL_UserEntity groups = new CRMTree.BLL.BL_UserEntity();
        MD_GroupsList myCarModel = groups.getGroupsList(UG_UType);
        return myCarModel;
    }
}