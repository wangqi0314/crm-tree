using System;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;
namespace ShInfoTech.Common
{
    /// <summary>
    /// 分页控件
    /// </summary>
    public class MultiPage
    {
        private uint _curpageindex;
        private DataList _datalist;
        private GridView _gridview;
        private string _hisqlstr;
        private bool _isexistdata;
        private bool _isuserule;
        private string _lastorder;
        private Literal _litcurpage;
        private Literal _litpageslnk;
        private Literal _littotalpage;
        private Literal _littotalrec;
        private HyperLink _lnkfirst;
        private HyperLink _lnklast;
        private HyperLink _lnknext;
        private HyperLink _lnkprev;
        private string _pageclnkstyle;
        private uint _pagelnknum;
        private string _pagelnkrule;
        private int _pagelnkstyle;
        private string _pagename;
        private string _pageotherlnkstyle;
        private string _pageparms;
        private string _pageparmsrule;
        private string _pagepath;
        private uint _pageshownum;
        private Panel _palctrl;
        private Repeater _repeater;
        private bool _showpageslnk;
        private uint _totalrecord;

        public MultiPage()
            : this(null, null, null, null, null, null, null, null, null, null, null, null, "", "", false, 0, 0)
        {
        }

        public MultiPage(GridView gridview, DataList datalist, Repeater repeater, HyperLink lnknext, HyperLink lnkprev, HyperLink lnkfirst, HyperLink lnklast, Panel palctrl, Literal litcurpage, Literal littotalpage, Literal litpageslnk, Literal littotalrec, string pagename, string pageparms, bool showpageslnk, uint pagelnknum, uint pageshownum)
        {
            this._gridview = gridview;
            this._datalist = datalist;
            this._repeater = repeater;
            this._lnknext = lnknext;
            this._lnkprev = lnkprev;
            this._lnkfirst = lnkfirst;
            this._lnklast = lnklast;
            this._palctrl = palctrl;
            this._litcurpage = litcurpage;
            this._littotalpage = littotalpage;
            this._litpageslnk = litpageslnk;
            this._littotalrec = littotalrec;
            this._pagename = pagename;
            this._pageparms = pageparms;
            this._showpageslnk = showpageslnk;
            this._pagelnknum = pagelnknum;
            this._pageshownum = pageshownum;
            this._pagepath = HttpContext.Current.Request.CurrentExecutionFilePath;
            this._lastorder = "";
            this._pageclnkstyle = "";
            this._pageotherlnkstyle = "";
            this._pagelnkstyle = 0;
            this._isuserule = false;
            this._pagelnkrule = "";
            this._pageparmsrule = pagename;
        }

        /// <summary>
        /// 配置页链接
        /// </summary>
        /// <param name="pagecount"></param>
        private void AllPageLnkConfig(uint pagecount)
        {
            if (this._showpageslnk)
            {
                uint num = 1;
                uint num2 = 1;
                StringBuilder builder = new StringBuilder();
                if ((Convert.ToInt32(this._curpageindex) - Convert.ToInt32(this._pagelnknum)) > 1)
                {
                    num = this._curpageindex - this._pagelnknum;
                }
                else
                {
                    num2 = pagecount;
                }
                if ((this._curpageindex + this._pagelnknum) <= pagecount)
                {
                    num2 = this._curpageindex + this._pagelnknum;
                }
                else
                {
                    num2 = pagecount;
                }
                for (uint i = num; i <= num2; i++)
                {
                    builder.Append(this.HyperLinkPage(i.ToString())).Append("&nbsp;");
                }
                this._litpageslnk.Text = "&nbsp;" + builder.ToString();
                this._litpageslnk.DataBind();
            }
        }

        /// <summary>
        ///  返回当前数据集(用于sql-server数据库)
        /// </summary>
        /// <param name="connstr"></param>
        /// <param name="sqlstr"></param>
        /// <returns></returns>
        public DataSet DS_Page(string connstr, string sqlstr)
        {
            return this.DS_Page(connstr, sqlstr, 1);
        }

