using CRMTree.Public;
using Shinfotech.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using System.Data;
using CRMTreeDatabase;
using System.Dynamic;
using PetaPoco;
using System.IO;
using CRMTree.BLL;

public partial class handler_Reports_Reports : BasePage
{
    protected override void OnLoad(EventArgs e)
    {
        try
        {
            UserBuss buss = new UserBuss();
            bool CheckLogin = buss.CheckLogin("PublicUser", "");
            if (CheckLogin)
            {
                base.OnLoad(e);

                var o = Request.Params["o"];
                var data = JsonConvert.DeserializeObject<dynamic>(o);
                string acion = data.action;
                switch (acion)
                {
                    case "DataGrid":
                        DataGrid(data);
                        break;
                    case "ExecProc":
                        DoProc(data);
                        break;
                    case "Page":
                        DoPage(data);
                        break;
                    case "getReportData":
                        getReportData(data);
                        break;
                    case "GetWords":
                        GetWords(Convert.ToString(data.wordIds));
                        break;
                    case "GetWordsByIds":
                        GetWordsByIds(Convert.ToString(data.wordIds));
                        break;
                    case "GetWordsWithParent":
                        GetWordsWithParent(Convert.ToString(data.wordIds));
                        break;
                    case "GetWordsByID":
                        GetWordsByID(Convert.ToInt32(data.wordId));
                        break;
                    case "getWordByValue":
                        getWordByValue(Convert.ToInt32(data.pid), Convert.ToString(data.value));
                        break;
                    case "GetLenDonData":
                        GetLenDonData(data);
                        break;
                    case "Get_Main_Search":
                        Get_Main_Search(data);
                        break;
                    case "Get_Grid_Window":
                        Get_Grid_Window(Convert.ToInt32(data.gw_code));
                        break;
                    case "Get_Tab_Links":
                        Get_Tab_Links(Convert.ToInt32(data.TL_Code));
                        break;
                    case "Get_Auth_Tab_Links":
                        Get_Auth_Tab_Links();
                        break;
                    case "Get_Help_Documents_Search":
                        Get_Help_Documents_Search(Convert.ToString(data.HD_Key_Words));
                        break;
                    case "Remove_App":
                        Remove_App(data);
                        break;
                    case "ExecActive":
                        ExecActive(data);
                        break;
                    case "/templete/report/AsscManager.ASPX?TI=Per&Action=D":
                        del(data);
                        break;
                    case "/templete/usercontrol/CarinfoManager.aspx?AC=D":
                        delCar(data);
                        break;
                    default:
                        break;
                }
            }
        }
        catch (Exception ex)
        {
            Response.Write(JsonConvert.SerializeObject(new { isOK = false, msg = ex.Message }));
        }
    }

    private void delCar(dynamic data)
    {
        BL_MyCar bllcar = new BL_MyCar();
        if (bllcar.DeleteCar(Convert.ToInt32(data.o.CI), Convert.ToInt32(data.o.AU)))
        {
            Response.Write(JsonConvert.SerializeObject(new { isOK = true }));
        }
    }
    private void del(dynamic data)
    {
        BL_MyCar bllcar = new BL_MyCar();
        if (bllcar.Delete(Convert.ToInt32(data.o.AU), Convert.ToInt32(data.o.MAU)))
        {
            Response.Write(JsonConvert.SerializeObject(new { isOK = true }));
        }
    }

    private void ExecActive(dynamic data)
    {
        var db = DBCRMTree.GetInstance();


        Response.Write(JsonConvert.SerializeObject(new { isOK = true }));
    }

    private void Remove_App(dynamic data)
    {
        var s_o = JsonConvert.SerializeObject(data.o);
        CT_Appt_Service ap = JsonConvert.DeserializeObject<CT_Appt_Service>(s_o);
        ap.AP_Updated_by = UserSession.User.AU_Code;
        ap.AP_Update_Dt = DateTime.Now;
        ap.AP_Canceled = true;
        var rows = ap.Update(new string[] { 
            "AP_Canceled",
            "AP_Updated_by",
            "AP_Update_Dt"
        });

        Response.Write(JsonConvert.SerializeObject(new { isOK = rows > 0 }));
    }

