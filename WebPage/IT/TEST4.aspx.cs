using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Net;
using iTextSharp.text.html.simpleparser;
using iTextSharp.tool.xml;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Drawing;
using System.Drawing.Imaging;

public partial class CDS_WebPage_IT_TEST4 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }


    public void ConvertUrlToPdf(string url, string pdfFilePath)
    {
        // Create a new instance of ChromeDriver
        var driverService = ChromeDriverService.CreateDefaultService(@"C:\chromedriver_win32");
        var driverOptions = new ChromeOptions();
        var driver = new ChromeDriver(driverService, driverOptions);
        driverOptions.AddArgument("--headless"); // Run Chrome in headless mode
        driverOptions.AddArguments("--start-maximized"); // 窗口最大化

        try
        {
            // Navigate to the specified URL
            driver.Navigate().GoToUrl(url);

            // Wait for the page to load
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            //wait.Until(driver => driver.Url.Equals(url));

            // Take a screenshot of the entire page
            var screenshot = ((ITakesScreenshot)driver).GetScreenshot();

            // Convert the screenshot to PDF and save it to the specified file path
            var bitmap = new Bitmap(new MemoryStream(screenshot.AsByteArray));
            bitmap.Save(pdfFilePath, ImageFormat.Png);
        }
        catch (Exception ex)
        {
            // Handle any exceptions that occur
        }
        finally
        {
            // Close the browser
            driver.Quit();
        }
    }



    protected void Button1_Click(object sender, EventArgs e)
    {
        // 創建一個 WebRequest 對象，下載網頁
        string URL = "https://blog.darkthread.net/blog/iis-auth-ntlm-kerberos-net-conn/";
        //string URL = "http://localhost/TKUOF/WKF/FormUse/FormPrint.aspx?TASK_ID=be271321-13fd-4d16-922f-6d1dbeabf584&SHOW_FILLER=false&80961";
        
        StringBuilder FILE = new StringBuilder();
        FILE.AppendFormat(@"output{0}", DateTime.Now.ToString("yyyyMMddHHmmss"));
        string filePath = Server.MapPath("~/PDF/"+ FILE + ".Png"); // 使用相對路徑

        ConvertUrlToPdf(URL, filePath);
    }
}