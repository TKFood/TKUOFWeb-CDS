﻿using Ede.Uof.Utility.Data;
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


public partial class CDS_WebPage_TBBU_TBPROMOTIONNFEEDialogADD : Ede.Uof.Utility.Page.BasePage
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
           
            ////接收主頁面傳遞之參數
            //lblParam.Text = Request["ID"];


            //if (!string.IsNullOrEmpty(lblParam.Text))
            //{
            //   // SEARCHTCOPCONDTIONS(lblParam.Text);
            //}

        }
        
    }




    #region BUTTON
    void CDS_WebPage_Dialog_Button1OnClick()
    {
        //設定回傳值並關閉視窗
        //Dialog.SetReturnValue2(txtReturnValue.Text);

        ADD();

        Dialog.Close(this);

    }


    void Button2OnClick()
    {
        //設定回傳值並關閉視窗
        //Dialog.SetReturnValue2(txtReturnValue.Text);

        ADD();

       
    }

    #endregion

    #region FUNCTION
   

    public void ADD()
    { 
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



        if ( !string.IsNullOrEmpty(YEARS)&& !string.IsNullOrEmpty(NAMES))
        {

            ADDTBPROMOTIONNFEE(YEARS, SALES, NAMES, KINDS, PROMOTIONS, PROMOTIONSSETS, SDATES, CLIENTS, STORES, SALESNUMS, SALESMONEYS, PROFITS, COMMENTS);
        }

        Dialog.SetReturnValue2("NeedPostBack");
        Dialog.Close(this);
    }
    public void ADDTBPROMOTIONNFEE(string YEARS, string SALES, string NAMES, string KINDS, string PROMOTIONS, string PROMOTIONSSETS, string SDATES, string CLIENTS, string STORES, string SALESNUMS, string SALESMONEYS, string PROFITS, string COMMENTS)
    {
        Label8.Text = "";

        string connectionString = ConfigurationManager.ConnectionStrings["ERPconnectionstring"].ToString();
        Ede.Uof.Utility.Data.DatabaseHelper m_db = new Ede.Uof.Utility.Data.DatabaseHelper(connectionString);

        try
        {
            string cmdTxt = @"  
                            INSERT INTO  [TKBUSINESS].[dbo].[TBPROMOTIONNFEE]
                            ([YEARS],[SALES],[NAMES],[KINDS],[PROMOTIONS],[PROMOTIONSSETS],[SDATES],[CLIENTS],[STORES],[SALESNUMS],[SALESMONEYS],[PROFITS],[COMMENTS])
                            VALUES
                            (@YEARS, @SALES, @NAMES, @KINDS, @PROMOTIONS, @PROMOTIONSSETS, @SDATES, @CLIENTS, @STORES, @SALESNUMS, @SALESMONEYS, @PROFITS, @COMMENTS)

                            ";



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

            Label8.Text = "成功";
        }
        catch
        {
            Label8.Text = "新增失敗";
        }
        finally
        {

        }

        

    }

   

    #endregion


}
