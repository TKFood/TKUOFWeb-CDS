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

public partial class CDS_WebPage_PUR_TK_UOF_PUR_INVMB_DELIVERY : Ede.Uof.Utility.Page.BasePage
{
    string ACCOUNT = null;
    string NAME = null;
    String ROLES = null;

    DataTable EXCELDT1 = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        ACCOUNT = Current.Account;
        NAME = Current.User.Name;

        if (!IsPostBack)
        {

        }
    }

    #region FUNCTION
    private void BindGrid()
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ERPconnectionstring"].ToString();
        Ede.Uof.Utility.Data.DatabaseHelper m_db = new Ede.Uof.Utility.Data.DatabaseHelper(connectionString);

        StringBuilder cmdTxt = new StringBuilder();
        StringBuilder QUERYS = new StringBuilder();
        StringBuilder QUERYS2 = new StringBuilder();
        StringBuilder QUERYS3 = new StringBuilder();



        cmdTxt.AppendFormat(@"                              
                            SELECT 
                            [ID]
                            ,[KINDS]
                            ,[MB001]
                            ,[MB002]
                            ,[MB003]
                            ,[MOQ]
                            ,[UNITS]
                            ,[DELIVERYDATS]
                            FROM [TKPUR].[dbo].[UOF_PUR_INVMB_DELIVERY]
                            ORDER BY [KINDS],[ID]

                            ", QUERYS.ToString());




        //m_db.AddParameter("@SDATE", SDATE);
        //m_db.AddParameter("@EDATE", EDATE);

        DataTable dt = new DataTable();

        dt.Load(m_db.ExecuteReader(cmdTxt.ToString()));

        //匯出專用
        EXCELDT1 = dt;

        Grid1.DataSource = dt;
        Grid1.DataBind();
    }

    protected void grid_PageIndexChanging1(object sender, GridViewPageEventArgs e)
    {
        //Grid1.PageIndex = e.NewPageIndex;
        //BindGrid();
    }
    protected void Grid1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Button btn2 = (Button)e.Row.FindControl("Button2");
            if (btn2 != null)
            {
                string cellValue2 = btn2.CommandArgument;
                dynamic param2 = new { ID = cellValue2 }.ToExpando();
            }
        }
    }

    protected void Grid1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        // 過濾無關的 Command
        if (e.CommandName == "Page" || e.CommandName == "Sort") return;

        int rowIndex = Convert.ToInt32(e.CommandArgument);

        // 初始化變數
        string ID = "";
        string KINDS = "";
        string MB001 = "";
        string MB002 = "";
        string MB003 = "";
        string MOQ = "";
        string UNITS = "";
        string DELIVERYDATS = "";

        if (rowIndex >= 0 && rowIndex < Grid1.Rows.Count)
        {
            GridViewRow row = Grid1.Rows[rowIndex];

            // 使用 FindControl 並加入防呆檢查 (避免找不控制項導致 NullReferenceException)
            Label lbl編號 = (Label)row.FindControl("Label_ID");
            TextBox txt類別 = (TextBox)row.FindControl("txtNewField_GV1_類別");
            TextBox txt品號 = (TextBox)row.FindControl("txtNewField_GV1_品號");
            TextBox txt品名 = (TextBox)row.FindControl("txtNewField_GV1_品名");
            TextBox txt規格 = (TextBox)row.FindControl("txtNewField_GV1_規格");
            TextBox txt最低採購量 = (TextBox)row.FindControl("txtNewField_GV1_最低採購量");
            TextBox txt單位 = (TextBox)row.FindControl("txtNewField_GV1_單位");
            TextBox txt交期 = (TextBox)row.FindControl("txtNewField_GV1_交期");


            // 賦值(C# 5.0 相容寫法)
            ID = (lbl編號 != null) ? lbl編號.Text : "";
            KINDS = txt類別.Text.Trim();
            MB001 = txt品號.Text.Trim();
            MB002 = txt品名.Text.Trim();
            MB003 = txt規格.Text.Trim();
            MOQ = txt最低採購量.Text.Trim();
            UNITS = txt單位.Text.Trim();
            DELIVERYDATS = txt交期.Text.Trim();

            // --- 邏輯判斷區 (修正括號層級) ---

            if (e.CommandName == "Button2")
            {
                // 保持原始 ISCLOSED
                ADD_UOF_PUR_INVMB_DELIVERY(
                ID
                , KINDS
                , MB001
                , MB002
                , MB003
                , MOQ
                , UNITS
                , DELIVERYDATS
                );
                MsgBox(ID + " 儲存完成", this.Page, this);
            }

            // 最後重新繫結
            BindGrid();
        }
    }


    public void OnBeforeExport1(object sender, Ede.Uof.Utility.Component.BeforeExportEventArgs e)
    {
        SETEXCEL();

    }


    public void SETEXCEL()
    {

    }

    public void ADD_UOF_PUR_INVMB_DELIVERY(
        string ID
        , string KINDS
        , string MB001
        , string MB002
        , string MB003
        , string MOQ
        , string UNITS
        , string DELIVERYDATS
        )
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ERPconnectionstring"].ToString();

        string SQLCOMMAND = @" 
                            MERGE [TKPUR].[dbo].[UOF_PUR_INVMB_DELIVERY] AS TARGET
                            USING (VALUES (@ID, @KINDS, @MB001, @MB002, @MB003, @MOQ, @UNITS, @DELIVERYDATS)) 
                            AS SOURCE (ID, KINDS, MB001, MB002, MB003, MOQ, UNITS, DELIVERYDATS)
                            ON TARGET.ID = SOURCE.ID 

                            WHEN MATCHED THEN 
                                UPDATE SET 
                                    KINDS = SOURCE.KINDS,
                                    MB001 = SOURCE.MB001,
                                    MB002 = SOURCE.MB002,
                                    MB003 = SOURCE.MB003,
                                    MOQ = SOURCE.MOQ,
                                    UNITS = SOURCE.UNITS,
                                    DELIVERYDATS = SOURCE.DELIVERYDATS
             

                            WHEN NOT MATCHED THEN
                                INSERT ( KINDS, MB001, MB002, MB003, MOQ, UNITS, DELIVERYDATS)
                                VALUES  (SOURCE.KINDS, SOURCE.MB001, SOURCE.MB002, SOURCE.MB003, SOURCE.MOQ, SOURCE.UNITS, SOURCE.DELIVERYDATS);
                            ";


        try
        {
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(SQLCOMMAND, cnn))
                {
                    // 2. 修正參數綁定，確保每個參數對應正確的 SQL 變數名稱
                    cmd.Parameters.AddWithValue("@ID", (object)ID ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@KINDS", (object)KINDS ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@MB001", (object)MB001 ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@MB002", (object)MB002 ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@MB003", (object)MB003 ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@MOQ", (object)MOQ ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@UNITS", (object)UNITS ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@DELIVERYDATS", (object)DELIVERYDATS ?? DBNull.Value);

                    cnn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected >= 1)
                    {
                        MsgBox(ID + " 完成", this.Page, this);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // 建議至少記錄錯誤，方便除錯
            // Log(ex.Message); 
            throw;
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