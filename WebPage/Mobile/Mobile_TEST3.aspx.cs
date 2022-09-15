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

        ////Convert Base64 Encoded string to Byte Array.
        byte[] imageBytes = Convert.FromBase64String(data.Split(',')[1]);

        STATICADDTKGAFFAIRSCHECKSPOOINTPHOTO(myTextcontent, NOWTIMES, imageBytes);

        ////Save the Byte Array as Image File.
        //string filePath = HttpContext.Current.Server.MapPath(string.Format("~/Captures/{0}.jpg", fileName));
        //File.WriteAllBytes(filePath, imageBytes);
        //return true;

        string MESSAGE = imageBytes.Length.ToString()+" "+ fileName + " 拍照成功";
        return MESSAGE;
    }

    #endregion

    #region BUTTON

    /// <summary>
    /// 手動打卡
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected  void Button1_Click(object sender, EventArgs e)
    {
        string CHECKSPOINT = myTextcontent.Text.ToString();

        Label3.Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
        string CHECKSTIME = Label3.Text.ToString();

        ADDTKGAFFAIRSCHECKSPOOINT(CHECKSPOINT, CHECKSTIME);       
    }
    #endregion
}