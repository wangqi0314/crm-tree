using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMTree.Model.Event
{
    public class MD_SuccMatrixList
    {
        private IList<CT_Succ_Matrix> _SuccMatrixList;

        public IList<CT_Succ_Matrix> SuccMatrixList
        {
            get { return _SuccMatrixList; }
            set { _SuccMatrixList = value; }
        }
    }
}
