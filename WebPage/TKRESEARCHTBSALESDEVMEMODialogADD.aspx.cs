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


public partial class CDS_WebPage_TKRESEARCHTBSALESDEVMEMODialogADD : Ede.Uof.Utility.Page.BasePage
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
        }
        
    }





    void CDS_WebPage_Dialog_Button1OnClick()
    {
        //設定回傳值並關閉視窗
        //Dialog.SetReturnValue2(txtReturnValue.Text);

        string ID = "";
        string SERNO = "";
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

        if (!string.IsNullOrEmpty(STATUS) && !string.IsNullOrEmpty(CLIENT) && !string.IsNullOrEmpty(PROD))
        {
            ADDTBSALESDEVMEMO(ID, SERNO, STATUS, CLIENT, PROD, PRICES, PROMOTIONS, SPEC, VALID, PLACES, ONSALES, PRODESGIN, ASSESSMENTDATES, COSTSDATES, SALESPRICES, TEST, TESTDATES, OWNER, MEMO);
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

    public void ADDTBSALESDEVMEMO(string ID, string SERNO, string STATUS, string CLIENT, string PROD, string PRICES, string PROMOTIONS, string SPEC, string VALID, string PLACES, string ONSALES, string PRODESGIN, string ASSESSMENTDATES, string COSTSDATES, string SALESPRICES, string TEST, string TESTDATES, string OWNER, string MEMO)
    {
        StringBuilder SQL = new StringBuilder();

        string connectionString = ConfigurationManager.ConnectionStrings["ERPconnectionstring"].ToString();

        SQL.AppendFormat(@" INSERT INTO [TKRESEARCH].[dbo].[TBSALESDEVMEMO]");
        SQL.AppendFormat(@" ([STATUS],[CLIENT],[PROD],[PRICES],[PROMOTIONS],[SPEC],[VALID],[PLACES],[ONSALES],[PRODESGIN],[ASSESSMENTDATES],[COSTSDATES],[SALESPRICES],[TEST],[TESTDATES],[OWNER],[MEMO])");
        SQL.AppendFormat(@" VALUES");
        SQL.AppendFormat(@" (@STATUS,@CLIENT,@PROD,@PRICES,@PROMOTIONS,@SPEC,@VALID,@PLACES,@ONSALES,@PRODESGIN,@ASSESSMENTDATES,@COSTSDATES,@SALESPRICES,@TEST,@TESTDATES,@OWNER,@MEMO)");
        SQL.AppendFormat(@" ");

        using (SqlConnection cnn = new SqlConnection(connectionString))
        {
            using (SqlCommand cmd = new SqlCommand(SQL.ToString(), cnn))
            {
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
    #endregion


}
