using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.IO;
using System.Web;
using System.Data;

namespace Shinfotech.Tools
{
    public class StringClass
    {
        public StringClass()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        #region 字符串截取函数
        /// <summary>
        /// 字符串截取函数
        /// </summary>
        /// <param name="inputString">要截取的字符串</param>
        /// <param name="len">要截取的长度</param>
        /// <returns>string</returns>
        /// 
        public static string CutString(string inputString, int len)
        {
            if (inputString.Length > len)
            {
                inputString = inputString.Substring(0, len);
            }
            return inputString;
            //ASCIIEncoding ascii = new ASCIIEncoding();
            //int tempLen = 0;
            //string tempString = "";
            //if (inputString.Length > 0)
            //{
            //    byte[] s = ascii.GetBytes(inputString);
            //    for (int i = 0; i < s.Length; i++)
            //    {
            //        if ((int)s[i] == 63)
            //        {
            //            tempLen += 2;
            //        }
            //        else
            //        {
            //            tempLen += 1;
            //        }

            //        try
            //        {
            //            tempString += inputString.Substring(i, 1);
            //        }
            //        catch
            //        {
            //            break;
            //        }

            //        if (tempLen > len)
            //            break;
            //    }
            //}
            //如果截过则加上半个省略号 
            //byte[] mybyte = System.Text.Encoding.Default.GetBytes(inputString);
            //if (mybyte.Length > len)
            //    tempString += "…"; 
            return "";
        }


