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
public partial class CDS_WebPage_PUR_TK_PUR_BOM_OBJECTS_COSTS : Ede.Uof.Utility.Page.BasePage
{
    string ACCOUNT = null;
    string NAME = null;
    String ROLES = null;

    DataTable EXCELDT1 = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        ACCOUNT = Current.Account;
        NAME = Current.User.Name;

        if (!IsPostBack)
        {
           
        }

    }


    #region FUNCTION
    private void BindGrid1(string MB001)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ERPconnectionstring"].ToString();
        Ede.Uof.Utility.Data.DatabaseHelper m_db = new Ede.Uof.Utility.Data.DatabaseHelper(connectionString);

        StringBuilder cmdTxt = new StringBuilder();
        StringBuilder QUERYS = new StringBuilder();


        if (!string.IsNullOrEmpty(MB001))
        {

            cmdTxt.AppendFormat(@"  
                                WITH RecursiveBOM AS (
                                    -- 1. 錨點查詢（第一層）：限定開頭母件條件
                                    SELECT 
                                        1 AS [階層],
                                        MC1.MC001 AS [主要品號], -- 🎯 記錄最頂層的主件品號
                                        MC1.MC001 AS [主件品號],
                                        MD1.MD003 AS [元件品號],
                                        CAST(MC1.MC001 + ' -> ' + ISNULL(MD1.MD003, '') AS NVARCHAR(MAX)) AS [階層路徑]
                                    FROM [TK].dbo.BOMMC MC1 WITH(NOLOCK)
                                    INNER JOIN [TK].dbo.INVMB MB1 WITH(NOLOCK) 
                                        ON MB1.MB001 = MC1.MC001
                                    -- 第一層展開子件
                                    LEFT JOIN [TK].dbo.BOMMD MD1 WITH(NOLOCK) 
                                        ON MD1.MD001 = MC1.MC001
                                    WHERE 1=1
                                      AND (MB1.MB001 LIKE '4%' OR MB1.MB001 LIKE '5%')     
                                      AND (MB1.MB001 LIKE '%{0}%' OR MB1.MB002 LIKE '%{0}%')

                                    UNION ALL

                                    -- 2. 遞迴查詢（第二層起）：繼承上一層的 [頂層主要品號]
                                    SELECT 
                                        R.[階層] + 1 AS [階層],
                                        R.[主要品號] AS [主要品號], -- 🎯 繼承第一層的頂層主要品號
                                        MD.MD001 AS [主件品號],     -- 上一層的元件品號 (R.[元件品號]) 當作這一層的母件
                                        MD.MD003 AS [元件品號],     -- 往下找出來的元件品號
                                        CAST(R.[階層路徑] + ' -> ' + MD.MD003 AS NVARCHAR(MAX)) AS [階層路徑]
                                    FROM RecursiveBOM R
                                    -- 這裡改用 INNER JOIN 以符合 SQL Server 遞迴限制
                                    INNER JOIN [TK].dbo.BOMMC MC WITH(NOLOCK) 
                                        ON MC.MC001 = R.[元件品號]  -- 只有當上一層的元件本身也有 BOM (時候) 繼續展開
                                    INNER JOIN [TK].dbo.BOMMD MD WITH(NOLOCK) 
                                        ON MD.MD001 = MC.MC001
                                )
                                -- 3. 最外層統一串接品名 (INVMB) 與頂層品名
                                SELECT 
                                    R.[主要品號],
                                    MB0.MB002 AS [主要品名], -- 🎯 額外帶出頂層主要品號的品名         
                                    R.[階層],
                                    R.[主件品號],
                                    MB1.MB002 AS [主件品名],
                                    R.[元件品號],
                                    MB2.MB002 AS [元件品名],
                                    R.[階層路徑],
                                    MB2.MB050 AS '最近進貨價',
	                                MB2.MB004 AS '單位'
                                FROM RecursiveBOM R
                                LEFT JOIN [TK].dbo.INVMB MB0 WITH(NOLOCK) 
                                    ON MB0.MB001 = R.[主要品號]
                                LEFT JOIN [TK].dbo.INVMB MB1 WITH(NOLOCK) 
                                    ON MB1.MB001 = R.[主件品號]
                                LEFT JOIN [TK].dbo.INVMB MB2 WITH(NOLOCK) 
                                    ON MB2.MB001 = R.[元件品號]
                                WHERE R.[元件品號] LIKE '2%'
                                ORDER BY R.[主要品號], R.[階層路徑];
                                ", MB001);


        }




        //m_db.AddParameter("@SDATE", SDATE);
        //m_db.AddParameter("@EDATE", EDATE);

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


    public void OnBeforeExport1(object sender, Ede.Uof.Utility.Component.BeforeExportEventArgs e)
    {

        MsgBox("MsgBox!!!!", this.Page, this);

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
    protected void btn1_Click(object sender, EventArgs e)
    {
        string MB001 = TextBox1.Text.Trim();
        BindGrid1(MB001);
    }

    #endregion
}