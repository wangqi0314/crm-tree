using CRMTree.Public;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using Shinfotech.Tools;
using ShInfoTech.Common;
using CRMTree.Model.Common;
using CRMTree.BLL;
using CRMTree.Model.Event;
using CRMTree.Model;
using System.Collections.Generic;
using Newtonsoft.Json;

public partial class handler_ajax_campaign : BasePage
{
    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        string strAction = RequestClass.GetString("action").ToLower();//操作动作 
        if ("campaign_create_file" == strAction || "campaign_edit_file" == strAction || "add_targeted" == strAction || "list_campaign" == strAction || "add_campaign" == strAction || "delete_campaign" == strAction || "up_campaign" == strAction || "del_file" == strAction || "edit_file" == strAction || "add_html" == strAction || "create_html" == strAction)
        {
            switch (strAction)
            {
                case "add_targeted":
                    Add_Customers_Targeted();//Customers Targeted 下拉框自定义添加
                    break;
                case "list_campaign":
                    CampaignList();//campaign 列表
                    break;
                case "add_campaign":
                    Add_Campaign();//添加 campaign
                    break;
                case "up_campaign":
                    Update_Campaign();//修改 campaign
                    break;
                case "delete_campaign":
                    CampaignDelete();//删除 campaign
                    break;
                case "del_file":
                    Delete_File();//删除 upload File 物理文件
                    break;
                case "edit_file":
                    Edit_file();
                    break;
                case "campaign_edit_file":
                    Campaign_Edit_file();
                    break;
                case "campaign_create_file":
                    Campaign_Save_file();
                    break;
                    
                case "add_html":
                    Edits_file();
                    break;
                case "create_html":
                    Create_file();
                    break;
            }
        }
        else
            Response.Write("-1");//action 参数不正确，没权限
    }

    #region Customers Targeted 下拉框自定义添加
    private void Add_Customers_Targeted()
    {
        int intCoede = RequestClass.GetInt("intCoede", 0);
        int intCG_Parameter = RequestClass.GetInt("targeted", 0);
        if (0 < intCoede && 0 < intCG_Parameter)
        {

        }
        else
        {
            Response.Write("-4");//参数不正确或未填写内容
        }
    }
    #endregion

    #region 查询
    private void CampaignList()
    {
        string strSortField = RequestClass.GetString("sortfield").Trim().Replace("'", "");  //排序字段
        string strSortRule = RequestClass.GetString("sortrule").Trim().Replace("'", ""); //排序规则
        int intCurrentPage = RequestClass.GetInt("page", 1);//当前页
        int CT = RequestClass.GetInt("CT", 1);
        int CA = RequestClass.GetInt("CA",1);
        string orderClass = String.Empty; //排序字段的样式
        if (string.IsNullOrEmpty(strSortField))
        {
            strSortField = "CG_Update_dt";
            strSortRule = "desc";
            orderClass = "taxisDown";
        }
        else
        {
            if (strSortRule.Equals("asc"))
                orderClass = "taxisUp";
            else
                orderClass = "taxisDown";
        }
        BL_Campaign Campaign = new BL_Campaign();
        string HTML = Campaign.getCampaignList(Interna, UserSession, "CG_Code", "*", strSortField, strSortRule, intCurrentPage, 5, orderClass ,CT,CA);
        Response.Write(HTML);
    }
    #endregion

    #region 添加 campaign
    private void Add_Campaign()
    {
        #region 接收参数
        string strTitle = Microsoft.JScript.GlobalObject.unescape(RequestClass.GetString("strTitle")).Replace("'", "").Trim();
        string strDesc = Microsoft.JScript.GlobalObject.unescape(RequestClass.GetString("strDesc")).Replace("'", "").Trim();
        string PL_Code = RequestClass.GetString("PL_Code");
        string PL_Val = Microsoft.JScript.GlobalObject.unescape(RequestClass.GetString("PL_Val")).Replace("'", "").Trim();

        string Share = RequestClass.GetString("Share");
        int Active = int.Parse(RequestClass.GetString("Active"));
        int intType = RequestClass.GetInt("intType", 0);
        int RP_Code = RequestClass.GetInt("RP_Code", 0);
        int intType_Time = RequestClass.GetInt("intType_Time", 0);
        DateTime dtSt = RequestClass.GetFormDateTime("strdtSt");
        DateTime dtEt = RequestClass.GetFormDateTime("strdtEt");

        string strMothod = RequestClass.GetString("strMothod").Replace("'", "").Trim();
        int textMethods = RequestClass.GetInt("textMethods", 0);
        int intWhom = RequestClass.GetInt("intPerson", 0);
        int intTargeted = RequestClass.GetInt("intTargeted", 0);
        int intcodeVal = RequestClass.GetInt("intcodeVal", 0);


        //int intSucc = RequestClass.GetInt("intSucc", 0);
        string intSucc = Microsoft.JScript.GlobalObject.unescape(RequestClass.GetString("intSucc")).Replace("'", "").Trim();

        string strFileNameAll = Microsoft.JScript.GlobalObject.unescape(RequestClass.GetString("strFileNameAll")).Replace("'", "").Trim();
         string o_succ = Microsoft.JScript.GlobalObject.unescape(RequestClass.GetString("o_succ")).Replace("'", "").Trim();
        MD_SuccMatrixList o = new MD_SuccMatrixList();
        o.SuccMatrixList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<CT_Succ_Matrix>>(o_succ);
        #endregion
        if (0 >= strTitle.Length || 0 >= strDesc.Length || (0 == intType_Time && (1900 == dtSt.Year || 1900 == dtEt.Year)) ||
            1 >= strMothod.Length || 0 == intTargeted || 1 >= strFileNameAll.Length)
        {
            //参数 没有全填写
            Response.Write("-2");
        }
        else
        {
            strMothod = strMothod.Substring(0, strMothod.Length - 1);
            #region 参数
            SqlParameter[] parameters = {
                    new SqlParameter("@results", SqlDbType.Int,4),
					new SqlParameter("@CG_Title", SqlDbType.NVarChar,50),
					new SqlParameter("@CG_Desc", SqlDbType.NVarChar,250),
					new SqlParameter("@CG_Type", SqlDbType.TinyInt,4),
					new SqlParameter("@CG_Type_Frequency", SqlDbType.TinyInt,4),
                    new SqlParameter("@CG_Method", SqlDbType.NVarChar,100),
                    new SqlParameter("@CG_Whom", SqlDbType.TinyInt,4),
                    new SqlParameter("@CG_Filename", SqlDbType.NVarChar,500),        
                    new SqlParameter("@CG_Start_Dt",SqlDbType.DateTime),
                    new SqlParameter("@CG_End_Dt",SqlDbType.DateTime),
                    new SqlParameter("@CG_Succ_Matrix", SqlDbType.NVarChar,80),
                    new SqlParameter("@CG_AD_OM_Code", SqlDbType.TinyInt,4),
                    new SqlParameter("@CG_UType",SqlDbType.Int,4),
                    new SqlParameter("@PL_Code",SqlDbType.NVarChar),
                    new SqlParameter("@Pl_Val",SqlDbType.NVarChar),
                    new SqlParameter("@RP_Code",SqlDbType.TinyInt,4),
                    new SqlParameter("@Share",SqlDbType.Bit),
                    new SqlParameter("@AU_Code",SqlDbType.Int,4),
                    new SqlParameter("@Active",SqlDbType.Int,4),
                    new SqlParameter("@textMethods",SqlDbType.Int,4),
        };
            parameters[0].Direction = ParameterDirection.Output;
            parameters[1].Value = strTitle;
            parameters[2].Value = strDesc;
            parameters[3].Value = intType;
            parameters[4].Value = intType_Time;
            parameters[5].Value = strMothod;
            parameters[6].Value = intWhom;
            parameters[7].Value = strFileNameAll;
            parameters[8].Value = dtSt;
            parameters[9].Value = dtEt;
            parameters[10].Value = intSucc;
            parameters[11].Value = UserSession.DealerEmpl.DE_AD_OM_Code;
            parameters[12].Value = UserSession.User.UG_UType;
            parameters[13].Value = PL_Code;
            parameters[14].Value = PL_Val;
            parameters[15].Value = RP_Code;
            parameters[16].Value = Convert.ToByte(Share);
            parameters[17].Value = UserSession.User.AU_Code;
            parameters[18].Value = Active;
            parameters[19].Value = textMethods;
            #endregion

            SqlConnection _c = new SqlConnection(Tools.GetConnString());
            _c.Open();
            SqlTransaction _t = _c.BeginTransaction();

            try
            {
                int i = SqlHelper.ExecuteNonQuery(_t, CommandType.StoredProcedure, "CT_Campaigns_Add", parameters);
                //Succ的操作
                if (i > 0)
                {
                    int _id = (int)parameters[0].Value;
                    SqlHelper.ExecuteNonQuery(_t, CommandType.Text,
                                          "delete from CT_SM_Values where SMV_Type=1 and SMV_CG_Code=" + _id + ";");
                    for (int j = 0; j < o.SuccMatrixList.Count; j++)
                    {
                        CT_Succ_Matrix _o = o.SuccMatrixList[j];
                        SqlHelper.ExecuteNonQuery(_t, CommandType.Text,
                                          "insert into CT_SM_Values values(" + _o.PSM_Code + ",1," + _id + "," + _o.SMV_Days + "," + _o.SMV_Val + ");");
                    }
                }
                _t.Commit();
                _c.Close();
            }
            catch { _t.Rollback(); _c.Close(); }
            //DbHelperSQL.RunProcedure("CT_Campaigns_Add", parameters, out rowsAffected);
            Response.Write(parameters[0].Value.ToString());
        }
    }
    #endregion

    #region 修改 campaign
    private void Update_Campaign()
    {
        #region 接收参数
        int intID = RequestClass.GetInt("id", 0);
        string strTitle = Microsoft.JScript.GlobalObject.unescape(RequestClass.GetString("strTitle")).Replace("'", "").Trim();
        string strDesc = Microsoft.JScript.GlobalObject.unescape(RequestClass.GetString("strDesc")).Replace("'", "").Trim();
        string PL_Code = RequestClass.GetString("PL_Code");
        string PL_Val = Microsoft.JScript.GlobalObject.unescape(RequestClass.GetString("PL_Val")).Replace("'", "").Trim();

        string Share = RequestClass.GetString("Share");
        int Active = int.Parse(RequestClass.GetString("Active"));
        int intType = RequestClass.GetInt("intType", 0);
        int intType_Time = RequestClass.GetInt("intType_Time", 0);
        DateTime dtSt = RequestClass.GetFormDateTime("strdtSt");
        DateTime dtEt = RequestClass.GetFormDateTime("strdtEt");

        string strMothod = RequestClass.GetString("strMothod").Replace("'", "").Trim();
        int textMethods = RequestClass.GetInt("textMethods", 0);

        int intWhom = RequestClass.GetInt("intPerson", 0);
        int intTargeted = RequestClass.GetInt("intTargeted", 0);
        int intcodeVal = RequestClass.GetInt("intcodeVal", 0);
        //int intSucc = RequestClass.GetInt("intSucc", 0);
        string intSucc = Microsoft.JScript.GlobalObject.unescape(RequestClass.GetString("intSucc")).Replace("'", "").Trim();

        string strFileNameAll = Microsoft.JScript.GlobalObject.unescape(RequestClass.GetString("strFileNameAll")).Replace("'", "").Trim();

        string o_succ = Microsoft.JScript.GlobalObject.unescape(RequestClass.GetString("o_succ")).Replace("'", "").Trim();
        MD_SuccMatrixList o = new MD_SuccMatrixList();
        o.SuccMatrixList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<CT_Succ_Matrix>>(o_succ);
        #endregion
        if (0 >= intID || 0 >= strTitle.Length || 0 >= strDesc.Length || (0 == intType_Time && (1900 == dtSt.Year ||
            1900 == dtEt.Year)) || 1 >= strMothod.Length || 0 == intTargeted || 1 >= strFileNameAll.Length)
        {
            //参数 没有全填写
            Response.Write("-2");
        }
        else
        {
            strMothod = strMothod.Substring(0, strMothod.Length - 1);
            #region 参数
            SqlParameter[] parameters = {
                    new SqlParameter("@results", SqlDbType.Int,4),
                    new SqlParameter("@CG_Code",SqlDbType.Int,4),
					new SqlParameter("@CG_Title", SqlDbType.NVarChar,50),
					new SqlParameter("@CG_Desc", SqlDbType.NVarChar,250),
					new SqlParameter("@CG_Type", SqlDbType.TinyInt,4),
					new SqlParameter("@CG_Type_Frequency", SqlDbType.TinyInt,4),
                    new SqlParameter("@CG_Method", SqlDbType.NVarChar,100),
                    new SqlParameter("@CG_Whom", SqlDbType.TinyInt,4),
                    new SqlParameter("@CG_Filename", SqlDbType.NVarChar,500),        
                    new SqlParameter("@CG_Start_Dt",SqlDbType.DateTime),
                    new SqlParameter("@CG_End_Dt",SqlDbType.DateTime),
                    new SqlParameter("@CG_Succ_Matrix", SqlDbType.NVarChar,80),
                    new SqlParameter("@CG_AD_OM_Code", SqlDbType.TinyInt,4),
                    new SqlParameter("@CG_RP_Code",SqlDbType.Int,4),
                    new SqlParameter("@PL_Code",SqlDbType.NVarChar),
                    new SqlParameter("@Pl_Val",SqlDbType.NVarChar),
                    new SqlParameter("@CG_UType",SqlDbType.Int,4),
                    new SqlParameter("@Share",SqlDbType.Bit),
                    new SqlParameter("@AU_Code",SqlDbType.Int,4),
                    new SqlParameter("@Active",SqlDbType.Int,4),
                    new SqlParameter("@textMethods",SqlDbType.Int,4),
        };
            parameters[0].Direction = ParameterDirection.Output;
            parameters[1].Value = intID;
            parameters[2].Value = strTitle;
            parameters[3].Value = strDesc;
            parameters[4].Value = intType;
            parameters[5].Value = intType_Time;
            parameters[6].Value = strMothod;
            parameters[7].Value = intWhom;
            parameters[8].Value = strFileNameAll;
            parameters[9].Value = dtSt;
            parameters[10].Value = dtEt;
            parameters[11].Value = intSucc;
            parameters[12].Value = UserSession.DealerEmpl.DE_AD_OM_Code;
            parameters[13].Value = intTargeted;
            parameters[14].Value = PL_Code;
            parameters[15].Value = PL_Val;
            parameters[16].Value = UserSession.User.UG_UType;
            parameters[17].Value = Convert.ToByte(Share);
            parameters[18].Value = UserSession.User.AU_Code;
            parameters[19].Value = Active;
            parameters[20].Value = textMethods;
            #endregion
            SqlConnection _c = new SqlConnection(Tools.GetConnString());
            _c.Open();
            SqlTransaction _t = _c.BeginTransaction();

            try
            {
                int i = SqlHelper.ExecuteNonQuery(_t, CommandType.StoredProcedure, "CT_Campaigns_Modify", parameters);
                //Succ的操作
                if (i > 0)
                {
                    int _id = intID;
                    SqlHelper.ExecuteNonQuery(_t, CommandType.Text,
                                          "delete from CT_SM_Values where SMV_Type=1 and SMV_CG_Code=" + _id + ";");
                    for (int j = 0; j < o.SuccMatrixList.Count; j++)
                    {
                        CT_Succ_Matrix _o = o.SuccMatrixList[j];
                        SqlHelper.ExecuteNonQuery(_t, CommandType.Text,
                                          "insert into CT_SM_Values values(" + _o.PSM_Code + ",1," + _id + "," + _o.SMV_Days + "," + _o.SMV_Val + ");");
                    }
                }
                _t.Commit();
                _c.Close();
            }
            catch { _t.Rollback(); _c.Close(); }
            Response.Write(parameters[0].Value.ToString());
        }
    }
    #endregion

    #region 删除 campaign
    private void CampaignDelete()
    {
        int intCG_Code = RequestClass.GetInt("id", 0);
        if (0 >= intCG_Code) Response.Write("-2");//参数 没有全填写
        else
        {
            SqlParameter[] parameters = {
                    new SqlParameter("@results", SqlDbType.Int,4),
					new SqlParameter("@CG_Code", SqlDbType.Int,4) };
            parameters[0].Direction = ParameterDirection.Output;
            parameters[1].Value = intCG_Code;
            SqlHelper.ExecuteNonQuery(Tools.GetConnString(), CommandType.StoredProcedure, "CT_Campaigns_Delete", parameters);           
            Response.Write(parameters[0].Value);
        }
    }
    #endregion 删除 campaign

    #region 删除 upload File 物理文件
    private void Delete_File()
    {
        string strFileName = RequestClass.GetString("fullname");
        string strPath = System.Configuration.ConfigurationManager.AppSettings["FileUploadPath"];
        if (1 < strFileName.Length)
        {
            string[] ArrFileName = strFileName.Split(',');
            for (int i = 0; i < ArrFileName.Length; i++)
            {
                string strFname = ArrFileName[i].ToString().Trim();
                if (0 < strFname.Length)
                {
                    try
                    {
                        //判断文件是不是存在
                        if (File.Exists(strPath + strFileName))
                        {
                            //如果存在则删除
                            File.Delete(strPath + strFileName);
                        }
                    }
                    catch (Exception)
                    {
                    }
                }

            }

        }
    }
    #endregion

    #region 编辑文件
    private void Campaign_Edit_file()
    {
       // var path = Server.MapPath("~/plupload/file/");
        string strFileName = RequestClass.GetString("fullname");
        strFileName = Server.MapPath(strFileName);
        if (strFileName.Length <= 0)
        {
            Response.Write("Empty file!");
        }
        else
        {
            try
            {
                //判断文件是不是存在
                if (File.Exists(strFileName))
                {
                    FileStream fs = new FileStream(strFileName, FileMode.Open);
                    StreamReader m_streamReader = new StreamReader(fs);
                    m_streamReader.BaseStream.Seek(0, SeekOrigin.Begin);// 从数据流中读取每一行，直到文件的最后一行
                    string strLine = m_streamReader.ReadToEnd();
                    m_streamReader.Dispose();
                    m_streamReader.Close();
                    Response.Write(strLine);
                }
            }
            catch 
            {
            }
        }
    }

    private void Campaign_Create_File()
    {
        string fileName = Guid.NewGuid().ToString();
        string Htmlvalue = "<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" />  <meta name=\"viewport\" content=\"width=device-width, initial-scale=1\" />";
        Htmlvalue += RequestClass.GetString("fileHtml");
        string Textvalue = RequestClass.GetString("fileText");
        Htmlvalue = Microsoft.JScript.GlobalObject.unescape(Htmlvalue).Replace("'", "").Trim();
        Textvalue = Microsoft.JScript.GlobalObject.unescape(Textvalue).Replace("'", "").Trim();
        string strPath = Server.MapPath("~/plupload/file_temp/");
        using (StreamWriter sw = new StreamWriter(strPath + fileName + ".html"))
        {
            sw.Write(Htmlvalue);
        }
        using (StreamWriter sw = new StreamWriter(strPath + fileName + ".txt"))
        {
            sw.Write(Textvalue);
        }
        Response.Write(JsonConvert.SerializeObject(new { fileName = fileName + ".html", isCreate = true }));
    }
    private void Campaign_Modify_File(string fileName)
    {
        string Htmlvalue = "<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" />  <meta name=\"viewport\" content=\"width=device-width, initial-scale=1\" />";
        Htmlvalue += RequestClass.GetString("fileHtml");
        string Textvalue = RequestClass.GetString("fileText");
        Htmlvalue = Microsoft.JScript.GlobalObject.unescape(Htmlvalue).Replace("'", "").Trim();
        Textvalue = Microsoft.JScript.GlobalObject.unescape(Textvalue).Replace("'", "").Trim();
        string fullPath = Server.MapPath(fileName);
        var lastIndex = fullPath.LastIndexOf('.');
        var len = fullPath.Length;
        var path = fullPath.Substring(0, len - (len - lastIndex));
        using (StreamWriter sw = new StreamWriter(fullPath))
        {
            sw.Write(Htmlvalue);
        }
        using (StreamWriter sw = new StreamWriter(path + ".txt"))
        {
            sw.Write(Textvalue);
        }
        Response.Write(JsonConvert.SerializeObject(new { fileName = fileName, isCreate = false }));
    }
    private void Campaign_Save_file()
    {
        try
        {
            string fileName = RequestClass.GetString("fileName");
            if (string.IsNullOrWhiteSpace(fileName))
            {
                Campaign_Create_File();
            }
            else
            {
                int t = 0;
                t= RequestClass.GetInt("T",t);
                if (t == 1)
                {
                    Campaign_Create_File();
                }
                else
                {
                    Campaign_Modify_File(fileName);
                }
            }
        }
        catch 
        {
            Response.Write(JsonConvert.SerializeObject(new { isOK = false }));
        }
    }
    private void Edit_file()
    {
        string strFileName = RequestClass.GetString("fullname");
        string strPath = System.Configuration.ConfigurationManager.AppSettings["FileUploadPath"];
        if (strFileName.Length <= 0)
        {
            Response.Write("Empty file!");
        }
        else
        {
            try
            {
                //判断文件是不是存在
                if (File.Exists(strPath + strFileName))
                {
                    FileStream fs = new FileStream(strPath + strFileName, FileMode.Open);
                    StreamReader m_streamReader = new StreamReader(fs);
                    m_streamReader.BaseStream.Seek(0, SeekOrigin.Begin);// 从数据流中读取每一行，直到文件的最后一行
                    string strLine = m_streamReader.ReadToEnd();
                    m_streamReader.Dispose();
                    m_streamReader.Close();
                    Response.Write(strLine);
                }
            }
            catch 
            {
            }
        }
    }
    private void Create_file()
    {
        string strFileName = DateTime.Now.ToString("yyMMddHHmmssffff");
        string Htmlvalue = "<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" />  <meta name=\"viewport\" content=\"width=device-width, initial-scale=1\" />";
        Htmlvalue += RequestClass.GetString("fileHtml");
        string Textvalue = RequestClass.GetString("fileText");
        Htmlvalue = Microsoft.JScript.GlobalObject.unescape(Htmlvalue).Replace("'", "").Trim();
        Textvalue = Microsoft.JScript.GlobalObject.unescape(Textvalue).Replace("'", "").Trim();
        string strPath = System.Configuration.ConfigurationManager.AppSettings["FileUploadPath"];
        try
        {
            using (StreamWriter sw = new StreamWriter(strPath + strFileName + ".html"))
            {
                sw.Write(Htmlvalue);
            }
            using (StreamWriter sw = new StreamWriter(strPath + strFileName + ".txt"))
            {
                sw.Write(Textvalue);
            }
            Response.Write(strFileName + ".html");
        }
        catch 
        {
            Response.Write(-1);
        }
    }
    private void Edits_file()
    {
        string strFileName = RequestClass.GetString("fileName");
        string Htmlvalue = "<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" /> <meta name='viewport' content='width=device-width, initial-scale=1' />";
        Htmlvalue += RequestClass.GetString("fileHtml");
        string Textvalue = RequestClass.GetString("fileText");
        Htmlvalue = Microsoft.JScript.GlobalObject.unescape(Htmlvalue).Replace("'", "").Trim();
        Textvalue = Microsoft.JScript.GlobalObject.unescape(Textvalue).Replace("'", "").Trim();
        string strPath = System.Configuration.ConfigurationManager.AppSettings["FileUploadPath"];
        try
        {
            using (StreamWriter sw = new StreamWriter(strPath + strFileName))
            {
                sw.Write(Htmlvalue);
            }
            using (StreamWriter sw = new StreamWriter(strPath + FileConversion(strFileName)))
            {
                sw.Write(Textvalue);
            }
            Response.Write(1);
        }
        catch 
        {
            Response.Write(-1);
        }
    }
    private string FileConversion(string fileName)
    {
        fileName = fileName.ToUpper();
        fileName = fileName.Replace(".HTML", ".txt");
        fileName = fileName.Replace(".HTM", ".txt");
        return fileName;
    }
    #endregion
}