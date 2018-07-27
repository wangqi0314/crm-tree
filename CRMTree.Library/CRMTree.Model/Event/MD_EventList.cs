using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMTree.Model.Event
{
    public class MD_EventList
    {
        private IList<CT_Events> _EventList;

        public IList<CT_Events> EventList
        {
            get { return _EventList; }
            set { _EventList = value; }
        }
    }
}
