using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMTree.Model.MyCar
{
    public class MD_CarTypeList
    {
        private CT_All_Users _User;
        /// <summary>
        /// MyCar_User
        /// </summary>
        public CT_All_Users User
        {
            get { return _User; }
            set { _User = value; }
        }
        private List<CT_Car_Type> _Car_Type_List;
        /// <summary>
        /// MyCar_Inventory_List
        /// </summary>
        public List<CT_Car_Type> Car_Type_List
        {
            get { return _Car_Type_List; }
            set { _Car_Type_List = value; }
        }
    }
}
