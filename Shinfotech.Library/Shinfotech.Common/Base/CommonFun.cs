using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ShInfoTech.Common
{

    
    public class CommonFun
    { 
        /// <summary>
        /// MD5　32位加密
        /// </summary>
        /// <param name="str">需要加密字符串</param>
        /// <returns>加密字符串</returns>
        /// Create By : Liu Jijun
        /// Create Time: 2014/08/07
        public static string GetMd5Str32(string str)
        {
            MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider(); 
            char[] temp = str.ToCharArray();
            byte[] buf = new byte[temp.Length];
            for (int i = 0; i < temp.Length; i++)
            {
                buf[i] = (byte)temp[i];
            }
            byte[] data = md5Hasher.ComputeHash(buf); 
            StringBuilder sBuilder = new StringBuilder(); 
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            } 
            return sBuilder.ToString();
        }


        /// <summary>
        /// 让SQL语句具有分页功能
        /// </summary>
        /// <param name="str_oldSql">原SQL语句</param>
        /// <param name="int_pageIndex">页码</param>
        /// <param name="int_pageSize">每页大小</param>
        /// <param name="str_orderByField">排序字段</param>
        /// <param name="bool_IsDesc">是否倒排</param>
        /// <returns></returns>
        public static string EnablePageForSQL(string str_oldSql, int int_pageIndex, int int_pageSize, string str_orderByField, bool bool_IsDesc)
        {
            if (str_oldSql.Trim() == string.Empty)
            {
                return "";
            }
            string strOrderBy = "";//排序键

            strOrderBy = " order by " + str_orderByField + (bool_IsDesc ? " desc " : " asc ");
            int_pageIndex = int_pageIndex - 1;
            int int_begin = (int_pageIndex * int_pageSize) + 1;
            int int_end = (int_pageIndex * int_pageSize) + int_pageSize;

            string formatSql = str_oldSql.Substring(str_oldSql.IndexOf("select") + 7);
            string str = String.Format(@"select * from ( select row_number() over ({0}) as ordernumber, {1} ) as tempTableName 
                                        where orderNumber between {2} and {3}", strOrderBy, formatSql, int_begin, int_end);

            return str.ToLower();
        }


    }


}
