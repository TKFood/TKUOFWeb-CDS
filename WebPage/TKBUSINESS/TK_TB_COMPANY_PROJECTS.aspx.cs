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
using Ede.Uof.Utility.Data;
using Ede.Uof.Utility.Page.Common;
using OfficeOpenXml;
using OfficeOpenXml.Drawing;
using OfficeOpenXml.Style;
using System.Web.UI.HtmlControls;

public partial class CDS_WebPage_TKBUSINESS_TK_TB_COMPANY_PROJECTSE : Ede.Uof.Utility.Page.BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtDate1.Text = DateTime.Now.AddDays(-7).ToString("yyyy/MM/dd");
            txtDate2.Text = DateTime.Now.ToString("yyyy/MM/dd");

            BindGrid(txtDate1.Text, txtDate2.Text);
        }
        else
        {

        }




    }
    #region FUNCTION

    private void BindGrid(string SDAYS, string EDAYS)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ERPconnectionstring"].ToString();
        Ede.Uof.Utility.Data.DatabaseHelper m_db = new Ede.Uof.Utility.Data.DatabaseHelper(connectionString);

        StringBuilder cmdTxt = new StringBuilder();

        cmdTxt.AppendFormat(@" 
                            SELECT 
                            [ID]
                            ,[ISCLOSED]
                            ,[PROJECTNAMES]
                            ,[CONTENTS]
                            ,CONVERT(NVARCHAR,[PROJECTDATES],111 ) [PROJECTDATES]
                            ,[DEPDEV]
                            ,[DEPMARKET]
                            ,[DEPACCOUNTS]
                            ,[DEPMOC]
                            ,[DEPLAWS]
                            ,[DEPSALES]
                            ,[DEPSTORES]
                            ,[DEPFACTORYS]
                            ,[DEPPURS]
                            ,[DEPQCS]
                            ,CONVERT(NVARCHAR,[CREATEDATES],111 ) [CREATEDATES]
                            ,ISNULL(CONVERT(NVARCHAR,[DEPDEVREPLAYDATES],111 ),'') [DEPDEVREPLAYDATES]
                            ,CONVERT(NVARCHAR,[DEPMARKETREPLAYDATES],111 ) [DEPMARKETREPLAYDATES]
                            ,CONVERT(NVARCHAR,[DEPACCOUNTSREPLAYDATES],111 ) [DEPACCOUNTSREPLAYDATES]
                            ,CONVERT(NVARCHAR,[DEPMOCREPLAYDATES],111 ) [DEPMOCREPLAYDATES]
                            ,CONVERT(NVARCHAR,[DEPLAWSREPLAYDATES],111 ) [DEPLAWSREPLAYDATES]
                            ,CONVERT(NVARCHAR,[DEPSALESREPLAYDATES],111 ) [DEPSALESREPLAYDATES]
                            ,CONVERT(NVARCHAR,[DEPSTORESREPLAYDATES],111 ) [DEPSTORESREPLAYDATES]
                            ,CONVERT(NVARCHAR,[DEPFACTORYSREPLAYDATES],111 ) [DEPFACTORYSREPLAYDATES]
                            ,CONVERT(NVARCHAR,[DEPPURSREPLAYDATES],111 ) [DEPPURSREPLAYDATES]
                            ,CONVERT(NVARCHAR,[DEPQCSREPLAYDATES],111 ) [DEPQCSREPLAYDATES]
                            FROM [TKBUSINESS].[dbo].[TB_COMPANY_PROJECTS]
                            WHERE [ISCLOSED]='N'
                            ORDER BY [PROJECTDATES]

                              
                            ", SDAYS, EDAYS);


        //m_db.AddParameter("@EDATE", EDATE);

        DataTable dt = new DataTable();

        dt.Load(m_db.ExecuteReader(cmdTxt.ToString()));

        Grid1.DataSource = dt;
        Grid1.DataBind();
    }

    protected void grid_PageIndexChanging1(object sender, GridViewPageEventArgs e)
    {
        //Grid1.PageIndex = e.NewPageIndex;
        //BindGrid("");
    }
    protected void Grid1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    protected void Grid1_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
    }

    public void OnBeforeExport1(object sender, Ede.Uof.Utility.Component.BeforeExportEventArgs e)
    {
        SETEXCEL(txtDate1.Text, txtDate2.Text);


    }


    public override void VerifyRenderingInServerForm(Control control)
    {

    }

    public void SETEXCEL(string SDAYS, string EDAYS)
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
        BindGrid(txtDate1.Text, txtDate2.Text);
    }



    #endregion
}