        /// <summary>
        /// 返回一个DataSet            datatype:1代表sql,2代表access
        /// </summary>
        /// <param name="connstr">数据库连接</param>
        /// <param name="sqlstr">Sql语句</param>
        /// <param name="datatype">数据库类型</param>
        /// <returns></returns>
        public DataSet DS_Page(string connstr, string sqlstr, int datatype)
        {
            DataSet set3;
            if (datatype == 2)
            {
                using (OleDbConnection connection = new OleDbConnection(connstr))
                {
                    connection.Open();
                    OleDbCommand selectCommand = new OleDbCommand(sqlstr, connection);
                    using (OleDbDataAdapter adapter = new OleDbDataAdapter(selectCommand))
                    {
                        DataSet dataSet = new DataSet();
                        adapter.Fill(dataSet);
                        connection.Close();
                        return dataSet;
                    }
                }
            }
            using (SqlConnection connection2 = new SqlConnection(connstr))
            {
                connection2.Open();
                SqlCommand command2 = new SqlCommand(sqlstr, connection2);
                using (SqlDataAdapter adapter2 = new SqlDataAdapter(command2))
                {
                    DataSet set2 = new DataSet();
                    adapter2.Fill(set2);
                    connection2.Close();
                    set3 = set2;
                }
            }
            return set3;
        }

        /// <summary>
        /// 获取当前页码
        /// </summary>
        /// <param name="cpagecount"></param>
        private void GetCurPageIndex(uint cpagecount)
        {
            try
            {
                this._curpageindex = Convert.ToUInt32(HttpContext.Current.Request.QueryString[this._pagename]);
            }
            catch
            {
                this._curpageindex = 1;
            }
            if (this._curpageindex < 1)
            {
                this._curpageindex = 1;
            }
            else if (this._curpageindex > cpagecount)
            {
                this._curpageindex = cpagecount;
            }
        }

        /// <summary>
        /// 获取总分页数 调用前必须将this._totalrecord进行赋值
        /// </summary>
        /// <returns></returns>
        private uint GetHiDataPageCount()
        {
            if (this._totalrecord > 0)
            {
                this._isexistdata = true;
            }
            if ((this._totalrecord % this._pageshownum) == 0)
            {
                return (this._totalrecord / this._pageshownum);
            }
            return ((this._totalrecord / this._pageshownum) + 1);
        }


