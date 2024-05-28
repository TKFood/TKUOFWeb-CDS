using System;
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
using Ede.Uof.EIP.SystemInfo;
using Ede.Uof.Utility.Data;
using Ede.Uof.Utility.Page.Common;

public partial class CDS_WebPage_COP_TB_SALES_PROMOTIONS : Ede.Uof.Utility.Page.BasePage
{
    string ACCOUNTS = null;
    string NAMES = null;
    string FIRSTTIMES = "N";

    protected void Page_Load(object sender, EventArgs e)
    {
        ACCOUNTS = Current.Account;
        NAMES = Current.User.Name;

        if (!IsPostBack)
        {
            FIRSTTIMES = "Y";

            BindGrid();

            BindDropDownListISCLOSE();
            BindDropDownListKINDS();
        }
        else
        {
            FIRSTTIMES = "N";

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
                        WHERE [KINDS]='TB_SALES_PROMOTIONS_ISCLOSED'
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
    private void BindDropDownListKINDS()
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
                        WHERE [KINDS]='TB_SALES_PROMOTIONS_KINDS'
                        ORDER BY [VALUE]
                        ";

        dt.Load(m_db.ExecuteReader(cmdTxt));

        if (dt.Rows.Count > 0)
        {
            DropDownListKINDS.DataSource = dt;
            DropDownListKINDS.DataTextField = "NAMES";
            DropDownListKINDS.DataValueField = "NAMES";
            DropDownListKINDS.DataBind();

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
        StringBuilder QUERYS1 = new StringBuilder();
        StringBuilder QUERYS2 = new StringBuilder();
        StringBuilder QUERYS3 = new StringBuilder();
        StringBuilder QUERYS4 = new StringBuilder();

        // 通路名稱
        if (!string.IsNullOrEmpty(TextBox1.Text) && !string.IsNullOrEmpty(TextBox1.Text))
        {
            QUERYS1.AppendFormat(@" AND [SALESTO] LIKE '%{0}%'", TextBox1.Text.Trim());

        }
        else
        {
            QUERYS1.AppendFormat(@" ");
        }
        // 產品規格
        if (!string.IsNullOrEmpty(TextBox2.Text) && !string.IsNullOrEmpty(TextBox2.Text))
        {
            QUERYS2.AppendFormat(@" AND [PRODUCTS] LIKE '%{0}%'", TextBox2.Text.Trim());

        }
        else
        {
            QUERYS2.AppendFormat(@" ");
        }
        // 是否結案
        string String_DropDownListISCLOSE= DropDownListISCLOSE.SelectedValue.ToString();
        if (!string.IsNullOrEmpty(String_DropDownListISCLOSE))
        {
            if (String_DropDownListISCLOSE.ToString().Equals("全部"))
            {
                QUERYS3.AppendFormat(@"");
            }
            else
            {
                QUERYS3.AppendFormat(@"  AND ID IN ( SELECT [ID] FROM [TKBUSINESS].[dbo].[TB_SALES_PROMOTIONS] WHERE [ISCLOSES] LIKE '%{0}%' ) ", String_DropDownListISCLOSE);
            }

        }
        else
        {
            QUERYS3.AppendFormat(@"");
        }
        // 活動類型
        string String_DropDownListKINDS = DropDownListKINDS.SelectedValue.ToString();
        if (!string.IsNullOrEmpty(String_DropDownListKINDS))
        {
            if (DropDownListKINDS.SelectedValue.ToString().Equals("全部"))
            {
                QUERYS4.AppendFormat(@"");
            }
            else
            {
                QUERYS4.AppendFormat(@"  AND ID IN ( SELECT [ID] FROM[TKBUSINESS].[dbo].[TB_SALES_PROMOTIONS] WHERE [KINIDS] LIKE '%{0}%' ) ", String_DropDownListKINDS);
            }

        }
        else
        {
            QUERYS4.AppendFormat(@"");
        }

        cmdTxt.AppendFormat(@"
                            SELECT 
                             [ID]
                            ,[ISCLOSES]
                            ,[SALESTO]
                            ,[SDATES]
                            ,[PRODUCTS]
                            ,[SHIPDATES]
                            ,[KINIDS]
                            ,[CONTEXTS]
                            FROM [TKBUSINESS].[dbo].[TB_SALES_PROMOTIONS]
                            WHERE 1=1 
                            {0}
                            {1}
                            {2}
                            {3}

                            ORDER BY [SDATES]
                        ", QUERYS1.ToString(), QUERYS2.ToString(), QUERYS3.ToString(), QUERYS4.ToString());
       

        //m_db.AddParameter("@SDATE", SDATE);
        //m_db.AddParameter("@EDATE", EDATE);

        DataTable dt = new DataTable();

        dt.Load(m_db.ExecuteReader(cmdTxt.ToString()));

        Grid1.DataSource = dt;
        Grid1.DataBind();

        if (FIRSTTIMES.Equals("N"))
        {
            MsgBox("完成 \r\n", this.Page, this);
        }
    }

    protected void grid1_PageIndexChanging(object sender, GridViewPageEventArgs e)
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

        if (e.CommandName == "Button1")
        {
          
        }
       

    }

    public void OnBeforeExport1(object sender, Ede.Uof.Utility.Component.BeforeExportEventArgs e)
    {
        
    }
    public void MsgBox(string ex, Page pg, object obj)
    {
        string script = "alert('" + ex.Replace("\r\n", "\\n").Replace("'", "") + "');";
        ScriptManager.RegisterStartupScript(pg, obj.GetType(), "AlertScript", script, true);
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
       
        
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        BindGrid();        

    }

    #endregion
}