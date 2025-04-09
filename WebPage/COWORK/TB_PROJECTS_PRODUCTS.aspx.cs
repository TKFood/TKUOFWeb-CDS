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

public partial class CDS_WebPage_COWORK_TB_PROJECTS_PRODUCTS : Ede.Uof.Utility.Page.BasePage
{
    string ACCOUNT = null;
    string NAME = null;
    String ROLES = null;

    protected void Page_Load(object sender, EventArgs e)
    {

    }


    #region FUNCTION
    private void BindGrid()
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ERPconnectionstring"].ToString();
        Ede.Uof.Utility.Data.DatabaseHelper m_db = new Ede.Uof.Utility.Data.DatabaseHelper(connectionString);

        StringBuilder cmdTxt = new StringBuilder();
        StringBuilder QUERYS = new StringBuilder();
        StringBuilder QUERYS2 = new StringBuilder();
        

        cmdTxt.AppendFormat(@" 
                            
                            SELECT 
                            [ID]
                            ,[NO] AS '專案編號'
                            ,[PROJECTNAMES] AS '項目名稱'
                            ,[TRYSDATES] AS '產品打樣日'
                            ,[TASTESDATES] AS '產品試吃日'
                            ,[DESIGNSDATES] AS '包裝設計日'
                            ,[SALESDATES] AS '上市日'
                            ,[OWNER] AS '專案負責人'
                            ,[STATUS] AS '狀態'
                            ,[ISCLOSED] AS '是否結案'
                            FROM [TKRESEARCH].[dbo].[TB_PROJECTS_PRODUCTS]
                            WHERE 1=1
                            AND [ISCLOSED]='N'
                            ORDER BY [OWNER],[NO]
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
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            //Button2
            //Get the button that raised the event
            Button btn2 = (Button)e.Row.FindControl("Button2");
            //Get the row that contains this button
            GridViewRow gvr2 = (GridViewRow)btn2.NamingContainer;
            //string cellvalue = gvr.Cells[2].Text.Trim();
            string Cellvalue2 = btn2.CommandArgument;
            DataRowView row2 = (DataRowView)e.Row.DataItem;
            Button lbtnName2 = (Button)e.Row.FindControl("Button2");
            ExpandoObject param2 = new { ID = Cellvalue2 }.ToExpando();


        }
    }

    protected void Grid1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int rowIndex = -1;
        // 獲取所選行的索引
        rowIndex = Convert.ToInt32(e.CommandArgument);

        if (e.CommandName == "Button2")
        {
            //MsgBox(e.CommandArgument.ToString() + "OK", this.Page, this);   

            if (rowIndex >= 0)
            {
                // 獲取TextBox的值
                GridViewRow row = Grid1.Rows[rowIndex];
                TextBox txtNewField = (TextBox)row.FindControl("txtNewField");
                string newTextValue = txtNewField.Text;

                Label Label_ID = (Label)row.FindControl("Label_ID");
                Label Label_NO = (Label)row.FindControl("Label_專案編號");
                Label Label_項目名稱 = (Label)row.FindControl("Label_項目名稱");
                Label Label_產品打樣日 = (Label)row.FindControl("Label_產品打樣日");
                Label Label_產品試吃日 = (Label)row.FindControl("Label_產品試吃日");
                Label Label_包裝設計日 = (Label)row.FindControl("Label_包裝設計日");
                Label Label_上市日 = (Label)row.FindControl("Label_上市日");
                Label Label_專案負責人 = (Label)row.FindControl("Label_專案負責人");
                Label Label_是否結案 = (Label)row.FindControl("Label_是否結案");

                string ID = Label_ID.Text;
                string NO = Label_NO.Text;
                string PROJECTNAMES = Label_項目名稱.Text;
                string TRYSDATES = Label_產品打樣日.Text;
                string TASTESDATES = Label_產品試吃日.Text;
                string DESIGNSDATES = Label_包裝設計日.Text;
                string SALESDATES = Label_上市日.Text;
                string OWNER = Label_專案負責人.Text;
                string STATUS = newTextValue;
                string ISCLOSED = Label_是否結案.Text;

                ADD_TB_PROJECTS_PRODUCTS_HISTORYS(                    
                    ID,
                    NO,
                    PROJECTNAMES,
                    TRYSDATES,
                    TASTESDATES,
                    DESIGNSDATES,
                    SALESDATES,
                    OWNER,
                    STATUS,
                    ISCLOSED
                );
            }
                
        }
    }


    public void OnBeforeExport1(object sender, Ede.Uof.Utility.Component.BeforeExportEventArgs e)
    {
        //SETEXCEL();

    }

    public void ADD_TB_PROJECTS_PRODUCTS_HISTORYS(
        string SID,
        string NO,
        string PROJECTNAMES,
        string TRYSDATES,
        string TASTESDATES,
        string DESIGNSDATES,
        string SALESDATES,
        string OWNER,
        string STATUS,
        string ISCLOSED      
        )
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ERPconnectionstring"].ToString();

        var SQLCOMMAND = @"                           
                            INSERT INTO [TKRESEARCH].[dbo].[TB_PROJECTS_PRODUCTS_HISTORYS]
                            (
                            [SID]
                            ,[NO]
                            ,[PROJECTNAMES]
                            ,[TRYSDATES]
                            ,[TASTESDATES]
                            ,[DESIGNSDATES]
                            ,[SALESDATES]
                            ,[OWNER]
                            ,[STATUS]
                            ,[ISCLOSED]
                        
                            )
                            VALUES
                            (
                            @SID
                            ,@NO
                            ,@PROJECTNAMES
                            ,@TRYSDATES
                            ,@TASTESDATES
                            ,@DESIGNSDATES
                            ,@SALESDATES
                            ,@OWNER
                            ,@STATUS
                            ,@ISCLOSED
                        
                            )
                            ";

        try
        {
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(SQLCOMMAND, cnn))
                {
                    cmd.Parameters.AddWithValue("@SID", SID);
                    cmd.Parameters.AddWithValue("@NO", NO);
                    cmd.Parameters.AddWithValue("@PROJECTNAMES", PROJECTNAMES);
                    cmd.Parameters.AddWithValue("@TRYSDATES", TRYSDATES);
                    cmd.Parameters.AddWithValue("@TASTESDATES", TASTESDATES);
                    cmd.Parameters.AddWithValue("@DESIGNSDATES", DESIGNSDATES);
                    cmd.Parameters.AddWithValue("@SALESDATES", SALESDATES);
                    cmd.Parameters.AddWithValue("@OWNER", OWNER);
                    cmd.Parameters.AddWithValue("@STATUS", STATUS);
                    cmd.Parameters.AddWithValue("@ISCLOSED", ISCLOSED);
       


                    cnn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();


                    if (rowsAffected >= 1)
                    {                   
                        MsgBox(NO + " 完成", this.Page, this);
                    }
                }
            }
        }

        catch
        {

        }
        finally
        {

        }
    }

    public void UPDATE_TB_PROJECTS_PRODUCTS()
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