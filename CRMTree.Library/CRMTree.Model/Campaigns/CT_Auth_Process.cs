using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMTree.Model.Campaigns
{
    public class CT_Auth_Process
    {
        public int AT_Code { get; set; }
        public string EX_Camp_Category { get; set; }
        public string AT_SType { get; set; }
        public int? AT_IType { get; set; }
        public int? AU_Code { get; set; }
        public string AU_Name { get; set; }
        public int? UG_Code { get; set; }
        public string UG_Name { get; set; }
        public int? AT_Level { get; set; }
        public int? AT_UType { get; set; }
        public int? AT_AD_OM_Code { get; set; }
        public int? AT_CG_Cat { get; set; }
        public int? AT_AType { get; set; }
    }
    public class CT_Camp_Calls
    {
        //public int? CC_UType { get; set; }
        //public int? CC_CG_AD_Code { get; set; }
        public int? CC_Team { get; set; }
        public int? CC_DE_Code { get; set; }
        public int? CC_Percent { get; set; }
    }

}
