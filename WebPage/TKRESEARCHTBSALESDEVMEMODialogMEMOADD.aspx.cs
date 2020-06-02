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


public partial class CDS_WebPage_TKRESEARCHTBSALESDEVMEMODialogMEMOADD : Ede.Uof.Utility.Page.BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
       
        //設定回傳值
        Dialog.SetReturnValue2("");

        //註冊Dialog的Button 狀態
        ((Master_DialogMasterPage)this.Master).Button1CausesValidation = false;
        ((Master_DialogMasterPage)this.Master).Button1AutoCloseWindow = false;
        ((Master_DialogMasterPage)this.Master).Button1OnClick += CDS_WebPage_Dialog_Button1OnClick;
        ((Master_DialogMasterPage)this.Master).Button2ClientOnClick = "Button2OnClick";

        if (!IsPostBack)
        {
            //接收主頁面傳遞之參數
            lblParam.Text = Request["ID"];

            if (!string.IsNullOrEmpty(lblParam.Text))
            {
                BindGrid(lblParam.Text);
            }

        }
        
    }





    void CDS_WebPage_Dialog_Button1OnClick()
    {
        //設定回傳值並關閉視窗
        //Dialog.SetReturnValue2(txtReturnValue.Text);

        ADDTBSALESDEVMEMOHISTORY(lblParam.Text, TextBox1.Text);
        Dialog.SetReturnValue2("NeedPostBack");
        Dialog.Close(this);

    }





    #region



    private void BindGrid(string ID)
    {
        //建立Grid資料
        DataSet ds = new DataSet();
        DatabaseHelper DbQuery = new DatabaseHelper();
        DataTable dt = new DataTable();

        //資源來源-用SqlCommand +SqlDataAdapter +DataTable 來查詢
        string connectionString = ConfigurationManager.ConnectionStrings["ERPconnectionstring"].ToString();
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            SqlCommand command = new SqlCommand("SELECT CONVERT(NVARCHAR,[MEMODATES],111) AS MEMODATES ,[PROD],[MEMO],[ID],[PID] FROM [TKRESEARCH].[dbo].[TBSALESDEVMEMOHISTORY] WHERE [PID]=@ID  ORDER BY [MEMODATES]", conn);
            command.Parameters.AddWithValue("@ID", ID);
            ds.Clear();

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            conn.Open();

            adapter.Fill(ds, command.ToString());
        }

        if(ds.Tables[0].Rows.Count>0)
        {
            TextBox2.Text = ds.Tables[0].Rows[0]["PROD"].ToString();
        }

        Grid1.DataSource = ds.Tables[0];
        Grid1.DataBind();
    }

    public void ADDTBSALESDEVMEMOHISTORY(string PID, string MEMO)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ERPconnectionstring"].ToString();
        var SQL = "";

        using (SqlConnection cnn = new SqlConnection(connectionString))
        {
            using (SqlCommand cmd = new SqlCommand(SQL, cnn))
            {
                cmd.Parameters.AddWithValue("@ID", ID);
               


                cnn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }

   
    #endregion


}
