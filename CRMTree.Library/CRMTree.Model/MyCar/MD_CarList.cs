using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMTree.Model.MyCar
{
    public class MD_CarList
    {
        private IList<CT_Car_Inventory> _Car_Inventory_List;
        /// <summary>
        /// MyCar_Inventory_List
        /// </summary>
        public IList<CT_Car_Inventory> Car_Inventory_List
        {
            get { return _Car_Inventory_List; }
            set { _Car_Inventory_List = value; }
        }
    }
}
