using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMTree.Model.Campaigns
{
    public class CT_Team_GroupList
    {
        private IList<CT_Team_Group> _Team_GroupList;
        public IList<CT_Team_Group> Team_GroupList
        {
            get { return _Team_GroupList; }
            set { _Team_GroupList = value; }
        }
    }
}
