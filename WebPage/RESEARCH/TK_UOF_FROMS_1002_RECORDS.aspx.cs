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

public partial class CDS_WebPage_RESEARCH_TK_UOF_FROMS_1002_RECORDS : Ede.Uof.Utility.Page.BasePage
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
        string connectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
        Ede.Uof.Utility.Data.DatabaseHelper m_db = new Ede.Uof.Utility.Data.DatabaseHelper(connectionString);

        StringBuilder cmdTxt = new StringBuilder();
        StringBuilder QUERYS = new StringBuilder();
        StringBuilder QUERYS2 = new StringBuilder();
        StringBuilder QUERYS3 = new StringBuilder();


        cmdTxt.AppendFormat(@"                              
                            SELECT 
                                f.FORM_NAME       AS '表單名稱',
                                u.NAME            AS '申請者',
                                CONVERT(nvarchar,t.BEGIN_TIME,111)      AS '申請時間',
                                t.DOC_NBR         AS '表單編號',   
	                            (CASE WHEN t.TASK_RESULT='0' THEN '已結案' ELSE '進行中' END ) TASK_RESULT,
                                t.CURRENT_SITE_ID,
	                            t.TASK_ID,
                                t.CURRENT_DOC,
	                            TD.Row.value('@order', 'INT') + 1 AS '項次',
                                TD.Row.value('(Cell[@fieldId=""DVV01""]/@fieldValue)[1]', 'NVARCHAR(MAX)') AS '產品名稱',
                                TD.Row.value('(Cell[@fieldId=""DVV02""]/@fieldValue)[1]', 'NVARCHAR(MAX)') AS '包裝方式',
                                TD.Row.value('(Cell[@fieldId=""DVV03""]/@fieldValue)[1]', 'NVARCHAR(MAX)') AS '規格',
                                TD.Row.value('(Cell[@fieldId=""DVV09""]/@fieldValue)[1]', 'NVARCHAR(MAX)') AS '尺寸',
                                TD.Row.value('(Cell[@fieldId=""DVV10""]/@fieldValue)[1]', 'NVARCHAR(MAX)') AS '包材',
                                TD.Row.value('(Cell[@fieldId=""DVV04""]/@fieldValue)[1]', 'NVARCHAR(MAX)') AS '需求量',
                                TD.Row.value('(Cell[@fieldId=""DVV07""]/@fieldValue)[1]', 'NVARCHAR(MAX)') AS '預計完工日',

                               	[TK_UOF_RECORDS_1002].[COMMENTS] AS '備註',
                                [TK_UOF_RECORDS_1002].[ISCLOSED] AS '結案'

                            FROM[UOF].dbo.TB_WKF_TASK AS t WITH(NOLOCK)
                            CROSS APPLY[CURRENT_DOC].nodes('/Form/FormFieldValue/FieldItem[@fieldId=""DETAILS""]/DataGrid/Row') AS TD(Row)
                            LEFT JOIN[192.168.1.105].[TKRESEARCH].[dbo].[TK_UOF_RECORDS_1002] WITH(NOLOCK) ON[TK_UOF_RECORDS_1002].[DOC_NBR] = t.[DOC_NBR] COLLATE Chinese_Taiwan_Stroke_CI_AS AND[TK_UOF_RECORDS_1002].[SERNO] = TD.Row.value('@order', 'INT') + 1
                            LEFT JOIN[UOF].dbo.TB_EB_USER AS u
                                ON u.USER_GUID = t.USER_GUID
                            LEFT JOIN[UOF].dbo.TB_EB_EMPL_DEP AS ed
                                ON ed.USER_GUID = u.USER_GUID AND ed.ORDERS = '0'
                            JOIN[UOF].dbo.TB_WKF_FORM_VERSION AS fv
                                ON t.FORM_VERSION_ID = fv.FORM_VERSION_ID
                            JOIN[UOF].dbo.TB_WKF_FORM AS f
                                ON f.FORM_ID = fv.FORM_ID
                            WHERE 1 = 1
                                AND t.BEGIN_TIME >= '2025-01-01'
                                AND f.FORM_NAME IN('1004.無品號試吃製作申請單')


                            ORDER BY f.FORM_NAME, t.DOC_NBR;

        ", QUERYS.ToString());




        //m_db.AddParameter("@SDATE", SDATE);
        //m_db.AddParameter("@EDATE", EDATE);

        DataTable dt = new DataTable();

        dt.Load(m_db.ExecuteReader(cmdTxt.ToString()));

        //匯出專用
        EXCELDT1 = dt;

        Grid1.DataSource = dt;
        Grid1.DataBind();
    }

    protected void grid_PageIndexChanging1(object sender, GridViewPageEventArgs e)
    {
        //Grid1.PageIndex = e.NewPageIndex;
        //BindGrid();
    }
    protected void Grid1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string taskId = DataBinder.Eval(e.Row.DataItem, "TASK_ID") as string;
            HyperLink hlTask = (HyperLink)e.Row.FindControl("hlTask");

            if (!string.IsNullOrEmpty(taskId))
            {
                hlTask.NavigateUrl = string.Format("https://eip.tkfood.com.tw/UOF/wkf/formuse/viewform.aspx?TASK_ID={0}", taskId);
            }
            else
            {
                hlTask.Visible = false; // 或改成顯示文字 Label
            }
        }
    }

    protected void Grid1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int rowIndex = -1;
        // 獲取所選行的索引
        rowIndex = Convert.ToInt32(e.CommandArgument);

    }


    public void OnBeforeExport1(object sender, Ede.Uof.Utility.Component.BeforeExportEventArgs e)
    {
        SETEXCEL();

    }


    public void SETEXCEL()
    {
        BindGrid();
        if (EXCELDT1 == null || EXCELDT1.Rows.Count == 0) return;

        var fileName = "客訴明細_" + DateTime.Now.ToString("yyyyMMdd_HHmm") + ".xlsx";
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

        using (var excel = new ExcelPackage())
        {
            var ws = excel.Workbook.Worksheets.Add("明細列表");

            // 【16 個欄位對應表：Key 為資料庫欄位名，Value 為 Excel 標題】
            var columnMap = new Dictionary<string, string>
        {
            { "DOC_NBR", "表單編號" },
            { "QCFrm002Date", "客訴日期" },
            { "QCFrm002PRD", "客訴商品" },
            { "QCFrm002PNO", "批號" },
            { "QCFrm002Abns", "客訴原因" },
            { "QCFrm002AbnscustomValue", "原因明細" },
            { "QCFrm002CUST", "客人" },
            { "QCFrm002Abn", "客訴內容" },
            { "QCFrm002Cmf", "品保初判" },
            { "TASK_RESULT", "簽核狀態" },
            { "ORIGINAL_SIGNER", "簽核人" },
            { "KINDS", "客訴類型" },
            { "REASONS", "原因分析" },
            { "IMPROVES", "改善方案" },
            { "IMPROVESOWNER", "改善負責單位" },
            { "IMPROVESDATES", "預計改善完成日" }
        };

            // 1. 寫入標題列並設定樣式
            int colIndex = 1;
            foreach (var item in columnMap)
            {
                var cell = ws.Cells[1, colIndex];
                cell.Value = item.Value;

                // 標題美化
                cell.Style.Font.Bold = true;
                cell.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                cell.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightSlateGray);
                cell.Style.Font.Color.SetColor(System.Drawing.Color.White);
                cell.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                colIndex++;
            }

            // 2. 寫入資料內容
            int rowIndex = 2;
            foreach (DataRow dr in EXCELDT1.Rows)
            {
                colIndex = 1;
                foreach (var item in columnMap)
                {
                    ws.Cells[rowIndex, colIndex].Value = dr[item.Key];

                    // 設定格線
                    ws.Cells[rowIndex, colIndex].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                    ws.Cells[rowIndex, colIndex].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                    colIndex++;
                }
                rowIndex++;
            }

            // 3. 後端優化設定
            // 開啟自動篩選
            ws.Cells[1, 1, 1, columnMap.Count].AutoFilter = true;

            // 凍結首列
            ws.View.FreezePanes(2, 1);

            // 自適應欄寬 (內容多時會跑比較久，若效能考量可固定特定欄寬)
            ws.Cells[ws.Dimension.Address].AutoFitColumns();

            // 針對「客訴內容」、「原因分析」等長文字欄位設定自動斷行
            // 假設它們是第 8, 13, 14 欄
            int[] longTextCols = { 8, 13, 14 };
            foreach (int colNum in longTextCols)
            {
                ws.Column(colNum).Width = 40; // 固定寬度避免太長
                ws.Column(colNum).Style.WrapText = true;
            }

            // 4. 輸出下載
            byte[] data = excel.GetAsByteArray();
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment; filename=" + HttpUtility.UrlEncode(fileName));
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.BinaryWrite(data);
            Response.Flush();
            Response.End();
        }
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