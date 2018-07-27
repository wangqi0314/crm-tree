using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMTree.Model.Appointmens
{
    public class MD_AdviserList 
    {
        private IList<CT_Dealer_Empl> _Adviser_List;
        /// <summary>
        /// MyCar_Inventory_List
        /// </summary>
        public IList<CT_Dealer_Empl> Adviser_List
        {
            get { return _Adviser_List; }
            set { _Adviser_List = value; }
        }
    }
    public class MD_AppointmensList 
    {
        private List<MD_Appointmens> _Appointmens_List;
        /// <summary>
        /// MyCar_Inventory_List
        /// </summary>
        public List<MD_Appointmens> Appointmens_List
        {
            get { return _Appointmens_List; }
            set { _Appointmens_List = value; }
        }
    }
    public class MD_DealerList 
    {
        private IList<CT_Auto_Dealers> _Dealers_List;
        /// <summary>
        /// MyCar_Inventory_List
        /// </summary>
        public IList<CT_Auto_Dealers> Dealers_List
        {
            get { return _Dealers_List; }
            set { _Dealers_List = value; }
        }
    }
    /// <summary>
    /// 服务类别实体类
    /// </summary>
    public class MD_ServCategoryList 
    {
        private IList<CT_Serv_Category> _Serv_Category_List;
        /// <summary>
        /// MyCar_Inventory_List
        /// </summary>
        public IList<CT_Serv_Category> Serv_Category_List
        {
            get { return _Serv_Category_List; }
            set { _Serv_Category_List = value; }
        }
    }
    public class MD_ServiceTypesList 
    {
        private IList<CT_Service_Types> _Service_Types_List;
        /// <summary>
        /// CT_Service_Types
        /// </summary>
        public IList<CT_Service_Types> Service_Types_List
        {
            get { return _Service_Types_List; }
            set { _Service_Types_List = value; }
        }
    }
    public class MD_MaintenancePackList 
    {
        private IList<CT_Maintenance_Pack> _Maintenance_Pack_List;
        /// <summary>
        /// CT_Service_Types
        /// </summary>
        public IList<CT_Maintenance_Pack> Maintenance_Pack_List
        {
            get { return _Maintenance_Pack_List; }
            set { _Maintenance_Pack_List = value; }
        }
    }
    public class MD_Appt_service
    {
    }
}
