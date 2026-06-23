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

public partial class CDS_WebPage_COP_TK_COPTATBSTOP : Ede.Uof.Utility.Page.BasePage
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
    private void BindGrid()
    {
        // 1. 取得連線字串
        string connectionString = ConfigurationManager.ConnectionStrings["ERPconnectionstring"].ConnectionString;
        Ede.Uof.Utility.Data.DatabaseHelper m_db = new Ede.Uof.Utility.Data.DatabaseHelper(connectionString);

        // 2. 處理查詢條件 (全面改用參數化，消滅 AppendFormat 注入風險)
        StringBuilder queryConditions = new StringBuilder();

        string ta002 = QUERY_TextBox1.Text.Trim();
        if (!string.IsNullOrEmpty(ta002))
        {
            queryConditions.AppendLine(" AND A.TA002 LIKE @TA002 + '%' ");
            m_db.AddParameter("@TA002", ta002);
        }

        // 💡 預留：如果您未來要加入日期欄位查詢 (範例)
        // DateTime searchDate;
        // if (DateTime.TryParse(txtSearchDate.Text, out searchDate))
        // {
        //     queryConditions.AppendLine(" AND B.TB016 = @SearchDate ");
        //     // 轉成 yyyy/MM/dd 萬國通用字串格式傳給 SQL
        //     m_db.AddParameter("@SearchDate", searchDate.ToString("yyyy/MM/dd", System.Globalization.CultureInfo.InvariantCulture));
        // }

        // 3. 組裝 SQL (改用標準 INNER JOIN 提高大表關聯效能)
        StringBuilder cmdTxt = new StringBuilder();
        cmdTxt.AppendFormat(@"
                            SELECT 
                                A.TA001 AS '報價單別',
                                A.TA002 AS '報價單號',
                                A.TA006 AS '客戶',
                                M.MV002 AS '業務員',
                                B.TB004 AS '品號',
                                B.TB005 AS '品名',
                                B.TB007 AS '報價數量',
                                B.TB008 AS '報價單位',
                                CONVERT(INT,B.TB009) AS '報價單價',
                                (CASE A.TA022 
                                    WHEN '1' THEN '內含'  
                                    WHEN '2' THEN '外加'  
                                    WHEN '3' THEN '零稅率' 
                                    WHEN '4' THEN '免稅' 
                                    WHEN '5' THEN '不計稅' 
                                    ELSE '' 
                                 END) AS '稅別',
                                CONVERT(VARCHAR(10), B.TB016, 111) AS '生效日期', -- 確保格式為 yyyy/MM/dd
                                CONVERT(VARCHAR(10), B.TB017, 111) AS '失效日期', -- 確保格式為 yyyy/MM/dd
                                B.TB006 AS '規格',
                                A.TA004 AS '客代',
                                A.TA005 AS '業務',
                                A.TA007 AS '幣別'
                            FROM [TK].dbo.COPTA A
                            INNER JOIN [TK].dbo.COPTB B ON A.TA001 = B.TB001 AND A.TA002 = B.TB002
                            LEFT JOIN [TK].dbo.CMSMV M ON M.MV001 = A.TA005
                            WHERE 1=1
                            {0}
                            ORDER BY A.TA001, A.TA002
                        ", queryConditions.ToString());

        // 4. 執行與綁定
        DataTable dt = new DataTable();
        using (var reader = m_db.ExecuteReader(cmdTxt.ToString()))
        {
            dt.Load(reader);
        }

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
        if (e.CommandName == "MYUPDATE")
        {     
            MsgBox("更新完成 \r\n " + ID, this.Page, this);
        }       

        BindGrid();
    }
    // 雖然不應該被觸發，但定義它以避免 HttpCException
    protected void Grid1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        // 這裡可以不寫程式碼
    }

    protected void Grid1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        // 這裡可以不寫程式碼，因為你的邏輯寫在 RowCommand 了
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