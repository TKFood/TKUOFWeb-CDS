using System;
using System.Web;
using System.IO;
using System.Security.Principal;

public class TKGETDESIGNED_ImageHandler : IHttpHandler
{
    public void ProcessRequest(HttpContext context)
    {
        string relPath = context.Request.QueryString["relPath"];
        string rootPath = @"\\192.168.1.199\美工檔案區\老楊食品\11.產品圖";
        
        using (WindowsImpersonationContext impersonation = LogonHelper.Impersonate("tkfood-tw\ecd_01", "192.168.1.199", "at160115@@"))
        {
            if (impersonation != null)
            {
                string fullPath = Path.Combine(rootPath, relPath);
                if (File.Exists(fullPath))
                {
                    string ext = Path.GetExtension(fullPath).ToLower();
                    context.Response.ContentType = "image/" + ext.Replace(".", "");
                    context.Response.WriteFile(fullPath);
                }
                impersonation.Undo();
            }
        }
    }
    public bool IsReusable { get { return false; } }
}