using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMTree.Model.MyCar
{
    public class MD_DealersList
    {
        private IList<CT_Auto_Dealers> _User_Dealers_List;
        /// <summary>
        /// MyCar_Inventory_List
        /// </summary>
        public IList<CT_Auto_Dealers> User_Dealers_List
        {
            get { return _User_Dealers_List; }
            set { _User_Dealers_List = value; }
        }
    }
}
