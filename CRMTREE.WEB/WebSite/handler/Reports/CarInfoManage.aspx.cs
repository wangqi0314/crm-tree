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
using CRMTree.Model.User;
using ShInfoTech.Common;
using CRMTree.BLL.Wechat;
using System.Configuration;
using CRMTree.BLL;

public partial class handler_Reports_CarInfoManage : BasePage
{
    protected override void OnLoad(EventArgs e)
    {
        try
        {
            base.OnLoad(e);

            var o = Request.Params["o"];
            var data = JsonConvert.DeserializeObject<dynamic>(o);
            string acion = data.action;
            switch (acion)
            {

                case "Save_Car":
                    Save_Car(data);
                    break;
                case "Get_CarInfo":
                    Get_CarInfo(data);
                    break;
                case "Get_Car_InventoryAndLendon":
                    Get_Car_InventoryAndLendon(Convert.ToInt32(data.id));
                    break;
                case "Get_Car_Selects":
                    Get_Car_Selects();
                    break;
                case "Get_Years":
                    Get_Years();
                    break;
                case "Get_Make":
                    Get_Make();
                    break;
                case "Get_Car_Model_All":
                    Get_Car_Model_All(Convert.ToInt32(data.id));
                    break;
                case "Get_Car_Style_All":
                    Get_Car_Style_All(Convert.ToInt32(data.id));
                    break;
                case "Get_Car_Model":
                    Get_Car_Model(Convert.ToInt32(data.id), Convert.ToInt32(data.id_year));
                    break;
                case "Get_Car_Style":
                    Get_Car_Style(Convert.ToInt32(data.id), Convert.ToInt32(data.id_year));
                    break;
                case "Get_Insurance_Agents":
                    Get_Insurance_Agents(Convert.ToInt32(data.id));
                    break;
                case "Get_Car_Lendon":
                    Get_Car_Lendon(Convert.ToInt32(data.id), Convert.ToInt32(data.id_year));
                    break;
                case "Get_Recursion_By_CS_Code":
                    Get_Recursion_By_CS_Code(Convert.ToInt32(data.id));
                    break;
                case "Get_Recursion_By_CM_Code":
                    Get_Recursion_By_CM_Code(Convert.ToInt32(data.id));
                    break;
            }
        }
        catch (Exception ex)
        {
            //Response.Write(JsonConvert.SerializeObject(new { isOK = false, msg = ex.Message }));
            throw new Exception(JsonConvert.SerializeObject(new { isOK = false, msg = ex.Message }));
        }
    }

