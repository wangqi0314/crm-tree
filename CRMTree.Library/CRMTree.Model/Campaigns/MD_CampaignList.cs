using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMTree.Model.Campaigns
{
    public class MD_CampaignList
    {
        private IList<CT_Campaigns> _CampaignList;

        public IList<CT_Campaigns> CampaignList
        {
            get { return _CampaignList; }
            set { _CampaignList = value; }
        } 
    }
}
