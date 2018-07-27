using CRMTree.Model.Common;
using System;
using System.Text;
namespace Shinfotech.Tools
{
    public class Pager
    {
        /// <summary>
        /// 生成分page代码
        /// </summary>
        /// <param name="page"></param>
        /// <param name="sumpage" />
        /// <param name="sum"></param>
        /// <param name="dangqianye"></param>
        /// <returns></returns>
        public static StringBuilder GetPager(int page, int sumpage, int sum, string dangqianye, string tiaoText, string tiaoValue, string canshu)
        {
            if (tiaoText == null) throw new ArgumentNullException("tiaoText");
            var sbpage = new StringBuilder();
            if (page <= 1)
            {
                if (page == sumpage)
                {
                    sbpage.Append("<a> 1  page</a> | <a>共 1 page</a>");
                }
                else
                {
                    sbpage.Append("<a> 1 page</a> | <a href=\"" + dangqianye + "?page=" + (page + 1) + "&" + tiaoText + "=" + tiaoValue + "\">下    page</a> | <a href=\"" + dangqianye + "?page=" + sumpage + "&" + tiaoText + "=" + tiaoValue + "\">end    page</a> | <a>共 " + sumpage + " page</a>\t\n");
                }
            }
            else
            {
                if (page >= sumpage)
                {
                    sbpage.Append("<a> " + sumpage + " page</a> | <a href=\"" + dangqianye + "?page=1&" + tiaoText + "=" + tiaoValue + "\">1   page</a> | <a href=\"" + dangqianye + "?page=" + (sumpage - 1) + "&" + tiaoText + "=" + tiaoValue + "\">上    page</a> | <a>共 " + sumpage + "page</a>\t\n");
                }
                else
                {
                    sbpage.Append("<a> " + page + "  page</a> | <a href=\"" + dangqianye + "?page=1&" + tiaoText + "=" + tiaoValue + "\">1   page</a> | <a href=\"" + dangqianye + "?page=" + (page - 1) + "&" + tiaoText + "=" + tiaoValue + "\"> 上    page</a> | <a href=\"" + dangqianye + "?page=" + (page + 1) + "&" + tiaoText + "=" + tiaoValue + "\">下    page</a> | <a href=\"" + dangqianye + "?page=" + sumpage + "&" + tiaoText + "=" + tiaoValue + "\">end    page</a> | <a>共 " + sumpage + " page</a>\t\n");
                }
            }
            sbpage.Append(" | Total <span color=red>" + sum + "</span> items" + canshu + "");
            return sbpage;
        }
        /// <summary>
        /// 分page代码
        /// </summary>
        /// <param name="page"></param>
        /// <param name="sumpage"></param>
        /// <param name="sum"></param>
        /// <param name="dangqianye"></param>
        /// <returns></returns>
        public static StringBuilder GetPager(int page, int sumpage, int sum, string dangqianye)
        {
            var sbpage = new StringBuilder();
            if (page <= 1)
            {
                if (page == sumpage)
                {
                    sbpage.Append("<a> 1  page</a> | <a>共 1 page</a>");
                }
                else
                {
                    sbpage.Append("<a> 1 page</a> | <a href=\"" + dangqianye + "?page=" + (page + 1) + "\">下    page</a> | <a href=\"" + dangqianye + "?page=" + sumpage + "\">end    page</a> | <a>共 " + sumpage + " page</a>\t\n");
                }
            }
            else
            {
                if (page >= sumpage)
                {
                    sbpage.Append("<a> " + sumpage + " page</a> | <a href=\"" + dangqianye + "?page=1\">1   page</a> | <a href=\"" + dangqianye + "?page=" + (sumpage - 1) + "\">上    page</a> | <a>共 " + sumpage + "page</a>\t\n");
                }
                else
                {
                    sbpage.Append("<a> " + page + "  page</a> | <a href=\"" + dangqianye + "?page=1\">1   page</a> | <a href=\"" + dangqianye + "?page=" + (page - 1) + "\"> 上    page</a> | <a href=\"" + dangqianye + "?page=" + (page + 1) + "\">下    page</a> | <a href=\"" + dangqianye + "?page=" + sumpage + "\">end    page</a> | <a>共 " + sumpage + " page</a>\t\n");
                }
            }
            sbpage.Append(" | Total <span color=red>" + sum + "</span> items");
            return sbpage;
        }
        /// <summary>
        /// 分page
        /// </summary>
        /// <param name="page"></param>
        /// <param name="sumpage"></param>
        /// <param name="sum"></param>
        /// <param name="dangqianye"></param>
        /// <returns></returns>
        public static StringBuilder fenPage(int page, int sumpage, string dangqianye, string houzui)
        {
            var sbPage = new StringBuilder();
            if (page > 0)
            {
                sbPage.Append("<a");
                if (page > 1)
                {

                    sbPage.Append(" href=\"");
                    sbPage.Append(dangqianye + (page - 1) + houzui);
                    sbPage.Append("\"");

                }
                sbPage.Append(">pageUp</a>");

                if (sumpage < 10)
                {
                    for (int i = 1; i <= sumpage; i++)
                    {
                        switch (page == i)
                        {
                            case true:
                                sbPage.Append("<a class=\"current\" href=\"");
                                break;
                            case false:
                                sbPage.Append("<a href=\"");
                                break;
                        }
                        sbPage.Append(dangqianye + i + houzui);
                        sbPage.Append("\">");
                        sbPage.Append(i);
                        sbPage.Append("</a>");
                    }
                }
                else
                {
                    if (page > sumpage)
                    {
                        page = sumpage;
                    }
                    else if (page < 1)
                    {
                        page = 1;
                    }
                    switch (page == 1)
                    {
                        case true:
                            sbPage.Append("<a class=\"current\" href=\"");
                            break;
                        case false:
                            sbPage.Append("<a href=\"");
                            break;
                    }
                    sbPage.Append(dangqianye + 1 + houzui);
                    sbPage.Append("\">1</a>");

                    switch (page == 2)
                    {
                        case true:
                            sbPage.Append("<a class=\"current\" href=\"");
                            break;
                        case false:
                            sbPage.Append("<a href=\"");
                            break;
                    }
                    sbPage.Append(dangqianye + 2 + houzui);
                    sbPage.Append("\">2</a>");

                    if (page < 5)
                    {
                        for (int i = 3; i < page + 3; i++)
                        {
                            switch (page == i)
                            {
                                case true:
                                    sbPage.Append("<a class=\"current\" href=\"");
                                    break;
                                case false:
                                    sbPage.Append("<a href=\"");
                                    break;
                            }
                            sbPage.Append(dangqianye + i + houzui);
                            sbPage.Append("\">");
                            sbPage.Append(i);
                            sbPage.Append("</a>");
                        }
                        sbPage.Append("<span class=\"etc\">...</span>");
                    }
                    else if (page > 4 && page < (sumpage - 4))
                    {
                        sbPage.Append("<span class=\"etc\">...</span>");
                        for (int i = page - 2; i < page + 3; i++)
                        {
                            switch (page == i)
                            {
                                case true:
                                    sbPage.Append("<a class=\"current\" href=\"");
                                    break;
                                case false:
                                    sbPage.Append("<a href=\"");
                                    break;
                            }
                            sbPage.Append(dangqianye + i + houzui);
                            sbPage.Append("\">");
                            sbPage.Append(i);
                            sbPage.Append("</a>");
                        }
                        sbPage.Append("<span class=\"etc\">...</span>");
                    }
                    else
                    {
                        sbPage.Append("<span class=\"etc\">...</span>");
                        for (int i = page - 2; i < (sumpage - 1); i++)
                        {
                            switch (page == i)
                            {
                                case true:
                                    sbPage.Append("<a class=\"current\" href=\"");
                                    break;
                                case false:
                                    sbPage.Append("<a href=\"");
                                    break;
                            }
                            sbPage.Append(dangqianye + i + houzui);
                            sbPage.Append("\">");
                            sbPage.Append(i);
                            sbPage.Append("</a>");
                        }
                    }

                    switch (page == sumpage - 1)
                    {
                        case true:
                            sbPage.Append("<a class=\"current\" href=\"");
                            break;
                        case false:
                            sbPage.Append("<a href=\"");
                            break;
                    }
                    sbPage.Append(dangqianye + (sumpage - 1) + houzui);
                    sbPage.Append("\">");
                    sbPage.Append(sumpage - 1);
                    sbPage.Append("</a>");

                    switch (page == sumpage)
                    {
                        case true:
                            sbPage.Append("<a class=\"current\" href=\"");
                            break;
                        case false:
                            sbPage.Append("<a href=\"");
                            break;
                    }
                    sbPage.Append(dangqianye + sumpage + houzui);
                    sbPage.Append("\">");
                    sbPage.Append(sumpage);
                    sbPage.Append("</a>");
                }
                sbPage.Append("<a");
                if (page < sumpage)
                {
                    sbPage.Append(" href=\"");
                    sbPage.Append(dangqianye + (page + 1) + houzui);
                    sbPage.Append("\"");
                }
                sbPage.Append(">pageDown</a>");
            }
            return sbPage;
        }
        /// <summary>
        /// 分page代码
        /// </summary>
        /// <param name="page"></param>
        /// <param name="sumpage"></param>
        /// <param name="sum"></param>
        /// <param name="dangqianye"></param>
        /// <returns></returns>
        public static StringBuilder GetPager(int page, int sumpage, int sum, string dangqianye, string tiaojian)
        {
            StringBuilder sbfen = new StringBuilder();
            if (page <= 1)
            {
                if (page == sumpage)
                {
                    sbfen.Append("<a>·1page·</a> | <a>共 1 page</a>");
                }
                else
                {
                    sbfen.Append("<a> 1 page</a> | <a href=\"" + dangqianye + "?page=" + (page + 1) + "" + tiaojian + "\">下    page</a> | <a href=\"" + dangqianye + "?page=" + sumpage + "" + tiaojian + "\">end    page</a> | <a>共 " + sumpage + " page</a>\t\n");
                }
            }
            else
            {
                if (page >= sumpage)
                {
                    sbfen.Append("<a> " + sumpage + " page</a> | <a href=\"" + dangqianye + "?page=1" + tiaojian + "\">1   page</a> | <a href=\"" + dangqianye + "?page=" + (sumpage - 1) + "" + tiaojian + "\">上    page</a> | <a>共 " + sumpage + "page</a>\t\n");
                }
                else
                {
                    sbfen.Append("<a> " + page + "  page</a> | <a href=\"" + dangqianye + "?page=1" + tiaojian + "\">1   page</a> | <a href=\"" + dangqianye + "?page=" + (page - 1) + "" + tiaojian + "\"> 上    page</a> | <a href=\"" + dangqianye + "?page=" + (page + 1) + "" + tiaojian + "\">下    page</a> | <a href=\"" + dangqianye + "?page=" + sumpage + "" + tiaojian + "\">end    page</a> | <a>共 " + sumpage + " page</a>\t\n");
                }
            }
            sbfen.Append(" | Total <span color=red>" + sum + "</span> items");
            return sbfen;
        }

