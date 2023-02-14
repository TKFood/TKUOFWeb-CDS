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

public partial class CDS_WebPage_RESEARCH_TKRESEARCH_COST : Ede.Uof.Utility.Page.BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SETYEARSWEEKS();
            BindGrid1("","","");

        }
        else
        {


        }




    }
    #region FUNCTION
  
    //private void BindDropDownList()
    //{
    //    DataTable dt = new DataTable();
    //    dt.Columns.Add("ID", typeof(String));
    //    dt.Columns.Add("NAMES", typeof(String));

    //    string connectionString = ConfigurationManager.ConnectionStrings["ERPconnectionstring"].ToString();
    //    Ede.Uof.Utility.Data.DatabaseHelper m_db = new Ede.Uof.Utility.Data.DatabaseHelper(connectionString);

    //    string cmdTxt = @"SELECT '未完成' AS KINDS UNION ALL SELECT '全部' ";

    //    dt.Load(m_db.ExecuteReader(cmdTxt));

    //    if (dt.Rows.Count > 0)
    //    {
    //        DropDownList1.DataSource = dt;
    //        DropDownList1.DataTextField = "KINDS";
    //        DropDownList1.DataValueField = "KINDS";
    //        DropDownList1.DataBind();

    //    }
    //    else
    //    {

    //    }
    //}


    public void SETYEARSWEEKS()
    {
        txtDate1.Text = DateTime.Now.ToString("yyyy");
       
    }

    private void BindGrid1(string YM, string MB001, string MB002)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ERPconnectionstring"].ToString();
        Ede.Uof.Utility.Data.DatabaseHelper m_db = new Ede.Uof.Utility.Data.DatabaseHelper(connectionString);

        StringBuilder cmdTxt = new StringBuilder();
        StringBuilder SQUERY = new StringBuilder();



        //查詢條件
        if (!string.IsNullOrEmpty(MB001))
        {
            SQUERY.AppendFormat(@" AND TA001 LIKE '%{0}%' ", MB001);
        }
        else
        {
            SQUERY.AppendFormat(@"");
        }

        if (!string.IsNullOrEmpty(MB002))
        {
            SQUERY.AppendFormat(@" AND MB002 LIKE '%{0}%' ", MB002);
        }
        else
        {
            SQUERY.AppendFormat(@"");
        }
      
        if (!string.IsNullOrEmpty(YM) && !string.IsNullOrEmpty(SQUERY.ToString()))
        {
            cmdTxt.AppendFormat(@"
                               SELECT TA002 AS '年月',TA001 AS '品號',MB002 AS '品名',MB003 AS '規格',生產入庫數,ME005 在製約量_材料,本階人工成本,本階製造費用,ME007 材料成本,ME008 人工成本,ME009 製造費用,ME010 加工費用
                                ,((ME007+ME008+ME009+ME010)/(生產入庫數+ME005)) 單位成本, ((ME007)/(生產入庫數+ME005)) 單位材料成本, ((ME008)/(生產入庫數+ME005)) 單位人工成本,((ME009)/(生產入庫數+ME005)) 單位製造成本,((ME010)/(生產入庫數+ME005)) 單位加工成本
                                ,MB068
                                ,(CASE WHEN MB068 IN ('09') THEN 本階人工成本/(生產入庫數+ME005) ELSE 0 END ) 平均包裝人工成本
                                ,(CASE WHEN MB068 IN ('09') THEN 本階製造費用/(生產入庫數+ME005) ELSE 0 END ) 平均包裝製造費用
                                ,(CASE WHEN MB068 IN ('03') THEN 本階人工成本/(生產入庫數+ME005) ELSE 0 END ) 平均小線人工成本
                                ,(CASE WHEN MB068 IN ('03') THEN 本階製造費用/(生產入庫數+ME005) ELSE 0 END ) 平均小線製造費用
                                ,(CASE WHEN MB068 IN ('02') THEN 本階人工成本/(生產入庫數+ME005) ELSE 0 END ) 平均大線人工成本
                                ,(CASE WHEN MB068 IN ('02') THEN 本階製造費用/(生產入庫數+ME005) ELSE 0 END ) 平均大線製造費用
                                FROM 
                                (
                                SELECT TA002,TA001,SUM(TA012) '生產入庫數',SUM(TA016-TA019) AS '本階人工成本',SUM(TA017-TA020) AS '本階製造費用'
                                FROM [TK].dbo.CSTTA
                                WHERE TA002 LIKE '{0}%'
                                GROUP BY TA002,TA001
                                ) AS TEMP
                                LEFT JOIN [TK].dbo.CSTME ON ME001=TA001 AND ME002=TA002
                                LEFT JOIN [TK].dbo.INVMB ON MB001=TA001
                                WHERE 1=1
                               {1} 

                                    AND (生產入庫數+ME005)>0
                                    ORDER BY TA001,TA002   

                                    ", YM, SQUERY.ToString());

        }
        else
        {

        }


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
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    ///Get the button that raised the event
        //    Button btn = (Button)e.Row.FindControl("GWButton1");
        //    //Get the row that contains this button
        //    GridViewRow gvr = (GridViewRow)btn.NamingContainer;
        //    //string cellvalue = gvr.Cells[2].Text.Trim();
        //    string Cellvalue = btn.CommandArgument;
        //    DataRowView row = (DataRowView)e.Row.DataItem;
        //    Button lbtnName = (Button)e.Row.FindControl("GWButton1");
        //    ExpandoObject param = new { ID = Cellvalue }.ToExpando();
        //    //Grid開窗是用RowDataBound事件再開窗          

        //    Dialog.Open2(lbtnName, "~/CDS/WebPage/CUSTOMERIZE/TK_SCH_DEVOLVEDialogEDIT.aspx", "", 800, 600, Dialog.PostBackType.AfterReturn, param);


        //}

    }
    protected void Grid1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int rowIndex = -1;

        //if (e.CommandName == "GWButton1")
        //{

        //    BindGrid1("");

        //}
      
       
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
        //if (!string.IsNullOrEmpty(TextBox1.Text))
        //{
        //    QUERYS.AppendFormat(@"", TextBox1.Text);
        //}


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
        BindGrid1(txtDate1.Text.ToString(), TextBox1.Text.ToString(), TextBox2.Text.ToString());
    }

   


    #endregion
}