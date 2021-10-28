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


public partial class CDS_WebPage_TKRESEARCHTBSAMPLEDialogEDITDEL : Ede.Uof.Utility.Page.BasePage
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
            BindDropDownList();
            //接收主頁面傳遞之參數
            lblParam.Text = Request["ID"];


            if (!string.IsNullOrEmpty(lblParam.Text))
            {
                SEARCHTKRESEARCHTBSAMPLE(lblParam.Text);
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

        SEARCHTKRESEARCHTBSAMPLE(lblParam.Text);
    }
    protected void btn1_Click(object sender, EventArgs e)
    {
        DELTKRESEARCHTBSAMPLE(lblParam.Text);
    }


   

    #endregion

    #region FUNCTION
    private void BindDropDownList()
    {
        //DataTable dt = new DataTable();
        //dt.Columns.Add("SALESFOCUS", typeof(String));


        //string connectionString = ConfigurationManager.ConnectionStrings["ERPconnectionstring"].ToString();
        //Ede.Uof.Utility.Data.DatabaseHelper m_db = new Ede.Uof.Utility.Data.DatabaseHelper(connectionString);

        //string cmdTxt = @"SELECT  [ID],[STORES] FROM [TKBUSINESS].[dbo].[STORESKIND] ORDER BY ID";

        //dt.Load(m_db.ExecuteReader(cmdTxt));

        //if (dt.Rows.Count > 0)
        //{
        //    DropDownList1.DataSource = dt;
        //    DropDownList1.DataTextField = "STORES";
        //    DropDownList1.DataValueField = "STORES";
        //    DropDownList1.DataBind();

        //}
        //else
        //{

        //}



    }

    public void SEARCHTKRESEARCHTBSAMPLE(string ID)
    {

        string connectionString = ConfigurationManager.ConnectionStrings["ERPconnectionstring"].ToString();
        Ede.Uof.Utility.Data.DatabaseHelper m_db = new Ede.Uof.Utility.Data.DatabaseHelper(connectionString);

        string cmdTxt = @"   
                       SELECT 
                        [ID]
                        ,[FORMID]
                        ,[DV01]
                        ,[DVV01]
                        ,[ISCLOSE]
                        FROM [TKRESEARCH].[dbo].[TBSAMPLE]
                        WHERE [ID]=@ID
                        ";

        m_db.AddParameter("@ID", ID);

        DataTable dt = new DataTable();

        dt.Load(m_db.ExecuteReader(cmdTxt));

        if (dt.Rows.Count > 0)
        {           
            TextBox1.Text = dt.Rows[0]["ID"].ToString();
            TextBox2.Text = dt.Rows[0]["FORMID"].ToString();
            TextBox3.Text = dt.Rows[0]["DV01"].ToString();
            TextBox4.Text = dt.Rows[0]["DVV01"].ToString();
            TextBox5.Text = dt.Rows[0]["ISCLOSE"].ToString();


        }




    }

    public void UPDATE()
    {
        string ID = lblParam.Text;
        string FORMID = TextBox2.Text.Trim();
        string DV01 = TextBox3.Text.Trim();
        string DVV01 = TextBox4.Text.Trim();
        string ISCLOSE = TextBox5.Text.Trim();


        if (!string.IsNullOrEmpty(ID) )
        {
            UPDATETBPROJECTS(ID, FORMID, DV01, DVV01, ISCLOSE);
        }

        Dialog.SetReturnValue2("NeedPostBack");
    }
    public void UPDATETBPROJECTS(string ID, string FORMID, string DV01, string DVV01, string  ISCLOSE)
    {


        string connectionString = ConfigurationManager.ConnectionStrings["ERPconnectionstring"].ToString();
        Ede.Uof.Utility.Data.DatabaseHelper m_db = new Ede.Uof.Utility.Data.DatabaseHelper(connectionString);

        string cmdTxt = @"  
                            UPDATE [TKRESEARCH].[dbo].[TBSAMPLE] 
                            SET 
                            [FORMID]=@FORMID
                            ,[DV01]=@DV01
                            ,[DVV01]=@DVV01
                            ,[ISCLOSE]=@ISCLOSE
                            WHERE [ID]=@ID
                   
                            ";


        m_db.AddParameter("@ID", ID);
        m_db.AddParameter("@FORMID", FORMID);
        m_db.AddParameter("@DV01", DV01);
        m_db.AddParameter("@DVV01", DVV01);
        m_db.AddParameter("@ISCLOSE", ISCLOSE);
   




        m_db.ExecuteNonQuery(cmdTxt);



    }



    public void DELTKRESEARCHTBSAMPLE(string ID)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ERPconnectionstring"].ToString();
        Ede.Uof.Utility.Data.DatabaseHelper m_db = new Ede.Uof.Utility.Data.DatabaseHelper(connectionString);

        string cmdTxt = @"
                        DELETE [TKRESEARCH].[dbo].[TBSAMPLE]  WHERE [ID]=@ID
                            ";

        m_db.AddParameter("@ID", ID);

        m_db.ExecuteNonQuery(cmdTxt);


        Dialog.SetReturnValue2("NeedPostBack");
        Dialog.Close(this);
    }

   
    #endregion


}
