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

public partial class CDS_WebPage_Mobile_SALES_RECORDS : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {        
        if (!IsPostBack)
        {
            RECORDSDATES.Text = DateTime.Now.ToString("yyyy/MM/dd");
        }
    }

    #region FUNCTION
   
    public static void ADD_TB_SALES_RECORDS(
        string SALESNAMES
        , string CLIENTSNAMES
        , string NEWCLIENTSNAMES
        , string RECORDS
        , string RECORDSDATES
        , byte[] PHOTOS)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ERPconnectionstring"].ToString();
        Ede.Uof.Utility.Data.DatabaseHelper m_db = new Ede.Uof.Utility.Data.DatabaseHelper(connectionString);

        string cmdTxt = @"                        
                        INSERT INTO [TKBUSINESS].[dbo].[TB_SALES_RECORDS]
                        (
                        [SALESNAMES]
                        ,[CLIENTSNAMES]
                        ,[NEWCLIENTSNAMES]
                        ,[RECORDS]
                        ,[RECORDSDATES]
                        ,[PHOTOS]
                        )
                        VALUES
                        (
                        @SALESNAMES
                        ,@CLIENTSNAMES
                        ,@NEWCLIENTSNAMES
                        ,@RECORDS
                        ,@RECORDSDATES
                        ,@PHOTOS
                        )
                            ";


        m_db.AddParameter("@SALESNAMES", SALESNAMES);
        m_db.AddParameter("@CLIENTSNAMES", CLIENTSNAMES);
        m_db.AddParameter("@NEWCLIENTSNAMES", NEWCLIENTSNAMES);
        m_db.AddParameter("@RECORDS", RECORDS);
        m_db.AddParameter("@RECORDSDATES", RECORDSDATES);
        m_db.AddParameter("@PHOTOS", PHOTOS);


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
        , string CLIENTSNAMES
        , string NEWCLIENTSNAMES
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
                     , CLIENTSNAMES
                     , NEWCLIENTSNAMES
                     , RECORDS
                     , RECORD2DATES
                     , imageBytes2);

            string MESSAGE = NOWTIMES + " 成功 存檔 ";
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


    #endregion


    #region BUTTON
    protected void Button1_Click(object sender, EventArgs e)
    {

    }

    #endregion


}