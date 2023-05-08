﻿using System;
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

public partial class CDS_WebPage_RESEARCH_TK_UOF_DESIGN_1002 : Ede.Uof.Utility.Page.BasePage
{
    string RowIndex = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindDropDownList();
            BindGrid1("");            
        }
        else
        {

        }

    }
    #region FUNCTION

    private void BindDropDownList()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("PARAID", typeof(String));


        string connectionString = ConfigurationManager.ConnectionStrings["ERPconnectionstring"].ToString();
        Ede.Uof.Utility.Data.DatabaseHelper m_db = new Ede.Uof.Utility.Data.DatabaseHelper(connectionString);

        string cmdTxt = @"    SELECT 
                                 [ID]
                                ,[KIND]
                                ,[PARAID]
                                ,[PARANAME]
                                FROM [TKRESEARCH].[dbo].[TBPARA]
                                WHERE [KIND]='TK_UOF_RESEARCH_1002'
                                ORDER BY [ID] ";

        dt.Load(m_db.ExecuteReader(cmdTxt));

        if (dt.Rows.Count > 0)
        {
            DropDownList1.DataSource = dt;
            DropDownList1.DataTextField = "PARAID";
            DropDownList1.DataValueField = "PARAID";
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
        StringBuilder QUERYS2 = new StringBuilder();


        //查詢條件
        if (!string.IsNullOrEmpty(TextBox1.Text))
        {
            QUERYS.AppendFormat(@" AND [FIELDS10] LIKE '%{0}%' ", TextBox1.Text);
        }
        else
        {
            QUERYS.AppendFormat(@" ");
        }
        if (!string.IsNullOrEmpty(DropDownList1.SelectedValue.ToString()))
        {
            QUERYS2.AppendFormat(@" AND [ISCLOSED]='{0}' ", DropDownList1.SelectedValue.ToString());
        }
        else
        {
            QUERYS2.AppendFormat(@" ");
        }


        cmdTxt.AppendFormat(@" 
                            SELECT
                            [FIELDS1] AS '表單編號'
                            ,[FIELDS2] AS '類別'
                            ,[FIELDS3] AS '填表日期'
                            ,[FIELDS4] AS '生產部代簽部門'
                            ,[FIELDS5] AS '交付人'
                            ,[FIELDS6] AS '交付部門'
                            ,[FIELDS7] AS '交付者職級'
                            ,[FIELDS8] AS '接辦人員'
                            ,[FIELDS9] AS '期望交期'
                            ,[FIELDS10] AS '簡述交辦事項'
                            ,[FIELDS11] AS '交辦說明'
                            ,[FIELDS12] AS '接辦人處理項目描述'
                            ,[FIELDS13] AS '完成交辦文件'
                            ,[INPROCESSING] AS '處理進度'
                            ,[ISCLOSED] AS '是否結案'
                            FROM [TKRESEARCH].[dbo].[TK_UOF_DESIGN_1002]
                            WHERE 1=1
                            {0}
                            {1}
                            ORDER BY  [FIELDS1]
                               
                                ", QUERYS.ToString(), QUERYS2.ToString());


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

    protected void Grid1_OnRowUpdating(object sender, GridViewUpdateEventArgs e)
    { 
        RowIndex = e.RowIndex.ToString();
    }
    protected void Grid1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Button1")
        {
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = Grid1.Rows[rowIndex];
            // 接下來可以從行中取得 TextBox 控制項的值
            //TextBox TextBoxFIELDS1 = (TextBox)row.FindControl("FIELDS1");
            //string FIELDS1 = TextBoxFIELDS1.Text;


            TextBox TextBoxGRIDVIEWTextBox1 = (TextBox)row.FindControl("GRIDVIEWTextBox1");
            string INPROCESSING = TextBoxGRIDVIEWTextBox1.Text;
            DropDownList DropDownListGRIDVIEWDropDownList1=(DropDownList)row.FindControl("GRIDVIEWDropDownList1");
            string ISCLOSED = DropDownListGRIDVIEWDropDownList1.SelectedValue.ToString();

            Label LabelFIELDS1=(Label)row.FindControl("LabelFIELDS1");
            string FIELDS1 = LabelFIELDS1.Text;

            //更新UPDATE_TK_UOF_DESIGN_1002
            if(!string.IsNullOrEmpty(INPROCESSING))
            {
                UPDATE_TK_UOF_DESIGN_1002(FIELDS1, INPROCESSING, ISCLOSED);

                BindGrid1("");
            }
            else
            {
                MsgBox("表單: " + FIELDS1 + "\r\n" + "未填寫處理進度，不允許更新 ", this.Page, this);
            }
            
            //MsgBox(e.CommandArgument.ToString() + "\r\n  "+ " INPROCESSING: " + INPROCESSING + "\r\n  "  + " ISCLOSED: " + ISCLOSED + "\r\n  " + " FIELDS1: " + FIELDS1, this.Page, this);
        }
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

        //建議售價
        if (!string.IsNullOrEmpty(TextBox1.Text))
        {
            QUERYS.AppendFormat(@" AND DOC_NBR LIKE '%{0}%'", TextBox1.Text);
        }


        cmdTxt.AppendFormat(@" 
                            SELECT
                            usr2.NAME
                            ,(CASE WHEN  usr.IS_SUSPENDED = 1 THEN  usr.NAME + '(x)' WHEN  ISNULL(usr.ACCOUNT,'''') = '' THEN  'unknown user' ELSE usr.NAME END) AS APPLICANT_NAME
                            ,form.FORM_NAME
                            ,DOC_NBR
                            ,CONVERT(NVARCHAR,NODES.START_TIME,111) AS 'START_TIME'
                            ,DATEDIFF(HOUR,START_TIME,GETDATE()) AS 'HRS'
                            ,CONVERT(NVARCHAR,BEGIN_TIME,111) AS BEGIN_TIME

                            ,task.TASK_ID
                            ,END_TIME
                            ,TASK_RESULT
                            ,TASK_STATUS
                            ,task.USER_GUID
                            ,formVer.FORM_VERSION_ID
                            ,formVer.FORM_ID
                            ,CURRENT_SITE_ID
                            ,MESSAGE_CONTENT
                            ,LOCK_STATUS
                            ,ISNULL(formVer.DISPLAY_TITLE,'') AS VERSION_TITLE
                            ,ISNULL(task.JSON_DISPLAY,'') AS JSON_DISPLAY

                            FROM [UOF].dbo.TB_WKF_TASK task
                            INNER JOIN [UOF].dbo.TB_WKF_FORM_VERSION formVer ON task.FORM_VERSION_ID = formVer.FORM_VERSION_ID
                            INNER JOIN [UOF].dbo.TB_WKF_FORM form  ON  formVer.FORM_ID = form.FORM_ID 
                            LEFT JOIN [UOF].dbo.TB_EB_USER [usr]  ON task.USER_GUID = usr.USER_GUID
                            LEFT JOIN [UOF].dbo.TB_WKF_TASK_NODE [NODES] ON NODES.SITE_ID=task.CURRENT_SITE_ID
                            LEFT JOIN [UOF].dbo.TB_EB_USER [usr2]  ON NODES.ORIGINAL_SIGNER = [usr2].USER_GUID
                            WHERE
                            1=1  AND  TASK_STATUS NOT IN ('2')
                                {0}
                            ORDER BY HRS DESC,usr2.NAME,form.FORM_NAME,DOC_NBR
                               
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
                ws.Cells[1, 2].Value = "申請者";
                ws.Cells[1, 2].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center; //欄位置中
                ws.Cells[1, 2].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center; //高度置中
                ws.Cells[1, 2].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin); //儲存格框線
                ws.Cells[1, 3].Value = "表單";
                ws.Cells[1, 3].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center; //欄位置中
                ws.Cells[1, 3].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center; //高度置中
                ws.Cells[1, 3].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin); //儲存格框線
                ws.Cells[1, 4].Value = "表單編號";
                ws.Cells[1, 4].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center; //欄位置中
                ws.Cells[1, 4].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center; //高度置中
                ws.Cells[1, 4].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin); //儲存格框線
                ws.Cells[1, 5].Value = "送簽核者時間";
                ws.Cells[1, 5].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center; //欄位置中
                ws.Cells[1, 5].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center; //高度置中
                ws.Cells[1, 5].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin); //儲存格框線
                ws.Cells[1, 6].Value = "停留時間(小時)";
                ws.Cells[1, 6].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center; //欄位置中
                ws.Cells[1, 6].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center; //高度置中
                ws.Cells[1, 6].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin); //儲存格框線
                ws.Cells[1, 7].Value = "申請時間";
                ws.Cells[1, 7].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center; //欄位置中
                ws.Cells[1, 7].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center; //高度置中
                ws.Cells[1, 7].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin); //儲存格框線
               

                foreach (DataRow od in dt.Rows)
                {
                    ws.Cells[ROWS, 1].Value = od["NAME"].ToString();
                    ws.Cells[ROWS, 1].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center; //高度置中
                    ws.Cells[ROWS, 1].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin); //儲存格框線
                    ws.Cells[ROWS, 2].Value = od["APPLICANT_NAME"].ToString();
                    ws.Cells[ROWS, 2].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center; //高度置中
                    ws.Cells[ROWS, 2].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin); //儲存格框線
                    ws.Cells[ROWS, 3].Value = od["FORM_NAME"].ToString();
                    ws.Cells[ROWS, 3].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center; //高度置中
                    ws.Cells[ROWS, 3].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin); //儲存格框線
                    ws.Cells[ROWS, 4].Value = od["DOC_NBR"].ToString();
                    ws.Cells[ROWS, 4].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center; //高度置中
                    ws.Cells[ROWS, 4].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin); //儲存格框線
                    ws.Cells[ROWS, 5].Value = od["START_TIME"].ToString();
                    ws.Cells[ROWS, 5].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center; //高度置中
                    ws.Cells[ROWS, 5].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin); //儲存格框線
                    ws.Cells[ROWS, 6].Value = od["HRS"].ToString();
                    ws.Cells[ROWS, 6].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center; //高度置中
                    ws.Cells[ROWS, 6].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin); //儲存格框線
                    ws.Cells[ROWS, 7].Value = od["BEGIN_TIME"].ToString();
                    ws.Cells[ROWS, 7].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center; //高度置中
                    ws.Cells[ROWS, 7].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin); //儲存格框線
                    

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

    public void MsgBox(String ex, Page pg, Object obj)
    {
        string s = "<SCRIPT language='javascript'>alert('" + ex.Replace("\r\n", "\\n").Replace("'", "") + "'); </SCRIPT>";
        Type cstype = obj.GetType();
        ClientScriptManager cs = pg.ClientScript;
        cs.RegisterClientScriptBlock(cstype, s, s.ToString());
    }

    public void UPDATE_TK_UOF_DESIGN_1002(string FIELDS1,string INPROCESSING ,string ISCLOSED)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ERPconnectionstring"].ToString();

        StringBuilder queryString = new StringBuilder();
        queryString.AppendFormat(@"   
                                    UPDATE [TKRESEARCH].[dbo].[TK_UOF_DESIGN_1002]
                                    SET INPROCESSING=@INPROCESSING,ISCLOSED=@ISCLOSED
                                    WHERE FIELDS1=@FIELDS1
                                        ");

        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                SqlCommand command = new SqlCommand(queryString.ToString(), connection);
                command.Parameters.Add("@FIELDS1", SqlDbType.NVarChar).Value = FIELDS1;
                command.Parameters.Add("@INPROCESSING", SqlDbType.NVarChar).Value = INPROCESSING;
                command.Parameters.Add("@ISCLOSED", SqlDbType.NVarChar).Value = ISCLOSED;


                command.Connection.Open();

                int count = command.ExecuteNonQuery();

                connection.Close();
                connection.Dispose();

            }
        }
        catch
        {

        }
        finally
        {

        }
    }


    #endregion

    #region BUTTON
    protected void btn1_Click(object sender, EventArgs e)
    {
        BindGrid1("");

    }


   
    #endregion
}