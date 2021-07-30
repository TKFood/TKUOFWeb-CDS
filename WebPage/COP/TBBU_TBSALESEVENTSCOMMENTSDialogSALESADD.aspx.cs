using Ede.Uof.Utility.Component;
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


public partial class CDS_WebPage_TBBU_TBSALESEVENTSCOMMENTSDialogSALESADD : Ede.Uof.Utility.Page.BasePage
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
            SEARCHTBSALESEVENTS(lblParam.Text);

            if (!string.IsNullOrEmpty(lblParam.Text))
            {                
                BindGrid(lblParam.Text);
                //SEARCHTBSALESDEVMEMO(lblParam.Text);
            }

        }
        
    }




    #region BUTTON
    void CDS_WebPage_Dialog_Button1OnClick()
    {
        //設定回傳值並關閉視窗
        //Dialog.SetReturnValue2(txtReturnValue.Text);
        if(!string.IsNullOrEmpty(lblParam.Text) && !string.IsNullOrEmpty(TextBox1.Text) )
        {
            ADDTBSALESEVENTSCOMMENTS(lblParam.Text, TextBox1.Text);
            UPDATETBSALESEVENTS(lblParam.Text, TextBox1.Text);
        }
        
        Dialog.SetReturnValue2("NeedPostBack");
        Dialog.Close(this);

    }


    void Button2OnClick()
    {
        if (!string.IsNullOrEmpty(lblParam.Text) && !string.IsNullOrEmpty(TextBox1.Text))
        {
            ADDTBSALESEVENTSCOMMENTS(lblParam.Text, TextBox1.Text);
            UPDATETBSALESEVENTS(lblParam.Text, TextBox1.Text);
        }

        BindGrid(lblParam.Text);

        Dialog.SetReturnValue2("NeedPostBack");
    }


    #endregion


    #region FUNCTION



    private void BindGrid(string ID)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ERPconnectionstring"].ToString();
        Ede.Uof.Utility.Data.DatabaseHelper m_db = new Ede.Uof.Utility.Data.DatabaseHelper(connectionString);

        string cmdTxt = @" 
                           SELECT 
                            [ID]
                            ,[MID]
                            ,CONVERT(NVARCHAR, [ADDDATES],120) AS ADDDATES 
                            ,[COMMENTS]
                            FROM [TKBUSINESS].[dbo].[TBSALESEVENTSCOMMENTS]
                            WHERE [MID]=@ID     
                            ORDER BY [ADDDATES] DESC
                            ";

        m_db.AddParameter("@ID", ID);

        DataTable dt = new DataTable();

        dt.Load(m_db.ExecuteReader(cmdTxt));

        Grid1.DataSource = dt;
        Grid1.DataBind();




    }

    public void SEARCHTBSALESEVENTS(string ID)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ERPconnectionstring"].ToString();
        Ede.Uof.Utility.Data.DatabaseHelper m_db = new Ede.Uof.Utility.Data.DatabaseHelper(connectionString);

        string cmdTxt = @" 
                        SELECT [ID]
                        ,[SALES]
                        ,[KINDS]
                        ,[EVENTS]
                        ,[SDAYS]
                        ,[EDAYS]
                        ,[COMMENTS]
                        ,[ISCLOSE]
                        FROM [TKBUSINESS].[dbo].[TBSALESEVENTS]
                        WHERE [ID]=@ID    
                        ";

        m_db.AddParameter("@ID", ID);

        DataTable dt = new DataTable();

        dt.Load(m_db.ExecuteReader(cmdTxt));

        if (dt.Rows.Count > 0)
        {
            TextBox2.Text = dt.Rows[0]["EVENTS"].ToString();
        }


    }

    public void ADDTBSALESEVENTSCOMMENTS(string MID, string COMMENTS)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ERPconnectionstring"].ToString();
        Ede.Uof.Utility.Data.DatabaseHelper m_db = new Ede.Uof.Utility.Data.DatabaseHelper(connectionString);

        string cmdTxt = @"  
                        INSERT INTO  [TKBUSINESS].[dbo].[TBSALESEVENTSCOMMENTS]
                        ([MID],[ADDDATES],[COMMENTS])
                        VALUES
                        (@MID,@ADDDATES,@COMMENTS)
                            ";

       
        
        m_db.AddParameter("@MID", MID);
        m_db.AddParameter("@ADDDATES", Convert.ToDateTime(DateTime.Now));
        m_db.AddParameter("@COMMENTS", COMMENTS);

        m_db.ExecuteNonQuery(cmdTxt);

        
    }

    public void UPDATETBSALESEVENTS(string ID, string COMMENTS)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ERPconnectionstring"].ToString();
        Ede.Uof.Utility.Data.DatabaseHelper m_db = new Ede.Uof.Utility.Data.DatabaseHelper(connectionString);

        string cmdTxt = @"  UPDATE [TKBUSINESS].[dbo].[TBSALESEVENTS]
                            SET [COMMENTS]=@COMMENTS
                            WHERE [ID]=@ID
                            ";

        m_db.AddParameter("@ID", ID);
        m_db.AddParameter("@COMMENTS", COMMENTS);


        m_db.ExecuteNonQuery(cmdTxt);

        
    }


    #endregion


}
