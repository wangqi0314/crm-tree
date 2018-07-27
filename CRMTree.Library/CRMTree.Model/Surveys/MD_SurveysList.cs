using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMTree.Model.Surveys
{
    public class MD_SurveysList
    {
        private IList<MD_Surveys> _Surveys_List;

        public IList<MD_Surveys> Surveys_List
        {
            get { return _Surveys_List; }
            set { _Surveys_List = value; }
        }
    }
}
