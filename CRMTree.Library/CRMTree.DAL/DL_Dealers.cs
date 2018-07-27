using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using ShInfoTech.Common;
using CRMTree.Model.MyCar;
using CRMTree.Model;
namespace CRMTree.DAL
{
    public partial class DL_Dealers
    {
        public MD_DealersList DealerList()
        {
            string sql = "select AD_Code,AD_Name_EN from CT_Auto_Dealers";

            MD_DealersList List = new MD_DealersList();
            List.User_Dealers_List = DataHelper.ConvertToList<CT_Auto_Dealers>(sql);
            return List;
        }
    }
}

