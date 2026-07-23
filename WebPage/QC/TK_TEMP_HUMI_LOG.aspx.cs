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
using Ede.Uof.Utility.FileCenter.V3;

public partial class CDS_WebPage_QC_TK_TEMP_HUMI_LOG : Ede.Uof.Utility.Page.BasePage
{
    string ACCOUNT = null;
    string NAME = null;
    String ROLES = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // 第一次載入時先查詢一次
            BindData();
        }
    }

    #region FUNCTION
    // 每次 Timer 到期時觸發的事件
    protected void Timer1_Tick(object sender, EventArgs e)
    {
        BindData(); // 重新向資料庫查詢並繫結資料
        BindGrid();
    }

    private void BindData()
    {
        lblStatus.Text = "最後更新時間：" + DateTime.Now.ToString("HH:mm:ss");
        // GridView1.DataSource = ... 取得資料庫資料
        // GridView1.DataBind();
    }

    private void BindGrid()
    {
        // 1.取得連線字串
        // 請將 "YourConnectionStringName" 替換為 Web.config 中定義的連線名稱
        string connectionString = ConfigurationManager.ConnectionStrings["ConnectionStringRecipe"].ConnectionString;
        Ede.Uof.Utility.Data.DatabaseHelper m_db = new Ede.Uof.Utility.Data.DatabaseHelper(connectionString);

        // 2. 定義 SQL 查詢字串           
        string cmdTxt = @"
                           SELECT 
                            [ID]
                            ,[機台名稱]
                            ,[區域]
                            ,[TMP]
                            ,[HUM]
                            ,(SELECT TOP 1  CONVERT(DECIMAL(16,2),[控項_1]) FROM  [TK_FOOD].[dbo].[log_table] WHERE [log_table].[機台名稱]=[Machine].[機台名稱] ORDER BY [日期時間] DESC) AS '實際溫度'
                            ,(SELECT TOP 1  CONVERT(DECIMAL(16,2),[控項_2]) FROM  [TK_FOOD].[dbo].[log_table] WHERE [log_table].[機台名稱]=[Machine].[機台名稱] ORDER BY [日期時間] DESC) AS '溫度上限'
                            ,(SELECT TOP 1  CONVERT(DECIMAL(16,2),[控項_2]) FROM  [TK_FOOD].[dbo].[log_table] WHERE [log_table].[機台名稱]=[Machine].[機台名稱] ORDER BY [日期時間] DESC) AS '溫度上限'
                            ,(SELECT TOP 1  CONVERT(DECIMAL(16,2),[控項_3]) FROM  [TK_FOOD].[dbo].[log_table] WHERE [log_table].[機台名稱]=[Machine].[機台名稱] ORDER BY [日期時間] DESC) AS '溫度下限'
                            ,(SELECT TOP 1  CONVERT(DECIMAL(16,2),[控項_4]) FROM  [TK_FOOD].[dbo].[log_table] WHERE [log_table].[機台名稱]=[Machine].[機台名稱] ORDER BY [日期時間] DESC) AS '實際溼度'
                            ,(SELECT TOP 1  CONVERT(DECIMAL(16,2),[控項_5]) FROM  [TK_FOOD].[dbo].[log_table] WHERE [log_table].[機台名稱]=[Machine].[機台名稱] ORDER BY [日期時間] DESC) AS '溼度上限'
                            ,(SELECT TOP 1  CONVERT(DECIMAL(16,2),[控項_6]) FROM  [TK_FOOD].[dbo].[log_table] WHERE [log_table].[機台名稱]=[Machine].[機台名稱] ORDER BY [日期時間] DESC) AS '溼度下限'
                            FROM [TK_FOOD].[dbo].[Machine]
                            WHERE [機台名稱] LIKE '%溫濕度%'
                            ORDER BY CONVERT(INT,[ID])
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
        // 必須先判斷是否為「資料列」（避開 Header 和 Footer）
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            decimal num = 0;
            decimal upLimit = 0;
            decimal lowLimit = 0;

            // 1. 抓取 TemplateField 內部的 Label 控制項（請確認 ID 是否正確）
            Label lblNum = (Label)e.Row.FindControl("Label_實際溫度"); // 假設 Cell[2] 裡面的 Label ID 是這個
            Label lblUp = (Label)e.Row.FindControl("Label_溫度上限");      // 若 Cell[3] 也是 TemplateField 就這樣抓
            Label lblLow = (Label)e.Row.FindControl("Label_溫度下限");     // 若 Cell[4] 也是 TemplateField 就這樣抓

            // 2. 取出 Label 的 .Text 並轉型 (如果不是 TemplateField，才用 e.Row.Cells[x].Text)
            string strNum = lblNum != null ? lblNum.Text : e.Row.Cells[2].Text;
            string strUp = lblUp != null ? lblUp.Text : e.Row.Cells[3].Text;
            string strLow = lblLow != null ? lblLow.Text : e.Row.Cells[4].Text;

            // 3. 安全轉型為 decimal
            decimal.TryParse(strNum, out num);
            decimal.TryParse(strUp, out upLimit);
            decimal.TryParse(strLow, out lowLimit);

            // 💡 條件判斷：超過上限 或 低於下限
            if (num > upLimit || num < lowLimit)
            {
                // 整行變粉紅色背景
                e.Row.BackColor = System.Drawing.Color.Red;

                // 或者：若只要「實際溫度」儲存格變紅底白字
                // if (lblNum != null) {
                //     e.Row.Cells[2].BackColor = System.Drawing.Color.Red;
                //     lblNum.ForeColor = System.Drawing.Color.White;
                // }
            }
        }
    }

    protected void Grid1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int rowIndex = -1;
    }


    public void OnBeforeExport1(object sender, Ede.Uof.Utility.Component.BeforeExportEventArgs e)
    {
        //SETEXCEL();

    }
    #endregion


    #region BUTTON
    protected void Button1_Click(object sender, EventArgs e)
    {
        BindGrid();
    }

    #endregion

}