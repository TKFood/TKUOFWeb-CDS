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

public partial class CDS_WebPage_TKREPORTtb_NOTE : Ede.Uof.Utility.Page.BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {

        //用ExpandoObject物件傳遞參數
        //ExpandoObject param = new { Name = "asd" }.ToExpando();
        //ExpandoObject param = new { Name = txtParam.Text }.ToExpando();

        //因為執行此行後，才會把JS的Event註冊到頁面上，所以過此行後下一次按btn元件的Event才會開窗並傳參數
        //故Dialog.Open2適合於參數為固定式的
        //Dialog.Open2(btn, "~/CDS/WebPage/Dialog.aspx", "", 800, 600, Dialog.PostBackType.Allows, param);


        if (!IsPostBack)
        {
            BindGrid();
        }
        else
        {
            BindGrid();
        }
        

        //BindDropDownList();

        //if (this.Session["STATUS"] != null)
        //{
        //    DropDownList1.Text = this.Session["STATUS"].ToString();

        //}

    }

    #region FUNCTION
    
    private void BindGrid()
    {
        string connectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
        Ede.Uof.Utility.Data.DatabaseHelper m_db = new Ede.Uof.Utility.Data.DatabaseHelper(connectionString);

        //this.Session["STATUS"] = STATUS;

        string cmdTxt = @" SELECT [COMPANY_NAME] ,[NOTE_CONTENT] ,[tb_NOTE].[CREATE_DATETIME] 
                           FROM [HJ_BM_DB].[dbo].[tb_NOTE],[HJ_BM_DB].[dbo].[tb_COMPANY] 
                           WHERE [tb_COMPANY].COMPANY_ID=[tb_NOTE].COMPANY_ID 
                           ORDER BY [COMPANY_NAME],[tb_NOTE].[CREATE_DATETIME]

                        ";

        //m_db.AddParameter("@STATUS", STATUS);

        DataTable dt = new DataTable();

        dt.Load(m_db.ExecuteReader(cmdTxt));

        Grid1.DataSource = dt;
        Grid1.DataBind();
    }

    protected void Grid1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {
    //        DataRowView row = (DataRowView)e.Row.DataItem;
    //        LinkButton lbtnName = (LinkButton)e.Row.FindControl("lbtnName");

    //        ExpandoObject param = new { ID = row["ID"].ToString() }.ToExpando();

    //        //Grid開窗是用RowDataBound事件再開窗
    //        Dialog.Open2(lbtnName, "~/CDS/WebPage/TKRESEARCHTBDEVMEMODialogEDITDEL.aspx", "", 800, 600, Dialog.PostBackType.AfterReturn, param);
    //    }


    }

    public void OnBeforeExport(object sender,Ede.Uof.Utility.Component.BeforeExportEventArgs e)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ERPconnectionstring"].ToString();
        Ede.Uof.Utility.Data.DatabaseHelper m_db = new Ede.Uof.Utility.Data.DatabaseHelper(connectionString);

        string STATUS = DropDownList1.Text;

        string cmdTxt = @" SELECT [COMPANY_NAME] ,[NOTE_CONTENT] ,[tb_NOTE].[CREATE_DATETIME] 
                           FROM [HJ_BM_DB].[dbo].[tb_NOTE],[HJ_BM_DB].[dbo].[tb_COMPANY] 
                           WHERE [tb_COMPANY].COMPANY_ID=[tb_NOTE].COMPANY_ID 
                           ORDER BY [COMPANY_NAME],[tb_NOTE].[CREATE_DATETIME]

                        ";

        m_db.AddParameter("@STATUS", STATUS);

        DataTable dt = new DataTable();

        dt.Load(m_db.ExecuteReader(cmdTxt));

        if (dt.Rows.Count>0)
        {
            dt.Columns[0].Caption = "狀態";
            dt.Columns[1].Caption = "類別";
            dt.Columns[2].Caption = "客戶";
            dt.Columns[3].Caption = "產品品項";
            dt.Columns[4].Caption = "規格及屬性";
            dt.Columns[5].Caption = "通路";
            dt.Columns[6].Caption = "預估上市日期";
            dt.Columns[7].Caption = "負責業務";
            dt.Columns[8].Caption = "業務進度";

            e.Datasource = dt;
        }
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

    protected void btn3_Click(object sender, EventArgs e)
    {

    }

    protected void btn4_Click(object sender, EventArgs e)
    {

    }

    protected void btn5_Click(object sender, EventArgs e)
    {
        if(!string.IsNullOrEmpty(Dialog.GetReturnValue()))
        {
            if (Dialog.GetReturnValue().Equals("NeedPostBack"))
            {                
                //this.Session["STATUS"] = DropDownList1.Text.Trim();
            }

        }
    }

    protected void btn6_Click(object sender, EventArgs e)
    {
        //this.Session["STATUS"] = DropDownList1.Text.Trim();
    }

    #endregion
}