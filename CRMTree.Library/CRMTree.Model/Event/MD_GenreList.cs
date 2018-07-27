using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMTree.Model.Event
{
    public class MD_GenreList
    {
        private IList<CT_Event_Genre> _GenreList;

        public IList<CT_Event_Genre> GenreList
        {
            get { return _GenreList; }
            set { _GenreList = value; }
        }
    }
}
