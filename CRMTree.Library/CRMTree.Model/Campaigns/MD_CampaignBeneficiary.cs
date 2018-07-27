using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMTree.Model.Campaigns
{
    public class MD_CampaignBeneficiary
    {
        /// <summary>
        /// 获取的Code
        /// </summary>
        private int _CG_Code;

        public int CG_Code
        {
            get { return _CG_Code; }
            set { _CG_Code = value; }
        }
        /// <summary>
        /// 活动的标题
        /// </summary>
        private string _CG_Title;

        public string CG_Title
        {
            get { return _CG_Title; }
            set { _CG_Title = value; }
        }
        private int _CG_RP_Code;
        /// <summary>
        /// 报表的Code
        /// </summary>

        public int CG_RP_Code
        {
            get { return _CG_RP_Code; }
            set { _CG_RP_Code = value; }
        }
        /// <summary>
        /// 活动受益人Code
        /// </summary>
        private int _AU_Code;

        public int AU_Code
        {
            get { return _AU_Code; }
            set { _AU_Code = value; }
        }
        /// <summary>
        /// 活动受益人姓名
        /// </summary>
        private string _AU_Name;

        public string AU_Name
        {
            get { return _AU_Name; }
            set { _AU_Name = value; }
        }
        /// <summary>
        /// 受益人电话Code
        /// </summary>
        private int _Pl_Code;

        public int PL_Code
        {
            get { return _Pl_Code; }
            set { _Pl_Code = value; }
        }
        /// <summary>
        /// 受益人电话号码
        /// </summary>
        private string _PL_Number;

        public string PL_Number
        {
            get { return _PL_Number; }
            set { _PL_Number = value; }
        }
        private int _ML_Code;
        /// <summary>
        /// 受益人发送短信的手机号码Code
        /// </summary>
        public int ML_Code
        {
            get { return _ML_Code; }
            set { _ML_Code = value; }
        }
        /// <summary>
        /// 受益人发送短信的手机号码
        /// </summary>
        private string _ML_Handle;

        public string ML_Handle
        {
            get { return _ML_Handle; }
            set { _ML_Handle = value; }
        }
        private int _EL_Code;
        /// <summary>
        /// 受益人邮件Code
        /// </summary>
        public int EL_Code
        {
            get { return _EL_Code; }
            set { _EL_Code = value; }
        }
        private string _EL_Address;
        /// <summary>
        /// 受益人邮件地址
        /// </summary>
        public string EL_Address
        {
            get { return _EL_Address; }
            set { _EL_Address = value; }
        }
        private string _CG_Filename;

        public string CG_Filename
        {
            get { return _CG_Filename; }
            set { _CG_Filename = value; }
        }
    }
}
