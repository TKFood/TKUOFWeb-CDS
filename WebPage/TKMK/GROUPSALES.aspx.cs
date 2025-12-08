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

public partial class CDS_WebPage_TKMK_GROUPSALES : Ede.Uof.Utility.Page.BasePage
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
            Label_Todays.Text = "本日日期: "+DateTime.Now.ToString("yyyyMMdd");
            string CREATEDATES = DateTime.Now.ToString("yyyyMMdd");
            SEARCHGROUPSALES(CREATEDATES);
            //BindDropDownList1();

            //BindGrid();
        }
    }

    #region FUNCTION
    /// <summary>
    /// 找出團務資料
    /// </summary>
    /// <param name="CREATEDATES"></param>
    public void SEARCHGROUPSALES(string CREATEDATES)
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
                            [SERNO] AS '序號'
                            ,[CARNAME] AS '車名'
                            ,[CARNO] AS '車號'
                            ,[CARKIND] AS '車種'
                            ,[GROUPKIND]  AS '團類'
                            ,[ISEXCHANGE] AS '兌換券'
                            ,[EXCHANGETOTALMONEYS] AS '券總額'
                            ,[EXCHANGESALESMMONEYS] AS '券消費'
                            ,[SALESMMONEYS] AS '消費總額'
                            ,[SPECIALMNUMS] AS '特賣數'
                            ,[SPECIALMONEYS] AS '特賣獎金'
                            ,[COMMISSIONBASEMONEYS] AS '茶水費'
                            ,[COMMISSIONPCTMONEYS] AS '消費獎金'
                            ,[TOTALCOMMISSIONMONEYS] AS '總獎金'
                            ,[CARNUM] AS '車數'
                            ,[GUSETNUM] AS '交易筆數'
                            ,[CARCOMPANY] AS '來車公司'
                            ,[TA008NO] AS '業務員名'
                            ,[TA008] AS '業務員帳號'
                            ,[EXCHANNO] AS '優惠券名'
                            ,[EXCHANACOOUNT] AS '優惠券帳號'
                            ,[PLAYDAYKINDS] AS '旅遊天數'
                            ,[PLAYDAYS] AS '第幾天'
                            ,CONVERT(varchar(100), [GROUPSTARTDATES],120) AS '實際到達時間'
                            ,CONVERT(varchar(100), [GROUPENDDATES],120) AS '實際離開時間'
                            ,[STATUS] AS '狀態'
                            ,CONVERT(varchar(100), [PURGROUPSTARTDATES],120) AS '預計到達時間'
                            ,CONVERT(varchar(100), [PURGROUPENDDATES],120) AS '預計離開時間'
                            ,[EXCHANGEMONEYS] AS '領券額'
                            ,[ID]
                            ,[CREATEDATES]

                            FROM [TKMK].[dbo].[GROUPSALES] WITH(NOLOCK)
                            WHERE CONVERT(nvarchar,[CREATEDATES],112)='{0}'
                            AND [STATUS]<>'取消預約'
                            ORDER BY CONVERT(nvarchar,[CREATEDATES],112),CONVERT(int,[SERNO]) DESC
                        ", CREATEDATES);


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
        // 查詢本日來車資料
        string CREATEDATES = DateTime.Now.ToString("yyyyMMdd");
        SEARCHGROUPSALES(CREATEDATES);
    }

    #endregion
}