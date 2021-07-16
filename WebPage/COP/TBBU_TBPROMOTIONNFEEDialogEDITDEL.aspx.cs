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
                        SELECT  
                        [YEARS],[SALES],[NAMES],[KINDS],[PROMOTIONS],[PROMOTIONSSETS],[SDATES],[CLIENTS],[STORES],[SALESNUMS],[SALESMONEYS],[PROFITS],[COMMENTS]
                        FROM [TKBUSINESS].[dbo].[TBPROMOTIONNFEE]
                        WHERE [ID]=@ID
                        ";
        m_db.AddParameter("@ID", ID);

        DataTable dt = new DataTable();

        dt.Load(m_db.ExecuteReader(cmdTxt));

        if (dt.Rows.Count > 0)
        {           
            TextBox1.Text = dt.Rows[0]["YEARS"].ToString();
            TextBox2.Text = dt.Rows[0]["SALES"].ToString();
            TextBox3.Text = dt.Rows[0]["NAMES"].ToString();
            TextBox4.Text = dt.Rows[0]["KINDS"].ToString();
            TextBox5.Text = dt.Rows[0]["PROMOTIONS"].ToString();
            TextBox6.Text = dt.Rows[0]["PROMOTIONSSETS"].ToString();
            TextBox7.Text = dt.Rows[0]["SDATES"].ToString();
            TextBox8.Text = dt.Rows[0]["CLIENTS"].ToString();
            TextBox9.Text = dt.Rows[0]["STORES"].ToString();
            TextBox10.Text = dt.Rows[0]["SALESNUMS"].ToString();
            TextBox11.Text = dt.Rows[0]["SALESMONEYS"].ToString();
            TextBox12.Text = dt.Rows[0]["PROFITS"].ToString();
            TextBox13.Text = dt.Rows[0]["COMMENTS"].ToString();




        }




    }

    public void UPDATE()
    {
        string ID = lblParam.Text;

        string YEARS = TextBox1.Text;
        string SALES = TextBox2.Text;
        string NAMES = TextBox3.Text;
        string KINDS = TextBox4.Text;
        string PROMOTIONS = TextBox5.Text;
        string PROMOTIONSSETS = TextBox6.Text;
        string SDATES = TextBox7.Text;
        string CLIENTS = TextBox8.Text;
        string STORES = TextBox9.Text;
        string SALESNUMS = TextBox10.Text;
        string SALESMONEYS = TextBox11.Text;
        string PROFITS = TextBox12.Text;
        string COMMENTS = TextBox13.Text;




        if (!string.IsNullOrEmpty(ID) )
        {
            UPDATETBPROMOTIONNFEE(ID, YEARS, SALES, NAMES, KINDS, PROMOTIONS, PROMOTIONSSETS, SDATES, CLIENTS, STORES, SALESNUMS, SALESMONEYS, PROFITS, COMMENTS);
        }

        Dialog.SetReturnValue2("NeedPostBack");
    }
    public void UPDATETBPROMOTIONNFEE(string ID, string YEARS, string SALES, string NAMES, string KINDS, string PROMOTIONS, string PROMOTIONSSETS, string SDATES, string CLIENTS, string STORES, string SALESNUMS, string SALESMONEYS, string PROFITS, string COMMENTS)
    {


        string connectionString = ConfigurationManager.ConnectionStrings["ERPconnectionstring"].ToString();
        Ede.Uof.Utility.Data.DatabaseHelper m_db = new Ede.Uof.Utility.Data.DatabaseHelper(connectionString);

        string cmdTxt = @"                         
                        UPDATE [TKBUSINESS].[dbo].[TBPROMOTIONNFEE]
                        SET 
                        [YEARS]=@YEARS
                        ,[SALES]=@SALES
                        ,[NAMES]=@NAMES
						,[KINDS]=@KINDS
                        ,[PROMOTIONS]=@PROMOTIONS
                        ,[PROMOTIONSSETS]=@PROMOTIONSSETS
                        ,[SDATES]=@SDATES
                        ,[CLIENTS]=@CLIENTS
                        ,[STORES]=@STORES
                        ,[SALESNUMS]=@SALESNUMS
                        ,[SALESMONEYS]=@SALESMONEYS
                        ,[PROFITS]=@PROFITS
                        ,[COMMENTS]=@COMMENTS

                        WHERE  [ID]= @ID
                   
                            ";


        m_db.AddParameter("@ID", ID);
        m_db.AddParameter("@YEARS", YEARS);
        m_db.AddParameter("@SALES", SALES);
        m_db.AddParameter("@NAMES", NAMES);
        m_db.AddParameter("@KINDS", KINDS);
        m_db.AddParameter("@PROMOTIONS", PROMOTIONS);
        m_db.AddParameter("@PROMOTIONSSETS", PROMOTIONSSETS);
        m_db.AddParameter("@SDATES", SDATES);
        m_db.AddParameter("@CLIENTS", CLIENTS);
        m_db.AddParameter("@STORES", STORES);
        m_db.AddParameter("@SALESNUMS", SALESNUMS);
        m_db.AddParameter("@SALESMONEYS", SALESMONEYS);
        m_db.AddParameter("@PROFITS", PROFITS);
        m_db.AddParameter("@COMMENTS", COMMENTS);


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
