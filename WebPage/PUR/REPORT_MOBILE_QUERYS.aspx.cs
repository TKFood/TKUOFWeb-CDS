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

public partial class CDS_WebPage_PUR_REPORT_MOBILE_QUERYS : Ede.Uof.Utility.Page.BasePage
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
            BindGrid2();
        }
    }
    #region FUNCTION
    private void BindGrid()
    {
        // 1.取得連線字串
        // 請將 "YourConnectionStringName" 替換為 Web.config 中定義的連線名稱
        string connectionString = ConfigurationManager.ConnectionStrings["ERPconnectionstring"].ConnectionString;
        Ede.Uof.Utility.Data.DatabaseHelper m_db = new Ede.Uof.Utility.Data.DatabaseHelper(connectionString);

        // 2. 定義 SQL 查詢字串           
        string cmdTxt = @"
                        SELECT
                        LA001 AS '品號'
                        ,MB002  AS '品名'
                        ,SUM(LA005*LA011)  AS '庫存數量'
                        , MB004  AS '單位'
                        FROM [TK].dbo.INVLA WITH(NOLOCK )
                        LEFT JOIN [TK].dbo.INVMB ON MB001=LA001
                        WHERE LA001 IN
                        (
                        SELECT  [MB001]
                        FROM [TKPUR].[dbo].[UOF_QUERYS]
                        )
                        GROUP BY LA001,MB002, MB004
                        ORDER BY LA001
                        ";

        //m_db.AddParameter("@DATESTART", TextBox1.Text.Trim());


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
        //SETEXCEL();

    }
    private void BindGrid2()
    {
        // 1.取得連線字串
        // 請將 "YourConnectionStringName" 替換為 Web.config 中定義的連線名稱
        string connectionString = ConfigurationManager.ConnectionStrings["ERPconnectionstring"].ConnectionString;
        Ede.Uof.Utility.Data.DatabaseHelper m_db = new Ede.Uof.Utility.Data.DatabaseHelper(connectionString);

        // 2. 定義 SQL 查詢字串           
        string cmdTxt = @"                        
                        SELECT 
                        TD012 AS '預交日'
                        ,TC001 AS '採購單'
                        ,TC002 AS '採購單號'
                        ,TD004 AS '品號'
                        ,TD005 AS '品名'
                        ,TD008 AS '採購數量'
                        ,TD015 AS '已進貨數量'
                        ,TD009 AS '單位'
                        FROM [TK].dbo.PURTC,[TK].dbo.PURTD
                        WHERE TC001=TD001 AND TC002=TD002
                        AND TD002>='20260408001'
                        AND TD004 IN
                        (
                        SELECT  [MB001]
                        FROM [TKPUR].[dbo].[UOF_QUERYS]
                        )
                        AND TD008>TD015
                        ORDER BY TD004,TC003

                        ";

        //m_db.AddParameter("@DATESTART", TextBox1.Text.Trim());


        DataTable dt = new DataTable();

        dt.Load(m_db.ExecuteReader(cmdTxt.ToString()));


        Grid2.DataSource = dt;
        Grid2.DataBind();

    }

    protected void grid_PageIndexChanging2(object sender, GridViewPageEventArgs e)
    {

    }
    protected void Grid2_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    protected void Grid2_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int rowIndex = -1;
    }


    public void OnBeforeExport2(object sender, Ede.Uof.Utility.Component.BeforeExportEventArgs e)
    {
        //SETEXCEL();

    }
    public void MsgBox(String ex, Page pg, Object obj)
    {
        string script = "alert('" + ex.Replace("\r\n", "\\n").Replace("'", "") + "');";
        ScriptManager.RegisterStartupScript(pg, obj.GetType(), "AlertScript", script, true);

        // MsgBox("MsgBox!!!!    " + error + "\r\n" + Form.OuterXml, this.Page, this);
    }
    #endregion


    #region BUTTON
    protected void Button1_Click(object sender, EventArgs e)
    {
        BindGrid();
        BindGrid2();
    }

    #endregion
}