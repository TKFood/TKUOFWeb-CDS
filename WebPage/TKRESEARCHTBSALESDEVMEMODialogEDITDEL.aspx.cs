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


public partial class CDS_WebPage_TKRESEARCHTBSALESDEVMEMODialogEDITDEL : Ede.Uof.Utility.Page.BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
       
        //設定回傳值
        Dialog.SetReturnValue2("");

        //註冊Dialog的Button 狀態
        ((Master_DialogMasterPage)this.Master).Button1CausesValidation = false;
        ((Master_DialogMasterPage)this.Master).Button1AutoCloseWindow = false;
        ((Master_DialogMasterPage)this.Master).Button1OnClick += CDS_WebPage_Dialog_Button1OnClick;
  

        if (!IsPostBack)
        {
            //接收主頁面傳遞之參數
            lblParam.Text = Request["ID"];

            BindDropDownList();

            if (!string.IsNullOrEmpty(lblParam.Text))
            {
                SEARCHTTBSALESDEVMEMO(lblParam.Text);
            }

        }
        
    }





    void CDS_WebPage_Dialog_Button1OnClick()
    {
        //設定回傳值並關閉視窗
        //Dialog.SetReturnValue2(txtReturnValue.Text);
        string ID = lblParam.Text;
        string STATUS = DropDownList1.SelectedValue.ToString().Trim();
        string CLIENT = TextBox1.Text.ToString().Trim();
        string PROD = TextBox2.Text.ToString().Trim();
        string PRICES = TextBox3.Text.ToString().Trim();
        string PROMOTIONS = TextBox4.Text.ToString().Trim();
        string SPEC = TextBox5.Text.ToString().Trim();
        string VALID = TextBox6.Text.ToString().Trim();
        string PLACES = TextBox7.Text.ToString().Trim();
        string ONSALES = TextBox8.Text.ToString().Trim();
        string PRODESGIN = TextBox9.Text.ToString().Trim();
        string ASSESSMENTDATES = txtDate1.Text.ToString().Trim();
        string COSTSDATES = txtDate2.Text.ToString().Trim();
        string SALESPRICES = TextBox10.Text.ToString().Trim();
        string TEST = TextBox11.Text.ToString().Trim();
        string TESTDATES = txtDate3.Text.ToString().Trim();
        string OWNER = TextBox13.Text.ToString().Trim();
        string MEMO = TextBox14.Text.ToString().Trim();
        if (!string.IsNullOrEmpty(ID) && !string.IsNullOrEmpty(STATUS) && !string.IsNullOrEmpty(CLIENT) && !string.IsNullOrEmpty(PROD))
        {
            UPDATETBSALESDEVMEMO(ID, STATUS, CLIENT, PROD, PRICES, PROMOTIONS, SPEC, VALID, PLACES, ONSALES, PRODESGIN, ASSESSMENTDATES, COSTSDATES, SALESPRICES, TEST, TESTDATES, OWNER, MEMO);
        }

        Dialog.SetReturnValue2("NeedPostBack");
        Dialog.Close(this);

    }





    #region
    private void BindDropDownList()
    {
        DataSet ds = new DataSet();
        DatabaseHelper DbQuery = new DatabaseHelper();
        DataTable dt = new DataTable();
        DataRow ndr = dt.NewRow();

        dt.Columns.Add("Filed1", typeof(String));
        dt.Columns.Add("Filed2", typeof(String));
        //1.二聯式、2.三聯式、3.二聯式收銀機發票、4.三聯式收銀機發票、5.電子計算機發票、6.免用統一發票、7.電子發票

        dt.Rows.Add(new Object[] { "進行中", "進行中" });
        dt.Rows.Add(new Object[] { "已完成", "已完成" });



        DropDownList1.DataSource = dt;
        DropDownList1.DataTextField = "Filed2";
        DropDownList1.DataValueField = "Filed1";
        DropDownList1.DataBind();
    }

    public void SEARCHTTBSALESDEVMEMO(string ID)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ERPconnectionstring"].ToString();
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            SqlCommand command = new SqlCommand(" SELECT [ID],[SERNO],[STATUS],[CLIENT],[PROD],[PRICES],[PROMOTIONS],[SPEC],[VALID],[PLACES],[ONSALES],[PRODESGIN],CONVERT(NVARCHAR,[ASSESSMENTDATES],111) ASSESSMENTDATES,CONVERT(NVARCHAR,[COSTSDATES],111) COSTSDATES,[SALESPRICES],[TEST],CONVERT(NVARCHAR,[TESTDATES],111) TESTDATES,[OWNER],[MEMO] FROM [TKRESEARCH].[dbo].[TBSALESDEVMEMO] WHERE [ID]=@ID", conn);
            command.Parameters.AddWithValue("@ID", ID);

            DataSet ds = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            conn.Open();

            adapter.Fill(ds, command.ToString());

            if (ds.Tables[0].Rows.Count > 0)
            {
                DropDownList1.Text = ds.Tables[0].Rows[0]["STATUS"].ToString();
                TextBox1.Text= ds.Tables[0].Rows[0]["CLIENT"].ToString();
                TextBox2.Text = ds.Tables[0].Rows[0]["PROD"].ToString();
                TextBox3.Text = ds.Tables[0].Rows[0]["PRICES"].ToString();
                TextBox4.Text = ds.Tables[0].Rows[0]["PROMOTIONS"].ToString();
                TextBox5.Text = ds.Tables[0].Rows[0]["SPEC"].ToString();
                TextBox6.Text = ds.Tables[0].Rows[0]["VALID"].ToString();
                TextBox7.Text = ds.Tables[0].Rows[0]["PLACES"].ToString();
                TextBox8.Text = ds.Tables[0].Rows[0]["ONSALES"].ToString();
                TextBox9.Text = ds.Tables[0].Rows[0]["PRODESGIN"].ToString();
                txtDate1.Text = ds.Tables[0].Rows[0]["ASSESSMENTDATES"].ToString();
                txtDate2.Text = ds.Tables[0].Rows[0]["COSTSDATES"].ToString();
                TextBox10.Text = ds.Tables[0].Rows[0]["SALESPRICES"].ToString();
                TextBox11.Text = ds.Tables[0].Rows[0]["TEST"].ToString();
                txtDate3.Text = ds.Tables[0].Rows[0]["TESTDATES"].ToString();
                TextBox13.Text = ds.Tables[0].Rows[0]["OWNER"].ToString();
                TextBox14.Text = ds.Tables[0].Rows[0]["MEMO"].ToString();

            }
        }


    }

    public void UPDATETBSALESDEVMEMO(string ID,string STATUS,string CLIENT, string PROD, string PRICES, string PROMOTIONS, string SPEC, string VALID, string PLACES, string ONSALES, string PRODESGIN, string ASSESSMENTDATES, string COSTSDATES, string SALESPRICES, string TEST, string TESTDATES,string OWNER, string MEMO)
    {
        if (string.IsNullOrEmpty(ASSESSMENTDATES))
        {
            ASSESSMENTDATES = "1911/1/1";   
        }
        if (string.IsNullOrEmpty(COSTSDATES))
        {
            COSTSDATES = "1911/1/1";
        }
        if (string.IsNullOrEmpty(TESTDATES))
        {
            TESTDATES = "1911/1/1";
        }
    

        string connectionString = ConfigurationManager.ConnectionStrings["ERPconnectionstring"].ToString();
        var SQL = "UPDATE [TKRESEARCH].[dbo].[TBSALESDEVMEMO] SET [STATUS]=@STATUS,[CLIENT]=@CLIENT,[PROD]=@PROD,[PRICES]=@PRICES,[PROMOTIONS]=@PROMOTIONS,[SPEC]=@SPEC,[VALID]=@VALID,[PLACES]=@PLACES,[ONSALES]=@ONSALES,[PRODESGIN]=@PRODESGIN,[ASSESSMENTDATES]=@ASSESSMENTDATES,[COSTSDATES]=@COSTSDATES,[SALESPRICES]=@SALESPRICES,[TEST]=@TEST,[TESTDATES]=@TESTDATES,[OWNER]=@OWNER,[MEMO]=@MEMO WHERE[ID]=@ID";

        using (SqlConnection cnn = new SqlConnection(connectionString))
        {
            using (SqlCommand cmd = new SqlCommand(SQL, cnn))
            {
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@STATUS", STATUS);
                cmd.Parameters.AddWithValue("@CLIENT", CLIENT);
                cmd.Parameters.AddWithValue("@PROD", PROD);
                cmd.Parameters.AddWithValue("@PRICES", PRICES);
                cmd.Parameters.AddWithValue("@PROMOTIONS", PROMOTIONS);
                cmd.Parameters.AddWithValue("@SPEC", SPEC);
                cmd.Parameters.AddWithValue("@VALID", VALID);
                cmd.Parameters.AddWithValue("@PLACES", PLACES);
                cmd.Parameters.AddWithValue("@ONSALES", ONSALES);
                cmd.Parameters.AddWithValue("@PRODESGIN", PRODESGIN);
                cmd.Parameters.AddWithValue("@ASSESSMENTDATES", Convert.ToDateTime(ASSESSMENTDATES));
                cmd.Parameters.AddWithValue("@COSTSDATES", Convert.ToDateTime(COSTSDATES));
                cmd.Parameters.AddWithValue("@SALESPRICES", SALESPRICES);
                cmd.Parameters.AddWithValue("@TEST", TEST);
                cmd.Parameters.AddWithValue("@TESTDATES", Convert.ToDateTime(TESTDATES));
                cmd.Parameters.AddWithValue("@OWNER", OWNER);
                cmd.Parameters.AddWithValue("@MEMO", MEMO);


                cnn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }

    protected void btn1_Click(object sender, EventArgs e)
    {
        DELTBSALESDEVMEMO(lblParam.Text);
    }

    public void DELTBSALESDEVMEMO(string ID)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ERPconnectionstring"].ToString();
        var SQL = "DELETE [TKRESEARCH].[dbo].[TBSALESDEVMEMO]  WHERE[ID]=@ID";

        using (SqlConnection cnn = new SqlConnection(connectionString))
        {
            using (SqlCommand cmd = new SqlCommand(SQL, cnn))
            {
                cmd.Parameters.AddWithValue("@ID", ID);
                cnn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        Dialog.SetReturnValue2("NeedPostBack");
        Dialog.Close(this);
    }
    #endregion


}
