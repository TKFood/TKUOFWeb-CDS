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

public partial class CDS_WebPage_Mobile_Mobile_TEST3 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
      

        if (!IsPostBack)
        {

        }
    }

    public void ADDTKGAFFAIRSCHECKSPOOINT()
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ERPconnectionstring"].ToString();
        Ede.Uof.Utility.Data.DatabaseHelper m_db = new Ede.Uof.Utility.Data.DatabaseHelper(connectionString);

        string cmdTxt = @"  
                       INSERT INTO [TKGAFFAIRS].[dbo].[CHECKSPOOINT]
                        ([CHECKSPOINT],[CHECKSTIME])
                        VALUES
                        (@CHECKSPOINT,@CHECKSTIME)
                            ";


        m_db.AddParameter("@CHECKSPOINT", "1");
        m_db.AddParameter("@CHECKSTIME", "2022/8/30");


        m_db.ExecuteNonQuery(cmdTxt);

    }


    protected void Button1_Click(object sender, EventArgs e)
    {
        ADDTKGAFFAIRSCHECKSPOOINT();

        Button1.Text = "AAS";
    }
}