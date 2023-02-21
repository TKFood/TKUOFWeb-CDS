using System;
using System.Data;
using System.Web;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.IO;
using System.Windows.Forms;
using System.Threading;
using System.Text;
using System.Net;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading.Tasks;

public partial class CDS_WebPage_IT_TEST6 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        // 創建一個 WebRequest 對象，下載網頁
        //string URL = "http://localhost/TKUOF/CDS/WebPage/IT/TEXT5.aspx";
        //string URL = "https://eip.tkfood.com.tw/UOFTEST/WKF/FormUse/FormPrint.aspx?TASK_ID=be271321-13fd-4d16-922f-6d1dbeabf584";

        string URL = "https://www.google.com/";

        StringBuilder FILE = new StringBuilder();
        FILE.AppendFormat(@"output{0}", DateTime.Now.ToString("yyyyMMddHHmmss"));
        string filePath = Server.MapPath("~/PDF/" + FILE + ".Bmp"); // 使用相對路徑   

     


    }

   

}
