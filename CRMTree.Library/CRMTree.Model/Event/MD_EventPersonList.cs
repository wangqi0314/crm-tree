using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMTree.Model.Event
{
    public class MD_EventPersonList
    {
        private IList<CT_Events_Person> _EventPersonList;

        public IList<CT_Events_Person> EventPersonList
        {
            get { return _EventPersonList; }
            set { _EventPersonList = value; }
        }
    }
}
