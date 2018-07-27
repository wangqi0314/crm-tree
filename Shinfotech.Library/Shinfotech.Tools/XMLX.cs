using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;
namespace Shinfotech.Tools
{
    public class XMLX : Page
    {
        public XMLX()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        #region 写XML1
        public string CreateXML(string NoAndOff, string PicAndText, string PicPath, string WM_Text, string WM_Location,
            string WM_Transparence, string WM_Angle, string Font, string FontSize, string FontColor, string FontShadow,
            string FontForm, string mkcolor, string ShadowX, string ShadowY, string PlaceX, string PlaceY, string MinWidth,
            string MinHeight, string ZipWidth, string ZipHeight, string MinPath, string SavePath, string Effect,
            string Txtshadowtransparence, string PicVal, string Mosaic, string Granule, string txtmX, string txtmY,
            string txtmWidth, string txtmHeight, string Cut, string txtcX, string txtcY, string txtcWidth, string txtcHeight,
            string Miniature, string Brightness, string Contrast, string RGBValueR, string RGBValueG, string RGBValueB,
            string imgXY, string freely, string imgdel, string txtshowoff)
        {
            //建一个新的空的XML文档
            XmlTextWriter objXml = new XmlTextWriter(Server.MapPath("/discount/xml/WM_Xml.xml"), null);
            //格式化输出XML文档
            objXml.Formatting = Formatting.Indented;
            objXml.Indentation = 4;
            //写入XML文档标记
            objXml.WriteStartDocument();
            //写入XML文档注释
            objXml.WriteComment("Created XML" + Context.Timestamp);
            //写入根元素
            objXml.WriteStartElement("WKConfig");
            //写入元素
            objXml.WriteStartElement("Config");
            //写入属性
            objXml.WriteAttributeString("Technology", "ASP.NET 2.0");
            //写入属性值
            objXml.WriteAttributeString("Author", "张春根");
            //写入子元素及文本值
            objXml.WriteElementString("Title", "水印参数配置");
            //写入子元素及文本值
            objXml.WriteElementString("Version", "ImageWaterMark V2.5.3");
            //写入元素
            objXml.WriteStartElement("Parameter");
            //写入子元素及文本值
            objXml.WriteElementString("NoAndOff", NoAndOff);
            objXml.WriteElementString("PicAndText", PicAndText);
            objXml.WriteElementString("PicPath", PicPath);
            objXml.WriteElementString("WM_Text", WM_Text);
            objXml.WriteElementString("WM_Location", WM_Location);
            objXml.WriteElementString("WM_Transparence", WM_Transparence);
            objXml.WriteElementString("WM_Angle", WM_Angle);
            objXml.WriteElementString("Font", Font);
            objXml.WriteElementString("FontSize", FontSize);
            objXml.WriteElementString("FontColor", FontColor);
            objXml.WriteElementString("FontShadow", FontShadow);
            objXml.WriteElementString("FontForm", FontForm);
            objXml.WriteElementString("Grounding", mkcolor);
            objXml.WriteElementString("ShadowX", ShadowX);
            objXml.WriteElementString("ShadowY", ShadowY);
            objXml.WriteElementString("PlaceX", PlaceX);
            objXml.WriteElementString("PlaceY", PlaceY);
            objXml.WriteElementString("MinWidth", MinWidth);
            objXml.WriteElementString("MinHeight", MinHeight);
            objXml.WriteElementString("ZipWidth", ZipWidth);
            objXml.WriteElementString("ZipHeight", ZipHeight);
            objXml.WriteElementString("MinPath", MinPath);
            objXml.WriteElementString("SavePath", SavePath);
            objXml.WriteElementString("Effect", Effect);
            objXml.WriteElementString("Txtshadowtransparence", Txtshadowtransparence);
            objXml.WriteElementString("PicVal", PicVal);
            objXml.WriteElementString("Mosaic", Mosaic);
            objXml.WriteElementString("Granule", Granule);
            objXml.WriteElementString("txtmX", txtmX);
            objXml.WriteElementString("txtmY", txtmY);
            objXml.WriteElementString("txtmWidth", txtmWidth);
            objXml.WriteElementString("txtmHeight", txtmHeight);
            objXml.WriteElementString("Cut", Cut);
            objXml.WriteElementString("txtcX", txtcX);
            objXml.WriteElementString("txtcY", txtcY);
            objXml.WriteElementString("txtcWidth", txtcWidth);
            objXml.WriteElementString("txtcHeight", txtcHeight);
            objXml.WriteElementString("Miniature", Miniature);
            objXml.WriteElementString("Brightness", Brightness);
            objXml.WriteElementString("Contrast", Contrast);
            objXml.WriteElementString("RGBValueR", RGBValueR);
            objXml.WriteElementString("RGBValueG", RGBValueG);
            objXml.WriteElementString("RGBValueB", RGBValueB);
            objXml.WriteElementString("imgXY", imgXY);
            objXml.WriteElementString("freely", freely);
            objXml.WriteElementString("imgdel", imgdel);
            objXml.WriteElementString("txtshowoff", txtshowoff);

            //关闭子元素、元素、根元素
            objXml.WriteEndElement();
            objXml.WriteEndElement();
            objXml.WriteEndElement();
            //清除缓存
            objXml.Flush();
            //关闭对象
            objXml.Close();
            return "";
        }
        #endregion
        public string XMLRead(string Value)
        {
            
            XmlDocument xd = new XmlDocument();
            xd.Load(Server.MapPath("/discount/xml/WM_Xml.xml"));

            XmlNodeList xnl = xd.GetElementsByTagName(Value);
            if (xnl.Count == 0)
                return "";
            else
            {
                XmlNode mNode = xnl[0];
                return mNode.InnerText;
            }
        }
        /// <summary>
        /// 绑定系统字体
        /// </summary>
        /// <param name="ddlfont"></param>
        public void getcolor(ref DropDownList ddlfont)
        {
            ddlfont.Items.Clear();
            System.Drawing.Text.InstalledFontCollection font;
            font = new System.Drawing.Text.InstalledFontCollection();
            foreach (System.Drawing.FontFamily family in font.Families)
            {
                ddlfont.Items.Add(family.Name);
            }
        }
        public static string HexEncoding(System.Drawing.Color color)
        {
            string R, G, B;
            string strHexEncoding;

            R = color.R.ToString("X");
            G = color.G.ToString("X");
            B = color.B.ToString("X");

            R = R.Length == 1 ? "0" + R : R;
            G = G.Length == 1 ? "0" + G : G;
            B = B.Length == 1 ? "0" + B : B;

            strHexEncoding = "#" + R + G + B;

            return strHexEncoding;


        }

        public void Messages(string Message, string href)
        {
            this.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script language=\"javascript\">alert('" + Message + "');location.href='" + href + "'</script>");
        }
        public void Alert(string msg)
        {
            this.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script language=\"javascript\">alert('" + msg + "');</script>");
        }
    }
}
