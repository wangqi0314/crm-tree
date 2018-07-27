using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMTree.Model.User
{
   public class MD_GroupsList
    {
        private IList<CT_User_Groups> _Groups_List;
        /// <summary>
        /// MyCar_Inventory_List
        /// </summary>
        public IList<CT_User_Groups> User_Groups_List
        {
            get { return _Groups_List; }
            set { _Groups_List = value; }
        }
    }
}