    private void Get_Tab_Links(int TL_Code)
    {
        var db = DBCRMTree.GetInstance();
        var o = db.Query<dynamic>(";exec P_Get_Tab_Links @TL_Code,@IsEn"
            , new { TL_Code = TL_Code, IsEn = Interna }
            );
        string _o = JsonConvert.SerializeObject(o);
        Response.Write(_o);
    }
    private void Get_Help_Documents_Search(string HD_Key_Words)
    {
        var db = DBCRMTree.GetInstance();
        var o = db.Query<dynamic>(";exec P_Get_Help_Documents_Search @AU_Code,@HD_Key_Words,@IsEn"
            , new
            {
                AU_Code = UserSession.User.AU_Code,
                HD_Key_Words = HD_Key_Words,
                IsEn = Interna
            }
            );

        Response.Write(JsonConvert.SerializeObject(o));
    }
    private void Get_Auth_Tab_Links()
    {
        var db = DBCRMTree.GetInstance();
        var o = db.Query<dynamic>(";exec P_Get_Auth_Tab_Links @AU_Code,@IsEn"
            , new { AU_Code = UserSession.User.AU_Code, IsEn = Interna }
            );

        Response.Write(JsonConvert.SerializeObject(o));
    }

    private void Get_Main_Search(dynamic data)
    {
        var q = (string)data.q;
        var type = (int)data.type;
        var rp_code = type == 1 ? 47 : 46;
        CRMTree.Function fun = new CRMTree.Function();
        var sql = fun.getReprotQuery(rp_code);

        var db = DBCRMTree.GetInstance();
        var items = db.Query<dynamic>(sql, q);
        Response.Write(JsonConvert.SerializeObject(items));
    }