        /// <summary>
        /// 分page
        /// </summary>
        /// <param name="page"></param>
        /// <param name="sumpage"></param>
        /// <param name="sum"></param>
        /// <param name="dangqianye"></param>
        /// <returns></returns>
        public static StringBuilder fenPage(int page, int sumpage, int sum, string dangqianye, string tiaojian)
        {
            var sbPage = new StringBuilder();
            sbPage.Append("<a  href=\"");
            sbPage.Append(dangqianye + "?page=1\"><<Page1</a>");
            sbPage.Append("<a");
            if (page > 1)
            {
                sbPage.Append(" href=\"");
                sbPage.Append(dangqianye);
                sbPage.Append("?page=");
                sbPage.Append(page - 1);
                sbPage.Append(tiaojian);
                sbPage.Append("\"");
            }
            sbPage.Append(">pageUp</a>");

            if (sumpage < 10)
            {
                for (int i = 1; i <= sumpage; i++)
                {
                    switch (page == i)
                    {
                        case true:
                            sbPage.Append("<a class=\"current\" href=\"");
                            break;
                        case false:
                            sbPage.Append("<a href=\"");
                            break;
                    }
                    sbPage.Append(dangqianye);
                    sbPage.Append("?page=");
                    sbPage.Append(i);
                    sbPage.Append(tiaojian);
                    sbPage.Append("\">");
                    sbPage.Append(i);
                    sbPage.Append("</a>");
                }
            }
            else
            {
                if (page > sumpage)
                {
                    page = sumpage;
                }
                else if (page < 1)
                {
                    page = 1;
                }
                switch (page == 1)
                {
                    case true:
                        sbPage.Append("<a class=\"current\" href=\"");
                        break;
                    case false:
                        sbPage.Append("<a href=\"");
                        break;
                }
                sbPage.Append(dangqianye);
                sbPage.Append("?page=1");
                sbPage.Append(tiaojian);
                sbPage.Append("\">1</a>");

                switch (page == 2)
                {
                    case true:
                        sbPage.Append("<a class=\"current\" href=\"");
                        break;
                    case false:
                        sbPage.Append("<a href=\"");
                        break;
                }
                sbPage.Append(dangqianye);
                sbPage.Append("?page=2");
                sbPage.Append(tiaojian);
                sbPage.Append("\">2</a>");

                if (page < 5)
                {
                    for (int i = 3; i < page + 3; i++)
                    {
                        switch (page == i)
                        {
                            case true:
                                sbPage.Append("<a class=\"current\" href=\"");
                                break;
                            case false:
                                sbPage.Append("<a href=\"");
                                break;
                        }
                        sbPage.Append(dangqianye);
                        sbPage.Append("?page=");
                        sbPage.Append(i);
                        sbPage.Append(tiaojian);
                        sbPage.Append("\">");
                        sbPage.Append(i);
                        sbPage.Append("</a>");
                    }
                    sbPage.Append("<span class=\"etc\">...</span>");
                }
                else if (page > 4 && page < (sumpage - 4))
                {
                    sbPage.Append("<span class=\"etc\">...</span>");
                    for (int i = page - 2; i < page + 3; i++)
                    {
                        switch (page == i)
                        {
                            case true:
                                sbPage.Append("<a class=\"current\" href=\"");
                                break;
                            case false:
                                sbPage.Append("<a href=\"");
                                break;
                        }
                        sbPage.Append(dangqianye);
                        sbPage.Append("?page=");
                        sbPage.Append(i);
                        sbPage.Append(tiaojian);
                        sbPage.Append("\">");
                        sbPage.Append(i);
                        sbPage.Append("</a>");
                    }
                    sbPage.Append("<span class=\"etc\">...</span>");
                }
                else
                {
                    sbPage.Append("<span class=\"etc\">...</span>");
                    for (int i = page - 2; i < (sumpage - 1); i++)
                    {
                        switch (page == i)
                        {
                            case true:
                                sbPage.Append("<a class=\"current\" href=\"");
                                break;
                            case false:
                                sbPage.Append("<a href=\"");
                                break;
                        }
                        sbPage.Append(dangqianye);
                        sbPage.Append("?page=");
                        sbPage.Append(i);
                        sbPage.Append(tiaojian);
                        sbPage.Append("\">");
                        sbPage.Append(i);
                        sbPage.Append("</a>");
                    }
                }

                switch (page == sumpage - 1)
                {
                    case true:
                        sbPage.Append("<a class=\"current\" href=\"");
                        break;
                    case false:
                        sbPage.Append("<a href=\"");
                        break;
                }
                sbPage.Append(dangqianye);
                sbPage.Append("?page=");
                sbPage.Append(sumpage - 1);
                sbPage.Append(tiaojian);
                sbPage.Append("\">");
                sbPage.Append(sumpage - 1);
                sbPage.Append("</a>");

                switch (page == sumpage)
                {
                    case true:
                        sbPage.Append("<a class=\"current\" href=\"");
                        break;
                    case false:
                        sbPage.Append("<a href=\"");
                        break;
                }
                sbPage.Append(dangqianye);
                sbPage.Append("?page=");
                sbPage.Append(sumpage);
                sbPage.Append(tiaojian);
                sbPage.Append("\">");
                sbPage.Append(sumpage);
                sbPage.Append("</a>");
            }
            sbPage.Append("<a");
            if (page < sumpage)
            {
                sbPage.Append(" href=\"");
                sbPage.Append(dangqianye);
                sbPage.Append("?page=");
                sbPage.Append(page + 1);
                sbPage.Append(tiaojian);
                sbPage.Append("\"");
            }
            sbPage.Append(">pageDown</a>");
            sbPage.Append("<a  href=\"");
            sbPage.Append(dangqianye + "?page=" + sumpage + "\">pageEnd>></a>");
            sbPage.Append(" | Total <span color=red>" + sum + "</span> items");
            return sbPage;
        }

