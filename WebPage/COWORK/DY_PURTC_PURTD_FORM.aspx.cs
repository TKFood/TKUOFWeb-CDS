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

public partial class CDS_WebPage_COWORK_DY_PURTC_PURTD_FORM : Ede.Uof.Utility.Page.BasePage
{
    string ACCOUNT = null;
    string NAME = null;
    String ROLES = null;

    string TC001 = "";
    string TC002 = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        ACCOUNT = Current.Account;
        NAME = Current.User.Name;

        if (!IsPostBack)
        {
            BindDropDownList();

            SETTEXT();
        }

    }

    #region FUNCTION
    public void SETTEXT()
    {
        DateTime DT = DateTime.Now;
        string NOWYEARS = DT.Year.ToString();
        string NOWMONTHS = DT.Month.ToString();

        TextBox1.Text = NOWYEARS;
        TextBox2.Text = NOWMONTHS;
    }
    private void BindDropDownList()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("STATUS", typeof(String));


        string connectionString = ConfigurationManager.ConnectionStrings["ERPconnectionstring"].ToString();
        Ede.Uof.Utility.Data.DatabaseHelper m_db = new Ede.Uof.Utility.Data.DatabaseHelper(connectionString);

        string cmdTxt = @" SELECT '未核單' AS 'STATUS' UNION ALL SELECT '已核單' AS 'STATUS' ";

        dt.Load(m_db.ExecuteReader(cmdTxt));

        if (dt.Rows.Count > 0)
        {
            DropDownList1.DataSource = dt;
            DropDownList1.DataTextField = "STATUS";
            DropDownList1.DataValueField = "STATUS";
            DropDownList1.DataBind();

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


        //日期
        if (!string.IsNullOrEmpty(TextBox1.Text) && !string.IsNullOrEmpty(TextBox2.Text))
        {
            if (TextBox2.Text.Length == 1)
            {
                TextBox2.Text = "0" + TextBox2.Text;
            }
            QUERYS.AppendFormat(@" AND TC002 LIKE '{0}%'", TextBox1.Text.Trim() + TextBox2.Text.Trim());

        }

        //核單
        if (!string.IsNullOrEmpty(DropDownList1.Text))
        {
            if (DropDownList1.Text.Equals("未核單"))
            {
                QUERYS.AppendFormat(@" AND TC014='N'");
            }
            else if (DropDownList1.Text.Equals("已核單"))
            {
                QUERYS.AppendFormat(@"  AND TC014='Y'");
            }
        }

        cmdTxt.AppendFormat(@" 
                            SELECT 
                            TC001,
                            TC002,
                            TC003,
                            TC004,
                            MA002,
                            STUFF((
                                    SELECT ',' + TD005+' ,數量'+CONVERT(NVARCHAR,TD008)+' ,到貨日'+TD012
                                    FROM [DY].dbo.PURTD
                                    WHERE TD001=TC001 AND TD002=TC002
                                    FOR XML PATH(''), TYPE
                                ).value('.', 'NVARCHAR(MAX)'), 1, 1, '') AS DETAILS

                            FROM [DY].dbo.PURTC,[DY].dbo.PURMA
                            WHERE TC004=MA001
                            {0}
                            {1}
                            ORDER BY TC001,TC002
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

    }


    public void OnBeforeExport1(object sender, Ede.Uof.Utility.Component.BeforeExportEventArgs e)
    {
        //SETEXCEL();

    }

    #endregion

    #region BUTTON
    protected void Button1_Click(object sender, EventArgs e)
    {
        BindGrid();
    }

    #endregion
}