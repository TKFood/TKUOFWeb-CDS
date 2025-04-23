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

public partial class CDS_WebPage_QC_TBUOFQC1002TRACES : Ede.Uof.Utility.Page.BasePage
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
                            WITH TEMP AS (
                            SELECT 
                            [TB_WKF_FORM].[FORM_NAME]
                            ,TB_WKF_TASK.[DOC_NBR]
                            ,[CURRENT_DOC].value('(/Form/FormFieldValue/FieldItem[@fieldId=""QCFrm002Date""]/@fieldValue)[1]', 'NVARCHAR(100)') AS QCFrm002Date
                            ,[CURRENT_DOC].value('(/Form/FormFieldValue/FieldItem[@fieldId=""QCFrm002PRD""]/@fieldValue)[1]', 'NVARCHAR(100)') AS QCFrm002PRD
                            ,[CURRENT_DOC].value('(/Form/FormFieldValue/FieldItem[@fieldId=""QCFrm002Abns""]/@fieldValue)[1]', 'NVARCHAR(100)') AS QCFrm002Abns
                            ,[CURRENT_DOC].value('(/Form/FormFieldValue/FieldItem[@fieldId=""QCFrm002Abns""]/@customValue)[1]', 'NVARCHAR(100)') AS QCFrm002AbnscustomValue
                            , TB_WKF_TASK.TASK_ID
                            , (CASE WHEN TASK_STATUS = '1' THEN '未簽核' ELSE '已簽核' END) TASK_STATUS
                            ,TASK_RESULT
                            ,[TBUOFQC1002TRACES].[IMPROVES]
                                FROM[UOF].[dbo].TB_WKF_TASK
                            LEFT JOIN[UOF].[dbo].[TB_WKF_FORM_VERSION] ON[TB_WKF_FORM_VERSION].FORM_VERSION_ID = TB_WKF_TASK.FORM_VERSION_ID
                            LEFT JOIN[UOF].[dbo].[TB_WKF_FORM] ON[TB_WKF_FORM].FORM_ID = [TB_WKF_FORM_VERSION].FORM_ID
                            LEFT JOIN[192.168.1.105].[TKQC].[dbo].[TBUOFQC1002TRACES] ON[TBUOFQC1002TRACES].[DOC_NBR]=TB_WKF_TASK.[DOC_NBR] COLLATE Chinese_Taiwan_Stroke_CI_AS
                            WHERE[FORM_NAME] = '1002.客訴異常處理單'
                            AND ISNULL(TASK_RESULT,'') NOT IN('1','2')

                            )
                            SELECT TEMP.*
                            FROM TEMP
                            WHERE 1=1
                            AND[DOC_NBR]>='QC1002250100001'
                            AND QCFrm002Date>='2025/04/01'
                            ORDER BY QCFrm002Date

                             ");




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
        ////BindGrid中已帶入EXCELDT1
        //if (EXCELDT1.Rows.Count >= 1)
        //{
        //    //檔案名稱
        //    var fileName = "明細" + DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss") + ".xlsx";
        //    ExcelPackage.LicenseContext = LicenseContext.NonCommercial; // 關閉新許可模式通知

        //    using (var excel = new ExcelPackage(new FileInfo(fileName)))
        //    {

        //        // 建立分頁
        //        var ws = excel.Workbook.Worksheets.Add("list" + DateTime.Now.ToShortDateString());


        //        //預設行高
        //        //ws.DefaultRowHeight = 60;

        //        // 寫入資料試試
        //        //ws.Cells[2, 1].Value = "測試測試";
        //        int ROWS = 2;
        //        int COLUMNS = 1;


        //        //excel標題
        //        ws.Cells[1, 1].Value = "專案編號";
        //        ws.Cells[1, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center; //欄位置中
        //        ws.Cells[1, 1].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center; //高度置中
        //        ws.Cells[1, 1].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin); //儲存格框線
        //        ws.Cells[1, 2].Value = "項目名稱";
        //        ws.Cells[1, 2].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center; //欄位置中
        //        ws.Cells[1, 2].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center; //高度置中
        //        ws.Cells[1, 2].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin); //儲存格框線
        //        ws.Cells[1, 3].Value = "產品打樣日";
        //        ws.Cells[1, 3].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center; //欄位置中
        //        ws.Cells[1, 3].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center; //高度置中
        //        ws.Cells[1, 3].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin); //儲存格框線
        //        ws.Cells[1, 4].Value = "產品試吃日";
        //        ws.Cells[1, 4].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center; //欄位置中
        //        ws.Cells[1, 4].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center; //高度置中
        //        ws.Cells[1, 4].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin); //儲存格框線
        //        ws.Cells[1, 5].Value = "包裝設計日";
        //        ws.Cells[1, 5].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center; //欄位置中
        //        ws.Cells[1, 5].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center; //高度置中
        //        ws.Cells[1, 5].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin); //儲存格框線
        //        ws.Cells[1, 6].Value = "上市日";
        //        ws.Cells[1, 6].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center; //欄位置中
        //        ws.Cells[1, 6].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center; //高度置中
        //        ws.Cells[1, 6].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin); //儲存格框線
        //        ws.Cells[1, 7].Value = "專案負責人";
        //        ws.Cells[1, 7].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center; //欄位置中
        //        ws.Cells[1, 7].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center; //高度置中
        //        ws.Cells[1, 7].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin); //儲存格框線
        //        ws.Cells[1, 8].Value = "狀態";
        //        ws.Cells[1, 8].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center; //欄位置中
        //        ws.Cells[1, 8].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center; //高度置中
        //        ws.Cells[1, 8].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin); //儲存格框線
        //        ws.Cells[1, 9].Value = "是否結案";
        //        ws.Cells[1, 9].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center; //欄位置中
        //        ws.Cells[1, 9].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center; //高度置中
        //        ws.Cells[1, 9].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin); //儲存格框線
        //        ws.Cells[1, 10].Value = "更新日";
        //        ws.Cells[1, 10].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left; //欄位置中
        //        ws.Cells[1, 10].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center; //高度置中
        //        ws.Cells[1, 10].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin); //儲存格框線
        //        ws.Cells[1, 11].Value = "ID";
        //        ws.Cells[1, 11].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left; //欄位置中
        //        ws.Cells[1, 11].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center; //高度置中
        //        ws.Cells[1, 11].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin); //儲存格框線 
        //        ws.Cells[1, 12].Value = "表單編號";
        //        ws.Cells[1, 12].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left; //欄位置中
        //        ws.Cells[1, 12].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center; //高度置中
        //        ws.Cells[1, 12].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin); //儲存格框線  w

        //        foreach (DataRow od in EXCELDT1.Rows)
        //        {
        //            ws.Cells[ROWS, 1].Value = od["專案編號"].ToString();
        //            ws.Cells[ROWS, 1].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center; //高度置中
        //            ws.Cells[ROWS, 1].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin); //儲存格框線
        //            ws.Cells[ROWS, 2].Value = od["項目名稱"].ToString();
        //            ws.Cells[ROWS, 2].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center; //高度置中
        //            ws.Cells[ROWS, 2].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin); //儲存格框線
        //            ws.Cells[ROWS, 3].Value = od["產品打樣日"].ToString();
        //            ws.Cells[ROWS, 3].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center; //高度置中
        //            ws.Cells[ROWS, 3].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin); //儲存格框線
        //            ws.Cells[ROWS, 4].Value = od["產品試吃日"].ToString();
        //            ws.Cells[ROWS, 4].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center; //高度置中
        //            ws.Cells[ROWS, 4].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin); //儲存格框線
        //            ws.Cells[ROWS, 5].Value = od["包裝設計日"].ToString();
        //            ws.Cells[ROWS, 5].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center; //高度置中
        //            ws.Cells[ROWS, 5].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin); //儲存格框線
        //            ws.Cells[ROWS, 6].Value = od["上市日"].ToString();
        //            ws.Cells[ROWS, 6].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center; //高度置中
        //            ws.Cells[ROWS, 6].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin); //儲存格框線
        //            ws.Cells[ROWS, 7].Value = od["專案負責人"].ToString();
        //            ws.Cells[ROWS, 7].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center; //高度置中
        //            ws.Cells[ROWS, 7].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin); //儲存格框線
        //            ws.Cells[ROWS, 8].Value = od["狀態"].ToString();
        //            ws.Cells[ROWS, 8].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center; //高度置中
        //            ws.Cells[ROWS, 8].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin); //儲存格框線
        //            ws.Cells[ROWS, 9].Value = od["是否結案"].ToString();
        //            ws.Cells[ROWS, 9].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center; //高度置中
        //            ws.Cells[ROWS, 9].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin); //儲存格框線
        //            ws.Cells[ROWS, 10].Value = od["更新日"].ToString();
        //            ws.Cells[ROWS, 10].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center; //高度置中
        //            ws.Cells[ROWS, 10].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin); //儲存格框線
        //            ws.Cells[ROWS, 11].Value = od["ID"].ToString();
        //            ws.Cells[ROWS, 11].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center; //高度置中
        //            ws.Cells[ROWS, 11].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin); //儲存格框線
        //            ws.Cells[ROWS, 12].Value = od["表單編號"].ToString();
        //            ws.Cells[ROWS, 12].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center; //高度置中
        //            ws.Cells[ROWS, 12].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin); //儲存格框線


        //            ROWS++;
        //        }




        //        ////預設列寬、行高
        //        //sheet.DefaultColWidth = 10; //預設列寬
        //        //sheet.DefaultRowHeight = 30; //預設行高

        //        //// 遇\n或(char)10自動斷行
        //        //ws.Cells.Style.WrapText = true;

        //        //自適應寬度設定
        //        ws.Cells[ws.Dimension.Address].AutoFitColumns();

        //        //自適應高度設定
        //        ws.Row(1).CustomHeight = true;



        //        //儲存Excel
        //        //Byte[] bin = excel.GetAsByteArray();
        //        //File.WriteAllBytes(@"C:\TEMP\" + fileName, bin);

        //        //儲存和歸來的Excel檔案作為一個ByteArray
        //        var data = excel.GetAsByteArray();
        //        HttpResponse response = HttpContext.Current.Response;
        //        Response.Clear();

        //        //輸出標頭檔案　　
        //        Response.AddHeader("content-disposition", "attachment;  filename=" + fileName + "");
        //        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        //        Response.BinaryWrite(data);
        //        Response.Flush();
        //        Response.End();
        //        //package.Save();//這個方法是直接下載到本地
        //    }
        //}
    }

    public void ADD_TBUOFQC1002TRACES(
        string DOC_NBR,
        string QCFrm002Date,
        string QCFrm002PRD,
        string QCFrm002Abns,
        string QCFrm002AbnscustomValue,
        string TASK_ID,
        string IMPROVES
        )
    {

        string connectionString = ConfigurationManager.ConnectionStrings["ERPconnectionstring"].ToString();

        var SQLCOMMAND = @" 
                        MERGE [TKQC].[dbo].[TBUOFQC1002TRACES] AS TARGET

                        USING (VALUES (@DOC_NBR, @QCFrm002Date, @QCFrm002PRD, @QCFrm002Abns, @QCFrm002AbnscustomValue, @TASK_ID, @IMPROVES)) 
                        AS SOURCE (DOC_NBR, QCFrm002Date, QCFrm002PRD, QCFrm002Abns, QCFrm002AbnscustomValue, TASK_ID, IMPROVES)
                        ON TARGET.DOC_NBR = SOURCE.DOC_NBR

                        WHEN MATCHED THEN 
                            UPDATE SET 
                                QCFrm002Date = SOURCE.QCFrm002Date,
                                QCFrm002PRD = SOURCE.QCFrm002PRD,
                                QCFrm002Abns = SOURCE.QCFrm002Abns,
                                QCFrm002AbnscustomValue = SOURCE.QCFrm002AbnscustomValue,
                                TASK_ID = SOURCE.TASK_ID,
                                IMPROVES = SOURCE.IMPROVES
                        WHEN NOT MATCHED THEN
                            INSERT (DOC_NBR, QCFrm002Date, QCFrm002PRD, QCFrm002Abns, QCFrm002AbnscustomValue, TASK_ID, IMPROVES)
                            VALUES (SOURCE.DOC_NBR, SOURCE.QCFrm002Date, SOURCE.QCFrm002PRD, SOURCE.QCFrm002Abns, SOURCE.QCFrm002AbnscustomValue, SOURCE.TASK_ID, SOURCE.IMPROVES);                                              
                            ";

        try
        {
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(SQLCOMMAND, cnn))
                {
                    // 傳入參數
                    cmd.Parameters.AddWithValue("@DOC_NBR", DOC_NBR);
                    cmd.Parameters.AddWithValue("@QCFrm002Date", QCFrm002Date);
                    cmd.Parameters.AddWithValue("@QCFrm002PRD", QCFrm002PRD);
                    cmd.Parameters.AddWithValue("@QCFrm002Abns", QCFrm002Abns);
                    cmd.Parameters.AddWithValue("@QCFrm002AbnscustomValue", QCFrm002AbnscustomValue);
                    cmd.Parameters.AddWithValue("@TASK_ID", TASK_ID);
                    cmd.Parameters.AddWithValue("@IMPROVES", IMPROVES);

                    cnn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected >= 1)
                    {
                        MsgBox(DOC_NBR + " 完成", this.Page, this);
                    }
                }
            }
        }
        catch
        {
        }
        finally
        {
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