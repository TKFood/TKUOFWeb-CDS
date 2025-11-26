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
    }

    #region FUNCTION
    private void BindGrid()
    {
        // 1.取得連線字串
        // 請將 "YourConnectionStringName" 替換為 Web.config 中定義的連線名稱
        string connectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ConnectionString;
        Ede.Uof.Utility.Data.DatabaseHelper m_db = new Ede.Uof.Utility.Data.DatabaseHelper(connectionString);

        // 2. 定義 SQL 查詢字串           
        string cmdTxt = @"
                           /****** SSMS 中 SelectTopNRows 命令的指令碼  ******/
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
                        WHERE [ISCLOSED]='N'
                        ORDER BY [MB001],[MB002]
                        ";

        //m_db.AddParameter("@DATESTART", TextBox1.Text.Trim());
        //m_db.AddParameter("@DATESEND", TextBox2.Text.Trim());
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
            TextBox txtPinMing = (TextBox)row.FindControl("TextBox_品名");
            string txtPinMing_string = txtPinMing.Text;

            // 取得是否結案 (DropDownList)
            DropDownList ddlIsClosed = (DropDownList)row.FindControl("DropDownList_是否結案");

            // 4. 取得其值
            if (txtPinMing != null && ddlIsClosed != null)
            {
                string newPinMing = txtPinMing.Text;
                string newIsClosed = ddlIsClosed.SelectedValue;

                // 5. 進行資料庫更新操作
                // 例如：
                // UpdateData(newPinMing, newIsClosed, primaryKey);

                UPDATE_TKGIFTSETS(recordId, txtPinMing_string);
               
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

    private void UPDATE_TKGIFTSETS(string recordId,string MB002)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ConnectionString;

        // 1. 📌 使用參數化查詢，避免 SQL Injection
        string sqlQuery = @"
                            UPDATE [TKMARKETING].[dbo].[TKGIFTSETS]
                            SET [MB002] = @MB002
                            WHERE [ID] = @recordId
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