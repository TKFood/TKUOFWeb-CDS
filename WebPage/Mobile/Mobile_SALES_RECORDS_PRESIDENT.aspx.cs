using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Dynamic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security.AntiXss;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ede.Uof.Utility.Data;
using Ede.Uof.Utility.Page.Common;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using OfficeOpenXml;
using OfficeOpenXml.Drawing;
using OfficeOpenXml.Style;
using System.Web.Services;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using Ede.Uof.EIP.SystemInfo;

public partial class CDS_WebPage_Mobile_SALES_RECORDS_PRESIDENT : Ede.Uof.Utility.Page.BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //不顯示子視窗的按鈕
        ((Master_MobileMasterPage)this.Master).Button1Text = string.Empty;
        ((Master_MobileMasterPage)this.Master).Button2Text = string.Empty;
        ((Master_MobileMasterPage)this.Master).Button3Text = string.Empty;

        if (!IsPostBack)
        {
            ViewState["ACCOUNT"] = null;
            ViewState["NAME"] = null;

            txtDate1.Text = DateTime.Now.AddDays(-7).ToString("yyyy/MM/dd");
            txtDate2.Text = DateTime.Now.ToString("yyyy/MM/dd");

            RECORDSDATES.Text = DateTime.Now.ToString("yyyy/MM/dd");
            BindDropDownList1();
            BindDropDownList3();

            ViewState["ACCOUNT"] = Current.Account;
            ViewState["NAME"] = Current.User.Name;
            SALESID.Text = ViewState["ACCOUNT"].ToString();
            // 使用 FindByText 方法來尋找並指定選項
            ListItem item = DropDownListSALESNAMES.Items.FindByText(ViewState["NAME"].ToString());
            if (item != null)
            {
                // 找到了選項，將其設定為所選
                DropDownListSALESNAMES.ClearSelection(); // 清除所有選擇
                item.Selected = true;

                BindDropDownList2("");
            }
            else
            {
                // 如果找不到匹配的選項，可以執行適當的處理
                // 例如，顯示一條錯誤消息或執行其他操作
            }
        }




    }

    #region FUNCTION
    private void BindDropDownList1()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("SALESID", typeof(String));
        dt.Columns.Add("SALESNAMES", typeof(String));

        string connectionString = ConfigurationManager.ConnectionStrings["ERPconnectionstring"].ToString();
        Ede.Uof.Utility.Data.DatabaseHelper m_db = new Ede.Uof.Utility.Data.DatabaseHelper(connectionString);

        string cmdTxt = @" SELECT 
                            [SALESID]
                            ,[SALESNAMES]
                            FROM [TKBUSINESS].[dbo].[TB_SALESNAMES] ";

        dt.Load(m_db.ExecuteReader(cmdTxt));

        if (dt.Rows.Count > 0)
        {
            DropDownListSALESNAMES.DataSource = dt;
            DropDownListSALESNAMES.DataTextField = "SALESNAMES";
            DropDownListSALESNAMES.DataValueField = "SALESID";
            DropDownListSALESNAMES.DataBind();

        }
        else
        {

        }
    }
    protected void DropDownListSALESNAMES_SelectedIndexChanged(object sender, EventArgs e)
    {
        // 獲取所選的值
        string selectedValue = DropDownListSALESNAMES.SelectedValue;
        SALESID.Text = selectedValue;
        ViewState["ACCOUNT"] = selectedValue;
        BindDropDownList2(ViewState["ACCOUNT"].ToString());

        // 執行其他操作，例如根據所選值更新頁面或處理伺服器端邏輯
    }

    private void BindDropDownList2(string MA002)
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("MA001", typeof(String));
        dt.Columns.Add("MA002", typeof(String));

        string connectionString = ConfigurationManager.ConnectionStrings["ERPconnectionstring"].ToString();
        Ede.Uof.Utility.Data.DatabaseHelper m_db = new Ede.Uof.Utility.Data.DatabaseHelper(connectionString);

        StringBuilder cmdTxt = new StringBuilder();
        cmdTxt.AppendFormat(@"
                            SELECT *
                            FROM 
                            (
                            SELECT '' MA001,'-請選擇'MA002
                            UNION ALL
                            SELECT MA001,MA002
                            FROM [TK].dbo.COPMA
                            WHERE MA002 LIKE '%{0}%'
                            AND (MA001 LIKE '2%' OR MA001 LIKE '3%' OR MA001 LIKE 'A%' OR MA001 LIKE 'B%')
                            AND MA002 NOT LIKE '%停用%'
                            )
                            AS TEMP
                            ORDER BY MA002
                            ", MA002);



        dt.Load(m_db.ExecuteReader(cmdTxt.ToString()));

        if (dt.Rows.Count > 0)
        {
            DropDownListCLIENTSNAMES.DataSource = dt;
            DropDownListCLIENTSNAMES.DataTextField = "MA002";
            DropDownListCLIENTSNAMES.DataValueField = "MA001";
            DropDownListCLIENTSNAMES.DataBind();

        }
        else
        {

        }
    }
    protected void DropDownListCLIENTSNAMES_SelectedIndexChanged(object sender, EventArgs e)
    {
        // 獲取所選的值
        string selectedValue = DropDownListCLIENTSNAMES.SelectedValue;
        CLIENTSID.Text = selectedValue;

        // 執行其他操作，例如根據所選值更新頁面或處理伺服器端邏輯
    }
    private void BindDropDownList3()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("ID", typeof(String));
        dt.Columns.Add("KINDS", typeof(String));

        string connectionString = ConfigurationManager.ConnectionStrings["ERPconnectionstring"].ToString();
        Ede.Uof.Utility.Data.DatabaseHelper m_db = new Ede.Uof.Utility.Data.DatabaseHelper(connectionString);

        string cmdTxt = @" 
                        SELECT 
                        [ID]
                        ,[KINDS]
                        FROM [TKBUSINESS].[dbo].[TB_SALES_KINDS]
                        ORDER BY [ID]
                        ";

        dt.Load(m_db.ExecuteReader(cmdTxt));

        if (dt.Rows.Count > 0)
        {
            DropDownListKINDS.DataSource = dt;
            DropDownListKINDS.DataTextField = "KINDS";
            DropDownListKINDS.DataValueField = "KINDS";
            DropDownListKINDS.DataBind();

        }
        else
        {

        }
    }
 
    public static void ADD_TB_SALES_RECORDS(
        string SALESNAMES
        , string CLIENTSID
        , string CLIENTSNAMES
        , string NEWCLIENTSNAMES
        , string KINDS
        , string RECORDS
        , string RECORDSDATES
        , byte[] PHOTOS)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ERPconnectionstring"].ToString();
        Ede.Uof.Utility.Data.DatabaseHelper m_db = new Ede.Uof.Utility.Data.DatabaseHelper(connectionString);

        string cmdTxt = @"   ";

        if (CLIENTSNAMES.Contains("請選擇"))
        {
            CLIENTSNAMES = "";
        }
        if (PHOTOS != null)
        {
            cmdTxt = @"
                        INSERT INTO [TKBUSINESS].[dbo].[TB_SALES_RECORDS]
                        (
                        [SALESNAMES]
                        ,[CLIENTSID]
                        ,[CLIENTSNAMES]
                        ,[NEWCLIENTSNAMES]
                        ,[KINDS]
                        ,[RECORDS]
                        ,[RECORDSDATES]
                        ,[PHOTOS]
                        )
                        VALUES
                        (
                        @SALESNAMES
                        ,@CLIENTSID
                        ,@CLIENTSNAMES
                        ,@NEWCLIENTSNAMES
                        ,@KINDS
                        ,@RECORDS
                        ,@RECORDSDATES
                        ,@PHOTOS
                        )
                            ";


            m_db.AddParameter("@SALESNAMES", SALESNAMES);
            m_db.AddParameter("@CLIENTSID", CLIENTSID);
            m_db.AddParameter("@CLIENTSNAMES", CLIENTSNAMES);
            m_db.AddParameter("@NEWCLIENTSNAMES", NEWCLIENTSNAMES);
            m_db.AddParameter("@KINDS", KINDS);
            m_db.AddParameter("@RECORDS", RECORDS);
            m_db.AddParameter("@RECORDSDATES", RECORDSDATES);
            m_db.AddParameter("@PHOTOS", PHOTOS);
        }
        else
        {
            cmdTxt = @"                        
                        INSERT INTO [TKBUSINESS].[dbo].[TB_SALES_RECORDS]
                        (
                        [SALESNAMES]
                        ,[CLIENTSID]
                        ,[CLIENTSNAMES]
                        ,[NEWCLIENTSNAMES]
                        ,[RECORDS]
                        ,[RECORDSDATES]              
                        )
                        VALUES
                        (
                        @SALESNAMES
                        ,@CLIENTSID
                        ,@CLIENTSNAMES
                        ,@NEWCLIENTSNAMES
                        ,@RECORDS
                        ,@RECORDSDATES                       
                        )
                            ";


            m_db.AddParameter("@SALESNAMES", SALESNAMES);
            m_db.AddParameter("@CLIENTSID", CLIENTSID);
            m_db.AddParameter("@CLIENTSNAMES", CLIENTSNAMES);
            m_db.AddParameter("@NEWCLIENTSNAMES", NEWCLIENTSNAMES);
            m_db.AddParameter("@RECORDS", RECORDS);
            m_db.AddParameter("@RECORDSDATES", RECORDSDATES);

        }



        m_db.ExecuteNonQuery(cmdTxt);
    }

    [WebMethod()]
    public static string TEST()
    {
        string NOWTIMES = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
        string MESSAGE = NOWTIMES + " 成功";
        return MESSAGE;
    }
    [WebMethod()]
    public static string SaveCapturedImage(
        string SALESNAMES
        , string CLIENTSID
        , string CLIENTSNAMES
        , string NEWCLIENTSNAMES
        , string KINDS
        , string RECORDS
        , string RECORD2DATES
        , string data)
    {
        string NOWTIMES = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");

        string ORI1 = "";
        string ORI2 = "";
        string ORI3 = "";
        ////Convert Base64 Encoded string to Byte Array.       
        byte[] imageBytes = Convert.FromBase64String(data.Split(',')[1]);
        ORI1 = imageBytes.Length.ToString();
        //加上浮水印
        byte[] imageBytes2 = GetWatermarkPic(imageBytes, DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
        ORI2 = imageBytes2.Length.ToString();
        ////壓縮圖片
        //byte[] imageBytes3 = CutImage(imageBytes2, 50, 50);
        //ORI3 = imageBytes3.Length.ToString();

        try
        {
            ADD_TB_SALES_RECORDS(
                     SALESNAMES
                     , CLIENTSID
                     , CLIENTSNAMES
                     , NEWCLIENTSNAMES
                     , KINDS
                     , RECORDS
                     , RECORD2DATES
                     , imageBytes2);

            string MESSAGE = NOWTIMES + " 成功 照片 存檔 ";
            return MESSAGE;
        }
        catch
        {
            string MESSAGE = " 失敗 存檔";
            return MESSAGE;
        }
        finally { }

    }

    [WebMethod()]
    public static string SaveCapturedImage_NOIMAGE(
      string SALESNAMES
      , string CLIENTSID
      , string CLIENTSNAMES
      , string NEWCLIENTSNAMES
      , string KINDS
      , string RECORDS
      , string RECORD2DATES
      )
    {
        string NOWTIMES = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
        
        try
        {
            ADD_TB_SALES_RECORDS(
                     SALESNAMES
                     , CLIENTSID
                     , CLIENTSNAMES
                     , NEWCLIENTSNAMES
                     , KINDS
                     , RECORDS
                     , RECORD2DATES
                     , null);

            string MESSAGE = NOWTIMES + " 成功 無照片 存檔 ";
            return MESSAGE;
        }
        catch
        {
            string MESSAGE = " 失敗 存檔";
            return MESSAGE;
        }
        finally { }

    }
    /// <summary>
    /// 生成縮略圖
    /// </summary>
    /// <param name="srcImageStream">原始圖片的位元組陣列</param>
    /// <param name="width">最終的圖片寬度</param>
    /// <param name="height">最終的圖片高度</param>
    /// <returns>壓縮後的base64字串</returns>
    public static byte[] CutImage(byte[] srcImageStream, int width, int height)
    {
        MemoryStream msSource = new MemoryStream(srcImageStream);
        Bitmap btImage = new Bitmap(msSource);
        msSource.Close();
        System.Drawing.Image serverImage = btImage;
        //畫板大小
        int finalWidth = width, finalHeight = height;
        int srcImageWidth = serverImage.Width;
        int srcImageHeight = serverImage.Height;
        if (srcImageWidth > srcImageHeight)
        {
            finalHeight = srcImageHeight * width / srcImageWidth;
        }
        else
        {
            finalWidth = srcImageWidth * height / srcImageHeight;
        }
        //新建一個bmp圖片
        System.Drawing.Image newImage = new Bitmap(width, height);
        //新建一個畫板
        Graphics g = Graphics.FromImage(newImage);
        //設置高品質插值法
        g.InterpolationMode = InterpolationMode.High;
        //設置高品質,低速度呈現平滑程度
        g.SmoothingMode = SmoothingMode.HighQuality;
        //清空畫布並以透明背景色填充
        g.Clear(Color.White);
        //在指定位置並且按指定大小繪製原圖片的指定部分
        g.DrawImage(serverImage, new Rectangle((width - finalWidth) / 2, (height - finalHeight) / 2, finalWidth, finalHeight), 0, 0, srcImageWidth, srcImageHeight, GraphicsUnit.Pixel);
        //以jpg格式保存縮略圖
        MemoryStream msSaveImage = new MemoryStream();
        newImage.Save(msSaveImage, ImageFormat.Jpeg);
        serverImage.Dispose();
        newImage.Dispose();
        g.Dispose();
        byte[] imageBytes = msSaveImage.ToArray();
        msSaveImage.Close();

        return imageBytes;
    }

    ///給圖片加浮水印
    public static byte[] GetWatermarkPic(byte[] bytes, string txt)
    {
        MemoryStream ms = new MemoryStream(bytes);
        System.Drawing.Image pic = System.Drawing.Image.FromStream(ms);
        //創建一把刷子
        //防止索引圖元格式引發異常的圖元格式
        Bitmap bmp = new Bitmap(pic.Width, pic.Height, PixelFormat.Format32bppArgb);
        Graphics grap = Graphics.FromImage(bmp);
        grap.DrawImage(pic, 0, 0, pic.Width, pic.Height);
        //計算字體大小
        int fontSize = 40;
        if (pic.Width <= txt.Length * fontSize)
        {
            while (pic.Width <= (txt.Length * fontSize + 10))
            {
                fontSize = fontSize - 2;
            }
        }
        //浮水印寬度
        int txtWidth = txt.Length * fontSize;
        //字體
        var font = new Font("黑體", fontSize);
        Brush brush = new SolidBrush(Color.FromArgb(120, 193, 143, 8));
        grap.RotateTransform(0);
        grap.DrawString(txt, font, brush, float.Parse((pic.Width / 2 - txtWidth * 0.3).ToString()), 0);
        grap.Dispose();
        //返回有浮水印的圖片流
        MemoryStream stream = new MemoryStream();
        bmp.Save(stream, ImageFormat.Png);
        return stream.GetBuffer();
    }

    private void BindGrid(string SDAYS, string EDAYS)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ERPconnectionstring"].ToString();
        Ede.Uof.Utility.Data.DatabaseHelper m_db = new Ede.Uof.Utility.Data.DatabaseHelper(connectionString);

        StringBuilder cmdTxt = new StringBuilder();

        cmdTxt.AppendFormat(@" 
                            SELECT 
                            [ID]
                            ,CONVERT(nvarchar,[CREATDATES],111) AS '建立日期'
                            ,[SALESNAMES] AS '業務員'
                            ,[CLIENTSID] AS '客戶代號'
                            ,[CLIENTSNAMES] AS '客戶'
                            ,[NEWCLIENTSNAMES] AS '新客'
                            ,[KINDS] AS '拜訪目的'
                            ,[RECORDS] AS '訪談內容'
                            ,CONVERT(nvarchar,[RECORDSDATES],111) AS '訪談日期'
                            ,[PHOTOS]
                            FROM [TKBUSINESS].[dbo].[TB_SALES_RECORDS]
                            WHERE CONVERT(nvarchar,[RECORDSDATES],111)>='{0}' AND CONVERT(nvarchar,[RECORDSDATES],111)<='{1}'
                            ORDER BY [SALESNAMES],[CLIENTSNAMES],[ID]

                              
                            ", SDAYS, EDAYS);


        //m_db.AddParameter("@EDATE", EDATE);

        DataTable dt = new DataTable();

        dt.Load(m_db.ExecuteReader(cmdTxt.ToString()));

        Grid1.DataSource = dt;
        Grid1.DataBind();
    }

    protected void grid_PageIndexChanging1(object sender, GridViewPageEventArgs e)
    {
        //Grid1.PageIndex = e.NewPageIndex;
        //BindGrid("");
    }
    protected void Grid1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView dr = (DataRowView)e.Row.DataItem;
            System.Web.UI.WebControls.Image imgPhoto = e.Row.FindControl("Image1") as System.Web.UI.WebControls.Image;


            if (dr["PHOTOS"] != DBNull.Value && dr["PHOTOS"] != null)
            {
                string imageUrl = "data:image/jpg;base64," + Convert.ToBase64String((byte[])dr["PHOTOS"]);
                imgPhoto.ImageUrl = imageUrl;
            }
            else
            {
                // 如果PHOTOS字段为空或NULL，清空图像
                imgPhoto.ImageUrl = string.Empty;
            }

        }

    }

    protected void Grid1_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
    }

    public void OnBeforeExport1(object sender, Ede.Uof.Utility.Component.BeforeExportEventArgs e)
    {
        SETEXCEL(txtDate1.Text, txtDate2.Text);


    }


    public override void VerifyRenderingInServerForm(Control control)
    {

    }

    public void SETEXCEL(string SDAYS, string EDAYS)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ERPconnectionstring"].ToString();
        Ede.Uof.Utility.Data.DatabaseHelper m_db = new Ede.Uof.Utility.Data.DatabaseHelper(connectionString);

        StringBuilder cmdTxt = new StringBuilder();
        StringBuilder QUERYS = new StringBuilder();

        int imageCounter = 1; // 用于生成唯一图像名称的计数器

        QUERYS.AppendFormat(@" ");

        cmdTxt.AppendFormat(@" 
                            SELECT 
                            [SALESNAMES] AS '業務員'
                            ,[CLIENTSNAMES] AS '客戶'
                            ,[NEWCLIENTSNAMES] AS '新客'
                            ,[KINDS] AS '拜訪目的'
                            ,[RECORDS] AS '訪談內容'
                            ,CONVERT(nvarchar,[RECORDSDATES],111) AS '訪談日期'
                            ,[PHOTOS]
                            ,[ID]
                            ,CONVERT(nvarchar,[CREATDATES],111) AS '建立日期'                            
                            ,[CLIENTSID] AS '客戶代號'
                            FROM [TKBUSINESS].[dbo].[TB_SALES_RECORDS]
                            WHERE CONVERT(nvarchar,[RECORDSDATES],111)>='{0}' AND CONVERT(nvarchar,[RECORDSDATES],111)<='{1}'
                            ORDER BY [SALESNAMES],[CLIENTSNAMES],[ID]

                              
                            ", SDAYS, EDAYS);


        DataTable dt = new DataTable();

        dt.Load(m_db.ExecuteReader(cmdTxt.ToString()));

        if (dt.Rows.Count > 0)
        {
            //檔案名稱
            var fileName = "拜訪清單" + DateTime.Now.ToString("yyyy-MM-dd--hh-mm-ss") + ".xlsx";
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
                ws.Cells[1, 1].Value = "業務員";
                ws.Cells[1, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center; //欄位置中
                ws.Cells[1, 1].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center; //高度置中
                ws.Cells[1, 1].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin); //儲存格框線
                ws.Cells[1, 2].Value = "客戶";
                ws.Cells[1, 2].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center; //欄位置中
                ws.Cells[1, 2].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center; //高度置中
                ws.Cells[1, 2].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin); //儲存格框線
                ws.Cells[1, 3].Value = "新客";
                ws.Cells[1, 3].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center; //欄位置中
                ws.Cells[1, 3].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center; //高度置中
                ws.Cells[1, 3].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin); //儲存格框線
                ws.Cells[1, 4].Value = "拜訪目的";
                ws.Cells[1, 4].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center; //欄位置中
                ws.Cells[1, 4].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center; //高度置中
                ws.Cells[1, 4].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin); //儲存格框線
                ws.Cells[1, 5].Value = "訪談內容";
                ws.Cells[1, 5].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center; //欄位置中
                ws.Cells[1, 5].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center; //高度置中
                ws.Cells[1, 5].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin); //儲存格框線
                ws.Cells[1, 6].Value = "訪談日期";
                ws.Cells[1, 6].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center; //欄位置中
                ws.Cells[1, 6].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center; //高度置中
                ws.Cells[1, 6].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin); //儲存格框線
                ws.Cells[1, 7].Value = "照片";
                ws.Cells[1, 7].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center; //欄位置中
                ws.Cells[1, 7].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center; //高度置中
                ws.Cells[1, 7].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin); //儲存格框線
                ws.Cells[1, 8].Value = "ID";
                ws.Cells[1, 8].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center; //欄位置中
                ws.Cells[1, 8].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center; //高度置中
                ws.Cells[1, 8].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin); //儲存格框線
                ws.Cells[1, 9].Value = "建立日期";
                ws.Cells[1, 9].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center; //欄位置中
                ws.Cells[1, 9].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center; //高度置中
                ws.Cells[1, 9].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin); //儲存格框線
                ws.Cells[1, 10].Value = "客戶代號";
                ws.Cells[1, 10].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center; //欄位置中
                ws.Cells[1, 10].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center; //高度置中
                ws.Cells[1, 10].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin); //儲存格框線

                foreach (DataRow od in dt.Rows)
                {
                    ws.Cells[ROWS, 1].Value = od["業務員"].ToString();
                    ws.Cells[ROWS, 1].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center; //高度置中
                    ws.Cells[ROWS, 1].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin); //儲存格框線
                    ws.Cells[ROWS, 2].Value = od["客戶"].ToString();
                    ws.Cells[ROWS, 2].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center; //高度置中
                    ws.Cells[ROWS, 2].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin); //儲存格框線
                    ws.Cells[ROWS, 3].Value = od["新客"].ToString();
                    ws.Cells[ROWS, 3].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center; //高度置中
                    ws.Cells[ROWS, 3].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin); //儲存格框線
                    ws.Cells[ROWS, 4].Value = od["拜訪目的"].ToString();
                    ws.Cells[ROWS, 4].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center; //高度置中
                    ws.Cells[ROWS, 4].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin); //儲存格框線
                    ws.Cells[ROWS, 5].Value = od["訪談內容"].ToString();
                    ws.Cells[ROWS, 5].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center; //高度置中
                    ws.Cells[ROWS, 5].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin); //儲存格框線
                    ws.Cells[ROWS, 6].Value = od["訪談日期"].ToString();
                    ws.Cells[ROWS, 6].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center; //高度置中
                    ws.Cells[ROWS, 6].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin); //儲存格框線
                    //ws.Cells[ROWS, 7].Value = od["照片"].ToString();
                    //ws.Cells[ROWS, 7].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center; //高度置中
                    ws.Cells[ROWS, 7].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin); //儲存格框線
                    try
                    {
                        if (od["PHOTOS"] != DBNull.Value && od["PHOTOS"] != null)
                        {
                            byte[] imageBytes = (byte[])od["PHOTOS"];

                            using (MemoryStream ms = new MemoryStream(imageBytes))
                            {
                                System.Drawing.Image img = System.Drawing.Image.FromStream(ms);
                                string imageName = od["ID"].ToString() + "_Image" + imageCounter; // 生成唯一的图像名称
                                ExcelPicture picture = excel.Workbook.Worksheets[0].Drawings.AddPicture(imageName, img);

                                picture.From.Row = ROWS;
                                picture.From.Column = COLUMNS;

                                picture.SetPosition(1 * ROWS - 1, 5, 6, 5);
                                picture.SetSize(50, 50);

                                imageCounter++; // 递增计数器以确保下一个图像具有唯一的名称
                            }
                        }
                    }
                    catch
                    {

                    }
                    finally
                    {

                    }

                    ws.Cells[ROWS, 8].Value = od["ID"].ToString();
                    ws.Cells[ROWS, 8].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center; //高度置中
                    ws.Cells[ROWS, 8].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin); //儲存格框線
                    ws.Cells[ROWS, 9].Value = od["建立日期"].ToString();
                    ws.Cells[ROWS, 9].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center; //高度置中
                    ws.Cells[ROWS, 9].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin); //儲存格框線
                    ws.Cells[ROWS, 10].Value = od["客戶代號"].ToString();
                    ws.Cells[ROWS, 10].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center; //高度置中
                    ws.Cells[ROWS, 10].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin); //儲存格框線



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
    protected void Button1_Click(object sender, EventArgs e)
    {

    }
    protected void btn1_Click(object sender, EventArgs e)
    {
        string MA002 = TextBox1.Text.Trim().ToString();
        BindDropDownList2(MA002);
    }
    protected void btn2_Click(object sender, EventArgs e)
    {
        BindGrid(txtDate1.Text, txtDate2.Text);
    }
    protected void btn3_Click(object sender, EventArgs e)
    {
        SETEXCEL(txtDate1.Text, txtDate2.Text);
    }

    #endregion


}