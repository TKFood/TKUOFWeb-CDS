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
            Bind_DropDownList_ISCLOSED();
            BindGrid();
        }
    }

    #region FUNCTION
    public void Bind_DropDownList_ISCLOSED()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("PARAID", typeof(String));
        dt.Columns.Add("PARANAME", typeof(String));

        string connectionString = ConfigurationManager.ConnectionStrings["ERPconnectionstring"].ToString();
        Ede.Uof.Utility.Data.DatabaseHelper m_db = new Ede.Uof.Utility.Data.DatabaseHelper(connectionString);

        string cmdTxt = @" 
                        SELECT
                        [ID]
                        ,[KIND]
                        ,[PARAID]
                        ,[PARANAME]
                        FROM [TKRESEARCH].[dbo].[TBPARA]
                        WHERE [KIND]='TB_DEV_PRODUCT_ISCLOSEDYN'
                        ORDER BY [PARAID]
                        ";

        dt.Load(m_db.ExecuteReader(cmdTxt));

        if (dt.Rows.Count > 0)
        {
            DropDownList_ISCLOSED.DataSource = dt;
            DropDownList_ISCLOSED.DataTextField = "PARANAME";
            DropDownList_ISCLOSED.DataValueField = "PARANAME";
            DropDownList_ISCLOSED.DataBind();

        }
        else
        {

        }

    }
    private void BindGrid()
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ERPconnectionstring"].ToString();
        Ede.Uof.Utility.Data.DatabaseHelper m_db = new Ede.Uof.Utility.Data.DatabaseHelper(connectionString);

        StringBuilder cmdTxt = new StringBuilder();
        StringBuilder QUERYS = new StringBuilder();
        StringBuilder QUERYS2 = new StringBuilder();
        StringBuilder QUERYS3 = new StringBuilder();
        //DropDownList_ISCLOSED
        if (DropDownList_ISCLOSED.Text.Equals("N"))
        {
            QUERYS.AppendFormat(@" AND [ISCLOSED]='N' ");
        }
        else if (DropDownList_ISCLOSED.Text.Equals("Y"))
        {
            QUERYS.AppendFormat(@" AND [ISCLOSED]='Y' ");
        }      
        //TextBox1
        if (!string.IsNullOrEmpty(TextBox1.Text))
        {
            QUERYS2.AppendFormat(@" AND [NAMES] LIKE '%{0}%' ", TextBox1.Text);
        }
        else
        {
            QUERYS2.AppendFormat(@"");
        }

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
                            WHERE 1=1
                            {0}
                            {1}
                            ORDER BY [ID]
                             ", QUERYS.ToString(), QUERYS2.ToString());




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
    protected void Button1_Click(object sender, EventArgs e)
    {
        BindGrid();
       
    }
    #endregion
}