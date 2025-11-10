using System;
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
using System.Xml;
using System.Xml.Linq;
using Ede.Uof.EIP.SystemInfo;
using Ede.Uof.Utility.Data;
using Ede.Uof.Utility.Log;
using Ede.Uof.Utility.Page.Common;
using OfficeOpenXml;
using OfficeOpenXml.Drawing;
using OfficeOpenXml.Style;
using System.Net.Mail;
using System.Threading.Tasks;


public partial class CDS_WebPage_MARKETING_TK_AD_MANAGE : Ede.Uof.Utility.Page.BasePage
{
    string ACCOUNT = null;
    string NAME = null;
    String ROLES = null;

    // 在這裡定義您的檔案儲存根目錄
    private const string UploadFolderName = "UPLOAD_ADMANAGES/FileStorage"; // 使用 / 符號在 Web 路徑中更標準
  

    protected void Page_Load(object sender, EventArgs e)
    {
        ACCOUNT = Current.Account;
        NAME = Current.User.Name;
    }

    #region FUNCTION

    #endregion
    private void BindGrid()
    {
        string connectionString = ConfigurationManager.ConnectionStrings["connectionstringUOF"].ToString();
        Ede.Uof.Utility.Data.DatabaseHelper m_db = new Ede.Uof.Utility.Data.DatabaseHelper(connectionString);

        StringBuilder cmdTxt = new StringBuilder();
        StringBuilder QUERYS = new StringBuilder();
        StringBuilder QUERYS2 = new StringBuilder();


        ////日期
        //if (!string.IsNullOrEmpty(TextBox1.Text) && !string.IsNullOrEmpty(TextBox2.Text))
        //{
        //    if (TextBox2.Text.Length == 1)
        //    {
        //        TextBox2.Text = "0" + TextBox2.Text;
        //    }
        //    QUERYS.AppendFormat(@" AND TC002 LIKE '{0}%'", TextBox1.Text.Trim() + TextBox2.Text.Trim());

        //}

  

        cmdTxt.AppendFormat(@" 
                            SELECT
                            [ID]
                            ,[YEARS]
                            ,[SUBJECTS]
                            ,[DESCRIPTIONS]
                            ,[STOREDPATHS]
                            ,[ORIGINALFILENAMS]
                            ,[UPLOADDATES]
                            FROM [TK_MARKETING].[dbo].[TK_MARKETING_AD_MANAGE]
                                ", QUERYS.ToString(), QUERYS2.ToString());




        //m_db.AddParameter("@SDATE", SDATE);
        //m_db.AddParameter("@EDATE", EDATE);

        DataTable dt = new DataTable();

        dt.Load(m_db.ExecuteReader(cmdTxt.ToString()));


        Grid1.DataSource = dt;
        Grid1.DataBind();
    }

    protected void grid_PageIndexChanging1(object sender, GridViewPageEventArgs e)
    {

    }
    protected void Grid1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    protected void Grid1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int rowIndex = -1;
    }


    public void OnBeforeExport1(object sender, Ede.Uof.Utility.Component.BeforeExportEventArgs e)
    {
        //SETEXCEL();

    }

    /// <summary>
    /// 根據檔案路徑判斷檔案類型，並生成對應的 HTML 預覽或下載連結。
    /// </summary>
    /// <param name="virtualPath">資料庫中儲存的虛擬路徑 (e.g., ~/UPLOAD_ADMANAGES/.../file.jpg)</param>
    /// <returns>生成預覽或下載的 HTML 標籤</returns>
    private string GetFileDisplayHtml(string virtualPath)
    {
        string fileUrl = Page.ResolveUrl(virtualPath);
        string extension = Path.GetExtension(virtualPath).ToLowerInvariant();

        // 檔案預設下載連結的 HTML 結構 (使用 string.Format)
        // {0} 替換為 fileUrl
        string defaultLink = string.Format("<a href='{0}' target='_blank' class='btn btn-sm btn-info' download><span class='file-icon'>&#128190;</span>下載檔案</a>", fileUrl);

        // 判斷檔案類型
        switch (extension)
        {
            case ".jpg":
            case ".jpeg":
            case ".png":
            case ".gif":
                // 圖片：顯示縮圖 (使用 string.Format)
                // {0} 替換為 fileUrl
                return string.Format("<a href='{0}' target='_blank'><img src='{0}' class='file-preview' alt='圖片縮圖' /></a>", fileUrl);

            case ".pdf":
                // PDF：顯示預覽/開啟連結 (使用 string.Format)
                // {0} 替換為 fileUrl
                return string.Format("<a href='{0}' target='_blank' class='pdf-preview-link'><span class='file-icon'>&#128220;</span>預覽 PDF</a>", fileUrl);

            case ".mp4":
                // 影片：提供下載/開啟連結 (使用 string.Format)
                // {0} 替換為 fileUrl
                return string.Format("<a href='{0}' target='_blank'><span class='file-icon'>&#128250;</span>播放影片</a>", fileUrl);

            default:
                // 其他類型：提供下載連結
                return defaultLink;

        }
    }

