using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMTree.Model.Appointmens
{
    public class MD_Appointmens:Base_Model
    {
        private int _ap_code;
        /// <summary>
        /// AP_Code
        /// </summary>		
        public int AP_Code
        {
            get { return _ap_code; }
            set { _ap_code = value; }
        }
        private long _ap_au_code;
        /// <summary>
        /// AP_AU_Code
        /// </summary>		
        public long AP_AU_Code
        {
            get { return _ap_au_code; }
            set { _ap_au_code = value; }
        }
        private int _ap_ci_code;
        /// <summary>
        /// AP_CI_Code
        /// </summary>		
        public int AP_CI_Code
        {
            get { return _ap_ci_code; }
            set { _ap_ci_code = value; }
        }
        private int _ap_ad_code;
        /// <summary>
        /// AP_AD_Code
        /// </summary>		
        public int AP_AD_Code
        {
            get { return _ap_ad_code; }
            set { _ap_ad_code = value; }
        }
        private DateTime _ap_time;
        /// <summary>
        /// AP_Time
        /// </summary>		
        public DateTime AP_Time
        {
            get { return _ap_time; }
            set { _ap_time = value; }
        }
        private long _ap_sa_selected;
        /// <summary>
        /// AP_SA_Selected
        /// </summary>		
        public long AP_SA_Selected
        {
            get { return _ap_sa_selected; }
            set { _ap_sa_selected = value; }
        }
        private int _ap_serv_req;
        /// <summary>
        /// AP_Serv_Req
        /// </summary>		
        public int AP_Serv_Req
        {
            get { return _ap_serv_req; }
            set { _ap_serv_req = value; }
        }
        private int _ap_method;
        /// <summary>
        /// AP_Method
        /// </summary>		
        public int AP_Method
        {
            get { return _ap_method; }
            set { _ap_method = value; }
        }
        private int _ap_transportation;
        /// <summary>
        /// AP_Transportation
        /// </summary>		
        public int AP_Transportation
        {
            get { return _ap_transportation; }
            set { _ap_transportation = value; }
        }
        private DateTime _ap_arrival;
        /// <summary>
        /// Ap_Arrival
        /// </summary>		
        public DateTime Ap_Arrival
        {
            get { return _ap_arrival; }
            set { _ap_arrival = value; }
        }
        private long _ap_created_by;
        /// <summary>
        /// AP_Created_by
        /// </summary>		
        public long AP_Created_by
        {
            get { return _ap_created_by; }
            set { _ap_created_by = value; }
        }
        private long _ap_updated_by;
        /// <summary>
        /// AP_Updated_by
        /// </summary>		
        public long AP_Updated_by
        {
            get { return _ap_updated_by; }
            set { _ap_updated_by = value; }
        }
        private DateTime _ap_update_dt;
        /// <summary>
        /// AP_Update_Dt
        /// </summary>		
        public DateTime AP_Update_Dt
        {
            get { return _ap_update_dt; }
            set { _ap_update_dt = value; }
        }
        private int _CI_Code;

        public int CI_Code
        {
            get { return _CI_Code; }
            set { _CI_Code = value; }
        }
        private string _CS_Style_EN;
        /// <summary>
        /// CS_Style
        /// </summary>		
        public string CS_Style_EN
        {
            get { return _CS_Style_EN; }
            set { _CS_Style_EN = value; }
        }
        private string _AD_Name_EN;
        /// <summary>
        /// AD_Name
        /// </summary>		
        public string AD_Name_EN
        {
            get { return _AD_Name_EN; }
            set { _AD_Name_EN = value; }
        } 
        private string _DE_ID;
        /// <summary>
        /// SD_Bays
        /// </summary>		
        public string DE_ID
        {
            get { return _DE_ID; }
            set { _DE_ID = value; }
        } 
    }
}