    /// <summary>    /// 根据词语编号获得下拉列表数据
    /// </summary>
    /// <param name="wordIds">1,2,3</param>
    private void GetWords(string wordIds)
    {
        if (!string.IsNullOrWhiteSpace(wordIds))
        {
            //check
            string[] s_ids = wordIds.Split(',');
            foreach (var id in s_ids)
            {
                Convert.ToInt32(id);
            }

            var db = DBCRMTree.GetInstance();
            var sql_text_part = Interna ? "[text_en]" : "[text_cn]";
            var sql_MC_Name_part = Interna ? "[MC_Name_EN]" : "[MC_Name_CN]";
            var sql = string.Format(@"SELECT p_id ,{0} as [text],[value],[isSelect] AS selected,sort FROM [words]
                                      WHERE p_id IN({1}) AND p_id <> 4064 
                                    UNION
                                    SELECT '4064' AS p_id,{2} AS [text],MC.MC_Code AS [value] ,'true' as selected,MC.MC_Code AS sort FROM CT_Messaging_Carriers MC
                                    ORDER BY p_id,sort
                ", sql_text_part, wordIds, sql_MC_Name_part);

            var o = db.Query<dynamic>(sql);
            Response.Write(JsonConvert.SerializeObject(o));
        }
    }

    private void GetWordsByIds(string wordIds)
    {
        if (!string.IsNullOrWhiteSpace(wordIds))
        {
            //check
            string[] s_ids = wordIds.Split(',');
            foreach (var id in s_ids)
            {
                Convert.ToInt32(id);
            }

            var db = DBCRMTree.GetInstance();
            var sql_text_part = Interna ? "[text_en]" : "[text_cn]";
            var sql = string.Format(@"SELECT 
                [id]
                ,[p_id]
                ,{0} as [text]
                ,[value]  as [value]
                ,[isSelect] as selected
                FROM [words]
                WHERE id IN({1})
                order by sort
                ", sql_text_part, wordIds);

            var o = db.Query<dynamic>(sql);
            Response.Write(JsonConvert.SerializeObject(o));
        }
    }

    private void GetWordsWithParent(string wordIds)
    {
        if (!string.IsNullOrWhiteSpace(wordIds))
        {
            //check
            string[] s_ids = wordIds.Split(',');
            foreach (var id in s_ids)
            {
                Convert.ToInt32(id);
            }

            var db = DBCRMTree.GetInstance();
            var sql_text_part = Interna ? "[text_en]" : "[text_cn]";
            var sql = string.Format(@"SELECT 
                [id]
                ,[p_id]
                ,{0} as [text]
                ,[value] --,[id] as [value]
                ,[isSelect] as selected
                FROM [words]
                WHERE id in({1}) or p_id IN({1})
                order by p_id,sort
                ", sql_text_part, wordIds);

            var o = db.Query<dynamic>(sql);
            Response.Write(JsonConvert.SerializeObject(o));
        }
    }
    private void getWordByValue(int pid, string value)
    {
        var db = DBCRMTree.GetInstance();
        var sql = string.Format(@"SELECT [p_id],{0} as [text]
            ,[value] --,[id] as [value]
            ,[isSelect] as selected
            FROM [words]
            WHERE p_id = @0 and value=@1
            ", Interna ? "[text_en]" : "[text_cn]");

        var o = db.SingleOrDefault<dynamic>(sql, pid, value);
        Response.Write(JsonConvert.SerializeObject(o));
    }
    private void GetWordsByID(int wordId)
    {
        var db = DBCRMTree.GetInstance();
        var sql = string.Format(@"SELECT [p_id],{0} as [text]
            ,[value] --,[id] as [value]
            ,[isSelect] as selected
            FROM [words]
            WHERE id = @0
            ", Interna ? "[text_en]" : "[text_cn]");

        var o = db.SingleOrDefault<dynamic>(sql, wordId);
        Response.Write(JsonConvert.SerializeObject(o));
    }

    private void GetLenDonData(dynamic data)
    {
        try
        {
            var sql_query = string.Empty;
            CRMTree.Function fun = new CRMTree.Function();
            sql_query = fun.getReprotQuery((int)data.rp_code);
            var db = DBCRMTree.GetInstance();
            var o = db.Query<dynamic>(sql_query, (int)data.up_code);
            Response.Write(JsonConvert.SerializeObject(o));
        }
        catch (Exception)
        {
            throw new Exception(Interna ? "Get the query exception！" : "获得查询语句异常！");
        }
    }

    private void Get_Grid_Window(int gw_code)
    {
        if (gw_code > 0)
        {
            var db = DBCRMTree.GetInstance();
            var sql = string.Format(@"SELECT {0} AS [title]
                ,[GW_Width] AS [width]
                ,[GW_Height] AS [height]
                ,[GW_X] AS [left]
                ,[GW_Y] AS [top]
                 FROM [CT_Grid_Window]
                WHERE GW_Code = @0
                ", Interna ? "[GW_Title_EN]" : "[GW_Title_CN]");
            var o = db.Query<dynamic>(sql, gw_code);
            Response.Write(JsonConvert.SerializeObject(o));
        }
    }

    /// <summary>
    /// 列表配置
    /// </summary>
    /// <param name="data">查询条件</param>
    private void DataGrid(dynamic data)
    {
        dynamic o = new ExpandoObject();
        o._winID = Guid.NewGuid();

        var MF_FL_FB_Code = -1;
        if (data.MF_FL_FB_Code != null)
        {
            MF_FL_FB_Code = (int)data.MF_FL_FB_Code;
        }
        var db = DBCRMTree.GetInstance();
        o.options = CT_Fields_list.SingleOrDefault(MF_FL_FB_Code);
        //标题
        if (o.options.FL_Type == 2)
        {
            o.columns = db.Query<dynamic>(string.Format(@"
            SELECT [FN_FieldName] as [title]
            ,[FN_FieldName] as [field]
            ,case when [FN_Type] = 2 or [FN_Type] = 12 then 1 else 0 end as [sortable]
            ,[FN_Width] as [width]
            ,[FN_Format]
            ,[FN_Font]
            ,'left' as halign
            ,(case when isnull(FN_Option,1) = 2 then 'center' when isnull(FN_Option,1) = 3 then 'right' else 'left' end) as align,FN_Type
            FROM [CT_Fields_Names]
            where [FN_FL_FB_Code] = @0
            order by fn_order", Interna ? "[FN_Desc_EN]" : "[FN_Desc_CN]"), MF_FL_FB_Code);

        }
        else
        {
            o.columns = db.Query<dynamic>(string.Format(@"
            SELECT {0} as [title]
            ,[FN_FieldName] as [field]
            ,case when [FN_Type] = 2 or [FN_Type] = 12 then 1 else 0 end as [sortable]
            ,[FN_Width] as [width]
            ,[FN_Format]
            ,[FN_Font]
            ,'left' as halign
            ,(case when isnull(FN_Option,1) = 2 then 'center' when isnull(FN_Option,1) = 3 then 'right' else 'left' end) as align,FN_Type
            FROM [CT_Fields_Names]
            where [FN_FL_FB_Code] = @0
            order by fn_order", Interna ? "[FN_Desc_EN]" : "[FN_Desc_CN]"), MF_FL_FB_Code);
        }
        if (o.options.FL_Type >= 10)  //Survey Report.  Find out how many columns there are 
        {
            o.FN_Type50 = db.Query<dynamic>(string.Format(@"
                select max(sq.cnt) as Max_Col from 
                (
                select SQ_CG_Code, count(SQ_Code) as cnt from CT_Survey_Questions 
                  left join CT_Campaigns on CG_Code=SQ_CG_Code
                 where CG_AD_OM_Code={0} and CG_UType=1 and SQ_SF_Code>=50
                 group by SQ_CG_Code
                 ) SQ", UserSession.Dealer.AD_Code));
        }

        //btns 
        if (data.NB == null || (int)data.NB != 1)
        {
            o.buttons = GetButtons(MF_FL_FB_Code);
        }

        //search
        o.search = GetSearch(MF_FL_FB_Code);

        var dataGridInfo = JsonConvert.SerializeObject(o);
        Response.Write(dataGridInfo);
    }

    private IEnumerable<dynamic> GetSearch(int MF_FL_FB_Code)
    {
        var db = DBCRMTree.GetInstance();
        var searchs = db.Fetch<CT_Frame_Sel_Row>(@"SELECT  FS_Code ,
        FS_FL_Code ,
        FS_Type ,
        FS_Param ,
        FS_Title_EN,
        FS_Title_CN,
        FS_RP_Code ,
        FS_Default ,
        FS_Order ,
        FS_Class ,
        FS_Option ,
        FS_Style ,
        FS_DataType FROM CT_Frame_Sel_Row
        where FS_FL_Code = @0 AND ISNULL(FS_ShowFlag,1) = 1
        order by FS_Order", MF_FL_FB_Code);

        if (searchs.Count() > 0)
        {

            var sql_query = string.Empty;
            CRMTree.Function fun = new CRMTree.Function();

            foreach (CT_Frame_Sel_Row search in searchs)
            {
                try
                {
                    if (search.FS_RP_Code > 0)
                    {
                        sql_query = fun.getReprotQuery((int)search.FS_RP_Code);
                        search.EX_Data = db.Query<dynamic>(sql_query);
                    }
                }
                catch (Exception)
                {
                    throw new Exception(Interna ? "Get the query exception！" : "获得查询语句异常！");
                }
            }
        }
        return searchs;
    }

    /// <summary>
    ///  根据存储过程替换参数，并执行查询
    /// </summary>
    /// <param name="data">条件</param>
    private void DoProc(dynamic data)
    {
        var db = DBCRMTree.GetInstance();

        object queryParams = data.queryParams;
        var MF_FL_FB_Code = (int)data.MF_FL_FB_Code;

        var rp_code = BL_Reports.GetReportCode(MF_FL_FB_Code);
        var sql = BL_Reports.GetReportSql(rp_code, queryParams);

        var items = db.Query<dynamic>(sql);
        var pageInfo = JsonConvert.SerializeObject(new { total = 0, rows = items });
        Response.Write(pageInfo);
    }

    /// <summary>
    /// 分页
    /// </summary>
    /// <param name="data">分页条件</param>
    private void DoPage(dynamic data)
    {
        Page<dynamic> pager = new Page<dynamic>();
        pager.Items = new List<dynamic>();

        var db = DBCRMTree.GetInstance();

        object queryParams = data.queryParams;
        var MF_FL_FB_Code = (int)data.MF_FL_FB_Code;

        var rp_code = BL_Reports.GetReportCode(MF_FL_FB_Code);
        var sql = BL_Reports.GetReportSql(rp_code, queryParams);

        var dataInfo = string.Empty;
        if (sql.SQL.IndexOf(";Exec", StringComparison.OrdinalIgnoreCase) >= 0)
        {
            var qData = db.Query<dynamic>(sql);
            dataInfo = JsonConvert.SerializeObject(new { total = qData.Count(), rows = qData });
        }
        else
        {
            var pSql = PetaPoco.Sql.Builder;
            var sort = (string)data.sort;
            var order = (string)data.order;

            pSql.Append("select * from(" + sql.SQL, sql.Arguments);
            pSql.Append(") as dt");
            if (!string.IsNullOrWhiteSpace(sort))
            {
                pSql.Append(string.Format(" order by {0} {1}", sort, order));
            }

            pager = db.Page<dynamic>((long)data.pageNumber, (long)data.pageSize, pSql);
            //pager.Items[0].Results = "<div style=\"width:100%;height:100%\">100<span class=\"SType50\" />75<span class=\"SType50\" />45</div>";           

            dataInfo = JsonConvert.SerializeObject(new { total = pager.TotalItems, rows = pager.Items });
        }


        Response.Write(dataInfo);
    }

    private void getReportData(dynamic data)
    {
        var db = DBCRMTree.GetInstance();
        var RP_Code = (int)data.RP_Code;
        object queryParams = data.queryParams;

        var sql = BL_Reports.GetReportSql(RP_Code, queryParams);

        var o = db.Query<dynamic>(sql);
        Response.Write(JsonConvert.SerializeObject(o));
    }

    /// <summary>
    /// 获得列表所有的按钮
    /// </summary>
    /// <param name="fl_code">列表编号</param>
    /// <returns>该列表所有的按钮</returns>
    private IEnumerable<dynamic> GetButtons(int FL_Code)
    {
        var db = DBCRMTree.GetInstance();

        return db.Query<dynamic>(string.Format(@"
        Select 
        CASE WHEN BF_Func = 0 THEN 1 
        WHEN BF_Func = 1 AND (UP_Create IS NULL OR UP_Create = 1) THEN 1
        WHEN BF_Func = 2 AND (UP_Delete IS NULL OR UP_Delete = 1) THEN 1
        WHEN BF_Func = 3 AND (UP_Copy IS NULL OR UP_Copy = 1) THEN 1
        WHEN BF_Func = 4 AND (UP_Edit IS NULL OR UP_Edit = 1) THEN 1
        WHEN BF_Func = 5 AND (UP_Print IS NULL OR UP_Print = 1) THEN 1
        WHEN BF_Func = 6 AND (UP_View IS NULL OR UP_View = 1) THEN 1
        WHEN BF_Func = 7 AND (UP_Activate IS NULL OR UP_Activate = 1) THEN 1
        WHEN BF_Func = 8 AND (UP_Export IS NULL OR UP_Export = 1) THEN 1
        ELSE 0 END AS b_visible,
        UP_Run,
        BF_Param, 
        BF_Func ,
        BF_Type ,
        BF_Target,
        BF_Code,
        {0} as text,
        {1} as tip,
        BF_UG_Code ,
        BF_Icon ,BF_Order ,
        BF_ShowTxt,
        BF_Link,UP_UType ,
        UP_RP_MI_Code,
        {2} as GW_Title,
        GW_Width ,
        GW_Height ,
        GW_X ,
        GW_Y
        From CT_Fields_lists
        left Join CT_Reports ON RP_Code = FL_RP_Code
        left Join CT_List_Btns_Func on BF_FL_Code = FL_Code
        Left Join CT_Users_Privileges on UP_UG_Code = RP_UG_Code
        left join CT_Grid_Window ON GW_Code = BF_GW_Code
        Where FL_Code = @0
        ORDER BY BF_Order",
        Interna ? "[BF_Name_EN]" : "[BF_Name_CN]",
        Interna ? "[BF_Ver_Mess_EN]" : "[BF_Ver_Mess_CN]",
        Interna ? "[GW_Title_EN]" : "[GW_Title_CN]"
        ), FL_Code);
    }
}