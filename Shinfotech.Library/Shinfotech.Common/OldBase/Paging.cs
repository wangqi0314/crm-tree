using System;
using System.Collections.Generic;
using System.Text;
namespace ShInfoTech.Common
{
    /// <summary>
    /// 分页类
    /// </summary>
    public class Paging
    {
        private int recordCount = 0;
        private int currentPage = 1;
        private int listNum = 3;
        private int pageSize = 10;
        private int showInputNum = 0;

        private bool isAutoDisplay = true;
        private bool isButton = true;
        private bool isInput = true;
        private bool isPreviousAndNext = true;
        private bool isTarget = false;
        private bool isFirstAndLast = true;
        private bool isDropdown = false;
        private bool isRewrite = true;
        private bool isShowNumericButton = true;

        private string pageHrefPath = "#";
        private string pageHrefEnd = "#";
        private string pageSplit = "&pageNo=";
        private string strButtonClass = "";
        private string strButtonStyle = "";
        private string strClass = "";
        private string strStyle = "";
        private string strCurrentPageClass = "";
        private string strCurrentPageStyle = "";
        private string strInputClass = "";
        private string strInputStyle = "border: 1px solid #C2D2DF";
        private string strSpace = "&#xA0;&#xA0;";//空格 相当于&nbsp; 只是XML不支持&nbsp;
        private string pageParms = "";//分页时所带的参数集合

        private string pagePath = "";

        public Paging()
        {

        }

