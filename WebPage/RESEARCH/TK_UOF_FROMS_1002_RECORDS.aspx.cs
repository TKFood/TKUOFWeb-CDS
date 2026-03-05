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

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Button btn2 = (Button)e.Row.FindControl("Button2");
            if (btn2 != null)
            {
                string cellValue2 = btn2.CommandArgument;
                dynamic param2 = new { ID = cellValue2 }.ToExpando();
            }
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Button btn3 = (Button)e.Row.FindControl("Button3");
            if (btn3 != null)
            {
                string cellValue3 = btn3.CommandArgument;
                dynamic param3 = new { ID = cellValue3 }.ToExpando();
            }
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Button btn4 = (Button)e.Row.FindControl("Button4");
            if (btn4 != null)
            {
                string cellValue4 = btn4.CommandArgument;
                dynamic param4 = new { ID = cellValue4 }.ToExpando();
            }
        }
    }

    protected void Grid1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
       // 過濾無關的 Command
         if (e.CommandName == "Page" || e.CommandName == "Sort") return;

        int rowIndex = Convert.ToInt32(e.CommandArgument);

        // 初始化變數
        string DOC_NBR = "", SERNO = "", DVV01 = "", DVV02 = "", DVV03 = "";
        string DVV09 = "", DVV10 = "", DVV04 = "", DVV07 = "", ISCLOSED = "", COMMENTS = "";

        if (rowIndex >= 0 && rowIndex < Grid1.Rows.Count)
        {
            GridViewRow row = Grid1.Rows[rowIndex];

            // 使用 FindControl 並加入防呆檢查 (避免找不控制項導致 NullReferenceException)
            TextBox txt備註 = (TextBox)row.FindControl("txtNewField_GV1_備註");
            Label lbl編號 = (Label)row.FindControl("Label_表單編號");
            Label lbl項次 = (Label)row.FindControl("Label_項次");
            Label lbl產品 = (Label)row.FindControl("Label_產品名稱");
            Label lbl包裝 = (Label)row.FindControl("Label_包裝方式");
            Label lbl規格 = (Label)row.FindControl("Label_規格");
            Label lbl尺寸 = (Label)row.FindControl("Label_尺寸");
            Label lbl包材 = (Label)row.FindControl("Label_包材");
            Label lbl需求 = (Label)row.FindControl("Label_需求量");
            Label lbl完工日 = (Label)row.FindControl("Label_預計完工日");
            Label lbl結案 = (Label)row.FindControl("Label_結案");

            // 賦值(C# 5.0 相容寫法)
            DOC_NBR = (lbl編號 != null) ? lbl編號.Text : "";
            SERNO = (lbl項次 != null) ? lbl項次.Text : "";
            DVV01 = (lbl產品 != null) ? lbl產品.Text : "";
            DVV02 = (lbl包裝 != null) ? lbl包裝.Text : "";
            DVV03 = (lbl規格 != null) ? lbl規格.Text : "";
            DVV09 = (lbl尺寸 != null) ? lbl尺寸.Text : "";
            DVV10 = (lbl包材 != null) ? lbl包材.Text : "";
            DVV04 = (lbl需求 != null) ? lbl需求.Text : "";
            DVV07 = (lbl完工日 != null) ? lbl完工日.Text : "";
            ISCLOSED = (lbl結案 != null) ? lbl結案.Text : "";

            // 備註欄位通常包含 Trim()
            COMMENTS = "";
            if (txt備註 != null)
            {
                COMMENTS = txt備註.Text.Trim();
            }

            // --- 邏輯判斷區 (修正括號層級) ---

            if (e.CommandName == "Button2")
            {
                // 保持原始 ISCLOSED
                ADD_TK_UOF_RECORDS_1002(DOC_NBR, SERNO, DVV01, DVV02, DVV03, DVV09, DVV10, DVV04, DVV07, COMMENTS, ISCLOSED);
                MsgBox(DOC_NBR + " 儲存完成", this.Page, this);
            }
            else if (e.CommandName == "Button3")
            {
                ISCLOSED = "Y";
                ADD_TK_UOF_RECORDS_1002(DOC_NBR, SERNO, DVV01, DVV02, DVV03, DVV09, DVV10, DVV04, DVV07, COMMENTS, ISCLOSED);
                MsgBox(DOC_NBR + " 已結案", this.Page, this);
            }
            else if (e.CommandName == "Button4")
            {
                ISCLOSED = "N";
                ADD_TK_UOF_RECORDS_1002(DOC_NBR, SERNO, DVV01, DVV02, DVV03, DVV09, DVV10, DVV04, DVV07, COMMENTS, ISCLOSED);
                MsgBox(DOC_NBR + " 已還原未結案", this.Page, this);
            }

            // 最後重新繫結
            BindGrid();
        }
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

    public void ADD_TK_UOF_RECORDS_1002(
        string DOC_NBR
        , string SERNO
        , string DVV01
        , string DVV02
        , string DVV03
        , string DVV09
        , string DVV10
        , string DVV04
        , string DVV07
        , string COMMENTS
        , string ISCLOSED
        )
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ERPconnectionstring"].ToString();

        string SQLCOMMAND = @" 
                            MERGE [TKRESEARCH].[dbo].[TK_UOF_RECORDS_1002] AS TARGET
                                USING (VALUES (@DOC_NBR, @SERNO, @DVV01, @DVV02, @DVV03, @DVV09, @DVV10, @DVV04, @DVV07, @COMMENTS, @ISCLOSED)) 
                                AS SOURCE (DOC_NBR, SERNO, DVV01, DVV02, DVV03, DVV09, DVV10, DVV04, DVV07, COMMENTS, ISCLOSED)
                                ON TARGET.DOC_NBR = SOURCE.DOC_NBR AND TARGET.SERNO = SOURCE.SERNO

                                WHEN MATCHED THEN 
                                    UPDATE SET 
                                        DVV01 = SOURCE.DVV01,
                                        DVV02 = SOURCE.DVV02,
                                        DVV03 = SOURCE.DVV03,
                                        DVV09 = SOURCE.DVV09,
                                        DVV10 = SOURCE.DVV10,
                                        DVV04 = SOURCE.DVV04,
                                        DVV07 = SOURCE.DVV07,
                                        COMMENTS = SOURCE.COMMENTS,
                                        ISCLOSED = SOURCE.ISCLOSED
             

                                WHEN NOT MATCHED THEN
                                    INSERT (DOC_NBR, SERNO, DVV01, DVV02, DVV03, DVV09, DVV10, DVV04, DVV07, COMMENTS, ISCLOSED)
                                    VALUES (SOURCE.DOC_NBR, SOURCE.SERNO, SOURCE.DVV01, SOURCE.DVV02, SOURCE.DVV03, SOURCE.DVV09, SOURCE.DVV10, SOURCE.DVV04, SOURCE.DVV07, SOURCE.COMMENTS, SOURCE.ISCLOSED);
                            ";


        try
        {
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(SQLCOMMAND, cnn))
                {
                    // 2. 修正參數綁定，確保每個參數對應正確的 SQL 變數名稱
                    cmd.Parameters.AddWithValue("@DOC_NBR", (object)DOC_NBR ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@SERNO", (object)SERNO ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@DVV01", (object)DVV01 ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@DVV02", (object)DVV02 ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@DVV03", (object)DVV03 ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@DVV09", (object)DVV09 ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@DVV10", (object)DVV10 ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@DVV04", (object)DVV04 ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@DVV07", (object)DVV07 ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@COMMENTS", (object)COMMENTS ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@ISCLOSED", (object)ISCLOSED ?? DBNull.Value);
              

                    cnn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected >= 1)
                    {
                        MsgBox(DOC_NBR + " 完成", this.Page, this);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // 建議至少記錄錯誤，方便除錯
            // Log(ex.Message); 
            throw;
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