        /// <summary>
        /// 分page
        /// </summary>
        /// <param name="page"></param>
        /// <param name="sumpage"></param>
        /// <param name="sum"></param>
        /// <param name="dangqianye"></param>
        /// <returns></returns>
        public static StringBuilder userListPage(int page, int sumpage, int sum, string dangqianye, string tiaojian)
        {
            var sbPage = new StringBuilder();
            sbPage.Append("<div class=\"page clearfix\"><ul class=\"pagelist\">");
            sbPage.Append("<li><a  title=\"Page1\" href=\"");
            sbPage.Append(dangqianye + "?page=1");
            sbPage.Append(tiaojian);
            sbPage.Append("\"><<</a></li>");

            if (page > 1)
            {
                sbPage.Append("<li><a  title=\"pageUp\" ");
                sbPage.Append(" href=\"");
                sbPage.Append(dangqianye);
                sbPage.Append("?page=");
                sbPage.Append(page - 1);
                sbPage.Append(tiaojian);
                sbPage.Append("\"");
                sbPage.Append(">＜</a></li>");
            }


            if (sumpage < 10)
            {
                for (int i = 1; i <= sumpage; i++)
                {
                    switch (page == i)
                    {
                        case true:
                            sbPage.Append("<li class=\"current\">");
                            sbPage.Append(i);
                            sbPage.Append("</li>");
                            break;
                        case false:
                            sbPage.Append("<li><a  href=\"");
                            sbPage.Append(dangqianye);
                            sbPage.Append("?page=");
                            sbPage.Append(i);
                            sbPage.Append(tiaojian);
                            sbPage.Append("\">");
                            sbPage.Append(i);
                            sbPage.Append("</a></li>");
                            break;
                    }

                }
            }
            else
            {
                if (page > sumpage)
                {
                    page = sumpage;
                }
                else if (page < 1)
                {
                    page = 1;
                }
                switch (page == 1)
                {
                    case true:
                        sbPage.Append("<li class=\"current\">");
                        sbPage.Append("1</li>");
                        break;
                    case false:
                        sbPage.Append("<li><a href=\"");
                        sbPage.Append(dangqianye);
                        sbPage.Append("?page=1");
                        sbPage.Append(tiaojian);
                        sbPage.Append("\">1</a></li>");
                        break;
                }

                switch (page == 2)
                {
                    case true:
                        sbPage.Append("<li class=\"current\">");
                        sbPage.Append("2</li>");
                        break;
                    case false:
                        sbPage.Append("<li><a href=\"");
                        sbPage.Append(dangqianye);
                        sbPage.Append("?page=2");
                        sbPage.Append(tiaojian);
                        sbPage.Append("\">2</a></li>");
                        break;
                }

                if (page < 5)
                {
                    for (int i = 3; i < page + 3; i++)
                    {
                        switch (page == i)
                        {
                            case true:
                                sbPage.Append("<li class=\"current\">");
                                sbPage.Append(i);
                                sbPage.Append("</li>");
                                break;
                            case false:
                                sbPage.Append("<li><a href=\"");
                                sbPage.Append(dangqianye);
                                sbPage.Append("?page=");
                                sbPage.Append(i);
                                sbPage.Append(tiaojian);
                                sbPage.Append("\">");
                                sbPage.Append(i);
                                sbPage.Append("</a></li>");
                                break;
                        }

                    }
                    sbPage.Append("<li><span class=\"etc\">...</span></li>");
                }
                else if (page > 4 && page < (sumpage - 4))
                {
                    sbPage.Append("<li><span class=\"etc\">...</span></li>");
                    for (int i = page - 2; i < page + 3; i++)
                    {
                        switch (page == i)
                        {
                            case true:
                                sbPage.Append("<li class=\"current\">");
                                sbPage.Append(i);
                                sbPage.Append("</li>");
                                break;
                            case false:
                                sbPage.Append("<li><a href=\"");
                                sbPage.Append(dangqianye);
                                sbPage.Append("?page=");
                                sbPage.Append(i);
                                sbPage.Append(tiaojian);
                                sbPage.Append("\">");
                                sbPage.Append(i);
                                sbPage.Append("</a></li>");
                                break;
                        }

                    }
                    sbPage.Append("<li><span class=\"etc\">...</span></li>");
                }
                else
                {
                    sbPage.Append("<li><span class=\"etc\">...</span></li>");
                    for (int i = page - 2; i < (sumpage - 1); i++)
                    {
                        switch (page == i)
                        {
                            case true:
                                sbPage.Append("<li class=\"current\">");
                                sbPage.Append(i);
                                sbPage.Append("</li>");
                                break;
                            case false:
                                sbPage.Append("<li><a href=\"");
                                sbPage.Append(dangqianye);
                                sbPage.Append("?page=");
                                sbPage.Append(i);
                                sbPage.Append(tiaojian);
                                sbPage.Append("\">");
                                sbPage.Append(i);
                                sbPage.Append("</a></li>");
                                break;
                        }

                    }
                }

                switch (page == sumpage - 1)
                {
                    case true:
                        sbPage.Append("<li class=\"current\" >");
                        sbPage.Append(sumpage - 1);
                        sbPage.Append("</li>");
                        break;
                    case false:
                        sbPage.Append("<li><a href=\"");
                        sbPage.Append(dangqianye);
                        sbPage.Append("?page=");
                        sbPage.Append(sumpage - 1);
                        sbPage.Append(tiaojian);
                        sbPage.Append("\">");
                        sbPage.Append(sumpage - 1);
                        sbPage.Append("</a></li>");
                        break;
                }








                switch (page == sumpage)
                {
                    case true:
                        sbPage.Append("<li class=\"current\">");
                        sbPage.Append(sumpage);
                        sbPage.Append("</li>");
                        break;
                    case false:
                        sbPage.Append("<li><a href=\"");
                        sbPage.Append(dangqianye);
                        sbPage.Append("?page=");
                        sbPage.Append(sumpage);
                        sbPage.Append(tiaojian);
                        sbPage.Append("\">");
                        sbPage.Append(sumpage);
                        sbPage.Append("</a></li>");
                        break;
                }

            }

            if (page < sumpage)
            {
                sbPage.Append("<li><a title=\"pageDown\"");
                sbPage.Append(" href=\"");
                sbPage.Append(dangqianye);
                sbPage.Append("?page=");
                sbPage.Append(page + 1);
                sbPage.Append(tiaojian);
                sbPage.Append("\"");
                sbPage.Append(">＞</a></li>");
            }

            sbPage.Append("<li><a title=\"pageEnd\" href=\"");
            sbPage.Append(dangqianye + "?page=" + sumpage + tiaojian + "\">>></a></li></ul></div>");
            //sbPage.Append(" | Total <span color=red>" + sum + "</span> items");
            return sbPage;
        }


