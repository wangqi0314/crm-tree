using CRMTree.DAL;
using CRMTree.Model.Adviser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMTree.BLL
{
    public class BL_Advisers
    {
        DL_Advisers a = new DL_Advisers();
        /// <summary>
        /// 获取用户汽车信息推荐的顾问
        /// </summary>
        /// <param name="AU_Code">用户ID</param>
        /// <param name="CI_Code">汽车ID</param>
        /// <returns></returns>
        public Model.Adviser.MD_AdviseList getRecommendAdviser(long AU_Code, int CI_Code)
        {
            return a.getRecommendAdviser(AU_Code, CI_Code);
        }
        /// <summary>
        /// 获取顾问的联系信息
        /// </summary>
        /// <param name="AU_Code">顾问的用户ID</param>
        /// <returns></returns>
        public Model.Adviser.MD_Adviser getAdviserMessage(int AU_Code)
        {
            return a.getAdviserMessage(AU_Code);
        }
        public Model.CT_Dealer_Empl getDefaultRecomAdviser(int CI_Code, int AD_Code)
        {
            return a.getDefaultRecomAdviser(CI_Code, AD_Code);
        }

        #region 微信端联系顾问推荐

        public DealerEmplList GetDealerEmplList()
        {
            return a.GetDealerEmplList();
        }
        #endregion
    }
}
