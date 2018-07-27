using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using Microsoft.VisualBasic;
using System.Collections;
using Shinfotech.Tools;
using System.Web.UI.WebControls;
namespace Shinfotech.Tools
{
    public class Utils
    {
        private static Regex RegexBr = new Regex(@"(\r\n)", RegexOptions.IgnoreCase);
        public static Regex RegexFont = new Regex(@"<font color=" + "\".*?\"" + @">([\s\S]+?)</font>", Utils.GetRegexCompiledOptions());

        private static FileVersionInfo AssemblyFileVersion = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location);

        private static string TemplateCookieName = string.Format("dnttemplateid_{0}_{1}_{2}", AssemblyFileVersion.FileMajorPart, AssemblyFileVersion.FileMinorPart, AssemblyFileVersion.FileBuildPart);


        /// <summary>
        /// µÃµ½ÕýÔò±àÒë²ÎÊýÉèÖÃ
        /// </summary>
        /// <returns></returns>
        public static RegexOptions GetRegexCompiledOptions()
        {
#if NET1
            return RegexOptions.Compiled;
#else
            return RegexOptions.None;
#endif
        }
        /// <summary>
        /// Ïòjs cookieÖÐ»ñÈ¡Öµ
        /// </summary>
        /// <param name="strName"></param>
        /// <returns></returns>
        public static void WriteCookieTojs(string strName, string strValue, string domain, int expires)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[strName];
            if (cookie == null)
            {
                cookie = new HttpCookie(strName);
            }
            cookie.Value = strValue;
            cookie.Domain = domain;
            cookie.Expires = DateTime.Now.AddMinutes(expires);
            HttpContext.Current.Response.AppendCookie(cookie);
        }
        /// <summary>
        /// Noes the HTML.
        /// </summary>
        /// <param name="Htmlstring">The htmlstring.</param>
        /// <returns></returns>
        public static string NoHTML(string Htmlstring)
        {
            //É¾³ý½Å±¾  
            Htmlstring = Regex.Replace(Htmlstring, @"<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);
            //É¾³ýHTML  
            Htmlstring = Regex.Replace(Htmlstring, @"<(.[^>]*)>", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"([\r\n])[\s]+", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"-->", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"<!--.*", "", RegexOptions.IgnoreCase);

            Htmlstring = Regex.Replace(Htmlstring, @"&(quot|#34);", "\"", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(amp|#38);", "&", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(lt|#60);", "<", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(gt|#62);", ">", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(nbsp|#160);", "   ", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(iexcl|#161);", "\xa1", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(cent|#162);", "\xa2", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(pound|#163);", "\xa3", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(copy|#169);", "\xa9", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&#(\d+);", "", RegexOptions.IgnoreCase);

            Htmlstring.Replace("<", "");
            Htmlstring.Replace(">", "");
            Htmlstring.Replace("\r\n", "");
            Htmlstring = HttpContext.Current.Server.HtmlEncode(Htmlstring).Trim();

            return Htmlstring;
        }

        /// <summary>
        /// Parses the tags.
        /// </summary>
        /// <param name="HTMLStr">The HTML STR.</param>
        /// <returns></returns>
        public static string ParseTags(string HTMLStr)
        {
            return System.Text.RegularExpressions.Regex.Replace(HTMLStr, "<[^>]*>", "");
        }


        /// <summary>
        /// ·µ»Ø×Ö·û´®ÕæÊµ³¤¶È, 1¸öºº×Ö³¤¶ÈÎª2
        /// </summary>
        /// <returns></returns>
        public static int GetStringLength(string str)
        {
            return Encoding.Default.GetBytes(str).Length;
        }

        public static bool IsCompriseStr(string str, string stringarray, string strsplit)
        {
            if (stringarray == "" || stringarray == null)
            {
                return false;
            }

            str = str.ToLower();
            string[] stringArray = Utils.SplitString(stringarray.ToLower(), strsplit);
            for (int i = 0; i < stringArray.Length; i++)
            {
                //string t1 = str;
                //string t2 = stringArray[i];
                if (str.IndexOf(stringArray[i]) > -1)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// ÅÐ¶ÏÖ¸¶¨×Ö·û´®ÔÚÖ¸¶¨×Ö·û´®Êý×éÖÐµÄÎ»ÖÃ
        /// </summary>
        /// <param name="strSearch">×Ö·û´®</param>
        /// <param name="stringArray">×Ö·û´®Êý×é</param>
        /// <param name="caseInsensetive">ÊÇ·ñ²»Çø·Ö´óÐ¡Ð´, trueÎª²»Çø·Ö, falseÎªÇø·Ö</param>
        /// <returns>×Ö·û´®ÔÚÖ¸¶¨×Ö·û´®Êý×éÖÐµÄÎ»ÖÃ, Èç²»´æÔÚÔò·µ»Ø-1</returns>
        public static int GetInArrayID(string strSearch, string[] stringArray, bool caseInsensetive)
        {
            for (int i = 0; i < stringArray.Length; i++)
            {
                if (caseInsensetive)
                {
                    if (strSearch.ToLower() == stringArray[i].ToLower())
                    {
                        return i;
                    }
                }
                else
                {
                    if (strSearch == stringArray[i])
                    {
                        return i;
                    }
                }

            }
            return -1;
        }


        /// <summary>
        /// ÅÐ¶ÏÖ¸¶¨×Ö·û´®ÔÚÖ¸¶¨×Ö·û´®Êý×éÖÐµÄÎ»ÖÃ
        /// </summary>
        /// <param name="strSearch">×Ö·û´®</param>
        /// <param name="stringArray">×Ö·û´®Êý×é</param>
        /// <returns>×Ö·û´®ÔÚÖ¸¶¨×Ö·û´®Êý×éÖÐµÄÎ»ÖÃ, Èç²»´æÔÚÔò·µ»Ø-1</returns>		
        public static int GetInArrayID(string strSearch, string[] stringArray)
        {
            return GetInArrayID(strSearch, stringArray, true);
        }

        /// <summary>
        /// ÅÐ¶ÏÖ¸¶¨×Ö·û´®ÊÇ·ñÊôÓÚÖ¸¶¨×Ö·û´®Êý×éÖÐµÄÒ»¸öÔªËØ
        /// </summary>
        /// <param name="strSearch">×Ö·û´®</param>
        /// <param name="stringArray">×Ö·û´®Êý×é</param>
        /// <param name="caseInsensetive">ÊÇ·ñ²»Çø·Ö´óÐ¡Ð´, trueÎª²»Çø·Ö, falseÎªÇø·Ö</param>
        /// <returns>ÅÐ¶Ï½á¹û</returns>
        public static bool InArray(string strSearch, string[] stringArray, bool caseInsensetive)
        {
            return GetInArrayID(strSearch, stringArray, caseInsensetive) >= 0;
        }

        /// <summary>
        /// ÅÐ¶ÏÖ¸¶¨×Ö·û´®ÊÇ·ñÊôÓÚÖ¸¶¨×Ö·û´®Êý×éÖÐµÄÒ»¸öÔªËØ
        /// </summary>
        /// <param name="str">×Ö·û´®</param>
        /// <param name="stringarray">×Ö·û´®Êý×é</param>
        /// <returns>ÅÐ¶Ï½á¹û</returns>
        public static bool InArray(string str, string[] stringarray)
        {
            return InArray(str, stringarray, false);
        }

        /// <summary>
        /// ÅÐ¶ÏÖ¸¶¨×Ö·û´®ÊÇ·ñÊôÓÚÖ¸¶¨×Ö·û´®Êý×éÖÐµÄÒ»¸öÔªËØ
        /// </summary>
        /// <param name="str">×Ö·û´®</param>
        /// <param name="stringarray">ÄÚ²¿ÒÔ¶ººÅ·Ö¸îµ¥´ÊµÄ×Ö·û´®</param>
        /// <returns>ÅÐ¶Ï½á¹û</returns>
        public static bool InArray(string str, string stringarray)
        {
            return InArray(str, SplitString(stringarray, ","), false);
        }

        /// <summary>
        /// ÅÐ¶ÏÖ¸¶¨×Ö·û´®ÊÇ·ñÊôÓÚÖ¸¶¨×Ö·û´®Êý×éÖÐµÄÒ»¸öÔªËØ
        /// </summary>
        /// <param name="str">×Ö·û´®</param>
        /// <param name="stringarray">ÄÚ²¿ÒÔ¶ººÅ·Ö¸îµ¥´ÊµÄ×Ö·û´®</param>
        /// <param name="strsplit">·Ö¸î×Ö·û´®</param>
        /// <returns>ÅÐ¶Ï½á¹û</returns>
        public static bool InArray(string str, string stringarray, string strsplit)
        {
            return InArray(str, SplitString(stringarray, strsplit), false);
        }

        /// <summary>
        /// ÅÐ¶ÏÖ¸¶¨×Ö·û´®ÊÇ·ñÊôÓÚÖ¸¶¨×Ö·û´®Êý×éÖÐµÄÒ»¸öÔªËØ
        /// </summary>
        /// <param name="str">×Ö·û´®</param>
        /// <param name="stringarray">ÄÚ²¿ÒÔ¶ººÅ·Ö¸îµ¥´ÊµÄ×Ö·û´®</param>
        /// <param name="strsplit">·Ö¸î×Ö·û´®</param>
        /// <param name="caseInsensetive">ÊÇ·ñ²»Çø·Ö´óÐ¡Ð´, trueÎª²»Çø·Ö, falseÎªÇø·Ö</param>
        /// <returns>ÅÐ¶Ï½á¹û</returns>
        public static bool InArray(string str, string stringarray, string strsplit, bool caseInsensetive)
        {
            return InArray(str, SplitString(stringarray, strsplit), caseInsensetive);
        }


        /// <summary>
        /// É¾³ý×Ö·û´®Î²²¿µÄ»Ø³µ/»»ÐÐ/¿Õ¸ñ
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string RTrim(string str)
        {
            for (int i = str.Length; i >= 0; i--)
            {
                if (str[i].Equals(" ") || str[i].Equals("\r") || str[i].Equals("\n"))
                {
                    str.Remove(i, 1);
                }
            }
            return str;
        }


        /// <summary>
        /// Çå³ý¸ø¶¨×Ö·û´®ÖÐµÄ»Ø³µ¼°»»ÐÐ·û
        /// </summary>
        /// <param name="str">ÒªÇå³ýµÄ×Ö·û´®</param>
        /// <returns>Çå³ýºó·µ»ØµÄ×Ö·û´®</returns>
        public static string ClearBR(string str)
        {
            //Regex r = null;
            Match m = null;

            //r = new Regex(@"(\r\n)",RegexOptions.IgnoreCase);
            for (m = RegexBr.Match(str); m.Success; m = m.NextMatch())
            {
                str = str.Replace(m.Groups[0].ToString(), "");
            }


            return str;
        }

        /// <summary>
        /// ´Ó×Ö·û´®µÄÖ¸¶¨Î»ÖÃ½ØÈ¡Ö¸¶¨³¤¶ÈµÄ×Ó×Ö·û´®
        /// </summary>
        /// <param name="str">Ô­×Ö·û´®</param>
        /// <param name="startIndex">×Ó×Ö·û´®µÄÆðÊ¼Î»ÖÃ</param>
        /// <param name="length">×Ó×Ö·û´®µÄ³¤¶È</param>
        /// <returns>×Ó×Ö·û´®</returns>
        public static string CutString(string str, int startIndex, int length)
        {
            if (startIndex >= 0)
            {
                if (length < 0)
                {
                    length = length * -1;
                    if (startIndex - length < 0)
                    {
                        length = startIndex;
                        startIndex = 0;
                    }
                    else
                    {
                        startIndex = startIndex - length;
                    }
                }


                if (startIndex > str.Length)
                {
                    return "";
                }


            }
            else
            {
                if (length < 0)
                {
                    return "";
                }
                else
                {
                    if (length + startIndex > 0)
                    {
                        length = length + startIndex;
                        startIndex = 0;
                    }
                    else
                    {
                        return "";
                    }
                }
            }

            if (str.Length - startIndex < length)
            {
                length = str.Length - startIndex;
            }

            return str.Substring(startIndex, length);
        }

        /// <summary>
        /// ´Ó×Ö·û´®µÄÖ¸¶¨Î»ÖÃ¿ªÊ¼½ØÈ¡µ½×Ö·û´®½áÎ²µÄÁË·û´®
        /// </summary>
        /// <param name="str">Ô­×Ö·û´®</param>
        /// <param name="startIndex">×Ó×Ö·û´®µÄÆðÊ¼Î»ÖÃ</param>
        /// <returns>×Ó×Ö·û´®</returns>
        public static string CutString(string str, int startIndex)
        {
            return CutString(str, startIndex, str.Length);
        }



        /// <summary>
        /// »ñµÃµ±Ç°¾ø¶ÔÂ·¾¶
        /// </summary>
        /// <param name="strPath">Ö¸¶¨µÄÂ·¾¶</param>
        /// <returns>¾ø¶ÔÂ·¾¶</returns>
        public static string GetMapPath(string strPath)
        {
            if (HttpContext.Current != null)
            {
                return HttpContext.Current.Server.MapPath(strPath);
            }
            else //·Çweb³ÌÐòÒýÓÃ
            {
                return System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, strPath);
            }
        }



        /// <summary>
        /// ·µ»ØÎÄ¼þÊÇ·ñ´æÔÚ
        /// </summary>
        /// <param name="filename">ÎÄ¼þÃû</param>
        /// <returns>ÊÇ·ñ´æÔÚ</returns>
        public static bool FileExists(string filename)
        {
            return System.IO.File.Exists(filename);
        }



        /// <summary>
        /// ÒÔÖ¸¶¨µÄContentTypeÊä³öÖ¸¶¨ÎÄ¼þÎÄ¼þ
        /// </summary>
        /// <param name="filepath">ÎÄ¼þÂ·¾¶</param>
        /// <param name="filename">Êä³öµÄÎÄ¼þÃû</param>
        /// <param name="filetype">½«ÎÄ¼þÊä³öÊ±ÉèÖÃµÄContentType</param>
        public static void ResponseFile(string filepath, string filename, string filetype)
        {
            Stream iStream = null;

            // »º³åÇøÎª10k
            byte[] buffer = new Byte[10000];

            // ÎÄ¼þ³¤¶È
            int length;

            // ÐèÒª¶ÁµÄÊý¾Ý³¤¶È
            long dataToRead;

            try
            {
                // ´ò¿ªÎÄ¼þ
                iStream = new FileStream(filepath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);


                // ÐèÒª¶ÁµÄÊý¾Ý³¤¶È
                dataToRead = iStream.Length;

                HttpContext.Current.Response.ContentType = filetype;
                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + Utils.UrlEncode(filename.Trim()).Replace("+", " "));

                while (dataToRead > 0)
                {
                    // ¼ì²é¿Í»§¶ËÊÇ·ñ»¹´¦ÓÚÁ¬½Ó×´Ì¬
                    if (HttpContext.Current.Response.IsClientConnected)
                    {
                        length = iStream.Read(buffer, 0, 10000);
                        HttpContext.Current.Response.OutputStream.Write(buffer, 0, length);
                        HttpContext.Current.Response.Flush();
                        buffer = new Byte[10000];
                        dataToRead = dataToRead - length;
                    }
                    else
                    {
                        // Èç¹û²»ÔÙÁ¬½ÓÔòÌø³öËÀÑ­»·
                        dataToRead = -1;
                    }
                }
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.Write("Error : " + ex.Message);
            }
            finally
            {
                if (iStream != null)
                {
                    // ¹Ø±ÕÎÄ¼þ
                    iStream.Close();
                }
            }
            HttpContext.Current.Response.End();
        }

        /// <summary>
        /// ÅÐ¶ÏÎÄ¼þÃûÊÇ·ñÎªä¯ÀÀÆ÷¿ÉÒÔÖ±½ÓÏÔÊ¾µÄÍ¼Æ¬ÎÄ¼þÃû
        /// </summary>
        /// <param name="filename">ÎÄ¼þÃû</param>
        /// <returns>ÊÇ·ñ¿ÉÒÔÖ±½ÓÏÔÊ¾</returns>
        public static bool IsImgFilename(string filename)
        {
            filename = filename.Trim();
            if (filename.EndsWith(".") || filename.IndexOf(".") == -1)
            {
                return false;
            }
            string extname = filename.Substring(filename.LastIndexOf(".") + 1).ToLower();
            return (extname == "jpg" || extname == "jpeg" || extname == "png" || extname == "bmp" || extname == "gif");
        }


        /// <summary>
        /// intÐÍ×ª»»ÎªstringÐÍ
        /// </summary>
        /// <returns>×ª»»ºóµÄstringÀàÐÍ½á¹û</returns>
        public static string IntToStr(int intValue)
        {
            //
            return Convert.ToString(intValue);
        }
        /// <summary>
        /// MD5º¯Êý
        /// </summary>
        /// <param name="str">Ô­Ê¼×Ö·û´®</param>
        /// <returns>MD5½á¹û</returns>
        public static string MD5(string str)
        {
            byte[] b = Encoding.Default.GetBytes(str);
            b = new MD5CryptoServiceProvider().ComputeHash(b);
            string ret = "";
            for (int i = 0; i < b.Length; i++)
                ret += b[i].ToString("x").PadLeft(2, '0');
            return ret;
        }

        /// <summary>
        /// SHA256º¯Êý
        /// </summary>
        /// /// <param name="str">Ô­Ê¼×Ö·û´®</param>
        /// <returns>SHA256½á¹û</returns>
        public static string SHA256(string str)
        {
            byte[] SHA256Data = Encoding.UTF8.GetBytes(str);
            SHA256Managed Sha256 = new SHA256Managed();
            byte[] Result = Sha256.ComputeHash(SHA256Data);
            return Convert.ToBase64String(Result);  //·µ»Ø³¤¶ÈÎª44×Ö½ÚµÄ×Ö·û´®
        }

        /// <summary>
        /// ½ØÈ¡Ö¸¶¨³¤¶ÈµÄ×Ö·û
        /// </summary>
        /// <param name="p_SrcString"></param>
        /// <param name="p_StartIndex"></param>
        /// <param name="p_Length"></param>
        /// <returns></returns>
        public static string GetSubString(string p_SrcString, int p_StartIndex, int p_Length)
        {
            p_SrcString = p_SrcString == null ? "" : p_SrcString;
            if (p_SrcString.Length > p_Length)
            {
                return p_SrcString.Substring(p_StartIndex, p_Length);
            }
            else
            {
                return p_SrcString;
            }
        }

        /// <summary>
        /// ×Ö·û´®Èç¹û²Ù¹ýÖ¸¶¨³¤¶ÈÔò½«³¬³öµÄ²¿·ÖÓÃÖ¸¶¨×Ö·û´®´úÌæ
        /// </summary>
        /// <param name="p_SrcString">Òª¼ì²éµÄ×Ö·û´®</param>
        /// <param name="p_Length">Ö¸¶¨³¤¶È</param>
        /// <param name="p_TailString">ÓÃÓÚÌæ»»µÄ×Ö·û´®</param>
        /// <returns>½ØÈ¡ºóµÄ×Ö·û´®</returns>
        public static string GetSubString(string p_SrcString, int p_Length, string p_TailString)
        {
            return GetSubString(p_SrcString, 0, p_Length, p_TailString);
        }


        /// <summary>
        /// È¡Ö¸¶¨³¤¶ÈµÄ×Ö·û´®
        /// </summary>
        /// <param name="p_SrcString">Òª¼ì²éµÄ×Ö·û´®</param>
        /// <param name="p_StartIndex">ÆðÊ¼Î»ÖÃ</param>
        /// <param name="p_Length">Ö¸¶¨³¤¶È</param>
        /// <param name="p_TailString">ÓÃÓÚÌæ»»µÄ×Ö·û´®</param>
        /// <returns>½ØÈ¡ºóµÄ×Ö·û´®</returns>
        public static string GetSubString(string p_SrcString, int p_StartIndex, int p_Length, string p_TailString)
        {
            string myResult = p_SrcString;

            //µ±ÊÇÈÕÎÄ»òº«ÎÄÊ±(×¢:ÖÐÎÄµÄ·¶Î§:\u4e00 - \u9fa5, ÈÕÎÄÔÚ\u0800 - \u4e00, º«ÎÄÎª\xAC00-\xD7A3)
            if (System.Text.RegularExpressions.Regex.IsMatch(p_SrcString, "[\u0800-\u4e00]+") ||
                System.Text.RegularExpressions.Regex.IsMatch(p_SrcString, "[\xAC00-\xD7A3]+"))
            {
                //µ±½ØÈ¡µÄÆðÊ¼Î»ÖÃ³¬³ö×Ö¶Î´®³¤¶ÈÊ±
                if (p_StartIndex >= p_SrcString.Length)
                {
                    return "";
                }
                else
                {
                    return p_SrcString.Substring(p_StartIndex,
                                                   ((p_Length + p_StartIndex) > p_SrcString.Length) ? (p_SrcString.Length - p_StartIndex) : p_Length);
                }
            }


            if (p_Length >= 0)
            {
                byte[] bsSrcString = Encoding.Default.GetBytes(p_SrcString);

                //µ±×Ö·û´®³¤¶È´óÓÚÆðÊ¼Î»ÖÃ
                if (bsSrcString.Length > p_StartIndex)
                {
                    int p_EndIndex = bsSrcString.Length;

                    //µ±Òª½ØÈ¡µÄ³¤¶ÈÔÚ×Ö·û´®µÄÓÐÐ§³¤¶È·¶Î§ÄÚ
                    if (bsSrcString.Length > (p_StartIndex + p_Length))
                    {
                        p_EndIndex = p_Length + p_StartIndex;
                    }
                    else
                    {   //µ±²»ÔÚÓÐÐ§·¶Î§ÄÚÊ±,Ö»È¡µ½×Ö·û´®µÄ½áÎ²

                        p_Length = bsSrcString.Length - p_StartIndex;
                        p_TailString = "";
                    }



                    int nRealLength = p_Length;
                    int[] anResultFlag = new int[p_Length];
                    byte[] bsResult = null;

                    int nFlag = 0;
                    for (int i = p_StartIndex; i < p_EndIndex; i++)
                    {

                        if (bsSrcString[i] > 127)
                        {
                            nFlag++;
                            if (nFlag == 3)
                            {
                                nFlag = 1;
                            }
                        }
                        else
                        {
                            nFlag = 0;
                        }

                        anResultFlag[i] = nFlag;
                    }

                    if ((bsSrcString[p_EndIndex - 1] > 127) && (anResultFlag[p_Length - 1] == 1))
                    {
                        nRealLength = p_Length + 1;
                    }

                    bsResult = new byte[nRealLength];

                    Array.Copy(bsSrcString, p_StartIndex, bsResult, 0, nRealLength);

                    myResult = Encoding.Default.GetString(bsResult);

                    myResult = myResult + p_TailString;
                }
            }

            return myResult;
        }

        /// <summary>
        /// Gets the sub string.
        /// </summary>
        /// <param name="stringToSub">The string to sub.</param>
        /// <param name="length">The length.</param>
        /// <returns></returns>
        public static string GetSubString(string stringToSub, string back, int length)
        {
            Regex regex = new Regex("[\u4e00-\u9fa5]+", RegexOptions.Compiled);
            char[] stringChar = stringToSub.ToCharArray();
            StringBuilder sb = new StringBuilder();
            int nLength = 0;
            for (int i = 0; i < stringChar.Length; i++)
            {
                if (regex.IsMatch((stringChar[i]).ToString()))
                {
                    nLength += 2;
                }
                else
                {
                    nLength = nLength + 1;
                }
                if (nLength <= length)
                {
                    sb.Append(stringChar[i]);
                }
                else
                {
                    break;
                }
            }
            if (sb.ToString() != stringToSub)
            {
                if (!"".Equals(back))
                {
                    sb.Append("...");
                }
            }
            return sb.ToString();
        }
        /// <summary>
        /// ×Ô¶¨ÒåµÄÌæ»»×Ö·û´®º¯Êý
        /// </summary>
        public static string ReplaceString(string SourceString, string SearchString, string ReplaceString, bool IsCaseInsensetive)
        {
            return Regex.Replace(SourceString, Regex.Escape(SearchString), ReplaceString, IsCaseInsensetive ? RegexOptions.IgnoreCase : RegexOptions.None);
        }

        /// <summary>
        /// Éú³ÉÖ¸¶¨ÊýÁ¿µÄhtml¿Õ¸ñ·ûºÅ
        /// </summary>
        public static string Spaces(int nSpaces)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < nSpaces; i++)
            {
                sb.Append(" &nbsp;&nbsp;");
            }
            return sb.ToString();
        }

        /// <summary>
        /// ¼ì²âÊÇ·ñ·ûºÏemail¸ñÊ½
        /// </summary>
        /// <param name="strEmail">ÒªÅÐ¶ÏµÄemail×Ö·û´®</param>
        /// <returns>ÅÐ¶Ï½á¹û</returns>
        public static bool IsValidEmail(string strEmail)
        {
            return Regex.IsMatch(strEmail, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }

        public static bool IsValidDoEmail(string strEmail)
        {
            return Regex.IsMatch(strEmail, @"^@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }
        /// <summary>
        /// ÊÇ·ñÊÇ¶ÌÊ±¼ä12:12:12
        /// </summary>
        /// <param name="strtime"></param>
        /// <returns></returns>
        public static bool Isduanshijian(string strtime)
        {
            return Regex.IsMatch(strtime, @"^(\d{1,2})(:)?(\d{1,2})\2(\d{1,2})$");
        }
        /// <summary>
        /// ³¤Ê±¼ä£¬ÐÎÈç (2008-07-22 13:04:06)
        /// </summary>
        /// <param name="strtime"></param>
        /// <returns></returns>
        public static bool Ischangshijian(string strtime)
        {
            return Regex.IsMatch(strtime, @"^(\d{1,4})(-|\/)(\d{1,2})\2(\d{1,2}) (\d{1,2}):(\d{1,2}):(\d{1,2})$");
        }

        /// <summary>
        /// ¼ì²âÊÇ·ñÊÇÕýÈ·µÄUrl
        /// </summary>
        /// <param name="strUrl">ÒªÑéÖ¤µÄUrl</param>
        /// <returns>ÅÐ¶Ï½á¹û</returns>
        public static bool IsURL(string strUrl)
        {
            return Regex.IsMatch(strUrl, @"^(http|https)\://([a-zA-Z0-9\.\-]+(\:[a-zA-Z0-9\.&%\$\-]+)*@)*((25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9])|localhost|([a-zA-Z0-9\-]+\.)*[a-zA-Z0-9\-]+\.(com|edu|gov|int|mil|net|org|biz|arpa|info|name|pro|aero|coop|museum|[a-zA-Z]{1,10}))(\:[0-9]+)*(/($|[a-zA-Z0-9\.\,\?\'\\\+&%\$#\=~_\-]+))*$");
        }

        public static string GetEmailHostName(string strEmail)
        {
            if (strEmail.IndexOf("@") < 0)
            {
                return "";
            }
            return strEmail.Substring(strEmail.LastIndexOf("@")).ToLower();
        }

        /// <summary>
        /// ÅÐ¶ÏÊÇ·ñÎªbase64×Ö·û´®
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsBase64String(string str)
        {
            //A-Z, a-z, 0-9, +, /, =
            return Regex.IsMatch(str, @"[A-Za-z0-9\+\/\=]");
        }
        /// <summary>
        /// ¼ì²âÊÇ·ñÓÐSqlÎ£ÏÕ×Ö·û
        /// </summary>
        /// <param name="str">ÒªÅÐ¶Ï×Ö·û´®</param>
        /// <returns>ÅÐ¶Ï½á¹û</returns>
        public static bool IsSafeSqlString(string str)
        {
            return !Regex.IsMatch(str, @"[-|;|,|\/|\(|\)|\[|\]|\}|\{|%|@|\*|!|\']");
        }

        /// <summary>
        /// ¼ì²âÊÇ·ñÓÐÎ£ÏÕµÄ¿ÉÄÜÓÃÓÚÁ´½ÓµÄ×Ö·û´®
        /// </summary>
        /// <param name="str">ÒªÅÐ¶Ï×Ö·û´®</param>
        /// <returns>ÅÐ¶Ï½á¹û</returns>
        public static bool IsSafeUserInfoString(string str)
        {
            return !Regex.IsMatch(str, @"^\s*$|^c:\\con\\con$|[%,\*" + "\"" + @"\s\t\<\>\&]|ÓÎ¿Í|^Guest");
        }

        /// <summary>
        /// ÇåÀí×Ö·û´®
        /// </summary>
        public static string CleanInput(string strIn)
        {
            return Regex.Replace(strIn.Trim(), @"[^\w\.@-]", "");
        }

        /// <summary>
        /// ·µ»ØURLÖÐ½áÎ²µÄÎÄ¼þÃû
        /// </summary>		
        public static string GetFilename(string url)
        {
            if (url == null)
            {
                return "";
            }
            string[] strs1 = url.Split(new char[] { '/' });
            return strs1[strs1.Length - 1].Split(new char[] { '?' })[0];
        }

        /// <summary>
        /// ¸ù¾Ý°¢À­²®Êý×Ö·µ»ØÔÂ·ÝµÄÃû³Æ(¿É¸ü¸ÄÎªÄ³ÖÖÓïÑÔ)
        /// </summary>	
        public static string[] Monthes
        {
            get
            {
                return new string[] { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
            }
        }

        /// <summary>
        /// Ìæ»»»Ø³µ»»ÐÐ·ûÎªhtml»»ÐÐ·û
        /// </summary>
        public static string StrFormat(string str)
        {
            string str2;

            if (str == null)
            {
                str2 = "";
            }
            else
            {
                str = str.Replace(" ", "&nbsp;");
                str = str.Replace("\r\n", "<br />");
                str = str.Replace("\n", "<br>");
                str2 = str;
            }
            return str2;
        }

        /// <summary>
        /// ÄæÏòÌæ»»»Ø³µ»»ÐÐ·ûÎªhtml»»ÐÐ·û
        /// </summary>//bshao 2009-02-20
        public static string StrFormatBack(string str)
        {
            string str2;

            if (str == null)
            {
                str2 = "";
            }
            else
            {
                str = str.Replace("<br>", "\n");
                str = str.Replace("<br />", "\r\n");
                str = str.Replace("&nbsp;", " ");
                str2 = str;
            }
            return str2;
        }

        /// <summary>
        ///  ·µ»Øµ±Ç°±ê×¼ÈÕÆÚ¸ñÊ½£¨ÓÐÐ¡Ê±·ÖÃë£© ÐÎÊ½Èç:2010-12-30 19:55:31
        ///  Modify By ÍõÅô³Ì on 2010-12-30
        /// </summary>
        /// <returns>×Ö·û´®</returns>
        public static string GetDate()
        {
            return DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
        }

        /// <summary>
        /// ·µ»ØÖ¸¶¨ÈÕÆÚ¸ñÊ½
        /// </summary>
        public static string GetDate(string datetimestr, string replacestr)
        {
            if (datetimestr == null)
            {
                return replacestr;
            }

            if (datetimestr.Equals(""))
            {
                return replacestr;
            }

            try
            {
                datetimestr = Convert.ToDateTime(datetimestr).ToString("yyyy-MM-dd").Replace("1900-01-01", replacestr);
            }
            catch
            {
                return replacestr;
            }
            return datetimestr;

        }


        /// <summary>
        /// ·µ»Ø±ê×¼Ê±¼ä¸ñÊ½string
        /// </summary>
        public static string GetTime()
        {
            return DateTime.Now.ToString("HH:mm:ss");
        }

        /// <summary>
        /// ·µ»Ø±ê×¼Ê±¼ä¸ñÊ½string
        /// </summary>
        public static string GetDateTime()
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        /// <summary>
        /// ·µ»ØÏà¶ÔÓÚµ±Ç°Ê±¼äµÄÏà¶ÔÌìÊý
        /// </summary>
        public static string GetDateTime(int relativeday)
        {
            return DateTime.Now.AddDays(relativeday).ToString("yyyy-MM-dd HH:mm:ss");
        }

        /// <summary>
        /// ·µ»Ø±ê×¼Ê±¼ä¸ñÊ½string
        /// </summary>
        public static string GetDateTimeF()
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fffffff");
        }

        /// <summary>
        /// ·µ»Ø±ê×¼Ê±¼ä 
        /// </sumary>
        public static string GetStandardDateTime(string fDateTime, string formatStr)
        {
            if (fDateTime == "0000-0-0 0:00:00")
            {

                return fDateTime;
            }
            DateTime s = Convert.ToDateTime(fDateTime);
            return s.ToString(formatStr);
        }

        /// <summary>
        /// ·µ»Ø±ê×¼Ê±¼ä yyyy-MM-dd HH:mm:ss
        /// </sumary>
        public static string GetStandardDateTime(string fDateTime)
        {
            return GetStandardDateTime(fDateTime, "yyyy-MM-dd HH:mm:ss");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static bool IsTime(string timeval)
        {
            return Regex.IsMatch(timeval, @"^((([0-1]?[0-9])|(2[0-3])):([0-5]?[0-9])(:[0-5]?[0-9])?)$");
        }


        public static string GetRealIP()
        {
            string ip = RequestClass.GetIP();

            return ip;
        }

        /// <summary>
        /// ¸ÄÕýsqlÓï¾äÖÐµÄ×ªÒå×Ö·û
        /// </summary>
        public static string mashSQL(string str)
        {
            string str2;

            if (str == null)
            {
                str2 = "";
            }
            else
            {
                str = str.Replace("\'", "'");
                str2 = str;
            }
            return str2;
        }

        /// <summary>
        /// Ìæ»»sqlÓï¾äÖÐµÄÓÐÎÊÌâ·ûºÅ
        /// </summary>
        public static string ChkSQL(string str)
        {
            string str2;

            if (str == null)
            {
                str2 = "";
            }
            else
            {
                str = str.Replace("'", "''");
                str2 = str;
            }
            return str2;
        }


        /// <summary>
        /// ×ª»»Îª¾²Ì¬html
        /// </summary>
        public void transHtml(string path, string outpath)
        {
            Page page = new Page();
            StringWriter writer = new StringWriter();
            page.Server.Execute(path, writer);
            FileStream fs;
            if (File.Exists(page.Server.MapPath("") + "\\" + outpath))
            {
                File.Delete(page.Server.MapPath("") + "\\" + outpath);
                fs = File.Create(page.Server.MapPath("") + "\\" + outpath);
            }
            else
            {
                fs = File.Create(page.Server.MapPath("") + "\\" + outpath);
            }
            byte[] bt = Encoding.Default.GetBytes(writer.ToString());
            fs.Write(bt, 0, bt.Length);
            fs.Close();
        }


        ///// <summary>
        ///// ×ª»»Îª¼òÌåÖÐÎÄ
        ///// </summary>
        //public static string ToSChinese(string str)
        //{

        //    return Strings.StrConv(str, VbStrConv.SimplifiedChinese, 0);
        //}

        ///// <summary>
        ///// ×ª»»Îª·±ÌåÖÐÎÄ
        ///// </summary>
        //public static string ToTChinese(string str)
        //{
        //    return Strings.StrConv(str, VbStrConv.TraditionalChinese, 0);
        //}

        /// <summary>
        /// ·Ö¸î×Ö·û´®
        /// </summary>
        public static string[] SplitString(string strContent, string strSplit)
        {
            if (strContent.IndexOf(strSplit) < 0)
            {
                string[] tmp = { strContent };
                return tmp;
            }
            return Regex.Split(strContent, Regex.Escape(strSplit), RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// ·Ö¸î×Ö·û´®
        /// </summary>
        /// <returns></returns>
        public static string[] SplitString(string strContent, string strSplit, int p_3)
        {
            string[] result = new string[p_3];

            string[] splited = SplitString(strContent, strSplit);

            for (int i = 0; i < p_3; i++)
            {
                if (i < splited.Length)
                    result[i] = splited[i];
                else
                    result[i] = string.Empty;
            }

            return result;
        }
        /// <summary>
        /// Ìæ»»html×Ö·û
        /// </summary>
        public static string EncodeHtml(string strHtml)
        {
            if (strHtml != "")
            {
                strHtml = strHtml.Replace(",", "&def");
                strHtml = strHtml.Replace("'", "&dot");
                strHtml = strHtml.Replace(";", "&dec");
                return strHtml;
            }
            return "";
        }



        //public static string ClearHtml(string strHtml)
        //{
        //    if (strHtml != "")
        //    {

        //        r = Regex.Replace(@"<\/?[^>]*>",RegexOptions.IgnoreCase);
        //        for (m = r.Match(strHtml); m.Success; m = m.NextMatch()) 
        //        {
        //            strHtml = strHtml.Replace(m.Groups[0].ToString(),"");
        //        }
        //    }
        //    return strHtml;
        //}


        /// <summary>
        /// ½øÐÐÖ¸¶¨µÄÌæ»»(Ôà×Ö¹ýÂË)
        /// </summary>
        public static string StrFilter(string str, string bantext)
        {
            string text1 = "";
            string text2 = "";
            string[] textArray1 = SplitString(bantext, "\r\n");
            for (int num1 = 0; num1 < textArray1.Length; num1++)
            {
                text1 = textArray1[num1].Substring(0, textArray1[num1].IndexOf("="));
                text2 = textArray1[num1].Substring(textArray1[num1].IndexOf("=") + 1);
                str = str.Replace(text1, text2);
            }
            return str;
        }



        /// <summary>
        /// »ñµÃÎ±¾²Ì¬Ò³ÂëÏÔÊ¾Á´½Ó
        /// </summary>
        /// <param name="curPage">µ±Ç°Ò³Êý</param>
        /// <param name="countPage">×ÜÒ³Êý</param>
        /// <param name="url">³¬¼¶Á´½ÓµØÖ·</param>
        /// <param name="extendPage">ÖÜ±ßÒ³ÂëÏÔÊ¾¸öÊýÉÏÏÞ</param>
        /// <returns>Ò³Âëhtml</returns>
        public static string GetStaticPageNumbers(int curPage, int countPage, string url, string expname, int extendPage)
        {
            int startPage = 1;
            int endPage = 1;

            string t1 = "<a href=\"" + url + "-1" + expname + "\">&laquo;</a>";
            string t2 = "<a href=\"" + url + "-" + countPage + expname + "\">&raquo;</a>";

            if (countPage < 1) countPage = 1;
            if (extendPage < 3) extendPage = 2;

            if (countPage > extendPage)
            {
                if (curPage - (extendPage / 2) > 0)
                {
                    if (curPage + (extendPage / 2) < countPage)
                    {
                        startPage = curPage - (extendPage / 2);
                        endPage = startPage + extendPage - 1;
                    }
                    else
                    {
                        endPage = countPage;
                        startPage = endPage - extendPage + 1;
                        t2 = "";
                    }
                }
                else
                {
                    endPage = extendPage;
                    t1 = "";
                }
            }
            else
            {
                startPage = 1;
                endPage = countPage;
                t1 = "";
                t2 = "";
            }

            StringBuilder s = new StringBuilder("");

            s.Append(t1);
            for (int i = startPage; i <= endPage; i++)
            {
                if (i == curPage)
                {
                    s.Append("<span>");
                    s.Append(i);
                    s.Append("</span>");
                }
                else
                {
                    s.Append("<a href=\"");
                    s.Append(url);
                    s.Append("-");
                    s.Append(i);
                    s.Append(expname);
                    s.Append("\">");
                    s.Append(i);
                    s.Append("</a>");
                }
            }
            s.Append(t2);

            return s.ToString();
        }


        /// <summary>
        /// »ñµÃÌû×ÓµÄÎ±¾²Ì¬Ò³ÂëÏÔÊ¾Á´½Ó
        /// </summary>
        /// <param name="expname"></param>
        /// <param name="countPage">×ÜÒ³Êý</param>
        /// <param name="url">³¬¼¶Á´½ÓµØÖ·</param>
        /// <param name="extendPage">ÖÜ±ßÒ³ÂëÏÔÊ¾¸öÊýÉÏÏÞ</param>
        /// <returns>Ò³Âëhtml</returns>
        public static string GetPostPageNumbers(int countPage, string url, string expname, int extendPage)
        {
            int startPage = 1;
            int endPage = 1;
            int curPage = 1;

            string t1 = "<a href=\"" + url + "-1" + expname + "\">&laquo;</a>";
            string t2 = "<a href=\"" + url + "-" + countPage + expname + "\">&raquo;</a>";

            if (countPage < 1) countPage = 1;
            if (extendPage < 3) extendPage = 2;

            if (countPage > extendPage)
            {
                if (curPage - (extendPage / 2) > 0)
                {
                    if (curPage + (extendPage / 2) < countPage)
                    {
                        startPage = curPage - (extendPage / 2);
                        endPage = startPage + extendPage - 1;
                    }
                    else
                    {
                        endPage = countPage;
                        startPage = endPage - extendPage + 1;
                        t2 = "";
                    }
                }
                else
                {
                    endPage = extendPage;
                    t1 = "";
                }
            }
            else
            {
                startPage = 1;
                endPage = countPage;
                t1 = "";
                t2 = "";
            }

            StringBuilder s = new StringBuilder("");

            s.Append(t1);
            for (int i = startPage; i <= endPage; i++)
            {
                s.Append("<a href=\"");
                s.Append(url);
                s.Append("-");
                s.Append(i);
                s.Append(expname);
                s.Append("\">");
                s.Append(i);
                s.Append("</a>");
            }
            s.Append(t2);

            return s.ToString();
        }



        /// <summary>
        /// »ñµÃÒ³ÂëÏÔÊ¾Á´½Ó
        /// </summary>
        /// <param name="curPage">µ±Ç°Ò³Êý</param>
        /// <param name="countPage">×ÜÒ³Êý</param>
        /// <param name="url">³¬¼¶Á´½ÓµØÖ·</param>
        /// <param name="extendPage">ÖÜ±ßÒ³ÂëÏÔÊ¾¸öÊýÉÏÏÞ</param>
        /// <returns>Ò³Âëhtml</returns>
        public static string GetPageNumbers(int curPage, int countPage, string url, int extendPage)
        {
            return GetPageNumbers(curPage, countPage, url, extendPage, "page");
        }

        /// <summary>
        /// »ñµÃÒ³ÂëÏÔÊ¾Á´½Ó
        /// </summary>
        /// <param name="curPage">µ±Ç°Ò³Êý</param>
        /// <param name="countPage">×ÜÒ³Êý</param>
        /// <param name="url">³¬¼¶Á´½ÓµØÖ·</param>
        /// <param name="extendPage">ÖÜ±ßÒ³ÂëÏÔÊ¾¸öÊýÉÏÏÞ</param>
        /// <param name="pagetag">Ò³Âë±ê¼Ç</param>
        /// <returns>Ò³Âëhtml</returns>
        public static string GetPageNumbers(int curPage, int countPage, string url, int extendPage, string pagetag)
        {
            return GetPageNumbers(curPage, countPage, url, extendPage, pagetag, null);
            //if (pagetag == "")
            //    pagetag = "page";
            //int startPage = 1;
            //int endPage = 1;

            //if(url.IndexOf("?") > 0)
            //{
            //    url = url + "&";
            //}
            //else
            //{
            //    url = url + "?";
            //}


            //string t1 = "<a href=\"" + url + "&" + pagetag + "=1" + "\">&laquo;</a>";
            //string t2 = "<a href=\"" + url + "&" + pagetag + "=" + countPage + "\">&raquo;</a>";

            //if (countPage < 1) 
            //    countPage = 1;
            //if (extendPage < 3) 
            //    extendPage = 2;

            //if (countPage > extendPage)
            //{
            //    if (curPage - (extendPage / 2) > 0)
            //    {
            //        if (curPage + (extendPage / 2) < countPage)
            //        {
            //            startPage = curPage - (extendPage / 2);
            //            endPage = startPage + extendPage - 1;
            //        }
            //        else
            //        {
            //            endPage = countPage;
            //            startPage = endPage - extendPage + 1;
            //            t2 = "";
            //        }
            //    }
            //    else
            //    {
            //        endPage = extendPage;
            //        t1 = "";
            //    }
            //}
            //else
            //{
            //    startPage = 1;
            //    endPage = countPage;
            //    t1 = "";
            //    t2 = "";
            //}

            //StringBuilder s = new StringBuilder("");

            //s.Append(t1);
            //for (int i = startPage; i <= endPage; i++)
            //{
            //    if (i == curPage)
            //    {
            //        s.Append("<span>");
            //        s.Append(i);
            //        s.Append("</span>");
            //    }
            //    else
            //    {
            //        s.Append("<a href=\"");
            //        s.Append(url);
            //        s.Append(pagetag);
            //        s.Append("=");
            //        s.Append(i);
            //        s.Append("\">");
            //        s.Append(i);
            //        s.Append("</a>");
            //    }
            //}
            //s.Append(t2);

            //return s.ToString();
        }

        /// <summary>
        /// »ñµÃÒ³ÂëÏÔÊ¾Á´½Ó
        /// </summary>
        /// <param name="curPage">µ±Ç°Ò³Êý</param>
        /// <param name="countPage">×ÜÒ³Êý</param>
        /// <param name="url">³¬¼¶Á´½ÓµØÖ·</param>
        /// <param name="extendPage">ÖÜ±ßÒ³ÂëÏÔÊ¾¸öÊýÉÏÏÞ</param>
        /// <param name="pagetag">Ò³Âë±ê¼Ç</param>
        /// <param name="anchor">Ãªµã</param>
        /// <returns>Ò³Âëhtml</returns>
        public static string GetPageNumbers(int curPage, int countPage, string url, int extendPage, string pagetag, string anchor)
        {
            if (pagetag == "")
                pagetag = "page";
            int startPage = 1;
            int endPage = 1;

            if (url.IndexOf("?") > 0)
            {
                url = url + "&";
            }
            else
            {
                url = url + "?";
            }

            string t1 = "<a href=\"" + url + "&" + pagetag + "=1";
            string t2 = "<a href=\"" + url + "&" + pagetag + "=" + countPage;
            if (anchor != null)
            {
                t1 += anchor;
                t2 += anchor;
            }
            t1 += "\">&laquo;</a>";
            t2 += "\">&raquo;</a>";

            if (countPage < 1)
                countPage = 1;
            if (extendPage < 3)
                extendPage = 2;

            if (countPage > extendPage)
            {
                if (curPage - (extendPage / 2) > 0)
                {
                    if (curPage + (extendPage / 2) < countPage)
                    {
                        startPage = curPage - (extendPage / 2);
                        endPage = startPage + extendPage - 1;
                    }
                    else
                    {
                        endPage = countPage;
                        startPage = endPage - extendPage + 1;
                        t2 = "";
                    }
                }
                else
                {
                    endPage = extendPage;
                    t1 = "";
                }
            }
            else
            {
                startPage = 1;
                endPage = countPage;
                t1 = "";
                t2 = "";
            }

            StringBuilder s = new StringBuilder("");

            s.Append(t1);
            for (int i = startPage; i <= endPage; i++)
            {
                if (i == curPage)
                {
                    s.Append("<span>");
                    s.Append(i);
                    s.Append("</span>");
                }
                else
                {
                    s.Append("<a href=\"");
                    s.Append(url);
                    s.Append(pagetag);
                    s.Append("=");
                    s.Append(i);
                    if (anchor != null)
                    {
                        s.Append(anchor);
                    }
                    s.Append("\">");
                    s.Append(i);
                    s.Append("</a>");
                }
            }
            s.Append(t2);

            return s.ToString();
        }

        /// <summary>
        /// ·µ»Ø HTML ×Ö·û´®µÄ±àÂë½á¹û
        /// </summary>
        /// <param name="str">×Ö·û´®</param>
        /// <returns>±àÂë½á¹û</returns>
        public static string HtmlEncode(string str)
        {
            return HttpUtility.HtmlEncode(str);
        }

        /// <summary>
        /// ·µ»Ø HTML ×Ö·û´®µÄ½âÂë½á¹û
        /// </summary>
        /// <param name="str">×Ö·û´®</param>
        /// <returns>½âÂë½á¹û</returns>
        public static string HtmlDecode(string str)
        {
            return HttpUtility.HtmlDecode(str);
        }

        /// <summary>
        /// ·µ»Ø URL ×Ö·û´®µÄ±àÂë½á¹û
        /// </summary>
        /// <param name="str">×Ö·û´®</param>
        /// <returns>±àÂë½á¹û</returns>
        public static string UrlEncode(string str)
        {
            return HttpUtility.UrlEncode(str);
        }

        /// <summary>
        /// ·µ»Ø URL ×Ö·û´®µÄ±àÂë½á¹û
        /// </summary>
        /// <param name="str">×Ö·û´®</param>
        /// <returns>½âÂë½á¹û</returns>
        public static string UrlDecode(string str)
        {
            return HttpUtility.UrlDecode(str);
        }


        /// <summary>
        /// ·µ»ØÖ¸¶¨Ä¿Â¼ÏÂµÄ·Ç UTF8 ×Ö·û¼¯ÎÄ¼þ
        /// </summary>
        /// <param name="Path">Â·¾¶</param>
        /// <returns>ÎÄ¼þÃûµÄ×Ö·û´®Êý×é</returns>
        public static string[] FindNoUTF8File(string Path)
        {
            //System.IO.StreamReader reader = null;
            StringBuilder filelist = new StringBuilder();
            DirectoryInfo Folder = new DirectoryInfo(Path);
            //System.IO.DirectoryInfo[] subFolders = Folder.GetDirectories(); 
            /*
            for (int i=0;i<subFolders.Length;i++) 
            { 
                FindNoUTF8File(subFolders[i].FullName); 
            }
            */
            FileInfo[] subFiles = Folder.GetFiles();
            for (int j = 0; j < subFiles.Length; j++)
            {
                if (subFiles[j].Extension.ToLower().Equals(".htm"))
                {
                    FileStream fs = new FileStream(subFiles[j].FullName, FileMode.Open, FileAccess.Read);
                    bool bUtf8 = IsUTF8(fs);
                    fs.Close();
                    if (!bUtf8)
                    {
                        filelist.Append(subFiles[j].FullName);
                        filelist.Append("\r\n");
                    }
                }
            }
            return Utils.SplitString(filelist.ToString(), "\r\n");

        }

        //0000 0000-0000 007F - 0xxxxxxx  (ascii converts to 1 octet!)
        //0000 0080-0000 07FF - 110xxxxx 10xxxxxx    ( 2 octet format)
        //0000 0800-0000 FFFF - 1110xxxx 10xxxxxx 10xxxxxx (3 octet format)

        /// <summary>
        /// ÅÐ¶ÏÎÄ¼þÁ÷ÊÇ·ñÎªUTF8×Ö·û¼¯
        /// </summary>
        /// <param name="sbInputStream">ÎÄ¼þÁ÷</param>
        /// <returns>ÅÐ¶Ï½á¹û</returns>
        private static bool IsUTF8(FileStream sbInputStream)
        {
            int i;
            byte cOctets;  // octets to go in this gb2312 encoded character 
            byte chr;
            bool bAllAscii = true;
            long iLen = sbInputStream.Length;

            cOctets = 0;
            for (i = 0; i < iLen; i++)
            {
                chr = (byte)sbInputStream.ReadByte();

                if ((chr & 0x80) != 0) bAllAscii = false;

                if (cOctets == 0)
                {
                    if (chr >= 0x80)
                    {
                        do
                        {
                            chr <<= 1;
                            cOctets++;
                        }
                        while ((chr & 0x80) != 0);

                        cOctets--;
                        if (cOctets == 0) return false;
                    }
                }
                else
                {
                    if ((chr & 0xC0) != 0x80)
                    {
                        return false;
                    }
                    cOctets--;
                }
            }

            if (cOctets > 0)
            {
                return false;
            }

            if (bAllAscii)
            {
                return false;
            }

            return true;

        }

        /// <summary>
        /// ¸ñÊ½»¯×Ö½ÚÊý×Ö·û´®
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string FormatBytesStr(int bytes)
        {
            if (bytes > 1073741824)
            {
                return ((double)(bytes / 1073741824)).ToString("0") + "G";
            }
            if (bytes > 1048576)
            {
                return ((double)(bytes / 1048576)).ToString("0") + "M";
            }
            if (bytes > 1024)
            {
                return ((double)(bytes / 1024)).ToString("0") + "K";
            }
            return bytes.ToString() + "Bytes";
        }

        /// <summary>
        /// ½«longÐÍÊýÖµ×ª»»ÎªInt32ÀàÐÍ
        /// </summary>
        /// <param name="objNum"></param>
        /// <returns></returns>
        public static int SafeInt32(object objNum)
        {
            if (objNum == null)
            {
                return 0;
            }
            string strNum = objNum.ToString();
            if (IsNumeric(strNum))
            {

                if (strNum.ToString().Length > 9)
                {
                    if (strNum.StartsWith("-"))
                    {
                        return int.MinValue;
                    }
                    else
                    {
                        return int.MaxValue;
                    }
                }
                return Int32.Parse(strNum);
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// ·µ»ØÏà²îµÄÃëÊý
        /// </summary>
        /// <param name="Time"></param>
        /// <param name="Sec"></param>
        /// <returns></returns>
        public static int StrDateDiffSeconds(string Time, int Sec)
        {
            TimeSpan ts = DateTime.Now - DateTime.Parse(Time).AddSeconds(Sec);
            if (ts.TotalSeconds > int.MaxValue)
            {
                return int.MaxValue;
            }
            else if (ts.TotalSeconds < int.MinValue)
            {
                return int.MinValue;
            }
            return (int)ts.TotalSeconds;
        }

        /// <summary>
        /// ·µ»ØÏà²îµÄ·ÖÖÓÊý
        /// </summary>
        /// <param name="time"></param>
        /// <param name="minutes"></param>
        /// <returns></returns>
        public static int StrDateDiffMinutes(string time, int minutes)
        {
            if (time == "" || time == null)
                return 1;
            TimeSpan ts = DateTime.Now - DateTime.Parse(time).AddMinutes(minutes);
            if (ts.TotalMinutes > int.MaxValue)
            {
                return int.MaxValue;
            }
            else if (ts.TotalMinutes < int.MinValue)
            {
                return int.MinValue;
            }
            return (int)ts.TotalMinutes;
        }

        /// <summary>
        /// ·µ»ØÏà²îµÄÐ¡Ê±Êý
        /// </summary>
        /// <param name="time"></param>
        /// <param name="hours"></param>
        /// <returns></returns>
        public static int StrDateDiffHours(string time, int hours)
        {
            if (time == "" || time == null)
                return 1;
            TimeSpan ts = DateTime.Now - DateTime.Parse(time).AddHours(hours);
            if (ts.TotalHours > int.MaxValue)
            {
                return int.MaxValue;
            }
            else if (ts.TotalHours < int.MinValue)
            {
                return int.MinValue;
            }
            return (int)ts.TotalHours;
        }

        /// <summary>
        /// ½¨Á¢ÎÄ¼þ¼Ð
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static bool CreateDir(string name)
        {
            return Utils.MakeSureDirectoryPathExists(name);
        }

        /// <summary>
        /// Îª½Å±¾Ìæ»»ÌØÊâ×Ö·û´®
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ReplaceStrToScript(string str)
        {
            str = str.Replace("\\", "\\\\");
            str = str.Replace("'", "\\'");
            str = str.Replace("\"", "\\\"");
            return str;
        }

        /// <summary>
        /// ÊÇ·ñÎªip
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static bool IsIP(string source)
        {
            return Regex.IsMatch(source, @"^(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9])$", RegexOptions.IgnoreCase);
        }



        public static bool IsIPSect(string ip)
        {
            return Regex.IsMatch(ip, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){2}((2[0-4]\d|25[0-5]|[01]?\d\d?|\*)\.)(2[0-4]\d|25[0-5]|[01]?\d\d?|\*)$");

        }



        /// <summary>
        /// ·µ»ØÖ¸¶¨IPÊÇ·ñÔÚÖ¸¶¨µÄIPÊý×éËùÏÞ¶¨µÄ·¶Î§ÄÚ, IPÊý×éÄÚµÄIPµØÖ·¿ÉÒÔÊ¹ÓÃ*±íÊ¾¸ÃIP¶ÎÈÎÒâ, ÀýÈç192.168.1.*
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="iparray"></param>
        /// <returns></returns>
        public static bool InIPArray(string ip, string[] iparray)
        {

            string[] userip = Utils.SplitString(ip, @".");
            for (int ipIndex = 0; ipIndex < iparray.Length; ipIndex++)
            {
                string[] tmpip = Utils.SplitString(iparray[ipIndex], @".");
                int r = 0;
                for (int i = 0; i < tmpip.Length; i++)
                {
                    if (tmpip[i] == "*")
                    {
                        return true;
                    }

                    if (userip.Length > i)
                    {
                        if (tmpip[i] == userip[i])
                        {
                            r++;
                        }
                        else
                        {
                            break;
                        }
                    }
                    else
                    {
                        break;
                    }

                }
                if (r == 4)
                {
                    return true;
                }


            }
            return false;

        }

        /// <summary>
        /// »ñµÃAssembly°æ±¾ºÅ
        /// </summary>
        /// <returns></returns>
        public static string GetAssemblyVersion()
        {
            return string.Format("{0}.{1}.{2}", AssemblyFileVersion.FileMajorPart, AssemblyFileVersion.FileMinorPart, AssemblyFileVersion.FileBuildPart);
        }

        /// <summary>
        /// »ñµÃAssembly²úÆ·Ãû³Æ
        /// </summary>
        /// <returns></returns>
        public static string GetAssemblyProductName()
        {
            return AssemblyFileVersion.ProductName;
        }

        /// <summary>
        /// »ñµÃAssembly²úÆ·°æÈ¨
        /// </summary>
        /// <returns></returns>
        public static string GetAssemblyCopyright()
        {
            return AssemblyFileVersion.LegalCopyright;
        }
        /// <summary>
        /// ´´½¨Ä¿Â¼
        /// </summary>
        /// <param name="name">Ãû³Æ</param>
        /// <returns>´´½¨ÊÇ·ñ³É¹¦</returns>
        [DllImport("dbgHelp", SetLastError = true)]
        private static extern bool MakeSureDirectoryPathExists(string name);


        /// <summary>
        /// Ð´cookieÖµ
        /// </summary>
        /// <param name="strName">Ãû³Æ</param>
        /// <param name="strValue">Öµ</param>
        public static void WriteCookie(string strName, string strValue)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[strName];
            if (cookie == null)
            {
                cookie = new HttpCookie(strName);
            }
            cookie.Value = StringClass.Encrypt(strValue, "20110101");
            HttpContext.Current.Response.AppendCookie(cookie);
        }
        /// <summary>
        /// Ð´cookieÖµ
        /// </summary>
        /// <param name="strName">Ãû³Æ</param>
        /// <param name="strValue">Öµ</param>
        /// <param name="strValue">¹ýÆÚÊ±¼ä(·ÖÖÓ)</param>
        public static void WriteCookie(string strName, string strValue, int expires)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[strName];
            if (cookie == null)
            {
                cookie = new HttpCookie(strName);
            }
            cookie.Value = StringClass.Encrypt(strValue, "20110101");
            cookie.Expires = DateTime.Now.AddDays(expires);
            HttpContext.Current.Response.AppendCookie(cookie);

        }
        /// <summary>
        /// cookie¹ýÆÚÊ±¼äÑÓ³Ù
        /// </summary>
        /// <param name="strName">Ãû³Æ</param>
        /// <param name="expires">¹ýÆÚÊ±¼ä</param>
        public static void CookieExpireAppend(string strName, string strValue, int expires, string domain)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[strName];
            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddMinutes(expires);
                cookie.Value = strValue;
                if (domain.Length == 0)
                {
                    domain = ".56dodo.com";
                }
                cookie.Domain = domain;
                cookie.Expires = DateTime.Now.AddMinutes(expires);
                HttpContext.Current.Response.AppendCookie(cookie);
            }
        }
        /// <summary>
        /// ¿çÓòÃûÐ´cookieÖµ
        /// </summary>
        /// <param name="strName">Ãû³Æ</param>
        /// <param name="domain">ÓòÃû</param>
        /// <param name="strValue">Öµ</param>
        public static void WriteCookie(string strName, string strValue, string strEncode, int expires, string domain)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[strName];
            if (cookie == null)
            {
                cookie = new HttpCookie(strName);
            }
            if (strEncode.Length > 0)
            {
                strValue = StringClass.Encrypt(strValue, strEncode);
            }
            cookie.Value = strValue;
            if (domain.Length == 0)
            {
                domain = ".56dodo.com";
            }
            cookie.Domain = domain;
            cookie.Expires = DateTime.Now.AddMinutes(expires);
            HttpContext.Current.Response.AppendCookie(cookie);

        }

        /// <summary>
        /// ¶ÁcookieÖµ
        /// </summary>
        /// <param name="strName">Ãû³Æ</param>
        /// <returns>cookieÖµ</returns>
        public static string GetCookie(string strName)
        {
            if (HttpContext.Current.Request.Cookies != null && HttpContext.Current.Request.Cookies[strName] != null)
            {
                return HttpContext.Current.Request.Cookies[strName].Value.ToString();
            }
            return "";
        }
        /// <summary>
        /// ´Ójs cookieÖÐ»ñÈ¡Öµ
        /// </summary>
        /// <param name="strName"></param>
        /// <returns></returns>
        public static string GetCookieFromjs(string strName)
        {
            if (HttpContext.Current.Request.Cookies != null && HttpContext.Current.Request.Cookies[strName] != null)
            {
                return HttpContext.Current.Request.Cookies[strName].Value.ToString();
            }
            return "";
        }

        /// <summary>
        /// ¶ÁcookieÖµstrEncode
        /// </summary>
        /// <param name="strName">Ãû³Æ</param>
        /// <returns>cookieÖµ</returns>
        public static string GetCookie(string strName, string strEncode)
        {
            if (HttpContext.Current.Request.Cookies != null && HttpContext.Current.Request.Cookies[strName] != null)
            {//¼ÓÃÜÂð  bshao 20110101
                return StringClass.Decrypt(HttpContext.Current.Request.Cookies[strName].Value.ToString(), strEncode);
            }
            return "";
        }

        /// <summary>
        /// ¶ÁcookieÖµstrEncode
        /// </summary>
        /// <param name="strName">Ãû³Æ</param>
        /// <returns>cookieÖµ</returns>
        public static string GetCookie(string strName, string key, string strEncode)
        {
            if (HttpContext.Current.Request.Cookies != null && HttpContext.Current.Request.Cookies[strName] != null && HttpContext.Current.Request.Cookies[strName][key] != null)
            {
                //¼ÓÃÜÂð  bshao 20110101
                if (strEncode.Length < 1)
                    return HttpContext.Current.Request.Cookies[strName][key].ToString();
                else
                    return StringClass.Decrypt(HttpContext.Current.Request.Cookies[strName][key].ToString(), strEncode);
            }
            return "";
        }
        /// <summary>
        /// ¶ÁcookieÖµ
        /// </summary>
        /// <param name="strName">Ãû³Æ</param>
        /// <returns>cookieÖµ</returns>
        public static string GetCookieDes(string strName, string key)
        {
            if (HttpContext.Current.Request.Cookies != null && HttpContext.Current.Request.Cookies[strName] != null && HttpContext.Current.Request.Cookies[strName][key] != null)
            {
                //¼ÓÃÜÂð  bshao 20110101
                return HttpContext.Current.Request.Cookies[strName][key].ToString();
            }
            return "";
        }


        /// <summary>
        /// µÃµ½ÂÛÌ³µÄÕæÊµÂ·¾¶
        /// </summary>
        /// <returns></returns>
        public static string GetTrueForumPath()
        {
            string forumPath = HttpContext.Current.Request.Path;
            if (forumPath.LastIndexOf("/") != forumPath.IndexOf("/"))
            {
                forumPath = forumPath.Substring(forumPath.IndexOf("/"), forumPath.LastIndexOf("/") + 1);
            }
            else
            {
                forumPath = "/";
            }
            return forumPath;

        }

        /// <summary>
        /// ÅÐ¶Ï×Ö·û´®ÊÇ·ñÊÇyy-mm-dd×Ö·û´®
        /// </summary>
        /// <param name="str">´ýÅÐ¶Ï×Ö·û´®</param>
        /// <returns>ÅÐ¶Ï½á¹û</returns>
        public static bool IsDateString(string str)
        {
            return Regex.IsMatch(str, @"(\d{4})-(\d{1,2})-(\d{1,2})");
        }

        /// <summary>
        /// ÒÆ³ýHtml±ê¼Ç
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string RemoveHtml(string content)
        {
            string regexstr = @"<[^>]*>";
            return Regex.Replace(content, regexstr, string.Empty, RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// ¹ýÂËHTMLÖÐµÄ²»°²È«±êÇ©
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string RemoveUnsafeHtml(string content)
        {
            content = Regex.Replace(content, @"(\<|\s+)o([a-z]+\s?=)", "$1$2", RegexOptions.IgnoreCase);
            content = Regex.Replace(content, @"(script|frame|form|meta|behavior|style)([\s|:|>])+", "$1.$2", RegexOptions.IgnoreCase);
            return content;
        }

        /// <summary>
        /// ½«ÓÃ»§×éTitleÖÐµÄfont±êÇ©È¥µô
        /// </summary>
        /// <param name="title">ÓÃ»§×éTitle</param>
        /// <returns></returns>
        public static string RemoveFontTag(string title)
        {
            Match m = RegexFont.Match(title);
            if (m.Success)
            {
                return m.Groups[1].Value;
            }
            return title;
        }

        /// <summary>
        /// ÅÐ¶Ï¶ÔÏóÊÇ·ñÎªInt32ÀàÐÍµÄÊý×Ö
        /// </summary>
        /// <param name="Expression"></param>
        /// <returns></returns>
        public static bool IsNumeric(object Expression)
        {
            return TypeParse.IsNumeric(Expression);
        }
        /// <summary>
        /// ´ÓHTMLÖÐ»ñÈ¡ÎÄ±¾,±£Áôbr,p,img
        /// </summary>
        /// <param name="HTML"></param>
        /// <returns></returns>
        public static string GetTextFromHTML(string HTML)
        {
            System.Text.RegularExpressions.Regex regEx = new System.Text.RegularExpressions.Regex(@"</?(?!br|/?p|img)[^>]*>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);

            return regEx.Replace(HTML, "");
        }

        public static bool IsDouble(object Expression)
        {
            return TypeParse.IsDouble(Expression);
        }

        /// <summary>
        /// stringÐÍ×ª»»ÎªboolÐÍ
        /// </summary>
        /// <param name="strValue">Òª×ª»»µÄ×Ö·û´®</param>
        /// <param name="defValue">È±Ê¡Öµ</param>
        /// <returns>×ª»»ºóµÄboolÀàÐÍ½á¹û</returns>
        public static bool StrToBool(object Expression, bool defValue)
        {
            return TypeParse.StrToBool(Expression, defValue);
        }

        /// <summary>
        /// ½«¶ÔÏó×ª»»ÎªInt32ÀàÐÍ
        /// </summary>
        /// <param name="strValue">Òª×ª»»µÄ×Ö·û´®</param>
        /// <param name="defValue">È±Ê¡Öµ</param>
        /// <returns>×ª»»ºóµÄintÀàÐÍ½á¹û</returns>
        public static int StrToInt(object Expression, int defValue)
        {
            return TypeParse.StrToInt(Expression, defValue);
        }

        /// <summary>
        /// stringÐÍ×ª»»ÎªfloatÐÍ
        /// </summary>
        /// <param name="strValue">Òª×ª»»µÄ×Ö·û´®</param>
        /// <param name="defValue">È±Ê¡Öµ</param>
        /// <returns>×ª»»ºóµÄintÀàÐÍ½á¹û</returns>
        public static float StrToFloat(object strValue, float defValue)
        {
            return TypeParse.StrToFloat(strValue, defValue);
        }

        /// <summary>
        /// stringÐÍ×ª»»ÎªdecimalÐÍ
        /// </summary>
        /// <param name="strValue">Òª×ª»»µÄ×Ö·û´®</param>
        /// <param name="defValue">È±Ê¡Öµ</param>
        /// <returns>×ª»»ºóµÄintÀàÐÍ½á¹û</returns>
        public static decimal StrToDecimal(object strValue, decimal defValue)
        {
            return TypeParse.StrToDecimal(strValue, defValue);
        }
        /// <summary>
        /// ÅÐ¶Ï¸ø¶¨µÄ×Ö·û´®Êý×é(strNumber)ÖÐµÄÊý¾ÝÊÇ²»ÊÇ¶¼ÎªÊýÖµÐÍ
        /// </summary>
        /// <param name="strNumber">ÒªÈ·ÈÏµÄ×Ö·û´®Êý×é</param>
        /// <returns>ÊÇÔò·µ¼Ótrue ²»ÊÇÔò·µ»Ø false</returns>
        public static bool IsNumericArray(string[] strNumber)
        {
            return TypeParse.IsNumericArray(strNumber);
        }


        public static string AdDeTime(int times)
        {
            string newtime = (DateTime.Now).AddMinutes(times).ToString();
            return newtime;

        }
        /// <summary>
        /// ÑéÖ¤ÊÇ·ñÎªÕýÕûÊý
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsInt(string str)
        {
            return Regex.IsMatch(str, @"^[0-9]*$");
        }

        public static bool IsRuleTip(Hashtable NewHash, string ruletype, out string key)
        {
            key = "";
            foreach (DictionaryEntry str in NewHash)
            {

                try
                {
                    string[] single = SplitString(str.Value.ToString(), "\r\n");

                    foreach (string strs in single)
                    {
                        if (strs != "")


                            switch (ruletype.Trim().ToLower())
                            {
                                case "email":
                                    if (IsValidDoEmail(strs.ToString()) == false)
                                        throw new Exception();
                                    break;

                                case "ip":
                                    if (IsIPSect(strs.ToString()) == false)
                                        throw new Exception();
                                    break;

                                case "timesect":
                                    string[] splitetime = strs.Split('-');
                                    if (Utils.IsTime(splitetime[1].ToString()) == false || Utils.IsTime(splitetime[0].ToString()) == false)
                                        throw new Exception();
                                    break;

                            }

                    }


                }
                catch
                {
                    key = str.Key.ToString();
                    return false;
                }
            }
            return true;

        }

        /// <summary>
        /// É¾³ý×îºóÒ»¸ö×Ö·û
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ClearLastChar(string str)
        {
            if (str == "")
                return "";
            else
                return str.Substring(0, str.Length - 1);
        }

        /// <summary>
        /// ±¸·ÝÎÄ¼þ
        /// </summary>
        /// <param name="sourceFileName">Ô´ÎÄ¼þÃû</param>
        /// <param name="destFileName">Ä¿±êÎÄ¼þÃû</param>
        /// <param name="overwrite">µ±Ä¿±êÎÄ¼þ´æÔÚÊ±ÊÇ·ñ¸²¸Ç</param>
        /// <returns>²Ù×÷ÊÇ·ñ³É¹¦</returns>
        public static bool BackupFile(string sourceFileName, string destFileName, bool overwrite)
        {
            if (!System.IO.File.Exists(sourceFileName))
            {
                throw new FileNotFoundException(sourceFileName + "ÎÄ¼þ²»´æÔÚ£¡");
            }
            if (!overwrite && System.IO.File.Exists(destFileName))
            {
                return false;
            }
            try
            {
                System.IO.File.Copy(sourceFileName, destFileName, true);
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        /// <summary>
        /// ±¸·ÝÎÄ¼þ,µ±Ä¿±êÎÄ¼þ´æÔÚÊ±¸²¸Ç
        /// </summary>
        /// <param name="sourceFileName">Ô´ÎÄ¼þÃû</param>
        /// <param name="destFileName">Ä¿±êÎÄ¼þÃû</param>
        /// <returns>²Ù×÷ÊÇ·ñ³É¹¦</returns>
        public static bool BackupFile(string sourceFileName, string destFileName)
        {
            return BackupFile(sourceFileName, destFileName, true);
        }


        /// <summary>
        /// »Ö¸´ÎÄ¼þ
        /// </summary>
        /// <param name="backupFileName">±¸·ÝÎÄ¼þÃû</param>
        /// <param name="targetFileName">Òª»Ö¸´µÄÎÄ¼þÃû</param>
        /// <param name="backupTargetFileName">Òª»Ö¸´ÎÄ¼þÔÙ´Î±¸·ÝµÄÃû³Æ,Èç¹ûÎªnull,Ôò²»ÔÙ±¸·Ý»Ö¸´ÎÄ¼þ</param>
        /// <returns>²Ù×÷ÊÇ·ñ³É¹¦</returns>
        public static bool RestoreFile(string backupFileName, string targetFileName, string backupTargetFileName)
        {
            try
            {
                if (!System.IO.File.Exists(backupFileName))
                {
                    throw new FileNotFoundException(backupFileName + "ÎÄ¼þ²»´æÔÚ£¡");
                }
                if (backupTargetFileName != null)
                {
                    if (!System.IO.File.Exists(targetFileName))
                    {
                        throw new FileNotFoundException(targetFileName + "ÎÄ¼þ²»´æÔÚ£¡ÎÞ·¨±¸·Ý´ËÎÄ¼þ£¡");
                    }
                    else
                    {
                        System.IO.File.Copy(targetFileName, backupTargetFileName, true);
                    }
                }
                System.IO.File.Delete(targetFileName);
                System.IO.File.Copy(backupFileName, targetFileName);
            }
            catch (Exception e)
            {
                throw e;
            }
            return true;
        }

        public static bool RestoreFile(string backupFileName, string targetFileName)
        {
            return RestoreFile(backupFileName, targetFileName, null);
        }

        /// <summary>
        /// »ñÈ¡¼ÇÂ¼Ä£°åidµÄcookieÃû³Æ
        /// </summary>
        /// <returns></returns>
        public static string GetTemplateCookieName()
        {
            return TemplateCookieName;
        }

        /// <summary>
        /// ½«È«½ÇÊý×Ö×ª»»ÎªÊý×Ö
        /// </summary>
        /// <param name="SBCCase"></param>
        /// <returns></returns>
        public static string SBCCaseToNumberic(string SBCCase)
        {
            char[] c = SBCCase.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                byte[] b = System.Text.Encoding.Unicode.GetBytes(c, i, 1);
                if (b.Length == 2)
                {
                    if (b[1] == 255)
                    {
                        b[0] = (byte)(b[0] + 32);
                        b[1] = 0;
                        c[i] = System.Text.Encoding.Unicode.GetChars(b)[0];
                    }
                }
            }
            return new string(c);
        }

        /// <summary>
        /// ÅÐ¶ÏÓÃ»§ÊÇ·ñµÇÂ¼
        /// </summary>
        /// <param name="strName">Ïî</param>
        /// <returns>Öµ</returns>
        public static bool CheckIsLogin(string strEncode)
        {
            bool flag = true;
            string username = GetCookie("username", strEncode);
            string userid = GetCookie("userid", strEncode);
            string degree = GetCookie("degree", strEncode);
            string group = GetCookie("group", strEncode);
            if (userid == "" || degree == "" || username == "" || group == "")
            {
                flag = false;
            }
            return flag;

        }

        public static bool CheckIsHaveShop(string strEncode)
        {
            bool flag = true;
            string stid = GetCookie("stid", strEncode);
            if (stid == "")
            {
                flag = false;
            }
            return flag;

        }

        /// <summary>
        /// ½«Êý¾Ý±í×ª»»³ÉJSONÀàÐÍ´®
        /// </summary>
        /// <param name="dt">Òª×ª»»µÄÊý¾Ý±í</param>
        /// <returns></returns>
        public static StringBuilder DataTableToJSON(System.Data.DataTable dt)
        {
            return DataTableToJson(dt, true);
        }

        /// <summary>
        /// ½«Êý¾Ý±í×ª»»³ÉJSONÀàÐÍ´®
        /// </summary>
        /// <param name="dt">Òª×ª»»µÄÊý¾Ý±í</param>
        /// <param name="dispose">Êý¾Ý±í×ª»»½áÊøºóÊÇ·ñdisposeµô</param>
        /// <returns></returns>
        public static StringBuilder DataTableToJson(System.Data.DataTable dt, bool dt_dispose)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("[\r\n");

            //Êý¾Ý±í×Ö¶ÎÃûºÍÀàÐÍÊý×é
            string[] dt_field = new string[dt.Columns.Count];
            int i = 0;
            string formatStr = "{{";
            string fieldtype = "";
            foreach (System.Data.DataColumn dc in dt.Columns)
            {
                dt_field[i] = dc.Caption.ToLower().Trim();
                formatStr += "'" + dc.Caption.ToLower().Trim() + "':";
                fieldtype = dc.DataType.ToString().Trim().ToLower();
                if (fieldtype.IndexOf("int") > 0 || fieldtype.IndexOf("deci") > 0 ||
                    fieldtype.IndexOf("floa") > 0 || fieldtype.IndexOf("doub") > 0 ||
                    fieldtype.IndexOf("bool") > 0)
                {
                    formatStr += "{" + i + "}";
                }
                else
                {
                    formatStr += "'{" + i + "}'";
                }
                formatStr += ",";
                i++;
            }

            if (formatStr.EndsWith(","))
            {
                formatStr = formatStr.Substring(0, formatStr.Length - 1);//È¥µôÎ²²¿","ºÅ
            }
            formatStr += "}},";

            i = 0;
            object[] objectArray = new object[dt_field.Length];
            foreach (System.Data.DataRow dr in dt.Rows)
            {

                foreach (string fieldname in dt_field)
                {   //¶Ô \ , ' ·ûºÅ½øÐÐ×ª»» 
                    objectArray[i] = dr[dt_field[i]].ToString().Trim().Replace("\\", "\\\\").Replace("'", "\\'");
                    switch (objectArray[i].ToString())
                    {
                        case "True":
                            {
                                objectArray[i] = "true"; break;
                            }
                        case "False":
                            {
                                objectArray[i] = "false"; break;
                            }
                        default: break;
                    }
                    i++;
                }
                i = 0;
                stringBuilder.Append(string.Format(formatStr, objectArray));
            }
            if (stringBuilder.ToString().EndsWith(","))
            {
                stringBuilder.Remove(stringBuilder.Length - 1, 1);//È¥µôÎ²²¿","ºÅ
            }

            if (dt_dispose)
            {
                dt.Dispose();
            }
            return stringBuilder.Append("\r\n];");
        }
        /// <summary>
        /// È«Ñ¡listbox
        /// </summary>
        /// <param name="ListBox"></param>
        public static void SelectAllListBox(ListBox ListBox)
        {
            for (int i = 0; i < ListBox.Items.Count; i++)
            {

                ListBox.SelectedIndex = i;
            }
        }
        /// <summary>
        /// »ñÈ¡µ±Ç°ÓòÃû
        /// </summary>
        /// <param name="strHtmlPagePath"></param>
        /// <returns></returns>
        public string GetUrlDomainName(string strHtmlPagePath)
        {
            string p = @"http://[^\.]*\.(?<domain>[^/]*)";
            Regex reg = new Regex(p, RegexOptions.IgnoreCase);
            Match m = reg.Match(strHtmlPagePath);
            return m.Groups["domain"].Value;
        }
        public static string escape(string s)
        {
            StringBuilder sb = new StringBuilder();
            byte[] byteArr = System.Text.Encoding.Unicode.GetBytes(s);

            for (int i = 0; i < byteArr.Length; i += 2)
            {
                sb.Append("%u");
                sb.Append(byteArr[i + 1].ToString("X2"));//°Ñ×Ö½Ú×ª»»ÎªÊ®Áù½øÖÆµÄ×Ö·û´®±íÏÖÐÎÊ½

                sb.Append(byteArr[i].ToString("X2"));
            }
            return sb.ToString();

        }
        //°ÑJavaScriptµÄescape()×ª»»¹ýÈ¥µÄ×Ö·û´®½âÊÍ»ØÀ´
        //Ð©·½·¨Ö§³Öºº×Ö
        public static string unescape(string s)
        {

            string str = s.Remove(0, 2);//É¾³ý×îÇ°ÃæÁ½¸ö£¢%u£¢
            string[] strArr = str.Split(new string[] { "%u" }, StringSplitOptions.None);//ÒÔ×Ó×Ö·û´®£¢%u£¢·Ö¸ô
            byte[] byteArr = new byte[strArr.Length * 2];
            for (int i = 0, j = 0; i < strArr.Length; i++, j += 2)
            {
                byteArr[j + 1] = Convert.ToByte(strArr[i].Substring(0, 2), 16); //°ÑÊ®Áù½øÖÆÐÎÊ½µÄ×Ö´®·û´®×ª»»Îª¶þ½øÖÆ×Ö½Ú
                byteArr[j] = Convert.ToByte(strArr[i].Substring(2, 2), 16);
            }
            str = System.Text.Encoding.Unicode.GetString(byteArr);//°Ñ×Ö½Ú×ªÎªunicode±àÂë
            return str;

        }

    }
}
