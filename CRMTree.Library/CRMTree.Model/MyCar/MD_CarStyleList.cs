using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMTree.Model.MyCar
{
    public class MD_CarStyleList 
    {
        private IList<CT_Car_Style> _Car_Style_list;
        public IList<CT_Car_Style> Car_Style_List
        {
            get { return _Car_Style_list; }
            set { _Car_Style_list = value; }
        }
    }
}
