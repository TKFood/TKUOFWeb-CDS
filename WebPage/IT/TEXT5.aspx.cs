using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Net;
using iTextSharp.text.html.simpleparser;
using iTextSharp.tool.xml;

public partial class CDS_WebPage_IT_TEXT5 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

   
    

    protected void Button1_Click(object sender, EventArgs e)
    {
        // 創建一個 WebRequest 對象，下載網頁
        //string URL = "http://localhost/TKUOF/CDS/WebPage/IT/TEXT5.aspx";
        string URL = "http://localhost/TKUOF/WKF/FormUse/FormPrint.aspx?TASK_ID=be271321-13fd-4d16-922f-6d1dbeabf584&SHOW_FILLER=false&80961";

        StringBuilder FILE = new StringBuilder();
        FILE.AppendFormat(@"output{0}", DateTime.Now.ToString("yyyyMMddHHmmss"));
        string filePath = Server.MapPath("~/PDF/" + FILE + ".pdf"); // 使用相對路徑   

     
        // 取得網頁 HTML 內容
        string html = GetHtmlFromUrl(URL);

        // 移除網頁中的圖片標籤
        html = RemoveHtmlTags(html, "img");

        // 將 HTML 內容轉換成 PDF
        byte[] pdfBytes = ConvertHtmlToPdf(html);

        // 將 PDF 寫入檔案
        WritePdfToFile(pdfBytes, filePath);

        // 將 PDF 輸出到瀏覽器
        Response.Clear();
        Response.ContentType = "application/pdf";
        Response.AddHeader("Content-Disposition", "attachment;filename=" + filePath);
        Response.OutputStream.Write(pdfBytes, 0, pdfBytes.Length);
        Response.End();

    }

    // 取得指定 URL 的 HTML 內容
    private string GetHtmlFromUrl(string url)
    {
        using (WebClient client = new WebClient())
        {
            return client.DownloadString(url);
        }
    }

    // 移除 HTML 中指定的標籤
    private string RemoveHtmlTags(string html, string tagName)
    {
        return System.Text.RegularExpressions.Regex.Replace(html, string.Format("<{0}[^>]*>", tagName), "");
    }

    // 將 HTML 內容轉換成 PDF
    private byte[] ConvertHtmlToPdf(string html)
    {
        using (MemoryStream memoryStream = new MemoryStream())
        {
            using (Document document = new Document())
            {
                PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);
                document.Open();
                using (StringReader stringReader = new StringReader(html))
                {
                    XMLWorkerHelper.GetInstance().ParseXHtml(writer, document, stringReader);
                }
            }
            return memoryStream.ToArray();
        }
    }

    // 將 PDF 寫入檔案
    private void WritePdfToFile(byte[] pdfBytes, string pdfFileName)
    {
        using (FileStream fileStream = new FileStream(pdfFileName, FileMode.Create))
        {
            fileStream.Write(pdfBytes, 0, pdfBytes.Length);
        }
    }
}