using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMTree.Model
{
    public class Base_Model
    {
        public CT_All_Users User { get; set; }
    }
    public class MD_TabLinks 
    {
        public IList<CT_Tab_Links> CT_Tab_LinkList { get; set; }
    }
    public class TranList
    {
        public IList<CT_Transportation> Tran { get; set; }
    }
    public class Fields_list
    {
        public IList<CT_Fields_list> List { get; set; }
    }

}
