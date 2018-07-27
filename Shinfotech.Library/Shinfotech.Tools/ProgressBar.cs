using System;
using System.Collections.Generic;

using System.Text;

namespace Shinfotech.Tools
{
    public class ProgressBar
    {
        /// <summary>
        /// 进度条的初始化
        /// </summary>
        public static void Start()
        {
            Start("正在加载...");
        }
        /// <summary>
        /// 进度条的初始化
        /// </summary>
        /// <param name="msg">最开始显示的信息</param>
        public static void Start(string msg)
        {
            var sbProgressBar = new StringBuilder();           
            sbProgressBar.Append("<html xmlns=\"http://www.w3.org/1999/xhtml\">\r\n<head>\r\n<title></title>\r\n\r\n");
            sbProgressBar.Append("<link href=\"/xrmanage/css/css.css\" rel=\"stylesheet\" type=\"text/css\" />\r\n ");
            sbProgressBar.Append("<style>body {text-align:center;margin-top: 50px;}#ProgressBarSide {height:25px;border:1px #2F2F2F;width:65%;background:#EEFAFF;}</style>\r\n ");
            sbProgressBar.Append("<script language=\"javascript\">\r\n ");
            sbProgressBar.Append("function SetPorgressBar(msg, pos)\r\n ");
            sbProgressBar.Append("{\r\n ");
            sbProgressBar.Append("document.getElementById('ProgressBar').style.width = pos + \"%\";\r\n ");
            sbProgressBar.Append("WriteText('Msg1',msg + \" 已完成\" + pos + \"%\");\r\n ");
            sbProgressBar.Append("}\r\n ");
            sbProgressBar.Append("function SetCompleted(msg)\r\n{\r\nif(msg==\"\")\r\nWriteText(\"Msg1\",\"完成。\");\r\n ");
            sbProgressBar.Append("else\r\nWriteText(\"Msg1\",msg);\r\n}\r\n ");
            sbProgressBar.Append("function WriteText(id, str)\r\n ");
            sbProgressBar.Append("{\r\n ");
            sbProgressBar.Append("var strTag = '<span style=\"font-family:Verdana, Arial, Helvetica;font-size=11.5px;color:#DD5800\">' + str + '</span>';\r\n ");
            sbProgressBar.Append("document.getElementById(id).innerHTML = strTag;\r\n ");
            sbProgressBar.Append("}\r\n ");
            sbProgressBar.Append("</script>\r\n</head>\r\n<body>\r\n ");
            sbProgressBar.Append("<div id=\"Msg1\"><span style=\"font-family:Verdana, Arial, Helvetica;font-size=11.5px;color:#DD5800\">" + msg + "</span></div>\r\n ");
            sbProgressBar.Append("<div id=\"ProgressBarSide\" align=\"left\" style=\"color:Silver;border-width:1px;border-style:Solid;\">\r\n ");
            sbProgressBar.Append("<div id=\"ProgressBar\" style=\"background-color:#008BCE; height:25px; width:0%;color:#fff;\"></div>\r\n ");
            sbProgressBar.Append("</div>\r\n</body>\r\n</html>\r\n ");
            System.Web.HttpContext.Current.Response.Write(sbProgressBar.ToString());
            System.Web.HttpContext.Current.Response.Flush();
        }
        /// <summary>
        /// 滚动进度条
        /// </summary>
        /// <param name="Msg">在进度条上方显示的信息</param>
        /// <param name="Pos">显示进度的百分比数字</param>
        public static void Roll(string Msg, int Pos)
        {
            string jsBlock = "<script language=\"javascript\">SetPorgressBar('" + Msg + "'," + Pos + ");</script> ";
            System.Web.HttpContext.Current.Response.Write(jsBlock);
            System.Web.HttpContext.Current.Response.Flush();
        }
    }
}
