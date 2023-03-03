﻿using Ede.Uof.Utility.Component;
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
using System.Drawing;
using System.Dynamic;
using System.IO;
using System.Net;
using OfficeOpenXml;
using OfficeOpenXml.Drawing;
using OfficeOpenXml.Style;
using System.Globalization;


public partial class CDS_WebPage_TKRESEARCH_COSTDialogROWS : Ede.Uof.Utility.Page.BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {

        //不顯示子視窗的按鈕
        //((Master_DialogMasterPage)this.Master).Button1Text = string.Empty;
        ((Master_DialogMasterPage)this.Master).Button2Text = string.Empty;
        //((Master_DialogMasterPage)this.Master).Button3Text = string.Empty;


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
            BindDropDownList();

            if (!string.IsNullOrEmpty(lblParam.Text))
            {
                BindGrid1(lblParam.Text);
            }

        }
        
    }




    #region BUTTON
    void CDS_WebPage_Dialog_Button1OnClick()
    {
        //設定回傳值並關閉視窗
        //Dialog.SetReturnValue2(txtReturnValue.Text);

        UPDATE(lblParam.Text);

        Dialog.Close(this);

    }


    void Button2OnClick()
    {
        //設定回傳值並關閉視窗
        //Dialog.SetReturnValue2(txtReturnValue.Text);

        UPDATE(lblParam.Text);

        SEARCH_TBCOSTRECORDS(lblParam.Text);
    }

    #endregion

    #region FUNCTION
    private void BindDropDownList()
    {
        //DataTable dt = new DataTable();
        //dt.Columns.Add("PARAID", typeof(String));
        //dt.Columns.Add("PARANAME", typeof(String));

        //string connectionString = ConfigurationManager.ConnectionStrings["ERPconnectionstring"].ToString();
        //Ede.Uof.Utility.Data.DatabaseHelper m_db = new Ede.Uof.Utility.Data.DatabaseHelper(connectionString);

        //string cmdTxt = @" SELECT  [ID],[KIND],[PARAID],[PARANAME] FROM [TKRESEARCH].[dbo].[TBPARA] WHERE [KIND]='人工記錄成本' ORDER BY  [ID]  ";

        //dt.Load(m_db.ExecuteReader(cmdTxt));

        //if (dt.Rows.Count > 0)
        //{
        //    DropDownList1.DataSource = dt;
        //    DropDownList1.DataTextField = "PARANAME";
        //    DropDownList1.DataValueField = "PARANAME";
        //    DropDownList1.DataBind();

        //}
        //else
        //{

        //}
    }

    private void BindGrid1(string MMB001)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ERPconnectionstring"].ToString();
        Ede.Uof.Utility.Data.DatabaseHelper m_db = new Ede.Uof.Utility.Data.DatabaseHelper(connectionString);

        StringBuilder cmdTxt = new StringBuilder();
        StringBuilder SQUERY = new StringBuilder();



        if (!string.IsNullOrEmpty(MMB001))
        {
            cmdTxt.AppendFormat(@"
                              SELECT
                                [MMB001]
                                ,[MB001]
                                ,[MB002]
                                ,[MB003]
                                ,[COSTROW]
                                FROM [TKRESEARCH].[dbo].[TBCOSTRECORDSROWS]
                                WHERE [MMB001]={0}
                                ORDER BY MB001
 

                                    ", MMB001);

            //m_db.AddParameter("@SDATE", SDATE);
            //m_db.AddParameter("@EDATE", EDATE);

            DataTable dt = new DataTable();

            dt.Load(m_db.ExecuteReader(cmdTxt.ToString()));

            Grid1.DataSource = dt;
            Grid1.DataBind();

        }
        else
        {

        }



    }




    protected void grid1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //Grid1.PageIndex = e.NewPageIndex;
        //BindGrid();
    }
    protected void Grid1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    string KINDS = e.Row.Cells[0].Text.ToString();
        //    if (KINDS.Equals("小計"))
        //    {
        //        e.Row.BackColor = System.Drawing.Color.LightPink;
        //    }
        //}

    }
    protected void Grid1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int rowIndex = -1;

        //if (e.CommandName == "GWButton1")
        //{

        //    BindGrid1("");

        //}


    }

    public void SEARCH_TBCOSTRECORDS(string ID)
    {

        string connectionString = ConfigurationManager.ConnectionStrings["ERPconnectionstring"].ToString();
        Ede.Uof.Utility.Data.DatabaseHelper m_db = new Ede.Uof.Utility.Data.DatabaseHelper(connectionString);

        string cmdTxt = @" 
                           
                      
                        ";


        m_db.AddParameter("@MB001", ID);

        DataTable dt = new DataTable();

        dt.Load(m_db.ExecuteReader(cmdTxt));


    }

    public void UPDATE(string ID)
    {
        //string SERNO = lblParam.Text;
        //string MB001 = TextBox1.Text;
        //string MB002 = TextBox2.Text;
        //string MB003 = TextBox3.Text;
        //string COSTROW = TextBox4.Text;
        //string COSTHR = TextBox5.Text;
        //string COSTMANU = TextBox6.Text;
        //string COSTPRO = TextBox7.Text;
        //string COMMEMTS = TextBox9.Text;
        //string ISCLOSED = DropDownList1.Text;
        //string COSTMAT = TextBox10.Text;

        //if (!string.IsNullOrEmpty(MB001))
        //{
        //    UPDATE_TBCOSTRECORDS(
        //                MB001, MB002, MB003, COSTROW, COSTMAT,COSTHR, COSTMANU, COSTPRO, COMMEMTS, ISCLOSED
        //                );
        //}

        //Dialog.SetReturnValue2("REFRESH");
    }
    public void UPDATE_TBCOSTRECORDS(
                            string MB001, string MB002, string MB003, string COSTROW,string COSTMAT, string COSTHR, string COSTMANU, string COSTPRO, string COMMEMTS, string ISCLOSED


                                )
    {

        


        string connectionString = ConfigurationManager.ConnectionStrings["ERPconnectionstring"].ToString();
        Ede.Uof.Utility.Data.DatabaseHelper m_db = new Ede.Uof.Utility.Data.DatabaseHelper(connectionString);

        string cmdTxt = @"  
                       UPDATE [TKRESEARCH].[dbo].[TBCOSTRECORDS]
                        SET 
                        MB002=@MB002
                       ,MB003=@MB003
                     ,COSTROW=@COSTROW
                     ,COSTMAT=@COSTMAT
                     ,COSTHR=@COSTHR
                     ,COSTMANU=@COSTMANU
                     ,COSTPRO=@COSTPRO
                     ,COMMEMTS=@COMMEMTS
                     ,ISCLOSED=@ISCLOSED
              
                       
                        WHERE[MB001]=@MB001
                            ";

        m_db.AddParameter("@MB001", MB001);
        m_db.AddParameter("@MB002", MB002);
        m_db.AddParameter("@MB003", MB003);
        m_db.AddParameter("@COSTROW", COSTROW);
        m_db.AddParameter("@COSTMAT", COSTMAT);
        m_db.AddParameter("@COSTHR", COSTHR);
        m_db.AddParameter("@COSTMANU", COSTMANU);
        m_db.AddParameter("@COSTPRO", COSTPRO);
        m_db.AddParameter("@COMMEMTS", COMMEMTS);
        m_db.AddParameter("@ISCLOSED", ISCLOSED);


        m_db.ExecuteNonQuery(cmdTxt);


    }

    protected void btn1_Click(object sender, EventArgs e)
    {
        DELTBDEVNEW(lblParam.Text);
    }

    public void DELTBDEVNEW(string ID)
    {
        //string connectionString = ConfigurationManager.ConnectionStrings["ERPconnectionstring"].ToString();
        //Ede.Uof.Utility.Data.DatabaseHelper m_db = new Ede.Uof.Utility.Data.DatabaseHelper(connectionString);

        //string cmdTxt = @"  DELETE [TKRESEARCH].[dbo].[TBDEVNEW]  WHERE [SERNO]=@ID
        //                    ";

        //m_db.AddParameter("@ID", ID);

        //m_db.ExecuteNonQuery(cmdTxt);


        //Dialog.SetReturnValue2("NeedPostBack");
        //Dialog.Close(this);
    }

    
   
    #endregion


}
