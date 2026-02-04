<%@ WebHandler Language="C#" Class="TKGETDESIGNED_ImageHandler" %>

using System;
using System.Web;
using System.IO;
using System.Runtime.InteropServices;

public class TKGETDESIGNED_ImageHandler : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        string relPath = context.Request.QueryString["relPath"];
        if (string.IsNullOrEmpty(relPath)) return;

        // 1. 再次確保磁碟機掛載 (ASHX 是獨立 Session，必須掛載才能讀取 Y:\)
        try {
            // 請務必填入與 aspx 相同的 帳號/密碼/路徑
            NetworkDrive.MapDrive("Y:", @"\\192.168.1.199\美工檔案區", @"tkfood-tw\ecd_01", "at160115@@");
        } catch {
            // 掛載失敗通常是因為已經存在連線，可忽略或記錄日誌
        }

        // 2. 設定完整路徑
        string rootPath = @"Y:\老楊食品\04.門市";
        string fullPath = Path.Combine(rootPath, relPath);

        // 3. 讀取並輸出檔案
        if (File.Exists(fullPath)) {
            string ext = Path.GetExtension(fullPath).ToLower();
            context.Response.ContentType = GetMimeType(ext);
            context.Response.WriteFile(fullPath);
        } else {
            context.Response.StatusCode = 404;
            context.Response.Write("File not found: " + fullPath);
        }
    }

    private string GetMimeType(string ext) {
        if (ext == ".jpg" || ext == ".jpeg") return "image/jpeg";
        if (ext == ".png") return "image/png";
        if (ext == ".gif") return "image/gif";
        return "image/octet-stream";
    }

    // 將掛載邏輯直接寫在 ashx 內，避免類別引用不到導致 500 錯誤
    [DllImport("mpr.dll")]
    private static extern int WNetAddConnection2(NetResource netResource, string password, string username, int flags);
    
    [StructLayout(LayoutKind.Sequential)]
    public class NetResource {
        public int Scope; public int Type; public int DisplayType; public int Usage;
        public string LocalName; public string RemoteName; public string Comment; public string Provider;
    }

    private void MapNetworkDrive(string local, string remote, string user, string pass) {
        NetResource nr = new NetResource();
        nr.Type = 1; nr.LocalName = local; nr.RemoteName = remote;
        WNetAddConnection2(nr, pass, user, 0);
    }

    public bool IsReusable { get { return false; } }
}