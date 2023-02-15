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

public partial class CDS_WebPage_Mobile_Mobile_TEST2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {


        if (!IsPostBack)
        {

        }
    }

    #region FUNCTION




    #endregion
    private void btnTransfer_Click(object sender, System.EventArgs e)
    {
        //Response.Redirect("https://social.msdn.microsoft.com/Forums/en-US/ebfe41c4-b553-4802-b102-dfb645a3fcbb/open-webpage-from-an-webform-button-click?forum=aspgettingstarted");

        Page.ClientScript.RegisterStartupScript(
        this.GetType(), "OpenWindow", "window.open('https://social.msdn.microsoft.com/Forums/en-US/ebfe41c4-b553-4802-b102-dfb645a3fcbb/open-webpage-from-an-webform-button-click?forum=aspgettingstarted','_newtab');", true);

    }

    #region BUTTON
    protected void Button1_Click(object sender, EventArgs e)
    {

        //Page.ClientScript.RegisterStartupScript(
        ////this.GetType(), "OpenWindow", "window.open('https://eip.tkfood.com.tw/UOF/WKF/FormUse/FormPrint.aspx?TASK_ID=33e2d96a-4ce0-4ab4-8344-f077271fc8fd','_newtab');", true);
        //this.GetType(), "OpenWindow", "window.open('https://www.google.com/','_blank');", true);

        //Response.Redirect("https://www.google.com/");

        Response.Redirect("https://eip.tkfood.com.tw/UOF/WKF/FormUse/FormPrint.aspx?TASK_ID=33e2d96a-4ce0-4ab4-8344-f077271fc8fd");
    }

    #endregion


}