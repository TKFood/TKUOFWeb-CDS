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

public partial class CDS_WebPage_STOCK_COPTGTH : Ede.Uof.Utility.Page.BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SET();
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
    public void SET()
    {
        TextBox1.Text = DateTime.Now.ToString("yyyyMMdd");
    }
    private void BindGrid(string SALESFOCUS)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ERPconnectionstring"].ToString();
        Ede.Uof.Utility.Data.DatabaseHelper m_db = new Ede.Uof.Utility.Data.DatabaseHelper(connectionString);

        StringBuilder cmdTxt = new StringBuilder();
        StringBuilder QUERYS = new StringBuilder();

      

        //建議售價
        if (!string.IsNullOrEmpty(TextBox1.Text))
        {
            QUERYS.AppendFormat(@"  AND TG002 LIKE '{0}%' ", TextBox1.Text);
        }       
        else 
        {
            QUERYS.AppendFormat(@" ");
        }

        //AND BOMMD.MD003 NOT IN (SELECT  [MD003]   FROM [TKMOC].[dbo].[MOCHALFPRODUCTDBOXSLIMITS])

        cmdTxt.AppendFormat(@" 
                            SELECT *
                            FROM
                            (
                            SELECT  [NO]
                            ,[TG001]
                            ,[TG002]
                            ,[BOXNO]                          
                            ,[ALLWEIGHTS]
                            ,[PACKWEIGHTS]
                            ,[PRODUCTWEIGHTS]
                            ,[PACKRATES]
                            ,[RATECLASS]
                            ,[CHECKRATES]
                            ,[ISVALIDS]
                            ,[PACKAGENAMES]
                            ,[PACKAGEFROM]
                            ,TG001+TG002 AS 'TG001TG002'
                            FROM [TKWAREHOUSE].[dbo].[PACKAGEBOXS]
                            UNION ALL
                            SELECT 
                            '' [NO]
                            ,[TG001]
                            ,[TG002]
                            ,'' [BOXNO]                      
                            ,0 [ALLWEIGHTS]
                            ,0 [PACKWEIGHTS]
                            ,0 [PRODUCTWEIGHTS]
                            ,0 [PACKRATES]
                            ,'' [RATECLASS]
                            ,'' [CHECKRATES]
                            ,'' [ISVALIDS]
                            ,'' [PACKAGENAMES]
                            ,'' [PACKAGEFROM]
                            ,[TG001]+[TG002] AS 'TG001TG002'
                            FROM [TK].dbo.COPTG                          
                            ) AS TEMP
                            WHERE 1=1
                            {0}
                            ORDER BY TG001,TG002,BOXNO
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
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //Get the button that raised the event
            Button btn_GVButton1 = (Button)e.Row.FindControl("GVButton1");
            string Cellvalue_GVButton1 = btn_GVButton1.CommandArgument;
            DataRowView row = (DataRowView)e.Row.DataItem;
            Button lbtnName_GVButton1 = (Button)e.Row.FindControl("GVButton1");
            ExpandoObject param_GVButton1 = new { ID = Cellvalue_GVButton1 }.ToExpando();

            //Grid開窗是用RowDataBound事件再開窗
            Dialog.Open2(lbtnName_GVButton1, "~/CDS/WebPage/STOCK/COPTGTHDialogADD.aspx", "", 800, 600, Dialog.PostBackType.AfterReturn, param_GVButton1);
            //Dialog.Open2(lbtnName, "~/CDS/WebPage/Mobile/Mobile_TEST3.aspx", "", 800, 600, Dialog.PostBackType.AfterReturn, param);
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //Get the button that raised the event
            Button btn_GVButton2 = (Button)e.Row.FindControl("GVButton2");
            string Cellvalue_GVButton2 = btn_GVButton2.CommandArgument;
            DataRowView row = (DataRowView)e.Row.DataItem;
            Button lbtnName_GVButton2 = (Button)e.Row.FindControl("GVButton2");
            ExpandoObject param_GVButton2 = new { ID = Cellvalue_GVButton2 }.ToExpando();

            //Grid開窗是用RowDataBound事件再開窗
            Dialog.Open2(lbtnName_GVButton2, "~/CDS/WebPage/STOCK/COPTGTHDialogADD.aspx", "", 800, 600, Dialog.PostBackType.AfterReturn, param_GVButton2);
            //Dialog.Open2(lbtnName, "~/CDS/WebPage/Mobile/Mobile_TEST3.aspx", "", 800, 600, Dialog.PostBackType.AfterReturn, param);
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string column1Value = "";
            // 取得資料綁定到 GridView 的資料物件
            DataRowView dataItem = (DataRowView)e.Row.DataItem;
            column1Value = dataItem["NO"].ToString();

            // 在這裡設定 Column2 的顯示值，您可以根據特定條件決定是否留空白
            if (string.IsNullOrEmpty(column1Value))
            {
                Button btn_GVButton1 = (Button)e.Row.FindControl("GVButton1");
                btn_GVButton1.Visible = true;
                Button btn_GVButton2 = (Button)e.Row.FindControl("GVButton2");
                btn_GVButton2.Visible = false;


                // 留空白
                e.Row.Cells[3].Text = string.Empty;
                e.Row.Cells[4].Text = string.Empty;
                e.Row.Cells[5].Text = string.Empty;
                e.Row.Cells[6].Text = string.Empty;
                e.Row.Cells[7].Text = string.Empty;
                e.Row.Cells[8].Text = string.Empty;
                e.Row.Cells[9].Text = string.Empty;
                e.Row.Cells[10].Text = string.Empty;
                e.Row.Cells[11].Text = string.Empty;
                
            }
            else
            {
                Button btn_GVButton1 = (Button)e.Row.FindControl("GVButton1");
                btn_GVButton1.Visible = false;
                Button btn_GVButton2 = (Button)e.Row.FindControl("GVButton2");
                btn_GVButton2.Visible = true;
            }
        }


            StringBuilder PATH = new StringBuilder();
        System.Web.UI.WebControls.Image img = (System.Web.UI.WebControls.Image)e.Row.FindControl("Image1");
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView row = (DataRowView)e.Row.DataItem;
            System.Web.UI.WebControls.Image img1 = (System.Web.UI.WebControls.Image)e.Row.FindControl("Image1");

            if (!string.IsNullOrEmpty(row["TG001TG002"].ToString()))
            {               
                PATH.AppendFormat(@"https://eip.tkfood.com.tw/UOF/UPLOAD/COPTGCOPTH/{0}/{1}.jpg", row["TG001TG002"].ToString().Substring(4,4), row["TG001TG002"].ToString());
                img.ImageUrl = PATH.ToString();
            }


        }


    }

    protected void Grid1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int rowIndex = -1;

        if (e.CommandName == "GVButton1")
        {          
            BindGrid("");
            MsgBox(e.CommandArgument.ToString() + " 已更新", this.Page, this);

        }

    }

    public void OnBeforeExport1(object sender, Ede.Uof.Utility.Component.BeforeExportEventArgs e)
    {
        SETEXCEL();     
    }

   

    public override void VerifyRenderingInServerForm(Control control) 
    { 

    }

    public void SETEXCEL()
    {
        //string connectionString = ConfigurationManager.ConnectionStrings["ERPconnectionstring"].ToString();
        //Ede.Uof.Utility.Data.DatabaseHelper m_db = new Ede.Uof.Utility.Data.DatabaseHelper(connectionString);

        //StringBuilder cmdTxt = new StringBuilder();
        //StringBuilder QUERYS = new StringBuilder();

        //QUERYS.AppendFormat(@" ");

        ////狀態
        //if (!string.IsNullOrEmpty(DropDownList1.Text))
        //{
        //    if (DropDownList1.Text.Equals("全部"))
        //    {
        //        QUERYS.AppendFormat(@" ");
        //    }
        //    else if (!DropDownList1.Text.Equals("全部"))
        //    {
        //        QUERYS.AppendFormat(@" AND  [SALESFOCUS] LIKE '%{0}%' ", DropDownList1.Text);
        //    }
        //}

        //this.Session["STATUS"] = DropDownList1.Text;

        ////建議售價
        //if (!string.IsNullOrEmpty(TextBox1.Text) && !string.IsNullOrEmpty(TextBox2.Text))
        //{
        //    QUERYS.AppendFormat(@" AND [PRICES1]>={0} AND [PRICES1]<={1}", TextBox1.Text, TextBox2.Text);
        //}
        //else if (!string.IsNullOrEmpty(TextBox1.Text) && string.IsNullOrEmpty(TextBox2.Text))
        //{
        //    QUERYS.AppendFormat(@" AND [PRICES1]>={0} ", TextBox1.Text);
        //}
        //else if (string.IsNullOrEmpty(TextBox1.Text) && !string.IsNullOrEmpty(TextBox2.Text))
        //{
        //    QUERYS.AppendFormat(@" AND [PRICES1]<={0} ", TextBox2.Text);
        //}

        ////IP價
        //if (!string.IsNullOrEmpty(TextBox3.Text) && !string.IsNullOrEmpty(TextBox4.Text))
        //{
        //    QUERYS.AppendFormat(@" AND [PRICES2]>={0} AND [PRICES2]<={1}", TextBox3.Text, TextBox4.Text);
        //}
        //else if (!string.IsNullOrEmpty(TextBox3.Text) && string.IsNullOrEmpty(TextBox4.Text))
        //{
        //    QUERYS.AppendFormat(@" AND [PRICES2]>={0} ", TextBox3.Text);
        //}
        //else if (string.IsNullOrEmpty(TextBox3.Text) && !string.IsNullOrEmpty(TextBox4.Text))
        //{
        //    QUERYS.AppendFormat(@" AND [PRICES2]<={0} ", TextBox4.Text);
        //}

        ////DM價
        //if (!string.IsNullOrEmpty(TextBox5.Text) && !string.IsNullOrEmpty(TextBox6.Text))
        //{
        //    QUERYS.AppendFormat(@" AND [PRICES3]>={0} AND [PRICES3]<={1}", TextBox5.Text, TextBox6.Text);
        //}
        //else if (!string.IsNullOrEmpty(TextBox5.Text) && string.IsNullOrEmpty(TextBox6.Text))
        //{
        //    QUERYS.AppendFormat(@" AND [PRICES3]>={0} ", TextBox5.Text);
        //}
        //else if (string.IsNullOrEmpty(TextBox5.Text) && !string.IsNullOrEmpty(TextBox6.Text))
        //{
        //    QUERYS.AppendFormat(@" AND [PRICES3]<={0} ", TextBox6.Text);
        //}

        ////口味
        //if (!string.IsNullOrEmpty(TextBox7.Text))
        //{
        //    QUERYS.AppendFormat(@" AND MA003 LIKE '%{0}%'", TextBox7.Text);
        //}

        ////效期
        //if (!string.IsNullOrEmpty(TextBox8.Text))
        //{
        //    QUERYS.AppendFormat(@" AND CONVERT(NVARCHAR,MB023)+(CASE WHEN MB198='1' THEN '天' ELSE (CASE WHEN MB198='2' THEN '月' ELSE '年' END ) END ) LIKE '%{0}%'", TextBox8.Text);
        //}

        ////銷售重點
        //if (!string.IsNullOrEmpty(TextBox9.Text))
        //{
        //    QUERYS.AppendFormat(@" AND PRODUCTSFEATURES LIKE '%{0}%'", TextBox9.Text);
        //}

        ////銷售重點
        //if (!string.IsNullOrEmpty(TextBox10.Text))
        //{
        //    QUERYS.AppendFormat(@" AND [INVMB].MB002 LIKE '%{0}%'", TextBox10.Text);
        //}

        //cmdTxt.AppendFormat(@" 
        //                        SELECT [PRODUCTS].[MB001],[PRODUCTSFEATURES],[SALESFOCUS],[COPYWRITINGS],[PICPATHS]
        //                        ,[PRICES1],[PRICES2],[PRICES3]
        //                        ,MB002,MB003,MB004,MA003,ISNULL(MD007,0) AS MD007,CONVERT(NVARCHAR,MB023)+(CASE WHEN MB198='1' THEN '天' ELSE (CASE WHEN MB198='2' THEN '月' ELSE '年' END ) END ) AS 'VALIDITYPERIOD',CONVERT(decimal(16,3),ISNULL(MB047,0)) AS MB047,MB013
        //                        ,[ALBUM_GUID], [PHOTO_GUID],[PHOTO_DESC],[FILE_ID],[RESIZE_FILE_ID],[THUMBNAIL_FILE_ID]
        //                       ,(CONVERT(NVARCHAR,MB093)+'*'+CONVERT(NVARCHAR,MB094)+'*'+CONVERT(NVARCHAR,MB095)) AS MB093094095

        //                        FROM [TKBUSINESS].[dbo].[PRODUCTS]
        //                        LEFT JOIN [TK].dbo.[INVMB] ON [PRODUCTS].[MB001]=[INVMB].[MB001]
        //                        LEFT JOIN [TK].dbo.INVMA ON MA001='9' AND MA002=MB115
        //                        LEFT JOIN [TK].dbo.BOMMD ON MD001=[INVMB].[MB001] AND MD003 LIKE '201%'
        //                        LEFT JOIN [192.168.1.223].[UOF].[dbo].[TB_EIP_ALBUM_PHOTO] ON [PHOTO_DESC] LIKE '%'+[PRODUCTS].[MB001]+'%' COLLATE Chinese_Taiwan_Stroke_BIN
        //                        WHERE 1=1
        //                        {0}
        //                        ORDER BY [PRODUCTS].[MB001]

        //                        ", QUERYS.ToString());

        ////string cmdTxt = @" 
        ////                SELECT [PRODUCTS].[MB001],[PRODUCTSFEATURES],[SALESFOCUS],[COPYWRITINGS],[PICPATHS]
        ////                ,MB002,MB003,MB004,MA003,ISNULL(MD007,0) AS MD007,CONVERT(NVARCHAR,MB023)+(CASE WHEN MB198='1' THEN '天' ELSE (CASE WHEN MB198='2' THEN '月' ELSE '年' END ) END ) AS 'VALIDITYPERIOD',CONVERT(decimal(16,3),ISNULL(MB047,0)) AS MB047,MB013
        ////                ,[ALBUM_GUID], [PHOTO_GUID],[PHOTO_DESC],[FILE_ID],[RESIZE_FILE_ID],[THUMBNAIL_FILE_ID]
        ////                FROM [TKBUSINESS].[dbo].[PRODUCTS]
        ////                LEFT JOIN [TK].dbo.[INVMB] ON [PRODUCTS].[MB001]=[INVMB].[MB001]
        ////                LEFT JOIN [TK].dbo.INVMA ON MA001='9' AND MA002=MB115
        ////                LEFT JOIN [TK].dbo.BOMMD ON MD001=[INVMB].[MB001] AND MD003 LIKE '201%'
        ////                LEFT JOIN [192.168.1.223].[UOF].[dbo].[TB_EIP_ALBUM_PHOTO] ON [PHOTO_DESC] LIKE '%'+[PRODUCTS].[MB001]+'%' COLLATE Chinese_Taiwan_Stroke_BIN
        ////                ORDER BY [PRODUCTS].[MB001]
        ////                ";

        ////m_db.AddParameter("@SDATE", SDATE);
        ////m_db.AddParameter("@EDATE", EDATE);

        //DataTable dt = new DataTable();

        //dt.Load(m_db.ExecuteReader(cmdTxt.ToString()));

        //if(dt.Rows.Count>0)
        //{
        //    //檔案名稱
        //    var fileName = "商品清單" + DateTime.Now.ToString("yyyy-MM-dd--hh-mm-ss") + ".xlsx";
        //    ExcelPackage.LicenseContext = LicenseContext.NonCommercial; // 關閉新許可模式通知

        //    using (var excel = new ExcelPackage(new FileInfo(fileName)))
        //    {              

        //        // 建立分頁
        //        var ws = excel.Workbook.Worksheets.Add("list" + DateTime.Now.ToShortDateString());


        //        //預設行高
        //        ws.DefaultRowHeight = 60;

        //        // 寫入資料試試
        //        //ws.Cells[2, 1].Value = "測試測試";
        //        int ROWS = 2;
        //        int COLUMNS = 1;


        //        //excel標題
        //        ws.Cells[1, 1].Value = "品號";
        //        ws.Cells[1, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center; //欄位置中
        //        ws.Cells[1, 1].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center; //高度置中
        //        ws.Cells[1, 1].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin); //儲存格框線
        //        ws.Cells[1, 2].Value = "品名";
        //        ws.Cells[1, 2].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center; //欄位置中
        //        ws.Cells[1, 2].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center; //高度置中
        //        ws.Cells[1, 2].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin); //儲存格框線
        //        ws.Cells[1, 3].Value = "規格";
        //        ws.Cells[1, 3].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center; //欄位置中
        //        ws.Cells[1, 3].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center; //高度置中
        //        ws.Cells[1, 3].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin); //儲存格框線
        //        ws.Cells[1, 4].Value = "單位";
        //        ws.Cells[1, 4].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center; //欄位置中
        //        ws.Cells[1, 4].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center; //高度置中
        //        ws.Cells[1, 4].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin); //儲存格框線
        //        ws.Cells[1, 5].Value = "口味";
        //        ws.Cells[1, 5].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center; //欄位置中
        //        ws.Cells[1, 5].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center; //高度置中
        //        ws.Cells[1, 5].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin); //儲存格框線
        //        ws.Cells[1, 6].Value = "箱入數";
        //        ws.Cells[1, 6].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center; //欄位置中
        //        ws.Cells[1, 6].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center; //高度置中
        //        ws.Cells[1, 6].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin); //儲存格框線
        //        ws.Cells[1, 7].Value = "有效期";
        //        ws.Cells[1, 7].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center; //欄位置中
        //        ws.Cells[1, 7].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center; //高度置中
        //        ws.Cells[1, 7].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin); //儲存格框線
        //        ws.Cells[1, 8].Value = "標準售價";
        //        ws.Cells[1, 8].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center; //欄位置中
        //        ws.Cells[1, 8].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center; //高度置中
        //        ws.Cells[1, 8].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin); //儲存格框線
        //        ws.Cells[1, 9].Value = "IP價";
        //        ws.Cells[1, 9].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center; //欄位置中
        //        ws.Cells[1, 9].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center; //高度置中
        //        ws.Cells[1, 9].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin); //儲存格框線
        //        ws.Cells[1, 10].Value = "DM價";
        //        ws.Cells[1, 10].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center; //欄位置中
        //        ws.Cells[1, 10].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center; //高度置中
        //        ws.Cells[1, 10].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin); //儲存格框線
        //        ws.Cells[1, 11].Value = "條碼";
        //        ws.Cells[1, 11].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center; //欄位置中
        //        ws.Cells[1, 11].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center; //高度置中
        //        ws.Cells[1, 11].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin); //儲存格框線
        //        ws.Cells[1, 12].Value = "銷售重點";
        //        ws.Cells[1, 12].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center; //欄位置中
        //        ws.Cells[1, 12].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center; //高度置中
        //        ws.Cells[1, 12].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin); //儲存格框線
        //        ws.Cells[1, 13].Value = "銷售通路";
        //        ws.Cells[1, 13].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center; //欄位置中
        //        ws.Cells[1, 13].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center; //高度置中
        //        ws.Cells[1, 13].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin); //儲存格框線
        //        ws.Cells[1, 14].Value = "照片";
        //        ws.Cells[1, 14].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center; //欄位置中
        //        ws.Cells[1, 14].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center; //高度置中
        //        ws.Cells[1, 14].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin); //儲存格框線
        //        ws.Cells[1, 15].Value = "長*寬*高 ";
        //        ws.Cells[1, 15].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center; //欄位置中
        //        ws.Cells[1, 15].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center; //高度置中
        //        ws.Cells[1, 15].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin); //儲存格框線

        //        foreach (DataRow od in dt.Rows)
        //        {
        //            ws.Cells[ROWS, 1].Value = od["MB001"].ToString();
        //            ws.Cells[ROWS, 1].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center; //高度置中
        //            ws.Cells[ROWS, 1].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin); //儲存格框線
        //            ws.Cells[ROWS, 2].Value = od["MB002"].ToString();
        //            ws.Cells[ROWS, 2].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center; //高度置中
        //            ws.Cells[ROWS, 2].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin); //儲存格框線
        //            ws.Cells[ROWS, 3].Value = od["MB003"].ToString();
        //            ws.Cells[ROWS, 3].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center; //高度置中
        //            ws.Cells[ROWS, 3].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin); //儲存格框線
        //            ws.Cells[ROWS, 4].Value = od["MB004"].ToString();
        //            ws.Cells[ROWS, 4].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center; //高度置中
        //            ws.Cells[ROWS, 4].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin); //儲存格框線
        //            ws.Cells[ROWS, 5].Value = od["MA003"].ToString();
        //            ws.Cells[ROWS, 5].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center; //高度置中
        //            ws.Cells[ROWS, 5].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin); //儲存格框線
        //            ws.Cells[ROWS, 6].Value = od["MD007"].ToString();
        //            ws.Cells[ROWS, 6].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center; //高度置中
        //            ws.Cells[ROWS, 6].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin); //儲存格框線
        //            ws.Cells[ROWS, 7].Value = od["VALIDITYPERIOD"].ToString();
        //            ws.Cells[ROWS, 7].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center; //高度置中
        //            ws.Cells[ROWS, 7].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin); //儲存格框線
        //            ws.Cells[ROWS, 8].Value = od["PRICES1"].ToString();
        //            ws.Cells[ROWS, 8].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center; //高度置中
        //            ws.Cells[ROWS, 8].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin); //儲存格框線
        //            ws.Cells[ROWS, 9].Value = od["PRICES2"].ToString();
        //            ws.Cells[ROWS, 9].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center; //高度置中
        //            ws.Cells[ROWS, 9].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin); //儲存格框線
        //            ws.Cells[ROWS, 10].Value = od["PRICES3"].ToString();
        //            ws.Cells[ROWS, 10].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center; //高度置中
        //            ws.Cells[ROWS, 10].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin); //儲存格框線
        //            ws.Cells[ROWS, 11].Value = od["MB013"].ToString();
        //            ws.Cells[ROWS, 11].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center; //高度置中
        //            ws.Cells[ROWS, 11].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin); //儲存格框線
        //            ws.Cells[ROWS, 12].Value = od["PRODUCTSFEATURES"].ToString();
        //            ws.Cells[ROWS, 12].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center; //高度置中
        //            ws.Cells[ROWS, 12].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin); //儲存格框線
        //            ws.Cells[ROWS, 13].Value = od["SALESFOCUS"].ToString();
        //            ws.Cells[ROWS, 13].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center; //高度置中
        //            ws.Cells[ROWS, 13].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin); //儲存格框線

        //            ws.Cells[ROWS, 15].Value = od["MB093094095"].ToString();
        //            ws.Cells[ROWS, 15].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center; //高度置中
        //            ws.Cells[ROWS, 15].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin); //儲存格框線


        //            ws.Cells[ROWS, 14].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin); //儲存格框線

        //            try
        //            {
        //                if (!string.IsNullOrEmpty(od["PHOTO_DESC"].ToString()))
        //                {
        //                    //網路圖片
        //                    WebClient MyWebClient = new WebClient();
        //                    StringBuilder PATH = new StringBuilder();



        //                    PATH.AppendFormat(@"https://eip.tkfood.com.tw/UOF/Common/FileCenter/V3/Handler/FileControlHandler.ashx?id={0}
        //                        ", od["RESIZE_FILE_ID"].ToString());

        //                    //PATH.AppendFormat(@"https://eip.tkfood.com.tw/UOF/common/filecenter/v3/handler/downloadhandler.ashx?id={0}&path=ALBUM%5C2021%5C03&contentType=image%2Fpng&name={1}
        //                    //        ", od["RESIZE_FILE_ID"].ToString(), od["PHOTO_DESC"].ToString());

        //                    //string fileURL = "https://eip.tkfood.com.tw/UOF/common/filecenter/v3/handler/downloadhandler.ashx?id=8b2a033b-c301-419b-938d-e6cfedf28b82&path=ALBUM%5C2021%5C03&contentType=image%2Fpng&name=40100010650490.png";
        //                    //string fileURL = "https://eip.tkfood.com.tw/UOF/common/filecenter/v3/handler/downloadhandler.ashx?id=2a44a870-f960-4178-9551-e9612fd46b30&path=ALBUM%5C2021%5C03&contentType=image%2Fpng&name=40100710430390.jpg";
        //                    string fileURL = PATH.ToString();

        //                    var pageData = MyWebClient.DownloadData(fileURL);

        //                    Stream imgms = new MemoryStream(pageData);
        //                    System.Drawing.Bitmap imgfs = new System.Drawing.Bitmap(imgms);

        //                    //MemoryStream fs = new MemoryStream();
        //                    //fs.Write(pageData, 0, pageData.Length - 1);
        //                    //var imgfs = System.Drawing.Image.FromStream(fs);
        //                    //fs.Close();

        //                    ExcelPicture picture = excel.Workbook.Worksheets[0].Drawings.AddPicture(od["MB001"].ToString(), imgfs);//插入圖片

        //                    //ExcelPicture picture = excel.Workbook.Worksheets[0].Drawings.AddPicture("logo", System.Drawing.Image.FromFile(@"https://eip.tkfood.com.tw/UOF/common/filecenter/v3/handler/downloadhandler.ashx?id=8b2a033b-c301-419b-938d-e6cfedf28b82&path=ALBUM%5C2021%5C03&contentType=image%2Fpng&name=40100010650490.png"));//插入圖片
        //                    //ExcelPicture picture = excel.Workbook.Worksheets[0].Drawings.AddPicture("logo", System.Drawing.Image.FromFile(@"C:\TEMP\40100010650490.png"));//插入圖片

        //                    picture.From.Row = ROWS;
        //                    picture.From.Column = COLUMNS;

        //                    picture.SetPosition(1 * ROWS - 1, 5, 13, 5);//設置圖片的位置
        //                    picture.SetSize(50, 50);//設置圖片的大小
        //                }
        //            }
        //            catch
        //            {

        //            }
        //            finally
        //            {

        //            }
                   

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
        //    //ExcelPackage.LicenseContext = LicenseContext.NonCommercial; // 關閉新許可模式通知
        //    //                                                            // 沒設置的話會跳出 Please set the excelpackage.licensecontext property

            
        //    ////var file = new FileInfo(fileName);
        //    //using (var excel = new ExcelPackage(file))
        //    //{
                
        //    //}
        //}

    }
     public void MsgBox(String ex, Page pg, Object obj)
    {
        string s = "<SCRIPT language='javascript'>alert('" + ex.Replace("\r\n", "\\n").Replace("'", "") + "'); </SCRIPT>";
        Type cstype = obj.GetType();
        ClientScriptManager cs = pg.ClientScript;
        cs.RegisterClientScriptBlock(cstype, s, s.ToString());
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
        //this.Session["SDATE"] = txtDate1.Text.Trim();
        //this.Session["EDATE"] = txtDate2.Text.Trim();
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

    protected void btn5_Click(object sender, EventArgs e)
    {
        BindGrid("");

    }
    #endregion
}