        /// <summary>
        /// 分页输出函数
        /// </summary>
        /// <param name="CurrentPage">当前页</param>
        /// <param name="PageSize">每页显示记录数</param>
        /// <param name="Count">记录总数</param>
        /// <param name="isRewrite">是否是转向地址</param>
        /// <returns>分页HTML代码</returns>
        public string OutPut()
        {
            string retValue = "";
            StringBuilder str = new StringBuilder();
            StringBuilder strTemp = new StringBuilder();


            int pageTotal = this.recordCount / this.pageSize;

            if ((this.recordCount % this.pageSize) > 0)
            {
                pageTotal++;
            }
            if (pageTotal <= 0)
            {
                pageTotal = 1;
            }

            if (this.currentPage > pageTotal)
            {
                this.currentPage = pageTotal;
            }
            //当前显示页数加显示数字按钮数
            int listPageEndNum = this.currentPage + this.listNum;

            if (listPageEndNum > pageTotal)
            {
                listPageEndNum = pageTotal;
            }
            //当前显示页数减去显示数字按钮数
            int listPageStartNum = this.currentPage - this.listNum;

            if (listPageStartNum < 1)
            {
                listPageStartNum = 1;
            }

            string strTarget = "";
            if (this.isTarget)
            {
                strTarget = "target=\"_blank\"";
            }

            if ((this.pageSplit == "&pageNo=") && this.pageHrefPath.EndsWith(".aspx"))
            {
                this.pageSplit = "?pageNo=";
            }

            #region 转向分页
            if (this.IsRewrite == true)
            {
                if (this.currentPage > 1)
                {
                    //首页
                    if (this.isFirstAndLast)
                    {
                        str.Append("<a href=" + this.pageHrefPath + this.pageHrefEnd + " class=\"" + this.strClass + "\" style=\"" + this.strStyle + "\" " + strTarget + ">首页</a>");
                    }
                    //上一页 
                    if (this.isPreviousAndNext)
                    {
                        str.Append(this.strSpace);
                        if (CurrentPage > 2)
                        {
                            string previousPage = Convert.ToString(this.currentPage - 1);
                            str.Append("<a href=" + this.pageHrefPath + this.pageSplit + previousPage + this.pageHrefEnd + " class=\"" + this.strClass + "\" style=\"" + this.strStyle + "\" " + strTarget + ">&lt;&lt;</a>");
                        }
                        else
                        {
                            str.Append("<a href=" + this.pageHrefPath + this.pageHrefEnd + " class=\"" + this.strClass + "\" style=\"" + this.strStyle + "\" " + strTarget + ">&lt;&lt;</a>");
                        }
                    }
                }
                //分页数字列表
                if (this.isShowNumericButton == true)
                {
                    for (int i = listPageStartNum; i <= listPageEndNum; i++)
                    {
                        str.Append(this.strSpace);
                        if (i == this.currentPage)
                        {
                            str.Append("<span class=\"" + this.strCurrentPageClass + "\" style=\"" + this.strCurrentPageStyle + "\">" + i.ToString() + "</span>");
                        }
                        else if (i == 1)
                        {
                            str.Append("<a href=" + this.pageHrefPath + this.pageHrefEnd + " class=\"" + this.strClass + "\" style=\"" + this.strStyle + "\" " + strTarget + ">" + i.ToString() + "</a>");
                        }
                        else
                        {
                            str.Append("<a href=" + this.pageHrefPath + this.pageSplit + i.ToString() + this.pageHrefEnd + " class=\"" + this.strClass + "\" style=\"" + this.strStyle + "\" " + strTarget + ">" + i.ToString() + "</a>");
                        }
                    }
                }

                if (this.currentPage < pageTotal)
                {
                    //下一页
                    if (this.isPreviousAndNext)
                    {
                        string nextPage;
                        if (this.currentPage < pageTotal)
                        {
                            nextPage = Convert.ToString(this.currentPage + 1);
                        }
                        else
                        {
                            nextPage = pageTotal.ToString();
                        }
                        str.Append(this.strSpace);
                        str.Append(" <a href=" + this.pageHrefPath + this.pageSplit + nextPage + this.pageHrefEnd + " class=\"" + this.strClass + "\" style=\"" + this.strStyle + "\" " + strTarget + ">&gt;&gt;</a>");
                    }

                    //尾页
                    if (this.isFirstAndLast)
                    {
                        str.Append(this.strSpace);
                        str.Append("<a href=" + this.pageHrefPath + this.pageSplit + pageTotal.ToString() + this.pageHrefEnd + " class=\"" + this.strClass + "\" style=\"" + this.strStyle + "\" " + strTarget + ">尾页</a>");
                    }
                }

                //输入框
                if (this.isInput && pageTotal > this.showInputNum)
                {
                    str.Append(this.strSpace + this.strSpace);

                    str.Append("<input onKeyDown=\"");
                    str.Append("var evt=event;evt = (evt) ? evt : ((window.event) ? window.event : ''); keyCode = evt.keyCode ? evt.keyCode : (evt.which ? evt.which : evt.charCode);if(keyCode==13)if(parseInt(this.value)==this.value){window.location='" + this.pageHrefPath.Replace("'", @"\'") + this.pageSplit.Replace("'", @"\'") + "'+parseInt(this.value)+'" + this.pageHrefEnd.Replace("'", @"\'") + "';return false;}else{alert('请输入正确的页数!');return false;}");
                    str.Append("\" type=\"text\" class=\" " + this.strInputClass + " \" style=\"" + this.strInputStyle + "\" value=\"" + currentPage + "\" >");

                    if (this.isButton)
                    {
                        str.Append(this.strSpace + this.strSpace);

                        str.Append("<button onclick=\"");
                        str.Append("if(parseInt(this.previousSibling.value)==this.previousSibling.value){window.location='" + this.pageHrefPath.Replace("'", @"\'") + this.pageSplit.Replace("'", @"\'") + "'+parseInt(this.previousSibling.value)+'" + this.pageHrefEnd.Replace("'", @"\'") + "';return false;}else{alert('请输入正确的页数!');return false;}\"");
                        str.Append(" class=\"" + this.strButtonClass + "\" style=\"" + this.strButtonStyle + "\">转到</button>");
                    }
                }

            }
            #endregion

            #region 非转向分页
            else
            {
                if (this.currentPage > 1)
                {
                    //首页
                    if (this.isFirstAndLast)
                    {
                        //JoinChar(this._pagepath + this._pageparms) + this._pagename + "=" + Convert.ToString(this._curpageindex + 1)
                        str.Append("<a href=\"" + Tools.JoinChar(this.pagePath + this.pageParms) + this.pageSplit + "=1 \" class=\"" + this.strClass + "\" style=\"" + this.strStyle + "\" " + strTarget + ">首页</a>");
                    }
                    //上一页 
                    if (this.isPreviousAndNext)
                    {
                        str.Append(this.strSpace);
                        if (CurrentPage > 2)
                        {
                            string previousPage = Convert.ToString(this.currentPage - 1);
                            str.Append("<a href=\"" + Tools.JoinChar(this.pagePath + this.pageParms) + this.pageSplit + "=" + previousPage + "\" class=\"" + this.strClass + "\" style=\"" + this.strStyle + "\" " + strTarget + ">&lt;</a>");
                        }
                        else
                        {
                            str.Append("<a href=\"" + Tools.JoinChar(this.pagePath + this.pageParms) + this.pageSplit + "=1 \"  class=\"" + this.strClass + "\" style=\"" + this.strStyle + "\" " + strTarget + ">&lt;</a>");
                        }
                    }
                }
                //分页数字列表
                if (this.isShowNumericButton == true)
                {
                    for (int i = listPageStartNum; i <= listPageEndNum; i++)
                    {
                        str.Append(this.strSpace);
                        if (i == this.currentPage)
                        {
                            str.Append("<span class=\"" + this.strCurrentPageClass + "\" style=\"" + this.strCurrentPageStyle + "\">" + i.ToString() + "</span>");
                        }
                        else
                        {
                            str.Append("<a href=\"" + Tools.JoinChar(this.pagePath + this.pageParms) + this.pageSplit + "=" + i.ToString() + "\" class=\"" + this.strClass + "\" style=\"" + this.strStyle + "\" " + strTarget + ">" + i.ToString() + "</a>");
                        }
                    }
                }

                if (this.currentPage < pageTotal)
                {
                    //下一页
                    if (this.isPreviousAndNext)
                    {
                        string nextPage;
                        if (this.currentPage < pageTotal)
                        {
                            nextPage = Convert.ToString(this.currentPage + 1);
                        }
                        else
                        {
                            nextPage = pageTotal.ToString();
                        }
                        str.Append(this.strSpace);
                        str.Append("<a href=\"" + Tools.JoinChar(this.pagePath + this.pageParms) + this.pageSplit + "=" + nextPage + "\" class=\"" + this.strClass + "\" style=\"" + this.strStyle + "\" " + strTarget + ">&gt;</a>");
                    }

                    //尾页
                    if (this.isFirstAndLast)
                    {
                        str.Append(this.strSpace);
                        str.Append("<a href=\"" + Tools.JoinChar(this.pagePath + this.pageParms) + this.pageSplit + "=" + pageTotal.ToString() + "\" class=\"" + this.strClass + "\" style=\"" + this.strStyle + "\" " + strTarget + ">尾页</a>");
                    }
                }

                //输入框
                if (this.isInput && pageTotal > this.showInputNum)
                {
                    str.Append(this.strSpace + this.strSpace);

                    str.Append("<input onKeyDown=\"");
                    str.Append("var evt=event;evt = (evt) ? evt : ((window.event) ? window.event : ''); keyCode = evt.keyCode ? evt.keyCode : (evt.which ? evt.which : evt.charCode);if(keyCode==13)if(parseInt(this.value)==this.value){window.location='" + Tools.JoinChar(this.pagePath + this.pageParms) + this.pageSplit + "='+parseInt(this.value)+'';return false;}else{alert('请输入正确的页数!');return false;}");
                    str.Append("\" type=\"text\" class=\" " + this.strInputClass + " \" style=\"" + this.strInputStyle + "\" value=\"" + currentPage + "\" >");

                    if (this.isButton)
                    {
                        str.Append(this.strSpace + this.strSpace);

                        str.Append("<button onclick=\"");
                        str.Append("if(parseInt(this.previousSibling.value)==this.previousSibling.value){window.location='" + Tools.JoinChar(this.pagePath + this.pageParms) + this.pageSplit + "='+parseInt(this.previousSibling.value)+'';return false;}else{alert('请输入正确的页数!');return false;}\"");
                        str.Append(" class=\"" + this.strButtonClass + "\" style=\"" + this.strButtonStyle + "\">转到</button>");
                    }
                }

            }
            #endregion


            retValue = str.ToString();
            return retValue;
        }

