using System;
using System.Collections.Generic;
using System.Web;

namespace CRMTree.Model.User
{
    /// <summary>
    /// 用户实体类
    /// </summary>
    public class MD_UserEntity : Base_Model
    {
        private CT_Dealer_Empl _DealerEmpl;
        /// <summary>
        /// 登陆的员工信息
        /// </summary>
        public CT_Dealer_Empl DealerEmpl
        {
            get { return _DealerEmpl; }
            set { _DealerEmpl = value; }
        }
        private CT_Auto_Dealers _Dealer;
        /// <summary>
        /// 员工所属的经销商
        /// </summary>
        public CT_Auto_Dealers Dealer
        {
            get { return _Dealer; }
            set { _Dealer = value; }
        }
        private CT_Dealer_Groups _DealerGroup;
        /// <summary>
        /// 用户单位对应的集团
        /// </summary>
        public CT_Dealer_Groups DealerGroup
        {
            get { return _DealerGroup; }
            set { _DealerGroup = value; }
        }
        private CT_OEM _OEM;
        /// <summary>
        /// 用户单位对应的厂商
        /// </summary>
        public CT_OEM OEM
        {
            get { return _OEM; }
            set { _OEM = value; }
        }
    }
}