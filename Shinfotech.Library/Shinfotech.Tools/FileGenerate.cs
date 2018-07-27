using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace Shinfotech.Tools
{
    public static class FileGenerate
    {
        /// <summary>
        /// 生成文件
        /// </summary>
        /// <param name="strSavePath">生成文件的相对路径（包含文件名和后缀）</param>
        /// <param name="strHtmlContent">文件内容</param>
        /// <returns>布尔</returns>
        public static bool GenerateFile(string strSavePath, string strHtmlContent)
        {
            if (0 >= strSavePath.Length || 0 >= strHtmlContent.Length)
            {
                return false;
            }
            else
            {
                try
                {
                    var strPath = HttpContext.Current.Server.MapPath(strSavePath);
                    using (
                        var sw = new StreamWriter(strPath, false, System.Text.Encoding.GetEncoding("utf-8"))
                        )
                    {
                        sw.WriteLine(Regex.Replace(strHtmlContent, @"(?is)(<link\s+[^<>]+>|<script\s+[^<>]+>\s*</script>)\s*(?=(((?!\1).)*\1)+)", ""));
                        sw.Flush();
                        sw.Close();
                    }
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// 读取文件内容
        /// </summary>
        /// <param name="strSavePath">文件相对路径（含文件名和后缀名）</param>
        /// <returns>字符串</returns>
        public static string ReadFileContent(string strSavePath)
        {
            if (0 < strSavePath.Length)
            {
                var strPath = HttpContext.Current.Server.MapPath(strSavePath);
                var htmltext = new StringBuilder();
                using (var sr = new StreamReader(strPath))
                {
                    String line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        htmltext.Append(line);
                    }
                    sr.Close();
                }
                return htmltext.ToString();
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
