
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMTree.Model.Adviser
{
    public class MD_Adviser
    {
        /// <summary>
        /// 顾问的员工ID号
        /// </summary>
        public int DE_Code { get; set; }
        /// <summary>
        /// 顾问的头像地址
        /// </summary>
        public string DE_Picture_FN { get; set; }
        /// <summary>
        /// 顾问的系统登录ID；
        /// </summary>
        public int AU_Code { get; set; }
        /// <summary>
        /// 顾问的姓名
        /// </summary>
        public string AU_Name { get; set; }
        /// <summary>
        /// 顾问的服务经销商ID
        /// </summary>

        public int AD_Code { get; set; }
        /// <summary>
        /// 顾问的移动电话
        /// </summary>
        public string Mobil { get; set; }
        /// <summary>
        /// 顾问的办公电话
        /// </summary>
        public string Office { get; set; }
        /// <summary>
        /// 顾问的经销商电话
        /// </summary>
        public string DealerShip { get; set; }
        /// <summary>
        /// 顾问的服务地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 顾问的EMail；
        /// </summary>
        public string Email { get; set; }

        public string AD_Name_CN { get; set; }

        public string AD_Name_EN { get; set; }
    }
    public class MD_AdviseList
    {
        /// <summary>
        /// 顾问对象列表
        /// </summary>
        public List<MD_Adviser> Adviser_List { get; set; }
    }

    public class DealerEmplList
    {
        public IList<CT_Auto_Dealers> Dealer { get; set; }
        public IList<CT_Dealer_Empl> Empl { get; set; }
    }
}