        /// <summary>
        /// 查询分页语句
        /// </summary>
        /// <param name="_tbName"></param>
        /// <param name="_fldName"></param>
        /// <param name="_PageSize"></param>
        /// <param name="_Page"></param>
        /// <param name="_PageCount"></param>
        /// <param name="_Counts"></param>
        /// <param name="_fldSort"></param>
        /// <param name="_Sort"></param>
        /// <param name="_strCondition"></param>
        /// <param name="_ID"></param>
        /// <param name="_Dist"></param>
        /// <returns></returns>
        private string GetPageListSql(string _tbName, string _fldName, uint _PageSize, uint _Page, uint _PageCount, uint _Counts, string _fldSort, int _Sort, string _strCondition, string _ID, int _Dist)
        {
            string str2 = "";
            string str3 = "";
            string str4 = "";
            if (_Dist == 0)
            {
                str2 = "SELECT ";
            }
            else
            {
                str2 = "SELECT DISTINCT ";
            }
            if (_Sort == 0)
            {
                str4 = " ASC";
                str3 = " DESC";
            }
            else
            {
                str4 = " DESC";
                str3 = " ASC";
            }
            uint num = 1;
            if (_Counts != 0)
            {
                num = _Counts;
            }
            if (_Page > _PageCount)
            {
                _Page = _PageCount;
            }
            if (_Page <= 0)
            {
                _Page = 1;
            }
            uint num2 = num / _PageSize;
            uint num3 = num % _PageSize;
            if (num3 > 0)
            {
                num2++;
            }
            else
            {
                num3 = _PageSize;
            }
            if ((num2 < 2) || (_Page <= ((num2 / 2) + (num2 % 2))))
            {
                if (_Page == 1)
                {
                    return string.Concat(new object[] { str2, " TOP ", _PageSize, " ", _fldName, " FROM ", _tbName, "  WHERE 1=1 ", _strCondition, "    ORDER BY ", _fldSort, " ", str4 });
                }
                if (_Sort == 0)
                {
                    return string.Concat(new object[] { 
                        str2, " TOP ", _PageSize, " ", _fldName, " FROM ", _tbName, " WHERE ", _ID, " >(SELECT MAX(", _ID, ") FROM (", str2, " TOP ", _PageSize * (_Page - 1), " ", 
                        _ID, " FROM ", _tbName, "  WHERE 1=1  ", _strCondition, "   ORDER BY ", _fldSort, " ", str4, ") AS TBMaxID)   ", _strCondition, "   ORDER BY ", _fldSort, " ", str4
                     });
                }
                return string.Concat(new object[] { 
                    str2, " TOP ", _PageSize, " ", _fldName, " FROM ", _tbName, " WHERE ", _ID, " <(SELECT MIN(", _ID, ") FROM (", str2, " TOP ", _PageSize * (_Page - 1), " ", 
                    _ID, " FROM ", _tbName, "   WHERE 1=1  ", _strCondition, "    ORDER BY ", _fldSort, " ", str4, ") AS TBMinID)    ", _strCondition, "       ORDER BY ", _fldSort, " ", str4
                 });
            }
            _Page = (num2 - _Page) + 1;
            if (_Page <= 1)
            {
                return string.Concat(new object[] { 
                    str2, " * FROM (", str2, " TOP ", num3, " ", _fldName, " FROM ", _tbName, "   WHERE 1=1   ", _strCondition, "    ORDER BY ", _fldSort, " ", str3, ") AS TempTB     ORDER BY ", 
                    _fldSort, " ", str4
                 });
            }
            if (_Sort == 0)
            {
                return string.Concat(new object[] { 
                    str2, " * FROM (", str2, " TOP ", _PageSize, " ", _fldName, " FROM ", _tbName, " WHERE ", _ID, " <(SELECT MIN(", _ID, ") FROM(", str2, " TOP ", 
                    (_PageSize * (_Page - 2)) + num3, " ", _ID, " FROM ", _tbName, "  WHERE 1=1  ", _strCondition, "      ORDER BY ", _fldSort, " ", str3, ") AS TBMinID)   ", _strCondition, "    ORDER BY ", _fldSort, " ", 
                    str3, ") AS TempTB      ORDER BY ", _fldSort, " ", str4
                 });
            }
            return string.Concat(new object[] { 
                str2, " * FROM (", str2, " TOP ", _PageSize, " ", _fldName, " FROM ", _tbName, " WHERE ", _ID, " >(SELECT MAX(", _ID, ") FROM(", str2, " TOP ", 
                (_PageSize * (_Page - 2)) + num3, " ", _ID, " FROM ", _tbName, "   WHERE 1=1  ", _strCondition, "    ORDER BY ", _fldSort, " ", str3, ") AS TBMaxID)   ", _strCondition, "     ORDER BY ", _fldSort, " ", 
                str3, ") AS TempTB         ORDER BY ", _fldSort, " ", str4
             });
        }

        /// <summary>
        /// 公共获取海量总记录数
        /// </summary>
        /// <param name="ConnStr"></param>
        /// <param name="Sql"></param>
        /// <returns></returns>
        private uint HiDataCount(string ConnStr, string Sql)
        {
            return this.HiDataCount(ConnStr, Sql, 1);
        }

