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

public partial class CDS_WebPage_PUR_TB_YEARS_COST_CALS : Ede.Uof.Utility.Page.BasePage
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
            BindGrid();
        }
    }

    #region FUNCTION
    private void BindGrid()
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ERPconnectionstring"].ToString();
        Ede.Uof.Utility.Data.DatabaseHelper m_db = new Ede.Uof.Utility.Data.DatabaseHelper(connectionString);

        StringBuilder cmdTxt = new StringBuilder();
        StringBuilder QUERYS1 = new StringBuilder();
        StringBuilder QUERYS2 = new StringBuilder();
        StringBuilder QUERYS3 = new StringBuilder();
  

        cmdTxt.AppendFormat(@"   
                            SELECT 
                                CASE 
                                    WHEN GROUPING([類別]) = 1 THEN '總合計'
                                    WHEN GROUPING([ID]) = 1 THEN [類別] + ' 小計'
                                    ELSE CAST([ID] AS NVARCHAR(50)) 
                                END AS [編號說明],    
                                -- 小計列不顯示年度
                                CASE WHEN GROUPING([ID]) = 0 THEN [年度] ELSE '' END AS [年度],
                                [類別],    
                                CASE WHEN GROUPING([ID]) = 0 THEN AVG([營業成本百分比a]) ELSE NULL END  AS [營業成本百分比],    
                                -- 修正點：如果是小計或合計列 (GROUPING = 1)，明細顯示空字串
                                CASE WHEN GROUPING([ID]) = 0 THEN MAX([明細]) ELSE '' END AS [明細],     
                                CASE WHEN GROUPING([類別]) <> 1 THEN SUM([進貨金額佔類別平均%b]) ELSE NULL END AS [進貨金額佔類別平均],
                                CASE WHEN GROUPING([ID]) = 0 THEN SUM([調漲增加(減少)c]) ELSE NULL END   AS [調漲增加減少],    
                                -- 計算 d 欄位的加總
                                SUM([影響成本率增加%  d=a*b*c]) AS [影響成本率增加]  

                            FROM [TKRESEARCH].[dbo].[TB_YEARS_COST_CALS]
                            WHERE [年度] = '2025'
                            GROUP BY GROUPING SETS (
                                ([類別], [ID], [年度]), -- 明細層
                                ([類別]),               -- 類別小計層
                                ()                      -- 總合計層
                            )
                            ORDER BY 
                                GROUPING([類別]) ASC, 
                                [類別], 
                                GROUPING([ID]) ASC, 
                                [ID];


                            ");




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
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

        }
    }

    protected void Grid1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
       
    }


    public void OnBeforeExport1(object sender, Ede.Uof.Utility.Component.BeforeExportEventArgs e)
    {
        SETEXCEL();

    }


    public void SETEXCEL()
    {

    }
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
    protected void Button1_Click(object sender, EventArgs e)
    {
        BindGrid();
      

    }
    #endregion
}