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
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class CDS_WebPage_CUSTOMERIZE_TK_SCH_DEVOLVEDialogEDIT : Ede.Uof.Utility.Page.BasePage
{
    string ACCOUNT = null;
    string NAME = null;
    string ROLES = null;
    string EXECUTE_USER = null;

    protected void Page_Load(object sender, EventArgs e)
    {

        ACCOUNT = Current.Account;
        NAME = Current.User.Name;
        ROLES = SEARCHROLES(ACCOUNT.Trim());
        EXECUTE_USER = SEARCHTB_EB_USER("iteng");
        NAMES.Text = NAME;


        //不顯示子視窗的按鈕
        //確定
        //((Master_DialogMasterPage)this.Master).Button1Text = string.Empty;
        //確定後繼續
        ((Master_DialogMasterPage)this.Master).Button2Text = string.Empty;
        //關閉
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
            lblParam.Text = "f62e152b-c2f6-46af-9b96-267214391665";

            BindDropDownList();

            if (!string.IsNullOrEmpty(lblParam.Text)&& !string.IsNullOrEmpty(EXECUTE_USER))
            {
                SEARCH_TB_EIP_SCH_WORK(lblParam.Text, EXECUTE_USER);
            }

        }

    }

    private void BindDropDownList()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("CODE", typeof(String));
        dt.Columns.Add("NAMES", typeof(String));

        string connectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
        Ede.Uof.Utility.Data.DatabaseHelper m_db = new Ede.Uof.Utility.Data.DatabaseHelper(connectionString);
        StringBuilder cmdTxt = new StringBuilder();
        cmdTxt.AppendFormat(@"SELECT  
                                ID
                                ,KINDS
                                ,CODE
                                ,NAMES
                                FROM [UOF].[dbo].[Z_SET_PARA]
                                WHERE [KINDS]='交辨'
                                ORDER BY [ID]
                                ");

        dt.Load(m_db.ExecuteReader(cmdTxt.ToString()));

        if (dt.Rows.Count > 0)
        {
            DropDownList1.DataSource = dt;
            DropDownList1.DataTextField = "NAMES";
            DropDownList1.DataValueField = "NAMES";
            DropDownList1.DataBind();

        }
        else
        {

        }
    }


    #region BUTTON
    void CDS_WebPage_Dialog_Button1OnClick()
    {
        //UPDATEITWEEKSREPORTS(lblParam.Text.ToString(), TextBox1.Text.ToString(), TextBox2.Text.ToString(), TextBox3.Text.ToString());

        //設定回傳值並關閉視窗
        //Dialog.SetReturnValue2(txtReturnValue.Text);

        Dialog.SetReturnValue2("OK");
       
        Dialog.Close(this);

    }


    void Button2OnClick()
    {
        //UPDATEITWEEKSREPORTS(lblParam.Text.ToString(), TextBox1.Text.ToString(), TextBox2.Text.ToString(), TextBox3.Text.ToString());
        //設定回傳值並關閉視窗
        Dialog.SetReturnValue2("OK");

        Dialog.Close(this);
        //SEARCHCOPTD(lblParam.Text);
    }


    #endregion

    #region FUNCTION


    public string SEARCHROLES(string ACCOUNT)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ERPconnectionstring"].ToString();
        Ede.Uof.Utility.Data.DatabaseHelper m_db = new Ede.Uof.Utility.Data.DatabaseHelper(connectionString);

        StringBuilder cmdTxt = new StringBuilder();

        cmdTxt.AppendFormat(@" 
                            SELECT  
                            [ID]
                            ,[ROLES]
                            ,[MV001]
                            ,[MV002]
                            FROM [TKBUSINESS].[dbo].[TBCOPTDCHECKROLES]
                            WHERE MV001 ='{0}'

                              ", ACCOUNT);




        //m_db.AddParameter("@SDATE", SDATE);
        //m_db.AddParameter("@EDATE", EDATE);

        DataTable dt = new DataTable();

        dt.Load(m_db.ExecuteReader(cmdTxt.ToString()));

        if (dt.Rows.Count > 0)
        {
            return dt.Rows[0]["ROLES"].ToString().Trim();
        }
        else
        {
            return "NOROLES";
        }

    }

    public string SEARCHTB_EB_USER(string ACCOUNT)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
        Ede.Uof.Utility.Data.DatabaseHelper m_db = new Ede.Uof.Utility.Data.DatabaseHelper(connectionString);

        StringBuilder cmdTxt = new StringBuilder();

        cmdTxt.AppendFormat(@" 
                            SELECT *
                            FROM [UOF].dbo.TB_EB_USER
                            WHERE ACCOUNT=@ACCOUNT

                              ", ACCOUNT);




        m_db.AddParameter("@ACCOUNT", ACCOUNT);
     
        DataTable dt = new DataTable();

        dt.Load(m_db.ExecuteReader(cmdTxt.ToString()));

        if (dt.Rows.Count > 0)
        {
            return dt.Rows[0]["USER_GUID"].ToString().Trim();
        }
        else
        {
            return null;
        }

    }
    public void SEARCH_TB_EIP_SCH_WORK(string DEVOLVE_GUID, string EXECUTE_USER)
    {
       
        string connectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
        Ede.Uof.Utility.Data.DatabaseHelper m_db = new Ede.Uof.Utility.Data.DatabaseHelper(connectionString);

        string cmdTxt = @"   
                        SELECT *
                        ,(ISNULL(TB_EIP_SCH_WORK.PROCEEDING_DESC,'')+ISNULL(TB_EIP_SCH_WORK.COMPLETE_DESC,''))  AS '交辨回覆'
                        FROM [UOF].dbo.TB_EIP_SCH_WORK
                        WHERE DEVOLVE_GUID=@DEVOLVE_GUID
                        AND EXECUTE_USER=@EXECUTE_USER
                        ORDER BY CREATE_TIME DESC
                        ";
        m_db.AddParameter("@DEVOLVE_GUID", DEVOLVE_GUID);
        m_db.AddParameter("@EXECUTE_USER", EXECUTE_USER);

        DataTable dt = new DataTable();

        dt.Load(m_db.ExecuteReader(cmdTxt));

        if (dt.Rows.Count > 0)
        {           
            TextBox1.Text = dt.Rows[0]["交辨回覆"].ToString();

            TextBox2.Text = dt.Rows[0]["SUBJECT"].ToString();
            
        }




    }

   



    public void UPDATEITWEEKSREPORTS(string ID, string COMMENTS,string NOTFINISHEDS, string PLANWORKS)
    {        

        string connectionString = ConfigurationManager.ConnectionStrings["ERPconnectionstring"].ToString();
        Ede.Uof.Utility.Data.DatabaseHelper m_db = new Ede.Uof.Utility.Data.DatabaseHelper(connectionString);

        string cmdTxt = @"  
                       
                        UPDATE [TKIT].[dbo].[ITWEEKSREPORTS]
                        SET [COMMENTS]=@COMMENTS,[NOTFINISHEDS]=@NOTFINISHEDS,[PLANWORKS]=@PLANWORKS
                        WHERE [ID]=@ID  
                        ";


        m_db.AddParameter("@ID", ID);
        m_db.AddParameter("@COMMENTS", COMMENTS);
        m_db.AddParameter("@NOTFINISHEDS", NOTFINISHEDS);
        m_db.AddParameter("@PLANWORKS", PLANWORKS);


        m_db.ExecuteNonQuery(cmdTxt);

    }


    #endregion




}
