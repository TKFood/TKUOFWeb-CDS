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
using Image = System.Web.UI.WebControls.Image;
using OfficeOpenXml.Style;

public partial class CDS_WebPage_COP_TBBU_PRODUCTS : Ede.Uof.Utility.Page.BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            BindGrid();
        }
        else
        {
            BindGrid();
        }
    }
    #region FUNCTION

    private void BindGrid()
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ERPconnectionstring"].ToString();
        Ede.Uof.Utility.Data.DatabaseHelper m_db = new Ede.Uof.Utility.Data.DatabaseHelper(connectionString);

        string cmdTxt = @" 
                        SELECT [PRODUCTS].[MB001],[PRODUCTSFEATURES],[SALESFOCUS],[COPYWRITINGS],[PICPATHS]
                        ,MB002,MB003,MB004,MA003,ISNULL(MD007,0) AS MD007,CONVERT(NVARCHAR,MB023)+(CASE WHEN MB198='1' THEN '天' ELSE (CASE WHEN MB198='2' THEN '月' ELSE '年' END ) END ) AS 'VALIDITYPERIOD',CONVERT(decimal(16,3),ISNULL(MB047,0)) AS MB047,MB013
                        ,[ALBUM_GUID], [PHOTO_GUID],[PHOTO_DESC],[FILE_ID],[RESIZE_FILE_ID],[THUMBNAIL_FILE_ID]
                        FROM [TKBUSINESS].[dbo].[PRODUCTS]
                        LEFT JOIN [TK].dbo.[INVMB] ON [PRODUCTS].[MB001]=[INVMB].[MB001]
                        LEFT JOIN [TK].dbo.INVMA ON MA001='9' AND MA002=MB115
                        LEFT JOIN [TK].dbo.BOMMD ON MD001=[INVMB].[MB001] AND MD003 LIKE '201%'
                        LEFT JOIN [192.168.1.223].[UOF].[dbo].[TB_EIP_ALBUM_PHOTO] ON [PHOTO_DESC] LIKE '%'+[PRODUCTS].[MB001]+'%' COLLATE Chinese_Taiwan_Stroke_BIN
                        ORDER BY [PRODUCTS].[MB001]
                        ";

        //m_db.AddParameter("@SDATE", SDATE);
        //m_db.AddParameter("@EDATE", EDATE);

        DataTable dt = new DataTable();

        dt.Load(m_db.ExecuteReader(cmdTxt));

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
            Button btn = (Button)e.Row.FindControl("Button1");

            //Get the row that contains this button
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;

            //string cellvalue = gvr.Cells[2].Text.Trim();
            string Cellvalue = btn.CommandArgument;

            DataRowView row = (DataRowView)e.Row.DataItem;
            Button lbtnName = (Button)e.Row.FindControl("Button1");

            ExpandoObject param = new { ID = Cellvalue }.ToExpando();

            //Grid開窗是用RowDataBound事件再開窗
            Dialog.Open2(lbtnName, "~/CDS/WebPage/COP/TBBU_PRODUCTSDialogEDITDEL.aspx", "", 800, 600, Dialog.PostBackType.AfterReturn, param);
        }


        

        StringBuilder PATH = new StringBuilder();        

        Image img = (Image)e.Row.FindControl("Image1");
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView row = (DataRowView)e.Row.DataItem;
            Image img1 = (Image)e.Row.FindControl("Image1");

           

            if (!string.IsNullOrEmpty(row["PHOTO_GUID"].ToString()))
            {
                //img.ImageUrl = "https://eip.tkfood.com.tw/UOF/common/filecenter/v3/handler/downloadhandler.ashx?id=8b2a033b-c301-419b-938d-e6cfedf28b82&path=ALBUM%5C2021%5C03&contentType=image%2Fpng&name=40100010650490.png";


                PATH.AppendFormat(@"https://eip.tkfood.com.tw/UOF/common/filecenter/v3/handler/downloadhandler.ashx?id={0}&path=ALBUM%5C2021%5C03&contentType=image%2Fpng&name={1}
                                ", row["RESIZE_FILE_ID"].ToString(), row["PHOTO_DESC"].ToString());
                
                img.ImageUrl = PATH.ToString();

                //img.ImageUrl  = Request.ApplicationPath + "/Common/FileCenter/ShowImage.aspx?id=" + row["THUMBNAIL_FILE_ID"].ToString();

                //img.ImageUrl = string.Format("~/Common/FileCenter/Downloadfile.ashx?id={0}", row["THUMBNAIL_FILE_ID"].ToString());

                //e.Row.Cells[0].Text = row["THUMBNAIL_FILE_ID"].ToString();
                ////獲取當前行的圖片路徑
                //string ImgUrl = img.ImageUrl;
                ////給帶圖片的單元格添加點擊事件
                //e.Row.Cells[3].Attributes.Add("onclick", e.Row.Cells[3].ClientID.ToString()
                //    + ".checked=true;CellClick('" + ImgUrl + "')");

                //  img.ImageUrl = "https://eip.tkfood.com.tw/BM/upload/note/20200926112527.jpg";
            }


        }


    }

    public void OnBeforeExport1(object sender, Ede.Uof.Utility.Component.BeforeExportEventArgs e)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ERPconnectionstring"].ToString();
        Ede.Uof.Utility.Data.DatabaseHelper m_db = new Ede.Uof.Utility.Data.DatabaseHelper(connectionString);


        string cmdTxt = @" 
                       SELECT [PRODUCTS].[MB001],[PRODUCTSFEATURES],[SALESFOCUS],[COPYWRITINGS],[PICPATHS]
                        ,MB002,MB003,MB004,MA003,ISNULL(MD007,0) AS MD007,CONVERT(NVARCHAR,MB023)+(CASE WHEN MB198='1' THEN '天' ELSE (CASE WHEN MB198='2' THEN '月' ELSE '年' END ) END ) AS 'VALIDITYPERIOD',CONVERT(decimal(16,3),ISNULL(MB047,0)) AS MB047,MB013
                        ,[ALBUM_GUID], [PHOTO_GUID],[PHOTO_DESC],[FILE_ID],[RESIZE_FILE_ID],[THUMBNAIL_FILE_ID]
                        FROM [TKBUSINESS].[dbo].[PRODUCTS]
                        LEFT JOIN [TK].dbo.[INVMB] ON [PRODUCTS].[MB001]=[INVMB].[MB001]
                        LEFT JOIN [TK].dbo.INVMA ON MA001='9' AND MA002=MB115
                        LEFT JOIN [TK].dbo.BOMMD ON MD001=[INVMB].[MB001] AND MD003 LIKE '201%'
                        LEFT JOIN [192.168.1.223].[UOF].[dbo].[TB_EIP_ALBUM_PHOTO] ON [PHOTO_TOPIC]=[PRODUCTS].[MB001] COLLATE Chinese_Taiwan_Stroke_BIN
                        ORDER BY [PRODUCTS].[MB001]
                        ";



        DataTable dt = new DataTable();

        dt.Load(m_db.ExecuteReader(cmdTxt));

        if (dt.Rows.Count > 0)
        {
            dt.Columns[0].Caption = "ID";


            e.Datasource = dt;
        }
    }

    //private void AddImage(ExcelWorksheet oSheet, int rowIndex, int colIndex, string imagePath)
    //{
    //    Bitmap image = new Bitmap(imagePath);
    //    ExcelPicture excelImage = null;
    //    if (image != null)
    //    {
    //        excelImage = oSheet.Drawings.AddPicture("Debopam Pal", image);
    //        excelImage.From.Column = colIndex;
    //        excelImage.From.Row = rowIndex;
    //        excelImage.SetSize(100, 100);
    //        //2x2 px space for better alignment
    //        excelImage.From.ColumnOff = Pixel2MTU(2);
    //        excelImage.From.RowOff = Pixel2MTU(2);
    //    }
    //}

    //public int Pixel2MTU(int pixels)
    //{
    //    int mtus = pixels * 9525;
    //    return mtus;
    //}

    public override void VerifyRenderingInServerForm(Control control) 
    { 

    }

    public void SETEXCEL()
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ERPconnectionstring"].ToString();
        Ede.Uof.Utility.Data.DatabaseHelper m_db = new Ede.Uof.Utility.Data.DatabaseHelper(connectionString);

        string cmdTxt = @" 
                        SELECT [PRODUCTS].[MB001],[PRODUCTSFEATURES],[SALESFOCUS],[COPYWRITINGS],[PICPATHS]
                        ,MB002,MB003,MB004,MA003,ISNULL(MD007,0) AS MD007,CONVERT(NVARCHAR,MB023)+(CASE WHEN MB198='1' THEN '天' ELSE (CASE WHEN MB198='2' THEN '月' ELSE '年' END ) END ) AS 'VALIDITYPERIOD',CONVERT(decimal(16,3),ISNULL(MB047,0)) AS MB047,MB013
                        ,[ALBUM_GUID], [PHOTO_GUID],[PHOTO_DESC],[FILE_ID],[RESIZE_FILE_ID],[THUMBNAIL_FILE_ID]
                        FROM [TKBUSINESS].[dbo].[PRODUCTS]
                        LEFT JOIN [TK].dbo.[INVMB] ON [PRODUCTS].[MB001]=[INVMB].[MB001]
                        LEFT JOIN [TK].dbo.INVMA ON MA001='9' AND MA002=MB115
                        LEFT JOIN [TK].dbo.BOMMD ON MD001=[INVMB].[MB001] AND MD003 LIKE '201%'
                        LEFT JOIN [192.168.1.223].[UOF].[dbo].[TB_EIP_ALBUM_PHOTO] ON [PHOTO_DESC] LIKE '%'+[PRODUCTS].[MB001]+'%' COLLATE Chinese_Taiwan_Stroke_BIN
                        ORDER BY [PRODUCTS].[MB001]
                        ";

        //m_db.AddParameter("@SDATE", SDATE);
        //m_db.AddParameter("@EDATE", EDATE);

        DataTable dt = new DataTable();

        dt.Load(m_db.ExecuteReader(cmdTxt));

        if(dt.Rows.Count>0)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial; // 關閉新許可模式通知
                                                                        // 沒設置的話會跳出 Please set the excelpackage.licensecontext property

            //檔案名稱
            var fileName = "ExampleExcel" + DateTime.Now.ToString("yyyy-MM-dd--hh-mm-ss") + ".xlsx";
            var file = new FileInfo(fileName);
            using (var excel = new ExcelPackage(file))
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

                    if(!string.IsNullOrEmpty(od["PHOTO_DESC"].ToString()))
                    {
                        //網路圖片
                        WebClient MyWebClient = new WebClient();
                        StringBuilder PATH = new StringBuilder();

                        PATH.AppendFormat(@"https://eip.tkfood.com.tw/UOF/common/filecenter/v3/handler/downloadhandler.ashx?id={0}&path=ALBUM%5C2021%5C03&contentType=image%2Fpng&name={1}
                                ", od["RESIZE_FILE_ID"].ToString(), od["PHOTO_DESC"].ToString());

                        //string fileURL = "https://eip.tkfood.com.tw/UOF/common/filecenter/v3/handler/downloadhandler.ashx?id=8b2a033b-c301-419b-938d-e6cfedf28b82&path=ALBUM%5C2021%5C03&contentType=image%2Fpng&name=40100010650490.png";
                        //string fileURL = "https://eip.tkfood.com.tw/UOF/common/filecenter/v3/handler/downloadhandler.ashx?id=2a44a870-f960-4178-9551-e9612fd46b30&path=ALBUM%5C2021%5C03&contentType=image%2Fpng&name=40100710430390.jpg";
                        string fileURL = PATH.ToString();

                        var pageData = MyWebClient.DownloadData(fileURL);

                        Stream imgms = new MemoryStream(pageData);
                        System.Drawing.Bitmap imgfs = new System.Drawing.Bitmap(imgms);

                        //MemoryStream fs = new MemoryStream();
                        //fs.Write(pageData, 0, pageData.Length - 1);
                        //var imgfs = System.Drawing.Image.FromStream(fs);
                        //fs.Close();

                        ExcelPicture picture= excel.Workbook.Worksheets[0].Drawings.AddPicture(od["MB001"].ToString(), imgfs);//插入圖片

                        //ExcelPicture picture = excel.Workbook.Worksheets[0].Drawings.AddPicture("logo", System.Drawing.Image.FromFile(@"https://eip.tkfood.com.tw/UOF/common/filecenter/v3/handler/downloadhandler.ashx?id=8b2a033b-c301-419b-938d-e6cfedf28b82&path=ALBUM%5C2021%5C03&contentType=image%2Fpng&name=40100010650490.png"));//插入圖片
                        //ExcelPicture picture = excel.Workbook.Worksheets[0].Drawings.AddPicture("logo", System.Drawing.Image.FromFile(@"C:\TEMP\40100010650490.png"));//插入圖片

                        picture.From.Row = ROWS;
                        picture.From.Column = COLUMNS;
                        
                        picture.SetPosition(1* ROWS-1,5, 3,0);//設置圖片的位置
                        picture.SetSize(50, 50);//設置圖片的大小
                    }

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
    #endregion
}