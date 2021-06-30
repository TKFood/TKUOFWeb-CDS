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


public partial class CDS_WebPage_TBPROMOTIONNFEEDETAILSDialog : Ede.Uof.Utility.Page.BasePage
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
                SEARCHTBSALESDEVMEMO(lblParam.Text);
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
            ADDTBPROMOTIONNFEEDETAILS(lblParam.Text, TextBox2.Text.Trim(), TextBox1.Text);
           
        }
        
        Dialog.SetReturnValue2("NeedPostBack");
        Dialog.Close(this);

    }


    void Button2OnClick()
    {
        if (!string.IsNullOrEmpty(lblParam.Text) && !string.IsNullOrEmpty(TextBox2.Text) && !string.IsNullOrEmpty(TextBox1.Text))
        {
            ADDTBPROMOTIONNFEEDETAILS(lblParam.Text, TextBox2.Text.Trim(), TextBox1.Text);
          
        }

        BindGrid(lblParam.Text);

        Dialog.SetReturnValue2("NeedPostBack");
    }


    #endregion


    #region FUNCTION



    private void BindGrid(string ID)
    {
        StringBuilder cmdTxt = new StringBuilder();

        string connectionString = ConfigurationManager.ConnectionStrings["ERPconnectionstring"].ToString();
        Ede.Uof.Utility.Data.DatabaseHelper m_db = new Ede.Uof.Utility.Data.DatabaseHelper(connectionString);

        cmdTxt.AppendFormat(@" 
                            SELECT  
                            [ID]
                            ,[MID]
                            ,[FEENAME]
                            ,[FEEMONEYS]
                            FROM [TKBUSINESS].[dbo].[TBPROMOTIONNFEEDETAILS]
                            WHERE [MID]=@MID
                            ");

        m_db.AddParameter("@MID", ID);

        DataTable dt = new DataTable();

        dt.Load(m_db.ExecuteReader(cmdTxt.ToString()));

        Grid1.DataSource = dt;
        Grid1.DataBind();




    }

    public void SEARCHTBSALESDEVMEMO(string ID)
    {
        StringBuilder cmdTxt = new StringBuilder();

        string connectionString = ConfigurationManager.ConnectionStrings["ERPconnectionstring"].ToString();
        Ede.Uof.Utility.Data.DatabaseHelper m_db = new Ede.Uof.Utility.Data.DatabaseHelper(connectionString);

        cmdTxt.AppendFormat(@" 
                            SELECT 
                            [ID]
                            ,[YEARS]
                            ,[NAMES]
                            FROM [TKBUSINESS].[dbo].[TBPROMOTIONNFEE]
                            WHERE [ID]=@ID
                            ");

        m_db.AddParameter("@ID", ID);

        DataTable dt = new DataTable();

        dt.Load(m_db.ExecuteReader(cmdTxt.ToString()));

        if (dt.Rows.Count > 0)
        {
            TextBox2.Text = dt.Rows[0]["NAMES"].ToString();
        }

        
    }

    public void ADDTBPROMOTIONNFEEDETAILS(string PID,string PROD, string MEMO)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ERPconnectionstring"].ToString();
        Ede.Uof.Utility.Data.DatabaseHelper m_db = new Ede.Uof.Utility.Data.DatabaseHelper(connectionString);

        string cmdTxt = @" 
                            ";

       
        m_db.AddParameter("@ID", Guid.NewGuid());
    

        m_db.ExecuteNonQuery(cmdTxt);

        
    }

    public void UPDATETBPROMOTIONNFEEDETAILS(string PID, string MEMO)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ERPconnectionstring"].ToString();
        Ede.Uof.Utility.Data.DatabaseHelper m_db = new Ede.Uof.Utility.Data.DatabaseHelper(connectionString);

        string cmdTxt = @"  

                            ";

        m_db.AddParameter("@ID", PID);
        m_db.AddParameter("@MEMO", MEMO);
        m_db.AddParameter("@MEMODATES", Convert.ToDateTime(DateTime.Now));

        m_db.ExecuteNonQuery(cmdTxt);

        
    }


    #endregion


}
