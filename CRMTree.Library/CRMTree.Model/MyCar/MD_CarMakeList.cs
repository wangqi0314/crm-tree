using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMTree.Model.MyCar
{
    public class MD_CarMakeList
    {
        private IList<CT_Make> _Car_Make_list;
        public IList<CT_Make> Car_Make_List
        {
            get { return _Car_Make_list; }
            set { _Car_Make_list = value; }
        }
    }
}
