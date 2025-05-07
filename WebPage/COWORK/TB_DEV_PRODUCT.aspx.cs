using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Linq;
using Ede.Uof.EIP.SystemInfo;
using Ede.Uof.Utility.Data;
using Ede.Uof.Utility.Log;
using Ede.Uof.Utility.Page.Common;
using OfficeOpenXml;
using OfficeOpenXml.Drawing;
using OfficeOpenXml.Style;
using System.Net.Mail;
using System.Threading.Tasks;

public partial class CDS_WebPage_COWORK_TB_DEV_PRODUCT : Ede.Uof.Utility.Page.BasePage
{
    string ACCOUNT = null;
    string NAME = null;
    string ROLES = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindGrid();
        }
    }

    #region FUNCTION
    private void BindGrid()
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ERPconnectionstring"].ToString();
        Ede.Uof.Utility.Data.DatabaseHelper m_db = new Ede.Uof.Utility.Data.DatabaseHelper(connectionString);

        StringBuilder cmdTxt = new StringBuilder();
        StringBuilder QUERYS = new StringBuilder();
        StringBuilder QUERYS2 = new StringBuilder();
        StringBuilder QUERYS3 = new StringBuilder();        

        cmdTxt.AppendFormat(@"
                            SELECT 
                            [ID]
                            ,[NAMES] AS '品名'
                            ,[PURPOSES] AS '開發目的'
                            ,[SPECIALS] AS '特色'
                            ,[REQUESTS] AS '訴求'
                            ,[ISCLOSED] AS '是否結案'
                            ,CONVERT(NVARCHAR,[CREATEDATES],112) AS '建立日期'
                            FROM [TKRESEARCH].[dbo].[TB_DEV_PRODUCT]
                            WHERE [ISCLOSED] ='N'
                            ORDER BY [ID]
                             ", QUERYS.ToString(), QUERYS2.ToString(), QUERYS3.ToString());




        //m_db.AddParameter("@SDATE", SDATE);
        //m_db.AddParameter("@EDATE", EDATE);

        DataTable dt = new DataTable();

        dt.Load(m_db.ExecuteReader(cmdTxt.ToString()));


        Grid1.DataSource = dt;
        Grid1.DataBind();
    }

    protected void grid_PageIndexChanging1(object sender, GridViewPageEventArgs e)
    {
        //Grid1.PageIndex = e.NewPageIndex;
        //BindGrid();
    }
    protected void Grid1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
    }

    protected void Grid1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int rowIndex = -1;
        // 獲取所選行的索引
        rowIndex = Convert.ToInt32(e.CommandArgument);

    }


    public void OnBeforeExport1(object sender, Ede.Uof.Utility.Component.BeforeExportEventArgs e)
    {
        SETEXCEL1();

    }
    public void SETEXCEL1()
    {
       
    }

    #endregion

    #region BUTTON

    #endregion
}