using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Linq;
using Ede.Uof.EIP.SystemInfo;
using Ede.Uof.Utility.Data;
using Ede.Uof.Utility.Log;
using Ede.Uof.Utility.Page.Common;
using OfficeOpenXml;
using OfficeOpenXml.Drawing;
using OfficeOpenXml.Style;
using System.Net.Mail;
using System.Threading.Tasks;

public partial class CDS_WebPage_PUR_TK_TBPURGOODS : Ede.Uof.Utility.Page.BasePage
{
    string ACCOUNT = null;
    string NAME = null;
    String ROLES = null;


    protected void Page_Load(object sender, EventArgs e)
    {
        ACCOUNT = Current.Account;
        NAME = Current.User.Name;

        if (!IsPostBack)
        {           
           BindGrid();
        }
    }

    #region FUNCTION
    private void BindGrid()
    {
        // 1.取得連線字串
        // 請將 "YourConnectionStringName" 替換為 Web.config 中定義的連線名稱
        string connectionString = ConfigurationManager.ConnectionStrings["ERPconnectionstring"].ConnectionString;
        Ede.Uof.Utility.Data.DatabaseHelper m_db = new Ede.Uof.Utility.Data.DatabaseHelper(connectionString);

        StringBuilder QUERY1 = new StringBuilder();
        StringBuilder QUERY2 = new StringBuilder();
        StringBuilder QUERY3 = new StringBuilder();
       
        //MB002
        string COMPANYS = QUERY_TextBox1.Text.Trim();
        if (!string.IsNullOrEmpty(COMPANYS))
        {
            QUERY1.AppendFormat(@" AND COMPANYS LIKE '%{0}%' ", COMPANYS);
        }
        else
        {
            QUERY1.AppendFormat(@"");
        }
        //YEARS
        string ITEMS = QUERY_TextBox2.Text.Trim();
        if (!string.IsNullOrEmpty(ITEMS))
        {
            QUERY2.AppendFormat(@" AND ITEMS LIKE '%{0}%' ", ITEMS);
        }
        else
        {
            QUERY2.AppendFormat(@"");
        }

        StringBuilder cmdTxt = new StringBuilder();
        cmdTxt.AppendFormat(@"
                            SELECT
                            [ID]
                            ,[COMPANYS] AS '廠商'
                            ,[ITEMS] AS '品項'
                            ,[NUMS] AS '數量'
                            ,[PRICES] AS '單價'
                            ,[MONEYS] AS '總計'
                            ,[UPDATEDATES] AS '提供日期'
                            ,[COMMENTS] AS '備註'
                            ,[USEDSTATES] AS '月叫貨量'
                            FROM [TKPUR].[dbo].[TBPURGOODS]
                            WHERE 1=1
                            {0}
                            {1}
                            ORDER BY [COMPANYS],[ITEMS]
                            ", QUERY1.ToString(), QUERY2.ToString());


        //m_db.AddParameter("@QUERYMONEY", TextBox3.Text.Trim());

        DataTable dt = new DataTable();

        dt.Load(m_db.ExecuteReader(cmdTxt.ToString()));


        Grid1.DataSource = dt;
        Grid1.DataBind();

    }

    protected void grid_PageIndexChanging1(object sender, GridViewPageEventArgs e)
    {

    }
    protected void Grid1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    protected void Grid1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int rowIndex = -1;

        // 1. 檢查 CommandName 是否是您定義的更新命令
        if (e.CommandName == "UPDATE")
        {

        }
    }
    // 雖然不應該被觸發，但定義它以避免 HttpCException
    protected void Grid1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        // 什麼都不做，因為您不打算使用內建更新功能

        // 如果 GridView 處於編輯模式，這兩行可以讓它退出編輯模式
        Grid1.EditIndex = -1;
        // Grid1.DataBind(); 
    }

    public void OnBeforeExport1(object sender, Ede.Uof.Utility.Component.BeforeExportEventArgs e)
    {
        //SETEXCEL();

    }

    public void MsgBox(String ex, Page pg, Object obj)
    {
        string script = "alert('" + ex.Replace("\r\n", "\\n").Replace("'", "") + "');";
        ScriptManager.RegisterStartupScript(pg, obj.GetType(), "AlertScript", script, true);

        //string s = "<SCRIPT language='javascript'>alert('" + ex.Replace("\r\n", "\\n").Replace("'", "") + "'); </SCRIPT>";
        //Type cstype = obj.GetType();
        //ClientScriptManager cs = pg.ClientScript;
        //cs.RegisterClientScriptBlock(cstype, s, s.ToString());
    }
    #endregion


    #region BUTTON
    protected void Button1_Click(object sender, EventArgs e)
    {       
        BindGrid();
    }
    #endregion
}