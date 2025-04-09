﻿using System;
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

public partial class CDS_WebPage_COWORK_TB_PROJECTS_PRODUCTS : Ede.Uof.Utility.Page.BasePage
{
    string ACCOUNT = null;
    string NAME = null;
    String ROLES = null;

    DataTable EXCELDT1 = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Bind_DropDownList_ISCLOSED();
            Bind_DropDownList_OWNER();
        }
    }


    #region FUNCTION
    public void Bind_DropDownList_ISCLOSED()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("PARAID", typeof(String));
        dt.Columns.Add("PARANAME", typeof(String));

        string connectionString = ConfigurationManager.ConnectionStrings["ERPconnectionstring"].ToString();
        Ede.Uof.Utility.Data.DatabaseHelper m_db = new Ede.Uof.Utility.Data.DatabaseHelper(connectionString);

        string cmdTxt = @" 
                        SELECT
                        [ID]
                        ,[KIND]
                        ,[PARAID]
                        ,[PARANAME]
                        FROM [TKRESEARCH].[dbo].[TBPARA]
                        WHERE [KIND]='TB_PROJECTS_PRODUCTS_ISCLOSED'
                        ORDER BY [PARAID]
                        ";

        dt.Load(m_db.ExecuteReader(cmdTxt));

        if (dt.Rows.Count > 0)
        {
            DropDownList_ISCLOSED.DataSource = dt;
            DropDownList_ISCLOSED.DataTextField = "PARANAME";
            DropDownList_ISCLOSED.DataValueField = "PARANAME";
            DropDownList_ISCLOSED.DataBind();

        }
        else
        {

        }

    }

    public void Bind_DropDownList_OWNER()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("PARAID", typeof(String));
        dt.Columns.Add("PARANAME", typeof(String));

        string connectionString = ConfigurationManager.ConnectionStrings["ERPconnectionstring"].ToString();
        Ede.Uof.Utility.Data.DatabaseHelper m_db = new Ede.Uof.Utility.Data.DatabaseHelper(connectionString);

        string cmdTxt = @"                         
                            SELECT OWNER
                            FROM 
                            (
	                            SELECT '全部' AS 'OWNER'
	                            UNION ALL
	                            SELECT
	                            [OWNER]      
	                            FROM [TKRESEARCH].[dbo].[TB_PROJECTS_PRODUCTS]
	                            GROUP BY [OWNER]
                            ) AS TEMP
                            ORDER BY OWNER
                        ";

        dt.Load(m_db.ExecuteReader(cmdTxt));

        if (dt.Rows.Count > 0)
        {
            DropDownList_OWNER.DataSource = dt;
            DropDownList_OWNER.DataTextField = "OWNER";
            DropDownList_OWNER.DataValueField = "OWNER";
            DropDownList_OWNER.DataBind();

        }
        else
        {

        }
    }
    private void BindGrid()
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ERPconnectionstring"].ToString();
        Ede.Uof.Utility.Data.DatabaseHelper m_db = new Ede.Uof.Utility.Data.DatabaseHelper(connectionString);

        StringBuilder cmdTxt = new StringBuilder();
        StringBuilder QUERYS = new StringBuilder();
        StringBuilder QUERYS2 = new StringBuilder();
        StringBuilder QUERYS3 = new StringBuilder();

        //DropDownList_ISCLOSED
        if (DropDownList_ISCLOSED.Text.Equals("全部"))
        {
            QUERYS.AppendFormat(@"");
        }
        else if (DropDownList_ISCLOSED.Text.Equals("進行中"))
        {
            QUERYS.AppendFormat(@" AND [ISCLOSED]='N' ");
        }
        else if (DropDownList_ISCLOSED.Text.Equals("已完成"))
        {
            QUERYS.AppendFormat(@" AND [ISCLOSED]='Y' ");
        }
        //DropDownList_OWNER
        if (DropDownList_OWNER.Text.Equals("全部"))
        {
            QUERYS2.AppendFormat(@"");
        }
        else
        {
            QUERYS2.AppendFormat(@" AND OWNER='{0}' ", DropDownList_OWNER.Text);
        }
        //TextBox1
        if (!string.IsNullOrEmpty(TextBox1.Text))
        {
            QUERYS3.AppendFormat(@" AND PROJECTNAMES LIKE '%{0}%' ", TextBox1.Text);
        }
        else
        {
            QUERYS3.AppendFormat(@"");
        }

        cmdTxt.AppendFormat(@"


                            SELECT 
                            [ID]
                            ,[NO] AS '專案編號'
                            ,[PROJECTNAMES] AS '項目名稱'
                            ,[TRYSDATES] AS '產品打樣日'
                            ,[TASTESDATES] AS '產品試吃日'
                            ,[DESIGNSDATES] AS '包裝設計日'
                            ,[SALESDATES] AS '上市日'
                            ,[OWNER] AS '專案負責人'
                            ,[STATUS] AS '狀態'
                            ,[ISCLOSED] AS '是否結案'
                            ,CONVERT(NVARCHAR,[UPDATEDATES],112) AS '更新日'
                            FROM [TKRESEARCH].[dbo].[TB_PROJECTS_PRODUCTS]
                            WHERE 1=1
                            {0}
                            {1}
                            {2}
                            ORDER BY [OWNER],[NO]
                             ", QUERYS.ToString(), QUERYS2.ToString(), QUERYS3.ToString());




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
        int rowIndex = -1;
        // 獲取所選行的索引
        rowIndex = Convert.ToInt32(e.CommandArgument);

        if (e.CommandName == "Button2")
        {
            //MsgBox(e.CommandArgument.ToString() + "OK", this.Page, this);

            if (rowIndex >= 0)
            {
                // 獲取TextBox的值
                GridViewRow row = Grid1.Rows[rowIndex];
                TextBox txtNewField = (TextBox)row.FindControl("txtNewField");
                string newTextValue = txtNewField.Text;

                Label Label_ID = (Label)row.FindControl("Label_ID");
                Label Label_NO = (Label)row.FindControl("Label_專案編號");
                Label Label_項目名稱 = (Label)row.FindControl("Label_項目名稱");
                Label Label_產品打樣日 = (Label)row.FindControl("Label_產品打樣日");
                Label Label_產品試吃日 = (Label)row.FindControl("Label_產品試吃日");
                Label Label_包裝設計日 = (Label)row.FindControl("Label_包裝設計日");
                Label Label_上市日 = (Label)row.FindControl("Label_上市日");
                Label Label_專案負責人 = (Label)row.FindControl("Label_專案負責人");
                Label Label_是否結案 = (Label)row.FindControl("Label_是否結案");

                string ID = Label_ID.Text;
                string NO = Label_NO.Text;
                string PROJECTNAMES = Label_項目名稱.Text;
                string TRYSDATES = Label_產品打樣日.Text;
                string TASTESDATES = Label_產品試吃日.Text;
                string DESIGNSDATES = Label_包裝設計日.Text;
                string SALESDATES = Label_上市日.Text;
                string OWNER = Label_專案負責人.Text;
                string STATUS = newTextValue;
                string ISCLOSED = Label_是否結案.Text;

                //新增記錄檔
                ADD_TB_PROJECTS_PRODUCTS_HISTORYS(
                    ID,
                    NO,
                    PROJECTNAMES,
                    TRYSDATES,
                    TASTESDATES,
                    DESIGNSDATES,
                    SALESDATES,
                    OWNER,
                    STATUS,
                    ISCLOSED
                );
                //更新狀態
                UPDATE_TB_PROJECTS_PRODUCTS_STATUS(
                    ID,
                    NO,
                    STATUS
                    );
            }

            BindGrid();
            BindGrid2();
        }
    }


    public void OnBeforeExport1(object sender, Ede.Uof.Utility.Component.BeforeExportEventArgs e)
    {
        SETEXCEL();

    }

    private void BindGrid2()
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ERPconnectionstring"].ToString();
        Ede.Uof.Utility.Data.DatabaseHelper m_db = new Ede.Uof.Utility.Data.DatabaseHelper(connectionString);

        StringBuilder cmdTxt = new StringBuilder();
        StringBuilder QUERYS = new StringBuilder();
        StringBuilder QUERYS2 = new StringBuilder();
        StringBuilder QUERYS3 = new StringBuilder();

        //DropDownList_ISCLOSED
        if (DropDownList_ISCLOSED.Text.Equals("全部"))
        {
            QUERYS.AppendFormat(@"");
        }
        else if (DropDownList_ISCLOSED.Text.Equals("進行中"))
        {
            QUERYS.AppendFormat(@" AND [ISCLOSED]='N' ");
        }
        else if (DropDownList_ISCLOSED.Text.Equals("已完成"))
        {
            QUERYS.AppendFormat(@" AND [ISCLOSED]='Y' ");
        }
        //DropDownList_OWNER
        if (DropDownList_OWNER.Text.Equals("全部"))
        {
            QUERYS2.AppendFormat(@"");
        }
        else
        {
            QUERYS2.AppendFormat(@" AND OWNER='{0}' ", DropDownList_OWNER.Text);
        }
        //TextBox1
        if (!string.IsNullOrEmpty(TextBox1.Text))
        {
            QUERYS3.AppendFormat(@" AND PROJECTNAMES LIKE '%{0}%' ", TextBox1.Text);
        }
        else
        {
            QUERYS3.AppendFormat(@"");
        }

        cmdTxt.AppendFormat(@"


                            SELECT 
                            [ID]
                            ,[NO] AS '專案編號'
                            ,[PROJECTNAMES] AS '項目名稱'
                            ,[TRYSDATES] AS '產品打樣日'
                            ,[TASTESDATES] AS '產品試吃日'
                            ,[DESIGNSDATES] AS '包裝設計日'
                            ,[SALESDATES] AS '上市日'
                            ,[OWNER] AS '專案負責人'
                            ,[STATUS] AS '狀態'
                            ,[ISCLOSED] AS '是否結案'
                            ,CONVERT(NVARCHAR,[UPDATEDATES],112) AS '更新日'
                            FROM [TKRESEARCH].[dbo].[TB_PROJECTS_PRODUCTS]
                            WHERE 1=1
                            {0}
                            {1}
                            {2}
                            ORDER BY [OWNER],[NO]
                             ", QUERYS.ToString(), QUERYS2.ToString(), QUERYS3.ToString());




        //m_db.AddParameter("@SDATE", SDATE);
        //m_db.AddParameter("@EDATE", EDATE);

        DataTable dt = new DataTable();

        dt.Load(m_db.ExecuteReader(cmdTxt.ToString()));

        Grid2.DataSource = dt;
        Grid2.DataBind();
    }

    protected void grid_PageIndexChanging2(object sender, GridViewPageEventArgs e)
    {
        //Grid1.PageIndex = e.NewPageIndex;
        //BindGrid();
    }
    protected void Grid2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList ddl = (DropDownList)e.Row.FindControl("ddlNewField_是否結案");
            if (ddl != null)
            {
                // 取得資料來源，例如從資料表 "CaseStatus" 抓出 "Name"、"Code"
                string connStr = ConfigurationManager.ConnectionStrings["ERPconnectionstring"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendFormat(@" 
                                        SELECT
                                        [ID]
                                        ,[KIND]
                                        ,[PARAID]
                                        ,[PARANAME]
                                        FROM[TKRESEARCH].[dbo].[TBPARA]
                                        WHERE[KIND] = 'TB_PROJECTS_PRODUCTS_ISCLOSEDYN'
                                        ORDER BY[PARAID]
                                    ");

                    SqlCommand cmd = new SqlCommand(query.ToString(), conn);
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    ddl.DataSource = reader;
                    ddl.DataTextField = "PARANAME";   // 顯示文字
                    ddl.DataValueField = "PARANAME";  // 對應值
                    ddl.DataBind();

                    // 設定選取值
                    string currentValue = DataBinder.Eval(e.Row.DataItem, "是否結案").ToString();
                    if (ddl.Items.FindByValue(currentValue) != null)
                        ddl.SelectedValue = currentValue;

                    reader.Close();
                }
            }
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList ddl = (DropDownList)e.Row.FindControl("ddlNewField_專案負責人");
            if (ddl != null)
            {
                // 取得資料來源，例如從資料表 "CaseStatus" 抓出 "Name"、"Code"
                string connStr = ConfigurationManager.ConnectionStrings["ERPconnectionstring"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendFormat(@" 
                                       SELECT
	                                   [OWNER]      
	                                   FROM [TKRESEARCH].[dbo].[TB_PROJECTS_PRODUCTS]
	                                   GROUP BY [OWNER]
                                       ORDER BY OWNER
                                    ");

                    SqlCommand cmd = new SqlCommand(query.ToString(), conn);
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    ddl.DataSource = reader;
                    ddl.DataTextField = "OWNER";   // 顯示文字
                    ddl.DataValueField = "OWNER";  // 對應值
                    ddl.DataBind();

                    // 設定選取值
                    string currentValue = DataBinder.Eval(e.Row.DataItem, "專案負責人").ToString();
                    if (ddl.Items.FindByValue(currentValue) != null)
                        ddl.SelectedValue = currentValue;

                    reader.Close();
                }
            }
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Button btn3 = (Button)e.Row.FindControl("Button3");
            if (btn3 != null)
            {
                string cellValue3 = btn3.CommandArgument;
                dynamic param3 = new { ID = cellValue3 }.ToExpando();
            }


        }
    }

    protected void Grid2_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int rowIndex = -1;
        // 獲取所選行的索引
        rowIndex = Convert.ToInt32(e.CommandArgument);

        if (e.CommandName == "Button3")
        {
            //MsgBox(e.CommandArgument.ToString() + "OK", this.Page, this);   

            if (rowIndex >= 0)
            {
                // 獲取TextBox的值
                GridViewRow row = Grid2.Rows[rowIndex];


                Label Label_ID = (Label)row.FindControl("Label_ID");

                TextBox txtNewField_專案編號 = (TextBox)row.FindControl("txtNewField_專案編號");
                TextBox txtNewField_項目名稱 = (TextBox)row.FindControl("txtNewField_項目名稱");
                TextBox txtNewField_產品打樣日 = (TextBox)row.FindControl("txtNewField_產品打樣日");
                TextBox txtNewField_產品試吃日 = (TextBox)row.FindControl("txtNewField_產品試吃日");
                TextBox txtNewField_包裝設計日 = (TextBox)row.FindControl("txtNewField_包裝設計日");
                TextBox txtNewField_上市日 = (TextBox)row.FindControl("txtNewField_上市日");
                TextBox txtNewField_輸入狀態 = (TextBox)row.FindControl("txtNewField_輸入狀態");
                DropDownList ddlNewField_專案負責人 = (DropDownList)row.FindControl("ddlNewField_專案負責人");
                DropDownList ddlNewField_是否結案 = (DropDownList)row.FindControl("ddlNewField_是否結案");





                string ID = Label_ID.Text;
                string NO = txtNewField_專案編號.Text;
                string PROJECTNAMES = txtNewField_項目名稱.Text;
                string TRYSDATES = txtNewField_產品打樣日.Text;
                string TASTESDATES = txtNewField_產品試吃日.Text;
                string DESIGNSDATES = txtNewField_包裝設計日.Text;
                string SALESDATES = txtNewField_上市日.Text;
                string OWNER = ddlNewField_專案負責人.SelectedItem.Text;
                string STATUS = txtNewField_輸入狀態.Text;
                string ISCLOSED = ddlNewField_是否結案.SelectedItem.Text;

                //新增記錄檔
                ADD_TB_PROJECTS_PRODUCTS_HISTORYS(
                    ID,
                    NO,
                    PROJECTNAMES,
                    TRYSDATES,
                    TASTESDATES,
                    DESIGNSDATES,
                    SALESDATES,
                    OWNER,
                    STATUS,
                    ISCLOSED
                );
                //更新TB_PROJECTS_PRODUCTS
                UPDATE_TB_PROJECTS_PRODUCTS(
                    ID,
                    NO,
                    PROJECTNAMES,
                    TRYSDATES,
                    TASTESDATES,
                    DESIGNSDATES,
                    SALESDATES,
                    OWNER,
                    STATUS,
                    ISCLOSED
                    );
            }

            BindGrid();
            BindGrid2();
        }

    }


    public void OnBeforeExport2(object sender, Ede.Uof.Utility.Component.BeforeExportEventArgs e)
    {

    }

    public void ADD_TB_PROJECTS_PRODUCTS_HISTORYS(
        string SID,
        string NO,
        string PROJECTNAMES,
        string TRYSDATES,
        string TASTESDATES,
        string DESIGNSDATES,
        string SALESDATES,
        string OWNER,
        string STATUS,
        string ISCLOSED
        )
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ERPconnectionstring"].ToString();

        var SQLCOMMAND = @"                           
                            INSERT INTO [TKRESEARCH].[dbo].[TB_PROJECTS_PRODUCTS_HISTORYS]
                            (
                            [SID]
                            ,[NO]
                            ,[PROJECTNAMES]
                            ,[TRYSDATES]
                            ,[TASTESDATES]
                            ,[DESIGNSDATES]
                            ,[SALESDATES]
                            ,[OWNER]
                            ,[STATUS]
                            ,[ISCLOSED]
                        
                            )
                            VALUES
                            (
                            @SID
                            ,@NO
                            ,@PROJECTNAMES
                            ,@TRYSDATES
                            ,@TASTESDATES
                            ,@DESIGNSDATES
                            ,@SALESDATES
                            ,@OWNER
                            ,@STATUS
                            ,@ISCLOSED
                        
                            )
                            ";

        try
        {
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(SQLCOMMAND, cnn))
                {
                    cmd.Parameters.AddWithValue("@SID", SID);
                    cmd.Parameters.AddWithValue("@NO", NO);
                    cmd.Parameters.AddWithValue("@PROJECTNAMES", PROJECTNAMES);
                    cmd.Parameters.AddWithValue("@TRYSDATES", TRYSDATES);
                    cmd.Parameters.AddWithValue("@TASTESDATES", TASTESDATES);
                    cmd.Parameters.AddWithValue("@DESIGNSDATES", DESIGNSDATES);
                    cmd.Parameters.AddWithValue("@SALESDATES", SALESDATES);
                    cmd.Parameters.AddWithValue("@OWNER", OWNER);
                    cmd.Parameters.AddWithValue("@STATUS", STATUS);
                    cmd.Parameters.AddWithValue("@ISCLOSED", ISCLOSED);



                    cnn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();


                    if (rowsAffected >= 1)
                    {
                        //MsgBox(NO + " 完成", this.Page, this);
                    }
                }
            }
        }
        catch
        {
        }
        finally
        {
        }
    }

    public void UPDATE_TB_PROJECTS_PRODUCTS_STATUS(
        string ID,
        string NO,
        string STATUS
        )
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ERPconnectionstring"].ToString();

        var SQLCOMMAND = @" 
                            UPDATE [TKRESEARCH].[dbo].[TB_PROJECTS_PRODUCTS]
                            SET [STATUS]=@STATUS,[UPDATEDATES]=@UPDATEDATES
                            WHERE [ID]=@ID
                        
                            
                            ";

        try
        {
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(SQLCOMMAND, cnn))
                {
                    cmd.Parameters.AddWithValue("@ID", ID);
                    cmd.Parameters.AddWithValue("@STATUS", STATUS);
                    cmd.Parameters.AddWithValue("@UPDATEDATES", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));

                    cnn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected >= 1)
                    {
                        MsgBox(NO + " 完成", this.Page, this);
                    }
                }
            }
        }
        catch
        {
        }
        finally
        {
        }
    }

    public void UPDATE_TB_PROJECTS_PRODUCTS(
                    string ID,
                    string NO,
                    string PROJECTNAMES,
                    string TRYSDATES,
                    string TASTESDATES,
                    string DESIGNSDATES,
                    string SALESDATES,
                    string OWNER,
                    string STATUS,
                    string ISCLOSED
                    )
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ERPconnectionstring"].ToString();

        var SQLCOMMAND = @" 
                         UPDATE [TKRESEARCH].[dbo].[TB_PROJECTS_PRODUCTS]
                        SET [NO]=@NO,[PROJECTNAMES]=@PROJECTNAMES,[TRYSDATES]=@TRYSDATES,[TASTESDATES]=@TASTESDATES,[DESIGNSDATES]=@DESIGNSDATES,[SALESDATES]=@SALESDATES,[OWNER]=@OWNER,[STATUS]=@STATUS,[ISCLOSED]=@ISCLOSED,[UPDATEDATES]=@UPDATEDATES
                        WHERE [ID]=@ID
                        
                            
                            ";

        try
        {
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(SQLCOMMAND, cnn))
                {
                    cmd.Parameters.AddWithValue("@ID", ID);
                    cmd.Parameters.AddWithValue("@NO", NO);
                    cmd.Parameters.AddWithValue("@PROJECTNAMES", PROJECTNAMES);
                    cmd.Parameters.AddWithValue("@TRYSDATES", TRYSDATES);
                    cmd.Parameters.AddWithValue("@TASTESDATES", TASTESDATES);
                    cmd.Parameters.AddWithValue("@DESIGNSDATES", DESIGNSDATES);
                    cmd.Parameters.AddWithValue("@SALESDATES", SALESDATES);
                    cmd.Parameters.AddWithValue("@OWNER", OWNER);
                    cmd.Parameters.AddWithValue("@STATUS", STATUS);
                    cmd.Parameters.AddWithValue("@ISCLOSED", ISCLOSED);
                    cmd.Parameters.AddWithValue("@UPDATEDATES", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));

                    cnn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected >= 1)
                    {
                        MsgBox(NO + " 完成", this.Page, this);
                    }
                }
            }
        }
        catch
        {
        }
        finally
        {
        }
    }


    public void SETEXCEL()
    {
        BindGrid();
        //BindGrid中已帶入EXCELDT1
        if (EXCELDT1.Rows.Count>=1)
        {
            //檔案名稱
            var fileName = "明細" + DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss") + ".xlsx";
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial; // 關閉新許可模式通知

            using (var excel = new ExcelPackage(new FileInfo(fileName)))
            {

                // 建立分頁
                var ws = excel.Workbook.Worksheets.Add("list" + DateTime.Now.ToShortDateString());


                //預設行高
                //ws.DefaultRowHeight = 60;

                // 寫入資料試試
                //ws.Cells[2, 1].Value = "測試測試";
                int ROWS = 2;
                int COLUMNS = 1;


                //excel標題
                ws.Cells[1, 1].Value = "專案編號";
                ws.Cells[1, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center; //欄位置中
                ws.Cells[1, 1].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center; //高度置中
                ws.Cells[1, 1].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin); //儲存格框線
                ws.Cells[1, 2].Value = "項目名稱";
                ws.Cells[1, 2].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center; //欄位置中
                ws.Cells[1, 2].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center; //高度置中
                ws.Cells[1, 2].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin); //儲存格框線
                ws.Cells[1, 3].Value = "產品打樣日";
                ws.Cells[1, 3].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center; //欄位置中
                ws.Cells[1, 3].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center; //高度置中
                ws.Cells[1, 3].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin); //儲存格框線
                ws.Cells[1, 4].Value = "產品試吃日";
                ws.Cells[1, 4].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center; //欄位置中
                ws.Cells[1, 4].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center; //高度置中
                ws.Cells[1, 4].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin); //儲存格框線
                ws.Cells[1, 5].Value = "包裝設計日";
                ws.Cells[1, 5].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center; //欄位置中
                ws.Cells[1, 5].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center; //高度置中
                ws.Cells[1, 5].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin); //儲存格框線
                ws.Cells[1, 6].Value = "上市日";
                ws.Cells[1, 6].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center; //欄位置中
                ws.Cells[1, 6].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center; //高度置中
                ws.Cells[1, 6].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin); //儲存格框線
                ws.Cells[1, 7].Value = "專案負責人";
                ws.Cells[1, 7].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center; //欄位置中
                ws.Cells[1, 7].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center; //高度置中
                ws.Cells[1, 7].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin); //儲存格框線
                ws.Cells[1, 8].Value = "狀態";
                ws.Cells[1, 8].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center; //欄位置中
                ws.Cells[1, 8].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center; //高度置中
                ws.Cells[1, 8].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin); //儲存格框線
                ws.Cells[1, 9].Value = "是否結案";
                ws.Cells[1, 9].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center; //欄位置中
                ws.Cells[1, 9].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center; //高度置中
                ws.Cells[1, 9].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin); //儲存格框線
                ws.Cells[1, 10].Value = "更新日";
                ws.Cells[1, 10].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left; //欄位置中
                ws.Cells[1, 10].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center; //高度置中
                ws.Cells[1, 10].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin); //儲存格框線
                ws.Cells[1, 11].Value = "ID";
                ws.Cells[1, 11].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left; //欄位置中
                ws.Cells[1, 11].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center; //高度置中
                ws.Cells[1, 11].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin); //儲存格框線                w

                foreach (DataRow od in EXCELDT1.Rows)
                {
                    ws.Cells[ROWS, 1].Value = od["專案編號"].ToString();
                    ws.Cells[ROWS, 1].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center; //高度置中
                    ws.Cells[ROWS, 1].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin); //儲存格框線
                    ws.Cells[ROWS, 2].Value = od["項目名稱"].ToString();
                    ws.Cells[ROWS, 2].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center; //高度置中
                    ws.Cells[ROWS, 2].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin); //儲存格框線
                    ws.Cells[ROWS, 3].Value = od["產品打樣日"].ToString();
                    ws.Cells[ROWS, 3].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center; //高度置中
                    ws.Cells[ROWS, 3].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin); //儲存格框線
                    ws.Cells[ROWS, 4].Value = od["產品試吃日"].ToString();
                    ws.Cells[ROWS, 4].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center; //高度置中
                    ws.Cells[ROWS, 4].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin); //儲存格框線
                    ws.Cells[ROWS, 5].Value = od["包裝設計日"].ToString();
                    ws.Cells[ROWS, 5].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center; //高度置中
                    ws.Cells[ROWS, 5].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin); //儲存格框線
                    ws.Cells[ROWS, 6].Value = od["上市日"].ToString();
                    ws.Cells[ROWS, 6].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center; //高度置中
                    ws.Cells[ROWS, 6].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin); //儲存格框線
                    ws.Cells[ROWS, 7].Value = od["專案負責人"].ToString();
                    ws.Cells[ROWS, 7].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center; //高度置中
                    ws.Cells[ROWS, 7].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin); //儲存格框線
                    ws.Cells[ROWS, 8].Value = od["狀態"].ToString();
                    ws.Cells[ROWS, 8].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center; //高度置中
                    ws.Cells[ROWS, 8].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin); //儲存格框線
                    ws.Cells[ROWS, 9].Value = od["是否結案"].ToString();
                    ws.Cells[ROWS, 9].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center; //高度置中
                    ws.Cells[ROWS, 9].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin); //儲存格框線
                    ws.Cells[ROWS, 10].Value = od["更新日"].ToString();
                    ws.Cells[ROWS, 10].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center; //高度置中
                    ws.Cells[ROWS, 10].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin); //儲存格框線
                    ws.Cells[ROWS, 11].Value = od["ID"].ToString();
                    ws.Cells[ROWS, 11].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center; //高度置中
                    ws.Cells[ROWS, 11].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin); //儲存格框線
                  

                    ROWS++;
                }




                ////預設列寬、行高
                //sheet.DefaultColWidth = 10; //預設列寬
                //sheet.DefaultRowHeight = 30; //預設行高

                //// 遇\n或(char)10自動斷行
                //ws.Cells.Style.WrapText = true;

                //自適應寬度設定
                ws.Cells[ws.Dimension.Address].AutoFitColumns();

                //自適應高度設定
                ws.Row(1).CustomHeight = true;



                //儲存Excel
                //Byte[] bin = excel.GetAsByteArray();
                //File.WriteAllBytes(@"C:\TEMP\" + fileName, bin);

                //儲存和歸來的Excel檔案作為一個ByteArray
                var data = excel.GetAsByteArray();
                HttpResponse response = HttpContext.Current.Response;
                Response.Clear();

                //輸出標頭檔案　　
                Response.AddHeader("content-disposition", "attachment;  filename=" + fileName + "");
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.BinaryWrite(data);
                Response.Flush();
                Response.End();
                //package.Save();//這個方法是直接下載到本地
            }
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
        BindGrid2();
    }

    #endregion
}