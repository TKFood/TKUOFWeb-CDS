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


public partial class CDS_WebPage_TBBU_PRODUCTSDialogADD : Ede.Uof.Utility.Page.BasePage
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
        Guid ID = Guid.NewGuid();
        //string SERNO = "";
        string MB001 = TextBox1.Text;
        string PRODUCTSFEATURES = TextBox2.Text;
        string SALESFOCUS = TextBox3.Text;
        string COPYWRITINGS = TextBox4.Text;
   

        if ( !string.IsNullOrEmpty(MB001))
        {

            ADDPRODUCTS(MB001, PRODUCTSFEATURES, SALESFOCUS, COPYWRITINGS);
        }

        Dialog.SetReturnValue2("NeedPostBack");
        Dialog.Close(this);
    }
    public void ADDPRODUCTS(string MB001, string PRODUCTSFEATURES, string SALESFOCUS, string COPYWRITINGS)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ERPconnectionstring"].ToString();
        Ede.Uof.Utility.Data.DatabaseHelper m_db = new Ede.Uof.Utility.Data.DatabaseHelper(connectionString);

        string cmdTxt = @"  
                        INSERT INTO [TKBUSINESS].[dbo].[PRODUCTS]
                        ([MB001],[PRODUCTSFEATURES],[SALESFOCUS],[COPYWRITINGS])
                        VALUES
                        (@MB001,@PRODUCTSFEATURES,@SALESFOCUS,@COPYWRITINGS)         

                            ";



        m_db.AddParameter("@MB001", MB001);
        m_db.AddParameter("@PRODUCTSFEATURES", PRODUCTSFEATURES);
        m_db.AddParameter("@SALESFOCUS", SALESFOCUS);
        m_db.AddParameter("@COPYWRITINGS", COPYWRITINGS);



        m_db.ExecuteNonQuery(cmdTxt);

    }

   

    #endregion


}