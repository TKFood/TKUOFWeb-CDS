using Ede.Uof.Utility.Data;
using Ede.Uof.Utility.Page.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class CDS_WebPage_TKRESEARCHTBDEVMEMODialogADD : Ede.Uof.Utility.Page.BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
       
        //設定回傳值
        Dialog.SetReturnValue2("");

        //註冊Dialog的Button 狀態
        ((Master_DialogMasterPage)this.Master).Button1CausesValidation = false;
        ((Master_DialogMasterPage)this.Master).Button1AutoCloseWindow = false;
        ((Master_DialogMasterPage)this.Master).Button1OnClick += CDS_WebPage_Dialog_Button1OnClick;
        ((Master_DialogMasterPage)this.Master).Button2OnClick += Button2OnClick;

        if (!IsPostBack)
        {
            //接收主頁面傳遞之參數
            lblParam.Text = Request["ID"];

            BindDropDownList();
            BindDropDownList2();
        }
        
    }




    #region BUTTON
    void CDS_WebPage_Dialog_Button1OnClick()
    {
        //設定回傳值並關閉視窗
        //Dialog.SetReturnValue2(txtReturnValue.Text);

        ADD();
    }

    void Button2OnClick()
    {
        //設定回傳值並關閉視窗
        //Dialog.SetReturnValue2(txtReturnValue.Text);

        ADD();
    }

    #endregion


    #region FUNCTION
    private void BindDropDownList()
    {
        DataSet ds = new DataSet();
        DatabaseHelper DbQuery = new DatabaseHelper();
        DataTable dt = new DataTable();
        DataRow ndr = dt.NewRow();

        dt.Columns.Add("PARAID", typeof(String));
        dt.Columns.Add("PARANAME", typeof(String));

        string connectionString = ConfigurationManager.ConnectionStrings["ERPconnectionstring"].ToString();
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            SqlCommand command = new SqlCommand(@" SELECT  [ID],[KIND],[PARAID],[PARANAME] FROM [TKRESEARCH].[dbo].[TBPARA] WHERE [KIND]='MEMOSTATUS' ORDER BY [PARAID]", conn);

            ds.Clear();

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            conn.Open();

            adapter.Fill(ds, command.ToString());

            if (ds.Tables[0].Rows.Count > 0)
            {
                DropDownList1.DataSource = ds.Tables[0];
                DropDownList1.DataTextField = "PARANAME";
                DropDownList1.DataValueField = "PARANAME";
                DropDownList1.DataBind();

            }
            else
            {

            }
        }
    }

    private void BindDropDownList2()
    {
        DataSet ds = new DataSet();
        DatabaseHelper DbQuery = new DatabaseHelper();
        DataTable dt = new DataTable();
        DataRow ndr = dt.NewRow();

        dt.Columns.Add("PARAID", typeof(String));
        dt.Columns.Add("PARANAME", typeof(String));

        string connectionString = ConfigurationManager.ConnectionStrings["ERPconnectionstring"].ToString();
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            SqlCommand command = new SqlCommand(@" SELECT  [ID],[KIND],[PARAID],[PARANAME] FROM [TKRESEARCH].[dbo].[TBPARA] WHERE [KIND]='DEVKIND' ORDER BY [PARAID]", conn);

            ds.Clear();

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            conn.Open();

            adapter.Fill(ds, command.ToString());

            if (ds.Tables[0].Rows.Count > 0)
            {
                DropDownList2.DataSource = ds.Tables[0];
                DropDownList2.DataTextField = "PARANAME";
                DropDownList2.DataValueField = "PARANAME";
                DropDownList2.DataBind();

            }
            else
            {

            }
        }
    }

    public void ADD()
    {
        string ID = "";
        string SERNO = "";
        string STATUS = DropDownList1.SelectedValue.ToString().Trim();
        string KIND = DropDownList2.SelectedValue.ToString().Trim();
        string CLIENT = TextBox1.Text.ToString().Trim();
        string PROD = TextBox2.Text.ToString().Trim();
        string SPEC = TextBox5.Text.ToString().Trim();     
        string PLACES = TextBox7.Text.ToString().Trim();
        string ONSALES = TextBox8.Text.ToString().Trim();      
        string OWNER = TextBox13.Text.ToString().Trim();
        string MEMO = TextBox14.Text.ToString().Trim();

        if (!string.IsNullOrEmpty(STATUS) && !string.IsNullOrEmpty(CLIENT) && !string.IsNullOrEmpty(PROD))
        {
            ADDTBDEVMEMO(ID, SERNO, STATUS, KIND, CLIENT, PROD, SPEC, PLACES, ONSALES, OWNER, MEMO);
        }

        Dialog.SetReturnValue2("NeedPostBack");
        Dialog.Close(this);
    }
    public void ADDTBDEVMEMO(string ID, string SERNO, string STATUS, string KIND, string CLIENT, string PROD, string SPEC, string PLACES, string ONSALES, string OWNER, string MEMO)
    {
       

        StringBuilder SQL = new StringBuilder();

        string connectionString = ConfigurationManager.ConnectionStrings["ERPconnectionstring"].ToString();

        SQL.AppendFormat(@" INSERT INTO [TKRESEARCH].[dbo].[TBDEVMEMO]");
        SQL.AppendFormat(@" ([STATUS],[KIND],[CLIENT],[PROD],[SPEC],[PLACES],[ONSALES],[OWNER],[MEMO])");
        SQL.AppendFormat(@" VALUES");
        SQL.AppendFormat(@" (@STATUS,@KIND,@CLIENT,@PROD,@SPEC,@PLACES,@ONSALES,@OWNER,@MEMO)");
        SQL.AppendFormat(@" ");

        using (SqlConnection cnn = new SqlConnection(connectionString))
        {
            using (SqlCommand cmd = new SqlCommand(SQL.ToString(), cnn))
            {
                cmd.Parameters.AddWithValue("@STATUS", STATUS);
                cmd.Parameters.AddWithValue("@KIND", KIND);
                cmd.Parameters.AddWithValue("@CLIENT", CLIENT);
                cmd.Parameters.AddWithValue("@PROD", PROD);       
                cmd.Parameters.AddWithValue("@SPEC", SPEC);              
                cmd.Parameters.AddWithValue("@PLACES", PLACES);
                cmd.Parameters.AddWithValue("@ONSALES", ONSALES); 
                cmd.Parameters.AddWithValue("@OWNER", OWNER);
                cmd.Parameters.AddWithValue("@MEMO", MEMO);
               

                cnn.Open();
                cmd.ExecuteNonQuery();
            }
        }


    }
    #endregion


}
