﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ede.Uof.Utility.Data;
using Ede.Uof.Utility.Page.Common;

public partial class CDS_WebPage_COP_TBBU_COPCOPMACLIENT : Ede.Uof.Utility.Page.BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindGrid(); 

        }
        else
        {
           
        }
    }

    #region FUNCTION

    private void BindGrid()
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ERPconnectionstring"].ToString();
        Ede.Uof.Utility.Data.DatabaseHelper m_db = new Ede.Uof.Utility.Data.DatabaseHelper(connectionString);

        StringBuilder cmdTxt = new StringBuilder();
        StringBuilder QUERYS = new StringBuilder();

        // 日期
        if (!string.IsNullOrEmpty(TextBox1.Text) && !string.IsNullOrEmpty(TextBox1.Text))
        {
            QUERYS.AppendFormat(@" AND (MA001 LIKE '%{0}%' OR MA002 LIKE '%{0}%')", TextBox1.Text.Trim());

        }
        else
        {
            QUERYS.AppendFormat(@" ");
        }
        cmdTxt.AppendFormat(@"
                            SELECT 
                            [ID]
                            ,[MA001]
                            ,[MA002]
                            ,[CLIENTS]
                            ,REPLACE([OPERATIONS],char(10),'<br/>') AS [OPERATIONS]
                            ,REPLACE([COMMENTS],char(10),'<br/>') AS [COMMENTS]
                            FROM [TKBUSINESS].[dbo].[COPCOPMACLIENT]
                            WHERE 1=1
                            {0}
                            ORDER BY [MA001],[CLIENTS]
                                ", QUERYS.ToString());
       

        //m_db.AddParameter("@SDATE", SDATE);
        //m_db.AddParameter("@EDATE", EDATE);

        DataTable dt = new DataTable();

        dt.Load(m_db.ExecuteReader(cmdTxt.ToString()));

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
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //Get the button that raised the event
            Button btn = (Button)e.Row.FindControl("GVButton1");

            //Get the row that contains this button
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;

            //string cellvalue = gvr.Cells[2].Text.Trim();
            string Cellvalue = btn.CommandArgument;

            DataRowView row = (DataRowView)e.Row.DataItem;
            Button lbtnName = (Button)e.Row.FindControl("GVButton1");

            ExpandoObject param = new { ID = Cellvalue }.ToExpando();

            //Grid開窗是用RowDataBound事件再開窗
            Dialog.Open2(lbtnName, "~/CDS/WebPage/COP/TBBU_COPCOPMACLIENTDialogEDITDEL.aspx", "", 800, 600, Dialog.PostBackType.AfterReturn, param);
        }


    }

    public void OnBeforeExport1(object sender, Ede.Uof.Utility.Component.BeforeExportEventArgs e)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ERPconnectionstring"].ToString();
        Ede.Uof.Utility.Data.DatabaseHelper m_db = new Ede.Uof.Utility.Data.DatabaseHelper(connectionString);

       

        string cmdTxt = @" 
                        SELECT [ID]
                        ,[MA001]
                        ,[MA002]
                        ,[CLIENTS]
                        ,[OPERATIONS]
                        ,[COMMENTS]
                        FROM [TKBUSINESS].[dbo].[COPCOPMACLIENT]
                        ORDER BY [MA001],[CLIENTS]
                        ";



        DataTable dt = new DataTable();

        dt.Load(m_db.ExecuteReader(cmdTxt));

        if (dt.Rows.Count > 0)
        {
            dt.Columns[0].Caption = "ID";
            dt.Columns[1].Caption = "客戶代號";
            dt.Columns[2].Caption = "客戶";
            dt.Columns[3].Caption = "客戶的賣家(渠道)";
            dt.Columns[4].Caption = "通路操作";
            dt.Columns[5].Caption = "備註";


            e.Datasource = dt;
        }
    }

    protected void Grid1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int rowIndex = -1;

        if (e.CommandName == "GVButton1")
        {
            BindGrid();
            MsgBox(e.CommandArgument.ToString() + " 已更新", this.Page, this);

        }

    }

    public void MsgBox(String ex, Page pg, Object obj)
    {
        string script = "alert('" + ex.Replace("\r\n", "\\n").Replace("'", "") + "');";
        ScriptManager.RegisterStartupScript(pg, obj.GetType(), "AlertScript", script, true);

        //MsgBox("完成", this.Page, this);
    }
    #endregion

    #region BUTTON

    protected void MyButtonClick(object sender, System.EventArgs e)
    {


    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        BindGrid();
    }
    #endregion
}