        /// <summary>
        /// 公共获取海量总记录数
        /// </summary>
        /// <param name="ConnStr"></param>
        /// <param name="Sql"></param>
        /// <param name="datatype"></param>
        /// <returns></returns>
        private uint HiDataCount(string ConnStr, string Sql, int datatype)
        {
            object obj2;
            uint num = 0;
            string cmdText = "select count(*) as [totalnum] from (" + Sql + ") as [table1]  ";
            if (datatype == 2)
            {
                using (OleDbConnection connection = new OleDbConnection(ConnStr))
                {
                    connection.Open();
                    OleDbCommand command = new OleDbCommand(cmdText, connection);
                    command.CommandType = CommandType.Text;
                    obj2 = command.ExecuteScalar();
                    if (!Convert.IsDBNull(obj2))
                    {
                        num = Convert.ToUInt32(obj2);
                    }
                    connection.Close();
                    return num;
                }
            }
            using (SqlConnection connection2 = new SqlConnection(ConnStr))
            {
                connection2.Open();
                SqlCommand command2 = new SqlCommand(cmdText, connection2);
                command2.CommandType = CommandType.Text;
                obj2 = command2.ExecuteScalar();
                if (!Convert.IsDBNull(obj2))
                {
                    num = Convert.ToUInt32(obj2);
                }
                connection2.Close();
                return num;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ConnStr"></param>
        /// <param name="Sql"></param>
        /// <param name="pagenum"></param>
        /// <param name="curpage"></param>
        /// <param name="ordersql"></param>
        /// <returns></returns>
        public DataSet HiDataDs(string ConnStr, string Sql, uint pagenum, uint curpage, string ordersql)
        {
            DataSet set2;
            using (SqlConnection connection = new SqlConnection(ConnStr))
            {
                connection.Open();
                StringBuilder builder = new StringBuilder();
                builder.Append(" Select Top " + pagenum + " * From ").Append(" ( Select  *,Row_Number() Over (" + ordersql + ") As RowNo From ").Append(" ( " + Sql + " ) as Table1 ").Append(" ) AS A  WHERE RowNo > " + ((curpage - 1) * pagenum) + " ").Append(this._lastorder);
                this._hisqlstr = builder.ToString();
                SqlCommand selectCommand = new SqlCommand(builder.ToString(), connection);
                using (SqlDataAdapter adapter = new SqlDataAdapter(selectCommand))
                {
                    DataSet dataSet = new DataSet();
                    adapter.Fill(dataSet);
                    connection.Close();
                    set2 = dataSet;
                }
            }
            return set2;
        }

        /// <summary>
        /// 分页链接
        /// </summary>
        /// <param name="pageindex"></param>
        /// <returns></returns>
        private string HyperLinkPage(string pageindex)
        {
            string str = "";
            StringBuilder builder = new StringBuilder();
            if (this._curpageindex == Convert.ToUInt32(pageindex))
            {
                if ("" == this._pageclnkstyle)
                {
                    str = " style='color:#ff6600;' ";
                }
                else
                {
                    str = " " + this._pageclnkstyle + " ";
                }
            }
            else
            {
                str = " " + this._pageotherlnkstyle + " ";
            }
            builder.Append("<a href=\"");
            if (!this._isuserule)
            {
                builder.Append(this.JoinChar(this._pagepath + this._pageparms) + this._pagename + "=" + pageindex);
            }
            else
            {
                builder.Append(this._pagelnkrule.Replace(this._pageparmsrule, Convert.ToString(pageindex)));
            }
            builder.Append("\"  " + str + "  >");
            if (this._pagelnkstyle == 0)
            {
                builder.Append(pageindex);
            }
            else
            {
                builder.Append(" [" + pageindex + "] ");
            }
            builder.Append("</a>");
            return builder.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strUrl"></param>
        /// <returns></returns>
        private string JoinChar(string strUrl)
        {
            if (strUrl.Equals("") || strUrl.Equals((string)null))
            {
                return "";
            }
            if (strUrl.IndexOf(Convert.ToChar("?")) < strUrl.Length)
            {
                if (strUrl.IndexOf(Convert.ToChar("?")) <= 1)
                {
                    return (strUrl + Convert.ToChar("?"));
                }
                if (strUrl.LastIndexOf(Convert.ToChar("&")) < strUrl.Length)
                {
                    return (strUrl + Convert.ToChar("&"));
                }
            }
            return strUrl;
        }
        /// <summary>
        /// 分页控件配置
        /// </summary>
        /// <param name="datasource">数据源</param>
        /// <param name="cpagecount">当前页总数</param>
        private void PageControlConfig(object datasource, uint cpagecount)
        {
            if (this._gridview != null)
            {
                this._gridview.DataSource = datasource;
                this._gridview.DataBind();
            }
            if (this._datalist != null)
            {
                this._datalist.DataSource = datasource;
                this._datalist.DataBind();
            }
            if (this._repeater != null)
            {
                this._repeater.DataSource = datasource;
                this._repeater.DataBind();
            }
            if (cpagecount > 1)
            {
                this._palctrl.Visible = true;
            }
            else
            {
                this._palctrl.Visible = false;
                return;
            }
            this._littotalrec.Text = this._totalrecord.ToString();
            this._littotalrec.DataBind();
            this._litcurpage.Text = this._curpageindex.ToString();
            this._litcurpage.DataBind();
            this._littotalpage.Text = cpagecount.ToString();
            this._littotalpage.DataBind();
            if (this._curpageindex != cpagecount)
            {
                if (!this._isuserule)
                {
                    this._lnknext.NavigateUrl = this.JoinChar(this._pagepath + this._pageparms) + this._pagename + "=" + Convert.ToString((uint)(this._curpageindex + 1));
                }
                else
                {
                    this._lnknext.NavigateUrl = this._pagelnkrule.Replace(this._pageparmsrule, Convert.ToString((uint)(this._curpageindex + 1)));
                }
                this._lnknext.DataBind();
            }
            if (this._curpageindex != 1)
            {
                if (!this._isuserule)
                {
                    this._lnkprev.NavigateUrl = this.JoinChar(this._pagepath + this._pageparms) + this._pagename + "=" + Convert.ToString((uint)(this._curpageindex - 1));
                }
                else
                {
                    this._lnkprev.NavigateUrl = this._pagelnkrule.Replace(this._pageparmsrule, Convert.ToString((uint)(this._curpageindex - 1)));
                }
                this._lnkprev.DataBind();
            }
            if (this._curpageindex != 1)
            {
                if (!this._isuserule)
                {
                    this._lnkfirst.NavigateUrl = this.JoinChar(this._pagepath + this._pageparms) + this._pagename + "=" + Convert.ToString(1);
                }
                else
                {
                    this._lnkfirst.NavigateUrl = this._pagelnkrule.Replace(this._pageparmsrule, "1");
                }
                this._lnkfirst.DataBind();
            }
            if (this._curpageindex != cpagecount)
            {
                if (!this._isuserule)
                {
                    this._lnklast.NavigateUrl = this.JoinChar(this._pagepath + this._pageparms) + this._pagename + "=" + Convert.ToString(cpagecount);
                }
                else
                {
                    this._lnklast.NavigateUrl = this._pagelnkrule.Replace(this._pageparmsrule, Convert.ToString(cpagecount));
                }
                this._lnklast.DataBind();
            }
        }

        /// <summary>
        ///  SqlServer及Sql2005数据分页
        /// </summary>
        /// <param name="ds"></param>
        public void ShowMultiPage(DataSet ds)
        {
            this.ShowMultiPageShareLow(ds);
        }

        /// <summary>
        /// SqlServer及Sql2005数据分页
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="totalrecord"></param>
        public void ShowMultiPage(DataSet ds, uint totalrecord)
        {
            this.ShowMultiPageShareHigh(ds, totalrecord);
        }

        /// <summary>
        ///  SqlServer数据分页
        /// </summary>
        /// <param name="connstr"></param>
        /// <param name="sqlstr"></param>
        public void ShowMultiPage(string connstr, string sqlstr)
        {
            this.ShowMultiPageShareLow(this.DS_Page(connstr, sqlstr, 1));
        }

        /// <summary>
        ///    Sql-Server2005 数据分页
        /// </summary>
        /// <param name="Connstr"></param>
        /// <param name="Sql"></param>
        /// <param name="ordersql"></param>
        public void ShowMultiPage(string Connstr, string Sql, string ordersql)
        {
            this._totalrecord = this.HiDataCount(Connstr, Sql);
            this.GetCurPageIndex(this.GetHiDataPageCount());
            DataSet ds = this.HiDataDs(Connstr, Sql, this._pageshownum, this._curpageindex, ordersql);
            this.ShowMultiPage(ds, this._totalrecord);
        }

        /// <summary>
        ///      公共海量数据分页            如使用规则,必须在外部设置CurPageIndex属性值
        /// </summary>
        /// <param name="ds">已分页数据集</param>
        /// <param name="totalrecord">总数</param>
        public void ShowMultiPageShareHigh(DataSet ds, uint totalrecord)
        {
            this._totalrecord = totalrecord;
            uint hiDataPageCount = this.GetHiDataPageCount();
            this.GetCurPageIndex(hiDataPageCount);
            this.PageControlConfig(ds, hiDataPageCount);
            this.AllPageLnkConfig(hiDataPageCount);
        }

        /// <summary>
        ///   公共海量数据分页
        /// </summary>
        /// <param name="connstr"></param>
        /// <param name="datatype"></param>
        /// <param name="countsql"></param>
        /// <param name="tbname"></param>
        /// <param name="fldname"></param>
        /// <param name="fldsort"></param>
        /// <param name="sort"></param>
        /// <param name="strcondition"></param>
        /// <param name="id"></param>
        /// <param name="dist"></param>
        public void ShowMultiPageShareHigh(string connstr, int datatype, string countsql, string tbname, string fldname, string fldsort, int sort, string strcondition, string id, int dist)
        {
            this._totalrecord = this.HiDataCount(connstr, countsql, datatype);
            uint hiDataPageCount = this.GetHiDataPageCount();
            this.GetCurPageIndex(hiDataPageCount);
            string sqlstr = this.GetPageListSql(tbname, fldname, this._pageshownum, this._curpageindex, hiDataPageCount, this._totalrecord, fldsort, sort, strcondition, id, dist);
            this._hisqlstr = sqlstr;
            DataSet datasource = this.DS_Page(connstr, sqlstr, datatype);
            this.PageControlConfig(datasource, hiDataPageCount);
            this.AllPageLnkConfig(hiDataPageCount);
        }

        /// <summary>
        /// 常规数据分页(适合小量数据)
        /// </summary>
        /// <param name="ds"></param>
        public void ShowMultiPageShareLow(DataSet ds)
        {
            uint num = this._pageshownum;
            PagedDataSource datasource = new PagedDataSource();
            datasource.AllowPaging = true;
            datasource.PageSize = Convert.ToInt32(num);
            datasource.DataSource = ds.Tables[0].DefaultView;
            this._totalrecord = Convert.ToUInt32(ds.Tables[0].DefaultView.Count);
            if (this._totalrecord > 0)
            {
                this._isexistdata = true;
            }
            this.GetCurPageIndex(Convert.ToUInt32(datasource.PageCount));
            datasource.CurrentPageIndex = Convert.ToInt32((uint)(this._curpageindex - 1));
            this.PageControlConfig(datasource, Convert.ToUInt32(datasource.PageCount));
            this.AllPageLnkConfig(Convert.ToUInt32(datasource.PageCount));
        }
        /// <summary>
        /// 获取或设置当前页页码(在有规则的情况下设置有效)
        /// </summary>
        public uint CurPageIndex
        {
            get
            {
                return this._curpageindex;
            }
            set
            {
                this._curpageindex = value;
            }
        }
        /// <summary>
        ///   设置DataList控件
        /// </summary>
        public DataList DataListCtrl
        {
            set
            {
                this._datalist = value;
            }
        }
        /// <summary>
        ///   设置GridView控件
        /// </summary>
        public GridView GridViewCtrl
        {
            set
            {
                this._gridview = value;
            }
        }

        /// <summary>
        ///  获取海量SQL语句,用于测试
        /// </summary>
        public string HiSqlStr
        {
            get
            {
                return this._hisqlstr;
            }
        }
        /// <summary>
        /// 获取是否存在数据集
        /// </summary>
        public bool IsExistData
        {
            get
            {
                return this._isexistdata;
            }
        }

        /// <summary>
        /// 设置分页是否使用规则
        /// </summary>
        public bool IsUseRule
        {
            set
            {
                this._isuserule = value;
            }
        }
        /// <summary>
        /// 设置最终排序字符串,仅针对Sql2005海量数据
        /// </summary>
        public string LastOrder
        {
            set
            {
                this._lastorder = value;
            }
        }
        /// <summary>
        ///  设置显示当前页数的lit控件
        /// </summary>
        public Literal LitCurPageCtrl
        {
            set
            {
                this._litcurpage = value;
            }
        }
        /// <summary>
        /// 设置显示分页子链接的lit控件
        /// </summary>
        public Literal LitPagesLnkCtrl
        {
            set
            {
                this._litpageslnk = value;
            }
        }
        /// <summary>
        /// 设置显示总页数的lit控件
        /// </summary>
        public Literal LitTotalPageCtrl
        {
            set
            {
                this._littotalpage = value;
            }
        }
        /// <summary>
        /// 设置显示总记录数lit控件
        /// </summary>
        public Literal LitTotalRecCtrl
        {
            set
            {
                this._littotalrec = value;
            }
        }
        /// <summary>
        ///   设置显示首页的lnk控件
        /// </summary>
        public HyperLink LnkFirstCtrl
        {
            set
            {
                this._lnkfirst = value;
            }
        }
        /// <summary>
        /// 设置显示最后一页的lnk控件
        /// </summary>
        public HyperLink LnkLastCtrl
        {
            set
            {
                this._lnklast = value;
            }
        }
        /// <summary>
        /// 设置显示下一页的lnk控件
        /// </summary>
        public HyperLink LnkNextCtrl
        {
            set
            {
                this._lnknext = value;
            }
        }
        /// <summary>
        /// 设计显示上一页的lnk控件
        /// </summary>
        public HyperLink LnkPrevCtrl
        {
            set
            {
                this._lnkprev = value;
            }
        }
        /// <summary>
        /// 获取记录序号起始位置
        /// </summary>
        public uint OrderStartNum
        {
            get
            {
                uint num = 0;
                if (this._curpageindex > 0)
                {
                    num = this._curpageindex - 1;
                }
                return (num * this._pageshownum);
            }
        }
        /// <summary>
        /// 设置当前页链接着色
        /// </summary>
        public string PageClnkStyle
        {
            set
            {
                this._pageclnkstyle = value;
            }
        }
        /// <summary>
        /// 设置分页子链接显示数量
        /// </summary>
        public uint PageLnkNum
        {
            set
            {
                this._pagelnknum = value;
            }
        }
        /// <summary>
        /// 获取或设置页链接规则字符串,如/Class-1-P$pagegno$.htm
        /// </summary>
        public string PageLnkRule
        {
            get
            {
                return this._pagelnkrule;
            }
            set
            {
                this._pagelnkrule = value;
            }
        }

        /// <summary>
        /// 设置分页子链接样式
        /// </summary>
        public int PageLnkStyle
        {
            set
            {
                this._pagelnkstyle = value;
            }
        }
        /// <summary>
        ///  设置外部获取的当前页参数(有规则时,此参数用于替换成当前页码;无规则时,作为地址栏需要获取的参数,如pageno)
        /// </summary>
        public string PageName
        {
            set
            {
                this._pagename = value;
            }
        }
        /// <summary>
        /// 设置分页子链接样式(除当前页外)
        /// </summary>
        public string PageOtherLnkStyle
        {
            set
            {
                this._pageotherlnkstyle = value;
            }
        }
        /// <summary>
        ///  设置分页传递参数
        /// </summary>
        public string PageParms
        {
            set
            {
                this._pageparms = value;
            }
        }
        /// <summary>
        ///  设置页码规则(在使用URL规则时,用于替换成页码)
        /// </summary>
        public string PageParmsRule
        {
            set
            {
                this._pageparms = value;
            }
        }
        /// <summary>
        ///    设置当前页路径
        /// </summary>
        public string PagePath
        {
            set
            {
                this._pagepath = value;
            }
        }
        /// <summary>
        /// 设置每页显示记录的条数
        /// </summary>
        public uint PageShowNum
        {
            set
            {
                this._pageshownum = value;
            }
        }
        /// <summary>
        /// 设置显示分页区的pannel控件
        /// </summary>
        public Panel PalCtrl
        {
            set
            {
                this._palctrl = value;
            }
        }
        /// <summary>
        /// 设置Repeater控件
        /// </summary>
        public Repeater RepeaterCtrl
        {
            set
            {
                this._repeater = value;
            }
        }
        /// <summary>
        /// 设置是否显示分页子链接
        /// </summary>
        public bool ShowPagesLnk
        {
            set
            {
                this._showpageslnk = value;
            }
        }
        /// <summary>
        /// 获取总记录数
        /// </summary>
        public uint TotalRecordCount
        {
            get
            {
                return this._totalrecord;
            }
        }
    }
}