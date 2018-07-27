using CRMTreeDatabase;
using Newtonsoft.Json;
using PetaPoco;
using System.Dynamic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMTree.DAL
{
    public class DL_DataGrid
    {
        public void DoPage(dynamic data, bool Interna)
        {
            Page<dynamic> pager = new Page<dynamic>();
            pager.Items = new List<dynamic>();

            var db = DBCRMTree.GetInstance();
            var MF_FL_FB_Code = data.MF_FL_FB_Code;
            var rp_code = 0;
            try
            {
                rp_code = db.ExecuteScalar<int>("select FL_RP_Code from CT_Fields_lists where FL_Code=@0", MF_FL_FB_Code);
            }
            catch (Exception)
            {
                throw new Exception(Interna ? "Invalid Field List！" : "无效的字段列表！");
            }

            var sql_query = string.Empty;
            try
            {
                //CRMTree.Function fun = new CRMTree.Function();
                //sql_query = fun.getReprotQuery(rp_code);
            }
            catch (Exception)
            {
                throw new Exception(Interna ? "Get the query exception！" : "获得查询语句异常！");
            }

            var sql = PetaPoco.Sql.Builder;
            var sort = (string)data.sort;
            var order = (string)data.order;

            //var paramData = JsonConvert.DeserializeObject<EX_CT_Params>(JsonConvert.SerializeObject(data.paramsData));

            var queryParams = JsonConvert.DeserializeObject<List<EX_Param>>(JsonConvert.SerializeObject(data.queryParams));
            var dt = new DataTable();
            var row = dt.NewRow();
            var index = 0;
            foreach (EX_Param queryParam in queryParams)
            {
                sql_query = sql_query.Replace("@" + queryParam.EX_Name, "@" + index);
                dt.Columns.Add(queryParam.EX_Name, getDataType(queryParam.EX_DataType));
                row[queryParam.EX_Name] = queryParam.EX_Value;
                index++;
            }

            sql.Append("select * from(" + sql_query, row.ItemArray);
            if (!string.IsNullOrWhiteSpace(sort))
            {
                sql.Append(string.Format(" order by {0} {1}", sort, order));
            }
            sql.Append(")peta_table");

            pager = db.Page<dynamic>((long)data.pageNumber, (long)data.pageSize, sql);

            var pageInfo = JsonConvert.SerializeObject(new { total = pager.TotalItems, rows = pager.Items });
            //Response.Write(pageInfo);
        }
        private Type getDataType(string dataTypeName)
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
    }
}
