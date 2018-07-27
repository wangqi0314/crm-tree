
using System.Collections;
namespace Shinfotech.Tools
{
    public static class OperatImage
    {
        /// <summary>
        /// 图片类型判断
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static bool IsAllowedExtension(string filePath)
       { 
            bool ret = false;
            System.IO.FileStream fs = new System.IO.FileStream(filePath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            System.IO.BinaryReader r = new System.IO.BinaryReader(fs);
            string fileclass = "";
            byte buffer;
            try
            {
                buffer = r.ReadByte();
                fileclass = buffer.ToString();
                buffer = r.ReadByte();
                fileclass += buffer.ToString();
            }
            catch
            {
                return false;
            }
            r.Close();
            fs.Close();
            /*文件扩展名说明
              *4946/104116 txt
              *7173        gif 
              *255216      jpg
              *13780       png
              *6677        bmp
              *239187      txt,aspx,asp,sql
              *208207      xls.doc.ppt
              *6063        xml
              *6033        htm,html
              *4742        js
              *8075        xlsx,zip,pptx,mmap,zip
              *8297        rar   
              *01          accdb,mdb
              *7790        exe,dll           
              *5666        psd 
              *255254      rdp 
              *10056       bt种子 
              *64101       bat 
              *4059        sgf
              */

            string[] fileType = { "255216", "7173", "6677", "13780" };
            string[] fileExtName = { "jpg", "gif", "bmp", "png" };
            /*
            //纯图片
            String[] fileType = { 
                    "7173",    //gif
                    "255216",  //jpg
                    "13780"    //png
                };
                */
            ret = ((IList)fileType).Contains(fileclass);
            //string fExt = "";
            //for (int i = 0; i < fileType.Length; i++)
            //{
            //    if (fileclass == fileType[i])
            //    {
            //        fExt = fileExtName[i];
            //        ret = true;
            //        break;
            //    }
            //}
           // System.Web.HttpContext.Current.Response.Write(fExt);//可以在这里输出你不知道的文件类型的扩展名
            return ret;
        }
    }
}
