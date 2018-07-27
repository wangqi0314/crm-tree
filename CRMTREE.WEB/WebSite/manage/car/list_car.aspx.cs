using System;
using CRMTree.Public;
public partial class manage_car_list_car : BasePage
{
    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        if (!IsPostBack)
        {
            this.top1.UserID = UserSession.User.AU_Code;
        }
    }
}