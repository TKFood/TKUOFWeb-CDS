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


public partial class CDS_WebPage_TBBU_PRODUCTSDialogEDITDEL : Ede.Uof.Utility.Page.BasePage
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
                SEARCHCOPTD(lblParam.Text);
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

        SEARCHCOPTD(lblParam.Text);
    }
  


    #endregion

    #region FUNCTION


    public void SEARCHCOPTD(string ID)
    {

        string connectionString = ConfigurationManager.ConnectionStrings["ERPconnectionstring"].ToString();
        Ede.Uof.Utility.Data.DatabaseHelper m_db = new Ede.Uof.Utility.Data.DatabaseHelper(connectionString);

        string cmdTxt = @"   
                        SELECT LTRIM(RTRIM(TD001))+LTRIM(RTRIM(TD002))+LTRIM(RTRIM(TD003)) AS 'TD123',*
                        ,(SELECT TOP 1 ISNULL([MOCCHECKDATES],'0') FROM [TKBUSINESS].[dbo].[TBCOPTDCHECK] WHERE [TBCOPTDCHECK].TD001=COPTD.TD001 AND [TBCOPTDCHECK].TD002=COPTD.TD002 AND [TBCOPTDCHECK].TD003=COPTD.TD003  ORDER BY ID DESC) AS 'MOCCHECKDATES'
                        ,(SELECT TOP 1 [MOCCHECKS] FROM [TKBUSINESS].[dbo].[TBCOPTDCHECK] WHERE [TBCOPTDCHECK].TD001=COPTD.TD001 AND [TBCOPTDCHECK].TD002=COPTD.TD002 AND [TBCOPTDCHECK].TD003=COPTD.TD003  ORDER BY ID DESC) AS 'MOCCHECKS'
                        ,(SELECT TOP 1 [MOCCHECKSCOMMENTS] FROM [TKBUSINESS].[dbo].[TBCOPTDCHECK] WHERE [TBCOPTDCHECK].TD001=COPTD.TD001 AND [TBCOPTDCHECK].TD002=COPTD.TD002 AND [TBCOPTDCHECK].TD003=COPTD.TD003  ORDER BY ID DESC) AS 'MOCCHECKSCOMMENTS'
                        ,(SELECT TOP 1 [PURCHECKDATES] FROM [TKBUSINESS].[dbo].[TBCOPTDCHECK] WHERE [TBCOPTDCHECK].TD001=COPTD.TD001 AND [TBCOPTDCHECK].TD002=COPTD.TD002 AND [TBCOPTDCHECK].TD003=COPTD.TD003  ORDER BY ID DESC) AS 'PURCHECKDATES'
                        ,(SELECT TOP 1 [PURCHECKS] FROM [TKBUSINESS].[dbo].[TBCOPTDCHECK] WHERE [TBCOPTDCHECK].TD001=COPTD.TD001 AND [TBCOPTDCHECK].TD002=COPTD.TD002 AND [TBCOPTDCHECK].TD003=COPTD.TD003  ORDER BY ID DESC) AS 'PURCHECKS'
                        ,(SELECT TOP 1 [PURCHECKSCOMMENTS] FROM [TKBUSINESS].[dbo].[TBCOPTDCHECK] WHERE [TBCOPTDCHECK].TD001=COPTD.TD001 AND [TBCOPTDCHECK].TD002=COPTD.TD002 AND [TBCOPTDCHECK].TD003=COPTD.TD003  ORDER BY ID DESC) AS 'PURCHECKSCOMMENTS'
                        FROM [TK].dbo.COPTC,[TK].dbo.COPTD

                        WHERE TC001=TD001 AND TC002=TD002
                        AND  LTRIM(RTRIM(TD001))+LTRIM(RTRIM(TD002))+LTRIM(RTRIM(TD003))=@ID
                        ORDER BY TD001,TD002,TD003
                        ";
        m_db.AddParameter("@ID", ID);

        DataTable dt = new DataTable();

        dt.Load(m_db.ExecuteReader(cmdTxt));

        if (dt.Rows.Count > 0)
        {           
            TextBox1.Text = dt.Rows[0]["TD001"].ToString();
            TextBox2.Text = dt.Rows[0]["TD002"].ToString();
            TextBox3.Text = dt.Rows[0]["TD003"].ToString();
            TextBox4.Text = dt.Rows[0]["TD004"].ToString();
            TextBox5.Text = dt.Rows[0]["TD005"].ToString();
            TextBox6.Text = Convert.ToDecimal(dt.Rows[0]["TD008"].ToString()).ToString("N0");
            TextBox7.Text = dt.Rows[0]["TD010"].ToString();
            TextBox8.Text = Convert.ToDecimal(dt.Rows[0]["TD011"].ToString()).ToString("N2");
            TextBox9.Text = Convert.ToDecimal(dt.Rows[0]["TD012"].ToString()).ToString("N0");
            TextBox10.Text = dt.Rows[0]["TD013"].ToString();
            //TextBox11.Text = dt.Rows[0]["MOCCHECKDATES"].ToString();
            TextBox11.Text = DateTime.Now.ToString("yyyyMMdd HH:mm:ss");
            TextBox12.Text = dt.Rows[0]["MOCCHECKS"].ToString();
            TextBox13.Text = dt.Rows[0]["MOCCHECKSCOMMENTS"].ToString();
            //TextBox14.Text = dt.Rows[0]["PURCHECKDATES"].ToString();
            TextBox14.Text = DateTime.Now.ToString("yyyyMMdd HH:mm:ss");
            TextBox15.Text = dt.Rows[0]["PURCHECKS"].ToString();
            TextBox16.Text = dt.Rows[0]["PURCHECKSCOMMENTS"].ToString();

        }




    }

    public void UPDATE()
    {
        string ID = lblParam.Text;

        if (!string.IsNullOrEmpty(ID) )
        {
            ADDTBCOPTDCHECK(ID);
        }

        Dialog.SetReturnValue2("NeedPostBack");
    }
    public void ADDTBCOPTDCHECK(string ID)
    {


        string connectionString = ConfigurationManager.ConnectionStrings["ERPconnectionstring"].ToString();
        Ede.Uof.Utility.Data.DatabaseHelper m_db = new Ede.Uof.Utility.Data.DatabaseHelper(connectionString);

        string cmdTxt = @"  
                        UPDATE [TKBUSINESS].[dbo].[PRODUCTS]
                        SET 
                        [PRODUCTSFEATURES]=@PRODUCTSFEATURES
                        ,[SALESFOCUS]=@SALESFOCUS
                        ,[COPYWRITINGS]=@COPYWRITINGS
                        ,[PRICES1]=@PRICES1
                        ,[PRICES2]=@PRICES2
                        ,[PRICES3]=@PRICES3
                        ,[MOQS]=@MOQS
                        WHERE [MB001]=@ID
                   
                            ";


        m_db.AddParameter("@ID", ID);
   



        m_db.ExecuteNonQuery(cmdTxt);



    }



  

    
    #endregion


}