        #region 属性
        /// <summary>
        /// 记录总数
        /// </summary>
        public int RecordCount
        {
            get { return this.recordCount; }
            set { this.recordCount = value; }
        }
        /// <summary>
        /// 当前页
        /// </summary>
        public int CurrentPage
        {
            get { return this.currentPage; }
            set { this.currentPage = value; }
        }
        /// <summary>
        /// 显示分页当中的数字按钮数
        /// </summary>
        public int ListNum
        {
            get { return this.listNum; }
            set { this.listNum = value; }
        }
        /// <summary>
        /// 每页显示记录数
        /// </summary>
        public int PageSize
        {
            get { return this.pageSize; }
            set { this.pageSize = value; }
        }
        /// <summary>
        /// 显示跳转输入框总页数大于多少
        /// </summary>
        public int ShowInputNum
        {
            get { return this.showInputNum; }
            set { this.showInputNum = value; }
        }


        /// <summary>
        /// 是否自动显示
        /// </summary>
        public bool IsAutoDisplay
        {
            get { return this.isAutoDisplay; }
            set { this.isAutoDisplay = value; }
        }
        /// <summary>
        /// 是否显示跳转按钮
        /// </summary>
        public bool IsButton
        {
            get { return this.isButton; }
            set { this.isButton = value; }
        }
        /// <summary>
        /// 是否显示跳转输入框
        /// </summary>
        public bool IsInput
        {
            get { return this.isInput; }
            set { this.isInput = value; }
        }
        /// <summary>
        /// 是否显示上一页跟下一页
        /// </summary>
        public bool IsPreviousAndNext
        {
            get { return this.isPreviousAndNext; }
            set { this.isPreviousAndNext = value; }
        }
        /// <summary>
        /// 是否开新窗口
        /// </summary>
        public bool IsTarget
        {
            get { return this.isTarget; }
            set { this.isTarget = value; }
        }
        /// <summary>
        /// 是否显示首页跟尾页
        /// </summary>
        public bool IsFirstAndLast
        {
            get { return this.isFirstAndLast; }
            set { this.isFirstAndLast = value; }
        }
        /// <summary>
        /// 下拉框跳转
        /// </summary>
        public bool IsDropdown
        {
            get { return this.isDropdown; }
            set { this.isDropdown = value; }
        }
        /// <summary>
        /// 是否做过地址转向
        /// </summary>
        public bool IsRewrite
        {
            get { return this.isRewrite; }
            set { this.isRewrite = value; }
        }
        /// <summary>
        /// 是否显示数字分页按钮
        /// </summary>
        public bool IsShowNumericButton
        {
            get { return this.isShowNumericButton; }
            set { this.isShowNumericButton = value; }
        }
        /// <summary>
        /// 分页路径 ex:/xiaoyuan/
        /// </summary>
        public string PageHrefPath
        {
            get { return this.pageHrefPath; }
            set { this.pageHrefPath = value; }
        }
        /// <summary>
        /// 路径结束 ex:/
        /// </summary>
        public string PageHrefEnd
        {
            get { return this.pageHrefEnd; }
            set { pageHrefEnd = value; }
        }
        /// <summary>
        /// 分页分割符ex:P1 P2
        /// </summary>
        public string PageSplit
        {
            get { return this.pageSplit; }
            set { this.pageSplit = value; }
        }
        /// <summary>
        /// 按钮CSS
        /// </summary>
        public string StrButtonClass
        {
            get { return this.strButtonClass; }
            set { this.strButtonClass = value; }
        }

