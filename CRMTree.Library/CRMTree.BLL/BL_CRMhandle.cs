using CRMTree.DAL;
using CRMTree.Model.Common;
using Newtonsoft.Json;
using ShInfoTech.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMTree.BLL
{
    public class BL_CRMhandle
    {
        readonly private DL_CRMhandle _crmHand = new DL_CRMhandle();
        public string GetWords(bool Interna, int p_id)
        {
            return _crmHand.GetWords(Interna, p_id);
        }
        /// <summary>
        /// 获取联系人信息
        /// </summary>
        /// <param name="AU_Code"></param>
        /// <param name="isEn"></param>
        /// <returns></returns>
        public string Contact_info(long AU_Code, bool? isEn)
        {
            dynamic o = new ExpandoObject();
            o.AllCarInfo = _crmHand.Get_userAllcarName(AU_Code, isEn);
            return JsonConvert.SerializeObject(o);
        }
        /// <summary>
        /// 检测，VIN 码
        /// </summary>
        /// <param name="vin"></param>
        /// <returns></returns>
        public DataTable GetCheckVIN(string vin)
        {
            DataSet _dataSet = _crmHand.check_get_VIN(vin);
            if (_dataSet == null || _dataSet.Tables.Count <= 0)
            {
                return null;
            }
            if (_dataSet.Tables[0] == null || _dataSet.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            return _dataSet.Tables[0];
        }

        /// <summary>
        /// 检测文件的错误行数
        /// </summary>
        /// <param name="Path"></param>
        /// <returns></returns>
        public int check_errVINs(string Path)
        {
            NPOI_Read_Write _npoi_RW = new NPOI_Read_Write();
            string _vins = _npoi_RW.ReadeExcelFile(Path, 1);
            if (string.IsNullOrEmpty(_vins))
            {
                return (int)_errCode.isNull;
            }
            DataTable _data = GetCheckVIN(_vins);
            if (_data == null || _data.Rows.Count <= 0)
            {
                return (int)_errCode.isObjectNull;
            }
            int _errRow = 0;
            for (int i = 0; i < _data.Rows.Count; i++)
            {
                if (_data.Rows[i][1].ToString() == "否")
                {
                    _errRow++;
                }
            }
            return _errRow;
        }
        /// <summary>
        /// 检测VIN ,同时写入文件
        /// </summary>
        /// <param name="Path"></param>
        /// <returns></returns>
        public int check_VINS(string Path)
        {
            NPOI_Read_Write _npoi_RW = new NPOI_Read_Write();
            string _vins = _npoi_RW.ReadeExcelFile(Path, 1);
            DataTable _data = GetCheckVIN(_vins);
            _npoi_RW.WriteExcelFile(Path, _data);
            return (int)_errCode.success;
        }

        /// <summary>
        /// 获取Campaign检测通过的Vin
        /// </summary>
        /// <param name="vin"></param>
        /// <returns></returns>
        public DataTable Get_CamVIN(string vin)
        {
            DataSet _dataSet = _crmHand.check_get_VIN(vin);
            if (_dataSet == null || _dataSet.Tables.Count <= 0)
            {
                return null;
            }
            if (_dataSet.Tables[1] == null || _dataSet.Tables[1].Rows.Count <= 0)
            {
                return null;
            }
            return _dataSet.Tables[1];
        }
        /// <summary>
        /// 获取Campaign检测通过的Vin 夏男调用
        /// </summary>
        /// <param name="Path"></param>
        /// <returns></returns>
        public DataTable Get_check_CamVIN(string Path)
        {
            NPOI_Read_Write _npoi_RW = new NPOI_Read_Write();
            string _vins = _npoi_RW.ReadeExcelFile(Path, 1);
            DataTable _data = Get_CamVIN(_vins);
            return _data;
        }

        public string GetMIName(int MI_Code, int FL_Code, bool Interna)
        {
            return _crmHand.GetMIName(MI_Code, FL_Code, Interna);
        }

        #region KPI

        /// <summary>
        /// 获取相关部门设置的KPI 列表组
        /// </summary>
        /// <param name="ad_code"></param>
        /// <returns></returns>
        public string getDepartmentKPIGroup(int ad_code)
        {
            return _crmHand.getDepartmentKPIGroup(ad_code);
        }
        /// <summary>
        /// 获取相关部门设置的KPI
        /// </summary>
        /// <param name="ad_code"></param>
        /// <param name="Interna"></param>
        /// <returns></returns>
        public string getDepartmentKPI(int ad_code, bool Interna)
        {
            return _crmHand.getDepartmentKPI(ad_code, Interna);
        }
        /// <summary>
        /// 获取部门列表
        /// </summary>
        /// <param name="ad_code"></param>
        /// <param name="Interna"></param>
        /// <returns></returns>
        public string getDepartment(int ad_code, bool Interna)
        {
            return _crmHand.getDepartment(ad_code, Interna);
        }
        /// <summary>
        /// 获取KPI
        /// </summary>
        /// <param name="Interna"></param>
        /// <returns></returns>
        public string getKPI(bool Interna)
        {
            return _crmHand.getKPI(Interna);
        }

        #region KPI员工 相关
        /// <summary>
        /// 用户获取部门下员工KPI 列表分组标示（用户帮助前端UI好分组数据结构）
        /// </summary>
        /// <param name="ad_code"></param>
        /// <param name="pn_code"></param>
        /// <param name="pt_code"></param>
        /// <returns></returns>
        public string getDepartmentKPIUserGroup(int ad_code, int pn_code, int pt_code)
        {
            return _crmHand.getDepartmentKPIUserGroup(ad_code, pn_code, pt_code);
        }

        /// <summary>
        /// 根据部门和KPI 获取员工的KPI信息
        /// </summary>
        /// <param name="ad_code"></param>
        /// <param name="pn_code"></param>
        /// <param name="pt_code"></param>
        /// <returns></returns>
        public string getDepartmentKPIUser(int ad_code, int pn_code, int pt_code)
        {
            return _crmHand.getDepartmentKPIUser(ad_code, pn_code, pt_code);
        }

        /// <summary>
        /// 获取部门员工
        /// </summary>
        /// <param name="ad_code"></param>
        /// <param name="pn_code"></param>
        /// <returns></returns>
        public string getDepartmentUser(int ad_code, int pn_code)
        {
            return _crmHand.getDepartmentUser(ad_code, pn_code);
        }
        #endregion
        #endregion

    }
}
