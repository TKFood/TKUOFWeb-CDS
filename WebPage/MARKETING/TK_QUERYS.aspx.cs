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

    // 當前組別的小計變數
    private decimal SUB_TOTALNUMS = 0.00m;
    private decimal SUB_TOTALMONEYS = 0.00m;
    private decimal SUB_TOTALCOSTS = 0.00m;

    // 追蹤前一筆資料的 TH004 值
    private string previousTH004 = string.Empty;
    string currentTH004 = string.Empty;

    // 新增：追蹤手動插入行的總數量
    private int insertedRowCount = 0;

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

        // ***關鍵步驟 1：重設所有追蹤變數***

         // 1. 重設小計和總計累積變數
        SUB_TOTALNUMS = 0.00m;
        SUB_TOTALMONEYS = 0.00m;
        SUB_TOTALCOSTS = 0.00m;
        // 如果您有總計變數 (如 GRAND_TOTALNUMS)，也要重設
        // GRAND_TOTALNUMS = 0.00m;

        // 2. 重設分組追蹤變數
        previousTH004 = string.Empty;

        // 3. 重設手動插入行追蹤變數
        insertedRowCount = 0;

        // *** 關鍵步驟 2：清除 GridView 的 Controls 集合 ***
        // 手動清除之前插入的小計行，避免它們殘留。
        // 這是一個額外的保險，特別是當您手動使用 AddAt 插入 Controls 時。
        if (Grid2.Controls.Count > 0 && Grid2.Controls[0].Controls.Count > 0)
        {
            // Controls[0] 通常是 Table 物件
            Grid2.Controls[0].Controls.Clear();
        }

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
            object th004Value = DataBinder.Eval(e.Row.DataItem, "TH004");
            // 檢查 DataBinder.Eval 的結果是否為 null，如果不是，則調用 ToString()；
            // 如果是 null，則直接返回 string.Empty
            currentTH004 = (th004Value != null) ? th004Value.ToString() : string.Empty;

            decimal currentNUMS = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TOTALNUMS"));
            decimal currentMONEYS = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TOTALMONEYS"));
            decimal currentCOSTS = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TOTALCOSTS"));

            // 只有在第一次載入之後 (previousTH004 不為空) 且 TH004 改變時才插入小計行
            if (!string.IsNullOrEmpty(previousTH004) && currentTH004 != previousTH004)
            {
                // 計算真實的插入位置：e.Row.RowIndex (來自 GridView) + 已經插入的行數 + 1 (插入在下方)
                int actualInsertIndex = e.Row.RowIndex + insertedRowCount + 1;

                InsertSubtotalRow(actualInsertIndex, previousTH004);
                // 重要：插入成功後，更新偏移量
                insertedRowCount++;
            }
            SUB_TOTALNUMS += currentNUMS;
            SUB_TOTALMONEYS += currentMONEYS;
            SUB_TOTALCOSTS += currentCOSTS;

            // ----------------------------------------------------------------------
            // 4. 更新追蹤變數
            // ----------------------------------------------------------------------
            previousTH004 = currentTH004;

            // 可選：對 DataRow 中的金額進行格式化顯示
            //e.Row.Cells[4].Text = amount.ToString("N0"); // 三位一逗點，無小數點
        }

        // 判斷當前處理的是否為合計列 (Footer)
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            // B. 顯示合計文字和結果

            // 設置第一個儲存格顯示 "合計" 字樣 (索引 0)
            e.Row.Cells[3].Text = "小計 (" + currentTH004 + ")：";
            // 可選：設置對齊和粗體
            e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Font.Bold = true;

            // 設置金額合計結果 (索引 1)
            e.Row.Cells[4].Text = SUB_TOTALNUMS.ToString("N0"); // 格式化為三位一逗點
            e.Row.Cells[5].Text = SUB_TOTALMONEYS.ToString("N0"); // 格式化為三位一逗點
            e.Row.Cells[6].Text = SUB_TOTALCOSTS.ToString("N0"); // 格式化為三位一逗點

            // 設定樣式
            e.Row.BackColor = System.Drawing.Color.LightYellow; // 淺黃色背景
            e.Row.Font.Bold = true;
            e.Row.Font.Size = new FontUnit("14pt");

        }
    }

    /// <summary>
    /// 手動插入一個小計行到 GridView
    /// </summary>
    private void InsertSubtotalRow(int currentRowIndex, string th004Value)
    {
        // 建立新的 GridView Row
        GridViewRow subTotalRow = new GridViewRow(currentRowIndex, -1, DataControlRowType.Separator, DataControlRowState.Normal);

        // 依照 GridView 的欄位數量建立儲存格
        for (int i = 0; i < Grid2.Columns.Count; i++)
        {
            TableCell cell = new TableCell();
            cell.Text = "&nbsp;"; // 預設空白
            subTotalRow.Cells.Add(cell);
        }

        // 合併 TH004 之前的欄位，顯示小計資訊 (假設是第 0, 1, 2 欄位)
        subTotalRow.Cells[0].Text = "小計 (" + th004Value + ")：";
        subTotalRow.Cells[0].ColumnSpan =4; // 合併欄位，假設前面有 3 個不顯示數字的欄位
        subTotalRow.Cells[0].HorizontalAlign = HorizontalAlign.Right;

        // 移除合併後多餘的儲存格，以確保 GridView 結構正確
        // 注意：您需要根據 GridView 的實際欄位數量來調整這個移除邏輯
        for (int i = 1; i < 3; i++)
        {
            subTotalRow.Cells.RemoveAt(1);
        }

        // 填充小計結果 (注意儲存格索引會因為 ColumnSpan 而改變)
        // 假設小計結果分別放在索引 1, 2, 3 的位置
        // 如果您合併了 3 欄 (0, 1, 2)，新的 Cell[1] 就是原來的 Cell[4]

        // 設定樣式
        subTotalRow.BackColor = System.Drawing.Color.LightYellow; // 淺黃色背景
        subTotalRow.Font.Bold = true;
        subTotalRow.Font.Size = new FontUnit("14pt");

        // 填充小計結果 (索引需要調整，如果合併 3 欄，TOTALNUMS 會在 Cell[1])
        subTotalRow.Cells[1].Text = SUB_TOTALNUMS.ToString("N0");
        subTotalRow.Cells[2].Text = SUB_TOTALMONEYS.ToString("N0");
        subTotalRow.Cells[3].Text = SUB_TOTALCOSTS.ToString("N0");

        // 將小計行插入
        Grid2.Controls[0].Controls.AddAt(currentRowIndex, subTotalRow);

        // 重置小計變數，準備下一組的計算
        SUB_TOTALNUMS = 0.00m;
        SUB_TOTALMONEYS = 0.00m;
        SUB_TOTALCOSTS = 0.00m;
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