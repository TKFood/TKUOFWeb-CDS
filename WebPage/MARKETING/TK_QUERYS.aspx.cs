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

public partial class CDS_WebPage_MARKETING_TK_QUERYS : Ede.Uof.Utility.Page.BasePage
{
    string ACCOUNT = null;
    string NAME = null;
    String ROLES = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        ACCOUNT = Current.Account;
        NAME = Current.User.Name;

        if (!IsPostBack)
        {
            SET_DATES();
        }
    }

    #region FUNCTION
    public void SET_DATES()
    {
        TextBox1.Text = DateTime.Now.ToString("yyyyMMdd");
        TextBox2.Text = DateTime.Now.ToString("yyyyMMdd");
    }
    private void BindGrid()
    {
        // 1.取得連線字串
        // 請將 "YourConnectionStringName" 替換為 Web.config 中定義的連線名稱
        string connectionString = ConfigurationManager.ConnectionStrings["ERPconnectionstring"].ConnectionString;
        Ede.Uof.Utility.Data.DatabaseHelper m_db = new Ede.Uof.Utility.Data.DatabaseHelper(connectionString);

        // 2. 定義 SQL 查詢字串           
        string cmdTxt = @"
                            SELECT
                                MIN(TA001) AS '日期起',
                                MAX(TA001) AS '日期迄',
                                POSTA.TA002 AS '門市代',
                                CMSME.ME002 AS '門市',
                                COUNT(POSTA.TA002) AS '銷售總筆數',
                                SUM(POSTA.TA026) AS '銷售總金額含稅',
                                SUM(CASE WHEN POSTA.TA026 >= @QUERYMONEY THEN 1 ELSE 0 END) AS '滿額總筆數',
                                SUM(CASE WHEN POSTA.TA026 >= @QUERYMONEY THEN POSTA.TA026 ELSE 0 END) AS '滿額金額含稅'
                            FROM
                                [TK].dbo.POSTA POSTA
                            INNER JOIN
                                [TK].dbo.CMSME CMSME ON POSTA.TA002 = CMSME.ME001
                            WHERE
                                POSTA.TA001 >= @DATESTART
                                AND POSTA.TA001 <= @DATESEND
                                AND POSTA.TA026 >=0
                                AND POSTA.TA002 LIKE '106%'
                            GROUP BY
                                POSTA.TA002, CMSME.ME002
                            ORDER BY
                                POSTA.TA002;
                        ";

        m_db.AddParameter("@DATESTART", TextBox1.Text.Trim());
        m_db.AddParameter("@DATESEND", TextBox2.Text.Trim());
        m_db.AddParameter("@QUERYMONEY", TextBox3.Text.Trim());

        DataTable dt = new DataTable();

        dt.Load(m_db.ExecuteReader(cmdTxt.ToString()));


        Grid1.DataSource = dt;
        Grid1.DataBind();

    }

    protected void grid_PageIndexChanging1(object sender, GridViewPageEventArgs e)
    {

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