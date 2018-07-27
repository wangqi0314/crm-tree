using CRMTree.DAL;
using CRMTree.Model;
using CRMTree.Model.Common;
using CRMTree.Model.Reports;
using CRMTree.Model.User;
using Newtonsoft.Json;
using PetaPoco;
using Shinfotech.Tools;
using ShInfoTech.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace CRMTree.BLL
{
    /// <summary>
    /// 此类作为处理报表表相关的方式
    /// </summary>
    public class BL_Reports
    {
        DL_Reports _D_re = new DL_Reports();
        #region GET_Reports

        #region GetList
        /// <summary>
        ///  获取报表列表 Name -- Desc
        /// 供页面列表调用
        /// </summary>
        /// <param name="Intern"></param>
        /// <param name="AU_TYPE"></param>
        /// <param name="CG_AD_OM_Code"></param>
        /// <param name="ordefiled"></param>
        /// <param name="orderway"></param>
        /// <param name="currentpage"></param>
        /// <param name="pagesize"></param>
        /// <param name="RP_Cat"></param>
        /// <param name="pagecount"></param>
        /// <param name="rowscount"></param>
        /// <param name="CharOrNot"></param>
        /// <returns></returns>
        public IList<CT_Reports> GetReportList(bool Intern, int AU_TYPE, int CG_AD_OM_Code, string ordefiled, string orderway, int currentpage, int pagesize, string RP_Cat, out int pagecount, out int rowscount, bool CharOrNot)
        {
            string RP_Code_LIST = "3,6,7,8,9,10,11,12";
            //获取分页后的Rp_code
            IList<CT_Reports> ReportList = _D_re.GetReportList(RP_Code_LIST, ordefiled, orderway, currentpage, pagesize, RP_Cat, out pagecount, out rowscount, CharOrNot);
            if (ReportList == null || ReportList.Count <= 0)
            {
                return null;
            }
            string RP_Code_LIst = ReturnString_Rp_Code(ReportList);
            string[] RP_Codes = RP_Code_LIst.Split(',');
            
            IList<CT_Reports> MD = new List<CT_Reports>();
            foreach (string RP_Code in RP_Codes)
            {
                CT_Reports Re = GetReplaceReport(Intern,
                    new CT_Param_Value() { RP_Code = int.Parse(RP_Code), PV_Type = 0, PV_CG_Code = -1, PV_UType = AU_TYPE, PV_AD_OM_Code = CG_AD_OM_Code });
                MD.Add(Re);
            }
            return MD;
        }

        #endregion

        /// <summary>
        /// 获取没有配置参数的报表
        /// </summary>
        /// <param name="RP_Code"></param>
        /// <returns></returns>
        public CT_Reports GetReprot(int RP_Code)
        {
            return _D_re.GetReprot(RP_Code);
        }
        public CT_Reports GetReprot_Chat(ChartType chartType, int FL_FB_Code)
        {
            return _D_re.GetReprot_Chat(chartType, FL_FB_Code);
        }
        /// <summary>
        /// 获取需要导出报表的标题， index=1 原始标题 index=2 英文标题 index=3 中文标题，默认index=1
        /// </summary>
        /// <param name="PL_Code"></param>
        /// <returns></returns>
        public List<string> GetExport_Title(int PL_Code)
        {
            return GetExport_Title(PL_Code, 1);
        }
        /// <summary>
        /// 获取需要导出报表的标题， index=1 原始标题 index=2 英文标题 index=3 中文标题，默认index=1
        /// </summary>
        /// <param name="PL_Code"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public List<string> GetExport_Title(int PL_Code, int index)
        {
            DataTable dt = _D_re.GetExport_Title(PL_Code);
            if (dt == null || dt.Rows.Count <= 0)
            {
                return new List<string>() { "" };
            }
            List<string> Title = new List<string>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Title.Add(dt.Rows[i][index].ToString());
            }
            return Title;
        }
        public DataTable GetExport_Type50(int PL_Code, int Type)
        {
            return _D_re.GetExport_Type50(PL_Code, Type);
        }
        /// <summary>
        /// 次方法针对Report Run后 提供调用列表的列表Code
        /// </summary>
        /// <param name="RP_Code"></param>
        /// <returns></returns>
        public string GetFields_List_Code(int RP_Code)
        {
            string _eer = "-1";
            bool _In = false;
            MD_UserEntity _user = BL_UserEntity.GetUserInfo();
            if (_user == null)
            {
                return _eer;
            }
            Fields_list FL = _D_re.GetFields_list(RP_Code);
            if (FL == null || FL.List == null)
            {
                return _eer;
            }

            if (Language.GetLang2() == EM_Language.en_us)
            {
                _In = true;
            }

            CT_Reports o = GetReplaceReport(_In, new CT_Param_Value() { RP_Code = RP_Code, PV_Type = 0, PV_CG_Code = -1, PV_UType = _user.User.UG_UType, PV_AD_OM_Code = _user.DealerEmpl.DE_AD_OM_Code });
            if (o == null)
            {
                return _eer;
            }
            _D_re.insertReport_Hist(_user.User.AU_Code, RP_Code, o.RP_Query, o.RP_Name_EN, o.RP_Name_CN);
            return FL.List[0].FL_Code.ToString();
        }

        #region 供报表的两种调用方式，1 报表名称调用 2 报表参数列表调用
        /// <summary>
        /// 获取报表,以及报表相关的参数值，单一的参数列表获取,只有默认值参数
        /// </summary>
        /// <param name="RP_Code"></param>
        /// <returns></returns>
        public CT_Reports GetReplaceReport(int RP_Code)
        {
            MD_ReportList Report = _D_re.GetReportValue(RP_Code);
            if (Report != null && Report.CT_Reports_List != null && Report.CT_Reports_List.Count > 0)
            {
                Report = BL_ReportReplace.ReportParamReplace(Report);
                Report.CT_Reports_List[0].RP_Query = ReportReplace.ReportParamReplace(Report.CT_Reports_List[0].RP_Query);
                return Report.CT_Reports_List[0];
            }
            else
            {
                CT_Reports o = _D_re.GetReprot(RP_Code);
                o.RP_Query = ReportReplace.ReportParamReplace(o.RP_Query);
                return o;
            }
        }
        /// <summary>
        /// 获取替换成功后的Report,
        /// Name Desc 都做了替换 可以单独使用，也可供其他方式调用
        /// </summary>
        /// <param name="Param_value"></param>
        /// <returns></returns>
        public CT_Reports GetReplaceReport(CT_Param_Value Param_value)
        {
            return GetReplaceReport(true, Param_value);
        }
        /// <summary>
        /// 获取替换成功后的Report,
        /// Name Desc 都做了替换 可以单独使用，也可供其他方式调用
        /// </summary>
        /// <param name="Intern"></param>
        /// <param name="Param_value"></param>
        /// <returns></returns>
        public CT_Reports GetReplaceReport(bool Intern, CT_Param_Value Param_value)
        {
            return GetReplaceReport(Intern, Param_value, EM_ParameterMode.Background, null);
        }
        /// <summary>
        /// 获取替换成功后的Report,
        /// Name Desc 都做了替换 可以单独使用，也可供其他方式调用
        /// </summary>
        /// <param name="Intern"></param>
        /// <param name="Param_value"></param>
        /// <param name="Mode"></param>
        /// <param name="Paramterslist"></param>
        /// <returns></returns>
        public CT_Reports GetReplaceReport(bool Intern, CT_Param_Value Param_value, EM_ParameterMode Mode, string Paramterslist)
        {
            MD_ReportList MD_List = GetReportValue(Param_value);
            if (MD_List == null || MD_List.CT_Reports_List == null)
            {
                return null;
            }
            if (!Intern)
            {
                MD_List = ReportReplace.NameReplace(MD_List);
            }
            MD_List = BL_ReportReplace.ReportParamReplace(MD_List, Mode, Paramterslist, Intern);
            CT_Reports Report = null;
            if (MD_List != null || MD_List.CT_Reports_List[0] != null)
            {
                Report = MD_List.CT_Reports_List[0];
                Report.RP_Query = ReportReplace.ReportParamReplace(Report.RP_Query);
            }
            return Report;
        }

        /// <summary>
        /// 根据单一的Code获取报表对应的参数列表,和参数名称
        /// 供更改参数列表的窗体调用
        /// </summary>
        /// <param name="Intern"></param>
        /// <param name="Param_value"></param>
        /// <returns></returns>
        public MD_Paramterslist GetParamterslist(bool Intern, CT_Param_Value Param_value)
        {
            MD_ReportList MD_List = GetReportValue(Param_value);
            if (!Intern)
            {
                MD_List = ReportReplace.NameReplace(MD_List);
            }
            MD_Paramterslist Param = new MD_Paramterslist();
            Param.CT_Paramters_list = new List<CT_Paramters_list>();
            foreach (CT_Reports RE in MD_List.CT_Reports_List)
            {
                CT_Paramters_list p_List = new CT_Paramters_list();
                p_List.PL_Code = RE.PL_Code;
                p_List.PL_Prompt_En = RE.PL_Prompt_En;
                p_List.PL_Type = RE.PL_Type;
                p_List.PL_Default = RE.PL_Default;
                Param.CT_Paramters_list.Add(p_List);
            }
            return Param;
        }
        #endregion

        #region

        public static string GetValues(string id, string FieldName)
        {
            string Query = "select text_en,text_cn,value from words where id=" + id;

            DataTable dtTmp = QueryExecution(Query);

            return dtTmp.Rows[0][FieldName].ToString();
        }

        public static DataTable GetRegardingTables(string id)
        {
            string Query = "select * from CT_Campaigns where CG_Template=1 and CG_Type =" + id;

            DataTable dtTmp = QueryExecution(Query);

            return dtTmp;
        }

        public static DataTable GetFieldsByQuery(string query)
        {
            DataTable dtTmp = QueryExecution(query);

            return dtTmp;
        }

        #endregion
        #endregion

        #region Insert
        /// <summary>
        /// Report的设置成功好保存相关的数据
        /// </summary>
        /// <param name="AU_TYPE"></param>
        /// <param name="CG_AD_OM_Code"></param>
        /// <param name="PL_Code_List"></param>
        /// <param name="Pl_Val_List"></param>
        /// <returns></returns>
        public int insertTags(int AU_TYPE, int CG_AD_OM_Code, string PL_Code_List, string Pl_Val_List)
        {
            return _D_re.insertTags(AU_TYPE, CG_AD_OM_Code, PL_Code_List, Pl_Val_List);
        }
        #endregion

        #region Return
        /// <summary>
        ///获取分页后的RP_Code
        /// </summary>
        /// <param name="Rp"></param>
        /// <returns></returns>
        public static string ReturnString_Rp_Code(IList<CT_Reports> Rp)
        {
            string str_Query = string.Empty;
            if (Rp == null || Rp.Count <= 0)
            {
                return str_Query;
            }
            for (int i = 0; i < Rp.Count; i++)
            {
                str_Query += Rp[i].RP_Code.ToString() + ",";
            }
            if (str_Query.Length > 1)
            {
                str_Query = str_Query.Substring(0, str_Query.Length - 1);
            }
            return str_Query;
        }
        #endregion

        #region Query
        /// <summary>
        /// 根据报表的RP_Query来执行SQl，返回表格形式
        /// </summary>
        /// <param name="RP_Query"></param>
        /// <returns></returns>
        public static DataTable QueryExecution(string RP_Query)
        {
            return DL_Reports.QueryExecution(RP_Query);
        }
        #endregion

        #region private
        /// <summary>
        /// 获取报表的相关的值，根据传递的Code
        /// 如果Pv_val 不为空，就将值赋值与PL_Default
        /// new CT_Param_Value() { 
        /// RP_Code = RP_Code, 
        /// PV_Type = 1/2, 
        /// PV_CG_Code = CG_Code/EV_Code, 
        /// PV_UType = UG_UType, 
        /// PV_AD_OM_Code =DE_AD_OM_Code }
        /// </summary>
        /// <param name="Param_value"></param>
        /// <returns></returns>
        private MD_ReportList GetReportValue(CT_Param_Value Param_value)
        {
            return _D_re.GetReportValue(Param_value);
        }
        #endregion

        #region 报表Run
        public int ReportRun(ReportRunParam rParam)
        {
            //-1 代表请求参数为空；
            if (rParam == null) { return -1; }
            CT_Reports _r;
            if (rParam.pType == 1)
            {
                BL_Campaign c = new BL_Campaign();
                CT_Campaigns _c = c.GetCampaign(rParam.CG_EV_Code);
                CT_Param_Value _p = new CT_Param_Value()
                {
                    RP_Code = _c.CG_RP_Code,
                    PV_Type = 1,
                    PV_CG_Code = _c.CG_Code,
                    PV_UType = rParam.UType,
                    PV_AD_OM_Code = rParam.AU_AD_OM_Code
                };
                _r = GetReplaceReport(_p);

                List<CRMTreeDatabase.EX_Param> eps = new List<CRMTreeDatabase.EX_Param>();
                CRMTreeDatabase.EX_Param ep = new CRMTreeDatabase.EX_Param();
                ep.EX_Name = "PR";
                ep.EX_Value = "0";
                ep.EX_DataType = "int";
                eps.Add(ep);
                //var oeps = JsonConvert.SerializeObject(eps);

                _r.RP_Query = GetReportSql(_r.RP_Code, eps).SQL;
                return _D_re.ReportRun<CT_Campaigns>(_r, _c, rParam.UType, rParam.AU_AD_OM_Code);
            }
            else if (rParam.pType == 2)
            {
                DL_Event e = new DL_Event();
                CT_Events _e = e.getEvents(rParam.CG_EV_Code);
                CT_Param_Value _p = new CT_Param_Value()
                {
                    RP_Code = _e.EV_RP_Code,
                    PV_Type = 2,
                    PV_CG_Code = _e.EV_Code,
                    PV_UType = rParam.UType,
                    PV_AD_OM_Code = rParam.AU_AD_OM_Code
                };
                _r = GetReplaceReport(_p);
                _r.RP_Query = GetReportSql(_r.RP_Code, null).SQL;
                return _D_re.ReportRun<CT_Events>(_r, _e, rParam.UType, rParam.AU_AD_OM_Code);
            }
            return -1;
        }
        #endregion


        #region Reports
        /// <summary>
        /// 获得数据类型
        /// </summary>
        /// <param name="dataTypeName"></param>
        /// <returns></returns>
        private static Type GetDataType(string dataTypeName)
        {
            var type = typeof(string);
            switch (dataTypeName)
            {
                case "int":
                    type = typeof(int);
                    break;
            }
            return type;
        }

        /// <summary>
        /// 获得报表编号
        /// </summary>
        /// <param name="MF_FL_FB_Code">列表编号</param>
        /// <returns></returns>
        public static int GetReportCode(int MF_FL_FB_Code)
        {
            var rp_code = 0;
            var db = CRMTreeDatabase.DBCRMTree.GetInstance();
            rp_code = db.ExecuteScalar<int>("select FL_RP_Code from CT_Fields_lists where FL_Code=@0", MF_FL_FB_Code);

            return rp_code;
        }

        /// <summary>
        /// 设置报表Sql
        /// </summary>
        /// <param name="reportSql">报表sql</param>
        /// <param name="queryParams">查询参数</param>
        /// <returns></returns>
        public static Sql SetReportSql(string reportSql, object queryParams)
        {
            var sql = PetaPoco.Sql.Builder;
            if (null == queryParams)
            {
                return sql.Append(reportSql);
            }
            var exParams = JsonConvert.DeserializeObject<List<CRMTreeDatabase.EX_Param>>(JsonConvert.SerializeObject(queryParams));
            var dt = new DataTable();
            var row = dt.NewRow();
            var index = 0;
            foreach (CRMTreeDatabase.EX_Param queryParam in exParams)
            {
                reportSql = reportSql.Replace("@" + queryParam.EX_Name, "@" + index);
                dt.Columns.Add(queryParam.EX_Name, GetDataType(queryParam.EX_DataType));
                row[queryParam.EX_Name] = queryParam.EX_Value;
                index++;
            }
            sql.Append(reportSql, row.ItemArray);

            return sql;
        }

        /// <summary>
        /// 获得报表Sql
        /// </summary>
        /// <param name="rp_code">报表编号</param>
        /// <param name="queryParams">查询参数</param>
        /// <returns></returns>
        public static Sql GetReportSql(int rp_code, object queryParams)
        {
            string sql = string.Empty;

            BL_Reports bllReports = new BL_Reports();
            sql = bllReports.GetReprot(rp_code).RP_Query;
            sql = ReportReplace.ReportParamReplace(sql);
            string[] sqls = sql.Split(new string[] { "|-|" }, StringSplitOptions.None);
            if (sqls.Length == 1)
            {
                sql = sqls[0];
            }
            else if (sqls.Length == 2)
            {
                sql = sqls[1];

                var db = CRMTreeDatabase.DBCRMTree.GetInstance();
                var procSql = sqls[0];
                var pSql = SetReportSql(procSql, queryParams);
                var tags = db.Query<CRMTreeDatabase.EX_Tag>(pSql);
                sql = sql.Replace("{{", "@").Replace("}}", "");
                foreach (var tag in tags)
                {
                    sql = sql.Replace(tag.PL_Tag, tag.PV_Val);
                }
            }

            return SetReportSql(sql, queryParams);
        }

        /// <summary>
        /// 获得报表单数据
        /// </summary>
        /// <param name="RP_Code">报表编号</param>
        /// <param name="queryParams">查询参数</param>
        /// <returns></returns>
        public static DataTable GetSingleReportData(int RP_Code, object queryParams)
        {
            var db = CRMTreeDatabase.DBCRMTree.GetInstance();
            var sql = GetReportSql(RP_Code, queryParams);

            var dt = new DataTable();
            db.Fill(dt, sql.SQL, sql.Arguments);

            return dt;
        }

        /// <summary>
        /// 获得报表多数据
        /// </summary>
        /// <param name="RP_Code">报表编号</param>
        /// <param name="queryParams">查询参数</param>
        /// <returns></returns>
        public static DataSet GetReportData(int RP_Code, object queryParams)
        {
            var db = CRMTreeDatabase.DBCRMTree.GetInstance();
            var sql = GetReportSql(RP_Code, queryParams);

            var ds = new DataSet();
            db.Fill(ds, sql.SQL, sql.Arguments);

            return ds;
        }


        public static DataTable GetReportSqlWithTemplate(string sql, int PR = 0)
        {
            string[] sqls = sql.Split(new string[] { "|-|" }, StringSplitOptions.None);
            if (sqls.Length == 1)
            {
                sql = sqls[0];
            }
            else if (sqls.Length == 2)
            {
                sql = sqls[1];

                var db = CRMTreeDatabase.DBCRMTree.GetInstance();
                var procSql = sqls[0];
                var pr = new CRMTreeDatabase.EX_Param();
                pr.EX_DataType = "int";
                pr.EX_Name = "PR";
                pr.EX_Value = PR.ToString();
                List<CRMTreeDatabase.EX_Param> ps = new List<CRMTreeDatabase.EX_Param>();
                ps.Add(pr);
                var pSql = SetReportSql(procSql, ps);
                var tags = db.Query<CRMTreeDatabase.EX_Tag>(pSql);
                foreach (var tag in tags)
                {
                    sql = sql.Replace(tag.PL_Tag, tag.PV_Val);
                }
            }
            return DL_Reports.QueryExecution(sql);
        }

        #endregion

        //13112
        public static string GetFileContent(int CG_Code, List<CRMTreeDatabase.EX_Param> ex_params = null, int EX_Type = 1)
        {
            string fileContent = string.Empty;
            var db = CRMTreeDatabase.DBCRMTree.GetInstance();
            var fileName = db.ExecuteScalar<string>("SELECT CG_Filename FROM CT_Campaigns WHERE CG_Code = @0", CG_Code);
            fileContent = GetFileContent(fileName, ex_params, EX_Type);
            return fileContent;
        }
        /// <summary>
        /// 
        /// EX_DataType = "int", EX_Name = "EX_Type", EX_Value = "1"   1=.txt 2=.html
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="ex_params"></param>
        /// <returns></returns>
        public static string GetFileContent(string fileName, List<CRMTreeDatabase.EX_Param> ex_params = null, int EX_Type = 1)
        {
            if (EX_Type != 1)
            {
                fileName = fileName.ToUpper();
                fileName = fileName.Replace(".HTML", ".txt");
                fileName = fileName.Replace(".HTM", ".txt");
            }

            string fileContent = string.Empty;
            string path = string.Empty;
            //path = "";
            path = System.Configuration.ConfigurationManager.AppSettings["_PLUpload_File_Path"];
            if (string.IsNullOrWhiteSpace(path))
            {
                //path = @"D:\Projects\CRMTREE\CRMTREE\CRMTREE.WEB\WebSite\plupload\file\";
                path = "~/plupload/";
            }
            path = HttpContext.Current.Server.MapPath(path) + "file/";

            fileContent = ShInfoTech.Common.Files.FileContext(path, fileName);

            Regex reg = new Regex(@"(?<={{)[^{{}}]+(?=}})");
            MatchCollection mcs = reg.Matches(fileContent);

            if (mcs.Count == 0)
            {
                return fileContent;
            }

            List<int> listCodes = new List<int>();
            foreach (Match mc in mcs)
            {
                listCodes.Add(Convert.ToInt32(mc.Value));
            }

            var codes = string.Join(",", listCodes.Distinct());
            var db = CRMTreeDatabase.DBCRMTree.GetInstance();
            var tags = db.Query<CRMTreeDatabase.CT_Camp_Tag>(@"SELECT CT_Code
            ,CT_Type
            ,CT_FieldName
            ,CT_Category
            ,CT_Desc_EN
            ,CT_Desc_CN
            FROM CT_Camp_Tags
            WHERE CT_Code IN (SELECT * FROM dbo.f_split(@0,','))", codes);

            //distinct CT_Category
            var category = (from tag in tags select tag.CT_Category).Distinct().ToList();
            //replace by CT_Category
            foreach (var c in category)
            {
                var sql = string.Empty;
                switch (c)
                {
                    case 1:
                        sql = "Select {0} from CT_Auto_Dealers AD where AD_Code = @AD";
                        break;
                    case 2:
                        sql = "Select {0} from CT_Dealer_Groups DG where DG_Code=@DG";
                        break;
                    case 3:
                        sql = "Select {0} from CT_OEM OM where OM_Code=@OM";
                        break;
                    case 4:
                        sql = "Select {0} from CT_CRMTree CT where CT_Code=@CT";
                        break;
                    case 5:
                        sql = "Select {0} from CT_All_Users AU where AU_Code=@AU";
                        break;
                    case 6:
                        sql = "Select {0} from CT_Car_Inventory CI where CI_Code=@CI";
                        break;
                    case 7:
                        sql = "Select {0} from CT_Campaigns CG where CG_Code=@CG";
                        break;
                    case 9:
                        sql = "Select {0} from CT_Dealer_Empl DE where DE_Code=@DE";
                        break;
                    case 10:
                        sql = "Select {0} from CT_Appt_Service AP where AP_Code=@AP";
                        break;
                    case 11:
                        sql = "Select {0} from CT_Purch_Appt PA where PA_Code=@PA";
                        break;
                    case 100:
                        sql = "Select {0}";
                        break;
                }

                try
                {
                    if (!string.IsNullOrWhiteSpace(sql))
                    {
                        var fieldNames = (from tag in tags where tag.CT_Category == c orderby tag.CT_Code select tag.CT_FieldName).ToList();
                        var fieldCodes = (from tag in tags where tag.CT_Category == c orderby tag.CT_Code select tag.CT_Code).ToList();
                        var cols = string.Join(",", fieldNames);
                        sql = string.Format(sql, cols);

                        sql = ReportReplace.ReportParamReplace(sql);

                        var dt = new DataTable();
                        var pSql = SetReportSql(sql, ex_params);
                        db.Fill(dt, pSql.SQL, pSql.Arguments);
                        if (dt.Rows.Count > 0)
                        {
                            var rowData = dt.Rows[0];
                            for (int i = 0, len = fieldCodes.Count; i < len; i++)
                            {
                                object value = rowData[i];
                                string val = "";
                                if (null != value)
                                {
                                    val = value.ToString();
                                }
                                fileContent = fileContent.Replace("{{" + fieldCodes[i].ToString() + "}}", val);
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                }
            }

            return fileContent;
        }


        public static string GetFileContent(int CG_Code, params object[] fileParams)
        {
            string fileContent = string.Empty;
            var db = CRMTreeDatabase.DBCRMTree.GetInstance();
            //            var fileName = db.ExecuteScalar<string>("SELECT CG_Filename FROM CT_Campaigns WHERE CG_Code = @0", CG_Code);
            var fileName = db.ExecuteScalar<string>("select CM_Filename from CT_Campaigns left join CT_Camp_Methods on CM_CG_Code=CG_Code WHERE CG_Code = @0 and CM_Contact_Index= @1", CG_Code);
            fileContent = GetFileContent(fileName, fileParams);
            return fileContent;
        }

        public static string GetFileContent(int CG_Code, int CM_Index, params object[] fileParams)
        {
            string fileContent = string.Empty;
            var db = CRMTreeDatabase.DBCRMTree.GetInstance();
            //            var fileName = db.ExecuteScalar<string>("SELECT CG_Filename FROM CT_Campaigns WHERE CG_Code = @0", CG_Code);
            var fileName = db.ExecuteScalar<string>("select CM_Filename from CT_Campaigns left join CT_Camp_Methods on CM_CG_Code=CG_Code WHERE CG_Code = @0 and CM_Contact_Index= @1", CG_Code, CM_Index);
            fileContent = GetFileContent(fileName, fileParams);
            return fileContent;
        }

        public static string GetFileContent(string fileName, params object[] fileParams)
        {
            string fileContent = string.Empty;
            string path = string.Empty;
            //path = "";
            path = System.Configuration.ConfigurationManager.AppSettings["_PLUpload_File_Path"];
            if (string.IsNullOrWhiteSpace(path))
            {
                //path = @"D:\Projects\CRMTREE\CRMTREE\CRMTREE.WEB\WebSite\plupload\file\";
                path = "~/plupload/";
            }
            path = HttpContext.Current.Server.MapPath(path) + "file/";

            fileContent = ShInfoTech.Common.Files.FileContext(path, fileName);

            Regex reg = new Regex(@"(?<={{)[^{{}}]+(?=}})");
            MatchCollection mcs = reg.Matches(fileContent);

            if (mcs.Count == 0)
            {
                return fileContent;
            }

            List<int> listCodes = new List<int>();
            foreach (Match mc in mcs)
            {
                listCodes.Add(Convert.ToInt32(mc.Value));
            }

            var codes = string.Join(",", listCodes.Distinct());
            var db = CRMTreeDatabase.DBCRMTree.GetInstance();
            var tags = db.Query<CRMTreeDatabase.CT_Camp_Tag>(@"SELECT CT_Code
            ,CT_Type
            ,CT_FieldName
            ,CT_Category
            ,CT_Desc_EN
            ,CT_Desc_CN
            FROM CT_Camp_Tags
            WHERE CT_Code IN (SELECT * FROM dbo.f_split(@0,','))", codes);

            //distinct CT_Category
            var category = (from tag in tags select tag.CT_Category).Distinct().ToList();
            //replace by CT_Category
            foreach (var c in category)
            {
                var sql = string.Empty;
                switch (c)
                {
                    case 1:
                        sql = "Select {0} from CT_Auto_Dealers AD where AD_Code = @AD";
                        break;
                    case 2:
                        sql = "Select {0} from CT_Dealer_Groups DG where DG_Code=@DG";
                        break;
                    case 3:
                        sql = "Select {0} from CT_OEM OM where OM_Code=@OM";
                        break;
                    case 4:
                        sql = "Select {0} from CT_CRMTree CT where CT_Code=@CT";
                        break;
                    case 5:
                        sql = "Select {0} from CT_All_Users AU where AU_Code=@AU";
                        break;
                    case 6:
                        sql = "Select {0} from CT_Car_Inventory CI where CI_Code=@CI";
                        break;
                    case 7:
                        sql = "Select {0} from CT_Campaigns CG where CG_Code=@CG";
                        break;
                    case 9:
                        sql = "Select {0} from CT_Dealer_Empl DE where DE_Code=@DE";
                        break;
                    case 10:
                        sql = "Select {0} from CT_Appt_Service AS where AS_Code=@AS";
                        break;
                    case 11:
                        sql = "Select {0} from CT_Purch_Appt PA where PA_Code=@PA";
                        break;
                    case 100:
                        sql = "Select {0}";
                        break;
                }

                try
                {
                    if (!string.IsNullOrWhiteSpace(sql))
                    {
                        var fieldNames = (from tag in tags where tag.CT_Category == c orderby tag.CT_Code select tag.CT_FieldName).ToList();
                        var fieldCodes = (from tag in tags where tag.CT_Category == c orderby tag.CT_Code select tag.CT_Code).ToList();
                        var cols = string.Join(",", fieldNames);
                        sql = string.Format(sql, cols);

                        sql = ReportReplace.ReportParamReplace(sql);

                        var dt = new DataTable();
                        db.Fill(dt, sql, fileParams);
                        if (dt.Rows.Count > 0)
                        {
                            var rowData = dt.Rows[0];
                            for (int i = 0, len = fieldCodes.Count; i < len; i++)
                            {
                                object value = rowData[i];
                                string val = "";
                                if (null != value)
                                {
                                    val = value.ToString();
                                }
                                fileContent = fileContent.Replace("{{" + fieldCodes[i].ToString() + "}}", val);
                            }
                        }
                    }
                }
                catch (Exception)
                {
                }
            }

            return fileContent;
        }

        /// <summary> 
        /// EX_DataType = "int", EX_Name = "EX_Type", EX_Value = "1"   1=.txt 2=.html
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="ex_params"></param>
        /// <returns></returns>
        public static string GetRunFileContent(string fileContent, List<CRMTreeDatabase.EX_Param> ex_params = null, int EX_Type = 1)
        {
            Regex reg = new Regex(@"(?<={{)[^{{}}]+(?=}})");
            MatchCollection mcs = reg.Matches(fileContent);
            if (mcs.Count == 0)
            {
                return fileContent;
            }

            List<int> listCodes = new List<int>();
            foreach (Match mc in mcs)
            {
                listCodes.Add(Convert.ToInt32(mc.Value));
            }

            var codes = string.Join(",", listCodes.Distinct());
            var db = CRMTreeDatabase.DBCRMTree.GetInstance();
            var tags = db.Query<CRMTreeDatabase.CT_Camp_Tag>(@"SELECT CT_Code
            ,CT_Type
            ,CT_FieldName
            ,CT_Category
            ,CT_Desc_EN
            ,CT_Desc_CN
            FROM CT_Camp_Tags
            WHERE CT_Code IN (SELECT * FROM dbo.f_split(@0,','))", codes);

            //distinct CT_Category
            var category = (from tag in tags select tag.CT_Category).Distinct().ToList();
            //replace by CT_Category
            foreach (var c in category)
            {
                var sql = string.Empty;
                switch (c)
                {
                    case 1:
                        sql = "Select {0} from CT_Auto_Dealers AD where AD_Code = @AD";
                        break;
                    case 2:
                        sql = "Select {0} from CT_Dealer_Groups DG where DG_Code=@DG";
                        break;
                    case 3:
                        sql = "Select {0} from CT_OEM OM where OM_Code=@OM";
                        break;
                    case 4:
                        sql = "Select {0} from CT_CRMTree CT where CT_Code=@CT";
                        break;
                    case 5:
                        sql = "Select {0} from CT_All_Users AU where AU_Code=@AU";
                        break;
                    case 6:
                        sql = "Select {0} from CT_Car_Inventory CI where CI_Code=@CI";
                        break;
                    case 7:
                        sql = "Select {0} from CT_Campaigns CG where CG_Code=@CG";
                        break;
                    case 9:
                        sql = "Select {0} from CT_Dealer_Empl DE where DE_Code=@DE";
                        break;
                    case 10:
                        sql = "Select {0} from CT_Appt_Service AS where AS_Code=@AS";
                        break;
                    case 11:
                        sql = "Select {0} from CT_Purch_Appt PA where PA_Code=@PA";
                        break;
                    case 100:
                        sql = "Select {0}";
                        break;
                }
                try
                {
                    if (!string.IsNullOrWhiteSpace(sql))
                    {
                        var fieldNames = (from tag in tags where tag.CT_Category == c orderby tag.CT_Code select tag.CT_FieldName).ToList();
                        var fieldCodes = (from tag in tags where tag.CT_Category == c orderby tag.CT_Code select tag.CT_Code).ToList();
                        var cols = string.Join(",", fieldNames);
                        sql = string.Format(sql, cols);

                        sql = ReportReplace.ReportParamReplace(sql);

                        var dt = new DataTable();
                        var pSql = SetReportSql(sql, ex_params);
                        db.Fill(dt, pSql.SQL, pSql.Arguments);
                        if (dt.Rows.Count > 0)
                        {
                            var rowData = dt.Rows[0];
                            for (int i = 0, len = fieldCodes.Count; i < len; i++)
                            {
                                object value = rowData[i];
                                string val = "";
                                if (null != value)
                                {
                                    val = value.ToString();
                                }
                                fileContent = fileContent.Replace("{{" + fieldCodes[i].ToString() + "}}", val);
                            }
                        }
                    }
                }
                catch {  }
            }

            return fileContent;
        }

        public string[] GetUserCode(string Query, string Code)
        {
            return _D_re.GetUserCode(Query, Code);
        }
    }
}
