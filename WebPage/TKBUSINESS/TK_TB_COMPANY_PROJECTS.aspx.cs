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
using Ede.Uof.EIP.SystemInfo;

public partial class CDS_WebPage_TKBUSINESS_TK_TB_COMPANY_PROJECTSE : Ede.Uof.Utility.Page.BasePage
{
    string DEPDEV_OPEN = "N";
    string ACCOUNT = null;
    string NAME = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        ACCOUNT = Current.Account;
        NAME = Current.User.Name;

        DEPDEV_OPEN = "Y";

        if (!IsPostBack)
        {
            BindDropDownListISCLOSE();

            BindGrid(DropDownListISCLOSE.SelectedValue.ToString());
        }
        else
        {

        }




    }
    #region FUNCTION
    private void BindDropDownListISCLOSE()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("ID", typeof(String));
        dt.Columns.Add("KIND", typeof(String));

        string connectionString = ConfigurationManager.ConnectionStrings["ERPconnectionstring"].ToString();
        Ede.Uof.Utility.Data.DatabaseHelper m_db = new Ede.Uof.Utility.Data.DatabaseHelper(connectionString);

        string cmdTxt = @" 
                        SELECT 
                        [ID]
                        ,[KINDS]
                        ,[NAMES]
                        ,[VALUE]
                        FROM [TKBUSINESS].[dbo].[TBPARA]
                        WHERE [KINDS]='TB_COMPANY_PROJECTS'
                        ORDER BY [ID]
                        ";

        dt.Load(m_db.ExecuteReader(cmdTxt));

        if (dt.Rows.Count > 0)
        {
            DropDownListISCLOSE.DataSource = dt;
            DropDownListISCLOSE.DataTextField = "NAMES";
            DropDownListISCLOSE.DataValueField = "NAMES";
            DropDownListISCLOSE.DataBind();

        }
        else
        {

        }
    }

    private void BindGrid(string DropDownListISCLOSE)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ERPconnectionstring"].ToString();
        Ede.Uof.Utility.Data.DatabaseHelper m_db = new Ede.Uof.Utility.Data.DatabaseHelper(connectionString);

        StringBuilder cmdTxt = new StringBuilder();
        StringBuilder Query1 = new StringBuilder();

        if (!string.IsNullOrEmpty(DropDownListISCLOSE))
        {
            if (DropDownListISCLOSE.Equals("全部"))
            {
                Query1.AppendFormat(@"");
            }
            else
            {
                Query1.AppendFormat(@"  AND ID IN ( SELECT [ID] FROM[TKBUSINESS].[dbo].[TB_COMPANY_PROJECTS] WHERE [ISCLOSED] LIKE '%{0}%' ) ", DropDownListISCLOSE);
            }

        }
        else
        {
            Query1.AppendFormat(@"");
        }

        cmdTxt.AppendFormat(@" 
                           SELECT 
                            [ID]
                            ,[NO]
                            ,[ISCLOSED]
                            ,[KINDS]
                            ,[PROJECTNAMES]
                            ,[DEPNAMES]
                            ,[PRODUCTAPPLYS]
                            ,[PACKAPPLYS]
                            ,[SALEDATES]
                            ,[STATUS]
                            ,[COMMENTS]
                            ,[COMMENTSDATES]
                            ,[TRACEDATES]
                            FROM [TKBUSINESS].[dbo].[TB_COMPANY_PROJECTS]
                            WHERE [ISCLOSED]='N'
                            {0}

                            ORDER BY [NO]

                              
                            ", Query1.ToString());


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
        int rowIndex = -1;

        if (e.CommandName == "Grid1Button1")
        {
            // 獲取所選行的索引
            rowIndex = Convert.ToInt32(e.CommandArgument);
            // 在GridView中找到所選行的索引


            // 確保找到了有效的行
            if (rowIndex >= 0)
            {
                GridViewRow row = Grid1.Rows[rowIndex];
                // 獲取相應的ID
                Label LabelID = (Label)row.FindControl("ID");
                string ID = LabelID.Text;
                // 獲取TextBox的值                 
                TextBox txtNewField = (TextBox)row.FindControl("txtNewField");
                string newTextValue = txtNewField.Text;

                string MID = ID;
                string DEPNAMES = "";
                string COMMETNS = newTextValue;

                if (DEPDEV_OPEN.Equals("Y"))
                {
                    DEPNAMES = "研發";
                    ADD_TB_COMPANY_PROJECTS_DETAILS(MID, DEPNAMES, COMMETNS);
                    UPDATE_TB_COMPANY_PROJECTS(MID, DEPNAMES, COMMETNS);

                    MsgBox("成功 \r\n" + ID + " > " + newTextValue, this.Page, this);
                }

                BindGrid(DropDownListISCLOSE.SelectedValue.ToString());


            }
        }
    }

    public void OnBeforeExport1(object sender, Ede.Uof.Utility.Component.BeforeExportEventArgs e)
    {
        SETEXCEL();


    }


    public override void VerifyRenderingInServerForm(Control control)
    {

    }

    public void SETEXCEL()
    {
        

    }

    public void MsgBox(String ex, Page pg, Object obj)
    {
        string s = "<SCRIPT language='javascript'>alert('" + ex.Replace("\r\n", "\\n").Replace("'", "") + "'); </SCRIPT>";
        Type cstype = obj.GetType();
        ClientScriptManager cs = pg.ClientScript;
        cs.RegisterClientScriptBlock(cstype, s, s.ToString());
    }

    public void ADD_TB_COMPANY_PROJECTS_DETAILS(string MID, string DEPNAMES, string COMMETNS)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ERPconnectionstring"].ToString();
        Ede.Uof.Utility.Data.DatabaseHelper m_db = new Ede.Uof.Utility.Data.DatabaseHelper(connectionString);

        string cmdTxt = @"   ";


        cmdTxt = @"
                    INSERT INTO [TKBUSINESS].[dbo].[TB_COMPANY_PROJECTS_DETAILS]
                    (
                    [MID]
                    ,[CREATEDATES]
                    ,[DEPNAMES]
                    ,[COMMETNS]
                    )
                    VALUES
                    (
                    @MID
                    ,GETDATE()
                    ,@DEPNAMES
                    ,@COMMETNS
                    )
                        ";


        m_db.AddParameter("@MID", MID);
        m_db.AddParameter("@DEPNAMES", DEPNAMES);
        m_db.AddParameter("@COMMETNS", NAME + ':' + Environment.NewLine +COMMETNS);

        m_db.ExecuteNonQuery(cmdTxt);
    }

    public void UPDATE_TB_COMPANY_PROJECTS(string ID, string DEPNAMES, string COMMETNS)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ERPconnectionstring"].ToString();
        Ede.Uof.Utility.Data.DatabaseHelper m_db = new Ede.Uof.Utility.Data.DatabaseHelper(connectionString);

        string cmdTxt = @"   ";

        if(DEPDEV_OPEN.Equals("Y"))
        {
            cmdTxt = @"
                     UPDATE  [TKBUSINESS].[dbo].[TB_COMPANY_PROJECTS]
                    SET [DEPDEV]=@DEPDEV,[DEPDEVREPLAYDATES]=GETDATE()
                    WHERE ID=@ID
                        ";
        }
        else
        {

        }
       


        m_db.AddParameter("@ID", ID);
        m_db.AddParameter("@DEPDEV", NAME + ':' + Environment.NewLine + COMMETNS);


        m_db.ExecuteNonQuery(cmdTxt);
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
        BindGrid(DropDownListISCLOSE.SelectedValue.ToString());
    }



    #endregion
}