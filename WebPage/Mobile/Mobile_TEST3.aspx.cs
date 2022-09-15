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

public partial class CDS_WebPage_Mobile_Mobile_TEST3 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        //頁面開啟時，顯示目前的時間
        Label3.Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");

        if (!IsPostBack)
        {

        }
    }

    #region FUNCTION

    
    /// <summary>
    /// 手動新增打卡記錄
    /// </summary>
    /// <param name="CHECKSPOINT"></param>
    /// <param name="CHECKSTIME"></param>
    public void ADDTKGAFFAIRSCHECKSPOOINT(string CHECKSPOINT, string CHECKSTIME)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ERPconnectionstring"].ToString();
        Ede.Uof.Utility.Data.DatabaseHelper m_db = new Ede.Uof.Utility.Data.DatabaseHelper(connectionString);

        string cmdTxt = @"  
                       INSERT INTO [TKGAFFAIRS].[dbo].[CHECKSPOOINT]
                        ([CHECKSPOINT],[CHECKSTIME])
                        VALUES
                        (@CHECKSPOINT,@CHECKSTIME)
                            ";


        m_db.AddParameter("@CHECKSPOINT", CHECKSPOINT);
        m_db.AddParameter("@CHECKSTIME", CHECKSTIME);


        m_db.ExecuteNonQuery(cmdTxt);

        Label4.Text = CHECKSPOINT+" "+ CHECKSTIME+ " 完成" +"";
    }

    /// <summary>
    /// 宣告static給WebMethod用
    /// 在到qrcode後，就自動打卡記錄
    /// </summary>
    /// <param name="CHECKSPOINT"></param>
    /// <param name="CHECKSTIME"></param>
    public static void STATICADDTKGAFFAIRSCHECKSPOOINT(string CHECKSPOINT, string CHECKSTIME)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ERPconnectionstring"].ToString();
        Ede.Uof.Utility.Data.DatabaseHelper m_db = new Ede.Uof.Utility.Data.DatabaseHelper(connectionString);

        string cmdTxt = @"  
                       INSERT INTO [TKGAFFAIRS].[dbo].[CHECKSPOOINT]
                        ([CHECKSPOINT],[CHECKSTIME])
                        VALUES
                        (@CHECKSPOINT,@CHECKSTIME)
                            ";


        m_db.AddParameter("@CHECKSPOINT", CHECKSPOINT);
        m_db.AddParameter("@CHECKSTIME", CHECKSTIME);


        m_db.ExecuteNonQuery(cmdTxt);
        
    }

    public static void STATICADDTKGAFFAIRSCHECKSPOOINTPHOTO(string CHECKSPOINT, string CHECKSTIME, byte[] PHOTOS)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ERPconnectionstring"].ToString();
        Ede.Uof.Utility.Data.DatabaseHelper m_db = new Ede.Uof.Utility.Data.DatabaseHelper(connectionString);

        string cmdTxt = @"  
                       INSERT INTO [TKGAFFAIRS].[dbo].[CHECKSPOOINTPHOTO]
                        ([CHECKSPOINT],[CHECKSTIME],[PHOTOS])
                        VALUES
                        (@CHECKSPOINT,@CHECKSTIME,@PHOTOS)
                            ";


        m_db.AddParameter("@CHECKSPOINT", CHECKSPOINT);
        m_db.AddParameter("@CHECKSTIME", CHECKSTIME);
        m_db.AddParameter("@PHOTOS", PHOTOS);


        m_db.ExecuteNonQuery(cmdTxt);

    }


    [WebMethod]
    public static string Name()
    {
        string MESSAGE = "OK";
        return MESSAGE;
    }
    /// <summary>
    /// 呼叫WebMethod的TAKEPIC_ADDTKGAFFAIRSCHECKSPOOINT
    /// </summary>
    /// <param name="myTextcontent"></param>
    /// <returns></returns>
    [WebMethod]
    public static string TAKEPIC_ADDTKGAFFAIRSCHECKSPOOINT(string myTextcontent)
    {
        string NOWTIMES = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
        STATICADDTKGAFFAIRSCHECKSPOOINT(myTextcontent, NOWTIMES);

        string MESSAGE = myTextcontent+" "+ NOWTIMES + "打卡成功";
        return MESSAGE;
    }


    [WebMethod()]
    public static string SaveCapturedImage(string myTextcontent,string data)
    {
        string NOWTIMES = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");      
        string fileName = DateTime.Now.ToString("yyyyMMddHHmmss");

        string ORI;
        string COM;
        ////Convert Base64 Encoded string to Byte Array.
        byte[] imageBytes = Convert.FromBase64String(data.Split(',')[1]);
        ORI = imageBytes.Length.ToString();

        byte[] imageBytes2 = CutImage(imageBytes,50, 50);
        COM = imageBytes2.Length.ToString();

        STATICADDTKGAFFAIRSCHECKSPOOINTPHOTO(myTextcontent, NOWTIMES, imageBytes2);

        ////Save the Byte Array as Image File.
        //string filePath = HttpContext.Current.Server.MapPath(string.Format("~/Captures/{0}.jpg", fileName));
        //File.WriteAllBytes(filePath, imageBytes);
        //return true;

        string MESSAGE = ORI +" / "+ COM + " "+ fileName + " 拍照成功";
        return MESSAGE;
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


    #endregion

    #region BUTTON

    /// <summary>
    /// 手動打卡
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button1_Click(object sender, EventArgs e)
    {
        string CHECKSPOINT = myTextcontent.Text.ToString();

        Label3.Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
        string CHECKSTIME = Label3.Text.ToString();

        ADDTKGAFFAIRSCHECKSPOOINT(CHECKSPOINT, CHECKSTIME);       
    }
    #endregion
}