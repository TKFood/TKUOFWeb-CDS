using System;
using System.Web;
using System.IO;

public class TKGETDESIGNED_ImageHandler : IHttpHandler
{
    public void ProcessRequest(HttpContext context)
    {
        string relPath = context.Request.QueryString["relPath"];
        // 圖片讀取的路徑同樣指向 Y 槽
        string fullPath = Path.Combine(@"Y:\老楊食品\04.門市", relPath);

        if (File.Exists(fullPath))
        {
            string ext = Path.GetExtension(fullPath).ToLower();
            context.Response.ContentType = "image/" + ext.Replace(".", "");
            context.Response.WriteFile(fullPath);
        }
    }

    public bool IsReusable { get { return false; } }
}