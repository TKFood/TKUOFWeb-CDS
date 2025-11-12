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

public partial class CDS_WebPage_MARKETING_TK_QUERYS : Ede.Uof.Utility.Page.BasePage
{
    string ACCOUNT = null;
    string NAME = null;
    String ROLES = null;

    private decimal TOTALNUMS = 0.00m;
    private decimal TOTALMONEYS = 0.00m;
    private decimal TOTALCOSTS = 0.00m;

    protected void Page_Load(object sender, EventArgs e)
    {
        ACCOUNT = Current.Account;
        NAME = Current.User.Name;

        if (!IsPostBack)
        {
            SET_DATES();
        }
    }

    #region FUNCTION
    public void SET_DATES()
    {
        TextBox1.Text = DateTime.Now.ToString("yyyyMMdd");
        TextBox2.Text = DateTime.Now.ToString("yyyyMMdd");
        TextBox4.Text = DateTime.Now.ToString("yyyyMMdd");
        TextBox5.Text = DateTime.Now.ToString("yyyyMMdd");
    }
    private void BindGrid()
    {
        // 1.取得連線字串
        // 請將 "YourConnectionStringName" 替換為 Web.config 中定義的連線名稱
        string connectionString = ConfigurationManager.ConnectionStrings["ERPconnectionstring"].ConnectionString;
        Ede.Uof.Utility.Data.DatabaseHelper m_db = new Ede.Uof.Utility.Data.DatabaseHelper(connectionString);

        // 2. 定義 SQL 查詢字串           
        string cmdTxt = @"
                            SELECT
                                MIN(TA001) AS '日期起',
                                MAX(TA001) AS '日期迄',
                                POSTA.TA002 AS '門市代',
                                CMSME.ME002 AS '門市',
                                COUNT(POSTA.TA002) AS '銷售總筆數',
                                SUM(POSTA.TA026) AS '銷售總金額含稅',
                                SUM(CASE WHEN POSTA.TA026 >= @QUERYMONEY THEN 1 ELSE 0 END) AS '滿額總筆數',
                                SUM(CASE WHEN POSTA.TA026 >= @QUERYMONEY THEN POSTA.TA026 ELSE 0 END) AS '滿額金額含稅'
                            FROM
                                [TK].dbo.POSTA POSTA
                            INNER JOIN
                                [TK].dbo.CMSME CMSME ON POSTA.TA002 = CMSME.ME001
                            WHERE
                                POSTA.TA001 >= @DATESTART
                                AND POSTA.TA001 <= @DATESEND
                                AND POSTA.TA026 >=0
                                AND POSTA.TA002 LIKE '106%'
                            GROUP BY
                                POSTA.TA002, CMSME.ME002
                            ORDER BY
                                POSTA.TA002;
                        ";

        m_db.AddParameter("@DATESTART", TextBox1.Text.Trim());
        m_db.AddParameter("@DATESEND", TextBox2.Text.Trim());
        m_db.AddParameter("@QUERYMONEY", TextBox3.Text.Trim());

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

        StringBuilder SQL_QUERY1 = new StringBuilder();
        StringBuilder cmdTxt = new StringBuilder();

        string DATESTART= TextBox4.Text.Trim();
        string DATESEND= TextBox5.Text.Trim();
        string MB001 = TextBox6.Text.Trim();

        if (!string.IsNullOrEmpty(MB001))
        {
            SQL_QUERY1.AppendFormat(@"
                                    AND ( TH004 LIKE '%{0}%' OR MB002 LIKE '%{0}%' )
                                    ", MB001);
        }
        // 2. 定義 SQL 查詢字串           
        cmdTxt.AppendFormat(@"
                            SELECT 
                            TG005
                            ,ME002
                            ,TH004
                            ,MB002
                            ,SUM(NUMS) AS 'TOTALNUMS'
                            ,SUM(MOEYS) AS 'TOTALMONEYS'
                            ,SUM(LA013) AS 'TOTALCOSTS'
                            FROM 
                            (
	                            SELECT 
	                            CONVERT(NVARCHAR,YEAR(TG003))+RIGHT('0' + CONVERT(NVARCHAR, MONTH(TG003)), 2) AS 'YM'	
	                            ,TG005,ME002,TH004,MB002,(TH008+TH024) AS 'NUMS',(TH013) AS 'MOEYS'
	                            ,LA013
	                            FROM [TK].dbo.COPTG
	                            LEFT JOIN [TK].dbo.CMSME ON ME001=TG005
	                            ,[TK].dbo.COPTH,[TK].dbo.INVMB
	                            ,[TK].dbo.INVLA
	                            WHERE TG001=TH001 AND TG002=TH002
	                            AND MB001=TH004
	                            AND TG023 IN ('Y')
	                            AND TH017<>'********************'
	                            AND LA006=TH001 AND LA007=TH002 AND LA008=TH003
	                            AND TG003>=@DATESTART AND TG003<=@DATESEND

	                            UNION ALL
	                            SELECT 
	                            CONVERT(NVARCHAR,YEAR(TI003))+RIGHT('0' + CONVERT(NVARCHAR, MONTH(TI003)), 2) AS 'YM'
	                            ,TI005,ME002,TJ004,MB002,(TJ007)*-1 AS 'NUMS',(TJ012)*-1 AS 'MOEYS'
	                            ,LA013*-1
	                            FROM [TK].dbo.COPTI
	                            LEFT JOIN [TK].dbo.CMSME ON ME001=TI005
	                            ,[TK].dbo.COPTJ,[TK].dbo.INVMB
	                            ,[TK].dbo.INVLA
	                            WHERE TI001=TJ001 AND TI002=TJ002
	                            AND MB001=TJ004
	                            AND TI019 IN ('Y')
	                            AND TJ014<>'********************'
	                            AND LA006=TJ001 AND LA007=TJ002 AND LA008=TJ003
	                            AND TI003>=@DATESTART AND TI003<=@DATESEND

	                            UNION ALL
	                            SELECT 
	                            CONVERT(NVARCHAR,YEAR(TB001))+RIGHT('0' + CONVERT(NVARCHAR, MONTH(TB001)), 2) AS 'YM'
	                            ,TB002,ME002,TB010,MB002,(TB019) AS 'NUMS',(TB031+TB032) AS 'MOEYS'	
	                            ,(CASE WHEN LA012>0  THEN  LA012 ELSE 0 END )*TB019
	                            FROM [TK].dbo.POSTB
	                            LEFT JOIN [TK].dbo.CMSME ON ME001=TB002
	                            ,[TK].dbo.INVMB
	                            , [TK].dbo.INVLA 
	                            WHERE MB001=TB010	
	                            AND LA001=TB010 AND LA006=TB002 AND LA007=TB001
	                            AND TB001>=@DATESTART AND TB001<=@DATESEND

                            )  AS TEMP
                            WHERE 1=1
                            {0}
                            GROUP BY 
                            TG005
                            ,ME002
                            ,TH004
                            ,MB002
                            ORDER BY 
                            TH004
                            ,TG005
                        ", SQL_QUERY1.ToString());

        m_db.AddParameter("@DATESTART", DATESTART);
        m_db.AddParameter("@DATESEND", DATESEND);
        //m_db.AddParameter("@QUERYMONEY", TextBox3.Text.Trim());

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
        // 判斷當前處理的是否為資料列 (DataRow)
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // A. 取得該列的資料
            // 假設您的資料來源是一個 DataRow 或您知道欄位的索引

            // 取得金額 (假設 "Amount" 是第二個欄位，索引為 1)
            // 建議使用 DataBinder.Eval 更安全，假設資料來源是 DataTable/List<T>
            decimal amount = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TOTALNUMS"));
            TOTALNUMS += amount;

            decimal MONEYS = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TOTALMONEYS"));
            TOTALMONEYS += MONEYS;

            decimal COSTS = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TOTALCOSTS"));
            TOTALCOSTS += COSTS;

            // 可選：對 DataRow 中的金額進行格式化顯示
            //e.Row.Cells[4].Text = amount.ToString("N0"); // 三位一逗點，無小數點
        }

        // 判斷當前處理的是否為合計列 (Footer)
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            // B. 顯示合計文字和結果

            // 設置第一個儲存格顯示 "合計" 字樣 (索引 0)
            e.Row.Cells[3].Text = "合計：";
            // 可選：設置對齊和粗體
            e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Font.Bold = true;

            // 設置金額合計結果 (索引 1)
            e.Row.Cells[4].Text = TOTALNUMS.ToString("N0"); // 格式化為三位一逗點
            e.Row.Cells[5].Text = TOTALMONEYS.ToString("N0"); // 格式化為三位一逗點
            e.Row.Cells[6].Text = TOTALCOSTS.ToString("N0"); // 格式化為三位一逗點

            e.Row.Cells[3].Font.Size = new FontUnit("16pt"); 
            e.Row.Cells[4].Font.Size = new FontUnit("16pt");
            e.Row.Cells[5].Font.Size = new FontUnit("16pt"); 
            e.Row.Cells[6].Font.Size = new FontUnit("16pt"); 


        }
    }

    protected void Grid2_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int rowIndex = -1;
    }


    public void OnBeforeExport2(object sender, Ede.Uof.Utility.Component.BeforeExportEventArgs e)
    {
        //SETEXCEL();

    }
    #endregion


    #region BUTTON
    protected void Button1_Click(object sender, EventArgs e)
    {
        BindGrid();
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        BindGrid2();

        Label_query_dates.Text = "查詢日期區間: "+TextBox4.Text.Trim() + " ~ " + TextBox5.Text.Trim();
        Label_query_dates.Font.Size = new FontUnit("16pt"); 
    }
    #endregion
}