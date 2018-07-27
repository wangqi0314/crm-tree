using MigrationService.DBConnection;
using MigrationService.Helper;
using MigrationService.ServiceLogic;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigrationService.ImplementMethod
{
    public class ImportFromSourceFile:IService.ITxtService
    {

        public System.Data.DataTable importToDB(string filepath)
        {
            DataTable dtTemp = new DataTable();
            try
            {
                string filename = Path.GetFileNameWithoutExtension(filepath);

                BulkModifyService.instance.GetAttributesFromBothSides(importTxtIntoDt(filepath), filename, out dtTemp);

            }
            catch (Exception ex)
            {


                //throw new Exception(ex.StackTrace + ex.ToString());


            }

            return dtTemp;
        }

        public System.Data.DataTable importTxtIntoDt(string filePath)
        {
            DataTable dtTemp = new DataTable();
            try
            {
                dtTemp = CommonHelper.instance.ReadFromText(filePath, '|');

            }
            catch (Exception ex)
            {
                 

                throw new Exception(ex.StackTrace+ex.ToString());

                
            }

            return dtTemp;
        }

        public System.Data.DataTable importExlIntoDt(string filePath,string fileName)
        {
            if (filePath.Contains("~"))
            {
                File.Delete(filePath);
                return null;
            }

            DataTable dtTemp = new DataTable();
            try
            {
                if(!filePath.Contains("DMS"))
                {
                    dtTemp = CommonHelper.instance.ReadFromSourceFile(filePath, 0,0,fileName);
                }
                else
                    dtTemp = CommonHelper.instance.ReadFromSourceFile(filePath, 5,0,fileName);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.StackTrace + ex.ToString());
                // return null;
            }
            return dtTemp;
        }

        public string inportDataTableIntoDB(string filePath)
        {
            DataTable dt = importExlIntoDt(filePath,null);

            string FileName =Path.GetFileNameWithoutExtension(filePath)+"_"+CommonHelper.instance.dealerParameter;
            //string FileName = "rodetails_";

            if (CommonHelper.instance.AddDataTableToDB(dt, FileName))
            {
                return FileName;
            }
            else
                return null;
            //dt
        }

        public System.Data.DataTable importCsvIntoDt(string filePath)
        {
            DataTable dtTemp = new DataTable();
            try
            {

                 dtTemp = CommonHelper.instance.ReadFromText(filePath, ',');

            }
            catch (Exception ex)
            {


                throw new Exception(ex.StackTrace + ex.ToString());


            }

            return dtTemp;
        }

        public List<string> getPathList(string folderPath)
        {
            return null;

            throw new NotImplementedException();
        }

        public List<string> getPathList(string folderPath, string specialGroupName)
        {
            return null;

            throw new NotImplementedException();
        }

        public bool scanFolderUpload(string folderPath)
        {
            String[] FileNameList = Directory.GetFiles(folderPath);
            bool flag = true;

            string nameMapping = string.Empty;

            string sql = "select * from temp_table_mapping";
            DataTable dtTemp = ExecuteSQL.ExecuteQuery(sql);
            Dictionary<string, string> fieldMapping = new Dictionary<string, string>();
            string filename = string.Empty;

            foreach (DataRow dr in dtTemp.AsEnumerable())
            {
                if (dr.Field<string>("myTable") != null)
                    fieldMapping.Add(dr.Field<string>("myTable"), dr.Field<string>("tableName"));
            }

            foreach (string file in FileNameList)
            {
                filename = System.IO.Path.GetFileNameWithoutExtension(file);

                //TableMigration dts = new TableMigration();

                AutoMappingMethod dts = new AutoMappingMethod();

                foreach (string temp in fieldMapping.Keys)
                {
                    if (filename.Contains(temp))
                    {
                        if (filename.StartsWith("ro_type"))
                        {
                            dts.RecurciveForDataMappings("ro_type", file, fieldMapping);
                            break;
                        }
                        else
                        {
                            if (filename.StartsWith("ro"))
                                dts.RecurciveForDataMappings("ro", file, fieldMapping);
                            else
                            {
                                if (filename.Contains("ro") && !filename.StartsWith(temp))
                                {
                                    continue;
                                }
                                else
                                {
                                    dts.RecurciveForDataMappings(temp, file, fieldMapping);
                                    break;
                                }
                            }
                        }
                    }
                    
                }
            }
            return flag;
        }

        public bool scanFolderUpload(string folderPath,out string errorFileName,string errorFolderPath,string successFolderPath)
        {
            String[] FileNameList = Directory.GetFiles(folderPath);
            bool flag = true;

            string nameMapping = string.Empty;

            string sql = "select * from temp_table_mapping";
            DataTable dtTemp = ExecuteSQL.ExecuteQuery(sql);
            Dictionary<string, string> fieldMapping = new Dictionary<string, string>();
            string filename = string.Empty;

            string tablename = string.Empty;
            foreach (DataRow dr in dtTemp.AsEnumerable())
            {
                if (dr.Field<string>("myTable") != null)
                    fieldMapping.Add(dr.Field<string>("myTable"), dr.Field<string>("tableName"));
            }

            StringBuilder sb = new StringBuilder();
            foreach (string file in FileNameList)
            {
                filename = System.IO.Path.GetFileNameWithoutExtension(file);

                //TableMigration dts = new TableMigration();

                AutoMappingMethod am = new AutoMappingMethod();

                foreach (string temp in fieldMapping.Keys)
                {
                    if (filename.Contains(temp))
                    {
                        if (filename.StartsWith("ro_type"))
                        {
                            if (!am.RecurciveForDataMappings(temp, file, fieldMapping))
                            {
                                moveFileFromOneToOther(file, errorFolderPath);
                                sb.Append(file + ";");
                            }
                            else
                                moveFileFromOneToOther(file, successFolderPath);
                            break;
                        }
                        else
                        {
                            if (filename.StartsWith("ro"))
                            {
                                if (!am.RecurciveForDataMappings(temp, file, fieldMapping))
                                {
                                    moveFileFromOneToOther(file, errorFolderPath);
                                    sb.Append(file + ";");
                                    flag = false;
                                }
                                else
                                {
                                    moveFileFromOneToOther(file, successFolderPath);
                                }
                                //dts.getDataFromDifferentTable("ro", file, fieldMapping);
                            }
                            else
                            {
                                if (filename.Contains("ro") && !filename.StartsWith(temp))
                                {
                                    continue;
                                }
                                else
                                {
                                    if (!am.RecurciveForDataMappings(temp, file, fieldMapping))
                                    {
                                        moveFileFromOneToOther(file, errorFolderPath);
                                        sb.Append(file + ";");
                                        flag = false;
                                    }
                                    else
                                    {
                                        moveFileFromOneToOther(file, successFolderPath);
                                    }
                                    break;
                                }
                            }
                        }
                    }
                }
            }

            errorFileName = sb.ToString();
            return flag;
        }

        private void moveFileFromOneToOther(string filename,string newPath)
        {
            try
            {
                FileInfo file = new FileInfo(filename);

                string fileN = Path.GetFileNameWithoutExtension(filename);

                file.MoveTo(newPath + "\\" + fileN);

                if (File.Exists(filename))
                    File.Delete(filename);

            }
            catch(Exception ex)
            {

            }

            //file.MoveTo(newPath);
        }


        public bool dataMappingExtend()
        {

            List<string> QueryCollection = new List<string>();

            string queryUpdateForAUType_Company = "update CT_All_Users set AU_Type=1 where AU_Type is null and len(AU_Name)>3";

            string queryUpdateForAUType_Private = "update CT_All_Users set AU_Type=0 where AU_Type is null and len(AU_Name)<=3";

            string queryUpdate1="update CT_Drivers_List set DL_Access=0 where DL_Access is null";

            string queryUpdate2="update CT_Drivers_List set DL_Type=1 where DL_Type is null";

            string queryUpdate3 = "update CT_Drivers_List set DL_Relation=1 where DL_Relation is null";

            string queryUpdate4="update CT_Drivers_List set DL_Access=0 where DL_Access is null";

            string queryUpdate5 ="update CT_Car_Inventory set CI_Mileage=COM.Mileage from (select Max(HS_Odometer) as Mileage,CI_VIN from CT_History_Service left join CT_Car_Inventory on HS_CI_Code=CI_Code group by CI_VIN,CI_Code) as COM,CT_Car_Inventory as CI where CI_Mileage is null and CI.CI_VIN=COM.CI_VIN";

            string queryUpdate6 = "update CT_Car_Inventory set CI_Create_dt=CI_Warr_St_dt where CI_Create_dt is null";

            string queryUpdate7 = "update CT_Car_Inventory set CI_Activate_Tag=1 where CI_Activate_Tag is null";

            string queryUpdate8 = "UPDATE CT_Users_Accnts set UA_UType=1 where UA_UType is null";

            string queryUpdate9="update CT_Phone_List set PL_UType=1 where PL_UType is null";

            string queryUpdate10 = "update CT_Phone_List set PL_Pref=1 where PL_Pref is null";

            string queryUpdate11 = "update CT_Phone_List set PL_Type=3 where PL_Type is null";

            string queryUpdate12 = "update hs set HS_AU_Code=com.CI_AU_Code from"
            +" (select * from CT_Car_Inventory left join CT_History_Service on CI_Code=HS_CI_Code"
            +" where HS_AD_Code="+ CommonHelper.instance.dealerParameter +" and CI_AU_Code is not null and HS_AU_Code is null) as com,CT_History_Service hs where hs.HS_RO_No=com.HS_RO_No";

            string queryUpdate13 = "update hs set HS_Owner=com.CI_AU_Code from"
            + " (select * from CT_Car_Inventory left join CT_History_Service on CI_Code=HS_CI_Code"
            + " where HS_AD_Code=" + CommonHelper.instance.dealerParameter + " and CI_AU_Code is not null and HS_Owner is null) as com,CT_History_Service hs where hs.HS_RO_No=com.HS_RO_No";
            //string queryUpdate12 = "update CT_History_Service set HS_AU_Code=CI.CI_AU_Code from CT_Car_Inventory CI where HS_CI_Code=CI.CI_Code";
            //string queryUpdate12 = "update CT_History_Service set HS_AU_Code=COMBINE.DL_AU_Code "+
            //"from (select distinct HS_Code,DL_AU_Code,DL_M_AU_Code,DL_CI_Code from CT_History_Service "+
            //"left join CT_Car_Inventory CI on HS_CI_Code=CI_Code "+
            //"left join CT_Drivers_List on CI.CI_Code=DL_CI_Code where HS_AD_Code="+ CommonHelper.instance.dealerParameter +" and DL_M_AU_Code is not null "+
            //"and DL_CI_Code is not null) COMBINE "+
            //"where HS_AU_Code=COMBINE.DL_M_AU_Code and HS_CI_Code=COMBINE.DL_CI_Code";

            QueryCollection.Add(queryUpdateForAUType_Company);
            QueryCollection.Add(queryUpdateForAUType_Private);
            QueryCollection.Add(queryUpdate1);
            QueryCollection.Add(queryUpdate2);
            QueryCollection.Add(queryUpdate3);
            QueryCollection.Add(queryUpdate4);
            QueryCollection.Add(queryUpdate5);
            QueryCollection.Add(queryUpdate6);
            QueryCollection.Add(queryUpdate7);
            QueryCollection.Add(queryUpdate8);
            QueryCollection.Add(queryUpdate9);
            QueryCollection.Add(queryUpdate10);
            QueryCollection.Add(queryUpdate11);
            QueryCollection.Add(queryUpdate12);
            QueryCollection.Add(queryUpdate13);
 


            int i=ExecuteSQL.ExecuteQuery(QueryCollection);


            if (i > 0)
                return true;
            else
                return false;
        }


        public bool dataMappingExtend(string sourDataPath)
        {
            string LimitationQuery = "select CT_Fields_Query from [dbo].[CT_DataMapping_Extra] where CT_Fields_Rules is not null and CT_Fields_Rules='RUN' and CT_Fields_Query is not null and CT_Dealer_Name=\'" + CommonHelper.instance.dealerActuallyName+"\';";

            DataTable dt=ExecuteSQL.ExecuteQuery(LimitationQuery);

            List<string> QueryCollection=QueryCollection = CommonHelper.instance.GetListFromTable(dt, "CT_Fields_Query").Distinct().ToList();

            
            //sourDataPath

            String[] FileNameList = Directory.GetFiles(sourDataPath);

            if (FileNameList.Length == 0)
                return true;
            foreach (string file in FileNameList)
            {
                string fileName = inportDataTableIntoDB(file);
                List<string> queryList = new List<string>();
                List<string> deleteList = new List<string>();
                foreach (string query in QueryCollection)
                {
                    
                    if(ExecuteSQL.RunSqlExecution(CommonHelper.instance.paraReplacement(query, fileName)))
                    {
                        moveFileFromOneToOther(file, sourDataPath+"\\Done");
                    }
                    else
                    {
                        moveFileFromOneToOther(file, sourDataPath + "\\UnDone");
                    }
                    //ExecuteSQL.RunSqlExecution("drop table " + fileName);
                }

                string deleteQuery = "drop Table " + fileName;

                bool sucOrNot = ExecuteSQL.RunSqlExecution(deleteQuery);


                if (sucOrNot)
                    return true;
                else
                    return false;
            }
            return true;
        }

        #region EnterIntoDataBase
       
        //public bool scanFolderUpload(string folderPath)
        //{
        //    String[] FileNameList = Directory.GetFiles(folderPath);
        //    bool flag = true;

        //    string nameMapping = string.Empty;

        //    string sql = "select * from temp_table_mapping";
        //    DataTable dtTemp = ExecuteSQL.ExecuteQuery(sql);
        //    Dictionary<string, string> fieldMapping = new Dictionary<string, string>();
        //    string filename = string.Empty;

        //    string tablename = string.Empty;
        //    foreach (DataRow dr in dtTemp.AsEnumerable())
        //    {
        //        if (dr.Field<string>("myTable") != null)
        //            fieldMapping.Add(dr.Field<string>("myTable"), dr.Field<string>("tableName"));
        //    }

        //    foreach (string file in FileNameList)
        //    {
        //        filename = System.IO.Path.GetFileNameWithoutExtension(file);

        //        DifferentTableShift dts = new DifferentTableShift();
                
        //        foreach (string temp in fieldMapping.Keys)
        //        {
        //            if (filename.Contains(temp))
        //            {
        //                if (filename.StartsWith("ro_type"))
        //                {
        //                    fieldMapping.TryGetValue(temp, out tablename);
        //                    break;
        //                }
        //                else
        //                {
        //                    fieldMapping.TryGetValue(temp, out tablename);
        //                    continue;
        //                }
        //            }
        //        }



        //        DataTable dt = importTxtIntoDt(file);



        //        if (!String.IsNullOrEmpty(tablename))
        //        {
        //            flag = ExecuteSQL.ExecuteQuery(dt, tablename);
        //            tablename = string.Empty;
        //        }
        //        if (!flag)
        //        {
        //            return false;
        //        }
        //    }
        //    return flag;
        //}

        #endregion
    }
}
