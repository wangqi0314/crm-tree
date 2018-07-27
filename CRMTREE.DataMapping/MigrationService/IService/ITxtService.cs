using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigrationService.IService
{
    public interface ITxtService
    {
        DataTable importToDB(string filepath);
        DataTable importTxtIntoDt(string filePath);
        DataTable importCsvIntoDt(string folderPath);
        List<string> getPathList(string folderPath);
        List<string> getPathList(string folderPath, string specialGroupName);
        bool scanFolderUpload(string folderPath);
        bool scanFolderUpload(string folderPath, out string filename, string errorFolderPath, string successFolderPath);
        bool dataMappingExtend();
        bool dataMappingExtend(string filePath);
        string inportDataTableIntoDB(string filePath);
    }
}