        /// <summary>
        /// 按钮Style
        /// </summary>
        public string StrButtonStyle
        {
            get { return this.strButtonStyle; }
            set { this.strButtonStyle = value; }
        }

        /// <summary>
        /// 链接Class
        /// </summary>
        public string StrClass
        {
            get { return this.strClass; }
            set { this.strClass = value; }
        }
        /// <summary>
        /// 链接Style
        /// </summary>
        public string StrStyle
        {
            get { return this.strStyle; }
            set { this.strStyle = value; }
        }

        /// <summary>
        /// 当前页Class
        /// </summary>
        public string StrCurrentPageClass
        {
            get { return this.strCurrentPageClass; }
            set { this.strCurrentPageClass = value; }
        }
        /// <summary>
        /// 当前页Style
        /// </summary>
        public string StrCurrentPageStyle
        {
            get { return this.strCurrentPageStyle; }
            set { this.strCurrentPageStyle = value; }
        }
        /// <summary>
        /// 跳转输入框Class
        /// </summary>
        public string StrInputClass
        {
            get { return this.strInputClass; }
            set { this.strInputClass = value; }
        }
        /// <summary>
        /// 跳转输入框Style
        /// </summary>
        public string StrInputStyle
        {
            get { return this.strInputStyle; }
            set { this.strInputStyle = value; }
        }
        /// <summary>
        /// 参数集合
        /// </summary>
        public string PageParms
        {
            get { return this.pageParms; }
            set { this.pageParms = value; }
        }
        /// <summary>
        /// 当前请求的虚拟路径
        /// </summary>
        public string PagePath
        {
            get { return this.pagePath; }
            set { this.pagePath = value; }
        }
        #endregion

    }
}
