using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMTree.Model.MyCar
{
   public class MD_CarModeList
   {
       private IList<CT_Car_Model> _Car_Model_list;
       public IList<CT_Car_Model> Car_Model_List
       {
           get { return _Car_Model_list; }
           set { _Car_Model_list = value; }
       }
    }
}
