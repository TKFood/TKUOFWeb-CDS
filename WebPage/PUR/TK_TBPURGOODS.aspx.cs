using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Linq;
using Ede.Uof.EIP.SystemInfo;
using Ede.Uof.Utility.Data;
using Ede.Uof.Utility.Log;
using Ede.Uof.Utility.Page.Common;
using OfficeOpenXml;
using OfficeOpenXml.Drawing;
using OfficeOpenXml.Style;
using System.Net.Mail;
using System.Threading.Tasks;

public partial class CDS_WebPage_PUR_TK_TBPURGOODS : Ede.Uof.Utility.Page.BasePage
{
    string ACCOUNT = null;
    string NAME = null;
    String ROLES = null;


    protected void Page_Load(object sender, EventArgs e)
    {
        ACCOUNT = Current.Account;
        NAME = Current.User.Name;

        if (!IsPostBack)
        {
            BindGrid();
        }
    }

    #region FUNCTION
    private void BindGrid()
    {
        // 1.取得連線字串
        // 請將 "YourConnectionStringName" 替換為 Web.config 中定義的連線名稱
        string connectionString = ConfigurationManager.ConnectionStrings["ERPconnectionstring"].ConnectionString;
        Ede.Uof.Utility.Data.DatabaseHelper m_db = new Ede.Uof.Utility.Data.DatabaseHelper(connectionString);

        StringBuilder QUERY1 = new StringBuilder();
        StringBuilder QUERY2 = new StringBuilder();
        StringBuilder QUERY3 = new StringBuilder();

        //MB002
        string COMPANYS = QUERY_TextBox1.Text.Trim();
        if (!string.IsNullOrEmpty(COMPANYS))
        {
            QUERY1.AppendFormat(@" AND COMPANYS LIKE '%{0}%' ", COMPANYS);
        }
        else
        {
            QUERY1.AppendFormat(@"");
        }
        //YEARS
        string ITEMS = QUERY_TextBox2.Text.Trim();
        if (!string.IsNullOrEmpty(ITEMS))
        {
            QUERY2.AppendFormat(@" AND ITEMS LIKE '%{0}%' ", ITEMS);
        }
        else
        {
            QUERY2.AppendFormat(@"");
        }

        StringBuilder cmdTxt = new StringBuilder();
        cmdTxt.AppendFormat(@"
                            SELECT
                            [ID]
                            ,[COMPANYS] AS '廠商'
                            ,[ITEMS] AS '品項'
                            ,[NUMS] AS '數量'
                            ,[PRICES] AS '單價'
                            ,[MONEYS] AS '總計'
                            ,[UPDATEDATES] AS '提供日期'
                            ,[COMMENTS] AS '備註'
                            ,[USEDSTATES] AS '月叫貨量'
                            FROM [TKPUR].[dbo].[TBPURGOODS]
                            WHERE 1=1
                            {0}
                            {1}
                            ORDER BY [COMPANYS],[ITEMS]
                            ", QUERY1.ToString(), QUERY2.ToString());


        //m_db.AddParameter("@QUERYMONEY", TextBox3.Text.Trim());

        DataTable dt = new DataTable();

        dt.Load(m_db.ExecuteReader(cmdTxt.ToString()));


        Grid1.DataSource = dt;
        Grid1.DataBind();

    }

    protected void grid_PageIndexChanging1(object sender, GridViewPageEventArgs e)
    {

    }
    protected void Grid1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    protected void Grid1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int rowIndex = -1;

        // 1. 檢查 CommandName 是否是您定義的更新命令
        if (e.CommandName == "MYUPDATE")
        {
            string ID = e.CommandArgument.ToString();
            GridViewRow row = (GridViewRow)((Control)e.CommandSource).NamingContainer;

            TextBox txt_COMPANYS = (TextBox)row.FindControl("TextBox_廠商");
            string COMPANYS = txt_COMPANYS.Text;
            TextBox txt_ITEMS = (TextBox)row.FindControl("TextBox_品項");
            string ITEMS = txt_ITEMS.Text;
            TextBox txt_NUMS = (TextBox)row.FindControl("TextBox_數量");
            string NUMS = txt_NUMS.Text;
            TextBox txt_PRICES = (TextBox)row.FindControl("TextBox_單價");
            string PRICES = txt_PRICES.Text;
            TextBox txt_MONEYS = (TextBox)row.FindControl("TextBox_總計");
            string MONEYS = txt_MONEYS.Text;
            TextBox txt_UPDATEDATES = (TextBox)row.FindControl("TextBox_提供日期");
            string UPDATEDATES = txt_UPDATEDATES.Text;
            TextBox txt_COMMENTS = (TextBox)row.FindControl("TextBox_備註");
            string COMMENTS = txt_COMMENTS.Text;
            TextBox txt_USEDSTATES = (TextBox)row.FindControl("TextBox_月叫貨量");
            string USEDSTATES = txt_USEDSTATES.Text;

            UODATE_TBPURGOODS(
             ID
            , COMPANYS
            , ITEMS
            , NUMS
            , PRICES
            , MONEYS
            , UPDATEDATES
            , COMMENTS
            , USEDSTATES
            );

            MsgBox("更新完成 \r\n " + ID, this.Page, this);
        }
        else if (e.CommandName == "MYDELETE")
        {
            string ID = e.CommandArgument.ToString();
            GridViewRow row = (GridViewRow)((Control)e.CommandSource).NamingContainer;

            DELETE_TBPURGOODS(ID);

            MsgBox("刪除完成 \r\n " + ID, this.Page, this);
        }

        BindGrid();
    }
    // 雖然不應該被觸發，但定義它以避免 HttpCException
    protected void Grid1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        // 這裡可以不寫程式碼
    }

    protected void Grid1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        // 這裡可以不寫程式碼，因為你的邏輯寫在 RowCommand 了
    }

   
    public void OnBeforeExport1(object sender, Ede.Uof.Utility.Component.BeforeExportEventArgs e)
    {
        //SETEXCEL();

    }

    public void UODATE_TBPURGOODS(
         string ID
        , string COMPANYS
        , string ITEMS
        , string NUMS
        , string PRICES
        , string MONEYS
        , string UPDATEDATES
        , string COMMENTS
        , string USEDSTATES
        )
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ERPconnectionstring"].ConnectionString;

        // 1. 📌 使用參數化查詢，避免 SQL Injection
        string sqlQuery = @"
                            UPDATE [TKPUR].[dbo].[TBPURGOODS]
                            SET 
                            COMPANYS=@COMPANYS
                            ,ITEMS=@ITEMS
                            ,NUMS=@NUMS
                            ,PRICES=@PRICES
                            ,MONEYS=@MONEYS
                            ,UPDATEDATES=@UPDATEDATES
                            ,COMMENTS=@COMMENTS
                            ,USEDSTATES=@USEDSTATES
                            WHERE ID=@ID
                            ";

        // 2. 📌 包裹在 Try-Catch 區塊中，處理例外狀況
        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    // 3. 📌 加入參數，將值安全地傳遞給 SQL 查詢
                    command.Parameters.AddWithValue("@ID", ID);
                    command.Parameters.AddWithValue("@COMPANYS", COMPANYS);
                    command.Parameters.AddWithValue("@ITEMS", ITEMS);
                    command.Parameters.AddWithValue("@NUMS", NUMS);
                    command.Parameters.AddWithValue("@PRICES", PRICES);
                    command.Parameters.AddWithValue("@MONEYS", MONEYS);
                    command.Parameters.AddWithValue("@UPDATEDATES", UPDATEDATES);
                    command.Parameters.AddWithValue("@COMMENTS", COMMENTS);
                    command.Parameters.AddWithValue("@USEDSTATES", USEDSTATES);

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();

                    // 檢查是否有資料被更新
                    if (rowsAffected > 0)
                    {
                        MsgBox("完成 \r\n ", this.Page, this);
                    }
                    else
                    {
                        // 雖然執行成功，但沒有任何資料列被影響 (可能 ID 找不到)                       
                    }
                }
            }
        }
        catch (Exception ex)
        {
        }
    }

    public void ADD_TBPURGOODS
        (
        string COMPANYS
        , string ITEMS
        , string NUMS
        , string PRICES
        , string MONEYS
        , string UPDATEDATES
        , string COMMENTS
        , string USEDSTATES
        )
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ERPconnectionstring"].ConnectionString;

        // 1. 📌 使用參數化查詢，避免 SQL Injection
        string sqlQuery = @"
                            INSERT INTO  [TKPUR].[dbo].[TBPURGOODS]
                            (
                            [COMPANYS]
                            ,[ITEMS]
                            ,[NUMS]
                            ,[PRICES]
                            ,[MONEYS]
                            ,[UPDATEDATES]
                            ,[COMMENTS]
                            ,[USEDSTATES])
                            VALUES
                            (
                            @COMPANYS
                            ,@ITEMS
                            ,@NUMS
                            ,@PRICES
                            ,@MONEYS
                            ,@UPDATEDATES
                            ,@COMMENTS
                            ,@USEDSTATES
                            )
                            ";

        // 2. 📌 包裹在 Try-Catch 區塊中，處理例外狀況
        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    // 3. 📌 加入參數，將值安全地傳遞給 SQL 查詢           
                    command.Parameters.AddWithValue("@COMPANYS", COMPANYS);
                    command.Parameters.AddWithValue("@ITEMS", ITEMS);
                    command.Parameters.AddWithValue("@NUMS", NUMS);
                    command.Parameters.AddWithValue("@PRICES", PRICES);
                    command.Parameters.AddWithValue("@MONEYS", MONEYS);
                    command.Parameters.AddWithValue("@UPDATEDATES", UPDATEDATES);
                    command.Parameters.AddWithValue("@COMMENTS", COMMENTS);
                    command.Parameters.AddWithValue("@USEDSTATES", USEDSTATES);

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();

                    // 檢查是否有資料被更新
                    if (rowsAffected > 0)
                    {
                        MsgBox("完成 \r\n ", this.Page, this);
                    }
                    else
                    {
                        // 雖然執行成功，但沒有任何資料列被影響 (可能 ID 找不到)                       
                    }
                }
            }
        }
        catch (Exception ex)
        {
        }
    }

    public void DELETE_TBPURGOODS
       (
       string ID     
       )
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ERPconnectionstring"].ConnectionString;

        // 1. 📌 使用參數化查詢，避免 SQL Injection
        string sqlQuery = @"
                            DELETE [TKPUR].[dbo].[TBPURGOODS]
                            WHERE  [ID]= @ID
                            ";

        // 2. 📌 包裹在 Try-Catch 區塊中，處理例外狀況
        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    // 3. 📌 加入參數，將值安全地傳遞給 SQL 查詢           
                    command.Parameters.AddWithValue("@ID", ID);
                  

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();

                    // 檢查是否有資料被更新
                    if (rowsAffected > 0)
                    {
                        MsgBox("完成 \r\n ", this.Page, this);
                    }
                    else
                    {
                        // 雖然執行成功，但沒有任何資料列被影響 (可能 ID 找不到)                       
                    }
                }
            }
        }
        catch (Exception ex)
        {
        }
    }
    public void MsgBox(String ex, Page pg, Object obj)
    {
        string script = "alert('" + ex.Replace("\r\n", "\\n").Replace("'", "") + "');";
        ScriptManager.RegisterStartupScript(pg, obj.GetType(), "AlertScript", script, true);

        //string s = "<SCRIPT language='javascript'>alert('" + ex.Replace("\r\n", "\\n").Replace("'", "") + "'); </SCRIPT>";
        //Type cstype = obj.GetType();
        //ClientScriptManager cs = pg.ClientScript;
        //cs.RegisterClientScriptBlock(cstype, s, s.ToString());
    }
    #endregion


    #region BUTTON
    protected void Button1_Click(object sender, EventArgs e)
    {
        BindGrid();
    }

    protected void btnADD_Click(object sender, EventArgs e)
    {
        string COMPANYS = ADD_TextBox1.Text.Trim().ToString();
        string ITEMS = ADD_TextBox2.Text.Trim().ToString();
        string NUMS = ADD_TextBox3.Text.Trim().ToString();
        string PRICES = ADD_TextBox4.Text.Trim().ToString();
        string MONEYS = ADD_TextBox5.Text.Trim().ToString();
        string UPDATEDATES = ADD_TextBox6.Text.Trim().ToString();
        string COMMENTS = ADD_TextBox7.Text.Trim().ToString();
        string USEDSTATES = ADD_TextBox8.Text.Trim().ToString();

        ADD_TBPURGOODS
         (
         COMPANYS
         , ITEMS
         , NUMS
         , PRICES
         , MONEYS
         , UPDATEDATES
         , COMMENTS
         , USEDSTATES
         );

        BindGrid();
    }

    #endregion
}