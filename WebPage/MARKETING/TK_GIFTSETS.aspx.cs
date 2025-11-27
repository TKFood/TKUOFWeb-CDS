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

public partial class CDS_WebPage_MARKETING_TK_GIFTSETS : Ede.Uof.Utility.Page.BasePage
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
            BindDropDownList1();

            BindGrid();
        }
    }

    #region FUNCTION
    private void BindDropDownList(DropDownList ddl, string sql, string textField, string valueField)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
        Ede.Uof.Utility.Data.DatabaseHelper m_db = new Ede.Uof.Utility.Data.DatabaseHelper(connectionString);

        DataTable dt = new DataTable();
        dt.Load(m_db.ExecuteReader(sql));

        ddl.DataSource = dt;
        ddl.DataTextField = textField;
        ddl.DataValueField = valueField;
        ddl.DataBind();
    }

    private void BindDropDownList1()
    {
        string sql = @"
       SELECT 
        [KINDS]
        ,[PARASNAMES]
        ,[DVALUES]
        FROM [TKMARKETING].[dbo].[TBZPARAS]
        WHERE [KINDS]='TKGIFTSETS'
        ORDER BY [DVALUES]
                ";

        BindDropDownList(DropDownList1, sql, "PARASNAMES", "PARASNAMES");
    }
    private void BindGrid()
    {
        // 1.取得連線字串
        // 請將 "YourConnectionStringName" 替換為 Web.config 中定義的連線名稱
        string connectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ConnectionString;
        Ede.Uof.Utility.Data.DatabaseHelper m_db = new Ede.Uof.Utility.Data.DatabaseHelper(connectionString);

        StringBuilder QUERY1 = new StringBuilder();
        StringBuilder QUERY2 = new StringBuilder();
        // 2. 定義 SQL 查詢字串  
        string ISCLOSED = DropDownList1.SelectedValue.ToString();
        if (!string.IsNullOrEmpty(ISCLOSED) && ISCLOSED.Equals("N"))
        {
            QUERY1.AppendFormat(@" AND  ISCLOSED='N' ");
        }
        else if(!string.IsNullOrEmpty(ISCLOSED) && ISCLOSED.Equals("Y"))
        {
            QUERY1.AppendFormat(@" AND  ISCLOSED='Y' ");
        }
        else
        {
            QUERY1.AppendFormat(@"");
        }
        string MB002 = TextBox1.Text.Trim();
        if (!string.IsNullOrEmpty(MB002) )
        {
            QUERY2.AppendFormat(@" AND MB002 LIKE '%{0}%' ", MB002);
        }       
        else
        {
            QUERY2.AppendFormat(@"");
        }

        StringBuilder cmdTxt = new StringBuilder();
        cmdTxt.AppendFormat(@"
                        SELECT  
                        [ID]
                        ,[MB001]
                        ,[MB002]
                        ,[MB003]
                        ,[PRICES]
                        ,[IPPRICES]
                        ,[DMPRICES]
                        ,[STORENUMS]
                        ,[ECOMMERCENUMS]
                        ,[TOURISHOPNUMS]
                        ,[INARMYNUMS]
                        ,[INOILNUMS]
                        ,[INSALENUMS]
                        ,[INPRNUMS]
                        ,[BOSSPRNUMS]
                        ,[STAFFNUMS]
                        ,[TOTALNUMS]
                        ,[PACKNUMS]
                        ,[PACKINDATES]
                        ,[PRODATES]
                        ,[ISCLOSED]
                        FROM [TKMARKETING].[dbo].[TKGIFTSETS]
                        WHERE 1=1
                        {0}
                        {1}
                        ORDER BY [MB001],[MB002]
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
        if (e.CommandName == "UPDATE")
        {
            // 取得 ID (主鍵) - 這個是正確的
            string recordId = e.CommandArgument.ToString();

            // 2. 📌 正確地取得按鈕所在的 GridViewRow 物件
            // e.CommandSource 是按鈕物件，它的 NamingContainer 就是 GridViewRow
            GridViewRow row = (GridViewRow)((Control)e.CommandSource).NamingContainer;

            // 3. 📌 使用 FindControl 取得 TextBox 和 DropDownList 控制項

            // 取得品名 (TextBox)
            TextBox txt_MB002 = (TextBox)row.FindControl("TextBox_品名");
            string MB002 = txt_MB002.Text;
            TextBox txt_MB003 = (TextBox)row.FindControl("TextBox_箱入數");
            string MB003 = txt_MB003.Text;
            TextBox txt_PRICES = (TextBox)row.FindControl("TextBox_售價");
            string PRICES = txt_PRICES.Text;
            TextBox txt_IPPRICES = (TextBox)row.FindControl("TextBox_IP價");
            string IPPRICES = txt_IPPRICES.Text;
            TextBox txt_DMPRICES = (TextBox)row.FindControl("TextBox_DM價");
            string DMPRICES = txt_DMPRICES.Text;
            TextBox txt_STORENUMS = (TextBox)row.FindControl("TextBox_門市");
            string STORENUMS = txt_STORENUMS.Text;
            TextBox txt_ECOMMERCENUMS = (TextBox)row.FindControl("TextBox_電商");
            string ECOMMERCENUMS = txt_ECOMMERCENUMS.Text;
            TextBox txt_TOURISHOPNUMS = (TextBox)row.FindControl("TextBox_觀光");
            string TOURISHOPNUMS = txt_TOURISHOPNUMS.Text;
            TextBox txt_INARMYNUMS = (TextBox)row.FindControl("TextBox_國內國軍");
            string INARMYNUMS = txt_INARMYNUMS.Text;
            TextBox txt_INOILNUMS = (TextBox)row.FindControl("TextBox_國內中油");
            string INOILNUMS = txt_INOILNUMS.Text;
            TextBox txtINSALENUMS = (TextBox)row.FindControl("TextBox_國內經銷");
            string INSALENUMS = txtINSALENUMS.Text;
            TextBox txt_INPRNUMS = (TextBox)row.FindControl("TextBox_業務公關");
            string INPRNUMS = txt_INPRNUMS.Text;
            TextBox txt_BOSSPRNUMS = (TextBox)row.FindControl("TextBox_總經理公關");
            string BOSSPRNUMS = txt_BOSSPRNUMS.Text;
            TextBox txt_STAFFNUMS = (TextBox)row.FindControl("TextBox_員購");
            string STAFFNUMS = txt_STAFFNUMS.Text;
            TextBox txt_TOTALNUMS = (TextBox)row.FindControl("TextBox_加總");
            string TOTALNUMS = txt_TOTALNUMS.Text;
            TextBox txt_PACKNUMS = (TextBox)row.FindControl("TextBox_預估包材下單總量");
            string PACKNUMS = txt_PACKNUMS.Text;
            TextBox txt_PACKINDATES = (TextBox)row.FindControl("TextBox_預估包材到廠日");
            string PACKINDATES = txt_PACKINDATES.Text;
            TextBox txt_PRODATES = (TextBox)row.FindControl("TextBox_預估成品完成日");
            string PRODATES = txt_PRODATES.Text;

            // 取得是否結案 (DropDownList)
            DropDownList ddlIsClosed = (DropDownList)row.FindControl("DropDownList_是否結案");
            string ISCLOSED = ddlIsClosed.SelectedValue;
            // 4. 取得其值
            if (MB002 != null )
            {
                // 5. 進行資料庫更新操作
                // 例如：
                // UpdateData(newPinMing, newIsClosed, primaryKey);

                UPDATE_TKGIFTSETS(
                    recordId                    
                    , MB002
                    , MB003
                    , PRICES
                    , IPPRICES
                    , DMPRICES
                    , STORENUMS
                    , ECOMMERCENUMS
                    , TOURISHOPNUMS
                    , INARMYNUMS
                    , INOILNUMS
                    , INSALENUMS
                    , INPRNUMS
                    , BOSSPRNUMS
                    , STAFFNUMS
                    , TOTALNUMS
                    , PACKNUMS
                    , PACKINDATES
                    , PRODATES
                    , ISCLOSED
                    );

                BindGrid();
            }
        }
    }
    // 雖然不應該被觸發，但定義它以避免 HttpCException
    protected void Grid1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        // 什麼都不做，因為您不打算使用內建更新功能

        // 如果 GridView 處於編輯模式，這兩行可以讓它退出編輯模式
        Grid1.EditIndex = -1;
        // Grid1.DataBind(); 
    }

    public void OnBeforeExport1(object sender, Ede.Uof.Utility.Component.BeforeExportEventArgs e)
    {
        //SETEXCEL();

    }

    private void UPDATE_TKGIFTSETS(
        string recordId
        ,string MB002
        ,string MB003
        ,string PRICES
        ,string IPPRICES
        ,string DMPRICES
        ,string STORENUMS
        ,string ECOMMERCENUMS
        ,string TOURISHOPNUMS
        ,string INARMYNUMS
        ,string INOILNUMS
        ,string INSALENUMS
        ,string INPRNUMS
        ,string BOSSPRNUMS
        ,string STAFFNUMS
        ,string TOTALNUMS
        ,string PACKNUMS
        ,string PACKINDATES
        ,string PRODATES
        ,string ISCLOSED
        )
    {
        string connectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ConnectionString;

        // 1. 📌 使用參數化查詢，避免 SQL Injection
        string sqlQuery = @"
                            UPDATE [TKMARKETING].[dbo].[TKGIFTSETS]
                            SET 
                            MB002 = @MB002
                            ,MB003=@MB003
                            ,PRICES=@PRICES
                            ,IPPRICES=@IPPRICES
                            ,DMPRICES=@DMPRICES
                            ,STORENUMS=@STORENUMS
                            ,ECOMMERCENUMS=@ECOMMERCENUMS
                            ,TOURISHOPNUMS=@TOURISHOPNUMS
                            ,INARMYNUMS=@INARMYNUMS
                            ,INOILNUMS=@INOILNUMS
                            ,INSALENUMS=@INSALENUMS
                            ,INPRNUMS=@INPRNUMS
                            ,BOSSPRNUMS=@BOSSPRNUMS
                            ,STAFFNUMS=@STAFFNUMS
                            ,TOTALNUMS=@TOTALNUMS
                            ,PACKNUMS=@PACKNUMS
                            ,PACKINDATES=@PACKINDATES
                            ,PRODATES=@PRODATES
                            ,ISCLOSED=@ISCLOSED
                            WHERE [ID] = @recordId

                            UPDATE  [TKMARKETING].[dbo].[TKGIFTSETS]
                            SET TOTALNUMS=STORENUMS+ECOMMERCENUMS+TOURISHOPNUMS+INARMYNUMS+INOILNUMS+INSALENUMS+INPRNUMS+BOSSPRNUMS+STAFFNUMS
                            ";

        // 2. 📌 包裹在 Try-Catch 區塊中，處理例外狀況
        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    // 3. 📌 加入參數，將值安全地傳遞給 SQL 查詢
                    command.Parameters.AddWithValue("@MB002", MB002);
                    command.Parameters.AddWithValue("@MB003", MB003);
                    command.Parameters.AddWithValue("@PRICES", PRICES);
                    command.Parameters.AddWithValue("@IPPRICES", IPPRICES);
                    command.Parameters.AddWithValue("@DMPRICES", DMPRICES);
                    command.Parameters.AddWithValue("@STORENUMS", STORENUMS);
                    command.Parameters.AddWithValue("@ECOMMERCENUMS", ECOMMERCENUMS);
                    command.Parameters.AddWithValue("@TOURISHOPNUMS", TOURISHOPNUMS);
                    command.Parameters.AddWithValue("@INARMYNUMS", INARMYNUMS);
                    command.Parameters.AddWithValue("@INOILNUMS", INOILNUMS);
                    command.Parameters.AddWithValue("@INSALENUMS", INSALENUMS);
                    command.Parameters.AddWithValue("@INPRNUMS", INPRNUMS);
                    command.Parameters.AddWithValue("@BOSSPRNUMS", BOSSPRNUMS);
                    command.Parameters.AddWithValue("@STAFFNUMS", STAFFNUMS);
                    command.Parameters.AddWithValue("@TOTALNUMS", TOTALNUMS);
                    command.Parameters.AddWithValue("@PACKNUMS", PACKNUMS);
                    command.Parameters.AddWithValue("@PACKINDATES", PACKINDATES);
                    command.Parameters.AddWithValue("@PRODATES", PRODATES);
                    command.Parameters.AddWithValue("@ISCLOSED", ISCLOSED);
                    command.Parameters.AddWithValue("@recordId", recordId);

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();

                    // 檢查是否有資料被更新
                    if (rowsAffected > 0)
                    {
                        MsgBox("更新完成 \r\n ", this.Page, this);
                    }
                    else
                    {
                        // 雖然執行成功，但沒有任何資料列被影響 (可能 ID 找不到)
                        MsgBox("更新完成，但沒有資料列被影響 (ID 可能不存在)。\r\n ", this.Page, this);
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


    #endregion
}