using CRMTree.Model;
using CRMTree.Model.Common;
using CRMTree.Model.Reports;
using CRMTree.Model.User;
using Newtonsoft.Json;
using ShInfoTech.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace CRMTree.BLL
{
    public class BL_Export
    {
        BL_Reports RP = new BL_Reports();
        CT_Reports _c = null;
        string _sheetTitle = "Data";
        int _PR = -1;
        public BL_Export()
        {
        }
        public BL_Export(int Pl_Code, int PR)
        {
            _c = RP.GetReprot_Chat(ChartType.List, Pl_Code);
            this._PR = PR;
        }
        public void Expore_Ex(int Pl_Code, int PR, out string fileName)
        {
            fileName = SetDownFileName();
            NPOI_Common Excel = new NPOI_Common();
            //获取Title
            Excel.Ex_Titel = getEx_Title(Pl_Code);
            Excel.Ex_FieldName = getFieldName(Pl_Code);
            Excel.Ex_Data = getEx_Data(Pl_Code, PR);

            Excel.Create_Ex(_sheetTitle);
            Excel.Seave_Ex(fileName);

        }
        /// <summary>
        /// 设置文档下载的文件名
        /// </summary>
        /// <returns></returns>
        private string SetDownFileName()
        {
            MD_UserEntity _user = BL_UserEntity.GetUserInfo();
            if (_user == null)
            {
                return null;
            }
            bool _In = false;
            if (Language.GetLang2() == EM_Language.en_us)
            {
                _In = true;
            }

            _c = RP.GetReplaceReport(_In, new CT_Param_Value() { RP_Code = _c.RP_Code, PV_Type = _PR, PV_CG_Code = -1, PV_UType = _user.User.UG_UType, PV_AD_OM_Code = _user.DealerEmpl.DE_AD_OM_Code });
            if (_c == null)
            {
                return null;
            }
            _sheetTitle = _c.RP_Name_EN.Trim();
            return _c.RP_Name_EN.Trim() + "_" + _user.User.AU_Code + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls";
        }
        /// <summary>
        /// 设置下载文档内容的标题
        /// </summary>
        /// <param name="Pl_Code"></param>
        /// <returns></returns>
        public List<string> getEx_Title(int Pl_Code)
        {
            int _index = 1;
            if (Language.GetLang2() == EM_Language.en_us)
            {
                _index = 2;
            }
            else { _index = 3; }
            return RP.GetExport_Title(Pl_Code, _index);
        }
        /// <summary>
        /// 获取下载数据的数据库字段名
        /// </summary>
        /// <param name="Pl_Code"></param>
        /// <returns></returns>
        public List<string> getFieldName(int Pl_Code)
        {
            return RP.GetExport_Title(Pl_Code);
        }
        /// <summary>
        /// 获取下载的数据
        /// </summary>
        /// <param name="Pl_Code"></param>
        /// <param name="PR"></param>
        /// <returns></returns>
        private DataTable getEx_Data(int Pl_Code, int PR)
        {
            CT_Reports Report = RP.GetReprot_Chat(ChartType.List, Pl_Code);
            List<CRMTreeDatabase.EX_Param> eps = new List<CRMTreeDatabase.EX_Param>();
            CRMTreeDatabase.EX_Param ep = new CRMTreeDatabase.EX_Param();
            ep.EX_Name = "PR";
            ep.EX_Value = PR.ToString();
            ep.EX_DataType = "int";
            eps.Add(ep);

            Report.RP_Query = BL_Reports.GetReportSql(Report.RP_Code, eps).SQL;
            DataTable dt = BL_Reports.GetReportSqlWithTemplate(Report.RP_Query);
            return dt;
        }

        public DataTable getEx_Data(int Pl_Code, IList<CRMTreeDatabase.EX_Param> eps)
        {
            try
            {
                CT_Reports Report = RP.GetReprot_Chat(ChartType.List, Pl_Code);
                var db = CRMTreeDatabase.DBCRMTree.GetInstance();
                PetaPoco.Sql _s = BL_Reports.GetReportSql(Report.RP_Code, eps);
                var qData = new object();
                if (_s.SQL.IndexOf(";Exec", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                     qData = db.Query<dynamic>(_s);
                }
                else
                {
                    var pSql = PetaPoco.Sql.Builder;
                    pSql.Append("select * from(" + _s.SQL, _s.Arguments);
                    pSql.Append(") as dt");
                     qData = db.Query<dynamic>(pSql);
                }

                //Report.RP_Query = _s.SQL;
                //DataTable dt = BL_Reports.GetReportSqlWithTemplate(Report.RP_Query);
                //return dt;
                string _j_d = JsonConvert.SerializeObject(qData);
                DataTable _d = ToDataTable(_j_d);
                return _d;
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// Json 字符串 转换为 DataTable数据集合
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static DataTable ToDataTable(string json)
        {
            DataTable dataTable = new DataTable();  //实例化
            DataTable result;
            try
            {
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                javaScriptSerializer.MaxJsonLength = Int32.MaxValue; //取得最大数值
                ArrayList arrayList = javaScriptSerializer.Deserialize<ArrayList>(json);
                if (arrayList.Count > 0)
                {
                    foreach (Dictionary<string, object> dictionary in arrayList)
                    {
                        if (dictionary.Keys.Count<string>() == 0)
                        {
                            result = dataTable;
                            return result;
                        }
                        if (dataTable.Columns.Count == 0)
                        {
                            foreach (string current in dictionary.Keys)
                            {
                                //if (dictionary[current])
                                //{
                                dataTable.Columns.Add(current);
                               // dataTable.Columns.Add(current, dictionary[current].GetType());
                                //}
                            }
                        }
                        DataRow dataRow = dataTable.NewRow();
                        foreach (string current in dictionary.Keys)
                        {
                            dataRow[current] = dictionary[current] == null ? null : dictionary[current];
                        }
                        dataTable.Rows.Add(dataRow); //循环添加行到DataTable中
                    }
                }
            }
            catch
            {
            }
            result = dataTable;
            return result;
        }
    }
}