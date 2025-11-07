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
    private const string UploadFolderName = "ADMANAGES/FileStorage"; // 使用 / 符號在 Web 路徑中更標準
  

    protected void Page_Load(object sender, EventArgs e)
    {
        ACCOUNT = Current.Account;
        NAME = Current.User.Name;
    }

    #region FUNCTION

    #endregion
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
            string uploadFolder = Server.MapPath("~/" + UploadFolderName);

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
            string storedPathForDB = string.Format("~/{0}/{1}", UploadFolderName, uniqueFileName);

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
    #endregion
}