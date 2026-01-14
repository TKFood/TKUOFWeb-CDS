using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Dynamic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security.AntiXss;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ede.Uof.Utility.Data;
using Ede.Uof.Utility.Page.Common;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using OfficeOpenXml;
using OfficeOpenXml.Drawing;
using OfficeOpenXml.Style;
using System.Web.Services;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using Ede.Uof.EIP.SystemInfo;
using Telerik.Web.UI;
using System.Web.UI.HtmlControls;

public partial class CDS_WebPage_COP_TK_COP_ADJUST_COPTH : Ede.Uof.Utility.Page.BasePage
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
            Label_Todays.Text = "本日日期: " + DateTime.Now.ToString("yyyyMMdd"); 

            //BindGrid();
        }
    }

    #region FUNCTION
    public void SEARCHGROUPSALES(string TG002)
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
                            TG001 AS '單別'
                            ,TG002 AS '單號'
                            ,TG007 AS '客戶'
                            ,TG013 AS '原幣銷貨金額'
                            ,TG025 AS '原幣銷貨稅額'
                            ,TG045 AS '本幣銷貨金額'
                            ,TG046 AS '本幣銷貨稅額'
                            ,TH003 AS '序號'
                            ,TH004 AS '品號'
                            ,TH005 AS '品名'
                            ,TH008 AS '數量'
                            ,TH009 AS '單位'
                            ,TH013 AS '金額'
                            ,TH035 AS '原幣未稅金額'
                            ,TH036 AS '原幣稅額'
                            ,TH037 AS '本幣未稅金額'
                            ,TH038 AS '本幣稅額'
                            ,TG027
                            ,TG028
                            ,TG029
                            FROM [TK].dbo.COPTG,[TK].dbo.COPTH
                            WHERE TG001=TH001 AND TG002=TH002
                            AND TG001 NOT IN ('A232')
                            AND TG002 LIKE '20260113%'
                            ORDER BY TG001,TG002
                        ", TG002);


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
    // 雖然不應該被觸發，但定義它以避免 HttpCException
    protected void Grid1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {        
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
        SEARCHGROUPSALES("");
    }
    #endregion
}