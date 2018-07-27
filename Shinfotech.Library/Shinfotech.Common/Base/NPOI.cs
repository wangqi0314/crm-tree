using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.HPSF.Extractor;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using NPOI.XSSF.UserModel;

namespace ShInfoTech.Common
{
    /// <summary>
    /// 该类主要用于实现生产Excel 2014/08/28 WangQi
    /// </summary>
    public class NPOI_Common
    {
        /// <summary>
        /// Excel 工作薄实例对象；
        /// </summary>
        private HSSFWorkbook HSSF_WK;
        /// <summary>
        /// 获取或者设置Excel标题
        /// </summary>
        public List<string> Ex_Titel{get;set;}
        /// <summary>
        /// 获取或设置Excel数据的原始字段名
        /// </summary>
        public List<string> Ex_FieldName{get;set;}
        /// <summary>
        /// 获取或设置Excel的数据
        /// </summary>
        public DataTable Ex_Data{get;set;}
        public NPOI_Common()
        {
            HSSF_WK = new HSSFWorkbook();
        }
        /// <summary>
        /// 创建Excel
        /// </summary>
        public void Create_Ex(string sheet = "Derived data")
        {
            ISheet sheet1 = HSSF_WK.CreateSheet(sheet);
            //设置Excel的标题
            sheet1 = set_Ex_Titel(sheet1);
            sheet1 = set_Ex_Data(sheet1);
        }
        public void Seave_Ex(string fileName)
        {
            using (FileStream fs = File.OpenWrite(HttpContext.Current.Request.PhysicalApplicationPath
                + "\\" + WebConfig.GetAppSettingString("DownFile")
                + "\\" + fileName))
            {
                HSSF_WK.Write(fs);//向打开的这个xls文件中写入并保存。  
            }
        }
        /// <summary>
        /// 创建一个Excel行
        /// </summary>
        /// <param name="Sheet">Sheet页</param>
        /// <param name="Index">行的索引</param>
        /// <returns></returns>
        private IRow Create_Ex_Row(ISheet Sheet, int Index)
        {
            if (Sheet == null)
            {
                ISheet sheet1 = HSSF_WK.CreateSheet("Derived data");
                return sheet1.CreateRow(0);
            }
            if (Sheet.IsRowBroken(Index))
            {
                return Sheet.GetRow(Index);
            }
            return Sheet.CreateRow(Index);
        }
        /// <summary>
        /// 创建一个Excel行的单元格集合
        /// </summary>
        /// <param name="row"></param>
        /// <param name="Cell_Count"></param>
        /// <returns></returns>
        private IRow Create_EX_Row_Cells(IRow row, int Cell_Count)
        {
            if (row == null) { return null; }
            for (int i = 0; i < Cell_Count; i++)
            {
                row.CreateCell(i);
            }
            return row;
        }
        /// <summary>
        /// 设置Excel的标题内容，占据首行
        /// </summary>
        /// <param name="Sheet"></param>
        /// <returns></returns>
        private ISheet set_Ex_Titel(ISheet Sheet)
        {
            IRow Row = Create_Ex_Row(Sheet, 0);
            Row = Create_EX_Row_Cells(Row, Ex_Titel.Count);
            if (Ex_Titel.Count <= 0)
            {
                return Sheet;
            }
            for (int i = 0; i < Ex_Titel.Count; i++)
            {
                Row.Cells[i].SetCellValue(Ex_Titel[i]);
            }
            return Sheet;
        }
        /// <summary>
        /// 设置Excel的数据
        /// </summary>
        /// <param name="Sheet"></param>
        /// <returns></returns>
        private ISheet set_Ex_Data(ISheet Sheet)
        {
            if (Ex_Data == null || Ex_Data.Rows.Count <= 0)
            {
                return Sheet;
            }
            for (int i = 0; i < Ex_Data.Rows.Count; i++)
            {
                IRow Row = Create_Ex_Row(Sheet, i + 1);
                Row = Create_EX_Row_Cells(Row, Ex_Titel.Count);
                for (int j = 0; j < Ex_Titel.Count; j++)
                {
                    try
                    {
                        Row.Cells[j].SetCellValue(Ex_Data.Rows[i][Ex_FieldName[j]].ToString());
                    }
                    catch
                    {
                        Row.Cells[j].SetCellValue("");
                    }
                }
            }
            return Sheet;
        }
    }
}