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

public partial class CDS_WebPage_PUR_TB_PUR_WORKS : Ede.Uof.Utility.Page.BasePage
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

        // 2. 定義 SQL 查詢字串           
        string cmdTxt = @"
                        SELECT 
                        [ID]
                        ,[OWNER] AS '負責承辦'
                        ,[WORKOBJECTS] AS '工作對象'
                        ,[PROCESS] AS '處理情況敘述'
                        ,[FINISHDATES] AS '預計完成日期'
                        ,[STATUS] AS '處理進度'
                        ,[COMMENTS] AS '備註說明'
                        FROM [TKPUR].[dbo].[TB_PUR_WORKS]
                        ORDER BY [ID]
                        ";

        //m_db.AddParameter("@DATESTART", TextBox1.Text.Trim());


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
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // 1. 尋找控制項
            DropDownList ddlProgress = (DropDownList)e.Row.FindControl("DropDownList_處理進度");
            HiddenField hfValue = (HiddenField)e.Row.FindControl("Hidden_處理進度");

            if (ddlProgress != null)
            {
                // 2. 取得 SQL 資料 (建議將此 SQL 邏輯抽出成一個 DataTable 以免重複查詢多次)
                DataTable dtOptions = GetProgressOptions();

                ddlProgress.DataSource = dtOptions;
                ddlProgress.DataTextField = "PARAID";  // 顯示的文字 (請依 SQL 欄位修改)
                ddlProgress.DataValueField = "PARAID"; // 實際的值 (請依 SQL 欄位修改)
                ddlProgress.DataBind();

                // 3. 加入預設空白選項
                ddlProgress.Items.Insert(0, new ListItem("請選擇", ""));

                // 4. 設定選取值 (對應資料庫原本的值)
                if (hfValue != null && !string.IsNullOrEmpty(hfValue.Value))
                {
                    ddlProgress.SelectedValue = hfValue.Value;
                }
            }
        }
    }

    protected void Grid1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int rowIndex = -1;
    }


    public void OnBeforeExport1(object sender, Ede.Uof.Utility.Component.BeforeExportEventArgs e)
    {
        //SETEXCEL();

    }

    // 取得下拉選單資料的專用方法
    private DataTable GetProgressOptions()
    {
        // 使用您現有的 DatabaseHelper
        string connectionString = ConfigurationManager.ConnectionStrings["ERPconnectionstring"].ConnectionString;
        Ede.Uof.Utility.Data.DatabaseHelper m_db = new Ede.Uof.Utility.Data.DatabaseHelper(connectionString);

        // 這裡替換成您實際要查詢進度清單的 SQL
        string cmdTxt = @"SELECT  [ID],[KIND],[PARAID],[PARANAME] FROM [TKPUR].[dbo].[TBPARA] WHERE [KIND]='TB_PUR_WORKS' ORDER BY [PARANAME]";

        DataTable dt = new DataTable();
        dt.Load(m_db.ExecuteReader(cmdTxt));
        return dt;
    }

    public void MsgBox(String ex, Page pg, Object obj)
    {
        string script = "alert('" + ex.Replace("\r\n", "\\n").Replace("'", "") + "');";
        ScriptManager.RegisterStartupScript(pg, obj.GetType(), "AlertScript", script, true);

        // MsgBox("MsgBox!!!!    " + error + "\r\n" + Form.OuterXml, this.Page, this);
    }
    #endregion


    #region BUTTON
    protected void Button1_Click(object sender, EventArgs e)
    {
        BindGrid();
      
    }

    #endregion
}