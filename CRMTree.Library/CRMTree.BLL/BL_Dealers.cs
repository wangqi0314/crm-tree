using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using CRMTree.DAL;
using CRMTree.Model.MyCar;
namespace CRMTree.BLL
{
    public partial class BL_Dealers
    {
        private readonly DL_Dealers user = new DL_Dealers();

        public MD_DealersList DealerList()
        {
            return user.DealerList();
        }
    }
}