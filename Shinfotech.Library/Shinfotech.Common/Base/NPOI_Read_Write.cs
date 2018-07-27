using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShInfoTech.Common
{
    public class NPOI_Read_Write
    {
        /// <summary>
        /// 读取Excel的某一列数据，拼接为字符串
        /// </summary>
        /// <param name="Path"></param>
        /// <param name="columnIndex"></param>
        /// <returns></returns>
        public string ReadeExcelFile(string Path, int columnIndex)
        {
            IWorkbook _workBook = ReadExcelFile(Path);
            if (_workBook == null)
            {
                return null;
            }
            int _columnCount = GetExcelColumnConut(_workBook);
            if (_columnCount < columnIndex)
            {
                return null;
            }
            int _rowCount = GetExcelRowCount(_workBook);
            if (_rowCount < 0)
            {
                return null;
            }
            ISheet _Sheet = _workBook.GetSheetAt(0);
            if (_Sheet == null)
            {
                return null;
            }
            string _VINs = string.Empty;
            for (int i = 0; i < _rowCount; i++)
            {
                _VINs += _Sheet.GetRow(i).GetCell(columnIndex-1).ToString() + ",";
            }
            return _VINs.Substring(0, _VINs.Length - 1);
        }
        /// <summary>
        /// 将数据写入Workbook
        /// </summary>
        /// <param name="Path"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public IWorkbook WriteExcelFile(string Path, DataTable data)
        {
            if (data == null || data.Rows.Count <= 0)
            {
                return null;
            }
            IWorkbook _workBook = CreateWorkbook(Path);
            //ICellStyle _cellStyle = _workBook.CreateCellStyle();
            //_cellStyle.FillForegroundColor = HSSFColor.RED.index;
            //_cellStyle.FillBackgroundColor = HSSFColor.RED.index;
            if (_workBook == null)
            {
                return null;
            }
            ISheet _sheet = _workBook.CreateSheet();
            for (int i = 0; i < data.Rows.Count; i++)
            {
                DataRow _dRow = data.Rows[i];
                IRow _row = _sheet.CreateRow(i);
                for (int j = 0; j < _dRow.Table.Columns.Count; j++)
                {
                    _row.CreateCell(j).SetCellValue(_dRow[j].ToString());
                }
            }
            if (File.Exists(Path))
            {
                File.Delete(Path);
            }            
            using (FileStream fs = File.OpenWrite(Path))
            {
                //保存
                _workBook.Write(fs);
            }
            //ICellStyle style8 = _workBook.CreateCellStyle();
            //style8.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.RED.index;
            //style8.FillPattern = HSSFCellStyle.SQUARES;
            //style8.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.RED.index;
            //sheet1.CreateRow(7).CreateCell(0).CellStyle = style8;
            return _workBook;
        }

        #region private
        /// <summary>
        /// 读取一个Excel文件 返回IWorkbook类型
        /// </summary>
        /// <param name="Path"></param>
        /// <returns></returns>
        private IWorkbook ReadExcelFile(string Path)
        {
            FileInfo _fileInfo = new FileInfo(Path);
            if (!_fileInfo.Exists)
            {
                return null;
            }            
            FileStream _fileStream = new FileStream(Path, FileMode.Open, FileAccess.ReadWrite);
            string _extension = _fileInfo.Extension;
            IWorkbook _workBook = null;
            if (_extension == ".xlsx")
            {
                _workBook = new XSSFWorkbook(_fileStream);
            }
            else if (_extension == ".xls")
            {
                _workBook = new HSSFWorkbook(_fileStream);
            }
            return _workBook;
        }
        /// <summary>
        /// 获取Excel的列数
        /// </summary>
        /// <param name="workBook"></param>
        /// <returns></returns>
        private int GetExcelColumnConut(IWorkbook workBook)
        {
            if (workBook == null)
            {
                return 0;
            }
            ISheet _Sheet = workBook.GetSheetAt(0);
            if (_Sheet == null)
            {
                return 0;
            }
            int RowCount = _Sheet.LastRowNum;
            if (RowCount > 0)
            {
                IRow _Row = _Sheet.GetRow(0);
                return _Row.LastCellNum;
            }
            else
            {
                return 0;
            }
        }
        /// <summary>
        /// 获取Excel的行数
        /// </summary>
        /// <param name="workBook"></param>
        /// <returns></returns>
        private int GetExcelRowCount(IWorkbook workBook)
        {
            if (workBook == null)
            {
                return 0;
            }
            ISheet _Sheet = workBook.GetSheetAt(0);
            if (_Sheet == null)
            {
                return 0;
            }
            return _Sheet.LastRowNum;
        }

        /// <summary>
        /// 根据传入的文件名 创建workBook 对象
        /// </summary>
        /// <param name="Path"></param>
        /// <returns></returns>
        private IWorkbook CreateWorkbook(string Path)
        {
            FileInfo _fileInfo = new FileInfo(Path);
            if (!_fileInfo.Exists)
            {
                return null;
            }
            string _extension = _fileInfo.Extension;
            IWorkbook _workBook = null;
            if (_extension == ".xlsx")
            {
                _workBook = new XSSFWorkbook();
                return _workBook;
            }
            else if (_extension == ".xls")
            {
                _workBook = new HSSFWorkbook();
                return _workBook;
            }
            return _workBook;
        }
        #endregion
    }
}
