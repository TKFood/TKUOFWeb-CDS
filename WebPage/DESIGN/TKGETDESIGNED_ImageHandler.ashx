<%@ WebHandler Language="C#" Class="TKGETDESIGNED_ImageHandler" %>

using System;
using System.Web;
using System.IO;

public class TKGETDESIGNED_ImageHandler : IHttpHandler {
    
    public void ProcessRequest(HttpContext context)
    {
        string relPath = context.Request.QueryString["relPath"];
        if (string.IsNullOrEmpty(relPath)) return;

        // C# 5.0 建議使用 @ 字串處理路徑
        string rootPath = @"\\192.168.1.199\美工檔案區\老楊食品";
        string fullPath = Path.Combine(rootPath, relPath);

        // 安全檢查
        if (File.Exists(fullPath) && fullPath.StartsWith(rootPath, StringComparison.OrdinalIgnoreCase))
        {
            string extension = Path.GetExtension(fullPath).ToLower();
            // 設定 MIME
            context.Response.ContentType = "image/" + extension.Replace(".", "");
            
            // 輸出檔案
            context.Response.WriteFile(fullPath);
        }
        else
        {
            context.Response.StatusCode = 404;
        }
    }

    public bool IsReusable { get { return false; } }


}