        /// <summary>
        /// 频道分page
        /// </summary>
        /// <param name="page"></param>
        /// <param name="sumpage"></param>
        /// <param name="sum"></param>
        /// <param name="dangqianye"></param>
        /// <returns></returns>
        public static StringBuilder channelListPage(int page, int sumpage, int sum)
        {
            var sbPage = new StringBuilder();
            sbPage.Append("<div id=\"pageList\" class=\"page clearfix\"><ul class=\"pagelist\">");

            if (page > 1)
            {
                sbPage.Append("<li><a  title=\"pageUp\" ");
                sbPage.Append(" href=\"javaScript:InitTable(" + (page - 1) + ")\"");
                sbPage.Append("><<</a></li>");
            }
            else
            {
                sbPage.Append("<li title=\"pageUp\"><<</li>");
            }

            if (sumpage < 10)
            {
                for (int i = 1; i <= sumpage; i++)
                {
                    switch (page == i)
                    {
                        case true:
                            sbPage.Append("<li class=\"current\">");
                            sbPage.Append(i);
                            sbPage.Append("</li>");
                            break;
                        case false:
                            sbPage.Append("<li><a title=\"" + i + "page\" href=\"javaScript:InitTable(" + i + ")\">");
                            sbPage.Append(i);
                            sbPage.Append("</a></li>");
                            break;
                    }

                }
            }
            else
            {
                if (page > sumpage)
                {
                    page = sumpage;
                }
                else if (page < 1)
                {
                    page = 1;
                }
                switch (page == 1)
                {
                    case true:
                        sbPage.Append("<li class=\"current\">");
                        sbPage.Append("1</li>");
                        break;
                    case false:
                        sbPage.Append("<li><a  title=\"" + 1 + "page\"  href=\"javaScript:InitTable(1)\">1</a></li>");
                        break;
                }


                switch (page == 2)
                {
                    case true:
                        sbPage.Append("<li class=\"current\">");
                        sbPage.Append("2</li>");
                        break;
                    case false:
                        sbPage.Append("<li><a title=\"" + 2 + "page\" href=\"javaScript:InitTable(2)\">2</a></li>");
                        break;
                }

                if (page < 5)
                {
                    for (int i = 3; i < page + 3; i++)
                    {
                        switch (page == i)
                        {
                            case true:
                                sbPage.Append("<li class=\"current\">");
                                sbPage.Append(i);
                                sbPage.Append("</li>");
                                break;
                            case false:
                                sbPage.Append("<li><a title=\"" + i + "page\" href=\"javaScript:InitTable(" + i + ")\">");
                                sbPage.Append(i);
                                sbPage.Append("</a></li>");
                                break;
                        }

                    }
                    sbPage.Append("<li><span class=\"etc\">...</span></li>");
                }
                else if (page > 4 && page < (sumpage - 4))
                {
                    sbPage.Append("<li><span class=\"etc\">...</span></li>");
                    for (int i = page - 2; i < page + 3; i++)
                    {
                        switch (page == i)
                        {
                            case true:
                                sbPage.Append("<li class=\"current\">");
                                sbPage.Append(i);
                                sbPage.Append("</li>");
                                break;
                            case false:
                                sbPage.Append("<li><a title=\"" + i + "page\" href=\"javaScript:InitTable(" + i + ")\">");
                                sbPage.Append(i);
                                sbPage.Append("</a></li>");
                                break;
                        }

                    }
                    sbPage.Append("<li><span class=\"etc\">...</span></li>");
                }
                else
                {
                    sbPage.Append("<li><span class=\"etc\">...</span></li>");
                    for (int i = page - 2; i < (sumpage - 1); i++)
                    {
                        switch (page == i)
                        {
                            case true:
                                sbPage.Append("<li class=\"current\">");
                                sbPage.Append(i);
                                sbPage.Append("</li>");
                                break;
                            case false:
                                sbPage.Append("<li><a title=\"" + i + "page\" href=\"javaScript:InitTable(" + i + ")\">");
                                sbPage.Append(i);
                                sbPage.Append("</a></li>");
                                break;
                        }

                    }
                }

                switch (page == sumpage - 1)
                {
                    case true:
                        sbPage.Append("<li class=\"current\" >");
                        sbPage.Append(sumpage - 1);
                        sbPage.Append("</li>");
                        break;
                    case false:
                        sbPage.Append("<li><a title=\"" + (sumpage - 1) + "page\" href=\"javaScript:InitTable(" + (sumpage - 1) + ")\">");
                        sbPage.Append(sumpage - 1);
                        sbPage.Append("</a></li>");
                        break;
                }








                switch (page == sumpage)
                {
                    case true:
                        sbPage.Append("<li class=\"current\">");
                        sbPage.Append(sumpage);
                        sbPage.Append("</li>");
                        break;
                    case false:
                        sbPage.Append("<li><a title=\"" + sumpage + "page\"  href=\"javaScript:InitTable(" + sumpage + ")\">");
                        sbPage.Append(sumpage);
                        sbPage.Append("</a></li>");
                        break;
                }

            }

            if (page < sumpage)
            {
                sbPage.Append("<li><a title=\"pageDown\"");
                sbPage.Append(" href=\"javaScript:InitTable(" + (page + 1) + ")\"");
                sbPage.Append(">>></a></li>");
            }
            else
            {
                sbPage.Append("<li title=\"pageDown\">>></li>");
            }
            sbPage.Append("</ul></div>");
            return sbPage;
        }


        /// <summary>
        /// 查询分page
        /// </summary>
        /// <param name="page"></param>
        /// <param name="sumpage"></param>
        /// <param name="sum"></param>
        /// <param name="dangqianye"></param>
        /// <returns></returns> 
        public static StringBuilder PageList(int page, int sumpage, string dangqianye, string houzui)
        {
            var sbPage = new StringBuilder();
            sbPage.Append("<a");
            if (page > 1)
            {
                sbPage.Append(" href=\"");
                sbPage.Append(dangqianye + (page - 1) + houzui);
                sbPage.Append("\"");
            }
            sbPage.Append(">pageUp</a>");

            if (sumpage < 10)
            {
                for (int i = 1; i <= sumpage; i++)
                {
                    switch (page == i)
                    {
                        case true:
                            sbPage.Append("<a class=\"current\" href=\"");
                            break;
                        case false:
                            sbPage.Append("<a href=\"");
                            break;
                    }
                    if (i == 1)
                    {
                        sbPage.Append(dangqianye + houzui);
                    }
                    else
                    {
                        sbPage.Append(dangqianye + i + houzui);
                    }
                    sbPage.Append("\">");
                    sbPage.Append(i);
                    sbPage.Append("</a>");
                }
            }
            else
            {
                if (page > sumpage)
                {
                    page = sumpage;
                }
                else if (page < 1)
                {
                    page = 1;
                }
                switch (page == 1)
                {
                    case true:
                        sbPage.Append("<a class=\"current\" href=\"");
                        break;
                    case false:
                        sbPage.Append("<a href=\"");
                        break;
                }
                if (page == 1)
                {
                    sbPage.Append(dangqianye + houzui);
                }
                else
                {
                    sbPage.Append(dangqianye + 1 + houzui);
                }
                sbPage.Append("\">1</a>");

                switch (page == 2)
                {
                    case true:
                        sbPage.Append("<a class=\"current\" href=\"");
                        break;
                    case false:
                        sbPage.Append("<a href=\"");
                        break;
                }
                sbPage.Append(dangqianye + 2 + houzui);
                sbPage.Append("\">2</a>");

                if (page < 5)
                {
                    for (int i = 3; i < page + 3; i++)
                    {
                        switch (page == i)
                        {
                            case true:
                                sbPage.Append("<a class=\"current\" href=\"");
                                break;
                            case false:
                                sbPage.Append("<a href=\"");
                                break;
                        }
                        if (i == 1)
                        {
                            sbPage.Append(dangqianye + houzui);
                        }
                        else
                        {
                            sbPage.Append(dangqianye + i + houzui);
                        }
                        sbPage.Append("\">");
                        sbPage.Append(i);
                        sbPage.Append("</a>");
                    }
                    sbPage.Append("<span class=\"etc\">...</span>");
                }
                else if (page > 4 && page < (sumpage - 4))
                {
                    sbPage.Append("<span class=\"etc\">...</span>");
                    for (int i = page - 2; i < page + 3; i++)
                    {
                        switch (page == i)
                        {
                            case true:
                                sbPage.Append("<a class=\"current\" href=\"");
                                break;
                            case false:
                                sbPage.Append("<a href=\"");
                                break;
                        }
                        if (i == 1)
                        {
                            sbPage.Append(dangqianye + houzui);
                        }
                        else
                        {
                            sbPage.Append(dangqianye + i + houzui);
                        }
                        sbPage.Append("\">");
                        sbPage.Append(i);
                        sbPage.Append("</a>");
                    }
                    sbPage.Append("<span class=\"etc\">...</span>");
                }
                else
                {
                    sbPage.Append("<span class=\"etc\">...</span>");
                    for (int i = page - 2; i < (sumpage - 1); i++)
                    {
                        switch (page == i)
                        {
                            case true:
                                sbPage.Append("<a class=\"current\" href=\"");
                                break;
                            case false:
                                sbPage.Append("<a href=\"");
                                break;
                        }
                        if (i == 1)
                        {
                            sbPage.Append(dangqianye + houzui);
                        }
                        else
                        {
                            sbPage.Append(dangqianye + i + houzui);
                        }
                        sbPage.Append("\">");
                        sbPage.Append(i);
                        sbPage.Append("</a>");
                    }
                }

                switch (page == sumpage - 1)
                {
                    case true:
                        sbPage.Append("<a class=\"current\" href=\"");
                        break;
                    case false:
                        sbPage.Append("<a href=\"");
                        break;
                }
                if (page == 1)
                {
                    sbPage.Append(dangqianye + houzui);
                }
                else
                {
                    sbPage.Append(dangqianye + (sumpage - 1) + houzui);
                }

                sbPage.Append("\">");
                sbPage.Append(sumpage - 1);
                sbPage.Append("</a>");

                switch (page == sumpage)
                {
                    case true:
                        sbPage.Append("<a class=\"current\" href=\"");
                        break;
                    case false:
                        sbPage.Append("<a href=\"");
                        break;
                }
                if (page == 1)
                {
                    sbPage.Append(dangqianye + houzui);
                }
                else
                {
                    sbPage.Append(dangqianye + sumpage + houzui);
                }

                sbPage.Append("\">");
                sbPage.Append(sumpage);
                sbPage.Append("</a>");
            }
            sbPage.Append("<a");
            if (page < sumpage)
            {
                sbPage.Append(" href=\"");
                sbPage.Append(dangqianye + (page + 1) + houzui);
                sbPage.Append("\"");
            }
            sbPage.Append(">pageDown</a>");
            return sbPage;
        }

