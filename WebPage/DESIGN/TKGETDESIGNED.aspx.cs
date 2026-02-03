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
using Ede.Uof.EIP.SystemInfo;
using Telerik.Web.UI;
using System.Web.UI.HtmlControls;

public partial class CDS_WebPage_DESIGN_TKGETDESIGNED : Ede.Uof.Utility.Page.BasePage
{
    string ACCOUNT = null;
    string NAME = null;
    String ROLES = null;

    private string RootPath = @"\\192.168.1.199\美工檔案區\老楊食品";

    protected void Page_Load(object sender, EventArgs e)
    {
        ACCOUNT = Current.Account;
        NAME = Current.User.Name;

        if (!IsPostBack)
        {
            string relPath = Request.QueryString["path"] ?? "";
            BindData(relPath);
        }
    }

    #region FUNCTION
    private void BindData(string relPath)
    {
        string fullPath = Path.Combine(RootPath, relPath);
        if (!Directory.Exists(fullPath)) fullPath = RootPath;

        litCurrentPath.Text = "目前路徑: " + fullPath;

        // 處理返回上一層按鈕
        if (!string.IsNullOrEmpty(relPath))
        {
            phBack.Visible = true;
            int lastSlash = relPath.TrimEnd('\\').LastIndexOf('\\');
            string parentPath = lastSlash > -1 ? relPath.Substring(0, lastSlash) : "";
            hlBack.NavigateUrl = "TKGETDESIGNED.aspx?path=" + HttpUtility.UrlEncode(parentPath);
        }

        DirectoryInfo di = new DirectoryInfo(fullPath);

        // 取得資料夾
        var folders = di.GetDirectories().Select(d => new {
            Name = d.Name,
            RelativePath = Path.Combine(relPath, d.Name),
            Type = "Folder",
            LinkUrl = "TKGETDESIGNED.aspx?path=" + HttpUtility.UrlEncode(Path.Combine(relPath, d.Name))
        });

        // 取得圖片
        string[] exts = { ".jpg", ".jpeg", ".png", ".gif" };
        var files = di.GetFiles().Where(f => exts.Contains(f.Extension.ToLower())).Select(f => new {
            Name = f.Name,
            RelativePath = Path.Combine(relPath, f.Name),
            Type = "File",
            LinkUrl = "TKGETDESIGNED_ImageHandler.ashx?relPath=" + HttpUtility.UrlEncode(Path.Combine(relPath, f.Name))
        });

        // 合併並綁定
        var allItems = folders.Cast<object>().Concat(files.Cast<object>()).ToList();
        rptFiles.DataSource = allItems;
        rptFiles.DataBind();
    }

    #endregion


    #region BUTTON
    #endregion
}