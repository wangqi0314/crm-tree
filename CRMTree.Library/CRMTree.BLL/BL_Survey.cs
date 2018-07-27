using CRMTree.DAL;
using CRMTree.Model.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMTree.BLL
{
    public class BL_Survey
    {
        DL_Survey _dal_Sur = new DL_Survey();
        public DataSet GetSurvey(int CG_Code, int AU_Code, int DE_advisor)
        {
            return _dal_Sur.GetSurvey(CG_Code,AU_Code, DE_advisor);
        }
        public int Save_Survey(int CG_Code, int AU_Code, int DE_Code, string An, string Not)
        {
            return _dal_Sur.Save_Survey(CG_Code, AU_Code, DE_Code, An, Not);
        }
        /// <summary>
        /// 获取问卷活动的类别
        /// </summary>
        /// <returns></returns>
        public DataTable GetSurveyCamCategory(bool Interna)
        {
            return _dal_Sur.GetSurveyCamCategory(Interna);
        }
        /// <summary>
        /// 获取问卷类别内的活动
        /// </summary>
        /// <param name="CG_Type"></param>
        /// <returns></returns>
        public DataTable GetSurveyCategoryCam(int CG_Type)
        {
            return _dal_Sur.GetSurveyCategoryCam(CG_Type);
        }
        public int Save_Survey_data(int CG_Code, dynamic data)
        {
            if (data == null)
            {
                return (int)_errCode.isObjectNull;
            }
            for (int i = 0; i < data.Count; i++)
            {
                if (data[i].Delete == null || (bool)data[i].Delete == false)
                {
                    int _err = _dal_Sur.Save_Survey_data(CG_Code, data[i], i);
                }
            }
            return (int)_errCode.success;
        }
        public int Delete_Survey(int SQ_Code, int SF_Code)
        {
            return _dal_Sur.Delete_Survey(SQ_Code, SF_Code);
        }

        public IList<DataTable> GetSurveyAnswer(int HD_Code)
        {
            return _dal_Sur.GetSurveyAnswer(HD_Code);
        }
    }
}