        #region 地址重写分page
        /// <summary>
        /// 功能：地址重写分page
        /// create by 王鹏程 on 2011-03-07
        /// </summary>
        /// <param name="strRootUrl">根目录url以http开始,以"/"结束</param>
        /// <param name="intCurrentPage">current 几page</param>
        /// <param name="intPageCount">总page数</param>
        /// <param name="strRewriteUrl">地址重写不包含current 几page，以"/"开始和结束</param>
        /// <param name="strCssClassCurrentPage">current pagea标签css的class样式</param>
        /// <param name="strCssClassFillChar">省略符号css的class样式</param>
        /// <returns>StringBuilder</returns>
        public static StringBuilder RewriteUrl(string strRootUrl, int intCurrentPage, int intPageCount, string strRewriteUrl, string strCssClassCurrentPage, string strCssClassFillChar)
        {
            var sbPage = new StringBuilder();

            if (intCurrentPage > 1)
            {
                sbPage.Append("<a href=\"");
                sbPage.Append(strRootUrl);
                sbPage.Append(intCurrentPage - 1);
                sbPage.Append(strRewriteUrl);
                sbPage.Append("\">pageUp</a>");
                if (intPageCount < 10)
                {
                    for (int i = 1; i <= intPageCount; i++)
                    {
                        switch (intCurrentPage == i)
                        {
                            case true:
                                sbPage.Append("<a class=\"" + strCssClassCurrentPage + "\" href=\"");
                                break;
                            case false:
                                sbPage.Append("<a href=\"");
                                break;
                        }
                        sbPage.Append(strRootUrl);
                        sbPage.Append(i);
                        sbPage.Append(strRewriteUrl);
                        sbPage.Append("\">");
                        sbPage.Append(i);
                        sbPage.Append("</a>");
                    }
                }
                else
                {
                    if (intCurrentPage > intPageCount)
                    {
                        intCurrentPage = intPageCount;
                    }
                    else if (intCurrentPage < 1)
                    {
                        intCurrentPage = 1;
                    }
                    switch (intCurrentPage == 1)
                    {
                        case true:
                            sbPage.Append("<a class=\"" + strCssClassCurrentPage + "\" href=\"");
                            break;
                        case false:
                            sbPage.Append("<a href=\"");
                            break;
                    }
                    sbPage.Append(strRootUrl);
                    sbPage.Append("1");
                    sbPage.Append(strRewriteUrl);
                    sbPage.Append("\">1</a>");

                    switch (intCurrentPage == 2)
                    {
                        case true:
                            sbPage.Append("<a class=\"" + strCssClassCurrentPage + "\" href=\"");
                            break;
                        case false:
                            sbPage.Append("<a href=\"");
                            break;
                    }
                    sbPage.Append(strRootUrl);
                    sbPage.Append("2");
                    sbPage.Append(strRewriteUrl);
                    sbPage.Append("\">2</a>");

                    if (intCurrentPage < 5)
                    {
                        for (int i = 3; i < intCurrentPage + 3; i++)
                        {
                            switch (intCurrentPage == i)
                            {
                                case true:
                                    sbPage.Append("<a class=\"" + strCssClassCurrentPage + "\" href=\"");
                                    break;
                                case false:
                                    sbPage.Append("<a href=\"");
                                    break;
                            }
                            sbPage.Append(strRootUrl);
                            sbPage.Append(i);
                            sbPage.Append(strRewriteUrl);
                            sbPage.Append("\">");
                            sbPage.Append(i);
                            sbPage.Append("</a>");
                        }
                        sbPage.Append("<a class=\"" + strCssClassFillChar + "\">...</a>");
                    }
                    else if (intCurrentPage > 4 && intCurrentPage < (intPageCount - 4))
                    {
                        sbPage.Append("<a class=\"" + strCssClassFillChar + "\">...</a>");
                        for (int i = intCurrentPage - 2; i < intCurrentPage + 3; i++)
                        {
                            switch (intCurrentPage == i)
                            {
                                case true:
                                    sbPage.Append("<a class=\"" + strCssClassCurrentPage + "\" href=\"");
                                    break;
                                case false:
                                    sbPage.Append("<a href=\"");
                                    break;
                            }
                            sbPage.Append(strRootUrl);
                            sbPage.Append(i);
                            sbPage.Append(strRewriteUrl);
                            sbPage.Append("\">");
                            sbPage.Append(i);
                            sbPage.Append("</a>");
                        }
                        sbPage.Append("<a class=\"" + strCssClassFillChar + "\">...</a>");
                    }
                    else
                    {
                        sbPage.Append("<a class=\"" + strCssClassFillChar + "\">...</a>");
                        for (int i = intCurrentPage - 2; i < (intPageCount - 1); i++)
                        {
                            switch (intCurrentPage == i)
                            {
                                case true:
                                    sbPage.Append("<a class=\"" + strCssClassCurrentPage + "\" href=\"");
                                    break;
                                case false:
                                    sbPage.Append("<a href=\"");
                                    break;
                            }
                            sbPage.Append(strRootUrl);
                            sbPage.Append(i);
                            sbPage.Append(strRewriteUrl);
                            sbPage.Append("\">");
                            sbPage.Append(i);
                            sbPage.Append("</a>");
                        }
                    }

                    switch (intCurrentPage == intPageCount - 1)
                    {
                        case true:
                            sbPage.Append("<a class=\"" + strCssClassCurrentPage + "\" href=\"");
                            break;
                        case false:
                            sbPage.Append("<a href=\"");
                            break;
                    }
                    sbPage.Append(strRootUrl);
                    sbPage.Append(intPageCount - 1);
                    sbPage.Append(strRewriteUrl);
                    sbPage.Append("\">");
                    sbPage.Append(intPageCount - 1);
                    sbPage.Append("</a>");

                    switch (intCurrentPage == intPageCount)
                    {
                        case true:
                            sbPage.Append("<a class=\"" + strCssClassCurrentPage + "\" href=\"");
                            break;
                        case false:
                            sbPage.Append("<a href=\"");
                            break;
                    }
                    sbPage.Append(strRootUrl);
                    sbPage.Append(intPageCount);
                    sbPage.Append(strRewriteUrl);
                    sbPage.Append("\">");
                    sbPage.Append(intPageCount);
                    sbPage.Append("</a>");
                }
                if (intCurrentPage < intPageCount)
                {
                    sbPage.Append("<a");
                    sbPage.Append(" href=\"");
                    sbPage.Append(strRootUrl);
                    sbPage.Append(intCurrentPage + 1);
                    sbPage.Append(strRewriteUrl);
                    sbPage.Append("\">pageDown</a>");
                }
            }
            if (intCurrentPage > 1)
            {
                sbPage.Append("<a  href=\"");
                sbPage.Append(strRootUrl);
                sbPage.Append(intPageCount);
                sbPage.Append(strRewriteUrl);
                sbPage.Append("\">pageEnd></a>");
            }
            return sbPage;
        }
        #endregion