    private void Get_Recursion_By_CM_Code(int id)
    {
        var db = DBCRMTree.GetInstance();

        var o = db.SingleOrDefault<dynamic>(@"SELECT top 1
        YR_Code,
        CM_MK_Code AS MK_Code,
        CM_Code
        FROM CT_Car_Model
        LEFT JOIN CT_Car_Style
        ON CS_CM_Code = CM_Code
        LEFT JOIN CT_Years 
        ON YR_Year = CS_Year
        WHERE CM_Code=@0", id);

        Response.Write(JsonConvert.SerializeObject(o));
    }

    private void Get_Recursion_By_CS_Code(int id)
    {
        var db = DBCRMTree.GetInstance();

        var o = db.SingleOrDefault<dynamic>(@"SELECT top 1
        YR_Code,
        CM_MK_Code AS MK_Code,
        CS_CM_Code AS CM_Code,
        CS_Code
        FROM CT_Car_Style
        LEFT JOIN CT_Car_Model
        ON CM_Code=CS_CM_Code
        LEFT JOIN CT_Years 
        ON YR_Year = CS_Year
        WHERE CS_Code=@0", id);

        Response.Write(JsonConvert.SerializeObject(o));
    }

    #region 保存客户信息

    /// <summary>
    /// 保存客户信息
    /// </summary>
    /// <param name="data"></param>
    private void Save_Cars(dynamic data)
    {
        var db = DBCRMTree.GetInstance();
        try
        {
            var au_code = (int)data.au_code;
            var CI_Code = (int)data.o.CI_Code;
            Save_Cars(data, au_code, CI_Code);
            using (var tran = db.GetTransaction())
            {
                if (au_code > 0)
                {
                    int Car_Code = Get_User_Only_Car(db, au_code);

                    Save_Cars(data, au_code, CI_Code);
                }
                tran.Complete();
            }
            Response.Write(JsonConvert.SerializeObject(new { isOK = true }));
        }
        catch (Exception ex)
        {
            db.AbortTransaction();
            Response.Write(JsonConvert.SerializeObject(new { isOK = false, msg = ex.Message }));
        }
    }


    private int Get_User_Only_Car(DBCRMTree db, long au_code)
    {
        string sql = "select top 1 CI_Code from CT_Car_Inventory where CI_AU_Code=@0;";

        dynamic car = db.Query<dynamic>(sql, au_code);
        dynamic _car = JsonConvert.DeserializeObject<dynamic>(JsonConvert.SerializeObject(car));
        string _CI = JsonConvert.SerializeObject(_car[0].CI_Code);
        return Convert.ToInt32(_CI);
    }




    /// <summary>
    /// 保存车信息
    /// </summary>
    /// <param name="data"></param>
    /// <param name="au_code"></param>
    private void Save_Car(dynamic data)
    {
        var db = DBCRMTree.GetInstance();
        try
        {
            using (var tran = db.GetTransaction())
            {
                var au_code = (long)data.au_code;
                var s = JsonConvert.SerializeObject(data.o);
                CT_Car_Inventory c = JsonConvert.DeserializeObject<CT_Car_Inventory>(s);
                var ci_code = c.CI_Code;
                c.CI_AU_Code = au_code;
                c.CI_Create_dt = DateTime.Now;
                if (c.CI_Code > 0)
                {
                    c.Update(new string[]{
                                "CI_AU_Code"
                                ,"CI_Create_dt"
                                ,"CI_CS_Code"
                                ,"CI_VIN"
                                ,"CI_Mileage"
                                ,"CI_Licence"
                                ,"CI_YR_Code"
                                ,"CI_Color_I"
                                ,"CI_Color_E"
                            });
                }
                else
                {
                    ci_code = (int)c.Insert();
                }

                CT_Auto_Insurance ai = JsonConvert.DeserializeObject<CT_Auto_Insurance>(s);
                ai.AI_CI_Code = c.CI_Code;
                if (db.Exists<CT_Auto_Insurance>("AI_CI_Code=@0", c.CI_Code))
                {
                    ai.AI_Update_dt = DateTime.Now;
                    string sql = @"UPDATE CT_Auto_Insurance
                    SET AI_IC_Code = @AI_IC_Code
                    ,AI_IA_Code = @AI_IA_Code
                    ,AI_Policy = @AI_Policy
                    ,AI_End_dt = @AI_End_dt
                    ,AI_Update_dt = @AI_Update_dt
                    WHERE AI_CI_Code=@AI_CI_Code";
                    db.Execute(sql, ai);
                }
                else
                {
                    ai.Insert();
                }

                var path = System.Configuration.ConfigurationManager.AppSettings["PLUploadPath"];
                if (string.IsNullOrWhiteSpace(path))
                {
                    path = "~/plupload/";
                }
                path = Server.MapPath(path);
                var path_save = path + "customer/";
                var path_temp = path + "customer_temp/";
                //添加图片
                if (!string.IsNullOrWhiteSpace(c.CP_Picture_FN))
                {
                    string[] imgs = c.CP_Picture_FN.Split(',');
                    foreach (var img in imgs)
                    {
                        if (!string.IsNullOrWhiteSpace(img))
                        {
                            var _CT_Car_Picture = new CT_Car_Picture();
                            _CT_Car_Picture.CP_CI_Code = ci_code;
                            _CT_Car_Picture.CP_Picture_FN = img;
                            _CT_Car_Picture.CP_Update_dt = DateTime.Now;
                            _CT_Car_Picture.Insert();
                            if (!Directory.Exists(path_save))
                            {
                                Directory.CreateDirectory(path_save);
                            }
                            if (File.Exists(path_temp + img))
                            {
                                File.Move(path_temp + img, path_save + img);
                            }
                        }
                    }
                }
                if (!string.IsNullOrWhiteSpace(c.Picture_Removed))
                {
                    string[] imgs_removed = c.Picture_Removed.Split(',');
                    foreach (var img in imgs_removed)
                    {
                        CT_Car_Picture.Delete("where [CP_Picture_FN]=@0", img);
                        var imgPath = path_save + img;
                        if (File.Exists(imgPath))
                        {
                            File.Delete(imgPath);
                        }
                    }
                }
                tran.Complete();
                Response.Write(JsonConvert.SerializeObject(new { CI_Code = ci_code, isOK = true }));
            }
        }
        catch (Exception ex)
        {
            db.AbortTransaction();
            Response.Write(JsonConvert.SerializeObject(new { isOK = false, msg = ex.Message }));
        }
    }
    private void Save_Cars(dynamic data, long au_code, long CI_Code)
    {
        var s_cars = JsonConvert.SerializeObject(data.o);
        List<CT_Car_Inventory> cars = JsonConvert.DeserializeObject<List<CT_Car_Inventory>>(s_cars);
        List<CT_Auto_Insurance> insurances = JsonConvert.DeserializeObject<List<CT_Auto_Insurance>>(s_cars);
        int i = 0;
        foreach (var c in cars)
        {
            var ci_code = c.CI_Code;
            c.CI_AU_Code = au_code;
            c.CI_Create_dt = DateTime.Now;

            var ai = insurances[i];
            if (c.CI_Code > 0)
            {
                c.Update(new string[]{
                    "CI_AU_Code"
                    ,"CI_Create_dt"
                    ,"CI_CS_Code"
                    ,"CI_VIN"
                    ,"CI_Mileage"
                    ,"CI_Licence"
                    ,"CI_YR_Code"
                    ,"CI_Color_I"
                    ,"CI_Color_E"
                    //,"CI_Picture_FN"
                });

                var db = DBCRMTree.GetInstance();
                ai.AI_CI_Code = c.CI_Code;
                if (db.Exists<CT_Auto_Insurance>("AI_CI_Code=@0", c.CI_Code))
                {
                    ai.AI_Update_dt = DateTime.Now;
                    string sql = @"UPDATE CT_Auto_Insurance
                    SET AI_IC_Code = @AI_IC_Code
                    ,AI_IA_Code = @AI_IA_Code
                    ,AI_Policy = @AI_Policy
                    ,AI_End_dt = @AI_End_dt
                    ,AI_Update_dt = @AI_Update_dt
                    WHERE AI_CI_Code=@AI_CI_Code";
                    db.Execute(sql, ai);
                }
                else
                {
                    ai.Insert();
                }
            }
            else
            {
                ci_code = (int)c.Insert();
                ai.AI_CI_Code = ci_code;
                ai.Insert();
            }
            i++;


            var path = System.Configuration.ConfigurationManager.AppSettings["PLUploadPath"];
            if (string.IsNullOrWhiteSpace(path))
            {
                path = "~/plupload/";
            }
            path = Server.MapPath(path);
            var path_save = path + "customer/";
            var path_temp = path + "customer_temp/";
            //添加图片
            if (!string.IsNullOrWhiteSpace(c.CP_Picture_FN))
            {
                string[] imgs = c.CP_Picture_FN.Split(',');
                foreach (var img in imgs)
                {
                    if (!string.IsNullOrWhiteSpace(img))
                    {
                        var _CT_Car_Picture = new CT_Car_Picture();
                        _CT_Car_Picture.CP_CI_Code = ci_code;
                        _CT_Car_Picture.CP_Picture_FN = img;
                        _CT_Car_Picture.CP_Update_dt = DateTime.Now;
                        _CT_Car_Picture.Insert();
                        if (!Directory.Exists(path_save))
                        {
                            Directory.CreateDirectory(path_save);
                        }
                        if (File.Exists(path_temp + img))
                        {
                            File.Move(path_temp + img, path_save + img);
                        }
                    }
                }
            }
            //删除图片
            if (!string.IsNullOrWhiteSpace(c.Picture_Removed))
            {
                string[] imgs_removed = c.Picture_Removed.Split(',');
                foreach (var img in imgs_removed)
                {
                    CT_Car_Picture.Delete("where [CP_Picture_FN]=@0", img);
                    var imgPath = path_save + img;
                    if (File.Exists(imgPath))
                    {
                        File.Delete(imgPath);
                    }
                }
            }
        }

        var s_delete_cars = JsonConvert.SerializeObject(data.cars.deletes);
        List<CT_Car_Inventory> delete_cars = JsonConvert.DeserializeObject<List<CT_Car_Inventory>>(s_delete_cars);
        foreach (var c in delete_cars)
        {
            c.CI_Update_dt = DateTime.Now;
            c.CI_Activate_Tag = 0;
            c.Update(new string[]{
                "CI_Update_dt",
                "CI_Activate_Tag"
            });
        }
    }
    #endregion

    /// <summary>
    /// 获得客户信息
    /// </summary>
    /// <param name="data"></param>
    private void Get_CarInfo(dynamic data)
    {
        dynamic o = new ExpandoObject();
        int AU_Code = (int)data.AU_Code;
        if (AU_Code > 0)
        {
            #region cars
            var db = DBCRMTree.GetInstance();
            o.cars = db.Query<dynamic>(
                        ";exec [CT_GetCustomerCars] @User_code,@IsEn"
                        , new { User_code = AU_Code, IsEn = Interna }
                        );
            #endregion
        }

        Response.Write(JsonConvert.SerializeObject(o));
    }


  
    /// <summary>
    /// 车信息和联动列表
    /// </summary>
    /// <param name="id"></param>
    /// <param name="CI_CS_Code"></param>
    private void Get_Car_InventoryAndLendon(int id)
    {
        dynamic d = new ExpandoObject();
        var db = DBCRMTree.GetInstance();

        d.CT_Car_Inventory = CT_Car_Inventory.SingleOrDefault(id);

        d.CT_Car_Style = CT_Car_Style.SingleOrDefault(d.CT_Car_Inventory.CI_CS_Code);
            //Get_CT_Car_Style(d.CT_Car_Inventory.CM_Code, d.CT_Car_Inventory.CI_YR_Code);

        d.CT_Car_Inventory.CI_YR_Code = db.ExecuteScalar<Byte>(@"select YR_Code from [CT_Years]
where YR_Year = @0", d.CT_Car_Style.CS_Year);
        
        d.CT_Car_Inventory.CM_Code = d.CT_Car_Style.CS_CM_Code;
            // db.ExecuteScalar<int>(@"select cs_cm_code from [CT_Car_Style] where cs_code = @0", d.CT_Car_Inventory.CI_CS_Code);

        d.CT_Car_Inventory.MK_Code = db.ExecuteScalar<int>(@"select cm_mk_code from [CT_Car_Model]
where cm_code = @0", d.CT_Car_Inventory.CM_Code);

        d.CT_Car_Inventory.CP_Picture_FN = db.ExecuteScalar<string>(@"	SELECT [CP_Picture_FN] + ',' FROM [CT_Car_Pictures] 
	WHERE CP_CI_Code =@0 FOR XML PATH('')",d.CT_Car_Inventory.CI_Code);

        d.CT_Car_Model = Get_CT_Car_Model(d.CT_Car_Inventory.MK_Code, d.CT_Car_Inventory.CI_YR_Code);

        d.CT_Car_Style = Get_CT_Car_Style(d.CT_Car_Inventory.CM_Code, d.CT_Car_Inventory.CI_YR_Code);

        d.CT_Auto_Insurance = CT_Auto_Insurance.FirstOrDefault("where AI_CI_Code = @0 order by AI_Update_dt desc", d.CT_Car_Inventory.CI_Code);
        //        d.CT_Auto_Insurance = CT_Auto_Insurance.SingleOrDefault(@"Select TOP 1 
        //              AI_Code,AI_CI_Code,AI_IC_Code,AI_IA_Code,AI_Policy,AI_Start_dt,AI_End_dt,AI_Update_dt
        //           from CT_Auto_Insurance where AI_CI_Code = @0 order by AI_Update_dt desc", d.CT_Car_Inventory.CI_Code);
        string sql_carInfo=string.Format(@"SELECT dbo.Get_Connect_Car(CI.CI_Code,1)AS CAR_CN,
	                    dbo.Get_Connect_Car(CI.CI_Code,2)AS CAR_EN,MK_Code,CM_Code,CS_Code,CI.* FROM CT_Make MK
                        INNER JOIN CT_Car_Model CM ON CM.CM_MK_Code=MK.MK_Code
                        INNER JOIN CT_Car_Style CS ON CS.CS_CM_Code=CM.CM_Code
                        INNER JOIN CT_Car_Inventory CI ON CI.CI_CS_Code=CS.CS_Code
                        WHERE CI.CI_Code=@0");
        d.carInfo = db.Query<dynamic>(sql_carInfo, id);
        Response.Write(JsonConvert.SerializeObject(d));
    }

    /// <summary>
    /// 车联动列表
    /// </summary>
    /// <param name="CI_CS_Code"></param>
    private void Get_Car_Lendon(int CI_CS_Code, int CI_YR_Code)
    {
        dynamic d = new ExpandoObject();
        var db = DBCRMTree.GetInstance();

        var CM_Code = db.ExecuteScalar<int>(@"select cs_cm_code from [CT_Car_Style]
where cs_code = @0", CI_CS_Code);
        d.CT_Car_Style = Get_CT_Car_Style(CM_Code, CI_YR_Code);

        var MK_Code = db.ExecuteScalar<int>(@"select cm_mk_code from [CT_Car_Model]
where cm_code = @0", CM_Code);
        d.CT_Car_Model = Get_CT_Car_Model(MK_Code, d.CT_Car_Style.CS_Year);

        //获得图片

        Response.Write(JsonConvert.SerializeObject(d));
    }

    /// <summary>
    /// 车表单列表
    /// </summary>
    private void Get_Car_Selects()
    {
        dynamic o = new ExpandoObject();

        var ad_code = null != UserSession.Dealer ? UserSession.Dealer.AD_Code : 0;
        if (ad_code > 0)
        {
            var ad = CT_Auto_Dealer.SingleOrDefault(ad_code);
            o.AD_MK_Code = ad.AD_MK_Code.HasValue ? ad.AD_MK_Code.Value.ToString() : "";
        }

        o.CT_Make = Get_CT_Make();
        o.CT_Years = Get_CT_Years();
        o.CT_Color_List = Get_CT_Color_List();
        o.CT_Insurance_Comp = Get_CT_Insurance_Comp();

        Response.Write(JsonConvert.SerializeObject(o));
    }

    private void Get_Years()
    {
        var o = Get_CT_Years();
        Response.Write(JsonConvert.SerializeObject(o));
    }

    /// <summary>
    /// 车型
    /// </summary>
    /// <param name="id"></param>
    private void Get_Make()
    {
        var o = Get_CT_Make();
        Response.Write(JsonConvert.SerializeObject(o));
    }

    private void Get_Car_Model_All(int id)
    {
        var o = Get_CT_Car_Model_All(id);
        Response.Write(JsonConvert.SerializeObject(o));
    }

    /// <summary>
    /// 车风格
    /// </summary>
    /// <param name="id"></param>
    private void Get_Car_Style_All(int id)
    {
        var o = Get_CT_Car_Style_All(id);
        Response.Write(JsonConvert.SerializeObject(o));
    }

    private void Get_Car_Model(int id, int CS_Year)
    {
        var o = Get_CT_Car_Model(id, CS_Year);
        Response.Write(JsonConvert.SerializeObject(o));
    }

    /// <summary>
    /// 车风格
    /// </summary>
    /// <param name="id"></param>
    private void Get_Car_Style(int id, int CI_YR_Code)
    {
        var o = Get_CT_Car_Style(id, CI_YR_Code);
        Response.Write(JsonConvert.SerializeObject(o));
    }

    private void Get_Insurance_Agents(int id)
    {
        var o = Get_CT_Insurance_Agents(id);
        Response.Write(JsonConvert.SerializeObject(o));
    }

    #region selects

    /// <summary>
    /// 生产商
    /// </summary>
    /// <returns></returns>
    private IEnumerable<dynamic> Get_CT_Make()
    {
        var db = DBCRMTree.GetInstance();
        var sql_text_part = Interna ? "[MK_Make_EN]" : "[MK_Make_CN]";
        var sql = string.Format(@"SELECT 
                isnull({0},'') as [text]
                ,[MK_Code] as value
                FROM [CT_Make]
group by {0},MK_Code
order by {0}
                ", sql_text_part);

        return db.Query<dynamic>(sql);
    }

    private IEnumerable<dynamic> Get_CT_Car_Model_All(int CM_MK_Code)
    {
        var db = DBCRMTree.GetInstance();
        var sql_text_part = Interna ? "[CM_Model_EN]" : "[CM_Model_CN]";
        var sql = string.Format(@"SELECT  
                isnull({0},'') as [text]
                ,[CM_Code] as value
                FROM [CT_Car_Model]
                LEFT JOIN CT_Car_Style ON CS_CM_Code = CM_Code
                where [CM_MK_Code] = @0 
                group by {0},CM_Code
                order by {0}
                ", sql_text_part);

        return db.Query<dynamic>(sql, CM_MK_Code);
    }

    /// <summary>
    /// 样式风格
    /// </summary>
    /// <param name="CS_CM_Code"></param>
    /// <returns></returns>
    private IEnumerable<dynamic> Get_CT_Car_Style_All(int CS_CM_Code)
    {
        var db = DBCRMTree.GetInstance();
        var sql_text_part = Interna ? "[CS_Style_EN]" : "[CS_Style_CN]";
        var sql = string.Format(@"SELECT distinct 
                        isnull({0},'') as [text]
                        ,[CS_Code] as value
                        FROM [CT_Car_Style]
                        where [CS_CM_Code] = @0 
                        ", sql_text_part);
        return db.Query<dynamic>(sql, CS_CM_Code);
    }

    /// <summary>
    /// 车型
    /// </summary>
    /// <param name="CM_MK_Code"></param>
    /// <returns></returns>
    private IEnumerable<dynamic> Get_CT_Car_Model(int CM_MK_Code, int CI_YR_Code)
    {
        var db = DBCRMTree.GetInstance();
        var sql_text_part = Interna ? "[CM_Model_EN]" : "[CM_Model_CN]";
        var sql = string.Format(@"SELECT  
                isnull({0},'') as [text]
                ,[CM_Code] as value
                FROM [CT_Car_Model]
                LEFT JOIN CT_Car_Style ON CS_CM_Code = CM_Code
                where [CM_MK_Code] = @0 and CS_Year = (SELECT YR_Year FROM CT_Years WHERE YR_Code = @1)
group by {0},CM_Code
order by {0}
                ", sql_text_part);

        return db.Query<dynamic>(sql, CM_MK_Code, CI_YR_Code);
    }
    private IEnumerable<dynamic> Get_CT_Car_ModelI(int CM_MK_Code)
    {
        var db = DBCRMTree.GetInstance();
        var sql_text_part = Interna ? "[CM_Model_EN]" : "[CM_Model_CN]";
        var sql = string.Format(@"SELECT  
                isnull({0},'') as [text]
                ,[CM_Code] as value
                FROM [CT_Car_Model]
                LEFT JOIN CT_Car_Style ON CS_CM_Code = CM_Code
                where [CM_MK_Code] = @0 
group by {0},CM_Code
order by {0}
                ", sql_text_part);

        return db.Query<dynamic>(sql, CM_MK_Code);
    }
    /// <summary>
    /// 样式风格
    /// </summary>
    /// <param name="CS_CM_Code"></param>
    /// <returns></returns>
    private IEnumerable<dynamic> Get_CT_Car_Style(int CS_CM_Code, int CI_YR_Code)
    {
        var db = DBCRMTree.GetInstance();
        var sql_text_part = Interna ? "[CS_Style_EN]" : "[CS_Style_CN]";
        //        var sql = string.Format(@"SELECT distinct 
        //                isnull({0},'') as [text]
        //                ,[CS_Code] as value
        //                FROM [CT_Car_Style]
        //                where [CS_CM_Code] = @0 
        //                ", sql_text_part);
        var sql = string.Format(@"SELECT  
                isnull({0},'') as [text]
                ,[CS_Code] as value
                FROM [CT_Car_Style]
                where [CS_CM_Code] = @0 and CS_Year = (SELECT YR_Year FROM CT_Years WHERE YR_Code = @1)
group by {0},CS_Code
order by {0}
                ", sql_text_part);

        return db.Query<dynamic>(sql, CS_CM_Code, CI_YR_Code);
    }

    private IEnumerable<dynamic> Get_CT_Car_Style2(int CS_Code)
    {
        var db = DBCRMTree.GetInstance();
        var sql_text_part = Interna ? "[CS_Style_EN]" : "[CS_Style_CN]";
        var sql = string.Format(@"SELECT  
                isnull({0},'') as [text]
                ,[CS_Code] as value
                FROM [CT_Car_Style]
                where [CS_Code] = @0
                ", sql_text_part);

        return db.Query<dynamic>(sql, CS_Code);
    }
    /// <summary>
    /// 年份
    /// </summary>
    /// <returns></returns>
    private IEnumerable<dynamic> Get_CT_Years()
    {
        var db = DBCRMTree.GetInstance();
        var sql = @"SELECT 
            [YR_Code] as [value]
            ,[YR_Year] as [text]
            FROM [CT_Years]
            order by YR_Year desc";

        return db.Query<dynamic>(sql);
    }

    /// <summary>
    /// 颜色
    /// </summary>
    /// <returns></returns>
    private IEnumerable<dynamic> Get_CT_Color_List()
    {
        var db = DBCRMTree.GetInstance();
        var sql_text_part = Interna ? "[CL_Color_EN]" : "[CL_Color_CN]";
        var sql = string.Format(@"SELECT 
                {0} as [text]
                ,[CL_Code] as [value]
                FROM [CT_Color_List]
                ", sql_text_part);

        return db.Query<dynamic>(sql);
    }

    private IEnumerable<dynamic> Get_CT_Insurance_Comp()
    {
        var db = DBCRMTree.GetInstance();
        var sql = @"SELECT 
            [IC_Code] as [value]
            ,[IC_Name] as [text]
            FROM [CT_Insurance_Comp]
            order by IC_Name";

        return db.Query<dynamic>(sql);
    }

    private IEnumerable<dynamic> Get_CT_Insurance_Agents(int IA_IC_Code)
    {
        var db = DBCRMTree.GetInstance();
        var sql = @"SELECT IA_Code AS value
        ,AU_Name AS [text]
        ,IA_AU_Code
        FROM CT_Insurance_Agents ia INNER JOIN CT_All_Users au
        ON au.AU_Code = ia.IA_AU_Code
        WHERE IA_IC_Code = @0";

        return db.Query<dynamic>(sql, IA_IC_Code);
    }
    #endregion


}