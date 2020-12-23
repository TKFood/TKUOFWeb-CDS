using Ede.Uof.Utility.Page;
using Ede.Uof.Utility.Page.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

public partial class CDS_WKF_Fields_ASPX_OptionField_PURTABA_ADD : BasePage
{
    string MB001 = "";
    string MB002 = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        ((Master_DialogMasterPage)this.Master).Button1AutoCloseWindow = false;
        ((Master_DialogMasterPage)this.Master).Button1OnClick += CDS_WKF_Fields_MaintainItem_Button1OnClick;
        ((Master_DialogMasterPage)this.Master).Button2OnClick += CDS_WKF_Fields_MaintainItem_Button2OnClick;

        if (!IsPostBack)
        {
            BindGrid("");
        }
        
    }

    #region FUNCTION
    private void BindGrid(string MB001)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ERPconnectionstring"].ToString();
        Ede.Uof.Utility.Data.DatabaseHelper m_db = new Ede.Uof.Utility.Data.DatabaseHelper(connectionString);

        this.Session["MB001"] = MB001;

        string cmdTxt = @" SELECT TOP 100 MB001,MB002 FROM [TK].dbo.INVMB WHERE MB001 LIKE @MB001+'%' ORDER BY MB001    ";

        m_db.AddParameter("@MB001", MB001);

        DataTable dt = new DataTable();

        dt.Load(m_db.ExecuteReader(cmdTxt));

        Grid1.DataSource = dt;
        Grid1.DataBind();
    }

    public void OnBeforeExport(object sender, Ede.Uof.Utility.Component.BeforeExportEventArgs e)
    {

    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "SELECT")
        {
            //TextBox1.Text = Convert.ToString(e.CommandArgument.ToString());
            //MB001 = Convert.ToString(e.CommandArgument.ToString());

            int rowIndex = Convert.ToInt32(e.CommandArgument);

            //Reference the GridView Row.
            GridViewRow row = Grid1.Rows[rowIndex];


            MB001 = row.Cells[1].Text.Trim();
            MB002 = row.Cells[2].Text.Trim();
            //TextBox1.Text = MB002; 

            Dialog.SetReturnValue2(GetXML(MB001, MB002));

            Dialog.Close(this);
        }
    }

    protected void Grid1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    public string GetXML(string MB001,string MB002)
    {
        XElement xe = new XElement("Item",
            new XAttribute("MB001", MB001),
             new XAttribute("MB002", MB002));

        return xe.ToString();
    }
    #endregion


    #region BUTTON
    protected void Button2_OnClick(object sender, EventArgs e)
    {
        BindGrid(TextBox1.Text);
    }

    private void CDS_WKF_Fields_MaintainItem_Button2OnClick()
    {


    }



    private void CDS_WKF_Fields_MaintainItem_Button1OnClick()
    {



    }
    #endregion

}