        #region
        /// <summary>
        /// 车队后台js分page
        /// </summary>
        /// <param name="intCurrentPage">current page</param>
        /// <param name="intTotalPage">总page数</param>
        /// <param name="intTotalRecord">总记录数</param>
        /// <param name="pageNum">eachpage数量</param>
        /// <returns></returns>
        public static StringBuilder JavascriptPagination(bool Interna,int intCurrentPage, int intTotalPage, int intTotalRecord)
        {
            var sbPage = new StringBuilder();
            sbPage.Append("<div class=\"page clearfix\"><ul class=\"pagelist\">");
            sbPage.Append("<li><a  title=\"Page1\"  onclick=\"JavascriptPagination(1)\"  href=\"javascript:;\"");
            sbPage.Append("\"><<</a></li>");
            if (intCurrentPage > 1)
            {
                sbPage.Append("<li><a  title=\"pageUp\" ");
                sbPage.Append(" onclick=\"JavascriptPagination(" + (intCurrentPage - 1) + ")\" href=\"javascript:;\"");
                sbPage.Append("\"");
                sbPage.Append(">＜</a></li>");
            }
            if (intTotalPage < 5)
            {
                for (int i = 1; i <= intTotalPage; i++)
                {
                    switch (intCurrentPage == i)
                    {
                        case true:
                            sbPage.Append("<li class=\"current\">");
                            sbPage.Append(i);
                            sbPage.Append("</li>");
                            break;
                        case false:
                            sbPage.Append("<li><a  onclick=\"JavascriptPagination(" + i + ")\" href=\"javascript:;\"");
                            sbPage.Append(">");
                            sbPage.Append(i);
                            sbPage.Append("</a></li>");
                            break;
                    }

                }
            }
            else
            {
                if (intCurrentPage > intTotalPage)
                {
                    intCurrentPage = intTotalPage;
                }
                else if (intCurrentPage < 1)
                {
                    intCurrentPage = 1;
                }
                if (intCurrentPage < 4)
                {
                    for (int i = 1; i < 5; i++)
                    {
                        switch (intCurrentPage == i)
                        {
                            case true:
                                sbPage.Append("<li class=\"current\">");
                                sbPage.Append(i);
                                sbPage.Append("</li>");
                                break;
                            case false:
                                sbPage.Append("<li><a  onclick=\"JavascriptPagination(" + i + ")\" href=\"javascript:;\"");
                                sbPage.Append(">");
                                sbPage.Append(i);
                                sbPage.Append("</a></li>");
                                break;
                        }

                    }
                }
                else
                {
                    for (int i = intCurrentPage - 3; i < intCurrentPage + 4; i++)
                    {

                        switch (intCurrentPage == i)
                        {
                            case true:
                                sbPage.Append("<li class=\"current\">");
                                sbPage.Append(i);
                                sbPage.Append("</li>");
                                break;
                            case false:
                                if (i <= intTotalPage)
                                {
                                    sbPage.Append("<li><a  onclick=\"JavascriptPagination(" + i + ")\" href=\"javascript:;\"");
                                    sbPage.Append(">");
                                    sbPage.Append(i);
                                    sbPage.Append("</a></li>");
                                }
                                break;

                        }

                    }
                }

                //switch (intCurrentPage == 1)
                //{
                //    case true:
                //        sbPage.Append("<li class=\"current\">");
                //        sbPage.Append("1</li>");
                //        break;
                //    case false:
                //        sbPage.Append("<li><a onclick=\"JavascriptPagination(1)\" href=\"javascript:;\"");
                //        sbPage.Append("\">1</a></li>");
                //        break;
                //}
                //switch (intCurrentPage == 2)
                //{
                //    case true:
                //        sbPage.Append("<li class=\"current\">");
                //        sbPage.Append("2</li>");
                //        break;
                //    case false:
                //        sbPage.Append("<li><a  onclick=\"JavascriptPagination(2)\" href=\"javascript:;\"");
                //        sbPage.Append("\">2</a></li>");
                //        break;
                //}
                //switch (intCurrentPage == 3)
                //{
                //    case true:
                //        sbPage.Append("<li class=\"current\">");
                //        sbPage.Append("3</li>");
                //        break;
                //    case false:
                //        sbPage.Append("<li><a  onclick=\"JavascriptPagination(3)\" href=\"javascript:;\"");
                //        sbPage.Append("\">3</a></li>");
                //        break;
                //}

                //for (int i = 4; i < intCurrentPage + 4; i++)
                //{
                //    switch (intCurrentPage == i)
                //    {
                //        case true:
                //            sbPage.Append("<li class=\"current\">");
                //            sbPage.Append(i);
                //            sbPage.Append("</li>");
                //            break;
                //        case false:
                //            sbPage.Append("<li><a  onclick=\"JavascriptPagination(" + i + ")\" href=\"javascript:;\"");
                //            sbPage.Append("\">");
                //            sbPage.Append(i);
                //            sbPage.Append("</a></li>");
                //            break;
                //    }

                //}
                //sbPage.Append("<li><span class=\"etc\">...</span></li>");
                //}
                //else if (intCurrentPage > 2 && intCurrentPage < (intTotalPage - 2))
                //{
                //    for (int i = intCurrentPage - 1; i < intCurrentPage + 2; i++)
                //    {
                //        switch (intCurrentPage == i)
                //        {
                //            case true:
                //                sbPage.Append("<li class=\"current\">");
                //                sbPage.Append(i);
                //                sbPage.Append("</li>");
                //                break;
                //            case false:
                //                sbPage.Append("<li><a onclick=\"JavascriptPagination(" + i + ")\" href=\"javascript:;\"");
                //                sbPage.Append("\">");
                //                sbPage.Append(i);
                //                sbPage.Append("</a></li>");
                //                break;
                //        }

                //    }
                //    sbPage.Append("<li><span class=\"etc\">...</span></li>");
                //}
                //else
                //{
                //    sbPage.Append("<li><span class=\"etc\">...</span></li>");
                //    for (int i = intCurrentPage - 4; i < (intTotalPage - 1); i++)
                //    {
                //        switch (intCurrentPage == i)
                //        {
                //            case true:
                //                sbPage.Append("<li class=\"current\">");
                //                sbPage.Append(i);
                //                sbPage.Append("</li>");
                //                break;
                //            case false:
                //                sbPage.Append("<li><a onclick=\"JavascriptPagination(" + i + ")\" href=\"javascript:;\"");
                //                sbPage.Append("\">");
                //                sbPage.Append(i);
                //                sbPage.Append("</a></li>");
                //                break;
                //        }

                //    }
                //}

                //    switch (intCurrentPage == intTotalPage - 1)
                //    {
                //        case true:
                //            sbPage.Append("<li class=\"current\" >");
                //            sbPage.Append(intTotalPage - 1);
                //            sbPage.Append("</li>");
                //            break;
                //        case false:
                //            sbPage.Append("<li><a onclick=\"JavascriptPagination(" + (intTotalPage - 1) + ")\" href=\"javascript:;\"");
                //            sbPage.Append("\">");
                //            sbPage.Append(intTotalPage - 1);
                //            sbPage.Append("</a></li>");
                //            break;
                //    }
                //    switch (intCurrentPage == intTotalPage)
                //    {
                //        case true:
                //            sbPage.Append("<li class=\"current\">");
                //            sbPage.Append(intTotalPage);
                //            sbPage.Append("</li>");
                //            break;
                //        case false:
                //            sbPage.Append("<li><a onclick=\"JavascriptPagination(" + intTotalPage + ")\" href=\"javascript:;\"");
                //            sbPage.Append("\">");
                //            sbPage.Append(intTotalPage);
                //            sbPage.Append("</a></li>");
                //            break;
                //    }
            }
            //string Title = Interna ? IN_Language.CGL_Title(EM_International.en_us) : IN_Language.CGL_Title(EM_International.zh_cn);
            if (intCurrentPage < intTotalPage)
            {
                sbPage.Append("<li><a onclick=\"javascript:JavascriptPagination(" + (intCurrentPage + 1) + ")\" title=\"pageDown\"");
                sbPage.Append(" href=\"javascript:;\"");
                sbPage.Append(">＞</a></li>");
            }
            sbPage.Append("<li><a title=\"pageEnd\" onclick=\"JavascriptPagination(" + intTotalPage + ")\" href=\"javascript:;\"");
            sbPage.Append(">>></a></li><li class=\"last\">");
            sbPage.Append("Total <span id=\"spanrowcount\" style=\"color:red;\">");
            sbPage.Append(intTotalRecord);
            sbPage.Append("</span> items | Total <span style=\"color:red;\" id=\"SumPage\">" + intTotalPage + "</span> page | Goto<input type=\"text\" class=\"input-text w40\" id=\"txtToPage\" onfocus=\"SetKeyGoToPage()\" onkeyup=\"value=value.replace(/[^\\d]/g,'')\" />page  <a class=\"btn\" href=\"javascript:;\" id=\"btnGoTo\" onClick=\"GoToPage(" + intTotalPage + ",this);\"><i>icon</i><span>GO</span></a></li></ul>");
            return sbPage;
        }


