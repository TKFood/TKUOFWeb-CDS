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
using Ede.Uof.Utility.Data;
using Ede.Uof.Utility.Page.Common;
using OfficeOpenXml;
using OfficeOpenXml.Drawing;
using OfficeOpenXml.Style;
using System.Globalization;

public partial class CDS_WebPage_CUSTOMERIZE_TK_SCH_DEVOLVE : Ede.Uof.Utility.Page.BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
    
            BindDropDownList();           

            BindGrid1("");

        }
        else
        {


        }




    }
    #region FUNCTION
  
    private void BindDropDownList()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("ID", typeof(String));
        dt.Columns.Add("NAMES", typeof(String));

        string connectionString = ConfigurationManager.ConnectionStrings["ERPconnectionstring"].ToString();
        Ede.Uof.Utility.Data.DatabaseHelper m_db = new Ede.Uof.Utility.Data.DatabaseHelper(connectionString);

        string cmdTxt = @"SELECT '未完成' AS KINDS UNION ALL SELECT '全部' ";

        dt.Load(m_db.ExecuteReader(cmdTxt));

        if (dt.Rows.Count > 0)
        {
            DropDownList1.DataSource = dt;
            DropDownList1.DataTextField = "KINDS";
            DropDownList1.DataValueField = "KINDS";
            DropDownList1.DataBind();

        }
        else
        {

        }
    }

  
   

    private void BindGrid1(string SALESFOCUS)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
        Ede.Uof.Utility.Data.DatabaseHelper m_db = new Ede.Uof.Utility.Data.DatabaseHelper(connectionString);

        StringBuilder cmdTxt = new StringBuilder();
        StringBuilder QUERYS = new StringBuilder();
        



        //查詢條件
        if (!string.IsNullOrEmpty(TextBox1.Text))
        {
            QUERYS.AppendFormat(@"  AND TB_EIP_FORUM_ARTICLE.SUBJECT  LIKE '%{0}%'", TextBox1.Text);
        }
        if (DropDownList1.Text.Equals("未完成"))
        {
            QUERYS.AppendFormat(@"  AND ISNULL(TB_EIP_SCH_DEVOLVE_EXAMINE_LOG.STATUS,'') NOT IN ('Approve')");
            QUERYS.AppendFormat(@"  AND TB_EIP_SCH_DEVOLVE.DEVOLVE_GUID NOT IN (SELECT [DEVOLVE_GUID]  FROM [UOF].[dbo].[Z_TB_EIP_SCH_DEVOLVE_IGNORES])");
        }
        if (DropDownList1.Text.Equals("全部"))
        {
            QUERYS.AppendFormat(@"  ");
        }


        cmdTxt.AppendFormat(@"
                            SELECT CONVERT(nvarchar,TB_EIP_SCH_WORK.CREATE_TIME,111) AS '交辨開始時間'
                            ,TB_EIP_SCH_DEVOLVE.SUBJECT AS '校稿區內容'
                            ,TB_EIP_FORUM_ARTICLE.SUBJECT AS '主題'
                            ,SUBSTRING(TB_EIP_FORUM_ARTICLE.SUBJECT,14,LEN(TB_EIP_FORUM_ARTICLE.SUBJECT)-14) AS SUBS
                            ,TB_EIP_SCH_DEVOLVE.DEVOLVE_GUID AS 'DEVOLVE_GUID'
                            ,TB_EIP_SCH_WORK.SUBJECT AS '交辨項目'
                            ,TB_EIP_SCH_WORK.EXECUTE_USER AS '交辨'
                            ,TB_EIP_SCH_WORK.WORK_STATE AS 'WORK_STATE'
                            ,(ISNULL(TB_EIP_SCH_WORK.PROCEEDING_DESC,'')+ISNULL(TB_EIP_SCH_WORK.COMPLETE_DESC,''))  AS '交辨回覆'
                            ,TB_EB_USER.NAME AS '交辨人'
                            ,(CASE  WHEN TB_EIP_SCH_WORK.WORK_STATE='Completed' THEN '審稿完成' WHEN TB_EIP_SCH_WORK.WORK_STATE='Audit' THEN '交辨完成' WHEN TB_EIP_SCH_WORK.WORK_STATE='Proceeding' THEN '處理中' WHEN TB_EIP_SCH_WORK.WORK_STATE='NotYetBegin' THEN '未開始' END) AS '交辨狀態'
                            ,TB_EIP_SCH_DEVOLVE_EXAMINE_LOG.*
                            FROM [UOF].dbo.TB_EIP_FORUM_ARTICLE,[UOF].dbo.TB_EIP_SCH_DEVOLVE
                            LEFT JOIN [UOF].dbo.TB_EIP_SCH_DEVOLVE_EXAMINE_LOG ON TB_EIP_SCH_DEVOLVE_EXAMINE_LOG.DEVOLVE_GUID=TB_EIP_SCH_DEVOLVE.DEVOLVE_GUID
                            LEFT JOIN [UOF].dbo.TB_EIP_SCH_WORK ON TB_EIP_SCH_WORK.DEVOLVE_GUID=TB_EIP_SCH_DEVOLVE.DEVOLVE_GUID
                            LEFT JOIN [UOF].dbo.TB_EB_USER ON TB_EB_USER.USER_GUID=TB_EIP_SCH_WORK.EXECUTE_USER
                            WHERE 1=1
                            AND TB_EIP_FORUM_ARTICLE.FLOOR=1
                            AND TB_EIP_FORUM_ARTICLE.SUBJECT LIKE '2%'
                            AND TB_EIP_FORUM_ARTICLE.SUBJECT NOT LIKE '%會議記錄%'
                            AND TB_EIP_SCH_DEVOLVE.SUBJECT LIKE '%'+SUBSTRING(TB_EIP_FORUM_ARTICLE.SUBJECT,14,LEN(TB_EIP_FORUM_ARTICLE.SUBJECT)-14)+'%'
                            
                            {0}

                            ORDER BY TB_EIP_SCH_DEVOLVE.CREATE_TIME,TB_EIP_FORUM_ARTICLE.SUBJECT
                               
                                ", QUERYS.ToString());




        //m_db.AddParameter("@SDATE", SDATE);
        //m_db.AddParameter("@EDATE", EDATE);

        DataTable dt = new DataTable();

        dt.Load(m_db.ExecuteReader(cmdTxt.ToString()));

        Grid1.DataSource = dt;
        Grid1.DataBind();
    }




    protected void grid1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //Grid1.PageIndex = e.NewPageIndex;
        //BindGrid();
    }
    protected void Grid1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {


        }

    }
    protected void Grid1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int rowIndex = -1;

        if (e.CommandName == "GWButton1")
        {
          
           
        }
      
       
    }


    public void OnBeforeExport1(object sender, Ede.Uof.Utility.Component.BeforeExportEventArgs e)
    {

        SETEXCEL();

    }

    public void SETEXCEL()
    {
        string connectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
        Ede.Uof.Utility.Data.DatabaseHelper m_db = new Ede.Uof.Utility.Data.DatabaseHelper(connectionString);

        StringBuilder cmdTxt = new StringBuilder();
        StringBuilder QUERYS = new StringBuilder();

        //
        if (!string.IsNullOrEmpty(TextBox1.Text))
        {
            QUERYS.AppendFormat(@"", TextBox1.Text);
        }


        cmdTxt.AppendFormat(@" 
                            
                               
                                ", QUERYS.ToString());



        DataTable dt = new DataTable();

        dt.Load(m_db.ExecuteReader(cmdTxt.ToString()));

        if (dt.Rows.Count > 0)
        {
            //檔案名稱
            var fileName = "資料" + DateTime.Now.ToString("yyyy-MM-dd--hh-mm-ss") + ".xlsx";
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
                ws.Cells[1, 1].Value = "目前簽核者";
                ws.Cells[1, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center; //欄位置中
                ws.Cells[1, 1].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center; //高度置中
                ws.Cells[1, 1].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin); //儲存格框線


                foreach (DataRow od in dt.Rows)
                {
                    ws.Cells[ROWS, 1].Value = od["NAME"].ToString();
                    ws.Cells[ROWS, 1].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center; //高度置中
                    ws.Cells[ROWS, 1].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin); //儲存格框線


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
            //ExcelPackage.LicenseContext = LicenseContext.NonCommercial; // 關閉新許可模式通知
            //                                                            // 沒設置的話會跳出 Please set the excelpackage.licensecontext property


            ////var file = new FileInfo(fileName);
            //using (var excel = new ExcelPackage(file))
            //{

            //}
        }

    }

    public void OnBeforeExport2(object sender, Ede.Uof.Utility.Component.BeforeExportEventArgs e)
    {

        //SETEXCEL();

    }

    public void MsgBox(String ex, Page pg, Object obj)
    {
        string s = "<SCRIPT language='javascript'>alert('" + ex.Replace("\r\n", "\\n").Replace("'", "") + "'); </SCRIPT>";
        Type cstype = obj.GetType();
        ClientScriptManager cs = pg.ClientScript;
        cs.RegisterClientScriptBlock(cstype, s, s.ToString());
    }
    #endregion

    #region BUTTON

    protected void btn1_Click(object sender, EventArgs e)
    {
        BindGrid1("");
    }

   


    #endregion
}