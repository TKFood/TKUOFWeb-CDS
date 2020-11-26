using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ede.Uof.Utility.Data;
using Ede.Uof.Utility.Page.Common;

public partial class CDS_WebPage_COP_TBBU_COPCONDTIONS : Ede.Uof.Utility.Page.BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            BindGrid();
        }
        else
        {
            BindGrid();
        }
    }
    #region FUNCTION

    private void BindGrid()
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ERPconnectionstring"].ToString();
        Ede.Uof.Utility.Data.DatabaseHelper m_db = new Ede.Uof.Utility.Data.DatabaseHelper(connectionString);

        string cmdTxt = @" 
                        SELECT 
                        [ID]
                        ,[SERNO]
                        ,[MA001]
                        ,[MA002]
                        ,[CONTACTPERSON]
                        ,[TEL1]
                        ,[TEL2]
                        ,[ISPURATTCH]
                        ,[ISCOPATTCH]
                        ,[ISSHOWMONEYS]
                        ,[ISINVOICES]
                        ,[ISSHIPMARK]
                        ,[LIMITDAYS]
                        ,[PAYMENT]
                        ,[SENDADDRESS]
                        ,REPLACE([COMMENT] ,char(10),'<br/>') AS [COMMENT] 
                        FROM [TKBUSINESS].[dbo].[COPCONDTIONS]
                        ORDER BY ID
                        ";

        //m_db.AddParameter("@SDATE", SDATE);
        //m_db.AddParameter("@EDATE", EDATE);

        DataTable dt = new DataTable();

        dt.Load(m_db.ExecuteReader(cmdTxt));

        Grid1.DataSource = dt;
        Grid1.DataBind();
    }

    protected void grid1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //Grid1.PageIndex = e.NewPageIndex;
        //BindGrid();
    }
    protected void Grid1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
       


    }

    public void OnBeforeExport1(object sender, Ede.Uof.Utility.Component.BeforeExportEventArgs e)
    {
        
    }
    #endregion

    #region BUTTON
    protected void btn_Click(object sender, EventArgs e)
    {


        //開窗後回傳參數
        if (!string.IsNullOrEmpty(Dialog.GetReturnValue()))
        {
            //txtReturnValue.Text = Dialog.GetReturnValue();
        }


    }


    protected void btn1_Click(object sender, EventArgs e)
    {
        //this.Session["SDATE"] = txtDate1.Text.Trim();
        //this.Session["EDATE"] = txtDate2.Text.Trim();
    }

    #endregion
}