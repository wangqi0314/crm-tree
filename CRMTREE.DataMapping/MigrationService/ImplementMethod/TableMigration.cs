using MigrationService.DBConnection;
using MigrationService.Helper;
using MigrationService.ServiceLogic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace MigrationService.ImplementMethod
{
    class TableMigration
    {
        public string FileName = "E:\\TEMP\\ExceptionListForAppointment";

        public string FileNameVINDecode = "";


        public bool getDataFromDifferentTable(string tablename, string filepath, Dictionary<string, string> dicTmp)
        {
            bool flagCus = true;
            if (tablename == "customerdetail")
            {
                flagCus = insertCustomerDetailIntoDB(filepath, dicTmp);

                if (!flagCus)
                    return false;
                else
                    return true;
            }

            if (tablename == "saleservice")
            {
                flagCus = insertSaleservice(filepath, dicTmp,tablename);

                if (!flagCus)
                    return false;
                else
                    return true;
            }


            DataTable dtTemp = new DataTable();
            string fieldsGroup = string.Empty;

            dicTmp.TryGetValue(tablename, out fieldsGroup);

            ImportFromSourceFile isf = new ImportFromSourceFile();
            List<String> tableNameList = new List<string>();
            Dictionary<string, string> fieldMapping = new Dictionary<string, string>();
            //MyTable
            DataTable regardingOurTable = getMyRegardingTable(tablename);

            //DataTable sourceTable = isf.importTxtIntoDt(filepath);
            DataTable sourceTable = isf.importExlIntoDt(filepath,tablename);

            if (sourceTable == null || sourceTable.Rows.Count <= 0)
                return true;

            DataTable dtCombineSourceDataWithDataMapping = BuildMyNewStructure(sourceTable, regardingOurTable);

            tableNameList = CommonHelper.instance.GetListFromTable(dtCombineSourceDataWithDataMapping, "DM_Fields_Table").Distinct().ToList();

            switch (tablename)
            {
                case "owner":
                    if (insertUserInfoIntoDB(dtCombineSourceDataWithDataMapping, sourceTable, regardingOurTable, tableNameList, fieldMapping))
                        return true;
                    else
                        return false;
                case "vehicle":
                    if (insertVehicleIntoDB(dtCombineSourceDataWithDataMapping, sourceTable, regardingOurTable, tableNameList, fieldMapping))
                        return true;
                    else
                        return false;
                case "ro_type":
                    if (insertRelationshipTypeIntoDB(filepath, fieldsGroup))
                        return true;
                    else
                        return false;
                case "part":
                    if (insertPartIntoDB(dtCombineSourceDataWithDataMapping, sourceTable, regardingOurTable, tableNameList, fieldMapping))
                        return true;
                    else
                        return false;
                case "jobcode":
                    if (insertJobcodeIntoDB(dtCombineSourceDataWithDataMapping, sourceTable, regardingOurTable, tableNameList, fieldMapping))
                        return true;
                    else
                        return false;
                case "ro":
                    if (insertRelationshipIntoDB(filepath, fieldsGroup))
                        return true;
                    else
                        return false;
                case "customerdetail":
                    if (insertCustomerDetailIntoDB(filepath,dicTmp))
                        return true;
                    else
                        return false;
                case "appointment":
                    if (insertAppointment(dtCombineSourceDataWithDataMapping, sourceTable, regardingOurTable, tableNameList, fieldMapping))
                        return true;
                    else
                        return false;
               default:
                    return false;
            }
        }
        public bool insertCustomerDetailIntoDB(string filepath, Dictionary<string, string> dicTmp)
        {
            bool owener_flag = getDataFromDifferentTable("owner", filepath, dicTmp);

            bool car_flag = getDataFromDifferentTable("vehicle", filepath, dicTmp);
             
             if (car_flag && owener_flag)
            //if (car_flag)
             {
                 return true;
             }
             else
             {
                 return false;
             }
             return true;
        }

        public bool insertUserInfoIntoDB(DataTable dtCombineSourceDataWithDataMapping, DataTable sourceTable, DataTable regardingOurTable, List<string> tableNameList, Dictionary<string, string> fieldMapping)
        {
            try
            {
                ArrayList alKeyList = new ArrayList();
                DataTable dtTemp = new DataTable();
                string level1TableName = (from row in regardingOurTable.AsEnumerable()
                                          where row.Field<string>("DM_Table_Level") == "1"
                                          select row.Field<string>("DM_Fields_Table")).FirstOrDefault();

                if (tableNameList.Contains(level1TableName))
                    tableNameList.Remove(level1TableName);

                StringBuilder sbSecQuery = new StringBuilder();
                string temp = string.Empty;
                string fkey = string.Empty;
                List<string> queryList = new List<string>();
                int calc = 0;
                int sumAll = 0;

                BulkModifyService bks = new BulkModifyService();

                bks.removeEmpty(sourceTable);

                string query = "select * from CT_All_Users cau left join CT_Phone_List cat on cau.AU_Code= cat.PL_AU_AD_Code left join CT_Users_Accnts cua on cau.AU_Code=cua.UA_AU_Code where UA_DMS_Code!='' and UA_AD_OM_Code="+CommonHelper.instance.dealerParameter;
                dtTemp = ExecuteSQL.ExecuteQuery(query);
                bks.removeEmpty(dtTemp);
                foreach (DataRow dr in dtCombineSourceDataWithDataMapping.AsEnumerable())
                {
                    if (dr.Field<string>("DM_Fields_Name") != null)
                        fieldMapping.Add(dr.Field<string>("DM_Fields_Name"), dr.Field<string>("MR_Fields_RegardingName"));
                }


                string username;
                string phoneNO;
                string dmsNo;
                string AL_Add1;
                string EL_Address;
                string contactname;

                if (!fieldMapping.TryGetValue("AU_Name", out username))
                    return false;
                fieldMapping.TryGetValue("PL_Number", out phoneNO);
                fieldMapping.TryGetValue("UA_DMS_Code", out dmsNo);
                fieldMapping.TryGetValue("AL_Add1", out AL_Add1);
                fieldMapping.TryGetValue("EL_Address", out EL_Address);
                fieldMapping.TryGetValue("DL_AU_Code", out contactname);

                if (phoneNO != null && phoneNO.ToString().Trim() != "")
                {
                    List<string> lsDMS = CommonHelper.instance.GetListFromTable(dtTemp, "UA_DMS_Code");

                    
                    lsDMS=lsDMS.Distinct().ToList<string>();
                    lsDMS.Remove("*");

                    var tempDmsNo = from source in sourceTable.AsEnumerable()
                                    where
                                        //source.Field<string>(username) == dt.Field<string>("AU_Name") && source.Field<string>(phoneNO) == dt.Field<string>("PL_Number")
                                        //&& source.Field<string>(phoneNO) != "" && 
                                    lsDMS.Contains(source.Field<string>(dmsNo))
                                    select source;
                     
                    DataTable tmpTable = new DataTable();
                    int countRecon = 0;
                    if (tempDmsNo != null && tempDmsNo.Count() > 0)
                    {
                        tmpTable = tempDmsNo.Distinct().CopyToDataTable();
                        countRecon = tmpTable.Rows.Count;
                    }

                    if (tmpTable != null && countRecon > 0)
                    {
                        var ss = sourceTable.AsEnumerable().Except(tempDmsNo).Distinct();
                        if (ss != null && ss.Count() != 0)
                            sourceTable = ss.CopyToDataTable();
                        else
                            sourceTable = null;

                        string duplicateDMSNo = "select UA_AU_Code from CT_Users_Accnts left join CT_All_Users on UA_AU_Code=AU_Code where UA_AD_OM_Code=" + CommonHelper.instance.dealerParameter + " and UA_DMS_Code=";
                        string au_Code = string.Empty;
                        
                        foreach (DataRow dr in tmpTable.AsEnumerable())
                        {
                            au_Code = ExecuteSQL.SaveReturnIndentityKey(duplicateDMSNo + "\'"+dr.Field<string>(dmsNo)+"\' and AU_Name=\'"+dr.Field<string>(username)+"\'");

                            //update CT_All_Users set AU_Name='' where AU_Code=''
                            if(string.IsNullOrEmpty(au_Code))
                            {
                                continue;
                            }
                            //update CT_Address_List set AL_Add1='' where AL_AU_AD_Code=''

                            //update CT_Phone_List set PL_Number='' where PL_AU_AD_Code='' 

                            if (username != null && !String.IsNullOrEmpty(dr.Field<string>(username)))
                            {
                                ExecuteSQL.RunSqlExecution("update CT_All_Users set AU_Name=\'" + dr.Field<string>(username) + "\' where AU_Code=" + au_Code);
                            }

                            if (phoneNO != null && !String.IsNullOrEmpty(dr.Field<string>(phoneNO)))
                            {
                                ExecuteSQL.RunSqlExecution("update CT_Phone_List set PL_Number=\'" + dr.Field<string>(phoneNO) + "\' where PL_AU_AD_Code=" + au_Code);
                            }

                            if (AL_Add1!=null && !String.IsNullOrEmpty(dr.Field<string>(AL_Add1)))
                            {
                                ExecuteSQL.RunSqlExecution("update CT_Address_List set AL_Add1=\'" + dr.Field<string>(AL_Add1) + "\' where AL_AU_AD_Code=" + au_Code);
                            }

                        }
                    }
                }

                string selectQuery = string.Empty;
                List<string> temporaryList = new List<string>();
                ArrayList al = new ArrayList();

                if (sourceTable == null || sourceTable.Rows.Count == 0)
                    return true;
                foreach (DataRow dr in sourceTable.AsEnumerable())
                {
                    sumAll++;
                    calc++;
                    if (calc >= 1000)
                    {
                        queryList = new List<string>();
                        queryList.Add(sbSecQuery.ToString());
                        int insertT = ExecuteSQL.ExecuteQuery(queryList);
                        sbSecQuery = new StringBuilder();
                        calc = 0;
                        //if (insertT <= 0)
                        //    return false;
                        //else
                        queryList = new List<string>();
                    }

                    temp = bks.MainGeneration(string.Empty, level1TableName, regardingOurTable, fieldMapping, dr, out selectQuery);
                    if (ExecuteSQL.RunSqlExcuteScalar(selectQuery) < 1 && !temporaryList.Contains(selectQuery))
                    {
                        temporaryList.Add(selectQuery);
                        fkey = ExecuteSQL.SaveReturnIndentityKey(temp.ToString(), selectQuery, level1TableName);
                    }
                    else
                        continue;

                    if (string.IsNullOrEmpty(fkey))
                        continue;

                    List<string> ls = new List<string>();

                    //if (tableNameList.Contains("CT_Address_List"))
                    //{

                    if (AL_Add1!=null && dr[AL_Add1] != null && !string.IsNullOrEmpty(dr[AL_Add1].ToString()))
                        sbSecQuery.Append(bks.GenerateSecondQuery("CT_Address_List", fkey, fieldMapping, dr, regardingOurTable));

                    //}
                    //if (tableNameList.Contains("CT_Phone_List"))
                    //{

                    if (phoneNO != null && dr[phoneNO] != null && !string.IsNullOrEmpty(dr[phoneNO].ToString()))
                        sbSecQuery.Append(bks.GenerateSecondQuery("CT_Phone_List", fkey, fieldMapping, dr, regardingOurTable));

                    //}
                    //if (tableNameList.Contains("CT_Email_List"))
                    //{

                    if (EL_Address != null && dr[EL_Address] != null && !string.IsNullOrEmpty(dr[EL_Address].ToString()))
                        sbSecQuery.Append(bks.GenerateSecondQuery("CT_Email_List", fkey, fieldMapping, dr, regardingOurTable));

                    // }
                    tableNameList.Remove("CT_Address_List");
                    tableNameList.Remove("CT_Phone_List");
                    tableNameList.Remove("CT_Email_List");

                    sbSecQuery.Append(bks.GenerateSecondQuery(tableNameList, fkey, fieldMapping, dr, regardingOurTable));
                    alKeyList.Add(fkey);
                }
                temporaryList = new List<string>();

                queryList.Add(sbSecQuery.ToString());

                queryList.Add("UPDATE CT_Users_Accnts set UA_UType=1 where UA_UType is null");

                int insertResult = ExecuteSQL.ExecuteQuery(queryList);

                if (insertResult > 0)
                    return true;
                else
                    return false;
            }
            catch(Exception ex)
            {
                CommonHelper.instance.generateEmailToItStuffs(null, "Error Happened when loading CustomerDetails", ex.ToString());
                
                return false;

            }
        }
        public bool insertVehicleIntoDB(DataTable dtCombineSourceDataWithDataMapping, DataTable sourceTable, DataTable regardingOurTable, List<string> tableNameList, Dictionary<string, string> fieldMapping)
        {
            StringBuilder query = new StringBuilder();

            List<string> lsTmp = new List<string>();

            DataTable vehicleTemp = new DataTable();

            bool flag = true;

            foreach (DataRow dr in dtCombineSourceDataWithDataMapping.AsEnumerable())
            {
                if (dr.Field<string>("DM_Fields_Name") != null)
                    fieldMapping.Add(dr.Field<string>("DM_Fields_Name"), dr.Field<string>("MR_Fields_RegardingName"));
            }

            BulkModifyService bks = new BulkModifyService();
            try
            {
                #region CT_Color_List

                if (tableNameList.Contains("CT_Color_List"))
                {

                    string colorName = string.Empty;
                    fieldMapping.TryGetValue("CL_Color_CN", out colorName);
                    lsTmp = CommonHelper.instance.GetListFromTable(sourceTable, colorName).Distinct().ToList();
                    DataTable colorList = ExecuteSQL.ExecuteQuery("select CL_Color_CN from CT_Color_List");

                    DataTable tempSourceTable = new DataTable();
                    tempSourceTable.Columns.Add(colorName);

                    foreach (DataRow dr in colorList.AsEnumerable())
                    {
                        if (lsTmp.Contains(dr["CL_Color_CN"].ToString()))
                        {
                            lsTmp.Remove(dr["CL_Color_CN"].ToString());
                        }
                    }

                    if (lsTmp != null && lsTmp.Count > 0)
                    {
                        foreach (string s in lsTmp)
                        {
                            tempSourceTable.Rows.Add(s);
                        }
                    }
                    foreach (DataRow dr in tempSourceTable.AsEnumerable())
                    {
                        query.Append(bks.MainGeneration("", "CT_Color_List", regardingOurTable, fieldMapping, dr));
                    }
                    if (!String.IsNullOrEmpty(query.ToString().Trim()))
                    {
                        flag = ExecuteSQL.RunSqlExecution(query.ToString());
                        if (!flag)
                            return false;
                    }
                    tableNameList.Remove("CT_Color_List");
                }

                #endregion

                #region CT_Make
                if (tableNameList.Contains("CT_Make"))
                {
                    //setForCarModelStyle(fieldMapping,sourceTable);
                    tableNameList.Remove("CT_Car_Model");
                    tableNameList.Remove("CT_Car_Style");
                    tableNameList.Remove("CT_Make");
                }

                if(tableNameList.Contains("CT_Drivers_List"))
                {
                    tableNameList.Remove("CT_Drivers_List");
                }

                #endregion

                #region CT_Car_Inventory

                string vinCode = string.Empty;
                
                 
                 
                bks.removeEmpty(sourceTable);

                foreach (string tableName in tableNameList)
                {
                    if (tableName == "CT_Car_Inventory")
                    {
                        string updateTime = string.Empty;
                        string licence = string.Empty;
                        string dms_no=string.Empty;

                        string au_code=string.Empty;
                        string au_value = string.Empty;
                        string au_CodeQuery = "select AU_Code from CT_All_Users left join CT_Phone_List on AU_Code=PL_AU_AD_Code where AU_Name=? and PL_Number in (?,?)";
                        
                        query = new StringBuilder();

                        fieldMapping.TryGetValue("CI_VIN", out vinCode);
                        fieldMapping.TryGetValue("CI_Update_dt", out updateTime);
                        fieldMapping.TryGetValue("CI_Licence", out licence);
                        fieldMapping.TryGetValue("CI_AU_Code", out dms_no);

                        //sourceTable = CommonHelper.instance.SelectDistinct(sourceTable, vinCode);

                        string field_vin = string.Empty;

                        vehicleTemp = new DataTable();
                        vehicleTemp = ExecuteSQL.ExecuteQuery("select CI_VIN from CT_Car_Inventory");

                        lsTmp = CommonHelper.instance.GetListFromTable(vehicleTemp, "CI_VIN").Distinct().ToList();

                        StringBuilder sb = new StringBuilder();

                        DataTable dttP = new DataTable();

                        int count = 0;
                        int count1 = 0;

                        string tempQuery=string.Empty;
                        string selectQuery = string.Empty;

                        List<string> temporaryList = new List<string>();

                        foreach (DataRow dr in sourceTable.AsEnumerable())
                        {
                            field_vin = dr[vinCode].ToString();

                            if (!lsTmp.Contains(field_vin))
                            {
                                selectQuery = string.Empty;
                                tempQuery = string.Empty;

                                tempQuery = bks.MainGeneration(string.Empty, "CT_Car_Inventory", regardingOurTable, fieldMapping, dr, out selectQuery);

                                //select CI_Code from CT_Car_Inventory where CI_CS_Code=65928 and CI_Licence='沪A' and CI_VIN='LFV3A23CXE3193414' and CI_AU_Code=183606 and CI_Warr_St_dt='2015-03-31 12:46:00' and CI_Update_dt='2015-03-27 14:48:00'
                                selectQuery = selectQuery.Split(new string[] { "and CI_Warr_St_dt","" },StringSplitOptions.RemoveEmptyEntries)[0];

                                if (ExecuteSQL.RunSqlExcuteScalar(selectQuery) < 1 && !temporaryList.Contains(selectQuery))
                                {
                                    temporaryList.Add(selectQuery);
                                    query.Append(tempQuery);
                                    count1++;
                                }
                                else
                                    continue;

                                if (count1 >= 2000)
                                {
                                    if (!string.IsNullOrEmpty(query.ToString()))
                                    {
                                        //ExecuteSQL.RunSqlExecution(sb.ToString());
                                        ExecuteSQL.RunSqlExecution(query.ToString());
                                    }
                                    query = new StringBuilder();
                                    //sb = new StringBuilder();
                                    count1 = 0;
                                }

                                //query.Append(bks.MainGeneration("", "CT_Car_Inventory", regardingOurTable, fieldMapping, dr));
                            }
                            else
                            {
                                if (licence != null && dr[licence] != null && !string.IsNullOrEmpty(dr[licence].ToString()))
                                {
                                    dttP = ExecuteSQL.ExecuteQuery("select CI_Update_dt,CI_Licence from CT_Car_Inventory where CI_VIN=\'" + field_vin + "\'");

                                    if (updateTime != null && dttP!=null && dttP.Rows[0]["CI_Update_dt"] != null && dr[updateTime] != null && dr[updateTime].ToString() != "")
                                    {

                                        DateTime dt1;
                                        DateTime dt2;

                                        if(!DateTime.TryParse(dttP.Rows[0]["CI_Update_dt"].ToString(), out dt1))
                                        {
                                            continue;
                                        }
                                        if(!DateTime.TryParse(dttP.Rows[0]["CI_Update_dt"].ToString(), out dt2))
                                        {
                                            continue;
                                        }

                                        if (dt1<= dt2)
                                        {
                                            au_code = ExecuteSQL.GetRegardingField(au_CodeQuery.Replace("{{DealerPara}}", CommonHelper.instance.dealerParameter).ToString(), dr, dms_no);

                                            if (!string.IsNullOrEmpty(au_code))
                                                au_value = ",CI_AU_Code=" + au_code;
                                            else
                                                au_value = string.Empty;

                                            sb.Append("update CT_Car_Inventory set CI_Licence=\'" + dr[licence].ToString() + "\',CI_Update_dt=\'" + dr[updateTime] + "\'" + au_value + " where CI_VIN=\'" + field_vin + "\';");

                                            au_code = string.Empty;
                                            au_value = string.Empty;

                                            count++;
                                        }
                                    }
                                    else if (Convert.IsDBNull(dttP.Rows[0]["CI_Update_dt"]))
                                    {
                                        au_code = ExecuteSQL.GetRegardingField(au_CodeQuery.Replace("{{DealerPara}}", CommonHelper.instance.dealerParameter).ToString(), dr, dms_no);

                                        if (!string.IsNullOrEmpty(au_code))
                                            au_value = ",CI_AU_Code=" + au_code;
                                        else
                                            au_value = string.Empty;

                                        string ci_Update_dt = string.Empty;

                                        if (updateTime != null)
                                            ci_Update_dt = updateTime;
                                        else
                                            ci_Update_dt = "1900-01-01 00:00:00"; 
                                        //DateTime.MinValue.ToString();

                                        sb.Append("update CT_Car_Inventory set CI_Licence=\'" + dr[licence].ToString() + "\',CI_Update_dt=\'" + ci_Update_dt + "\'" + au_value + " where CI_VIN=\'" + field_vin + "\';");

                                        au_code = string.Empty;
                                        au_value = string.Empty;

                                        count++;
                                    }
                                }
                                else
                                {
                                    continue;
                                }

                                if (count >= 2000)
                                {
                                    if (!string.IsNullOrEmpty(sb.ToString()))
                                    {
                                        ExecuteSQL.RunSqlExecution(sb.ToString());
                                        //ExecuteSQL.RunSqlExecution(query.ToString());
                                    }
                                    //query = new StringBuilder();
                                    sb = new StringBuilder();
                                    count = 0;
                                }
                            }
                        }
                        temporaryList = new List<string>();
                        if(!string.IsNullOrEmpty(sb.ToString()))
                        {
                            ExecuteSQL.RunSqlExecution(sb.ToString());
                        }
                        if (string.IsNullOrEmpty(query.ToString().Trim()))
                            continue;

                        flag = ExecuteSQL.RunSqlExecution(query.ToString());
                        if (!flag)
                            return false;

                        string queryx = "select * from CT_Car_Inventory left join CT_Car_Style on CI_CS_Code=CS_Code where CI_Code not in (select CI_Code from CT_Car_Inventory left join CT_Car_Style on CI_CS_Code=CS_Code" +
                        " where CS_VIN_series= SUBSTRING(CI_VIN,4,5) and CS_VIN_Yr=SUBSTRING(CI_VIN,10,1)) and CI_VIN is not null and CI_Update_dt>'2015/01/25'";

                        DataTable dtTemp = ExecuteSQL.ExecuteQuery(queryx);

                        var tempTable = from st in sourceTable.AsEnumerable()
                                        from dt in dtTemp.AsEnumerable()
                                        where st.Field<string>(vinCode) == dt.Field<string>("CI_VIN")
                                        select st;

                        DataTable dtt = new DataTable();
                        string mailContent = string.Empty;
                        string mailContent2 = string.Empty;
                        if (tempTable == null || tempTable.Count()==0)
                        {
                            dtt = null;
                            mailContent = "Good news, All vin has been decoded succesfully";
                            mailContent2 = "All VIN has been decoded";
                        }
                        else
                        {
                            dtt = tempTable.CopyToDataTable();
                            mailContent = "Please use the below query select CS_Code from CT_Car_Style where CS_VIN_series= SUBSTRING(@CI_VIN,4,5) and CS_VIN_Yr=SUBSTRING(@CI_VIN,10,1)";
                            mailContent2 = "Below VIN can't be decoded";
                        }
                        CommonHelper.instance.generateEmailToItStuffs(dtt, mailContent2, mailContent);
                    }
                    else
                    {
                        query = new StringBuilder();
                        string tmpQuery = string.Empty;
                        string tmpQuery2 = string.Empty;
                        ArrayList al = new ArrayList();
                        foreach (DataRow dr in sourceTable.AsEnumerable())
                        {
                            tmpQuery2 = bks.MainGeneration("", tableName, regardingOurTable, fieldMapping, dr, out tmpQuery);

                            if (String.IsNullOrEmpty(ExecuteSQL.SaveReturnIndentityKey(tmpQuery)))
                            {
                                query.Append(tmpQuery2);
                            }
                        }
                        if (string.IsNullOrEmpty(query.ToString().Trim()))
                            continue;
                        flag = ExecuteSQL.RunSqlExecution(query.ToString());
                        //if (!flag)
                        //    return false;
                    }
                }
                #endregion

                #region CT_Driver_List

                    string queryRelative = string.Empty;
                    string queryPhoneNO = string.Empty;
                    string queryValidation = string.Empty;

                    string relativeName = string.Empty;
                    fieldMapping.TryGetValue("DL_AU_Code", out relativeName);
                    //string relativePhoneNo = string.Empty;
                    //fieldMapping.TryGetValue("DL_AU_Code", out relativeName);
                    string ci_code = string.Empty;
                    fieldMapping.TryGetValue("DL_CI_Code", out ci_code);

                    queryValidation = "select distinct CI_VIN,AU_Name,PL_Number,AU_Code from CT_Car_Inventory left join CT_Drivers_List on CI_Code=DL_CI_Code " +
                        "left join CT_All_Users on AU_Code=CI_AU_Code left join CT_Phone_List on AU_Code=PL_AU_AD_Code where DL_CI_Code is not null and CI_VIN is not null " +
                        "union select distinct CI_VIN,AU_Name,PL_Number,AU_Code from CT_Car_Inventory left join CT_All_Users on AU_Code=CI_AU_Code left join CT_Phone_List on AU_Code=PL_AU_AD_Code where CI_VIN is not null and AU_Name is not null and PL_Number is not null";
                    DataTable dtTempValid = ExecuteSQL.ExecuteQuery(queryValidation);

                    DataTable dtMappingRelationShip = ExecuteSQL.ExecuteQuery("select * from CT_DataMapping_Extra where CT_DataGroup_ID=2 and CT_Fields_Name='contact_No'");

                    List<string> lsSourceName = CommonHelper.instance.GetListFromTable(dtMappingRelationShip, "CT_Source_Field_Name").Distinct().ToList();

                    //if(lsSourceName.Count==1)

                    //string mobile= string.Empty;
                  
                    foreach(string ss in lsSourceName)
                    {
                        if (sourceTable.Columns.Contains(ss))
                        {
                            updateForDrivers(ss, sourceTable, dtTempValid,ci_code,relativeName);
                        }
                    }

                    //Put here
                    //ExecuteSQL.RunSqlExecution(query.ToString());

                    List<string> alBatch = new List<string>();
                     
                    string batchForPhoneList = "delete from CT_Phone_List where LEN(PL_Number)<=0";
                    string batchForDL_M_Au_Code = "update CT_Drivers_List set DL_M_AU_Code=CI.CI_AU_Code from CT_Car_Inventory CI where CI.CI_Code=DL_CI_Code and DL_M_AU_Code is null";

                    alBatch.Add(batchForDL_M_Au_Code);
                    alBatch.Add(batchForPhoneList);

                    ExecuteSQL.ExecuteQuery(alBatch);
                    

                    #endregion

            }
            catch (Exception ex)
            {
                
                return false;
            }
            finally
            {
                List<string> alBatch = new List<string>();

                string batchForPhoneList = "delete from CT_Phone_List where LEN(PL_Number)<=0";
                //string batchForDL_M_Au_Code = "update CT_Drivers_List set DL_M_AU_Code=CI.CI_AU_Code from CT_Car_Inventory CI where CI.CI_Code=DL_CI_Code and DL_M_AU_Code is null";

                //alBatch.Add(batchForDL_M_Au_Code);
                alBatch.Add(batchForPhoneList);

                ExecuteSQL.ExecuteQuery(alBatch);
            }

            return flag;
        }

        public bool updateForDrivers(string mobile, DataTable sourceTable,DataTable dtTempValid,string ci_code,string relativeName)
        {
            try
            {
                string queryRelative = string.Empty;
                string queryPhoneNO = string.Empty;
                string queryValidation = string.Empty;

                DataTable sourceTableNew = new DataTable();

                if (sourceTable != null && sourceTable.Rows.Count > 0 && dtTempValid.Rows.Count > 0)
                {
                    var reconAlreadyHave = from dt1 in dtTempValid.AsEnumerable()
                                           from dt2 in sourceTable.AsEnumerable()
                                           where dt1.Field<string>("CI_VIN") == dt2.Field<string>(ci_code)
                                           && dt1.Field<string>("AU_Name") == dt2.Field<string>(relativeName)
                                           && (dt1.Field<string>("PL_Number") == dt2.Field<string>(mobile))

                                           select dt2;

                    if (reconAlreadyHave != null && reconAlreadyHave.AsEnumerable().Count() > 0)
                    {
                        sourceTableNew = sourceTable.AsEnumerable().Except(reconAlreadyHave.AsEnumerable()).CopyToDataTable();
                    }

                    var reconPLNoNotMatch = from dt1 in dtTempValid.AsEnumerable()
                                            from dt2 in sourceTable.AsEnumerable()
                                            where dt1.Field<string>("CI_VIN") == dt2.Field<string>(ci_code)
                                            && dt1.Field<string>("AU_Name") == dt2.Field<string>(relativeName)
                                            select new
                                            {
                                                mobileNo = dt2.Field<string>(mobile),
                                                AU_Code = dt1["AU_Code"]
                                            };
                    if (reconPLNoNotMatch != null && reconPLNoNotMatch.AsEnumerable().Count() > 0)
                    {
                        StringBuilder sbList = new StringBuilder();
                        string queryx = string.Empty;
                        List<string> ls = new List<string>();
                        string au = string.Empty;
                        int count = 0;
                        foreach (var dr in reconPLNoNotMatch.Distinct())
                        {
                            if (String.IsNullOrEmpty(dr.mobileNo))
                                continue;
                            queryx = "select * from CT_All_Users left join CT_Phone_List on AU_Code=PL_Au_ad_code where AU_code=" + dr.AU_Code + " and PL_Number=\'" + dr.mobileNo + "\'";
                            if (ls.Contains(queryx) || ExecuteSQL.RunSqlExcuteScalar(queryx) >= 1)
                                continue;
                            else
                                ls.Add(queryx);
                            count++;
                            sbList.Append("insert into CT_Phone_List (PL_AU_AD_Code,PL_Number,PL_Active,PL_Update_dt) values (" + dr.AU_Code + ",\'" + (dr.mobileNo != null ? dr.mobileNo.ToString() : "") + "\',1,GETDATE());");
                            if (count == 2000)
                            {
                                count = 0;
                                ExecuteSQL.RunSqlExecution(sbList.ToString());
                                sbList = new StringBuilder();
                            }
                        }
                        ExecuteSQL.RunSqlExecution(sbList.ToString());
                        ls = new List<string>();
                    }
                }

                string DriverAucode = string.Empty;
                string cicode = string.Empty;
                string ownerAucode = string.Empty;
                StringBuilder query = new StringBuilder();

                string CheckQuery = string.Empty;

                foreach (DataRow dr in sourceTableNew.AsEnumerable())
                {
                    string dateTimeNow = String.Format("{0:g}", DateTime.Now);

                    if (dr[mobile] == null || dr[mobile].ToString() == "")
                        continue;

                    CheckQuery = "select * from CT_All_Users left join CT_Phone_List on AU_Code=PL_AU_AD_Code where AU_Name=\'" + (dr[relativeName] != null ? dr[relativeName].ToString() : "") + "\' and PL_Number=\'" + dr[mobile] + "\'";

                    if (ExecuteSQL.RunSqlExcuteScalar(CheckQuery) >= 1)
                    {
                        DriverAucode = ExecuteSQL.ExecuteQuery(CheckQuery).Rows[0]["AU_Code"].ToString();
                        CheckQuery = "select * from CT_Drivers_List where DL_AU_Code=" + DriverAucode;

                        if (ExecuteSQL.RunSqlExcuteScalar(CheckQuery) >= 1)
                            continue;
                        else
                        {

                            queryValidation = " select CI_AU_Code from CT_Car_Inventory where CI_VIN=\'" + dr[ci_code] + "\'";

                            ownerAucode = ExecuteSQL.SaveReturnIndentityKey(queryValidation);

                            if (DriverAucode == ownerAucode)
                                continue;
                            if (string.IsNullOrEmpty(ownerAucode))
                                ownerAucode = "null";
                            queryValidation = " select CI_Code from CT_Car_Inventory where CI_VIN=\'" + dr[ci_code] + "\'";

                            cicode = ExecuteSQL.SaveReturnIndentityKey(queryValidation);

                            if (!string.IsNullOrEmpty(cicode))
                                queryValidation = "insert into CT_Drivers_List (DL_M_AU_Code,DL_AU_Code,DL_CI_Code,DL_Update_dt,DL_Type) values (" + ownerAucode + "," + DriverAucode + "," + cicode + ",\'" + dateTimeNow + "\',1);";

                            ExecuteSQL.RunSqlExecution(queryValidation);

                            continue;

                        }
                    }
                    else
                    {
                        queryRelative = "insert into CT_All_Users (AU_Active_tag,AU_Update_dt,AU_Name,AU_UG_Code) values (1,\'" + dateTimeNow + "\',\'" + (dr[relativeName] != null ? dr[relativeName].ToString() : "") + "\',1)";

                        if (ExecuteSQL.RunSqlExecution(queryRelative))
                        {
                            queryValidation = "select AU_Code from CT_All_Users where AU_Update_dt=\'" + dateTimeNow + "\' and AU_Name=\'" + (dr[relativeName] != null ? dr[relativeName].ToString() : "") + "\'";

                            DriverAucode = ExecuteSQL.SaveReturnIndentityKey(queryValidation);

                            if (!string.IsNullOrEmpty(DriverAucode))
                            {
                                queryPhoneNO = "insert into CT_Phone_List (PL_AU_AD_Code,PL_Number,PL_Active,PL_Update_dt) values (" + DriverAucode + ",\'" + (dr[mobile] != null ? dr[mobile].ToString() : "") + "\',1,GETDATE());";

                                query.Append(queryPhoneNO);

                                queryValidation = " select CI_Code from CT_Car_Inventory where CI_VIN=\'" + dr[ci_code] + "\'";

                                cicode = ExecuteSQL.SaveReturnIndentityKey(queryValidation);

                                queryValidation = " select CI_AU_Code from CT_Car_Inventory where CI_VIN=\'" + dr[ci_code] + "\'";

                                ownerAucode = ExecuteSQL.SaveReturnIndentityKey(queryValidation);

                                if (!string.IsNullOrEmpty(cicode))
                                    queryValidation = "insert into CT_Drivers_List (DL_M_AU_Code,DL_AU_Code,DL_CI_Code,DL_Update_dt,DL_Type) values (" + ownerAucode + "," + DriverAucode + "," + cicode + ",\'" + dateTimeNow + "\',1);";

                                ExecuteSQL.RunSqlExecution(queryValidation + query.ToString());

                                //ExecuteSQL.RunSqlExecution(query.ToString());

                                query = new StringBuilder();

                            }
                        }
                        else
                            continue;
                    }

                }
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public bool insertRelationshipTypeIntoDB(string filepath, string fieldsGroup)
        {
            return true;
        }
        public bool insertRelationshipIntoDB(string filepath, string fieldsGroup)
        {
            return true;
        }
        public bool insertPartIntoDB(DataTable dtCombineSourceDataWithDataMapping, DataTable sourceTable, DataTable regardingOurTable, List<string> tableNameList, Dictionary<string, string> fieldMapping)
        {
            StringBuilder query = new StringBuilder();

            List<string> lsTmp = new List<string>();

            DataTable partsTemp = new DataTable();

            bool flag = true;

            foreach (DataRow dr in dtCombineSourceDataWithDataMapping.AsEnumerable())
            {
                if (dr.Field<string>("DM_Fields_Name") != null)
                    fieldMapping.Add(dr.Field<string>("DM_Fields_Name"), dr.Field<string>("MR_Fields_RegardingName"));
            }

            BulkModifyService bks = new BulkModifyService();

            List<string> tbNameList = new List<string>();
            foreach (string tableName in tableNameList)
            {
                if (tableName == "CT_All_Users")
                {
                    StringBuilder sbSecQuery = new StringBuilder();
                    string temp = string.Empty;
                    string fkey = string.Empty;
                    List<string> queryList = new List<string>();
                    int calc = 0;
                    int sumAll = 0;

                    DataTable dttemp = getDTTemp(sourceTable, "EMPLOYEE_NAME");
                    tbNameList.Add("CT_Dealer_Empl");
                    foreach (DataRow dr in dttemp.AsEnumerable())
                    {

                        sumAll++;
                        calc++;
                        if (calc >= 1000)
                        {
                            queryList.Add(sbSecQuery.ToString());
                            sbSecQuery = new StringBuilder();
                            calc = 0;
                        }
                        temp = bks.MainGeneration(string.Empty, tableName, regardingOurTable, fieldMapping, dr);
                        fkey = ExecuteSQL.SaveReturnIndentityKey(temp.ToString(), tableName);

                        sbSecQuery.Append(bks.GenerateSecondQuery(tbNameList, fkey, fieldMapping, dr, regardingOurTable));

                    }

                    queryList.Add(sbSecQuery.ToString());

                    int insertResult = ExecuteSQL.ExecuteQuery(queryList);

                    if (insertResult > 0)
                        return true;
                    else
                        return false;
                }
                else
                {

                    bks.setIdentityOn(tableName);
                    foreach (DataRow dr in sourceTable.AsEnumerable())
                    {
                        query.Append(bks.MainGeneration("", tableName, regardingOurTable, fieldMapping, dr));
                    }
                    flag = ExecuteSQL.RunSqlExecution(query.ToString());
                    if (!flag)
                        return false;
                }

            }

            return flag;

        }
        public bool insertJobcodeIntoDB(DataTable dtCombineSourceDataWithDataMapping, DataTable sourceTable, DataTable regardingOurTable, List<string> tableNameList, Dictionary<string, string> fieldMapping)
        {
            StringBuilder query = new StringBuilder();

            List<string> lsTmp = new List<string>();

            DataTable partsTemp = new DataTable();

            bool flag = true;

            foreach (DataRow dr in dtCombineSourceDataWithDataMapping.AsEnumerable())
            {
                if (dr.Field<string>("DM_Fields_Name") != null)
                    fieldMapping.Add(dr.Field<string>("DM_Fields_Name"), dr.Field<string>("MR_Fields_RegardingName"));
            }

            BulkModifyService bks = new BulkModifyService();


            if (tableNameList.Contains("CT_Service_Codes"))
            {

                bks.setIdentityOn("CT_Service_Codes");
                foreach (DataRow dr in sourceTable.AsEnumerable())
                {
                    query.Append(bks.MainGeneration("", "CT_Service_Codes", regardingOurTable, fieldMapping, dr));
                }
                flag = ExecuteSQL.RunSqlExecution(query.ToString());
                if (!flag)
                    return false;

                tableNameList.Remove("CT_Service_Codes");
            }



            foreach (string tableName in tableNameList)
            {
                if (tableName == "CT_All_Users")
                {
                    StringBuilder sbSecQuery = new StringBuilder();
                    string temp = string.Empty;
                    string fkey = string.Empty;
                    List<string> queryList = new List<string>();
                    int calc = 0;
                    int sumAll = 0;

                    string value=string.Empty;
                    string hs_ro_no = string.Empty;
                    fieldMapping.TryGetValue("AU_Name", out value);
                    fieldMapping.TryGetValue("HS_RO_No", out hs_ro_no);

                    DataTable dtEmpl = ExecuteSQL.ExecuteQuery("select AU_Name,DE_ID from CT_Dealer_Empl left join CT_All_Users on DE_AU_Code=AU_Code where DE_AD_OM_Code=" + CommonHelper.instance.dealerParameter + " and (AU_UG_Code=26 or AU_UG_Code=28) and AU_ID_Type=1");

                    DataTable dttemp = getDTTemp(dtEmpl, "AU_Name");

                    string updateOrNot =  (from v in dtCombineSourceDataWithDataMapping.AsEnumerable()
                                       where v.Field<string>("MR_Fields_Name") == "AU_Name"
                                       select v.Field<int>("MR_Fields_Update_Or_Not")).ToString();

                    List<string> lstemp = CommonHelper.instance.GetListFromTable(dttemp, "AU_Name");

                    string roQuery = "select distinct HS_RO_No from CT_History_Service where HS_AD_Code=" + CommonHelper.instance.dealerParameter + " and HS_Update_dt>'2015/01/01'";
                    List<string> lsRoList = CommonHelper.instance.GetListFromTable(ExecuteSQL.ExecuteQuery(roQuery), "HS_RO_No");

                    Dictionary<string, string> dic = new Dictionary<string, string>();
                    List<string> tbNameDE = new List<string>();
                    List<string> tbNameHS = new List<string>();
                    tbNameDE.Add("CT_Dealer_Empl");
                    tbNameHS.Add("CT_History_Service");
                    bks.removeEmpty(sourceTable);

                    int count = 0;
                    //int count = sourceTable.Rows.Count;
                    //DataRow drT=sourceTable.Rows[count-1];
                    //sourceTable.Rows.Remove(drT);

                    string queryTemp=string.Empty;

                    foreach (DataRow dr in sourceTable.AsEnumerable())
                    {
                        if (lsRoList!=null && lsRoList.Count!=0 && lsRoList.Contains(dr[hs_ro_no].ToString()))
                            continue;
                        sumAll++;
                        calc++;

                        if (calc >= 1000)
                        {
                            queryList = new List<string>();

                            queryList.Add(sbSecQuery.ToString());

                            int tempInsert=ExecuteSQL.ExecuteQuery(queryList);

                            if (tempInsert <= 0)
                                return false;

                            queryList = new List<string>();
                            sbSecQuery = new StringBuilder();
                            calc = 0;
                        }
                        //if (lstemp.Contains(dr[value]))
                        //{
                        //    //temp = bks.MainGeneration(string.Empty, tableName, regardingOurTable, fieldMapping, dr);

                        //    temp = "select AU_Code from CT_Dealer_Empl left join CT_All_Users on DE_AU_Code=AU_Code where DE_AD_OM_Code=" + CommonHelper.instance.dealerParameter + " and (AU_UG_Code=26 or AU_UG_Code=28) and AU_ID_Type=1 and AU_Name=\'" + dr[value] + '\'';

                        //    fkey = ExecuteSQL.SaveReturnIndentityKey(temp.ToString());
                        //    lstemp.Remove(dr[value].ToString());
                        //    dic.Add(dr[value].ToString(), fkey);
                        //    //sbSecQuery.Append(bks.GenerateSecondQuery(tbNameDE, fkey, fieldMapping, dr, regardingOurTable));
                        //    sbSecQuery.Append(bks.GenerateSecondQuery(tbNameHS, fkey, fieldMapping, dr, regardingOurTable)+";");
                        //}
                        //else if (!lstemp.Contains(dr[value]) && dr[value]!="" && !dic.TryGetValue(dr[value].ToString(), out fkey))
                        //{
                        //    temp = bks.MainGeneration(string.Empty, tableName, regardingOurTable, fieldMapping, dr);
                        //    fkey = ExecuteSQL.SaveReturnIndentityKey(temp.ToString(), tableName);

                        //    if(string.IsNullOrEmpty(fkey))
                        //    {
                        //        continue;
                        //    }

                        //    lstemp.Remove(dr[value].ToString());
                        //    dic.Add(dr[value].ToString(), fkey);
                        //    sbSecQuery.Append(bks.GenerateSecondQuery(tbNameDE, fkey, fieldMapping, dr, regardingOurTable) + ";");
                        //    sbSecQuery.Append(bks.GenerateSecondQuery(tbNameHS, fkey, fieldMapping, dr, regardingOurTable) + ";");
                        //}
                        //else
                        //{
                        //    dic.TryGetValue(dr[value].ToString(), out fkey);
                        //    sbSecQuery.Append(bks.GenerateSecondQuery(tbNameHS, fkey, fieldMapping, dr, regardingOurTable) + ";");
                        //}
                        sbSecQuery.Append(bks.GenerateSecondQuery(tbNameHS, fkey, fieldMapping, dr, regardingOurTable) + ";");

                    }

                    queryList.Add(sbSecQuery.ToString());

                    int insertResult = ExecuteSQL.ExecuteQuery(queryList);

                    string OurSql = "select * from CT_History_Service where HS_AD_Code=" + CommonHelper.instance.dealerParameter;
                    var dataForRecon=ExecuteSQL.ExecuteQuery(OurSql);

                    string ro_no=string.Empty;
                    fieldMapping.TryGetValue("HS_RO_No", out ro_no);
                    string hs_close_date=string.Empty;
                    fieldMapping.TryGetValue("HS_RO_Close",out hs_close_date);


                    if (dataForRecon != null && dataForRecon.Rows.Count >= 0)
                    {
                        var result = from sT in sourceTable.AsEnumerable()
                                     join dr in dataForRecon.AsEnumerable()
                                     on sT.Field<string>(ro_no) equals dr.Field<string>("HS_RO_No") into JoinRecon
                                     from jr in JoinRecon.DefaultIfEmpty()
                                     where sT[hs_close_date] != null && sT[hs_close_date].ToString() != "" && jr!=null && jr["HS_RO_Close"] != sT[hs_close_date]
                                     select new
                                     {
                                         RO_NO = jr.Field<string>("HS_RO_No"),
                                         RO_CLOSE = sT[hs_close_date]
                                     };

                        StringBuilder sbList = new StringBuilder();
                        count = 0;
                        if (result != null && result.Count() > 0)
                        {
                            foreach (var gnData in result)
                            {
                                count++;

                                if (count == 2000)
                                {
                                    count = 0;
                                    ExecuteSQL.RunSqlExecution(sbList.ToString());
                                    sbList = new StringBuilder();
                                }
                                sbList.Append("update CT_History_Service set HS_RO_Close=\'" + gnData.RO_CLOSE + "\' where HS_RO_No=\'" + gnData.RO_NO + "\';");
                            }
                        }
                        ExecuteSQL.RunSqlExecution(sbList.ToString());
                    }
                    
                    
                    if (insertResult > 0)
                        return true;
                    else
                        return false;
                }
            }

            return flag;
        }

        public bool insertAppointment(DataTable dtCombineSourceDataWithDataMapping, DataTable sourceTable, DataTable regardingOurTable, List<string> tableNameList, Dictionary<string, string> fieldMapping)
        {

            ArrayList alKeyList = new ArrayList();
            DataTable dtTemp = new DataTable();
            string level1TableName = (from row in regardingOurTable.AsEnumerable()
                                      where row.Field<string>("DM_Table_Level") == "1"
                                      select row.Field<string>("DM_Fields_Table")).FirstOrDefault();

            if (tableNameList.Contains(level1TableName))
                tableNameList.Remove(level1TableName);

            StringBuilder sbSecQuery = new StringBuilder();
            string temp = string.Empty;
            string fkey = string.Empty;
            List<string> queryList = new List<string>();
            int calc = 0;
            int sumAll = 0;

            BulkModifyService bks = new BulkModifyService();

            bks.removeEmpty(sourceTable);

            string query = "select * from CT_Car_Inventory left join CT_All_Users on CI_AU_Code=AU_Code left join CT_Phone_List on PL_AU_AD_Code=AU_Code";
            dtTemp = ExecuteSQL.ExecuteQuery(query);
            bks.removeEmpty(dtTemp);
            foreach (DataRow dr in dtCombineSourceDataWithDataMapping.AsEnumerable())
            {
                if (dr.Field<string>("DM_Fields_Name") != null)
                    fieldMapping.Add(dr.Field<string>("DM_Fields_Name"), dr.Field<string>("MR_Fields_RegardingName"));
            }


            string username;
            string phoneNO;
            string licenNO;
            string apID;
            bool ingnoreFlag = true;
            if (!fieldMapping.TryGetValue("AP_AU_Code", out username))
                return false;

            username = username.Split('|')[0].ToString();

            fieldMapping.TryGetValue("AP_PL_ML_Code", out phoneNO);
            fieldMapping.TryGetValue("AP_CI_Code", out licenNO);
            fieldMapping.TryGetValue("AP_ID", out apID);

            string queryApList = "select distinct AP_ID from CT_Appt_Service where AP_ID is not null";

            List<string> dtTmpAppointNo =CommonHelper.instance.GetListFromTable(ExecuteSQL.ExecuteQuery(queryApList),"AP_ID");

            if (phoneNO != null && phoneNO.ToString().Trim() != "")
            {
                var tempTable = (from dt in dtTemp.AsEnumerable()
                                from source in sourceTable.AsEnumerable()
                                where source.Field<string>(licenNO) == dt.Field<string>("CI_Licence")
                                select source).Distinct();
                                //source.Field<string>(username) == dt.Field<string>("AU_Name") 
                                //&& source.Field<string>(phoneNO) == dt.Field<string>("PL_Number")
                                //&& 
                                
                if(tempTable!=null && tempTable.Count()>0 && sourceTable!=null)
                {

                    int CurrentCount=tempTable.CopyToDataTable().Rows.Count;
                    int BeforeCount=sourceTable.Rows.Count;

                    if (CurrentCount != BeforeCount)
                    {
                        DataTable dtExceptionReport = sourceTable.AsEnumerable().Except(tempTable.AsEnumerable()).CopyToDataTable();

                        FileName = FileName + DateTime.Now.ToString("yyyy-MM-dd-HHmmss") + ".xls";

                        CommonHelper.instance.ExportToExcel(FileName,dtExceptionReport);


                        //SendMail("crm@shinfotech.cn", "shinfotech", toList, ccList, bccList, "Testing From Thinking Tree", "support@crmtree.com", "ThinkingTree", "mail.shinfotech.cn", "Testing From Nicolas");

                    }

                }
                if (dtTmpAppointNo != null && dtTmpAppointNo.Count != 0)
                {
                    var temporyFile=from dt in tempTable.AsEnumerable()
                                   where !dtTmpAppointNo.Contains(dt.Field<string>(apID))
                                   select dt;
                    if (temporyFile != null && temporyFile.ToList().Count != 0)
                    {
                        sourceTable = temporyFile.CopyToDataTable();
                    }
                    else
                        ingnoreFlag = false;
                }
                else
                {
                    sourceTable = tempTable.CopyToDataTable();
                }
                
            }

            string remark;
            fieldMapping.TryGetValue("SN_Note", out remark);
            BulkModifyService.instance.setIdentityOn(level1TableName);

            List<string> lsKeyList =new List<string>();
            if (ingnoreFlag)
            {
                foreach (DataRow dr in sourceTable.AsEnumerable())
                {
                    sumAll++;
                    calc++;
                    if (calc >= 1000)
                    {
                        queryList = new List<string>();
                        queryList.Add(sbSecQuery.ToString());
                        int insertT = ExecuteSQL.ExecuteQuery(queryList);
                        sbSecQuery = new StringBuilder();
                        calc = 0;
                        if (insertT <= 0)
                            return false;
                        else
                            queryList = new List<string>();
                    }
                    temp = bks.MainGeneration(string.Empty, level1TableName, regardingOurTable, fieldMapping, dr);

                    fkey = ExecuteSQL.SaveReturnIndentityKey(temp.ToString(), level1TableName);
                    if (string.IsNullOrEmpty(fkey))
                        continue;

                    lsKeyList.Add(fkey);

                    if (remark!=null && dr[remark] != null && !String.IsNullOrEmpty(dr[remark].ToString()))
                        sbSecQuery.Append(bks.GenerateSecondQuery(tableNameList, fkey, fieldMapping, dr, regardingOurTable));

                    alKeyList.Add(fkey);
                }
            }
            int insertResult = 1;
            if (!String.IsNullOrEmpty(sbSecQuery.ToString()))
            {
                queryList.Add(sbSecQuery.ToString());

                ExecuteSQL.ExecuteQuery(queryList);
            }


            //List<string> toList = new List<string>();
            //toList.Add("xianan@shinfotech.cn");
            //List<string> ccList = new List<string>();
            ////ccList.Add("fagahdel@gmail.com");
            //ccList.Add("shihong881214@163.com");
            //ccList.Add("xianan@shinfotech.cn");
            //ccList.Add("234163000@qq.com");
            //ccList.Add("fariborz@shunovo.com");
            //List<string> bccList = new List<string>();
            //bccList.Add("fagahdel@gmail.com");

            //CommonHelper.instance.SendMail("crm@shinfotech.cn", "shinfotech", toList, ccList, bccList, "Testing From Thinking Tree", "support@crmtree.com", "ThinkingTree", "smtp.mxhichina.com", "Testing From Nicolas", FileName);

            DataCleanningForAppointment(lsKeyList, sourceTable,fieldMapping);

            if (insertResult > 0)
                return true;
            else
                return false;


        }

        public bool insertSaleservice(string filepath, Dictionary<string, string> dicTmp,string tablename)
        {

            ImportFromSourceFile isf = new ImportFromSourceFile();

            DataTable sourceTable = isf.importExlIntoDt(filepath, tablename);

            BulkModifyService bks = new BulkModifyService();

            bks.removeEmpty(sourceTable);

            #region CustomerDetails

            customerForNotHavingID(sourceTable,dicTmp);

            #endregion


            #region Vehicle

            getDataFromDifferentTable("vehicle", filepath, dicTmp);

            #endregion


            #region JobCode

           
            string fieldsGroup = string.Empty;

            dicTmp.TryGetValue("saleservice", out fieldsGroup);

            List<String> tableNameList = new List<string>();
            Dictionary<string, string> fieldMapping = new Dictionary<string, string>();
            //MyTable
            DataTable regardingOurTable = getMyRegardingTable("saleservice");

            DataTable dtCombineSourceDataWithDataMapping = BuildMyNewStructure(sourceTable, regardingOurTable);

            tableNameList = CommonHelper.instance.GetListFromTable(dtCombineSourceDataWithDataMapping, "DM_Fields_Table").Distinct().ToList();

            foreach (DataRow dr in dtCombineSourceDataWithDataMapping.AsEnumerable())
            {
                if (dr.Field<string>("DM_Fields_Name") != null)
                    fieldMapping.Add(dr.Field<string>("DM_Fields_Name"), dr.Field<string>("MR_Fields_RegardingName"));
            }

            ArrayList alKeyList = new ArrayList();
            DataTable dtTemp = new DataTable();
            string level1TableName = (from row in regardingOurTable.AsEnumerable()
                                      where row.Field<string>("DM_Table_Level") == "1"
                                      select row.Field<string>("DM_Fields_Table")).FirstOrDefault();

            if (tableNameList.Contains(level1TableName))
                tableNameList.Remove(level1TableName);

            StringBuilder sbSecQuery = new StringBuilder();
            string temp = string.Empty;
            string fkey = string.Empty;
            List<string> queryList = new List<string>();
            int calc = 0;
            int sumAll = 0;

            string selectQuery = string.Empty;
            List<string> temporaryList = new List<string>();
            ArrayList al = new ArrayList();

            if (sourceTable == null || sourceTable.Rows.Count == 0)
                return true;

            StringBuilder errorLog = new StringBuilder();
            DataTable dtClone = sourceTable.Clone();

            foreach (DataRow dr in sourceTable.AsEnumerable())
            {
                sumAll++;
                calc++;
                if (calc >= 1000)
                {
                    queryList = new List<string>();
                    queryList.Add(sbSecQuery.ToString());
                    int insertT = ExecuteSQL.ExecuteQuery(queryList);
                    sbSecQuery = new StringBuilder();
                    calc = 0;
                    //if (insertT <= 0)
                    //    return false;
                    //else
                    queryList = new List<string>();
                }

                temp = bks.MainGeneration(string.Empty, level1TableName, regardingOurTable, fieldMapping, dr, out selectQuery);

                if (selectQuery.Contains("SX_Buyer_Code is null"))
                {
                    //StringBuilder errorLog = new StringBuilder();
                    dtClone.ImportRow(dr);
                    //errorLog.Append(temp);
                    continue;
                }

                if (ExecuteSQL.RunSqlExcuteScalar(selectQuery) < 1 && !temporaryList.Contains(selectQuery))
                {
                    temporaryList.Add(selectQuery);
                    fkey = ExecuteSQL.SaveReturnIndentityKey(temp.ToString(), selectQuery, level1TableName);
                }
                else
                    continue;

                sbSecQuery.Append(bks.GenerateSecondQuery(tableNameList, fkey, fieldMapping, dr, regardingOurTable));
                alKeyList.Add(fkey);
            }
            temporaryList = new List<string>();

            queryList.Add(sbSecQuery.ToString());

            int insertResult = ExecuteSQL.ExecuteQuery(queryList);

            if(!string.IsNullOrEmpty(errorLog.ToString()))
                CommonHelper.instance.generateEmailToItStuffs(null, errorLog.ToString(), "Missing these Buyer Code" + DateTime.Now.ToShortDateString());

            if (insertResult > 0)
                return true;
            else
                return false;

            #endregion

            return true;

        }

        public bool customerForNotHavingID(DataTable sourceTable, Dictionary<string, string> dicTmp)
        {
            DataTable dtTempN = new DataTable();
            string fieldsGroup = string.Empty;

            //dicTmp.TryGetValue("owner", out fieldsGroup);

             
            List<String> tableNameList = new List<string>();
            Dictionary<string, string> fieldMapping = new Dictionary<string, string>();
            //MyTable
            DataTable regardingOurTable = getMyRegardingTable("owner");

            DataTable dtCombineSourceDataWithDataMapping = BuildMyNewStructure(sourceTable, regardingOurTable);

            tableNameList = CommonHelper.instance.GetListFromTable(dtCombineSourceDataWithDataMapping, "DM_Fields_Table").Distinct().ToList();

            try
            {
                ArrayList alKeyList = new ArrayList();
                DataTable dtTemp = new DataTable();
                string level1TableName = (from row in regardingOurTable.AsEnumerable()
                                          where row.Field<string>("DM_Table_Level") == "1"
                                          select row.Field<string>("DM_Fields_Table")).FirstOrDefault();

                if (tableNameList.Contains(level1TableName))
                    tableNameList.Remove(level1TableName);

                StringBuilder sbSecQuery = new StringBuilder();
                string temp = string.Empty;
                string fkey = string.Empty;
                List<string> queryList = new List<string>();
                int calc = 0;
                int sumAll = 0;

                BulkModifyService bks = new BulkModifyService();

                bks.removeEmpty(sourceTable);

                string query = "select * from CT_All_Users cau left join CT_Phone_List cat on cau.AU_Code= cat.PL_AU_AD_Code left join CT_Users_Accnts cua on cau.AU_Code=cua.UA_AU_Code where UA_DMS_Code!='' and UA_AD_OM_Code=" + CommonHelper.instance.dealerParameter;
                dtTemp = ExecuteSQL.ExecuteQuery(query);
                bks.removeEmpty(dtTemp);
                foreach (DataRow dr in dtCombineSourceDataWithDataMapping.AsEnumerable())
                {
                    if (dr.Field<string>("DM_Fields_Name") != null)
                        fieldMapping.Add(dr.Field<string>("DM_Fields_Name"), dr.Field<string>("MR_Fields_RegardingName"));
                }

                #region  update For AU_Name or PL_Number
               

                //string username;
                string phoneNO;
                //string dmsNo;
                string AL_Add1;
                string EL_Address;
                ////string contactname;

                //if (!fieldMapping.TryGetValue("AU_Name", out username))
                //    return false;
                fieldMapping.TryGetValue("PL_Number", out phoneNO);
                //fieldMapping.TryGetValue("UA_DMS_Code", out dmsNo);
                fieldMapping.TryGetValue("AL_Add1", out AL_Add1);
                fieldMapping.TryGetValue("EL_Address", out EL_Address);
                //fieldMapping.TryGetValue("DL_AU_Code", out contactname);

                //if (phoneNO != null && phoneNO.ToString().Trim() != "")
                //{
                //    //List<string> lsDMS = CommonHelper.instance.GetListFromTable(dtTemp, "UA_DMS_Code");


                //    //lsDMS = lsDMS.Distinct().ToList<string>();
                //    //lsDMS.Remove("*");

                //    var tempDmsNo = from source in sourceTable.AsEnumerable()
                //                    from dt in dtTemp.AsEnumerable()
                //                    where source.Field<string>(username) == dt.Field<string>("AU_Name") 
                //                        && source.Field<string>(phoneNO) == dt.Field<string>("PL_Number")
                //                        && source.Field<string>(phoneNO) != "" 
                //                    //lsDMS.Contains(source.Field<string>(dmsNo))
                //                    select source;

                //    DataTable tmpTable = new DataTable();
                //    int countRecon = 0;
                //    if (tempDmsNo != null && tempDmsNo.Count() > 0)
                //    {
                //        tmpTable = tempDmsNo.Distinct().CopyToDataTable();
                //        countRecon = tmpTable.Rows.Count;
                //    }

                //    if (tmpTable != null && countRecon > 0)
                //    {
                //        var ss = sourceTable.AsEnumerable().Except(tempDmsNo).Distinct();
                //        if (ss != null && ss.Count() != 0)
                //            sourceTable = ss.CopyToDataTable();
                //        else
                //            sourceTable = null;

                //        string duplicateDMSNo = "select UA_AU_Code from CT_Users_Accnts left join CT_All_Users on UA_AU_Code=AU_Code where UA_AD_OM_Code=" + CommonHelper.instance.dealerParameter + " and UA_DMS_Code=";
                //        string au_Code = string.Empty;

                //        foreach (DataRow dr in tmpTable.AsEnumerable())
                //        {
                //            au_Code = ExecuteSQL.SaveReturnIndentityKey(duplicateDMSNo + "\'" + dr.Field<string>(dmsNo) + "\' and AU_Name=\'" + dr.Field<string>(username) + "\'");

                //            //update CT_All_Users set AU_Name='' where AU_Code=''
                //            if (string.IsNullOrEmpty(au_Code))
                //            {
                //                continue;
                //            }
                //            //update CT_Address_List set AL_Add1='' where AL_AU_AD_Code=''

                //            //update CT_Phone_List set PL_Number='' where PL_AU_AD_Code='' 

                //            if (username != null && !String.IsNullOrEmpty(dr.Field<string>(username)))
                //            {
                //                ExecuteSQL.RunSqlExecution("update CT_All_Users set AU_Name=\'" + dr.Field<string>(username) + "\' where AU_Code=" + au_Code);
                //            }

                //            if (phoneNO != null && !String.IsNullOrEmpty(dr.Field<string>(phoneNO)))
                //            {
                //                ExecuteSQL.RunSqlExecution("update CT_Phone_List set PL_Number=\'" + dr.Field<string>(phoneNO) + "\' where PL_AU_AD_Code=" + au_Code);
                //            }

                //            if (AL_Add1 != null && !String.IsNullOrEmpty(dr.Field<string>(AL_Add1)))
                //            {
                //                ExecuteSQL.RunSqlExecution("update CT_Address_List set AL_Add1=\'" + dr.Field<string>(AL_Add1) + "\' where AL_AU_AD_Code=" + au_Code);
                //            }

                //        }
                //    }
                //}

                #endregion

                string selectQuery = string.Empty;
                List<string> temporaryList = new List<string>();
                ArrayList al = new ArrayList();

                if (sourceTable == null || sourceTable.Rows.Count == 0)
                    return true;
                foreach (DataRow dr in sourceTable.AsEnumerable())
                {
                    sumAll++;
                    calc++;
                    if (calc >= 1000)
                    {
                        queryList = new List<string>();
                        queryList.Add(sbSecQuery.ToString());
                        int insertT = ExecuteSQL.ExecuteQuery(queryList);
                        sbSecQuery = new StringBuilder();
                        calc = 0;
                        //if (insertT <= 0)
                        //    return false;
                        //else
                        queryList = new List<string>();
                    }

                    temp = bks.MainGeneration(string.Empty, level1TableName, regardingOurTable, fieldMapping, dr, out selectQuery);
                    if (ExecuteSQL.RunSqlExcuteScalar(selectQuery) < 1 && !temporaryList.Contains(selectQuery))
                    {
                        temporaryList.Add(selectQuery);
                        fkey = ExecuteSQL.SaveReturnIndentityKey(temp.ToString(), selectQuery, level1TableName);
                    }
                    else
                        continue;

                    if (string.IsNullOrEmpty(fkey))
                        continue;

                    List<string> ls = new List<string>();

                    //if (tableNameList.Contains("CT_Address_List"))
                    //{

                    if (AL_Add1 != null && dr[AL_Add1] != null && !string.IsNullOrEmpty(dr[AL_Add1].ToString()))
                        sbSecQuery.Append(bks.GenerateSecondQuery("CT_Address_List", fkey, fieldMapping, dr, regardingOurTable));

                    //}
                    //if (tableNameList.Contains("CT_Phone_List"))
                    //{

                    if (phoneNO != null && dr[phoneNO] != null && !string.IsNullOrEmpty(dr[phoneNO].ToString()))
                        sbSecQuery.Append(bks.GenerateSecondQuery("CT_Phone_List", fkey, fieldMapping, dr, regardingOurTable));

                    //}
                    //if (tableNameList.Contains("CT_Email_List"))
                    //{

                    if (EL_Address != null && dr[EL_Address] != null && !string.IsNullOrEmpty(dr[EL_Address].ToString()))
                        sbSecQuery.Append(bks.GenerateSecondQuery("CT_Email_List", fkey, fieldMapping, dr, regardingOurTable));

                    // }
                    //sbSecQuery.Append(bks.GenerateSecondQuery(tableNameList, fkey, fieldMapping, dr, regardingOurTable));
                    alKeyList.Add(fkey);
                }
                temporaryList = new List<string>();

                queryList.Add(sbSecQuery.ToString());

                //queryList.Add("UPDATE CT_Users_Accnts set UA_UType=1 where UA_UType is null");

                int insertResult = ExecuteSQL.ExecuteQuery(queryList);

                if (insertResult > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                CommonHelper.instance.generateEmailToItStuffs(null, "Error Happened when loading CustomerDetails", ex.ToString());

                return false;

            }

        }

        public bool vehicleForNotHavingID(DataTable sourceTable, string filepath, Dictionary<string, string> dicTmp)
        {
            DataTable dtTemp = new DataTable();
            string fieldsGroup = string.Empty;

            dicTmp.TryGetValue("vehicle", out fieldsGroup);

            ImportFromSourceFile isf = new ImportFromSourceFile();
            List<String> tableNameList = new List<string>();
            Dictionary<string, string> fieldMapping = new Dictionary<string, string>();
            //MyTable
            DataTable regardingOurTable = getMyRegardingTable("vehicle");

            //DataTable sourceTable = isf.importTxtIntoDt(filepath);
            //DataTable sourceTable = isf.importExlIntoDt(filepath);

            DataTable dtCombineSourceDataWithDataMapping = BuildMyNewStructure(sourceTable, regardingOurTable);

            tableNameList = CommonHelper.instance.GetListFromTable(dtCombineSourceDataWithDataMapping, "DM_Fields_Table").Distinct().ToList();

            return true;

        }

        public void DataCleanningForAppointment(List<string> ls, DataTable dtResourceTable, Dictionary<string, string> fieldMapping)
        {
            string query=string.Empty;

            string appointmentKey = "AP_ID";

            string sourceValue=string.Empty;

            string phoneNo = string.Empty;

            fieldMapping.TryGetValue("AP_PL_ML_Code", out phoneNo);

            phoneNo = phoneNo.Split('|')[0];
            
            fieldMapping.TryGetValue(appointmentKey, out sourceValue);

            if (ls != null && ls.Count > 0)
                query = "select * from CT_Appt_Service where AP_Code in (" + string.Join(",", ls.ToArray()) + ") and AP_PL_ML_Code is null";
            else
            {
                if (dtResourceTable.Rows.Count <= 0)
                    return;

                ls = new List<string>();

                for(int i=0;i<dtResourceTable.Rows.Count;i++)
                {
                    ls.Add("\'"+dtResourceTable.Rows[i][sourceValue].ToString()+"\'");
                }

                query = "select * from CT_Appt_Service where AP_ID in (" + string.Join(",", ls.ToArray()) + ") and AP_PL_ML_Code is null and AP_AU_Code is not null";
            }

            DataTable dtCurrent = ExecuteSQL.ExecuteQuery(query);

            if (dtCurrent == null || dtCurrent.Rows.Count == 0)
                return;

            var linq = from cr in dtResourceTable.AsEnumerable()

                       join dc in dtCurrent.AsEnumerable() on cr.Field<string>(sourceValue) equals dc.Field<string>(appointmentKey)

                       select new
                           {
                               AU_Code = dc["AP_AU_Code"],

                               PhoneNo = cr[phoneNo],

                               AP_ID = cr[sourceValue]
                           };

            string temp=string.Empty;

            string tempSelectQuery=string.Empty;

            StringBuilder sbTemp = new StringBuilder();

            string dateTime=string.Empty;

            string indentityKey = string.Empty;

            foreach(var lq in linq.AsEnumerable())
            {
                dateTime=DateTime.Now.ToString();

                temp = "insert into CT_Phone_List (PL_AU_AD_Code,PL_Active,PL_Number,PL_Update_dt) values(" + lq.AU_Code + ",1,\'" + lq.PhoneNo + "\',\'" + dateTime + "\')";

                tempSelectQuery = "select * from CT_Phone_List where PL_Active=1 and PL_AU_AD_Code="+lq.AU_Code+" and PL_Number=\'"+lq.PhoneNo+"\' and PL_Update_dt=\'"+dateTime+"\'";

                indentityKey=ExecuteSQL.SaveReturnIndentityKey(temp, tempSelectQuery, "CT_Phone_List");

                sbTemp.Append("update CT_Appt_Service set AP_PL_ML_Code="+indentityKey+" where AP_ID=\'"+lq.AP_ID+"\';");

            }
            
            ExecuteSQL.RunSqlExecution(sbTemp.ToString());

        }


        public void setForCarModelStyle(Dictionary<string, string> fieldMapping, DataTable sourceTable)
        {
                    StringBuilder query = new StringBuilder();
                    DataTable vehicleTemp = ExecuteSQL.ExecuteQuery("select MK_Make_EN from CT_Make");

                    List<string> lsTmp = CommonHelper.instance.GetListFromTable(vehicleTemp, "MK_Make_CN").Distinct().ToList();

                    bool flag = true;

                    string value = string.Empty;


                    fieldMapping.TryGetValue("MK_Make_EN", out value);

                    List<string> field_MK_EN = CommonHelper.instance.GetListFromTable(sourceTable, value).Distinct().ToList();

                    if (field_MK_EN != null && field_MK_EN.Count != 0)
                    {
                        foreach (string fd in field_MK_EN)
                        {
                            if (!lsTmp.Contains(fd))
                            {
                                query.Append("insert into CT_MAKE (MK_AM_CODE,MK_Make_EN,MK_Make_CN) values(null,");

                                query.Append("\'" + fd.Trim() + "\',null)");

                                lsTmp.Add(fd);
                            }
                        }
                    }
                    if (!String.IsNullOrEmpty(query.ToString().Trim()))
                    {
                        flag = ExecuteSQL.RunSqlExecution(query.ToString());
                        if (!flag)
                            return;
                    }
                   

                    //

                    string modelValue=string.Empty;
                    string styleValue=string.Empty;
                    fieldMapping.TryGetValue("CM_Model_EN", out modelValue);
                    fieldMapping.TryGetValue("CS_Style_EN",out styleValue);


                    
                    foreach (string tt in field_MK_EN)
                    {
                        string fkey = ExecuteSQL.ExecuteQuery("select MK_Code from CT_Make where MK_Make_EN=\'"+tt+"\'").Rows[0][0].ToString();
                        if (string.IsNullOrEmpty(fkey))
                            break;

                        List<string> dtTmp = (from ss in sourceTable.AsEnumerable()
                                          where ss.Field<string>(value) == tt
                                          select ss.Field<string>(modelValue)).Distinct().ToList();

                        DataTable dtCarModel = ExecuteSQL.ExecuteQuery("select CM_Model_EN from CT_Car_Model where CM_MK_Code=\'"+fkey+"\'");
                        List<string> lsModel = CommonHelper.instance.GetListFromTable(dtCarModel, "CM_Model_EN").Distinct().ToList();

                        foreach(string mv in dtTmp)
                        {
                            if (!lsModel.Contains(mv))
                            {
                                string tmpmv = string.Empty;

                                if (mv.Contains(tt))
                                    tmpmv = mv.Replace(tt, "");
                                else
                                    tmpmv = mv;
                                string queryTemp = "insert into CT_Car_Model (CM_Model_EN,CM_MK_Code,CM_Update_dt,CM_CT_Code) values(\'" + tmpmv.Trim() + "\'," + fkey + ",GetDate(),7)";

                                flag = ExecuteSQL.RunSqlExecution(queryTemp);
                                if (!flag)
                                    return ;

                                string secondKey = ExecuteSQL.ExecuteQuery("select CM_Code from CT_Car_Model where CM_Model_EN=\'" + tmpmv + "\' and CM_MK_Code=" + fkey).Rows[0][0].ToString();

                                List<string> dtTmp2 = (from ss in sourceTable.AsEnumerable()
                                                       where ss.Field<string>(modelValue) == mv
                                                       select ss.Field<string>(styleValue)).Distinct().ToList();
                                string stTemp = string.Empty;

                                DataTable dtCarStyle = ExecuteSQL.ExecuteQuery("select CS_Style_EN from CT_Car_Style where CS_CM_Code=\'" + secondKey + "\'");
                                List<string> lsStyle = CommonHelper.instance.GetListFromTable(dtCarStyle, "CS_Style_EN").Distinct().ToList();
                                query = new StringBuilder();
                                foreach (string sT in dtTmp2)
                                {
                                    if (!lsStyle.Contains(sT))
                                    {
                                        if (string.IsNullOrEmpty(tmpmv))
                                            stTemp = "";
                                        else
                                        {
                                            if (sT.Contains(tmpmv))
                                                stTemp = sT.Replace(tmpmv, "");
                                            else
                                                stTemp = sT;
                                        }
                                        query.Append("insert into CT_Car_Style (CS_Style_CN,CS_Style_EN,CS_CM_Code,CS_UpDate_dt) values(\'\',\'" + stTemp + "\'," + secondKey + ",getDate())");
                                    }
                                    else
                                        query.Append("");
                                }
                                if(string.IsNullOrEmpty(query.ToString()))
                                {
                                    continue;
                                }
                                flag = ExecuteSQL.RunSqlExecution(query.ToString());
                                if (!flag)
                                    return ;
                                
                            }
                        }
                        
                    }

        }

        public DataTable getDTTemp(DataTable dtSource, string fieldName)
        {
            List<string> ls = new List<string>();
            DataTable dt = new DataTable();
            dt.Columns.Add(fieldName);
            //dt=dtSource.Clone();
            foreach (DataRow dr in dtSource.AsEnumerable())
            {
                if (!ls.Contains(dr[fieldName].ToString()))
                {
                    ls.Add(dr.Field<string>(fieldName));
                    dt.Rows.Add(dr[fieldName]);
                }

            }
            return dt;

        }
        public DataTable getMyRegardingTable(string fileName)
        {
            //Here which system could be enhanced as parameter
            string SqlQuery = "select *,cr.MR_Fields_RegardingName as ALIAS from CT_DataMapping_MigrationResouceFilelds cr "
                + "left join CT_DataMapping_MyFields cg on cr.MR_Fields_ID=cg.DM_MyFields_ID "
                + "left join CT_DataMapping_Group cdg on cdg.DG_ID_Group=cg.DM_Fields_Group_ID where (MR_Fields_Description=\'" + CommonHelper.instance.dealerActuallyName + "\' or ISNULL(MR_Fields_Description,'0')='0' or MR_Fields_Description='') and cdg.DG_ResourceTitle=? ";
            ArrayList alTemp = new ArrayList();
            alTemp.Add(fileName);
            DataTable MyTable = ExecuteSQL.ExecuteQuery(SqlQuery, alTemp);
            BulkModifyService.instance.removeEmpty(MyTable);

            foreach(DataRow dr in MyTable.AsEnumerable())
            {
                if (dr["ALIAS"] != null && dr["ALIAS"].ToString() != "")
                {
                    if (dr["ALIAS"].ToString().Contains('|'))
                        dr["ALIAS"] = dr["ALIAS"].ToString().Split('|')[0];
                }
            }

            return MyTable;
        }

        public DataTable BuildMyNewStructure(DataTable sourceFile, DataTable MyTable)
        {


            DataTable dtCol = new DataTable();
            List<string> lsCol = new List<string>();
            dtCol.Columns.Add("ColumnName");
            foreach (DataColumn col in sourceFile.Columns)
            {
                dtCol.Rows.Add(col.ColumnName);
                lsCol.Add(col.ColumnName);
            }
            if (MyTable.Rows.Count != 0 & MyTable != null)
            {
                var fieldsAssociate = from sf in dtCol.AsEnumerable()
                                      join mt in MyTable.AsEnumerable()
                                      on sf.Field<string>("ColumnName").Trim() equals mt.Field<string>("ALIAS")
                                      select new
                                      {
                                          ColumnName = sf.Field<string>("ColumnName") != null ? sf.Field<string>("ColumnName") : string.Empty,
                                          DG_ResourceTitle = mt.Field<string>("DG_ResourceTitle") != null ? mt.Field<string>("DG_ResourceTitle") : string.Empty,
                                          DM_Fields_Table = mt.Field<string>("DM_Fields_Table") != null ? mt.Field<string>("DM_Fields_Table") : string.Empty,
                                          MR_Fields_RegardingName = mt.Field<string>("MR_Fields_RegardingName") != null ? mt.Field<string>("MR_Fields_RegardingName") : string.Empty,
                                          MR_Fields_Rules = mt.Field<string>("MR_Fields_Rules") != null ? mt.Field<string>("MR_Fields_Rules") : string.Empty,
                                          MR_Table_Sequence = mt.Field<string>("MR_Table_Sequence") != null ? mt.Field<string>("MR_Table_Sequence").ToString() : string.Empty,
                                          MR_Fields_Insert_Parameter = mt.Field<string>("MR_Fields_Insert_Parameter") != null ? mt.Field<string>("MR_Fields_Insert_Parameter").ToString() : string.Empty,
                                          MR_Fields_Insert_Query = mt.Field<string>("MR_Fields_Insert_Query") != null ? mt.Field<string>("MR_Fields_Insert_Query").ToString() : string.Empty,
                                          MR_Fields_Select_Query = mt.Field<string>("MR_Fields_Select_Query") != null ? mt.Field<string>("MR_Fields_Select_Query").ToString() : string.Empty,
                                          MR_Fields_Select_Parameter = mt.Field<string>("MR_Fields_Select_Parameter") != null ? mt.Field<string>("MR_Fields_Select_Parameter").ToString() : string.Empty,
                                          
                                          DM_Fields_Name = mt.Field<string>("DM_Fields_Name") != null ? mt.Field<string>("DM_Fields_Name") : string.Empty,
                                          DM_Fields_PrimaryKeyTable = mt.Field<string>("DM_Fields_PrimaryKeyTable") != null ? mt.Field<string>("DM_Fields_PrimaryKeyTable") : string.Empty,
                                          DM_Fields_PrimaryKey = mt.Field<string>("DM_Fields_PrimaryKey") != null ? mt.Field<string>("DM_Fields_PrimaryKey") : string.Empty,
                                          
                                          DM_Fields_TYPE = mt.Field<string>("DM_Fields_TYPE") != null ? mt.Field<string>("DM_Fields_TYPE").ToString() : string.Empty,
                                      };
                DataTable dtNew = new DataTable();
                dtNew.Columns.Add("ColumnName");
                dtNew.Columns.Add("DG_ResourceTitle");
                dtNew.Columns.Add("DM_Fields_Table");
                dtNew.Columns.Add("MR_Fields_RegardingName");
                dtNew.Columns.Add("MR_Fields_Rules");

                dtNew.Columns.Add("MR_Table_Sequence");
                dtNew.Columns.Add("MR_Fields_Insert_Parameter");
                dtNew.Columns.Add("MR_Fields_Insert_Query");
                dtNew.Columns.Add("MR_Fields_Select_Parameter");
                dtNew.Columns.Add("MR_Fields_Select_Query");


                dtNew.Columns.Add("DM_Fields_Name");
                dtNew.Columns.Add("DM_Fields_PrimaryKeyTable");
                dtNew.Columns.Add("DM_Fields_PrimaryKey");

                dtNew.Columns.Add("DM_Fields_TYPE");

                foreach (var item in fieldsAssociate)
                {
                    DataRow row = dtNew.NewRow();
                    row["ColumnName"] = item.ColumnName.Trim();
                    row["DG_ResourceTitle"] = item.DG_ResourceTitle.Trim();
                    row["DM_Fields_Table"] = item.DM_Fields_Table.Trim();
                    row["MR_Fields_RegardingName"] = item.MR_Fields_RegardingName.Trim();
                    row["MR_Fields_Rules"] = item.MR_Fields_Rules.Trim();

                    row["MR_Table_Sequence"] = item.MR_Table_Sequence.Trim();
                    row["MR_Fields_Insert_Parameter"] = item.MR_Fields_Insert_Parameter.Trim();
                    row["MR_Fields_Insert_Query"] = item.MR_Fields_Insert_Query.Trim();
                    row["MR_Fields_Select_Parameter"] = item.MR_Fields_Select_Parameter.Trim();
                    row["MR_Fields_Select_Query"] = item.MR_Fields_Select_Query.Trim();

                    row["DM_Fields_Name"] = item.DM_Fields_Name.Trim();
                    row["DM_Fields_PrimaryKeyTable"] = item.DM_Fields_PrimaryKeyTable.Trim();
                    row["DM_Fields_PrimaryKey"] = item.DM_Fields_PrimaryKey.Trim();

                    row["DM_Fields_TYPE"] = item.DM_Fields_TYPE.Trim();
                    dtNew.Rows.Add(row);
                }
                return dtNew;

            }
            else
                return null;


        }


    }
}
