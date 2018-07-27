using MigrationService.DBConnection;
using MigrationService.Helper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigrationService.ServiceLogic
{
    class BulkModifyService
    {
        public static BulkModifyService instance = new BulkModifyService();

        public bool GetAttributesFromBothSides(DataTable sourceFile,string filedsGroup,out DataTable lsTemp)
        {
            string SqlQuery = "select * from CT_DataMapping_MigrationResouceFilelds cr "
            + "left join CT_DataMapping_MyFields cg on cr.MR_Fields_ID=cg.DM_MyFields_ID "
		    + "left join CT_DataMapping_Group cdg on cdg.DG_ID_Group=cg.DM_Fields_Group_ID where cdg.DG_ResourceTitle=? ";
            ArrayList alTemp=new ArrayList();
            alTemp.Add(filedsGroup);
            DataTable MyTable=ExecuteSQL.ExecuteQuery(SqlQuery, alTemp);

            DataTable dtCol=new DataTable();
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
                                      on sf.Field<string>("ColumnName") equals mt.Field<string>("MR_Fields_RegardingName")
                                      select new 
                                      {
                                          ColumnName=sf.Field<string>("ColumnName")!=null?sf.Field<string>("ColumnName"):string.Empty,
                                          DG_ResourceTitle = mt.Field<string>("DG_ResourceTitle") != null ? mt.Field<string>("DG_ResourceTitle") : string.Empty,
                                          DM_Fields_Table = mt.Field<string>("DM_Fields_Table") != null ? mt.Field<string>("DM_Fields_Table") : string.Empty,
                                          MR_Fields_RegardingName = mt.Field<string>("MR_Fields_RegardingName") != null ? mt.Field<string>("MR_Fields_RegardingName") : string.Empty,
                                          MR_Fields_Rules=mt.Field<string>("MR_Fields_Rules")!= null ? mt.Field<string>("MR_Fields_Rules") : string.Empty,
                                          DM_Fields_Name = mt.Field<string>("DM_Fields_Name") != null ? mt.Field<string>("DM_Fields_Name") : string.Empty,
                                          DM_Fields_PrimaryKeyTable=mt.Field<string>("DM_Fields_PrimaryKeyTable")!=null?mt.Field<string>("DM_Fields_PrimaryKeyTable"):string.Empty,
                                          DM_Fields_PrimaryKey = mt.Field<string>("DM_Fields_PrimaryKey") != null ? mt.Field<string>("DM_Fields_PrimaryKey") : string.Empty,
                                          DM_Table_Level = mt.Field<string>("DM_Table_Level") != null ? mt.Field<string>("DM_Table_Level").ToString() : string.Empty,
                                          DM_Fields_TYPE = mt.Field<string>("DM_Fields_TYPE") != null ? mt.Field<string>("DM_Fields_TYPE").ToString() : string.Empty,
                                       };
                DataTable dtNew = new DataTable();
                dtNew.Columns.Add("ColumnName");
                dtNew.Columns.Add("DG_ResourceTitle");
                dtNew.Columns.Add("DM_Fields_Table");
                dtNew.Columns.Add("MR_Fields_RegardingName");
                dtNew.Columns.Add("MR_Fields_Rules");
                dtNew.Columns.Add("DM_Fields_Name");
                dtNew.Columns.Add("DM_Fields_PrimaryKeyTable");
                dtNew.Columns.Add("DM_Fields_PrimaryKey");
                dtNew.Columns.Add("DM_Table_Level");
                dtNew.Columns.Add("DM_Fields_TYPE");
               
                foreach (var item in fieldsAssociate) 
                {
                    DataRow row = dtNew.NewRow();
                    row["ColumnName"] = item.ColumnName;
                    row["DG_ResourceTitle"] = item.DG_ResourceTitle;
                    row["DM_Fields_Table"] = item.DM_Fields_Table;
                    row["MR_Fields_RegardingName"] = item.MR_Fields_RegardingName;
                    row["MR_Fields_Rules"] = item.MR_Fields_Rules;
                    row["DM_Fields_Name"] = item.DM_Fields_Name;
                    row["DM_Fields_PrimaryKeyTable"] = item.DM_Fields_PrimaryKeyTable;
                    row["DM_Fields_PrimaryKey"] = item.DM_Fields_PrimaryKey;
                    row["DM_Table_Level"] = item.DM_Table_Level;
                    row["DM_Fields_TYPE"] = item.DM_Fields_TYPE;
                    dtNew.Rows.Add(row);
                }

                reconForInvalidFields(dtCol, dtNew);

                ArrayList alQueryList= BulkLoadToDB(sourceFile, dtNew, MyTable);

                lsTemp = new DataTable();

                if (alQueryList != null && alQueryList.Count > 0)
                {
                    //lsTemp.Rows.Add();
                    return true;
                }
                else
                    return false;
                //StringBuilder query = new StringBuilder();

                //query.Append("select * from v_userRegardingInfo where AU_Code in (");

                //for (int i = 1; i <= alQueryList.Count;i++ )
                //{
                    
                //    if (i == alQueryList.Count)
                //    {
                //        query.Append("?");
                //    }
                //    else
                //    {
                //        query.Append("?,");
                //    }
                //}
                //query.Append(")");

                //lsTemp = ExecuteSQL.ExecuteQuery(query.ToString(), alQueryList);
            }
            else
            {
                lsTemp = new DataTable();
                return false;
            }
             
        }

        public ArrayList BulkLoadToDB(DataTable sourceData, DataTable dtNewStructure, DataTable dtMappingRelationShip)
        {
            List<String> tableNameList = new List<string>();
            List<string> queryList = new List<string>();
            Dictionary<string, string> fieldMapping = new Dictionary<string, string>();
            string queryForFirstLevel = string.Empty;
            string queryForSecondLevel = string.Empty;

            tableNameList = CommonHelper.instance.GetListFromTable(dtNewStructure, "DM_Fields_Table").Distinct().ToList();

            foreach(DataRow dr in dtNewStructure.AsEnumerable())
            {
                if(dr.Field<string>("DM_Fields_Name")!=null)
                    fieldMapping.Add(dr.Field<string>("DM_Fields_Name"),dr.Field<string>("MR_Fields_RegardingName"));
            }

            ArrayList alList= GenerateQuery(tableNameList, fieldMapping, sourceData, dtMappingRelationShip);

            return alList;
        }

        //public bool 

        public ArrayList GenerateQuery(List<string> tbNameList, Dictionary<string, string> regardingFileds, DataTable sourceData, DataTable dtMappingRelationShip)
        {
            //string sqlQuery = "select c.name from sysobjects o,syscolumns c where o.id =c.id and o.name=?";
            //List<string> lsFields = ExecuteSQL.ExecuteQuery(sqlQuery, tableName, "name");
            ArrayList alKeyList = new ArrayList();
            DataTable dtTemp = new DataTable();
            string level1TableName = (from row in dtMappingRelationShip.AsEnumerable()
                                      where row.Field<string>("DM_Table_Level") == "1"
                                      select row.Field<string>("DM_Fields_Table")).FirstOrDefault();

            if(tbNameList.Contains(level1TableName))
                tbNameList.Remove(level1TableName);
            
            StringBuilder sbSecQuery = new StringBuilder();
            string temp = string.Empty;
            string fkey = string.Empty;
            List<string> queryList = new List<string>();
            int calc = 0;
            int sumAll = 0;

            removeEmpty(sourceData);

            string query = "select AU_Name,PL_Number from CT_All_Users cau left join CT_Phone_List cat on cau.AU_Code= cat.PL_AU_AD_Code";
            dtTemp = ExecuteSQL.ExecuteQuery(query);



            
            foreach (DataRow dr in sourceData.AsEnumerable())
            {
                sumAll++;
                calc++;
                if (calc >= 1000)
                {
                    queryList.Add(sbSecQuery.ToString());
                    sbSecQuery = new StringBuilder();
                    calc = 0;
                }
                temp=MainGeneration(string.Empty, level1TableName, dtMappingRelationShip, regardingFileds, dr);
                fkey = ExecuteSQL.SaveReturnIndentityKey(temp.ToString(),level1TableName);
                sbSecQuery.Append(GenerateSecondQuery(tbNameList, fkey,regardingFileds, dr, dtMappingRelationShip));
                alKeyList.Add(fkey);
            }

            queryList.Add(sbSecQuery.ToString());

            int insertResult= ExecuteSQL.ExecuteQuery(queryList);

            if (insertResult > 0)
                return alKeyList;
            else
                return null;
        }

        public string GenerateSecondQuery(List<string> tbNameList, string fkey, Dictionary<string,string> regardingFields, DataRow row, DataTable dtMappingRelationShip)
        {
            StringBuilder sbSum = new StringBuilder();
            foreach (string name in tbNameList)
            {
                sbSum.Append(MainGeneration(fkey,name, dtMappingRelationShip, regardingFields, row)); 
            }
            return sbSum.ToString();
        }

        public string GenerateSubQuery(string fkey, Dictionary<string, string> regardingFields, DataRow row, DataTable dtMappingRelationShip, int tableSeqence)
        {
            StringBuilder sbSum = new StringBuilder();

            List<string> ls = (from nRow in dtMappingRelationShip.AsEnumerable()
                               where nRow.Field<string>("MR_Table_Sequence") == tableSeqence.ToString() && nRow.Field<string>("MR_Fields_Description")==CommonHelper.instance.dealerActuallyName
                               select nRow.Field<string>("DM_Fields_Table")).Distinct().ToList();

            string selectQuery = string.Empty;
            string insertQuery = string.Empty;

            List<string> temporaryList = new List<string>();

            foreach (string name in ls)
            {
                insertQuery = MainGeneration(fkey, name, dtMappingRelationShip, regardingFields, row,out selectQuery);

                if (ExecuteSQL.RunSqlExcuteScalar(selectQuery) < 1 && !temporaryList.Contains(selectQuery))
                {
                    temporaryList.Add(selectQuery);
                    sbSum.Append(insertQuery);
                }
                
            }

            List<string> lsNextLv = (from nRow in dtMappingRelationShip.AsEnumerable()
                                     where nRow.Field<string>("MR_Table_Sequence") == (tableSeqence + 1).ToString()
                                     select nRow.Field<string>("DM_Fields_Table")).Distinct().ToList();

            if(lsNextLv!=null && lsNextLv.Count>0)
            {
                foreach (string name in ls)
                {
                    sbSum.Append(GenerateSubQuery(fkey, regardingFields, row, dtMappingRelationShip, tableSeqence + 1));
                }
            }

            return sbSum.ToString();
        }
        public string GenerateSecondQuery(string tableName, string fkey, Dictionary<string, string> regardingFields, DataRow row, DataTable dtMappingRelationShip)
        {
            return MainGeneration(fkey, tableName, dtMappingRelationShip, regardingFields, row);
        }

        public void setIdentityOn(string tbname)
        {
            string query = "set identity_insert "+tbname+" on";
            ExecuteSQL.RunSqlExecution(query);
        }
        public string MainGeneration(string fkey, string tbname, DataTable dtAllMappingRelationShip, Dictionary<string, string> regardingFileds,DataRow dr)
        {
            try
            {
                string value = string.Empty;
                string rule = string.Empty;

                DataTable dtMappingRelationShip = (from rows in dtAllMappingRelationShip.AsEnumerable()
                                                   where rows.Field<string>("DM_Fields_Table") == tbname
                                                   select rows).CopyToDataTable<DataRow>();
                int calc = 0;
                string endIcon = string.Empty;

                StringBuilder sbTemp = new StringBuilder();
                sbTemp.Append("insert into " + tbname + "(");

                foreach (DataRow row in dtMappingRelationShip.AsEnumerable())
                {
                    calc++;
                    endIcon = (calc == dtMappingRelationShip.Rows.Count ? "" : ",");
                    if (row["MR_Fields_Rules"].ToString() != "IDENTITY")
                    {
                        sbTemp.Append(row["DM_Fields_Name"] + endIcon);
                    }
                }
                calc = 0;

                sbTemp.Append(") values(");

                foreach (DataRow row in dtMappingRelationShip.AsEnumerable())
                {
                    calc++;

                    endIcon = (calc == dtMappingRelationShip.Rows.Count ? "" : ",");
                    if (row["MR_Fields_Rules"].ToString() != "IDENTITY")
                    {
                        if (row["MR_Fields_Rules"].ToString().StartsWith("DEFAULT"))
                        {
                            if (row["MR_Fields_Rules"].ToString().Split('|')[1] == "INT")
                                sbTemp.Append(row["MR_Fields_Rules"].ToString().Split('|')[2].Replace("{{DealerPara}}", CommonHelper.instance.dealerParameter) + endIcon);
                            else if (row["MR_Fields_Rules"].ToString().Split('|')[1] == "DATETIME")
                                sbTemp.Append(row["MR_Fields_Rules"].ToString().Split('|')[2] + endIcon);
                        }
                        else if (row["MR_Fields_Rules"].ToString().StartsWith("LOGIC"))
                        {
                            if (regardingFileds.TryGetValue(row["DM_Fields_Name"] != null ? row["DM_Fields_Name"].ToString() : "", out value))
                            {
                                if (row["MR_Fields_Rules"].ToString().StartsWith("LOGICQUERY"))
                                {
                                    string query = row["MR_Fields_Rules"].ToString().Split('|')[1].ToString();

                                    string regardingField = ExecuteSQL.GetRegardingField(query.Replace("{{DealerPara}}", CommonHelper.instance.dealerParameter), dr, value);

                                    if (string.IsNullOrEmpty(regardingField))
                                        regardingField = "null";


                                    if (row["DM_Fields_TYPE"].ToString() == "INT")
                                        sbTemp.Append(regardingField + endIcon);
                                    else
                                        sbTemp.Append("\'" + regardingField + "\'" + endIcon);
                                }
                                else
                                {
                                    bool getLogicValue = true;

                                    for (int i = 1; i < row["MR_Fields_Rules"].ToString().Split('|').Count(); i++)
                                    {
                                        if (row["MR_Fields_Rules"].ToString().Split('|')[i].Split(':')[0] == dr[value].ToString().Trim())
                                        {
                                            sbTemp.Append(row["MR_Fields_Rules"].ToString().Split('|')[i].Split(':')[1] + endIcon);
                                            getLogicValue = false;
                                            break;
                                        }
                                    }
                                    if (getLogicValue)
                                        sbTemp.Append("null" + endIcon);
                                }
                            }
                            else
                            {
                                sbTemp.Append(0 + endIcon);
                            }
                        }
                        else if (row["MR_Fields_Rules"].ToString().StartsWith("FK"))
                        {
                            if (!String.IsNullOrEmpty(fkey))
                                sbTemp.Append(fkey + endIcon);
                            else
                                sbTemp.Append("null" + endIcon);
                        }
                        else if (!String.IsNullOrEmpty(row["DM_Fields_TYPE"].ToString()))
                        {
                            if (regardingFileds.TryGetValue(row["DM_Fields_Name"] != null ? row["DM_Fields_Name"].ToString() : "", out value))
                            {
                                if (row["DM_Fields_TYPE"].ToString() == "DATETIME")
                                    sbTemp.Append((dr[value] != "" ? dr[value].ToString() : "null") + endIcon);
                                else if (row["DM_Fields_TYPE"].ToString() == "INT")
                                    sbTemp.Append(((dr[value] != null && dr[value].ToString().Trim() != string.Empty) ? dr[value].ToString() : "1") + endIcon);
                                else
                                    sbTemp.Append("\'" + dr[value].ToString() + "\'" + endIcon);
                            }
                            else
                            {
                                if (row["DM_Fields_TYPE"].ToString() == "DATETIME")
                                    sbTemp.Append("null" + endIcon);
                                else if (row["DM_Fields_TYPE"].ToString() == "INT" || row["DM_Fields_TYPE"].ToString() == "DECIMAL")
                                    sbTemp.Append("1" + endIcon);
                                else
                                    sbTemp.Append("\'\'" + endIcon);
                            }
                        }
                        else
                        {
                            if (regardingFileds.TryGetValue(row["DM_Fields_Name"] != null ? row["DM_Fields_Name"].ToString() : "", out value))
                            {
                                sbTemp.Append("\'" + dr[value].ToString() + "\'" + endIcon);
                            }
                            else
                            {
                                sbTemp.Append("null" + endIcon);
                            }
                        }
                    }
                }
                calc = 0;
                sbTemp.Append(")");
                return sbTemp.ToString().Replace("{{DealerPara}}", CommonHelper.instance.dealerParameter);
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }

        public string MainGeneration(string fkey, string tbname, DataTable dtAllMappingRelationShip, Dictionary<string, string> regardingFileds, DataRow dr,out string selectQuery)
        {
            try
            {
                string value = string.Empty;
                string rule = string.Empty;
                DataTable dtMappingRelationShip = (from rows in dtAllMappingRelationShip.AsEnumerable()
                                                   where rows.Field<string>("DM_Fields_Table") == tbname
                                                   select rows).CopyToDataTable<DataRow>();
                int calc = 0;
                string endIcon = string.Empty;
                string endIconT = string.Empty;
                string pkName = (from rows in dtAllMappingRelationShip.AsEnumerable()
                                 where rows.Field<string>("MR_Fields_Rules") == "IDENTITY" && rows.Field<string>("DM_Fields_Table") == tbname
                                 select rows.Field<string>("DM_Fields_Name")).FirstOrDefault();

                StringBuilder sbTemp = new StringBuilder();
                sbTemp.Append("insert into " + tbname + "(");
                StringBuilder sbTempValidateQuery = new StringBuilder();
                sbTempValidateQuery.Append("select " + pkName + " from " + tbname + " where ");

                foreach (DataRow row in dtMappingRelationShip.AsEnumerable())
                {
                    calc++;
                    endIcon = (calc == dtMappingRelationShip.Rows.Count ? "" : ",");
                    if (row["MR_Fields_Rules"].ToString() != "IDENTITY")
                    {
                        sbTemp.Append(row["DM_Fields_Name"] + endIcon);
                    }
                }
                calc = 0;

                sbTemp.Append(") values(");

                foreach (DataRow row in dtMappingRelationShip.AsEnumerable())
                {
                    calc++;

                    endIcon = (calc == dtMappingRelationShip.Rows.Count ? "" : ",");
                    endIconT = (calc == dtMappingRelationShip.Rows.Count ? "" : " and ");
                    if (row["MR_Fields_Rules"].ToString() != "IDENTITY")
                    {
                        if (row["MR_Fields_Rules"].ToString().StartsWith("DEFAULT"))
                        {
                            if (row["MR_Fields_Rules"].ToString().Split('|')[1] == "INT")
                                sbTemp.Append(row["MR_Fields_Rules"].ToString().Split('|')[2].Replace("{{DealerPara}}", CommonHelper.instance.dealerParameter) + endIcon);
                            else if (row["MR_Fields_Rules"].ToString().Split('|')[1] == "DATETIME")
                                sbTemp.Append(row["MR_Fields_Rules"].ToString().Split('|')[2] + endIcon);
                        }
                        else if (row["MR_Fields_Rules"].ToString().StartsWith("LOGIC"))
                        {
                            if (regardingFileds.TryGetValue(row["DM_Fields_Name"] != null ? row["DM_Fields_Name"].ToString() : "", out value))
                            {
                                if (row["MR_Fields_Rules"].ToString().StartsWith("LOGICQUERY"))
                                {
                                    string query = row["MR_Fields_Rules"].ToString().Split('|')[1].ToString();

                                    string regardingField = ExecuteSQL.GetRegardingField(query.Replace("{{DealerPara}}", CommonHelper.instance.dealerParameter), dr, value);

                                    if (string.IsNullOrEmpty(regardingField))
                                    {
                                        string insertField = row["MR_Fields_Insert_Query"].ToString();
                                        string insertPara= row["MR_Fields_Insert_Parameter"].ToString();
                                        string selectField=row["MR_Fields_Select_Query"].ToString();
                                        string selectPara=row["MR_Fields_Select_Parameter"].ToString();

                                        regardingField = ExecuteSQL.SaveReturnIndentityKey(insertField.Replace("{{DealerPara}}", CommonHelper.instance.dealerParameter), insertPara, selectField.Replace("{{DealerPara}}", CommonHelper.instance.dealerParameter), selectPara,dr);
                                    }


                                    if (row["DM_Fields_TYPE"].ToString() == "INT")
                                    {
                                        sbTemp.Append(regardingField + endIcon);
                                        if (row["MR_Fields_Select"].ToString()!="2")
                                        sbTempValidateQuery.Append(row["DM_Fields_Name"].ToString() + "=" + regardingField + endIconT);
                                    }
                                    else
                                    {
                                        sbTemp.Append("\'" + regardingField + "\'" + endIcon);
                                        if (row["MR_Fields_Select"].ToString() != "2")
                                        sbTempValidateQuery.Append(row["DM_Fields_Name"].ToString() + "=\'" + regardingField + "\'" + endIconT);
                                    }
                                }
                                else
                                {
                                    bool getLogicValue = true;

                                    for (int i = 1; i < row["MR_Fields_Rules"].ToString().Split('|').Count(); i++)
                                    {
                                        if (row["MR_Fields_Rules"].ToString().Split('|')[i].Split(':')[0] == dr[value].ToString().Trim())
                                        {
                                            sbTemp.Append(row["MR_Fields_Rules"].ToString().Split('|')[i].Split(':')[1] + endIcon);
                                            getLogicValue = false;
                                            break;
                                        }
                                    }
                                    if (getLogicValue)
                                        sbTemp.Append("null" + endIcon);
                                }
                            }
                            else
                            {
                                sbTemp.Append(0 + endIcon);
                            }
                        }
                        else if (row["MR_Fields_Rules"].ToString().StartsWith("FK"))
                        {
                            if (!String.IsNullOrEmpty(fkey))
                            {
                                sbTemp.Append(fkey + endIcon);
                                if (row["MR_Fields_Select"].ToString() != "2")
                                sbTempValidateQuery.Append(row["DM_Fields_Name"].ToString() + "=" + fkey + endIconT);
                            }
                            else
                            {
                                sbTemp.Append("null" + endIcon);
                                if (row["MR_Fields_Select"].ToString() != "2")
                                sbTempValidateQuery.Append(row["DM_Fields_Name"].ToString() + "=null" + endIconT);
                            }
                        }
                        else if (!String.IsNullOrEmpty(row["DM_Fields_TYPE"].ToString()))
                        {
                            if (regardingFileds.TryGetValue(row["DM_Fields_Name"] != null ? row["DM_Fields_Name"].ToString() : "", out value))
                            {
                                if (row["DM_Fields_TYPE"].ToString() == "DATETIME")
                                {
                                    sbTemp.Append((dr[value].ToString().Trim() != "" ? dr[value].ToString() : "null") + endIcon);
                                    if (row["MR_Fields_Select"].ToString() != "2")
                                        sbTempValidateQuery.Append(row["DM_Fields_Name"].ToString() + "=" + "\'" + (dr[value].ToString().Trim() != "" ? dr[value].ToString() : "null") + "\'" + endIconT);
                                }
                                else if (row["DM_Fields_TYPE"].ToString() == "INT")
                                {
                                    sbTemp.Append(((dr[value] != null && dr[value].ToString().Trim() != string.Empty) ? dr[value].ToString() : "1") + endIcon);
                                    if (row["MR_Fields_Select"].ToString() != "2")
                                    sbTempValidateQuery.Append(row["DM_Fields_Name"].ToString() + "=" + ((dr[value] != null && dr[value].ToString().Trim() != string.Empty) ? dr[value].ToString() : "1") + endIconT);
                                }
                                else
                                {
                                    sbTemp.Append("\'" + dr[value].ToString() + "\'" + endIcon);
                                    if (row["MR_Fields_Select"].ToString() != "2")
                                    sbTempValidateQuery.Append(row["DM_Fields_Name"].ToString() + "=" + "\'" + dr[value].ToString() + "\'" + endIconT);
                                }
                            }
                            else
                            {
                                if (row["DM_Fields_TYPE"].ToString() == "DATETIME")
                                    sbTemp.Append("null" + endIcon);
                                else if (row["DM_Fields_TYPE"].ToString() == "INT" || row["DM_Fields_TYPE"].ToString() == "DECIMAL")
                                    sbTemp.Append("1" + endIcon);
                                else
                                    sbTemp.Append("\'\'" + endIcon);
                            }
                        }
                        else
                        {
                            if (regardingFileds.TryGetValue(row["DM_Fields_Name"] != null ? row["DM_Fields_Name"].ToString() : "", out value))
                            {
                                sbTemp.Append("\'" + dr[value].ToString() + "\'" + endIcon);
                                if (row["MR_Fields_Select"].ToString() != "2")
                                sbTempValidateQuery.Append(row["DM_Fields_Name"].ToString() + "=" + "\'" + dr[value].ToString() + "\'" + endIconT);
                            }
                            else
                            {
                                sbTemp.Append("null" + endIcon);
                            }
                        }
                    }
                }
                calc = 0;
                sbTemp.Append(")");

                selectQuery = sbTempValidateQuery.Append(")").ToString();

                if (selectQuery.EndsWith("and )"))
                    selectQuery = selectQuery.Replace("and )", "");
                else if (selectQuery.EndsWith(")"))
                    selectQuery = selectQuery.Substring(0, selectQuery.Length - 1);

                selectQuery = selectQuery.Replace("=null", " is null");
                return sbTemp.ToString().Replace("{{DealerPara}}", CommonHelper.instance.dealerParameter);
            }
            catch(Exception ex)
            {
                selectQuery = "";
                return "";
            }

            
        }

        public List<string> getFieldsList(string tableName)
        {
            string sqlQuery = "select c.name from sysobjects o,syscolumns c where o.id =c.id and o.name=?";
            List<string> lsFields = ExecuteSQL.ExecuteQuery(sqlQuery, tableName,"name");
            return lsFields;
        }

        public void removeEmpty(DataTable dt)
        {
            List<DataRow> removelist = new List<DataRow>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                bool rowdataisnull = true;
                for (int j = 0; j < dt.Columns.Count; j++)
                {

                    if (!string.IsNullOrEmpty(dt.Rows[i][j].ToString().Trim()))
                    {

                        rowdataisnull = false;
                    }

                }
                if (rowdataisnull)
                {
                    removelist.Add(dt.Rows[i]);
                }

            }
            for (int i = 0; i < removelist.Count; i++)
            {
                dt.Rows.Remove(removelist[i]);
            }
        }
        public void reconForInvalidFields(DataTable beforeData, DataTable newGenerated)
        {
            StringBuilder sb = new StringBuilder();
            if (beforeData.Rows.Count == 0 || newGenerated.Rows.Count == 0)
                return;
            sb.Append("The below fields can not find regarding configuration in DB");

            var onlyInBeforeData = from before in beforeData.AsEnumerable()
                                   from newlis in newGenerated.AsEnumerable()
                                   where !newlis.Field<string>("DG_ResourceTitle").Contains(before.Field<string>("ColumnName"))
                                   select new
                                   {
                                        reconResult=before.Field<string>("ColumnName")
                                   };



            foreach (var item in onlyInBeforeData.Distinct())
            {
                sb.Append(item.reconResult+";");
            }

            CommonHelper.instance.LogWrriten(sb.ToString(), @"C:/Users/Administrator/Desktop/Issues/");
        }
        public Boolean BulkInsertToRegardingTables()
        {

            return true;
        }


    }
}