        /// <summary>
        /// 车队后台js分page
        /// </summary>
        /// <param name="intCurrentPage">current page</param>
        /// <param name="intTotalPage">总page数</param>
        /// <param name="intTotalRecord">总记录数</param>
        /// <param name="pageNum">eachpage数量</param>
        /// <returns></returns>
        public static StringBuilder JavascriptPagination(int intCurrentPage, int intTotalPage, int intTotalRecord, int pageSize)
        {
            var sbPage = new StringBuilder();
            sbPage.Append("<div class=\"page clearfix\"><ul class=\"pagelist\">");
            sbPage.Append("<li><a  title=\"Page1\"  onclick=\"JavascriptPagination(1)\"  href=\"javascript:;\"");
            sbPage.Append("\"><<</a></li>");
            if (intCurrentPage > 1)
            {
                sbPage.Append("<li><a  title=\"pageUp\" ");
                sbPage.Append(" onclick=\"JavascriptPagination(" + (intCurrentPage - 1) + ")\" href=\"javascript:;\"");
                sbPage.Append("\"");
                sbPage.Append(">＜</a></li>");
            }
            if (intTotalPage < 5)
            {
                for (int i = 1; i <= intTotalPage; i++)
                {
                    switch (intCurrentPage == i)
                    {
                        case true:
                            sbPage.Append("<li class=\"current\">");
                            sbPage.Append(i);
                            sbPage.Append("</li>");
                            break;
                        case false:
                            sbPage.Append("<li><a  onclick=\"JavascriptPagination(" + i + ")\" href=\"javascript:;\"");
                            sbPage.Append(">");
                            sbPage.Append(i);
                            sbPage.Append("</a></li>");
                            break;
                    }

                }
            }
            else
            {
                if (intCurrentPage > intTotalPage)
                {
                    intCurrentPage = intTotalPage;
                }
                else if (intCurrentPage < 1)
                {
                    intCurrentPage = 1;
                }
                if (intCurrentPage < 4)
                {
                    for (int i = 1; i < 5; i++)
                    {
                        switch (intCurrentPage == i)
                        {
                            case true:
                                sbPage.Append("<li class=\"current\">");
                                sbPage.Append(i);
                                sbPage.Append("</li>");
                                break;
                            case false:
                                sbPage.Append("<li><a  onclick=\"JavascriptPagination(" + i + ")\" href=\"javascript:;\"");
                                sbPage.Append(">");
                                sbPage.Append(i);
                                sbPage.Append("</a></li>");
                                break;
                        }

                    }
                }
                else
                {
                    for (int i = intCurrentPage - 3; i < intCurrentPage + 4; i++)
                    {

                        switch (intCurrentPage == i)
                        {
                            case true:
                                sbPage.Append("<li class=\"current\">");
                                sbPage.Append(i);
                                sbPage.Append("</li>");
                                break;
                            case false:
                                if (i <= intTotalPage)
                                {
                                    sbPage.Append("<li><a  onclick=\"JavascriptPagination(" + i + ")\" href=\"javascript:;\"");
                                    sbPage.Append(">");
                                    sbPage.Append(i);
                                    sbPage.Append("</a></li>");
                                }
                                break;

                        }

                    }
                }

            }
            if (intCurrentPage < intTotalPage)
            {
                sbPage.Append("<li><a onclick=\"javascript:JavascriptPagination(" + (intCurrentPage + 1) + ")\" title=\"pageDown\"");
                sbPage.Append(" href=\"javascript:;\"");
                sbPage.Append(">＞</a></li>");
            }
            sbPage.Append("<li><a title=\"pageEnd\" onclick=\"JavascriptPagination(" + intTotalPage + ")\" href=\"javascript:;\"");
            sbPage.Append(">>></a></li><li class=\"last\">");
            sbPage.Append("Total<span id=\"spanrowcount\" style=\"color:red;\">");
            sbPage.Append(intTotalRecord);
            sbPage.Append("</span>items | Total <span style=\"color:red;\" id=\"SumPage\">" + intTotalPage + "</span> page to<input type=\"text\" class=\"input-text w40\" id=\"txtToPage\" onfocus=\"SetKeyGoToPage()\" onkeyup=\"value=value.replace(/[^\\d]/g,'')\" />page <a class=\"btn\" href=\"javascript:;\" id=\"btnGoTo\" onClick=\"GoToPage(" + intTotalPage + ");\"><i>icon</i><span>GO</span></a></li></ul><div class=\"page2\">eachpage<input type=\"text\" value=\"" + pageSize + "\" onkeyup=\"value.replace(/[^\\d]/g,'')\" onblur=\"checkNum(this)\"autocomplete=\"off\"  onclick=\"SetKeySearch();\"  class=\"input-text w40 SetInputStyle\" id=\"txtPageSize\" name=\"txtPageSize\" />items<input value=\"GO\" class=\"inputBtn btnSearch\"  onclick=\"javascript:PageClick();\" type=\"button\" /></div></div>");
            return sbPage;
        }
        #endregion

