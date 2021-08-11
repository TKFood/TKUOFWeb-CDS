﻿using Ede.Uof.Utility.Data;
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
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class CDS_WebPage_TBBU_TBSALESEVENTSFORSALESDialogADD : Ede.Uof.Utility.Page.BasePage
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
            BindDropDownList1();
            BindDropDownList2();
            BindDropDownList3();


            RadDatePicker1.SelectedDate = DateTime.Now;
            RadDatePicker2.SelectedDate = DateTime.Now;

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
        
        ADD_HJ_BM_DB_tb_NOTE(DropDownList2.Text);

        Dialog.Close(this);

    }


    void Button2OnClick()
    {
        //設定回傳值並關閉視窗
        //Dialog.SetReturnValue2(txtReturnValue.Text);

        ADD();
        
        ADD_HJ_BM_DB_tb_NOTE(DropDownList2.Text);


    }

    #endregion

    #region FUNCTION
    private void BindDropDownList1()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("NAME", typeof(String));


        string connectionString = ConfigurationManager.ConnectionStrings["ERPconnectionstring"].ToString();
        Ede.Uof.Utility.Data.DatabaseHelper m_db = new Ede.Uof.Utility.Data.DatabaseHelper(connectionString);

        string cmdTxt = @"SELECT  [ID],[NAME] FROM [TKBUSINESS].[dbo].[TBSALESNAME] WHERE NAME NOT IN ('全部') ORDER BY [ID]";

        dt.Load(m_db.ExecuteReader(cmdTxt));

        if (dt.Rows.Count > 0)
        {
            DropDownList1.DataSource = dt;
            DropDownList1.DataTextField = "NAME";
            DropDownList1.DataValueField = "NAME";
            DropDownList1.DataBind();

        }
        else
        {

        }



    }

    private void BindDropDownList2()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("KINDS", typeof(String));


        string connectionString = ConfigurationManager.ConnectionStrings["ERPconnectionstring"].ToString();
        Ede.Uof.Utility.Data.DatabaseHelper m_db = new Ede.Uof.Utility.Data.DatabaseHelper(connectionString);

        string cmdTxt = @"SELECT  [ID],[KINDS] FROM [TKBUSINESS].[dbo].[TBSALESKINDS] ORDER BY [ID]";

        dt.Load(m_db.ExecuteReader(cmdTxt));

        if (dt.Rows.Count > 0)
        {
            DropDownList2.DataSource = dt;
            DropDownList2.DataTextField = "KINDS";
            DropDownList2.DataValueField = "KINDS";
            DropDownList2.DataBind();

        }
        else
        {

        }



    }

    private void BindDropDownList3()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("ISCLOSES", typeof(String));


        string connectionString = ConfigurationManager.ConnectionStrings["ERPconnectionstring"].ToString();
        Ede.Uof.Utility.Data.DatabaseHelper m_db = new Ede.Uof.Utility.Data.DatabaseHelper(connectionString);

        string cmdTxt = @"SELECT [ID],[ISCLOSES] FROM [TKBUSINESS].[dbo].[TBSALESCLOSES] ORDER BY [ID]";

        dt.Load(m_db.ExecuteReader(cmdTxt));

        if (dt.Rows.Count > 0)
        {
            DropDownList3.DataSource = dt;
            DropDownList3.DataTextField = "ISCLOSES";
            DropDownList3.DataValueField = "ISCLOSES";
            DropDownList3.DataBind();

        }
        else
        {

        }



    }
    public void ADD()
    {

        string SALES = DropDownList1.Text;
        string KINDS = DropDownList2.Text;
        //string CLIENTS = TextBox1.Text;
        string CLIENTSNAME = RadAutoCompleteBox1.Text.Replace(";", "");
        string CLIENTS = CLIENTSNAME;
        string PROJECTS = TextBox2.Text;
        string EVENTS = TextBox3.Text;
        string SDAYS = null;
        if (!string.IsNullOrEmpty(RadDatePicker1.SelectedDate.Value.ToString("yyyy/MM/dd")))
        {
            SDAYS = RadDatePicker1.SelectedDate.Value.ToString("yyyy/MM/dd");
        }
        string EDAYS = null;
        if (!string.IsNullOrEmpty(RadDatePicker2.SelectedDate.Value.ToString("yyyy/MM/dd")))
        {
            EDAYS = RadDatePicker2.SelectedDate.Value.ToString("yyyy/MM/dd");
        }
   
        string COMMENTS = TextBox6.Text;
        string ISCLOSE = "N";
        //string ISCLOSE = TextBox7.Text;



        if ( !string.IsNullOrEmpty(SALES) && !string.IsNullOrEmpty(KINDS) && !string.IsNullOrEmpty(COMMENTS))
        {

            ADDTBSALESEVENTS(SALES, KINDS, PROJECTS, EVENTS, CLIENTS, SDAYS, EDAYS, COMMENTS, ISCLOSE);
        }

        Dialog.SetReturnValue2("NeedPostBack");
        Dialog.Close(this);
    }
    public void ADDTBSALESEVENTS(string SALES, string KINDS,string PROJECTS, string EVENTS,string CLIENTS, string SDAYS, string EDAYS, string COMMENTS, string ISCLOSE)
    {
        Label8.Text = "";

        string connectionString = ConfigurationManager.ConnectionStrings["ERPconnectionstring"].ToString();
        Ede.Uof.Utility.Data.DatabaseHelper m_db = new Ede.Uof.Utility.Data.DatabaseHelper(connectionString);

        try
        {
            string cmdTxt = @"  
                            INSERT INTO [TKBUSINESS].[dbo].[TBSALESEVENTS]
                            ([SALES],[KINDS],[CLIENTS],[PROJECTS],[EVENTS],[SDAYS],[EDAYS],[COMMENTS],[ISCLOSE])
                            VALUES
                            (@SALES,@KINDS,@CLIENTS,@PROJECTS,@EVENTS,@SDAYS,@EDAYS,@COMMENTS,@ISCLOSE)
                            ";



            m_db.AddParameter("@SALES", SALES);
            m_db.AddParameter("@KINDS", KINDS);
            m_db.AddParameter("@CLIENTS", CLIENTS);
            m_db.AddParameter("@PROJECTS", PROJECTS);
            m_db.AddParameter("@EVENTS", EVENTS);
            m_db.AddParameter("@SDAYS", SDAYS);
            m_db.AddParameter("@EDAYS", EDAYS);
            m_db.AddParameter("@COMMENTS", COMMENTS);
            m_db.AddParameter("@ISCLOSE", ISCLOSE);




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

    public void ADD_HJ_BM_DB_tb_NOTE(string KIND)
    {
        string NOTE_CONTENT = null;

        if (!string.IsNullOrEmpty(TextBox2.Text))
        {
            NOTE_CONTENT = NOTE_CONTENT+ TextBox2.Text + "<br>";
        }
        if (!string.IsNullOrEmpty(TextBox3.Text))
        {
            NOTE_CONTENT = NOTE_CONTENT+ TextBox3.Text + "<br>";
        }

        if(KIND.Equals("拜訪"))
        {
            if (!string.IsNullOrEmpty(RadDatePicker2.SelectedDate.Value.ToString("yyyy/MM/dd")))
            {
                NOTE_CONTENT = NOTE_CONTENT + "拜訪日:" + RadDatePicker2.SelectedDate.Value.ToString("yyyy/MM/dd") + "<br>";
            }
        }
        else
        {
            if (!string.IsNullOrEmpty(RadDatePicker2.SelectedDate.Value.ToString("yyyy/MM/dd")))
            {
                NOTE_CONTENT = NOTE_CONTENT + "結案日:" + RadDatePicker2.SelectedDate.Value.ToString("yyyy/MM/dd") + "<br>";
            }
        }
        
        if (!string.IsNullOrEmpty(TextBox6.Text))
        {
            NOTE_CONTENT = NOTE_CONTENT + TextBox6.Text + "";
        }

        string NOTE_ID=null;
        //string NOTE_CONTENT = TextBox2 .Text+ "<br>" + TextBox3.Text + "<br>" + "結案日 " + RadDatePicker2.SelectedDate.Value.ToString("yyyy/MM/dd") + "<br>" + TextBox6.Text;
        string NOTE_KIND = "1";
        string FILE_NAME = null;
        string NOTE_DATE = DateTime.Now.ToString("yyyy-MM-dd");
        string NOTE_TIME = DateTime.Now.ToString("HH:mm");
        string UPDATE_DATETIME = DateTime.Now.ToString("yyyy-MM-dd HH:mm:00");
        string CONTACT_ID = "0";
        string CLIENTSNAME = RadAutoCompleteBox1.Text.Replace(";", "");
        string COMPANY_ID = SEARCHCOMPANY_ID(CLIENTSNAME);
        string OPPORTUNITY_ID = "0";
        string SALES_STAGE = null;
        string CREATE_DATETIME = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        string CREATE_USER_ID = SEARCHCREATE_USER_ID(DropDownList1.Text);

        string connectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
        Ede.Uof.Utility.Data.DatabaseHelper m_db = new Ede.Uof.Utility.Data.DatabaseHelper(connectionString);

        if(!String.IsNullOrEmpty(COMPANY_ID) && !String.IsNullOrEmpty(CREATE_USER_ID))
        {
            try
            {
                string cmdTxt = @"  
                                INSERT INTO [HJ_BM_DB].[dbo].[tb_NOTE]
                                (                            
                                [NOTE_CONTENT]
                                ,[NOTE_KIND]
                               
                                ,[NOTE_DATE]
                                ,[NOTE_TIME]
                                ,[UPDATE_DATETIME]
                                ,[CONTACT_ID]
                                ,[COMPANY_ID]
                                ,[OPPORTUNITY_ID]
                               
                                ,[CREATE_DATETIME]
                                ,[CREATE_USER_ID]
                                )
                                VALUES
                                (                              
                                @NOTE_CONTENT
                                ,@NOTE_KIND
                               
                                ,@NOTE_DATE
                                ,@NOTE_TIME
                                ,@UPDATE_DATETIME
                                ,@CONTACT_ID
                                ,@COMPANY_ID
                                ,@OPPORTUNITY_ID
                               
                                ,@CREATE_DATETIME
                                ,@CREATE_USER_ID
                                )
                                 
                                ";



        
                m_db.AddParameter("@NOTE_CONTENT", NOTE_CONTENT);
                m_db.AddParameter("@NOTE_KIND", NOTE_KIND);
                //m_db.AddParameter("@FILE_NAME", FILE_NAME);
                m_db.AddParameter("@NOTE_DATE", NOTE_DATE);
                m_db.AddParameter("@NOTE_TIME", NOTE_TIME);
                m_db.AddParameter("@UPDATE_DATETIME", UPDATE_DATETIME);
                m_db.AddParameter("@CONTACT_ID", CONTACT_ID);
                m_db.AddParameter("@COMPANY_ID", COMPANY_ID);
                m_db.AddParameter("@OPPORTUNITY_ID", OPPORTUNITY_ID);
                //m_db.AddParameter("@SALES_STAGE", SALES_STAGE);
                m_db.AddParameter("@CREATE_DATETIME", CREATE_DATETIME);
                m_db.AddParameter("@CREATE_USER_ID", CREATE_USER_ID);





                m_db.ExecuteNonQuery(cmdTxt);


            }
            catch
            {

            }
            finally
            {

            }
        }
       

    }

    public string SEARCHCOMPANY_ID(string COMPANYNAME)
    {
        DataTable dt = new DataTable();     


        string connectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
        Ede.Uof.Utility.Data.DatabaseHelper m_db = new Ede.Uof.Utility.Data.DatabaseHelper(connectionString);

        StringBuilder cmdTxt = new StringBuilder();
        cmdTxt.AppendFormat(@"
                            SELECT  
                            [COMPANY_ID]
                            ,[COMPANY_NAME]
                            FROM [HJ_BM_DB].[dbo].[tb_COMPANY]
                            WHERE [COMPANY_NAME]=@COMPANY_NAME
                                ");

        m_db.AddParameter("@COMPANY_NAME", COMPANYNAME);

        dt.Load(m_db.ExecuteReader(cmdTxt.ToString()));

        if (dt.Rows.Count > 0)
        {
            return dt.Rows[0]["COMPANY_ID"].ToString();

        }
        else
        {
            return null;
        }
    }

    public string SEARCHCREATE_USER_ID(string USER_NAME)
    {
        DataTable dt = new DataTable();


        string connectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
        Ede.Uof.Utility.Data.DatabaseHelper m_db = new Ede.Uof.Utility.Data.DatabaseHelper(connectionString);

        StringBuilder cmdTxt = new StringBuilder();
        cmdTxt.AppendFormat(@"
                            SELECT 
                            [USER_ID]
                            ,[USER_NAME]
                            FROM [HJ_BM_DB].[dbo].[tb_USER]
                            WHERE [USER_NAME]=@USER_NAME
                                ");

        m_db.AddParameter("@USER_NAME", USER_NAME);

        dt.Load(m_db.ExecuteReader(cmdTxt.ToString()));

        if (dt.Rows.Count > 0)
        {
            return dt.Rows[0]["USER_ID"].ToString();

        }
        else
        {
            return null;
        }
    }

   

    [WebMethod]
    public static AutoCompleteBoxData GetCompanyNames(object context)
    {
        string searchString = ((Dictionary<string, object>)context)["Text"].ToString();

        List<AutoCompleteBoxItemData> result = new List<AutoCompleteBoxItemData>();
               
        DataTable data = SEARCHCOMPANYNAME(searchString);

        //string CLIENTS = RadAutoCompleteBox1.Text;

        foreach (DataRow row in data.Rows)
        {
            AutoCompleteBoxItemData childNode = new AutoCompleteBoxItemData();
            childNode.Text = row["COMPANY_NAME"].ToString();
            childNode.Value = row["COMPANY_NAME"].ToString();
            result.Add(childNode);
        }

        AutoCompleteBoxData res = new AutoCompleteBoxData();
        res.Items = result.ToArray();

        return res;

        //string searchString = ((Dictionary<string, object>)context)["Text"].ToString();
        //DataTable data = GetChildNodes(searchString);
        //List<AutoCompleteBoxItemData> result = new List<AutoCompleteBoxItemData>();

        //foreach (DataRow row in data.Rows)
        //{
        //    AutoCompleteBoxItemData childNode = new AutoCompleteBoxItemData();
        //    childNode.Text = row["ACCOUNT"].ToString();
        //    childNode.Value = row["ACCOUNT"].ToString();
        //    result.Add(childNode);
        //}

        //AutoCompleteBoxData res = new AutoCompleteBoxData();
        //res.Items = result.ToArray();

        //return res;
    }


    private static DataTable SEARCHCOMPANYNAME(string searchString)
    {
        DataTable dt = new DataTable();

      

        string connectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
        Ede.Uof.Utility.Data.DatabaseHelper m_db = new Ede.Uof.Utility.Data.DatabaseHelper(connectionString);

        StringBuilder cmdTxt = new StringBuilder();
        cmdTxt.AppendFormat(@"
                            SELECT  
                            [COMPANY_ID]
                            ,[COMPANY_NAME]
                            FROM [HJ_BM_DB].[dbo].[tb_COMPANY]
                            WHERE [COMPANY_NAME] LIKE @COMPANY_NAME+'%'
                            ORDER BY [COMPANY_NAME]
                                
                            ");

        m_db.AddParameter("@COMPANY_NAME", searchString);

        dt.Load(m_db.ExecuteReader(cmdTxt.ToString()));

        if (dt.Rows.Count > 0)
        {
            return dt;

        }
        else
        {
            return null;
        }
    }

    #endregion


}
