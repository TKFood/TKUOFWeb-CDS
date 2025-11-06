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
    private const string UploadFolderName = "FileStorage";
    // 資料表名稱
    private const string TableName = "TK_MARKETING_AD_MANAGE";

    protected void Page_Load(object sender, EventArgs e)
    {
        ACCOUNT = Current.Account;
        NAME = Current.User.Name;
    }

    #region FUNCTION

    #endregion
    /// <summary>
    /// 使用 ADO.NET 將檔案記錄插入資料庫 (已更新為 TK_MARKETING_AD_MANAGE 結構)
    /// </summary>
    private void InsertFileRecord(int year, string subject, string description, string path, string originalName)
    {
        // 從 Web.config 取得連線字串
        string connectionString = ConfigurationManager.ConnectionStrings["connectionstringUOF"].ConnectionString;

        // **SQL 語句已更新以符合新的表格和欄位名稱**
        string sql = string.Format(@"INSERT INTO {0} 
                       (YEARS, SUBJECTS, DESCRIPTIONS, STOREDPATHS, ORIGINALFILENAMS) 
                       VALUES (@Year, @Subject, @Description, @Path, @OriginalName)", TableName);

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
        
    }
    #endregion
}