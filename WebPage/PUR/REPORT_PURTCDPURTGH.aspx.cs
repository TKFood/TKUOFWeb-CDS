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

public partial class CDS_WebPage_REPORT_PURTCDPURTGH : Ede.Uof.Utility.Page.BasePage
{
    string CHECK_column1Value = "";
    string column1Value = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           
            BindGrid("");
        }
        else
        {

          

            //if (ViewState["TextBox1"] != null)
            //{
            //    TextBox1.Text = ViewState["TextBox1"].ToString();

            //}


            //if (this.Session["STATUS"] != null)
            //{
            //    DropDownList1.SelectedItem.Text = this.Session["STATUS"].ToString();

            //}

        }

       


    }
    #region FUNCTION
    private void BindDropDownList()
    {
        //DataTable dt = new DataTable();
        //dt.Columns.Add("SALESFOCUS", typeof(String));


        //string connectionString = ConfigurationManager.ConnectionStrings["ERPconnectionstring"].ToString();
        //Ede.Uof.Utility.Data.DatabaseHelper m_db = new Ede.Uof.Utility.Data.DatabaseHelper(connectionString);

        //string cmdTxt = @" SELECT '全部' AS SALESFOCUS UNION ALL SELECT SALESFOCUS  FROM  [TKBUSINESS].[dbo].[PRODUCTS]  GROUP BY SALESFOCUS ";

        //dt.Load(m_db.ExecuteReader(cmdTxt));

        //if (dt.Rows.Count > 0)
        //{
        //    DropDownList1.DataSource = dt;
        //    DropDownList1.DataTextField = "SALESFOCUS";
        //    DropDownList1.DataValueField = "SALESFOCUS";
        //    DropDownList1.DataBind();

        //}
        //else
        //{

        //}



    }
    private void BindGrid(string SALESFOCUS)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ERPconnectionstring"].ToString();
        Ede.Uof.Utility.Data.DatabaseHelper m_db = new Ede.Uof.Utility.Data.DatabaseHelper(connectionString);

        StringBuilder cmdTxt = new StringBuilder();
        StringBuilder QUERYS = new StringBuilder();



        //TextBox1
        if (!string.IsNullOrEmpty(TextBox1.Text))
        {
            QUERYS.AppendFormat(@" AND (MB001 LIKE '%'+@MB001+'%' OR MB002 LIKE '%'+@MB001+'%')");
        }


        if (!string.IsNullOrEmpty(TextBox1.Text))
        {
            cmdTxt.AppendFormat(@"
                                SELECT 
                                TC004 AS '供應廠商'
                                ,MA002 AS '廠商'
                                ,TC003 AS '採購日'
                                ,TC001 AS '採購單別'
                                ,TC002 AS '採購單號'
                                ,TD003 AS '採購序號'
                                ,TD004 AS '品號'
                                ,MB002 AS '品名'
                                ,TD008 AS '請購數量'
                                ,TD009 AS '請購單位'
                                ,TD012 AS '預交日'
                                ,TD015 AS '已交數量'
                                ,TD010 AS '請購單價'
                                ,TD011 AS '請購金額'
                                FROM [TK].dbo.PURTC,[TK].dbo.PURTD,[TK].dbo.PURMA,[TK].dbo.INVMB
                                WHERE TC001=TD001 AND TC002=TD002 
                                AND TC004=MA001
                                AND TD004=MB001
                                AND TC003 LIKE '%202411%'
                                ORDER BY TC004,TC001,TC002,TD003
        


                             ", QUERYS.ToString());
        }
        else
        {
            cmdTxt.AppendFormat(@"
                              
                             ");
        }



        m_db.AddParameter("@MB001", TextBox1.Text.Trim());


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
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ////Get the button that raised the event
            //Button btn = (Button)e.Row.FindControl("GVButton1");

            ////Get the row that contains this button
            //GridViewRow gvr = (GridViewRow)btn.NamingContainer;

            ////string cellvalue = gvr.Cells[2].Text.Trim();
            //string Cellvalue = btn.CommandArgument;

            //DataRowView row = (DataRowView)e.Row.DataItem;
            //Button lbtnName = (Button)e.Row.FindControl("GVButton1");

            //ExpandoObject param = new { ID = Cellvalue }.ToExpando();

            ////Grid開窗是用RowDataBound事件再開窗
            ////Dialog.Open2(lbtnName, "~/CDS/WebPage/STOCK/COPTGTHDialogEDIT.aspx", "", 800, 600, Dialog.PostBackType.AfterReturn, param);
           
        }


    }

    public void OnBeforeExport1(object sender, Ede.Uof.Utility.Component.BeforeExportEventArgs e)
    {
        SETEXCEL();

        //string connectionString = ConfigurationManager.ConnectionStrings["ERPconnectionstring"].ToString();
        //Ede.Uof.Utility.Data.DatabaseHelper m_db = new Ede.Uof.Utility.Data.DatabaseHelper(connectionString);


        //string cmdTxt = @" 
        //               SELECT [PRODUCTS].[MB001],[PRODUCTSFEATURES],[SALESFOCUS],[COPYWRITINGS],[PICPATHS]
        //                ,MB002,MB003,MB004,MA003,ISNULL(MD007,0) AS MD007,CONVERT(NVARCHAR,MB023)+(CASE WHEN MB198='1' THEN '天' ELSE (CASE WHEN MB198='2' THEN '月' ELSE '年' END ) END ) AS 'VALIDITYPERIOD',CONVERT(decimal(16,3),ISNULL(MB047,0)) AS MB047,MB013
        //                ,[ALBUM_GUID], [PHOTO_GUID],[PHOTO_DESC],[FILE_ID],[RESIZE_FILE_ID],[THUMBNAIL_FILE_ID]
        //                FROM [TKBUSINESS].[dbo].[PRODUCTS]
        //                LEFT JOIN [TK].dbo.[INVMB] ON [PRODUCTS].[MB001]=[INVMB].[MB001]
        //                LEFT JOIN [TK].dbo.INVMA ON MA001='9' AND MA002=MB115
        //                LEFT JOIN [TK].dbo.BOMMD ON MD001=[INVMB].[MB001] AND MD003 LIKE '201%'
        //                LEFT JOIN [192.168.1.223].[UOF].[dbo].[TB_EIP_ALBUM_PHOTO] ON [PHOTO_TOPIC]=[PRODUCTS].[MB001] COLLATE Chinese_Taiwan_Stroke_BIN
        //                ORDER BY [PRODUCTS].[MB001]
        //                ";



        //DataTable dt = new DataTable();

        //dt.Load(m_db.ExecuteReader(cmdTxt));

        //if (dt.Rows.Count > 0)
        //{
        //    dt.Columns[0].Caption = "ID";


        //    e.Datasource = dt;
        //}
    }

    public override void VerifyRenderingInServerForm(Control control) 
    { 

    }

    public void SETEXCEL()
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ERPconnectionstring"].ToString();
        Ede.Uof.Utility.Data.DatabaseHelper m_db = new Ede.Uof.Utility.Data.DatabaseHelper(connectionString);

        StringBuilder cmdTxt = new StringBuilder();
        StringBuilder QUERYS = new StringBuilder();

        QUERYS.AppendFormat(@" ");

       
       cmdTxt.AppendFormat(@" 
                             
                                ", QUERYS.ToString());

        //string cmdTxt = @" 
        //                SELECT [PRODUCTS].[MB001],[PRODUCTSFEATURES],[SALESFOCUS],[COPYWRITINGS],[PICPATHS]
        //                ,MB002,MB003,MB004,MA003,ISNULL(MD007,0) AS MD007,CONVERT(NVARCHAR,MB023)+(CASE WHEN MB198='1' THEN '天' ELSE (CASE WHEN MB198='2' THEN '月' ELSE '年' END ) END ) AS 'VALIDITYPERIOD',CONVERT(decimal(16,3),ISNULL(MB047,0)) AS MB047,MB013
        //                ,[ALBUM_GUID], [PHOTO_GUID],[PHOTO_DESC],[FILE_ID],[RESIZE_FILE_ID],[THUMBNAIL_FILE_ID]
        //                FROM [TKBUSINESS].[dbo].[PRODUCTS]
        //                LEFT JOIN [TK].dbo.[INVMB] ON [PRODUCTS].[MB001]=[INVMB].[MB001]
        //                LEFT JOIN [TK].dbo.INVMA ON MA001='9' AND MA002=MB115
        //                LEFT JOIN [TK].dbo.BOMMD ON MD001=[INVMB].[MB001] AND MD003 LIKE '201%'
        //                LEFT JOIN [192.168.1.223].[UOF].[dbo].[TB_EIP_ALBUM_PHOTO] ON [PHOTO_DESC] LIKE '%'+[PRODUCTS].[MB001]+'%' COLLATE Chinese_Taiwan_Stroke_BIN
        //                ORDER BY [PRODUCTS].[MB001]
        //                ";

        //m_db.AddParameter("@SDATE", SDATE);
        //m_db.AddParameter("@EDATE", EDATE);

        DataTable dt = new DataTable();

        dt.Load(m_db.ExecuteReader(cmdTxt.ToString()));

        if(dt.Rows.Count>0)
        {
            //檔案名稱
            var fileName = "清單" + DateTime.Now.ToString("yyyy-MM-dd--hh-mm-ss") + ".xlsx";
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial; // 關閉新許可模式通知

            using (var excel = new ExcelPackage(new FileInfo(fileName)))
            {              

                // 建立分頁
                var ws = excel.Workbook.Worksheets.Add("list" + DateTime.Now.ToShortDateString());


                //預設行高
                ws.DefaultRowHeight = 60;

                // 寫入資料試試
                //ws.Cells[2, 1].Value = "測試測試";
                int ROWS = 2;
                int COLUMNS = 1;


                //excel標題
                ws.Cells[1, 1].Value = "品號";
                ws.Cells[1, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center; //欄位置中
                ws.Cells[1, 1].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center; //高度置中
                ws.Cells[1, 1].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin); //儲存格框線
               

                foreach (DataRow od in dt.Rows)
                {
                    ws.Cells[ROWS, 1].Value = od["MB001"].ToString();
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



    #endregion

    #region BUTTON
    protected void btn_Click(object sender, EventArgs e)
    {


        //開窗後回傳參數
        if (!string.IsNullOrEmpty(Dialog.GetReturnValue()))
        {
            //txtReturnValue.Text = Dialog.GetReturnValue();
        }


    }
    protected void btn1_Click(object sender, EventArgs e)
    {
        BindGrid("");

    }


    protected void btn2_Click(object sender, EventArgs e)
    {
        SETEXCEL();
    }
    protected void btn3_Click(object sender, EventArgs e)
    {
        Response.ClearContent();
        Response.AddHeader("content-disposition", "attachment; filename=test.xls");
        Response.ContentEncoding = System.Text.Encoding.GetEncoding("big5");
        HttpContext.Current.Response.Write("<meta http-equiv=Content-Type content=text/html;charset=big5>");
        HttpContext.Current.Response.Write("<head><meta http-equiv=Content-Type content=text/html;charset=big5></head>");
        Response.Charset = "big5";
        Response.ContentType = "application/excel";


        System.IO.StringWriter sw = new System.IO.StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        Grid1.RenderControl(htw);
        Response.Write(sw.ToString());
        Response.End();
    }
    protected void MyButtonClick(object sender, System.EventArgs e)
    {
      

    }

   
    protected void GVButton1_Click(object sender, EventArgs e)
    {
       
    }

    #endregion
}