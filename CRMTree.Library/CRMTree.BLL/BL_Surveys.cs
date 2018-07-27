using CRMTree.DAL;
using CRMTree.Model.Surveys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMTree.BLL
{
    public class BL_Surveys
    {
        DL_Surveys Sur = new DL_Surveys();
        public MD_SurveysList getSurveys(long Customer_code)
        {
            return Sur.getSurveys(Customer_code);
        }
    }
}