        /// <summary>
        ///字符串截取函数(不足添加。。。)
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
                if ("".Equals(back))
                {
                    sb.Append("...");
                }
            }
            return sb.ToString();
        }

        #endregion
        public static double ToDouble(object strValue)
        {
            return strValue == null ? 0 : (strValue.ToString() != "" ? Convert.ToDouble(strValue) : 0);
        }

        #region 生成由日期组成的唯一的文件名
        /// <summary>
        /// 生成由日期组成的唯一的文件名
        /// </summary>
        /// <returns>string</returns>
        public static string MakeFileName()
        {
            string dateName = DateTime.Now.ToString("yyyyMMddhhmmss");
            var srd = new Random();
            int srdName = srd.Next(1000);
            return dateName + srdName;
        }

        public static string MakeFileName24()
        {
            return DateTime.Now.Year.ToString() + (DateTime.Now.Month >= 10 ? DateTime.Now.Month.ToString() : "0" + DateTime.Now.Month.ToString()) + (DateTime.Now.Day >= 10 ? DateTime.Now.Day.ToString() : "0" + DateTime.Now.Day.ToString()) + (DateTime.Now.Hour >= 10 ? DateTime.Now.Hour.ToString() : "0" + DateTime.Now.Hour.ToString()) + (DateTime.Now.Minute >= 10 ? DateTime.Now.Minute.ToString() : "0" + DateTime.Now.Minute.ToString()) + (DateTime.Now.Second >= 10 ? DateTime.Now.Second.ToString() : "0" + DateTime.Now.Second.ToString());
        }
        #endregion

        #region 过滤特殊字符
        /// <summary>
        /// 过滤特殊字符
        /// </summary>
        /// <param name="inputStr">字符串</param>
        /// <returns>string</returns>
        public static string CutBadStr(string inputStr)
        {

            //strContent = strContent.Replace("&", "&amp");
            //strContent = strContent.Replace("´", "∵");
            //strContent = strContent.Replace("<", "&lt");
            //strContent = strContent.Replace(">", "&gt");
            //strContent = strContent.Replace("chr(60)", "&lt");
            //strContent = strContent.Replace("chr(37)", "&gt");
            //strContent = strContent.Replace("\"", "&quot"); //不允许使用
            //strContent = strContent.Replace(";", ";");
            //strContent = strContent.Replace("\n", "<br />");
            //strContent = strContent.Replace(" ", "&nbsp");
            //inputStr = inputStr.ToLower().Replace(";", "■");
            //inputStr = inputStr.ToLower().Replace(",", "■");
            inputStr = inputStr.ToLower().Replace("'", "‘");
            inputStr = inputStr.ToLower().Replace("’", "’");
            inputStr = inputStr.ToLower().Replace("<", "[");
            inputStr = inputStr.ToLower().Replace(">", "]");
            inputStr = inputStr.ToLower().Replace("\n", "");
            inputStr = inputStr.ToLower().Replace("%", "");
            //inputStr = inputStr.ToLower().Replace(".", "");
            inputStr = inputStr.ToLower().Replace(":", "");
            inputStr = inputStr.ToLower().Replace("#", "");
            inputStr = inputStr.ToLower().Replace("&", "");
            inputStr = inputStr.ToLower().Replace("$", "");
            inputStr = inputStr.ToLower().Replace("^", "");
            inputStr = inputStr.ToLower().Replace("*", "");
            inputStr = inputStr.ToLower().Replace("`", "");
            inputStr = inputStr.ToLower().Replace(" ", "");
            //inputStr = inputStr.ToLower().Replace("~", "");
            //inputStr = inputStr.ToLower().Replace("or", "");
            //inputStr = inputStr.ToLower().Replace("and", "");
            inputStr = inputStr.ToLower().Replace("+", "");

            return inputStr;
        }
        #endregion

        #region 转换成html
        public static string HtmlStr(string strContent)
        {
            //strContent = strContent.Replace(";", "");
            strContent = strContent.ToLower().Replace("■", ",");
            strContent = strContent.ToLower().Replace("∴", "'");
            //strContent = strContent.ToLower().Replace("∴", "’");
            strContent = strContent.Replace("&lt", "<");
            strContent = strContent.Replace("&gt", ">");
            strContent = strContent.Replace("<;", "<");
            strContent = strContent.Replace(">;", ">");
            //strContent = strContent.Replace("■", ";");
            //strContent = strContent.Replace("&amp", "&");
            //strContent = strContent.Replace("&lt","chr(60)");
            //strContent = strContent.Replace("&gt","chr(37)");
            //strContent = strContent.Replace("&quot","\"");
            //strContent = strContent.Replace("<br />","\n");
            //strContent = strContent.Replace("&nbsp"," ");
            return strContent;
        }
        #endregion


        #region 过滤html标记
        /// <summary>
        /// 过滤html标记
        /// </summary>
        /// <param name="HTMLStr">要过滤的字符串</param>
        /// <returns>string</returns>
        /// 

        public static string CutHtml(string strHtml)
        {
            if (strHtml.Length > 0)
            {
                string[] aryReg ={   
                  @"<script[^>]*?>.*?</script>",
                  @"<(\/\s*)?!?((\w+:)?\w+)(\w+(\s*=?\s*(([""'])(\\[""'tbnr]|[^\7])*?\7|\w+)|.{0})|\s)*?(\/\s*)?>",   
                  @"([\r\n])[\s]+",   
                  @"&(quot|#34);",   
                  @"&(amp|#38);",   
                  @"&(lt|#60);",   
                  @"&(gt|#62);",     
                  @"&(nbsp|#160);",     
                  @"&(iexcl|#161);",   
                  @"&(cent|#162);",   
                  @"&(pound|#163);",   
                  @"&(copy|#169);",   
                  @"&#(\d+);",   
                  @"-->",   
                  @"<!--.*\n" , 
                  @"\|\|" ,
                  @"'" ,
                  
                 };

                string[] aryRep =   {   
                     "",   
                     "",   
                     "",   
                     "\"",   
                     "&",   
                     "<",   
                     ">",   
                     "   ",   
                     "\xa1",//chr(161),   
                     "\xa2",//chr(162),   
                     "\xa3",//chr(163),   
                     "\xa9",//chr(169),   
                     "",   
                     "\r\n",   
                     "",
                     "| |",
                     "" 
                    };

                string newReg = aryReg[0];
                string strOutput = strHtml;
                for (int i = 0; i < aryReg.Length; i++)
                {
                    Regex regex = new Regex(aryReg[i], RegexOptions.IgnoreCase);
                    strOutput = regex.Replace(strOutput, aryRep[i]);
                }
                strOutput.Replace("<", "");
                strOutput.Replace(">", "");
                strOutput.Replace("\r\n", "");
                return strOutput;
            }
            else
            {
                return "";
            }

        }
        #endregion

        #region 标题固定长度


        /// <summary>
        /// 功能描述：填充或截断原始字符串为指定长度 
        /// </summary>
        /// <param name="strOriginal">原始字符串</param>
        /// <param name="maxTrueLength">字符串的字节长度</param>
        /// <param name="chrPad">填充字符</param>
        /// <param name="blnCutTail">字符串的字节长度超过maxTrueLength时截断多余字符</param>
        /// <returns>填充或截断后的字符串</returns>
        static public string PadRightTrueLen(string strOriginal, int maxTrueLength, char chrPad, bool blnCutTail)
        {
            string strNew = strOriginal;

            if (strOriginal == null || maxTrueLength <= 0)
            {
                strNew = "";
                return strNew;
            }

            int trueLen = TrueLength(strOriginal);
            if (trueLen > maxTrueLength)//超过maxTrueLength
            {
                if (blnCutTail)//截断
                {
                    for (int i = strOriginal.Length - 1; i > 0; i--)
                    {
                        strNew = strNew.Substring(0, i);
                        if (TrueLength(strNew) == maxTrueLength)
                            break;
                        else if (TrueLength(strNew) < maxTrueLength)
                        {
                            strNew += chrPad.ToString();
                            break;
                        }
                    }
                }
            }
            else//填充
            {
                for (int i = 0; i < maxTrueLength - trueLen; i++)
                {
                    strNew += chrPad.ToString();
                }
            }

            return strNew;
        }

        //主方法
        public static string CutStringTitle(string inputString, int i)
        {
            return PadRightTrueLen(inputString, i, ' ', true);
        }



        /// <summary>
        /// <table style="font-size:12px">
        /// <tr><td><b>功能描述</b>：字符串的字节长度 </td></tr>
        /// <tr><td><b>创 建 人</b>： </td></tr>
        /// <tr><td><b>创建时间</b>：</td></tr>
        /// </table>
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>字符串的字节长度</returns>
        static public int TrueLength(string str)
        {
            int lenTotal = 0;
            int n = str.Length;
            string strWord = "";
            int asc;
            for (int i = 0; i < n; i++)
            {
                strWord = str.Substring(i, 1);
                asc = Convert.ToChar(strWord);
                if (asc < 0 || asc > 127)
                    lenTotal = lenTotal + 2;
                else
                    lenTotal = lenTotal + 1;
            }

            return lenTotal;
        }
        #endregion

        #region 替换特殊字符
        /// <summary>
        /// 特殊字符串替换
        /// </summary> 
        public static string RepString(string strTemp)
        {
            if (strTemp == null)
                strTemp = "";
            strTemp = strTemp.Replace(" ", "");
            strTemp = strTemp.Replace("*", "");
            strTemp = strTemp.Replace("?", "");
            strTemp = strTemp.Replace("#", "");
            strTemp = strTemp.Replace("@", "");
            strTemp = strTemp.Replace("^", "");
            strTemp = strTemp.Replace("&", "");
            strTemp = strTemp.Replace("+", "");
            strTemp = strTemp.Replace("-", "");
            strTemp = strTemp.Replace("(", "");
            strTemp = strTemp.Replace(")", "");
            strTemp = strTemp.Replace("!", "");
            strTemp = strTemp.Replace("`", "");
            strTemp = strTemp.Replace("~", "");
            strTemp = strTemp.Replace("<", "");
            strTemp = strTemp.Replace(">", "");
            strTemp = strTemp.Replace("'", "");
            strTemp = strTemp.Replace("\"", "");
            strTemp = strTemp.Replace("\\", "");
            strTemp = strTemp.Replace("|", "");
            strTemp = strTemp.Replace("=", "");
            strTemp = strTemp.Replace(",", "");
            return strTemp;
        }
        #endregion


        #region 删除html格式
        /// <summary>
        /// 清除html特殊字符
        /// </summary>
        /// <param name="strContent"></param>
        /// <returns></returns>
        public static string ClearHtml(string strContent)
        {
            strContent = strContent.Replace("&", "");
            strContent = strContent.Replace("´", "");
            strContent = strContent.Replace("<", "");
            strContent = strContent.Replace(">", "");
            strContent = strContent.Replace("chr(60)", "");
            strContent = strContent.Replace("chr(37)", "");
            strContent = strContent.Replace("\"", "");
            strContent = strContent.Replace(";", "");
            strContent = strContent.Replace("\n", "<br/>");
            strContent = strContent.Replace("\\", "");
            return strContent;
        }
        #endregion

        #region md5 加密
        public static string EncryptMd5(string cleanString)
        {
            Byte[] clearBytes = new UnicodeEncoding().GetBytes(cleanString);
            Byte[] hashedBytes = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(clearBytes);
            return BitConverter.ToString(hashedBytes);
        }

        public static string MD5(string str)
        {
            //byte[] b = Encoding.Default.GetBytes(str);
            //b = new MD5CryptoServiceProvider().ComputeHash(b);
            //string ret = "";
            //for(int i = 0; i < b.Length; i++)
            //    ret += b[i].ToString("x").PadLeft(2,'0');
            //return ret;
            string ret = "";
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            ret = BitConverter.ToString(md5.ComputeHash(UTF8Encoding.Default.GetBytes(str)), 4, 8);
            ret = ret.Replace("-", "");
            return ret.ToLower();
        }
        #endregion


        #region DEC 加密过程
        ///
        public static string Encrypt(string pToEncrypt, string sKey)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();　//把字符串放到byte数组中

            byte[] inputByteArray = Encoding.Default.GetBytes(pToEncrypt);
            //byte[]　inputByteArray=Encoding.Unicode.GetBytes(pToEncrypt);

            des.Key = ASCIIEncoding.ASCII.GetBytes(sKey);　//建立加密对象的密钥和偏移量
            des.IV = ASCIIEncoding.ASCII.GetBytes(sKey);　 //原文使用ASCIIEncoding.ASCII方法的GetBytes方法
            MemoryStream ms = new MemoryStream();　　 //使得输入密码必须输入英文文本
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
            if (cs != null)
            {
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
            }
            StringBuilder ret = new StringBuilder();
            foreach (byte b in ms.ToArray())
            {
                ret.AppendFormat("{0:X2}", b);
            }
            ret.ToString();
            return ret.ToString();
        }
        #endregion

        #region  DEC 解密过程

        public static string Decrypt(string pToDecrypt, string sKey)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();

            byte[] inputByteArray = new byte[pToDecrypt.Length / 2];
            for (int x = 0; x < pToDecrypt.Length / 2; x++)
            {
                int i = (Convert.ToInt32(pToDecrypt.Substring(x * 2, 2), 16));
                inputByteArray[x] = (byte)i;
            }

            des.Key = ASCIIEncoding.ASCII.GetBytes(sKey);　//建立加密对象的密钥和偏移量，此值重要，不能修改
            des.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);

            if (cs != null)
            {
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
            }
            StringBuilder ret = new StringBuilder();　//建立StringBuild对象，CreateDecrypt使用的是流对象，必须把解密后的文本变成流对象

            return System.Text.Encoding.Default.GetString(ms.ToArray());
        }
        ///其中的sKey非常重要,定义的时候定义成string然后赋值等于八个字母或数字,注意,必须8个
        ///这个也很实用,譬如你想进入文章页面,传入的参数的aid=10000
        ///这时把10000给加密
        ///然后接受的时候解密.这样能有效的防止sql注入攻击!!!Ò®æÙ
        #endregion

        #region  获得日期（包括毫秒）
        public static string getDatatime()
        {
            string FDate = DateTime.Now.Year.ToString() + "-" + (DateTime.Now.Month >= 10 ? DateTime.Now.Month.ToString() : "0" + DateTime.Now.Month.ToString()) + "-" + (DateTime.Now.Day >= 10 ? DateTime.Now.Day.ToString() : "0" + DateTime.Now.Day.ToString()) + " " + (DateTime.Now.Hour >= 10 ? DateTime.Now.Hour.ToString() : "0" + DateTime.Now.Hour.ToString()) + ":" + (DateTime.Now.Minute >= 10 ? DateTime.Now.Minute.ToString() : "0" + DateTime.Now.Minute.ToString()) + ":" + (DateTime.Now.Second >= 10 ? DateTime.Now.Second.ToString() : "0" + DateTime.Now.Second.ToString());
            //string FDate = DateTime.Now.Year.ToString() + "-" + (DateTime.Now.Month >= 10 ? DateTime.Now.Month.ToString() : "0" + DateTime.Now.Month.ToString()) + "-" + (DateTime.Now.Day >= 10 ? DateTime.Now.Day.ToString() : "0" + DateTime.Now.Day.ToString()) + " " + DateTime.Now.ToLongTimeString();
            return FDate;
        }
        #endregion

        #region  获得日期
        /// <summary>
        /// 得到当前日期，没有小时
        /// </summary>
        /// <returns>string</returns>
        public static string GetData()
        {
            string FDate = DateTime.Now.Year.ToString() + "-" + (DateTime.Now.Month >= 10 ? DateTime.Now.Month.ToString() : "0" + DateTime.Now.Month.ToString()) + "-" + (DateTime.Now.Day >= 10 ? DateTime.Now.Day.ToString() : "0" + DateTime.Now.Day.ToString());
            return FDate;
        }
        /// <summary>
        /// 得到当前时间，没有日期
        /// </summary>
        /// <returns>string</returns>
        public static string GetHourAndMAndSecond()
        {
            return (DateTime.Now.Hour >= 10 ? DateTime.Now.Hour.ToString() : "0" + DateTime.Now.Hour.ToString()) + ":" + (DateTime.Now.Minute >= 10 ? DateTime.Now.Minute.ToString() : "0" + DateTime.Now.Minute.ToString()) + ":" + (DateTime.Now.Second >= 10 ? DateTime.Now.Second.ToString() : "0" + DateTime.Now.Second.ToString());
        }

        /// <summary>
        /// 得到当前时间，没有日期
        /// </summary>
        /// <returns>string</returns>
        public static string GetFullDate()
        {
            return GetData() + " " + GetHourAndMAndSecond();
        }

        #endregion

        #region 过滤重复的字符
        public static string shanchuchongfu(string zifuchuan)
        {
            //原始
            string[] s = zifuchuan.Split(',');
            //保存的string
            string savestring = "";
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] != "")
                {
                    savestring = addstring(s[i], savestring);
                }
            }
            return savestring;

        }

        private static string addstring(string val, string list)
        {
            string[] s = list.Split(',');
            bool flag = true;
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == val)
                {
                    flag = false;
                    break;
                }
            }
            if (flag)
            {
                list += val + ",";
            }
            return list;
        }
        #endregion

        /// <summary>
        /// Removes the HTML.
        /// </summary>
        /// <param name="html">The HTML.</param>
        /// <returns></returns>
        public static string RemoveHtml(string html)
        {
            System.Text.RegularExpressions.Regex regex1 = new System.Text.RegularExpressions.Regex(@"<script[\s\S]+</script *>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex2 = new System.Text.RegularExpressions.Regex(@" href *= *[\s\S]*script *:", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex3 = new System.Text.RegularExpressions.Regex(@" no[\s\S]*=", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex4 = new System.Text.RegularExpressions.Regex(@"<iframe[\s\S]+</iframe *>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex5 = new System.Text.RegularExpressions.Regex(@"<frameset[\s\S]+</frameset *>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex6 = new System.Text.RegularExpressions.Regex(@"\<img[^\>]+\>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex7 = new System.Text.RegularExpressions.Regex(@"</p>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex8 = new System.Text.RegularExpressions.Regex(@"<p>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex9 = new System.Text.RegularExpressions.Regex(@"<[^>]*>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            html = regex1.Replace(html, ""); //过滤<script></script>标记 
            html = regex2.Replace(html, ""); //过滤href=javascript: (<A>) 属性 
            html = regex3.Replace(html, " _disibledevent="); //过滤其它控件的on...事件 
            html = regex4.Replace(html, ""); //过滤iframe 
            html = regex5.Replace(html, ""); //过滤frameset 
            html = regex6.Replace(html, ""); //过滤frameset 
            html = regex7.Replace(html, ""); //过滤frameset 
            html = regex8.Replace(html, ""); //过滤frameset 
            html = regex9.Replace(html, "");
            html = html.Replace(" ", "");
            html = html.Replace("</strong>", "");
            html = html.Replace("<strong>", "");
            return html;
        }
        /// <summary>
        /// Regexdoms the specified URL.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns></returns>
        public static string Regexdom(string url)
        {
            string text = url;
            string pattern = @"(?<=http://)[\w\.]+[^/]";　//C#正则表达式提取匹配URL的模式，
            string s = "";
            MatchCollection mc = Regex.Matches(text, pattern);//满足pattern的匹配集合               
            foreach (Match match in mc)
            {
                s = match.ToString();
            } return s;
        }

        /// <summary>
        /// 格式化.00
        /// </summary>
        /// <param name="Price"></param>
        /// <returns></returns>
        public static string SplitPrice(object Price)
        {
            var strPrice = Price.ToString();
            string strValue = strPrice.Substring(strPrice.IndexOf(".") + 1, 2);
            if (strValue.Equals("00"))
            {
                return strPrice.Substring(0, strPrice.IndexOf("."));
            }
            else
            {
                return strPrice;
            }
        }

        /// <summary>
        /// 格式化 价格
        /// </summary>
        /// <param name="Price"></param>
        /// <returns></returns>
        public static string SplitPrice(object Price, string formmat)
        {
            var result = "";
            var strPrice = Price.ToString();
            string strValue = strPrice.Substring(strPrice.IndexOf(".") + 1, 2);
            if (strValue.Equals("00"))
            {
                result = strPrice.Substring(0, strPrice.IndexOf("."));
            }
            else
            {
                result = strPrice;
            }
            if (result == formmat)
            {
                result = "面议";
            }
            return result;
        }

        #region 地址字符串处理 CREATE Michael Wang
        /// <summary>
        /// 返回格式地址
        /// CreateUser  Michael Wang
        /// CreateDate 2012-03-02
        /// </summary>
        /// <param name="strAddress">地址字符串</param>
        /// <param name="type">type=1:地区的市级项 type=2:地区省-市两项 type=3：地区市-县项 type=4：地区县项</param>
        /// <returns>返回项</returns>
        public static string GetAddress(string strAddress, int type)
        {
            string strValue = String.Empty;
            string[] arrayAddress = strAddress.Split('-');
            int intLength = arrayAddress.Length;
            if (intLength > 0)
            {
                switch (type)
                {
                    case 1:
                        if (intLength == 1)
                        {
                            strValue = strAddress;
                        }
                        else if (intLength == 2)
                        {
                            strValue = arrayAddress[0];
                        }
                        else
                        {
                            strValue = arrayAddress[1];
                        }
                        break;
                    case 2:
                        if (intLength == 1 || intLength == 2)
                        {
                            strValue = strAddress;
                        }
                        else
                        {
                            strValue = arrayAddress[0] + "-" + arrayAddress[1];
                        }
                        break;
                    case 3:
                        if (intLength == 1 || intLength == 2)
                        {
                            strValue = strAddress;
                        }
                        else
                        {
                            strValue = arrayAddress[1] + "-" + arrayAddress[2];
                        }
                        break;
                    case 4:
                        if (intLength == 1)
                        {
                            strValue = strAddress;
                        }
                        else if (intLength == 2)
                        {
                            strValue = arrayAddress[1];
                        }
                        else
                        {
                            strValue = arrayAddress[2];
                        }
                        break;
                    default:
                        strValue = strAddress;
                        break;
                }
            }
            return strValue;
        }

        /// <summary>
        /// 格式化标题
        /// </summary>
        /// <param name="title"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string FormatTitle(string title, int length)
        {
            if (title.Length > length)
            {
                title = title.Substring(0, length - 1) + "..";
            }
            return title;
        }

        /// <summary>
        /// 格式化价格
        /// </summary>
        /// <param name="title"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string FormatPrice(string strPrice)
        {
            string price = "";
            if (0 >= strPrice.Length)
            {
                return "-";
            }
            else
            {
                string[] priceArr = strPrice.Split('/');
                for (int i = 0; i < priceArr.Length; i++)
                {
                    if (priceArr[i] == "0")
                    {
                        priceArr[i] = "-";
                    }
                    price += priceArr[i] + "/";
                }
            }
            return price.TrimEnd('/');
        }

        /// <summary>
        /// 截取字符长度（提交表单）
        /// </summary>
        /// <param name="title"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string FormatForm(string inputString, int len)
        {

            ASCIIEncoding ascii = new ASCIIEncoding();
            int tempLen = 0;
            string tempString = "";
            if (inputString.Length > 0)
            {
                inputString = inputString.Trim();
                byte[] s = ascii.GetBytes(inputString);
                for (int i = 0; i < s.Length; i++)
                {
                    if ((int)s[i] == 63)
                    {
                        tempLen += 2;
                    }
                    else
                    {
                        tempLen += 1;
                    }

                    try
                    {
                        tempString += inputString.Substring(i, 1);
                    }
                    catch
                    {
                        break;
                    }

                    if (tempLen > len)
                        break;
                }
            }
            //如果截过则加上半个省略号 
            byte[] mybyte = System.Text.Encoding.Default.GetBytes(inputString);
            if (mybyte.Length - 2 > len)
                tempString += "…";
            return tempString;
        }

        #endregion

        /// <summary>
        /// 过滤有害标记,create by 王亮 on 2011/4/12
        /// </summary>
        /// <param name="NoHTML">包括HTML，脚本，数据库关键字，特殊字符的源码 </param>
        /// <returns>已经去除标记后的文字</returns>
        public static string NoHtml(string Htmlstring)
        {
            if (Htmlstring == null)
            {
                return "";
            }
            else
            {
                //删除脚本
                Htmlstring = Regex.Replace(Htmlstring, @"<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);
                //删除HTML
                Htmlstring = Regex.Replace(Htmlstring, @"<(.[^>]*)>", "", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, @"([\r\n])[\s]+", "", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, @"-->", "", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, @"<!--.*", "", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, @"&(quot|#34);", "\"", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, @"&(amp|#38);", "&", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, @"&(lt|#60);", "<", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, @"&(gt|#62);", ">", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, @"&(nbsp|#160);", " ", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, @"&(iexcl|#161);", "\xa1", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, @"&(cent|#162);", "\xa2", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, @"&(pound|#163);", "\xa3", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, @"&(copy|#169);", "\xa9", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, @"&#(\d+);", "", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, "xp_cmdshell", "", RegexOptions.IgnoreCase);

                //删除与数据库相关的词
                Htmlstring = Regex.Replace(Htmlstring, "select", "", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, "insert", "", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, "update", "", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, "delete from", "", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, "count''", "", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, "drop table", "", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, "truncate", "", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, "asc", "", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, "mid", "", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, "char", "", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, "xp_cmdshell", "", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, "exec master", "", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, "net localgroup administrators", "", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, "and", "", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, "net user", "", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, "or", "", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, "net", "", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, "delete", "", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, "drop", "", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, "script", "", RegexOptions.IgnoreCase);
                //去除系统存储过程或扩展存储过程关键字
                Htmlstring = Regex.Replace(Htmlstring, "xp_", "", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, "sp_", "", RegexOptions.IgnoreCase);
                //防止16进制注入
                Htmlstring = Regex.Replace(Htmlstring, "0x", "", RegexOptions.IgnoreCase);



                //特殊的字符
                Htmlstring = Htmlstring.Replace("<", "");
                Htmlstring = Htmlstring.Replace(">", "");
                Htmlstring = Htmlstring.Replace("*", "");
                Htmlstring = Htmlstring.Replace("--", "");
                Htmlstring = Htmlstring.Replace("?", "");
                Htmlstring = Htmlstring.Replace("'", "''");
                Htmlstring = Htmlstring.Replace(",", "");
                Htmlstring = Htmlstring.Replace("/", "");
                Htmlstring = Htmlstring.Replace(";", "");
                Htmlstring = Htmlstring.Replace("*/", "");
                Htmlstring = Htmlstring.Replace("\r\n", "");
                Htmlstring = HttpContext.Current.Server.HtmlEncode(Htmlstring).Trim();

                return Htmlstring;
            }
        }

        /// <summary>
        /// 格式国家
        /// </summary>
        /// <param name="strAddress">地址字符串</param>
        /// <param name="type"></param>
        /// <returns>返回项</returns>
        public static string SplitCountry(string strCountry)
        {
            string[] arrayStr = strCountry.Split(' ');
            if (arrayStr.Length > 0)
            {
                return arrayStr[0];
            }
            else
            {
                return strCountry;
            }

        }

        #region 加偏移量
        /// <summary>
        /// 加偏移量
        /// </summary>
        /// <returns></returns>
        public static string GetCode()
        {
            Random rd = new Random();
            string code = rd.Next(10000000, 99999999).ToString();
            return code.ToString();
        }
        #endregion

        #region 生成手机短信验证码 6位

        /// <summary>
        /// 生成手机短信验证码 6位
        /// </summary>
        /// <returns></returns>
        public static string GetSMSCode()
        {
            Random rd = new Random();
            string code = rd.Next(100000, 999999).ToString();
            return code.ToString();
        }
        #endregion


        #region IP隐藏格式转换
        /// <summary>
        /// IP隐藏格式转换
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static string ConvertIP(string ip)
        {
            string[] arrIp = ip.Split('.');
            if (arrIp.Length > 0)
            {

                if (arrIp.Length > 2)
                {
                    return arrIp[0] + "." + arrIp[1] + ".*.*";
                }
                else
                {
                    return ip;
                }
            }
            else
            {
                string[] arrayIp = ip.Split(':');

                if (arrayIp.Length > 2)
                {
                    return arrayIp[0] + "." + arrayIp[1] + ".*.*.*.*.*.*";
                }
                else
                {
                    return ip;
                }
            }
        }
        #endregion
        /// <summary>
        /// 将一个表格的某一列用逗号隔开
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="Query_Field"></param>
        /// <returns></returns>
        public static string ReturnString_Query(DataTable dt, string Query_Field)
        {
            string str_Query = string.Empty;
            if (dt == null || dt.Rows.Count <= 0)
            {
                return str_Query;
            }
            if (dt.Columns[Query_Field].ColumnName != Query_Field)
            {
                return str_Query;
            }
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                str_Query += dt.Rows[i][Query_Field].ToString() + ",";
            }
            if (str_Query.Length > 1)
            {
                str_Query = str_Query.Substring(0, str_Query.Length - 1);
            }
            return str_Query;
        }
    }
}
