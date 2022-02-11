using Ede.Uof.EIP.SystemInfo;
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
    string ACCOUNT = null;
    string NAME = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        
        ACCOUNT = Current.Account;
        NAME = Current.User.Name;

        ACCOUNTLabel.Text = ACCOUNT+ NAME;

        if (ACCOUNT.Equals("160115"))
	    {
            Button1.Enabled = true;
        }
        else
        {
            Button1.Enabled = false;
        }

        //不顯示子視窗的按鈕
        //((Master_DialogMasterPage)this.Master).Button1Text = string.Empty;
        //((Master_DialogMasterPage)this.Master).Button2Text = string.Empty;
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


        Dialog.Close(this);

    }


    void Button2OnClick()
    {
        //設定回傳值並關閉視窗
        //Dialog.SetReturnValue2(txtReturnValue.Text);


        SEARCHCOPTD(lblParam.Text);
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        //Button1.Text = "OK";
        ADDTBCOPTDCHECK(
                        TextBox1.Text,
                        TextBox2.Text,
                        TextBox3.Text,
                        TextBox4.Text,
                        TextBox5.Text,
                        TextBox6.Text,
                        TextBox7.Text,
                        TextBox8.Text,
                        TextBox9.Text,
                        TextBox10.Text,
                        TextBox11.Text,
                        DropDownList1.SelectedValue,
                        TextBox13.Text,
                        TextBox14.Text,
                        DropDownList2.SelectedValue,
                        TextBox16.Text,
                        TextBox17.Text
                        );
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
                        ,(SELECT TOP 1 [SALESCHECKSCOMMENTS] FROM [TKBUSINESS].[dbo].[TBCOPTDCHECK] WHERE [TBCOPTDCHECK].TD001=COPTD.TD001 AND [TBCOPTDCHECK].TD002=COPTD.TD002 AND [TBCOPTDCHECK].TD003=COPTD.TD003  ORDER BY ID DESC) AS 'SALESCHECKSCOMMENTS'

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
            //TextBox12.Text = dt.Rows[0]["MOCCHECKS"].ToString();
            DropDownList1.SelectedValue= dt.Rows[0]["MOCCHECKS"].ToString();
            TextBox13.Text = dt.Rows[0]["MOCCHECKSCOMMENTS"].ToString();
            //TextBox14.Text = dt.Rows[0]["PURCHECKDATES"].ToString();
            TextBox14.Text = DateTime.Now.ToString("yyyyMMdd HH:mm:ss");
            //TextBox15.Text = dt.Rows[0]["PURCHECKS"].ToString();
            DropDownList2.SelectedValue = dt.Rows[0]["PURCHECKS"].ToString();
            TextBox16.Text = dt.Rows[0]["PURCHECKSCOMMENTS"].ToString();
            TextBox17.Text = dt.Rows[0]["SALESCHECKSCOMMENTS"].ToString();
        }




    }

   



    public void ADDTBCOPTDCHECK(string TD001,
                                string TD002,
                                string TD003,
                                string TD004,
                                string TD005,
                                string TD008,
                                string TD010,
                                string TD011,
                                string TD012,
                                string TD013,
                                string MOCCHECKDATES,
                                string MOCCHECKS,
                                string MOCCHECKSCOMMENTS,
                                string PURCHECKDATES,
                                string PURCHECKS,
                                string PURCHECKSCOMMENTS,
                                string SALESCHECKSCOMMENTS)
    {
        string ADDTD001 = TD001;
        string ADDTD002 = TD002;
        string ADDTD003 = TD003;
        string ADDTD004 = TD004;
        string ADDTD005 = TD005;
        string ADDTD008 = TD008;
        string ADDTD010 = TD010;
        string ADDTD011 = TD011;
        string ADDTD012 = TD012;
        string ADDTD013 = TD013;
        string ADDMOCCHECKDATES = MOCCHECKDATES;
        string ADDMOCCHECKS = MOCCHECKS;
        string ADDMOCCHECKSCOMMENTS = MOCCHECKSCOMMENTS;
        string ADDPURCHECKDATES = PURCHECKDATES;
        string ADDPURCHECKS = PURCHECKS;
        string ADDPURCHECKSCOMMENTS = PURCHECKSCOMMENTS;
        string ADDSALESCHECKSCOMMENTS = SALESCHECKSCOMMENTS;

        string connectionString = ConfigurationManager.ConnectionStrings["ERPconnectionstring"].ToString();
        Ede.Uof.Utility.Data.DatabaseHelper m_db = new Ede.Uof.Utility.Data.DatabaseHelper(connectionString);

        string cmdTxt = @"  
                        INSERT INTO [TKBUSINESS].[dbo].[TBCOPTDCHECK]
                        ([TD001]
                        ,[TD002]
                        ,[TD003]
                        ,[TD004]
                        ,[TD005]
                        ,[TD008]
                        ,[TD010]
                        ,[TD011]
                        ,[TD012]
                        ,[TD013]
                        ,[MOCCHECKDATES]
                        ,[MOCCHECKS]
                        ,[MOCCHECKSCOMMENTS]
                        ,[PURCHECKDATES]
                        ,[PURCHECKS]
                        ,[PURCHECKSCOMMENTS]
                        ,[SALESCHECKSCOMMENTS]
             
                        )
                        VALUES
                        (@TD001
                        ,@TD002
                        ,@TD003
                        ,@TD004
                        ,@TD005
                        ,@TD008
                        ,@TD010
                        ,@TD011
                        ,@TD012
                        ,@TD013
                        ,@MOCCHECKDATES
                        ,@MOCCHECKS
                        ,@MOCCHECKSCOMMENTS
                        ,@PURCHECKDATES
                        ,@PURCHECKS
                        ,@PURCHECKSCOMMENTS
                        ,@SALESCHECKSCOMMENTS
                        )
                   
                            ";


        m_db.AddParameter("@TD001", TD001);
        m_db.AddParameter("@TD002", TD002);
        m_db.AddParameter("@TD003", TD003);
        m_db.AddParameter("@TD004", TD004);
        m_db.AddParameter("@TD005", TD005);
        m_db.AddParameter("@TD008", Convert.ToDecimal(TD008));
        m_db.AddParameter("@TD010", TD010);
        m_db.AddParameter("@TD011", Convert.ToDecimal(TD011));
        m_db.AddParameter("@TD012", Convert.ToDecimal(TD012));
        m_db.AddParameter("@TD013", TD013);
        m_db.AddParameter("@MOCCHECKDATES", MOCCHECKDATES);
        m_db.AddParameter("@MOCCHECKS", MOCCHECKS);
        m_db.AddParameter("@MOCCHECKSCOMMENTS", MOCCHECKSCOMMENTS);
        m_db.AddParameter("@PURCHECKDATES", PURCHECKDATES);
        m_db.AddParameter("@PURCHECKS", PURCHECKS);
        m_db.AddParameter("@PURCHECKSCOMMENTS", PURCHECKSCOMMENTS);
        m_db.AddParameter("@SALESCHECKSCOMMENTS", SALESCHECKSCOMMENTS);


        m_db.ExecuteNonQuery(cmdTxt);

    }


    #endregion




}
