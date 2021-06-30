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


public partial class CDS_WebPage_TBBU_TBBU_TBPROMOTIONNFEEDialogEDITDEL : Ede.Uof.Utility.Page.BasePage
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
                SEARCHTBPROMOTIONNFEE(lblParam.Text);
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
        SEARCHTBPROMOTIONNFEE(lblParam.Text);
    }
    protected void btn1_Click(object sender, EventArgs e)
    {
        DELTBPROMOTIONNFEE(lblParam.Text);
        SEARCHTBPROMOTIONNFEE(lblParam.Text);
    }


    #endregion

    #region FUNCTION


    public void SEARCHTBPROMOTIONNFEE(string ID)
    {

        string connectionString = ConfigurationManager.ConnectionStrings["ERPconnectionstring"].ToString();
        Ede.Uof.Utility.Data.DatabaseHelper m_db = new Ede.Uof.Utility.Data.DatabaseHelper(connectionString);

        string cmdTxt = @"   
                        SELECT  [ID]
                        ,[YEARS]
                        ,[NAMES]
                        FROM [TKBUSINESS].[dbo].[TBPROMOTIONNFEE]
                        WHERE [ID]=@ID
                        ";
        m_db.AddParameter("@ID", ID);

        DataTable dt = new DataTable();

        dt.Load(m_db.ExecuteReader(cmdTxt));

        if (dt.Rows.Count > 0)
        {           
            TextBox1.Text = dt.Rows[0]["YEARS"].ToString();
            TextBox2.Text = dt.Rows[0]["NAMES"].ToString();
           



        }




    }

    public void UPDATE()
    {
        string ID = lblParam.Text;
        string YEARS = TextBox1.Text;
        string NAMES = TextBox2.Text;
        


        if (!string.IsNullOrEmpty(ID) )
        {
            UPDATETBPROMOTIONNFEE(ID, YEARS, NAMES);
        }

        Dialog.SetReturnValue2("NeedPostBack");
    }
    public void UPDATETBPROMOTIONNFEE(string ID, string YEARS, string NAMES)
    {


        string connectionString = ConfigurationManager.ConnectionStrings["ERPconnectionstring"].ToString();
        Ede.Uof.Utility.Data.DatabaseHelper m_db = new Ede.Uof.Utility.Data.DatabaseHelper(connectionString);

        string cmdTxt = @"                         
                        UPDATE [TKBUSINESS].[dbo].[TBPROMOTIONNFEE]
                        SET [YEARS]=@YEARS
                        ,[NAMES]=@NAMES
                        WHERE  [ID]= @ID
                   
                            ";


        m_db.AddParameter("@ID", ID);
        m_db.AddParameter("@YEARS", YEARS);
        m_db.AddParameter("@NAMES", NAMES);
       



        m_db.ExecuteNonQuery(cmdTxt);



    }



    public void DELTBPROMOTIONNFEE(string ID)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ERPconnectionstring"].ToString();
        Ede.Uof.Utility.Data.DatabaseHelper m_db = new Ede.Uof.Utility.Data.DatabaseHelper(connectionString);

        string cmdTxt = @"  DELETE [TKBUSINESS].[dbo].[TBPROMOTIONNFEE] WHERE [ID]=@ID
                            ";

        m_db.AddParameter("@ID", ID);

        m_db.ExecuteNonQuery(cmdTxt);


        Dialog.SetReturnValue2("NeedPostBack");
        Dialog.Close(this);
    }

    
    #endregion


}