    public void UPLOAD()
    {
        if (!Page.IsValid)
        {
            lblMessage.Text = "請修正輸入錯誤。";
            return;
        }

        if (!fileUploader.HasFile)
        {
            lblMessage.Text = "請選擇一個檔案進行上傳。";
            return;
        }

        try
        {
            // 檔案驗證 (略)
            string originalFileName = fileUploader.FileName;
            string fileExtension = Path.GetExtension(originalFileName).ToLowerInvariant();

            string[] allowedExtensions = { ".jpg", ".jpeg", ".png", ".pdf", ".mp4" };
            if (Array.IndexOf(allowedExtensions, fileExtension) == -1)
            {
                lblMessage.Text = "不允許的檔案類型。";
                return;
            }

            // 儲存檔案到伺服器 (路徑邏輯與之前一致)
            string baseFileName = Path.GetFileNameWithoutExtension(originalFileName);
            string uniqueFileName = baseFileName + "-" + Guid.NewGuid().ToString() + fileExtension;
            //結合路徑：UploadFolderName (ADMANAGES/FileStorage) + yearFolder (2024)
            string yearFolder = txtYear.Text; 
            string fullUploadVirtualPath = Path.Combine(UploadFolderName, yearFolder);
            // 轉換為伺服器實體路徑
            string uploadFolder = Server.MapPath("~/" + fullUploadVirtualPath);

            if (!Directory.Exists(uploadFolder))
            {
                Directory.CreateDirectory(uploadFolder);
            }

            string savePath = Path.Combine(uploadFolder, uniqueFileName);
            fileUploader.SaveAs(savePath);

            // 準備資料庫中繼資料
            int year = Convert.ToInt32(txtYear.Text);
            string subject = txtSubject.Text;
            string description = txtDescription.Text;
            string storedPathForDB = string.Format("~/{0}/{1}/{2}", UploadFolderName, yearFolder, uniqueFileName);

            // 寫入資料庫
            InsertFileRecord(year, subject, description, storedPathForDB, originalFileName);

            lblMessage.Text = "檔案 **" + originalFileName + "** 上傳並記錄成功！";
            // 清空輸入欄位 (可選)
            txtYear.Text = string.Empty;
            txtSubject.Text = string.Empty;
            txtDescription.Text = string.Empty;
        }
        catch (Exception ex)
        {
            lblMessage.Text = "上傳失敗: " + ex.Message;
        }
    }
    /// <summary>
    /// 使用 ADO.NET 將檔案記錄插入資料庫 (已更新為 TK_MARKETING_AD_MANAGE 結構)
    /// </summary>
    private void InsertFileRecord(int year, string subject, string description, string path, string originalName)
    {
        // 從 Web.config 取得連線字串
        string connectionString = ConfigurationManager.ConnectionStrings["connectionstringUOF"].ConnectionString;

        // **SQL 語句已更新以符合新的表格和欄位名稱**
        string sql = string.Format(@"INSERT INTO [TK_MARKETING].[dbo].[TK_MARKETING_AD_MANAGE]
                       (YEARS, SUBJECTS, DESCRIPTIONS, STOREDPATHS, ORIGINALFILENAMS) 
                       VALUES (@Year, @Subject, @Description, @Path, @OriginalName)");

        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                // 設定參數值
                cmd.Parameters.AddWithValue("@Year", year);
                cmd.Parameters.AddWithValue("@Subject", subject);
                cmd.Parameters.AddWithValue("@Description", description);
                cmd.Parameters.AddWithValue("@Path", path);
                cmd.Parameters.AddWithValue("@OriginalName", originalName);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }

    #region BUTTON
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        UPLOAD();
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        BindGrid();
    }
    #endregion
}