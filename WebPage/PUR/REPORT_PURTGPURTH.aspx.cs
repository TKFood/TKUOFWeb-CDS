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

public partial class CDS_WebPage_PUR_REPORT_PURTGPURTH : Ede.Uof.Utility.Page.BasePage
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
            //BindGrid();
        }
    }

    #region FUNCTION
    public void SEARCH(string SDATE,string EDATES)
    {
        // 1.取得連線字串
        // 請將 "YourConnectionStringName" 替換為 Web.config 中定義的連線名稱
        string connectionString = ConfigurationManager.ConnectionStrings["ERPconnectionstring"].ConnectionString;
        Ede.Uof.Utility.Data.DatabaseHelper m_db = new Ede.Uof.Utility.Data.DatabaseHelper(connectionString);

        StringBuilder QUERY1 = new StringBuilder();

        // 2. 定義 SQL 查詢字串         

        StringBuilder cmdTxt = new StringBuilder();
        cmdTxt.AppendFormat(@"
                             SELECT 
                             TG003 AS '進貨日'
                            ,TG001 AS '進貨單別'
                            ,TG002 AS '進貨單號'
                            ,TH004 AS '品號'
                            ,TH005 AS '品名'
                            ,TH006 AS '規格'
                            ,CONVERT(INT,TH007) AS '數量'
                            ,TH008 AS '單位'
                            ,TH010 AS '批號'
                            ,(CASE WHEN TG013='Y' THEN '已確認' ELSE '未確認' END ) AS '是否確認'
                            FROM [TK].dbo.PURTG,[TK].dbo.PURTH
                            WHERE TG001=TH001 AND TG002=TH002
                            AND TG003>='20251201' AND TG003<='20251201'
                            ORDER BY TG003,TG001,TG002,TH004
                        ");


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
    }
    
    protected void Grid1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        Grid1.EditIndex = -1;
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
        // 查詢
       
        SEARCH("","");
    }
    #endregion
}