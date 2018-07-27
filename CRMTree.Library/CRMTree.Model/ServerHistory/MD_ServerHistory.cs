using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMTree.Model.ServerHistory
{
    public class MD_ServerHistory 
    {
        private List<CT_History_Service> _History_Service;
        /// <summary>
        /// MyCar_Inventory_List
        /// </summary>
        public List<CT_History_Service> History_Service
        {
            get { return _History_Service; }
            set { _History_Service = value; }
        }
    }
}
