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
using Ede.Uof.Utility.Data;
using Ede.Uof.Utility.Page.Common;
using OfficeOpenXml;
using OfficeOpenXml.Drawing;
using OfficeOpenXml.Style;
using System.Globalization;

public partial class CDS_WebPage_IT_TKITWEEKSREPORTS : Ede.Uof.Utility.Page.BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SETYEARSWEEKS();
            BindDropDownList();

            BindGrid1("");
           
        }
        else
        {


        }

       


    }
    #region FUNCTION
    public void SETYEARSWEEKS()
    {
        //計算日期為第幾週
        System.Globalization.Calendar TW = new System.Globalization.CultureInfo("zh-TW").Calendar;


        TextBox1.Text = DateTime.Now.Year.ToString();
        TextBox2.Text = TW.GetWeekOfYear(DateTime.Now, System.Globalization.CalendarWeekRule.FirstDay, DayOfWeek.Sunday).ToString();
        TextBox3.Text = DateTime.Now.Year.ToString();
        TextBox4.Text = TW.GetWeekOfYear(DateTime.Now, System.Globalization.CalendarWeekRule.FirstDay, DayOfWeek.Sunday).ToString();

    }
    private void BindDropDownList()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("ID", typeof(String));
        dt.Columns.Add("NAMES", typeof(String));

        string connectionString = ConfigurationManager.ConnectionStrings["ERPconnectionstring"].ToString();
        Ede.Uof.Utility.Data.DatabaseHelper m_db = new Ede.Uof.Utility.Data.DatabaseHelper(connectionString);

        string cmdTxt = @"SELECT [ID] ,[NAMES]  FROM [TKIT].[dbo].[ITWEEKSREPORTSNAMES] ORDER BY ID ";

        dt.Load(m_db.ExecuteReader(cmdTxt));

        if (dt.Rows.Count > 0)
        {
            DropDownList1.DataSource = dt;
            DropDownList1.DataTextField = "NAMES";
            DropDownList1.DataValueField = "NAMES";
            DropDownList1.DataBind();

        }
        else
        {

        }



    }

    private void BindGrid1(string SALESFOCUS)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ERPconnectionstring"].ToString();
        Ede.Uof.Utility.Data.DatabaseHelper m_db = new Ede.Uof.Utility.Data.DatabaseHelper(connectionString);

        StringBuilder cmdTxt = new StringBuilder();
        StringBuilder QUERYS = new StringBuilder();

      

   
        //查詢條件
        if (!string.IsNullOrEmpty(TextBox1.Text))
        {
            QUERYS.AppendFormat(@" AND [WYEARS]='{0}'", TextBox1.Text);
        }
        if (!string.IsNullOrEmpty(TextBox2.Text))
        {
            QUERYS.AppendFormat(@" AND [WEEKS]='{0}' ", TextBox2.Text);
        }


        cmdTxt.AppendFormat(@" 
                            SELECT 
                            [ID]
                            ,[NAMES]
                            ,[WYEARS]
                            ,[WEEKS]
                            ,CONVERT(NVARCHAR,[SDATES],111) AS SDATES
                            ,CONVERT(NVARCHAR,[EDATES],111) AS EDATES
                            ,[COMMENTS]
                            FROM [TKIT].[dbo].[ITWEEKSREPORTS]
                            WHERE  1=1
                            {0}

                            ORDER BY [NAMES],[WYEARS],[WEEKS]
                               
                                ", QUERYS.ToString());

       


        //m_db.AddParameter("@SDATE", SDATE);
        //m_db.AddParameter("@EDATE", EDATE);

        DataTable dt = new DataTable();

        dt.Load(m_db.ExecuteReader(cmdTxt.ToString()));

        Grid1.DataSource = dt;
        Grid1.DataBind();
    }

  


    protected void grid1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //Grid1.PageIndex = e.NewPageIndex;
        //BindGrid();
    }
    protected void Grid1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        


    }

   

    public void OnBeforeExport1(object sender, Ede.Uof.Utility.Component.BeforeExportEventArgs e)
    {

        SETEXCEL();

        //string connectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
        //Ede.Uof.Utility.Data.DatabaseHelper m_db = new Ede.Uof.Utility.Data.DatabaseHelper(connectionString);

        //StringBuilder cmdTxt = new StringBuilder();
        //StringBuilder QUERYS = new StringBuilder();

        ////建議售價
        //if (!string.IsNullOrEmpty(TextBox1.Text))
        //{
        //    QUERYS.AppendFormat(@" AND DOC_NBR LIKE '%{0}%'", TextBox1.Text);
        //}


        //cmdTxt.AppendFormat(@" 
        //                    SELECT
        //                    usr2.NAME
        //                    ,(CASE WHEN  usr.IS_SUSPENDED = 1 THEN  usr.NAME + '(x)' WHEN  ISNULL(usr.ACCOUNT,'''') = '' THEN  'unknown user' ELSE usr.NAME END) AS APPLICANT_NAM
        //                    ,form.FORM_NAME
        //                    ,DOC_NBR
        //                    ,CONVERT(NVARCHAR,NODES.START_TIME,111) AS 'START_TIME'
        //                    ,DATEDIFF(HOUR,START_TIME,GETDATE()) AS 'HRS'
        //                    ,CONVERT(NVARCHAR,BEGIN_TIME,111) AS BEGIN_TIME

        //                    ,task.TASK_ID
        //                    ,END_TIME
        //                    ,TASK_RESULT
        //                    ,TASK_STATUS
        //                    ,task.USER_GUID
        //                    ,formVer.FORM_VERSION_ID
        //                    ,formVer.FORM_ID
        //                    ,CURRENT_SITE_ID
        //                    ,MESSAGE_CONTENT
        //                    ,LOCK_STATUS
        //                    ,ISNULL(formVer.DISPLAY_TITLE,'') AS VERSION_TITLE
        //                    ,ISNULL(task.JSON_DISPLAY,'') AS JSON_DISPLAY

        //                    FROM [UOF].dbo.TB_WKF_TASK task
        //                    INNER JOIN [UOF].dbo.TB_WKF_FORM_VERSION formVer ON task.FORM_VERSION_ID = formVer.FORM_VERSION_ID
        //                    INNER JOIN [UOF].dbo.TB_WKF_FORM form  ON  formVer.FORM_ID = form.FORM_ID 
        //                    LEFT JOIN [UOF].dbo.TB_EB_USER [usr]  ON task.USER_GUID = usr.USER_GUID
        //                    LEFT JOIN [UOF].dbo.TB_WKF_TASK_NODE [NODES] ON NODES.SITE_ID=task.CURRENT_SITE_ID
        //                    LEFT JOIN [UOF].dbo.TB_EB_USER [usr2]  ON NODES.ORIGINAL_SIGNER = [usr2].USER_GUID
        //                    WHERE
        //                    1=1  AND  TASK_STATUS NOT IN ('2')
        //                        {0}
        //                    ORDER BY HRS DESC,usr2.NAME,form.FORM_NAME,DOC_NBR
                               
        //                        ", QUERYS.ToString());



        //DataTable dt = new DataTable();

        //dt.Load(m_db.ExecuteReader(cmdTxt.ToString()));

        //if (dt.Rows.Count > 0)
        //{
        //    dt.Columns[0].Caption = "目前簽核者";
        //    dt.Columns[1].Caption = "申請者";
        //    dt.Columns[2].Caption = "表單";
        //    dt.Columns[3].Caption = "表單編號";
        //    dt.Columns[4].Caption = "送簽核者時間";
        //    dt.Columns[5].Caption = "停留時間(小時)";
        //    dt.Columns[6].Caption = "申請時間";



        //    e.Datasource = dt;
        //}
    }

    public void SETEXCEL()
    {
        string connectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
        Ede.Uof.Utility.Data.DatabaseHelper m_db = new Ede.Uof.Utility.Data.DatabaseHelper(connectionString);

        StringBuilder cmdTxt = new StringBuilder();
        StringBuilder QUERYS = new StringBuilder();

        //
        if (!string.IsNullOrEmpty(TextBox1.Text))
        {
            QUERYS.AppendFormat(@"", TextBox1.Text);
        }


        cmdTxt.AppendFormat(@" 
                            
                               
                                ", QUERYS.ToString());

        

        DataTable dt = new DataTable();

        dt.Load(m_db.ExecuteReader(cmdTxt.ToString()));

        if (dt.Rows.Count > 0)
        {
            //檔案名稱
            var fileName = "未結案表單" + DateTime.Now.ToString("yyyy-MM-dd--hh-mm-ss") + ".xlsx";
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial; // 關閉新許可模式通知

            using (var excel = new ExcelPackage(new FileInfo(fileName)))
            {

                // 建立分頁
                var ws = excel.Workbook.Worksheets.Add("list" + DateTime.Now.ToShortDateString());


                //預設行高
                //ws.DefaultRowHeight = 60;

                // 寫入資料試試
                //ws.Cells[2, 1].Value = "測試測試";
                int ROWS = 2;
                int COLUMNS = 1;


                //excel標題
                ws.Cells[1, 1].Value = "目前簽核者";
                ws.Cells[1, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center; //欄位置中
                ws.Cells[1, 1].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center; //高度置中
                ws.Cells[1, 1].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin); //儲存格框線
             

                foreach (DataRow od in dt.Rows)
                {
                    ws.Cells[ROWS, 1].Value = od["NAME"].ToString();
                    ws.Cells[ROWS, 1].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center; //高度置中
                    ws.Cells[ROWS, 1].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin); //儲存格框線
                   

                    ROWS++;
                }




                ////預設列寬、行高
                //sheet.DefaultColWidth = 10; //預設列寬
                //sheet.DefaultRowHeight = 30; //預設行高

                //// 遇\n或(char)10自動斷行
                //ws.Cells.Style.WrapText = true;

                //自適應寬度設定
                ws.Cells[ws.Dimension.Address].AutoFitColumns();

                //自適應高度設定
                ws.Row(1).CustomHeight = true;



                //儲存Excel
                //Byte[] bin = excel.GetAsByteArray();
                //File.WriteAllBytes(@"C:\TEMP\" + fileName, bin);

                //儲存和歸來的Excel檔案作為一個ByteArray
                var data = excel.GetAsByteArray();
                HttpResponse response = HttpContext.Current.Response;
                Response.Clear();

                //輸出標頭檔案　　
                Response.AddHeader("content-disposition", "attachment;  filename=" + fileName + "");
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.BinaryWrite(data);
                Response.Flush();
                Response.End();
                //package.Save();//這個方法是直接下載到本地
            }
            //ExcelPackage.LicenseContext = LicenseContext.NonCommercial; // 關閉新許可模式通知
            //                                                            // 沒設置的話會跳出 Please set the excelpackage.licensecontext property


            ////var file = new FileInfo(fileName);
            //using (var excel = new ExcelPackage(file))
            //{

            //}
        }

    }

    public void OnBeforeExport2(object sender, Ede.Uof.Utility.Component.BeforeExportEventArgs e)
    {

        //SETEXCEL();

    }
    #endregion

    #region BUTTON

    protected void btn1_Click(object sender, EventArgs e)
    {
        BindGrid1("");
    }

    protected void btn2_Click(object sender, EventArgs e)
    {
     
    }


    #endregion
}