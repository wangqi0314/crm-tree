using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMTree.Model.Event
{
    public class MD_EventToolsList
    {
        private IList<CT_Events_Tools> _EventToolsLIst;

        public IList<CT_Events_Tools> EventToolsLIst
        {
            get { return _EventToolsLIst; }
            set { _EventToolsLIst = value; }
        }
    }
}
