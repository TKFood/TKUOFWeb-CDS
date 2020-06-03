using Ede.Uof.Utility.Data;
using Ede.Uof.Utility.Page.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.ServiceModel.Activation;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class CDS_WebPage_TKRESEARCHTBDEVMEMODialogEDITDEL : Ede.Uof.Utility.Page.BasePage
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

            if (!string.IsNullOrEmpty(lblParam.Text))
            {
                SEARCHTBDEVMEMO(lblParam.Text);
            }

        }
        
    }




    #region BUTTON
    void CDS_WebPage_Dialog_Button1OnClick()
    {
        //設定回傳值並關閉視窗
        //Dialog.SetReturnValue2(txtReturnValue.Text);

        UPDATE();

        Dialog.Close(this);

    }


    void Button2OnClick()
    {
        //設定回傳值並關閉視窗
        //Dialog.SetReturnValue2(txtReturnValue.Text);

        UPDATE();

        SEARCHTBDEVMEMO(lblParam.Text);
    }

    #endregion

    #region FUNCTION
    private void BindDropDownList()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("PARAID", typeof(String));
        dt.Columns.Add("PARANAME", typeof(String));

        string connectionString = ConfigurationManager.ConnectionStrings["ERPconnectionstring"].ToString();
        Ede.Uof.Utility.Data.DatabaseHelper m_db = new Ede.Uof.Utility.Data.DatabaseHelper(connectionString);

        string cmdTxt = @" SELECT  [ID],[KIND],[PARAID],[PARANAME] FROM [TKRESEARCH].[dbo].[TBPARA] WHERE [KIND]='DEVSTATUS' ORDER BY [PARAID] ";

        dt.Load(m_db.ExecuteReader(cmdTxt));

        if (dt.Rows.Count > 0)
        {
            DropDownList1.DataSource = dt;
            DropDownList1.DataTextField = "PARANAME";
            DropDownList1.DataValueField = "PARANAME";
            DropDownList1.DataBind();

        }
        else
        {

        }
    }

    private void BindDropDownList2()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("PARAID", typeof(String));
        dt.Columns.Add("PARANAME", typeof(String));

        string connectionString = ConfigurationManager.ConnectionStrings["ERPconnectionstring"].ToString();
        Ede.Uof.Utility.Data.DatabaseHelper m_db = new Ede.Uof.Utility.Data.DatabaseHelper(connectionString);

        string cmdTxt = @" SELECT  [ID],[KIND],[PARAID],[PARANAME] FROM [TKRESEARCH].[dbo].[TBPARA] WHERE [KIND]='DEVKIND' ORDER BY [PARAID] ";

        dt.Load(m_db.ExecuteReader(cmdTxt));

        if (dt.Rows.Count > 0)
        {
            DropDownList2.DataSource = dt;
            DropDownList2.DataTextField = "PARANAME";
            DropDownList2.DataValueField = "PARANAME";
            DropDownList2.DataBind();

        }
        else
        {

        }
    }

    public void SEARCHTBDEVMEMO(string ID)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ERPconnectionstring"].ToString();
        Ede.Uof.Utility.Data.DatabaseHelper m_db = new Ede.Uof.Utility.Data.DatabaseHelper(connectionString);

        string cmdTxt = @" SELECT [ID],[SERNO],[STATUS],[KIND],[CLIENT],[PROD],[SPEC],[PLACES],[ONSALES],[OWNER],[MEMO] FROM [TKRESEARCH].[dbo].[TBDEVMEMO] WHERE [ID]=@ID ";
        m_db.AddParameter("@ID", ID);

        DataTable dt = new DataTable();

        dt.Load(m_db.ExecuteReader(cmdTxt));

        if (dt.Rows.Count > 0)
        {
            DropDownList1.Text = dt.Rows[0]["STATUS"].ToString();
            DropDownList2.Text = dt.Rows[0]["KIND"].ToString();
            TextBox1.Text = dt.Rows[0]["CLIENT"].ToString();
            TextBox2.Text = dt.Rows[0]["PROD"].ToString();
            TextBox5.Text = dt.Rows[0]["SPEC"].ToString();
            TextBox7.Text = dt.Rows[0]["PLACES"].ToString();
            TextBox8.Text = dt.Rows[0]["ONSALES"].ToString();
            TextBox13.Text = dt.Rows[0]["OWNER"].ToString();
            TextBox14.Text = dt.Rows[0]["MEMO"].ToString();

        }

        //string connectionString = ConfigurationManager.ConnectionStrings["ERPconnectionstring"].ToString();
        //using (SqlConnection conn = new SqlConnection(connectionString))
        //{
        //    SqlCommand command = new SqlCommand(" SELECT [ID],[SERNO],[STATUS],[KIND],[CLIENT],[PROD],[SPEC],[PLACES],[ONSALES],[OWNER],[MEMO] FROM [TKRESEARCH].[dbo].[TBDEVMEMO] WHERE [ID]=@ID", conn);
        //    command.Parameters.AddWithValue("@ID", ID);

        //    DataSet ds = new DataSet();
        //    SqlDataAdapter adapter = new SqlDataAdapter(command);
        //    conn.Open();

        //    adapter.Fill(ds, command.ToString());

        //    if (ds.Tables[0].Rows.Count > 0)
        //    {
        //        DropDownList1.Text = ds.Tables[0].Rows[0]["STATUS"].ToString();
        //        DropDownList2.Text = ds.Tables[0].Rows[0]["KIND"].ToString();
        //        TextBox1.Text= ds.Tables[0].Rows[0]["CLIENT"].ToString();
        //        TextBox2.Text = ds.Tables[0].Rows[0]["PROD"].ToString();               
        //        TextBox5.Text = ds.Tables[0].Rows[0]["SPEC"].ToString();               
        //        TextBox7.Text = ds.Tables[0].Rows[0]["PLACES"].ToString();
        //        TextBox8.Text = ds.Tables[0].Rows[0]["ONSALES"].ToString();                
        //        TextBox13.Text = ds.Tables[0].Rows[0]["OWNER"].ToString();
        //        TextBox14.Text = ds.Tables[0].Rows[0]["MEMO"].ToString();

        //    }
        //}


    }

    public void UPDATE()
    {
        string ID = lblParam.Text;
        string STATUS = DropDownList1.SelectedValue.ToString().Trim();
        string KIND = DropDownList2.SelectedValue.ToString().Trim();
        string CLIENT = TextBox1.Text.ToString().Trim();
        string PROD = TextBox2.Text.ToString().Trim();      
        string SPEC = TextBox5.Text.ToString().Trim();        
        string PLACES = TextBox7.Text.ToString().Trim();
        string ONSALES = TextBox8.Text.ToString().Trim(); 
        string OWNER = TextBox13.Text.ToString().Trim();
        string MEMO = TextBox14.Text.ToString().Trim();

        if (!string.IsNullOrEmpty(ID) && !string.IsNullOrEmpty(STATUS) && !string.IsNullOrEmpty(CLIENT) && !string.IsNullOrEmpty(PROD))
        {
            UPDATETBDEVMEMO(ID, STATUS, KIND, CLIENT, PROD, SPEC, PLACES, ONSALES, OWNER, MEMO);
        }

        Dialog.SetReturnValue2("NeedPostBack");
    }
    public void UPDATETBDEVMEMO(string ID, string STATUS, string KIND, string CLIENT, string PROD, string SPEC, string PLACES, string ONSALES, string OWNER, string MEMO)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ERPconnectionstring"].ToString();
        Ede.Uof.Utility.Data.DatabaseHelper m_db = new Ede.Uof.Utility.Data.DatabaseHelper(connectionString);

        string cmdTxt = @"  UPDATE [TKRESEARCH].[dbo].[TBDEVMEMO] SET STATUS=@STATUS,KIND=@KIND,CLIENT=@CLIENT,PROD=@PROD,SPEC=@SPEC,PLACES=@PLACES,ONSALES=@ONSALES,OWNER=@OWNER,MEMO=@MEMO WHERE[ID]=@ID
                            ";

        m_db.AddParameter("@ID", ID);
        m_db.AddParameter("@STATUS", STATUS);
        m_db.AddParameter("@KIND", KIND);
        m_db.AddParameter("@CLIENT", CLIENT);
        m_db.AddParameter("@PROD", PROD);
        m_db.AddParameter("@SPEC", SPEC);
        m_db.AddParameter("@PLACES", PLACES);
        m_db.AddParameter("@ONSALES", ONSALES);
        m_db.AddParameter("@OWNER", OWNER);
        m_db.AddParameter("@MEMO", MEMO);

        m_db.ExecuteNonQuery(cmdTxt);

        //string connectionString = ConfigurationManager.ConnectionStrings["ERPconnectionstring"].ToString();
        //var SQL = " UPDATE [TKRESEARCH].[dbo].[TBDEVMEMO] SET STATUS=@STATUS,KIND=@KIND,CLIENT=@CLIENT,PROD=@PROD,SPEC=@SPEC,PLACES=@PLACES,ONSALES=@ONSALES,OWNER=@OWNER,MEMO=@MEMO WHERE[ID]=@ID";

        //using (SqlConnection cnn = new SqlConnection(connectionString))
        //{
        //    using (SqlCommand cmd = new SqlCommand(SQL, cnn))
        //    {
        //        cmd.Parameters.AddWithValue("@ID", ID);
        //        cmd.Parameters.AddWithValue("@STATUS", STATUS);
        //        cmd.Parameters.AddWithValue("@KIND", KIND);
        //        cmd.Parameters.AddWithValue("@CLIENT", CLIENT);
        //        cmd.Parameters.AddWithValue("@PROD", PROD);
        //        cmd.Parameters.AddWithValue("@SPEC", SPEC);                
        //        cmd.Parameters.AddWithValue("@PLACES", PLACES);
        //        cmd.Parameters.AddWithValue("@ONSALES", ONSALES);               
        //        cmd.Parameters.AddWithValue("@OWNER", OWNER);
        //        cmd.Parameters.AddWithValue("@MEMO", MEMO);

        //        cnn.Open();
        //        cmd.ExecuteNonQuery();
        //    }
        //}
    }

    protected void btn1_Click(object sender, EventArgs e)
    {
        DELTBSALESDEVMEMO(lblParam.Text);
    }

    public void DELTBSALESDEVMEMO(string ID)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ERPconnectionstring"].ToString();
        Ede.Uof.Utility.Data.DatabaseHelper m_db = new Ede.Uof.Utility.Data.DatabaseHelper(connectionString);

        string cmdTxt = @"  DELETE [TKRESEARCH].[dbo].[TBDEVMEMO]   WHERE[ID]=@ID
                            ";

        m_db.AddParameter("@ID", ID);    

        m_db.ExecuteNonQuery(cmdTxt);

        //string connectionString = ConfigurationManager.ConnectionStrings["ERPconnectionstring"].ToString();
        //var SQL = "DELETE [TKRESEARCH].[dbo].[TBDEVMEMO]   WHERE[ID]=@ID";

        //using (SqlConnection cnn = new SqlConnection(connectionString))
        //{
        //    using (SqlCommand cmd = new SqlCommand(SQL, cnn))
        //    {
        //        cmd.Parameters.AddWithValue("@ID", ID);
        //        cnn.Open();
        //        cmd.ExecuteNonQuery();
        //    }
        //}

        Dialog.SetReturnValue2("NeedPostBack");
        Dialog.Close(this);
    }
    #endregion


}
