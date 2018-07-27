using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMTree.Model.Campaigns
{
    public class MD_BeneficiaryList
    {
        private List<MD_CampaignBeneficiary> _CampaignBeneficiaryList;
        /// <summary>
        /// 活动受益人列表
        /// </summary>
        public List<MD_CampaignBeneficiary> CampaignBeneficiaryList
        {
            get { return _CampaignBeneficiaryList; }
            set { _CampaignBeneficiaryList = value; }
        }
    }
}
