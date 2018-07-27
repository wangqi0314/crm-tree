using CRMTree.BLL;
using CRMTree.BLL.Wechat;
using CRMTree.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ShInfoTech.Common;

public partial class wechat_frmTest : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
       // Response.Write("<script>window.open('/loginS.aspx', 'ShuNovo Login', 'scrollbars=no,resizable=no,status=no,location=no,toolbar=no,menubar=no,width=350,height=255,left = 490,top = 200')</script>");
        //Response.Write("<div id=\"_FG2\"><iframe id=\"TR2_iframe\" class=\"nui-msgbox\" src=\"/loginS.aspx\" style=\"width: 306px; height:234px\" frameborder=\"0\" border=\"0\" scrolling=\"no\"></iframe><div id=\"_df\" class=\"nui-mask\"></div></div>");
        //Response.End();
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        //wechatHandle.ConvertMaterial(3, "images//1.jpg");
        //new BL_Wechat().GETMultimedium_ImageTextID(13135);
        //High_news _new = new High_news()
        //{
        //    thumb_media_id = "IwbOyms_7NVVMwigpwCmgrYY_5Qo-WnnSn8Q_0IRsMVjQz6rgJKK5BYacjmmRyc_",
        //    title = "WangQi_QI",
        //    content = "NIHAo!",
        //};
        //IList<High_news> _Ihigh = new List<High_news>();
        //_Ihigh.Add(_new);
        //string _news = wechatHandle.High_news(_Ihigh);
        //wechatHandle.UploadImageText(_news);
        //customSend _new = new customSend()
        //{
        //    title = "WangQi_QI",
        //    description = "NIHAo!",
        //    url = "http://www.daeku.com",
        //    picurl = "http://www.daeku.com/images/1.jpg"
        //};
        //IList<customSend> _Ihigh = new List<customSend>();
        //_Ihigh.Add(_new);
        //string _news = wechatHandle.custom_News("ogw2MjhK4qPJbDSmtCeXWLezqAOM", _Ihigh);
        //wechatHandle.SendCustom_news(_news);
        //BL_Wechat.SendCustom_News("ogw2MjhK4qPJbDSmtCeXWLezqAOM", 51);
        //string[] _d = { "1","sdfsdfsdfsdf2","3sdfsdfsdfsdfsdf","422","5","6","7","8sdfsdfsdfsdfsdf"};
        //IList<string> d = ReportWechat.GroupData(_d, 3);
        //IList<string> _d =new List<string> { "1", "sdfsdfsdfsdf2", "3sdfsdfsdfsdfsdf", "422", "5", "6", "7", "8sdfsdfsdfsdfsdf" };
        //IList<string> d = ReportWechat.GroupData(_d, 9);
        //string[] _d = { "1", "74202", "3", "422", "5", "6", "7", "8" };
        //int _err = new BL_Wechat().SendWechat_news(10,"p196hpf2g71tprhf27jphqi1ca53.html",_d);
        //string img = "<img name=\"AB\" src=\" dddddd\" /> ;dfddgdfgdfg <img name='ABC' src=' dddddd/df.html' />;gggggggggggggggggggg<img name=\"AB\" src=\" dddddd\" ></img>";
        //ReportWechat.GetImgSrc(img);

        // new CRMTree.DAL.DL_Survey().Save_Survey_data();

        //NPOI_Read_Write _w = new NPOI_Read_Write();
        //_w.ReadeExcelFile("D:\\jobcode_20150624_17_30_49.xls", 1);

    }
    //[ExceptionAttribute]
    protected void btn_GetOAuthUrl_Click(object sender, EventArgs e)
    {
        Response.Write(wechatHandle.CreateOAuthUrl(txt_url.Text));
    }
}
