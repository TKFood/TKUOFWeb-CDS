﻿using System;
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

public partial class CDS_WebPage_COWORK_TBBU_TBCOPTACOPTB : Ede.Uof.Utility.Page.BasePage
{
    string ACCOUNT = null;
    string NAME = null;
    String ROLES = null;

    string DBNAME = "UOF";
    //string DBNAME = "UOFTEST";

    string TA001 = "";
    string TA002 = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        ACCOUNT = Current.Account;
        NAME = Current.User.Name;

        if (!IsPostBack)
        {
            SETDATES();
            BindGrid();
        }
    }

    #region FUNCTION
    public void SETDATES()
    {
        TextBox1.Text = DateTime.Now.ToString("yyyy");     
    }
    private void BindGrid()
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ERPconnectionstring"].ToString();
        Ede.Uof.Utility.Data.DatabaseHelper m_db = new Ede.Uof.Utility.Data.DatabaseHelper(connectionString);

        StringBuilder cmdTxt = new StringBuilder();
        StringBuilder QUERYS = new StringBuilder();

        //核單
        if (!string.IsNullOrEmpty(TextBox1.Text))
        {
            QUERYS.AppendFormat(@" 
                                AND TA003 LIKE '{0}%'
                                ", TextBox1.Text);
        }
        else
        {

        }

        cmdTxt.AppendFormat(@" 
                                SELECT TA006,TA001,TA002,TB003,TB004,TB005,TB009,TB010,TA001+TA002 AS TA001TA002
                                FROM [TK].dbo.COPTA,[TK].dbo.COPTB
                                WHERE TA001=TB001 AND TA002=TB002
                                AND TA019 IN ('N')
                                {0}
                                ORDER BY TA001,TA002  

                                ", QUERYS.ToString());




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
            //Grid1_Button1
            //Get the button that raised the event
            Button Grid1_Button1 = (Button)e.Row.FindControl("Grid1_Button1");
            //Get the row that contains this button
            GridViewRow gvr1 = (GridViewRow)Grid1_Button1.NamingContainer;
            //string cellvalue = gvr.Cells[2].Text.Trim();
            string Cellvalue1 = Grid1_Button1.CommandArgument;
            DataRowView row1 = (DataRowView)e.Row.DataItem;
            Button lbtnName1 = (Button)e.Row.FindControl("Grid1_Button1");
            ExpandoObject param1 = new { ID = Cellvalue1 }.ToExpando();

        }
    }

    protected void Grid1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int rowIndex = -1;

        if (e.CommandName == "Grid1_Button1")
        {
            MsgBox(e.CommandArgument.ToString() + "", this.Page, this);
        }

    }


    public void OnBeforeExport1(object sender, Ede.Uof.Utility.Component.BeforeExportEventArgs e)
    {
        //SETEXCEL();        
    }
    public void MsgBox(String ex, Page pg, Object obj)
    {
        string s = "<SCRIPT language='javascript'>alert('" + ex.Replace("\r\n", "\\n").Replace("'", "") + "'); </SCRIPT>";
        Type cstype = obj.GetType();
        ClientScriptManager cs = pg.ClientScript;
        cs.RegisterClientScriptBlock(cstype, s, s.ToString());
    }

    #endregion

    #region BUTTON
    protected void Button1_Click(object sender, EventArgs e)
    {
        BindGrid();
      

    }
    #endregion
}