using MigrationService.DBConnection;
using MigrationService.Helper;
using MigrationService.ServiceLogic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigrationService.ImplementMethod
{
    class AutoMappingMethod
    {
        public bool RecurciveForDataMappings(string tablename, string filepath, Dictionary<string, string> dicTmp)
        {
            try
            {
                TableMigration tm = new TableMigration();
                ImportFromSourceFile isf = new ImportFromSourceFile();
                List<String> tableNameList = new List<string>();
                Dictionary<string, string> fieldMapping = new Dictionary<string, string>();

                

                DataTable regardingOurTable = tm.getMyRegardingTable(tablename);

                DataTable sourceTable = isf.importExlIntoDt(filepath,tablename);

                if (sourceTable == null || sourceTable.Rows.Count <= 0)
                    return true;

                DataTable dtCombineSourceDataWithDataMapping = tm.BuildMyNewStructure(sourceTable, regardingOurTable);

                fieldMapping = GetMappingResults(dtCombineSourceDataWithDataMapping);

                string fieldsGroup = string.Empty;

                dicTmp.TryGetValue(tablename, out fieldsGroup);

                tableNameList = CommonHelper.instance.GetListFromTable(dtCombineSourceDataWithDataMapping, "DM_Fields_Table").Distinct().ToList();



                insertIntoDB(dtCombineSourceDataWithDataMapping, sourceTable, regardingOurTable, tableNameList, fieldMapping);
            
            }
            catch(Exception ex)
            {
                return false;
            }

            return true;
        }

        private bool insertIntoDB(DataTable dtCombineSourceDataWithDataMapping, DataTable sourceTable, DataTable regardingOurTable, List<string> tableNameList, Dictionary<string, string> fieldMapping)
        {
            try 
            {
                StringBuilder sbSecQuery = new StringBuilder();

                string temp = string.Empty;
                string fkey = string.Empty;
                List<string> queryList = new List<string>();
                int calc = 0;
                int sumAll = 0;
                BulkModifyService bks = new BulkModifyService();
                string selectQuery = string.Empty;
                List<string> temporaryList = new List<string>();

                string level1TableName = (from row in regardingOurTable.AsEnumerable()
                                          where row.Field<string>("MR_Table_Sequence") == "1"
                                          select row.Field<string>("DM_Fields_Table")).FirstOrDefault();

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

                    sbSecQuery.Append(bks.GenerateSubQuery(fkey, fieldMapping, dr, regardingOurTable, 2));
                    
                }
                queryList = new List<string>();
                queryList.Add(sbSecQuery.ToString());
                ExecuteSQL.ExecuteQuery(queryList);
            }
            catch(Exception ex)
            {
                return false;
            }
            return true;
        }


        private Dictionary<string, string> GetMappingResults(DataTable dtCombineSourceDataWithDataMapping)
        {
             
            Dictionary<string, string> fieldMapping = new Dictionary<string, string>();
            


            foreach (DataRow dr in dtCombineSourceDataWithDataMapping.AsEnumerable())
            {
                if (dr.Field<string>("DM_Fields_Name") != null)
                    fieldMapping.Add(dr.Field<string>("DM_Fields_Name"), dr.Field<string>("MR_Fields_RegardingName"));
            }

            return fieldMapping;
        }
    }
}
