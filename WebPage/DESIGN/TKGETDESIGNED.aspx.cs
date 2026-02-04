using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

public partial class CDS_WebPage_DESIGN_TKGETDESIGNED : Ede.Uof.Utility.Page.BasePage
{
    // 設定實體路徑 Y 槽
    private string RootPath = @"Y:\老楊食品\04.門市";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                // 1. 確保 Y 槽已經掛載 (請填入正確帳密)
                // 注意：這裡掛載到根目錄即可
                NetworkDrive.MapDrive("Y:", @"\\192.168.1.199\美工檔案區", @"網域\帳號", "密碼");

                string relPath = Request.QueryString["path"] ?? "";
                BindData(relPath);
            }
            catch (Exception ex)
            {
                Response.Write("磁碟連線失敗: " + ex.Message);
            }
        }
    }

    private void BindData(string relPath)
    {
        string fullPath = Path.Combine(RootPath, relPath);
        if (!Directory.Exists(fullPath)) fullPath = RootPath;

        DirectoryInfo di = new DirectoryInfo(fullPath);
        litCurrentPath.Text = "目前位置: " + di.FullName;

        // 返回按鈕
        if (!string.IsNullOrEmpty(relPath))
        {
            phBack.Visible = true;
            string p = Path.GetDirectoryName(relPath.TrimEnd('\\'));
            phBack.NavigateUrl = "TKGETDESIGNED.aspx?path=" + HttpUtility.UrlEncode(p);
        }

        // 讀取清單
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
        
        rptItems.DataSource = folders.Cast<object>().Concat(files.Cast<object>()).ToList();
        rptItems.DataBind();
    }
}