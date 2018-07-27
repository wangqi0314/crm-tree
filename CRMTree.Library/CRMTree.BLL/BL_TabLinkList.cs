using CRMTree.DAL;
using CRMTree.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMTree.BLL
{
    public class BL_TabLinkList
    {
        DL_TabLinkList TbLinks = new DL_TabLinkList();
        //top标签
        StringBuilder strNav = null;
        //标签内容
        MD_TabLinks TabLink = new MD_TabLinks();
        public StringBuilder getTabLinkList(long UserCode, bool Interna)
        {
            TabLink = TbLinks.getTabLinkList(UserCode);
            strNav = new StringBuilder();
            for (int i = 0; i < TabLink.CT_Tab_LinkList.Count; i++)
            {
                if (TabLink.CT_Tab_LinkList[i].TL_Parent == 0)
                {
                    if (i != 0)
                    {
                        strNav.Append("<li class=\"nav_line\"></li>");
                    }
                    strNav.Append(" <li><a href=\"" + TabLink.CT_Tab_LinkList[i].TL_Link + "\" class=\"\"  id=\"nav_" + TabLink.CT_Tab_LinkList[i].TL_Text_EN.Trim() + "\">" + (Interna ? TabLink.CT_Tab_LinkList[i].TL_Text_EN.Trim() : TabLink.CT_Tab_LinkList[i].TL_Text_CN.Trim()) + "</a>");
                    GetSubMenuByParentID(TabLink, TabLink.CT_Tab_LinkList[i].TL_Code, Interna);
                    strNav.Append("</li>");
                }
            }
            return strNav;
        }
        private void GetSubMenuByParentID(MD_TabLinks TabLinkChildren, int TL_Parent, bool Interna)
        {
            MD_TabLinks TabLinks = new MD_TabLinks();
            TabLinks.CT_Tab_LinkList = new List<Model.CT_Tab_Links>();
            for (int j = 0; j < TabLinkChildren.CT_Tab_LinkList.Count; j++)
            {
                if (TabLinkChildren.CT_Tab_LinkList[j].TL_Parent == TL_Parent && TabLinkChildren.CT_Tab_LinkList[j] != null)
                {
                    TabLinks.CT_Tab_LinkList.Add(TabLinkChildren.CT_Tab_LinkList[j]);
                }
            }
            for (int i = 0; i < TabLinks.CT_Tab_LinkList.Count; i++)
            {
                if (i == 0)
                {
                    strNav.Append("<ul style='display:none'>");
                }
                strNav.Append(" <li><a href=\"" + TabLinks.CT_Tab_LinkList[i].TL_Link + "\" class=\"");
                if (0 != TabLinks.CT_Tab_LinkList[i].TL_Parent && TabLinks.CT_Tab_LinkList[i].TL_Children > 0)
                {
                    strNav.Append("rightarrow");
                }
                strNav.Append("\"  id=\"nav_" + TabLinks.CT_Tab_LinkList[i].TL_Text_EN.Trim() + "\">" + " " + (Interna ? TabLinks.CT_Tab_LinkList[i].TL_Text_EN.Trim() : TabLinks.CT_Tab_LinkList[i].TL_Text_CN.Trim()) + " " + "</a>");

                GetSubMenuByParentID(TabLink, TabLinks.CT_Tab_LinkList[i].TL_Code, Interna);

                strNav.Append("</li>");

                if (i == TabLinks.CT_Tab_LinkList.Count - 1)
                {
                    strNav.Append("</ul>");
                }
            }
        }

        public StringBuilder getTabLinkListForHelpSystem(long UserCode, bool Interna, string pagePath, out string pageOnLoad)
        {
            string pageTittle = string.Empty;
            string pageTittleContent = string.Empty;

            pageTittle = TbLinks.getTabName(pagePath, UserCode);
            pageTittleContent = pageTittle;
            TabLink = TbLinks.getTabLinkList(UserCode);
            strNav = new StringBuilder();
            string hrefTittle = string.Empty;
            for (int i = 0; i < TabLink.CT_Tab_LinkList.Count; i++)
            {
                if (TabLink.CT_Tab_LinkList[i].TL_Parent == 0)
                {
                    hrefTittle = Interna ? TabLink.CT_Tab_LinkList[i].TL_Text_EN.Trim() : TabLink.CT_Tab_LinkList[i].TL_Text_CN.Trim();
                    if (hrefTittle == pageTittle)
                    {
                        strNav.Append("<li><span>" + hrefTittle + "</span><ul>");
                        GetSubMenuByID(TabLink, TabLink.CT_Tab_LinkList[i].TL_Code, Interna, hrefTittle);
                        strNav.Append("</ul></li>");
                    }
                    else
                    {
                        strNav.Append("<li data-options=\"state:'closed'\"><span>" + hrefTittle + "</span><ul>");
                        GetSubMenuByID(TabLink, TabLink.CT_Tab_LinkList[i].TL_Code, Interna, hrefTittle);
                        strNav.Append("</ul></li>");
                    }
                }
            }

            if (!String.IsNullOrEmpty(pageTittle))
                pageOnLoad = "window.onload = function () {$('#tt').tabs('add', {title: \'" + pageTittle + "\',content: '<div style=\"padding:10\"><div style=\"padding:10\"><img src=\"/images/HelpDocument/PieChart.png\"/></div>',closable: true}); tittleList ='" + pageTittle + "'}";
            else
                pageOnLoad = "function test(){}";

            return strNav;
        }

        private void GetSubMenuByID(MD_TabLinks TabLinkChildren, int TL_Parent, bool Interna, string hrefTittle)
        {
            MD_TabLinks TabLinks = new MD_TabLinks();
            TabLinks.CT_Tab_LinkList = new List<Model.CT_Tab_Links>();
            string subName = string.Empty;
            string tlCode = string.Empty;
            for (int j = 0; j < TabLinkChildren.CT_Tab_LinkList.Count; j++)
            {
                if (TabLinkChildren.CT_Tab_LinkList[j].TL_Parent == TL_Parent && TabLinkChildren.CT_Tab_LinkList[j] != null)
                {
                    TabLinks.CT_Tab_LinkList.Add(TabLinkChildren.CT_Tab_LinkList[j]);
                }
            }
            if (TabLinks.CT_Tab_LinkList.Count == 0)
                strNav.Append("<li><a href=\"#\" id=nav_" + TL_Parent + " onclick=\"addPanel('" + hrefTittle + "')\">" + hrefTittle + "</a></li>");
            else
            {
                for (int i = 0; i < TabLinks.CT_Tab_LinkList.Count; i++)
                {
                    subName = Interna ? TabLinks.CT_Tab_LinkList[i].TL_Text_EN.Trim() : TabLinks.CT_Tab_LinkList[i].TL_Text_CN.Trim();

                    tlCode = TabLinks.CT_Tab_LinkList[i].TL_Code.ToString();
                    strNav.Append("<li><a href=\"#\" id=nav_" + tlCode + " onclick=\"addPanel('" + subName + "')\">" + subName + "</a></li>");
                }
            }
        }

        /// <summary>
        /// 根据某一Url 获取他的树形层级
        /// </summary>
        /// <param name="UG_Code"></param>
        /// <param name="LinkUrl"></param>
        /// <returns></returns>
        public MD_TabLinks GetTabLevelList(int UG_Code, string LinkUrl)
        {
            return TbLinks.GetTabLevelList(UG_Code, LinkUrl);
        }
        public string GetLevelLink(int UG_Code, string LinkUrl, bool Intern, out string ClassName)
        {
            MD_TabLinks _o = GetTabLevelList(UG_Code, LinkUrl);
            if (_o == null || _o.CT_Tab_LinkList == null)
            {
                ClassName = string.Empty;
                return "";
            }
            else
            {
                string _LinkUrl = string.Empty;
                for (int i = 0; i < _o.CT_Tab_LinkList.Count; i++)
                {
                    CT_Tab_Links _c = _o.CT_Tab_LinkList[i];
                    if (i != _o.CT_Tab_LinkList.Count - 1)
                    {
                        _LinkUrl += "<a class='brown' href='" + _c.TL_Link + "'>" + (Intern ? _c.TL_Text_EN : _c.TL_Text_CN) + "</a>&nbsp;&gt;&nbsp;";
                    }
                    else
                    {
                        _LinkUrl += "<span style='width:300px'>" + (Intern ? _c.TL_Text_EN : _c.TL_Text_CN) + "</span>";
                    }
                }
                ClassName = _o.CT_Tab_LinkList[0].TL_Text_EN;
                return _LinkUrl;
            }
        }
    }
}
