using CRMTree.Model.Surveys;
using ShInfoTech.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMTree.DAL
{
    public class DL_Surveys
    {
        public MD_SurveysList getSurveys(long Customer_code)
        {
            string sql = @"Select CG_Title,
                                  (Select AD_Name_EN from CT_Auto_Dealers where CG_AD_OM_Code=AD_Code)as Dealer_Name,
                                  * from CT_Comm_History
                            join CT_Campaigns on CH_CG_Code=CG_Code
                            where cg_Utype=3  and CH_Status != 4 and ch_utype=44 and CH_AU_Code=@Customer_code";
            SqlParameter[] parameters = { new SqlParameter("@Customer_code", SqlDbType.BigInt) };
            parameters[0].Value = Customer_code;
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text, sql, parameters);
            MD_SurveysList o = new MD_SurveysList();
            o.Surveys_List = DataHelper.ConvertToList<MD_Surveys>(ds);            
            return o;
        }
    }
}