        #region 绿光平台 培训资料JS分page
        /// <summary>
        /// 绿光平台 培训资料JS分page
        /// </summary>
        /// <param name="intCurrentPage">current page</param>
        /// <param name="intTotalPage">总page数</param>
        /// <param name="intTotalRecord">总记录数</param>
        /// <returns></returns>
        public static StringBuilder JavascriptPagination_gree(int intCurrentPage, int intTotalPage, int intTotalRecord, string objDiv)
        {
            var sbPage = new StringBuilder();
            //<div class="pagination"><a class="disabled" href="#">Page1</a><a class="current">上page</a><a href="#">下page</a><a href="#">pageEnd</a></div> disable_
            sbPage.Append("<div class=\"pagination\">");

            sbPage.Append("<a  title=\"Page1\" href=\"javascript:;\"");
            if (intCurrentPage > 1)
            {
                sbPage.Append(" class=\"home\" onclick=\"JavascriptGree(1,'" + objDiv + "')\" ");
            }
            else
            {
                sbPage.Append(" class=\"disable_home\" ");
            }
            sbPage.Append("></a>");

            sbPage.Append("<a title=\"pageUp\" href=\"javascript:;\"");
            if (intCurrentPage > 1)
            {
                sbPage.Append(" class=\"prev\" onclick=\"JavascriptGree(" + (intCurrentPage - 1) + ",'" + objDiv + "')\" ");
            }
            else
            {
                sbPage.Append(" class=\"disable_prev\" ");
            }
            sbPage.Append("></a>");


            sbPage.Append("<a title=\"pageDown\" href=\"javascript:;\"");
            if (intCurrentPage < intTotalPage)
            {
                sbPage.Append(" class=\"next\" onclick=\"JavascriptGree(" + (intCurrentPage + 1) + ",'" + objDiv + "')\" ");
            }
            else
            {
                sbPage.Append(" class=\"disable_next\" ");
            }
            sbPage.Append("></a>");

            sbPage.Append("<a title=\"pageEnd\" href=\"javascript:;\"");
            if (intCurrentPage < intTotalPage)
            {
                sbPage.Append(" class=\"end\" onclick=\"JavascriptGree(" + intTotalPage + ",'" + objDiv + "')\" ");
                // sbPage.Append(" class=\"disabled current\" ");
            }
            else
            {
                sbPage.Append(" class=\"disable_end\" ");
            }

            sbPage.Append("></a>");
            sbPage.Append("</div>");


            return sbPage;
        }
        #endregion


        #region
        /// <summary>
        /// 车队后台js分page
        /// </summary>
        /// <param name="intCurrentPage">current page</param>
        /// <param name="intTotalPage">总page数</param>
        /// <param name="intTotalRecord">总记录数</param>
        /// <returns></returns>
        public static StringBuilder JsPagination(int intCurrentPage, int intTotalPage, int intTotalRecord)
        {
            var sbPage = new StringBuilder();
            sbPage.Append("<div style=\"text-align:center;\">");
            sbPage.Append("<a  title=\"Page1\"  onclick=\"JavascriptPagination(1)\"  href=\"javascript:;\"");
            sbPage.Append("\">Page1</a> | ");
            if (intCurrentPage > 1)
            {
                sbPage.Append("<a  title=\"pageUp\" ");
                sbPage.Append(" onclick=\"JavascriptPagination(" + (intCurrentPage - 1) + ")\" href=\"javascript:;\"");
                sbPage.Append(">pageUp</a> | ");
            }
            if (intTotalPage < 10)
            {
                for (int i = 1; i <= intTotalPage; i++)
                {
                    switch (intCurrentPage == i)
                    {
                        case true:
                            sbPage.Append("<span style=\"color:red\">" + i + "</span>");
                            break;
                        case false:
                            sbPage.Append("<a  onclick=\"JavascriptPagination(" + i + ")\" href=\"javascript:;\"");
                            sbPage.Append("\">");
                            sbPage.Append(i);
                            sbPage.Append("</a> ");
                            break;
                    }

                }
            }
            else
            {
                if (intCurrentPage > intTotalPage)
                {
                    intCurrentPage = intTotalPage;
                }
                else if (intCurrentPage < 1)
                {
                    intCurrentPage = 1;
                }
                switch (intCurrentPage == 1)
                {
                    case true:
                        sbPage.Append("<span style=\"color:red\">1</span>");
                        break;
                    case false:
                        sbPage.Append("<a onclick=\"JavascriptPagination(1)\" href=\"javascript:;\"");
                        sbPage.Append("\">1</a> ");
                        break;
                }
                switch (intCurrentPage == 2)
                {
                    case true:
                        sbPage.Append("<span style=\"color:red\">2</span>");
                        break;
                    case false:
                        sbPage.Append("<a  onclick=\"JavascriptPagination(2)\" href=\"javascript:;\"");
                        sbPage.Append("\">2</a> ");
                        break;
                }

                if (intCurrentPage < 5)
                {
                    for (int i = 3; i < intCurrentPage + 3; i++)
                    {
                        switch (intCurrentPage == i)
                        {
                            case true:
                                sbPage.Append("<span style=\"color:red\">" + i + "</span>");
                                break;
                            case false:
                                sbPage.Append("<a  onclick=\"JavascriptPagination(" + i + ")\" href=\"javascript:;\"");
                                sbPage.Append("\">");
                                sbPage.Append(i);
                                sbPage.Append("</a> ");
                                break;
                        }

                    }
                    sbPage.Append("<span class=\"etc\">...</span>");
                }
                else if (intCurrentPage > 4 && intCurrentPage < (intTotalPage - 4))
                {
                    for (int i = intCurrentPage - 2; i < intCurrentPage + 3; i++)
                    {
                        switch (intCurrentPage == i)
                        {
                            case true:
                                sbPage.Append("<span style=\"color:red\">" + i + "</span>");
                                break;
                            case false:
                                sbPage.Append(" <a onclick=\"JavascriptPagination(" + i + ")\" href=\"javascript:;\"");
                                sbPage.Append("\">");
                                sbPage.Append(i);
                                sbPage.Append("</a> ");
                                break;
                        }

                    }
                    sbPage.Append("<span class=\"etc\">...</span>");
                }
                else
                {
                    sbPage.Append("<span class=\"etc\">...</span>");
                    for (int i = intCurrentPage - 2; i < (intTotalPage - 1); i++)
                    {
                        switch (intCurrentPage == i)
                        {
                            case true:
                                sbPage.Append("<span style=\"color:red\">" + i + "</span>");
                                break;
                            case false:
                                sbPage.Append(" <a onclick=\"JavascriptPagination(" + i + ")\" href=\"javascript:;\"");
                                sbPage.Append("\">");
                                sbPage.Append(i);
                                sbPage.Append("</a>  ");
                                break;
                        }

                    }
                }

                switch (intCurrentPage == intTotalPage - 1)
                {
                    case true:
                        sbPage.Append(intTotalPage - 1);
                        break;
                    case false:
                        sbPage.Append("<a onclick=\"JavascriptPagination(" + (intTotalPage - 1) + ")\" href=\"javascript:;\"");
                        sbPage.Append("\">");
                        sbPage.Append(intTotalPage - 1);
                        sbPage.Append("</a> ");
                        break;
                }
                switch (intCurrentPage == intTotalPage)
                {
                    case true:
                        sbPage.Append(intTotalPage);
                        break;
                    case false:
                        sbPage.Append("<a onclick=\"JavascriptPagination(" + intTotalPage + ")\" href=\"javascript:;\"");
                        sbPage.Append("\">");
                        sbPage.Append(intTotalPage);
                        sbPage.Append("</a> ");
                        break;
                }
            }
            if (intCurrentPage < intTotalPage)
            {
                sbPage.Append(" | <a onclick=\"JavascriptPagination(" + (intCurrentPage + 1) + ")\" title=\"pageDown\"");
                sbPage.Append(" href=\"javascript:;\"");
                sbPage.Append("\"");
                sbPage.Append(">pageDown</a> | ");
            }
            sbPage.Append(" <a title=\"pageEnd\" onclick=\"JavascriptPagination(" + intTotalPage + ")\" href=\"javascript:;\"");
            sbPage.Append("\">pageEnd</a> | ");
            sbPage.Append("Totalitems");
            sbPage.Append(intTotalRecord);
            sbPage.Append("|Total <span style=\"color:red;\" id=\"SumPage\">" + intTotalPage + "</span> page <input type=\"text\" class=\"input-text w40\" id=\"txtToPage\" onfocus=\"SetKeyGoToPage()\" onkeyup=\"value=value.replace(/[^\\d]/g,'')\" /> <a href=\"javascript:;\" id=\"btnGoTo\" onClick=\"GoToPage(" + intTotalPage + ");\">redirect</a></div>");
            return sbPage;
        }
        #endregion
    }
}
