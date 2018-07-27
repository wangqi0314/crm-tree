using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.IO;

namespace Shinfotech.Tools
{
    /**/
    /// <summary>
    /// FileControl 的摘要说明
    /// </summary>
    public class FileControl
    {
        public FileControl()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        /**/
        /// <summary> 
        /// 判断是否存在指定文件,如果存在则不添加
        /// </summary> 
        /// <param name="strFolderPathName">要创建的文件路径</param> 
        public bool CreateFolder(string strFolderPathName)
        {
            if (0<strFolderPathName.Trim().Length)
            {
                try
                {
                    string strCreatePath = System.Web.HttpContext.Current.Server.MapPath("/" + strFolderPathName).ToString();
                    if (!Directory.Exists(strCreatePath))
                    {
                        Directory.CreateDirectory(strCreatePath);
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch
                {
                    //throw;
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        /**/
        /// <summary> 
        /// 删除一个文件夹下面的字文件夹和文件 
        /// </summary> 
        /// <param name="FolderPathName"></param> 
        public void DeleteChildFolder(string FolderPathName)
        {
            if (FolderPathName.Trim().Length > 0)
            {
                try
                {
                    string CreatePath = System.Web.HttpContext.Current.Server.MapPath(FolderPathName).ToString();
                    if (Directory.Exists(CreatePath))
                    {
                        Directory.Delete(CreatePath, true);
                    }
                }
                catch
                {
                    throw;
                }
            }
        }

        /**/
        /// <summary> 
        /// 删除一个文件 
        /// </summary> 
        /// <param name="FilePathName"></param> 
        public Boolean DeleteFile(string FilePathName)
        {
            try
            {
                FileInfo DeleFile = new FileInfo(System.Web.HttpContext.Current.Server.MapPath(FilePathName).ToString());
                DeleFile.Delete();
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 建立一个文件
        /// </summary>
        /// <param name="FilePathName"></param>
        public void CreateFile(string FilePathName)
        {
            try
            {
                //创建文件夹 
                string[] strPath = FilePathName.Split('/');
                CreateFolder(FilePathName.Replace("/" + strPath[strPath.Length - 1].ToString(), "")); //创建文件夹 
                FileInfo CreateFile = new FileInfo(System.Web.HttpContext.Current.Server.MapPath(FilePathName).ToString());         //创建文件 
                if (!CreateFile.Exists)
                {
                    FileStream FS = CreateFile.Create();
                    FS.Close();
                }
            }
            catch
            {
            }
        }
        /**/
        /// <summary> 
        /// 删除整个文件夹及其字文件夹和文件 
        /// </summary> 
        /// <param name="FolderPathName"></param> 
        public void DeleParentFolder(string FolderPathName)
        {
            try
            {
                DirectoryInfo DelFolder = new DirectoryInfo(System.Web.HttpContext.Current.Server.MapPath(FolderPathName).ToString());
                if (DelFolder.Exists)
                {
                    DelFolder.Delete();
                }
            }
            catch
            {
            }
        }
        /**/
        /// <summary> 
        /// 在文件里追加内容 
        /// </summary> 
        /// <param name="FilePathName"></param> 
        public void ReWriteReadinnerText(string FilePathName, string WriteWord)
        {
            try
            {
                //建立文件夹和文件 
                //CreateFolder(FilePathName); 
                CreateFile(FilePathName);
                //得到原来文件的内容 
                FileStream FileRead = new FileStream(System.Web.HttpContext.Current.Server.MapPath(FilePathName).ToString(), FileMode.Open, FileAccess.ReadWrite);
                StreamReader FileReadWord = new StreamReader(FileRead, System.Text.Encoding.Default);
                string OldString = FileReadWord.ReadToEnd().ToString();
                OldString = OldString + WriteWord;
                //把新的内容重新写入 
                StreamWriter FileWrite = new StreamWriter(FileRead, System.Text.Encoding.Default);
                FileWrite.Write(WriteWord);
                //关闭 
                FileWrite.Close();
                FileReadWord.Close();
                FileRead.Close();
            }
            catch
            {
                //    throw; 
            }
        }

        /**/
        /// <summary> 
        /// 读取文件里内容 
        /// </summary> 
        /// <param name="FilePathName"></param> 
        public string ReaderFileData(string FilePathName)
        {
            try
            {

                FileStream FileRead = new FileStream(System.Web.HttpContext.Current.Server.MapPath(FilePathName).ToString(), FileMode.Open, FileAccess.Read);
                StreamReader FileReadWord = new StreamReader(FileRead, System.Text.Encoding.Default);
                string TxtString = FileReadWord.ReadToEnd().ToString();
                //关闭 
                FileReadWord.Close();
                FileRead.Close();
                return TxtString;
            }
            catch
            {
                throw;
            }
        }
        /**/
        /// <summary> 
        /// 读取文件夹的文件 
        /// </summary> 
        /// <param name="FilePathName"></param> 
        /// <returns></returns> 
        public DirectoryInfo checkValidSessionPath(string FilePathName)
        {
            try
            {
                DirectoryInfo MainDir = new DirectoryInfo(System.Web.HttpContext.Current.Server.MapPath(FilePathName));
                return MainDir;

            }
            catch
            {
                throw;
            }
        }


        /**/
        /// <summary> 
        /// 在文件里追加内容 
        /// </summary> 
        /// <param name="FilePathName"></param> 
        public bool Bool_WriteReadinnerText(string FilePathName, string WriteWord)
        {
            try
            {
                //建立文件夹和文件 
                //CreateFolder(FilePathName); 
                CreateFile(FilePathName);
                //得到原来文件的内容 
                FileStream FileRead = new FileStream(System.Web.HttpContext.Current.Server.MapPath(FilePathName).ToString(), FileMode.Open, FileAccess.ReadWrite);
                StreamReader FileReadWord = new StreamReader(FileRead, System.Text.Encoding.Default);
                string OldString = FileReadWord.ReadToEnd().ToString();
                OldString = OldString + WriteWord;
                //把新的内容重新写入 
                StreamWriter FileWrite = new StreamWriter(FileRead, System.Text.Encoding.Default);
                FileWrite.Write(WriteWord);
                //关闭 
                FileWrite.Close();
                FileReadWord.Close();
                FileRead.Close();
                return true;
            }
            catch
            {
                //    throw; 
                return false;
            }
        }



        /**/
        /// <summary> 
        /// 判断是否存在指定文件 
        /// </summary> 
        /// <param name="strFolderPathName">要创建的文件路径</param> 
        public bool ExistsFile(string strFolderPathName)
        {
            if (0 < strFolderPathName.Trim().Length)
            {
                try
                {
                    string strCreatePath = System.Web.HttpContext.Current.Server.MapPath("/" + strFolderPathName).ToString();
                    if (File.Exists(strCreatePath))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch
                {
                    //throw;
                    return false;
                }
            }
            else
            {
                return false;
            }
        }


    }
}
