<%@ WebHandler Language="C#" Class="TKGETDESIGNED_ImageHandler" %>

using System;
using System.Web;
using System.IO;
using System.Runtime.InteropServices;

public class TKGETDESIGNED_ImageHandler : IHttpHandler {

    public void ProcessRequest (HttpContext context) {
        // 1. 取得路徑
        string fullPath = context.Request.QueryString["fullPath"];
        if (string.IsNullOrEmpty(fullPath)) return;

        try {
            // 2. 在 ASHX 內部也要確保 Y 槽存在
            // 請填入與 ASPX 相同的 網域\帳號 與 密碼
            NetworkDrive.MapDrive("Y:", @"\\192.168.1.199\美工檔案區", @"tkfood-tw\ecd_01", "at160115@@");

            // 3. 讀取圖檔
            if (File.Exists(fullPath)) {
                string ext = Path.GetExtension(fullPath).ToLower();
                context.Response.ContentType = GetMimeType(ext);
                context.Response.WriteFile(fullPath);
            } else {
                context.Response.StatusCode = 404;
                context.Response.Write("找不到檔案: " + fullPath);
            }
        }
        catch (Exception ex) {
            // 如果 500 錯誤發生，這行能幫你看到具體報錯
            context.Response.StatusCode = 500;
            context.Response.Write("ASHX 錯誤: " + ex.Message);
        }
    }

    private string GetMimeType(string ext) {
        switch (ext) {
            case ".jpg": case ".jpeg": return "image/jpeg";
            case ".png": return "image/png";
            case ".gif": return "image/gif";
            default: return "application/octet-stream";
        }
    }

    // --- Win32 API 掛載邏輯 (直接放在 ASHX 內部最保險) ---
    [DllImport("mpr.dll")]
    private static extern int WNetAddConnection2(NetResource netResource, string password, string username, int flags);

    [StructLayout(LayoutKind.Sequential)]
    public class NetResource {
        public int Scope; public int Type; public int DisplayType; public int Usage;
        public string LocalName; public string RemoteName; public string Comment; public string Provider;
    }

    private void MapDrive(string local, string remote, string user, string pass) {
        NetResource nr = new NetResource();
        nr.Type = 1; // RESOURCETYPE_DISK
        nr.LocalName = local;
        nr.RemoteName = remote;
        int res = WNetAddConnection2(nr, pass, user, 0);
        // 85 代表已經連線，0 代表成功，這兩個都算 OK
        if (res != 0 && res != 85) throw new Exception("掛載失敗，代碼: " + res);
    }

    public bool IsReusable { get { return false; } }
}