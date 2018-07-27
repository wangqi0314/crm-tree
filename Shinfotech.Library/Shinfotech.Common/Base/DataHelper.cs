using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ShInfoTech.Common
{
    /// <summary>
    /// 本类用作TataTable 跟IList之间的转换
    /// </summary>
    public class DataHelper
    {
        #region 列表对象转换为数据表格
        /// <summary>
        /// 列表对象转换为数据表格
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static DataTable ConvertToTable<T>(IList<T> list)
        {
            DataTable table = CreateTable<T>();
            Type entityType = typeof(T);
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(entityType);

            foreach (T item in list)
            {
                DataRow row = table.NewRow();

                foreach (PropertyDescriptor prop in properties)
                {
                    row[prop.Name] = prop.GetValue(item);
                }

                table.Rows.Add(row);
            }

            return table;
        }

        #endregion

        #region 数据集表格转换为集合
        public static IList<T> ConvertToList<T>(string sql)
        {
            DataSet ds = SqlHelper.ExecuteDataset(sql);
            return ConvertToList<T>(ds, 0);
        }
        /// <summary>
        /// 数据集合的第一张表转换为集合列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static IList<T> ConvertToList<T>(DataSet ds)
        {
            return ConvertToList<T>(ds, 0);
        }
        /// <summary>
        /// 数据集合的第 i 张表转换为集合列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ds"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        public static IList<T> ConvertToList<T>(DataSet ds, int i)
        {
            if (ds == null || ds.Tables.Count == 0) { return null; }
            if (ds.Tables[i] == null || ds.Tables[i].Rows.Count <= 0) { return null; }
            return ConvertToList<T>(ds.Tables[i]);
        }
        /// <summary>
        /// DataTable转换为IList
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="table"></param>
        /// <returns></returns>
        public static IList<T> ConvertToList<T>(DataTable table)
        {
            if (table == null)
            {
                return null;
            }
            List<DataRow> rows = new List<DataRow>();
            foreach (DataRow row in table.Rows)
            {
                rows.Add(row);
            }
            return ConvertTo<T>(rows);
        }
        /// <summary>
        /// 将数据行转换为列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static IList<T> ConvertTo<T>(IList<DataRow> rows)
        {
            IList<T> list = null;
            if (rows != null)
            {
                list = new List<T>();
                foreach (DataRow row in rows)
                {
                    T item = ConvertToObject<T>(row);
                    list.Add(item);
                }
            }
            return list;
        }
        #endregion

        #region 数据转换为对象
        /// <summary>
        /// 执行配置好的sql 转换为对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static T ConvertToObject<T>(string sql)
        {
            DataSet ds = SqlHelper.ExecuteDataset(sql);
            return ConvertToObject<T>(ds, 0);
        }
        public static T ConvertToObject<T>(DataSet ds)
        {
            return ConvertToObject<T>(ds, 0);
        }
        /// <summary>
        /// 数据集合的第 i 张表转换为集合列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ds"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        public static T ConvertToObject<T>(DataSet ds, int i)
        {
            if (ds == null || ds.Tables.Count == 0) { return default(T); }
            if (ds.Tables[i] == null || ds.Tables[i].Rows.Count <= 0) { return default(T); }
            return ConvertToObject<T>(ds.Tables[0].Rows[0]);
        }
        /// <summary>
        ///  DataTable行转换为对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="row"></param>
        /// <returns></returns>
        public static T ConvertToObject<T>(DataRow row)
        {
            T obj = default(T);
            if (row != null)
            {
                obj = Activator.CreateInstance<T>();
                foreach (DataColumn column in row.Table.Columns)
                {
                    try
                    {
                        PropertyInfo prop = obj.GetType().GetProperty(column.ColumnName);
                        object value = row[column.ColumnName];
                        if (prop != null && value != null && value != DBNull.Value)
                        {
                            prop.SetValue(obj, value, null);
                            // 反射属性的特性
                            //object[] o = prop.GetCustomAttributes(true);
                        }
                    }
                    catch { throw; }
                }
            }
            return obj;
        }
        #endregion

        #region 数据转换为JSON  以及分页
        /// <summary>
        /// 执行配置好的SQL 转换为JSON 字符串
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static string ConvertToJSON(string sql)
        {
            DataSet ds = SqlHelper.ExecuteDataset(sql);
            return ConvertToJSON(ds, 0);
        }
        /// <summary>
        /// 数据集表格转换为JSON字符串
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static string ConvertToJSON(DataSet ds)
        {
            return ConvertToJSON(ds, 0);
        }
        /// <summary>
        /// 数据集表格转换为JSON字符串
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        public static string ConvertToJSON(DataSet ds, int i)
        {
            if (ds == null) { return null; }
            if (ds.Tables[i] == null || ds.Tables[i].Rows.Count <= 0) { return null; }
            return ConvertToJSON(ds.Tables[i]);
        }
        /// <summary>
        /// 数据集表格转换为JSON字符串
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ConvertToJSON(DataTable dt)
        {
            if (dt == null || dt.Rows.Count <= 0) { return null; }
            string data = "[";
            foreach (DataRow row in dt.Rows)
            {
                data += ConvertToJSON(row) + ",";
            }
            data = data.Substring(0, data.Length - 1) + "]";
            return data;
        }

        /// <summary>
        /// 数据集表格转换为JSON字符串
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public static string ConvertToJSON(DataRow row)
        {
            if (row == null) { return null; }
            string rowJson = "{";
            foreach (DataColumn column in row.Table.Columns)
            {
                try
                {
                    rowJson += "\"" + column.ColumnName.Trim() + "\":\"" + row[column.ColumnName].ToString().Trim() + "\",";
                }
                catch { throw; }
            }
            rowJson = rowJson.Substring(0, rowJson.Length - 1) + "}";
            return rowJson;
        }


        public static string ConvertToJSONPage(string sql)
        {
            DataSet ds = SqlHelper.ExecuteDataset(sql);
            if (ds == null || ds.Tables.Count == 0) { return null; }
            if (ds.Tables[0] == null || ds.Tables[0].Rows.Count <= 0) { return null; }
            if (ds.Tables[1] == null || ds.Tables[1].Rows.Count <= 0) { return null; }
            return ConvertToJSONPage(ds.Tables[0], ds.Tables[1]);
        }

        /// <summary>
        /// 数据集表格转换为JSON字符串 并且分页
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static string ConvertToJSONPage(DataSet ds)
        {
            if (ds == null || ds.Tables.Count == 0) { return null; }
            if (ds.Tables[0] == null || ds.Tables[0].Rows.Count <= 0) { return null; }
            if (ds.Tables[1] == null || ds.Tables[1].Rows.Count <= 0) { return null; }
            return ConvertToJSONPage(ds.Tables[0], ds.Tables[1]);
        }
        /// <summary>
        /// 数据集表格转换为JSON字符串 并且分页
        /// </summary>
        /// <param name="DataDt"></param>
        /// <param name="PageDt"></param>
        /// <returns></returns>
        public static string ConvertToJSONPage(DataTable DataDt, DataTable PageDt)
        {
            if (DataDt == null || DataDt.Rows.Count <= 0) { return null; }
            if (PageDt == null || PageDt.Rows.Count <= 0) { return null; }
            string data = "\"dataJson\":[";
            foreach (DataRow row in DataDt.Rows)
            {
                data += ConvertToJSON(row) + ",";
            }
            data = data.Substring(0, data.Length - 1) + "]";
            return "{\"dataCount\":" + DataDt.Rows.Count + "," + ConvertToPage(PageDt) + "" + data + "}";
        }
        /// <summary>
        /// 数据集表格转换为JSON字符串 并且分页
        /// </summary>
        /// <param name="PageDt"></param>
        /// <returns></returns>
        private static string ConvertToPage(DataTable PageDt)
        {
            if (PageDt == null || PageDt.Rows.Count <= 0) { return null; }
            string rowJson = string.Empty;
            foreach (DataRow row in PageDt.Rows)
            {
                foreach (DataColumn column in row.Table.Columns)
                {
                    try
                    {
                        rowJson += "\"" + column.ColumnName.Trim() + "\":\"" + row[column.ColumnName].ToString().Trim() + "\",";
                    }
                    catch { throw; }
                }
            }
            return rowJson;
        }

        #endregion

        #region 内部私有化

        /// <summary>
        /// 根据提供的泛型类，实例化一个对应列属性的表格 DataTable 对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        private static DataTable CreateTable<T>()
        {
            Type entityType = typeof(T);
            DataTable table = new DataTable(entityType.Name);
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(entityType);

            foreach (PropertyDescriptor prop in properties)
            {
                table.Columns.Add(prop.Name, prop.PropertyType);
            }

            return table;
        }

        #endregion
    }
}
