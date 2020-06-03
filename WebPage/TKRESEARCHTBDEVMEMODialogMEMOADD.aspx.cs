using Ede.Uof.Utility.Component;
using Ede.Uof.Utility.Data;
using Ede.Uof.Utility.Page.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.ServiceModel.Activation;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class CDS_WebPage_TKRESEARCHTBDEVMEMODialogMEMOADD : Ede.Uof.Utility.Page.BasePage
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

            if (!string.IsNullOrEmpty(lblParam.Text))
            {                
                BindGrid(lblParam.Text);
                SEARCHTBDEVMEMO(lblParam.Text);
            }

        }
        
    }




    #region BUTTON
    void CDS_WebPage_Dialog_Button1OnClick()
    {
        //設定回傳值並關閉視窗
        //Dialog.SetReturnValue2(txtReturnValue.Text);
        if(!string.IsNullOrEmpty(lblParam.Text)&& !string.IsNullOrEmpty(TextBox2.Text) && !string.IsNullOrEmpty(TextBox1.Text) )
        {
            ADDTBDEVMEMOHISTORY(lblParam.Text, TextBox2.Text.Trim(), TextBox1.Text);
            UPDATETBDEVMEMO(lblParam.Text, TextBox1.Text);
        }
        
        Dialog.SetReturnValue2("NeedPostBack");
        Dialog.Close(this);

    }


    void Button2OnClick()
    {
        if (!string.IsNullOrEmpty(lblParam.Text) && !string.IsNullOrEmpty(TextBox2.Text) && !string.IsNullOrEmpty(TextBox1.Text))
        {
            ADDTBDEVMEMOHISTORY(lblParam.Text, TextBox2.Text.Trim(), TextBox1.Text);
            UPDATETBDEVMEMO(lblParam.Text, TextBox1.Text);
        }

        BindGrid(lblParam.Text);

        Dialog.SetReturnValue2("NeedPostBack");
    }


    #endregion


    #region FUNCTION



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
            SqlCommand command = new SqlCommand("SELECT CONVERT(NVARCHAR,[MEMODATES],120) AS MEMODATES ,[PROD],[MEMO],[ID],[PID] FROM [TKRESEARCH].[dbo].[TBDEVMEMOHISTORY] WHERE [PID]=@ID ORDER BY [MEMODATES] DESC", conn);
            command.Parameters.AddWithValue("@ID", ID);
            ds.Clear();

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            conn.Open();

            adapter.Fill(ds, command.ToString());
        }


        Grid1.DataSource = ds.Tables[0];        
        Grid1.DataBind();
              


    }

    public void SEARCHTBDEVMEMO(string ID)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ERPconnectionstring"].ToString();
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            SqlCommand command = new SqlCommand(" SELECT [ID],[SERNO],[STATUS],[KIND],[CLIENT],[PROD],[SPEC],[PLACES],[ONSALES],[OWNER],[MEMO] FROM [TKRESEARCH].[dbo].[TBDEVMEMO] WHERE [ID]=@ID", conn);
            command.Parameters.AddWithValue("@ID", ID);

            DataSet ds = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            conn.Open();

            adapter.Fill(ds, command.ToString());

            if (ds.Tables[0].Rows.Count > 0)
            {
                TextBox2.Text = ds.Tables[0].Rows[0]["PROD"].ToString();
            }
        }
    }

    public void ADDTBDEVMEMOHISTORY(string PID,string PROD, string MEMO)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ERPconnectionstring"].ToString();
        StringBuilder SQL = new StringBuilder();

        SQL.AppendFormat(@" INSERT INTO [TKRESEARCH].[dbo].[TBDEVMEMOHISTORY]");
        SQL.AppendFormat(@" ([ID],[PID],[MEMODATES],[PROD],[MEMO])");
        SQL.AppendFormat(@" VALUES (@ID,@PID,@MEMODATES,@PROD,@MEMO)");
        SQL.AppendFormat(@" ");

        using (SqlConnection cnn = new SqlConnection(connectionString))
        {
            using (SqlCommand cmd = new SqlCommand(SQL.ToString(), cnn))
            {
                cmd.Parameters.AddWithValue("@ID", Guid.NewGuid());
                cmd.Parameters.AddWithValue("@PID", PID);
                cmd.Parameters.AddWithValue("@MEMODATES", Convert.ToDateTime(DateTime.Now));
                cmd.Parameters.AddWithValue("@PROD", PROD);
                cmd.Parameters.AddWithValue("@MEMO", MEMO);



                cnn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }

    public void UPDATETBDEVMEMO(string PID, string MEMO)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ERPconnectionstring"].ToString();
        StringBuilder SQL = new StringBuilder();

        SQL.AppendFormat(@" UPDATE [TKRESEARCH].[dbo].[TBDEVMEMO]");
        SQL.AppendFormat(@" SET [MEMO]=@MEMO");
        SQL.AppendFormat(@" WHERE [ID]=@ID");
        SQL.AppendFormat(@" ");

        using (SqlConnection cnn = new SqlConnection(connectionString))
        {
            using (SqlCommand cmd = new SqlCommand(SQL.ToString(), cnn))
            {
                cmd.Parameters.AddWithValue("@ID", PID);
                cmd.Parameters.AddWithValue("@MEMO", MEMO);

                cnn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }


    #endregion


}
