using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMTree.Model.Reports
{
    public  class MD_Paramterslist
    {
        private List<CT_Paramters_list> _CT_Paramters_list;

        public List<CT_Paramters_list> CT_Paramters_list
        {
            get { return _CT_Paramters_list; }
            set { _CT_Paramters_list = value; }
        }

    }
}
