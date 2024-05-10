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
            txtDate1.Text = DateTime.Now.AddDays(-7).ToString("yyyy/MM/dd");
            txtDate2.Text = DateTime.Now.ToString("yyyy/MM/dd");

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

                              
                            ");


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

                BindGrid();


            }
        }
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
        BindGrid();
    }



    #endregion
}