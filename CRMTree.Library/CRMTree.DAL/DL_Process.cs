using CRMTree.Model.Campaigns;
using CRMTree.Model.Common;
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
    public class DL_Process
    {
        public int Save_Provess(IList<CT_Auth_Process> _ProList,int Cat,int IType,long AU_Code,int UType,int AD_Code)
        {
            if (_ProList == null || _ProList.Count<=0)
            {
                return (int)_errCode.isNull;
            }
            int _level = 0;
            string _sql = @"delete FROM CT_Auth_Process WHERE AT_UType = "+UType+" AND AT_AD_OM_Code = "+AD_Code+" AND AT_CG_Cat = "+Cat+" AND AT_IType = "+IType+";";
            foreach (CT_Auth_Process _p in _ProList)
            {
                _level++;
                _sql += @"INSERT INTO [CT_Auth_Process]([AT_UType],[AT_AD_OM_Code],[AT_Level],[AT_AType],[AT_AU_UG_Code],[AT_IType],[AT_CG_Cat],[AT_SType],[AT_Created_By],[AT_Update_dt]) VALUES(" + UType + "," + AD_Code + "," + _level + "," + ((_p.AU_Code != null && _p.AU_Code > 0) ? 1 : 2) + "," + ((_p.AU_Code != null && _p.AU_Code > 0) ? _p.AU_Code : _p.UG_Code) + "," + IType + "," + Cat + ",'" + _p.AT_SType + "'," + AU_Code + ",GETDATE());";
            }
            int i = SqlHelper.ExecuteNonQuery(_sql);
            if (i > 0)
            {
                return (int)_errCode.success;
            }
            else
            {
                return (int)_errCode.systomError;
            }
        }
    }
}
