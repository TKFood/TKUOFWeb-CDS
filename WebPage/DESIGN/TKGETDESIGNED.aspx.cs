using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Security.Principal;

public partial class CDS_WebPage_DESIGN_TKGETDESIGNED : System.Web.UI.Page
{
    private string RootPath = @"\\192.168.1.199\美工檔案區\老楊食品\11.產品圖";
    // 這裡請填入你的 1.199 帳密
    private string User = "tkfood-tw\ecd_01";
    private string Domain = "192.168.1.199";
    private string Pass = "at160115@@";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string relPath = Request.QueryString["path"] ?? "";
            BindData(relPath);
        }
    }

    private void BindData(string relPath)
    {
        using (WindowsImpersonationContext context = LogonHelper.Impersonate(User, Domain, Pass))
        {
            if (context == null) { litCurrentPath.Text = "登入 1.199 失敗，請檢查帳密。"; return; }

            string fullPath = Path.Combine(RootPath, relPath);
            if (!Directory.Exists(fullPath)) fullPath = RootPath;

            DirectoryInfo di = new DirectoryInfo(fullPath);
            litCurrentPath.Text = "目前目錄: " + di.FullName;

            // 返回按鈕邏輯
            if (!string.IsNullOrEmpty(relPath))
            {
                phBack.Visible = true;
                string p = Path.GetDirectoryName(relPath.TrimEnd('\\'));
                hlBack.NavigateUrl = "TKGETDESIGNED.aspx?path=" + HttpUtility.UrlEncode(p);
            }

            var folders = di.GetDirectories().Select(d => new {
                Name = d.Name,
                RelativePath = Path.Combine(relPath, d.Name),
                Type = "Folder",
                LinkUrl = "TKGETDESIGNED.aspx?path=" + HttpUtility.UrlEncode(Path.Combine(relPath, d.Name))
            });

            string[] exts = { ".jpg", ".jpeg", ".png", ".gif" };
            var files = di.GetFiles().Where(f => exts.Contains(f.Extension.ToLower())).Select(f => new {
                Name = f.Name,
                RelativePath = Path.Combine(relPath, f.Name),
                Type = "File",
                LinkUrl = "TKGETDESIGNED_ImageHandler.ashx?relPath=" + HttpUtility.UrlEncode(Path.Combine(relPath, f.Name))
            });

            rptFiles.DataSource = folders.Cast<object>().Concat(files.Cast<object>()).ToList();
            rptFiles.DataBind();

            context.Undo(); // 結束模擬
        }
    }
}