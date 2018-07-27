using CRMTree.DAL;
using CRMTree.Model;
using CRMTree.Model.ServerHistory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMTree.BLL
{
    public class BL_ServerHistory
    {
        private readonly DL_ServerHistory _d_Server = new DL_ServerHistory();
        public MD_ServerHistory getMyServiceHis(CT_History_Service HistoryService)
        {
            return _d_Server.getMyServiceHis(HistoryService);
        }
        public CT_History_Service getMyServiceHisInfo(CT_History_Service HistoryService)
        {
            return _d_Server.getMyServiceHisInfo(HistoryService);
        }
        /// <summary>
        /// 获取进店历史服务
        /// 具体服务列表可以调用方法：GetService_Info
        /// </summary>
        /// <param name="AU_Code"></param>
        /// <param name="SearchDate"></param>
        /// <returns></returns>
        public string GetHistory_Service(long AU_Code, DateTime? SearchDate)
        {
            return _d_Server.GetHistory_Service(AU_Code, SearchDate);
        }
        /// <summary>
        /// 获取单次服务的具体服务列表
        /// </summary>
        /// <param name="HS_Code"></param>
        /// <returns></returns>
        public string GetService_Info(int HS_Code)
        {
            return _d_Server.GetService_Info(HS_Code);
        }
    }
}
