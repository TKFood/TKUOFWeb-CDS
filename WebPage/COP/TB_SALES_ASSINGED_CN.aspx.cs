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

public partial class CDS_WebPage_COP_TB_SALES_ASSINGED_CN : Ede.Uof.Utility.Page.BasePage
{
    DataTable EXCELDT1 = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    #region FUNCTION


    public void MsgBox(String ex, Page pg, Object obj)
    {
        string script = "alert('" + ex.Replace("\r\n", "\\n").Replace("'", "") + "');";
        ScriptManager.RegisterStartupScript(pg, obj.GetType(), "AlertScript", script, true);

        //string s = "<SCRIPT language='javascript'>alert('" + ex.Replace("\r\n", "\\n").Replace("'", "") + "'); </SCRIPT>";
        //Type cstype = obj.GetType();
        //ClientScriptManager cs = pg.ClientScript;
        //cs.RegisterClientScriptBlock(cstype, s, s.ToString());
    }
    #endregion


    #region BUTTON
    protected void btn1_Click(object sender, EventArgs e)
    {
        
    }
    protected void btn2_Click(object sender, EventArgs e)
    {
       
    }

    protected void btn3_Click(object sender, EventArgs e)
    {
       
    }
    protected void btn4_Click(object sender, EventArgs e)
    {
        
    }
    #endregion
}