using CRMTree.DAL;
using CRMTree.Model.Campaigns;
using CRMTree.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMTree.BLL
{
    public class BL_Process
    {
        DL_Process _Pre = new DL_Process();
        public int Save_Provess(IList<CT_Auth_Process> _ProList, int Cat, int IType, long AU_Code, int UType, int AD_Code)
        {
            return _Pre.Save_Provess(_ProList, Cat, IType, AU_Code, UType, AD_Code);
        }
    }
}
