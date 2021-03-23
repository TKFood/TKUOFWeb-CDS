using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ede.Uof.Utility.Data;
using Ede.Uof.Utility.Page.Common;

public partial class CDS_WebPage_COP_TBBU_PRODUCTS : Ede.Uof.Utility.Page.BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            BindGrid();
        }
        else
        {
            BindGrid();
        }
    }
    #region FUNCTION

    private void BindGrid()
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ERPconnectionstring"].ToString();
        Ede.Uof.Utility.Data.DatabaseHelper m_db = new Ede.Uof.Utility.Data.DatabaseHelper(connectionString);

        string cmdTxt = @" 
                        SELECT [PRODUCTS].[MB001],[PRODUCTSFEATURES],[SALESFOCUS],[COPYWRITINGS],[PICPATHS]
                        ,MB002,MB003,MB004,MA003,ISNULL(MD007,0) AS MD007,CONVERT(NVARCHAR,MB023)+(CASE WHEN MB198='1' THEN '天' ELSE (CASE WHEN MB198='2' THEN '月' ELSE '年' END ) END ) AS 'VALIDITYPERIOD',CONVERT(decimal(16,3),ISNULL(MB047,0)) AS MB047,MB013
                        ,[ALBUM_GUID], [PHOTO_GUID],[PHOTO_DESC],[FILE_ID],[RESIZE_FILE_ID],[THUMBNAIL_FILE_ID]
                        FROM [TKBUSINESS].[dbo].[PRODUCTS]
                        LEFT JOIN [TK].dbo.[INVMB] ON [PRODUCTS].[MB001]=[INVMB].[MB001]
                        LEFT JOIN [TK].dbo.INVMA ON MA001='9' AND MA002=MB115
                        LEFT JOIN [TK].dbo.BOMMD ON MD001=[INVMB].[MB001] AND MD003 LIKE '201%'
                        LEFT JOIN [192.168.1.223].[UOF].[dbo].[TB_EIP_ALBUM_PHOTO] ON [PHOTO_TOPIC]=[PRODUCTS].[MB001] COLLATE Chinese_Taiwan_Stroke_BIN
                        ORDER BY [PRODUCTS].[MB001]
                        ";

        //m_db.AddParameter("@SDATE", SDATE);
        //m_db.AddParameter("@EDATE", EDATE);

        DataTable dt = new DataTable();

        dt.Load(m_db.ExecuteReader(cmdTxt));

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
            //Get the button that raised the event
            Button btn = (Button)e.Row.FindControl("Button1");

            //Get the row that contains this button
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;

            //string cellvalue = gvr.Cells[2].Text.Trim();
            string Cellvalue = btn.CommandArgument;

            DataRowView row = (DataRowView)e.Row.DataItem;
            Button lbtnName = (Button)e.Row.FindControl("Button1");

            ExpandoObject param = new { ID = Cellvalue }.ToExpando();

            //Grid開窗是用RowDataBound事件再開窗
            Dialog.Open2(lbtnName, "~/CDS/WebPage/COP/TBBU_PRODUCTSDialogEDITDEL.aspx", "", 800, 600, Dialog.PostBackType.AfterReturn, param);
        }


        

        StringBuilder PATH = new StringBuilder();        

        Image img = (Image)e.Row.FindControl("Image1");
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView row = (DataRowView)e.Row.DataItem;
            Image img1 = (Image)e.Row.FindControl("Image1");

           

            if (!string.IsNullOrEmpty(row["PHOTO_GUID"].ToString()))
            {
                //img.ImageUrl = "https://eip.tkfood.com.tw/UOF/common/filecenter/v3/handler/downloadhandler.ashx?id=8b2a033b-c301-419b-938d-e6cfedf28b82&path=ALBUM%5C2021%5C03&contentType=image%2Fpng&name=40100010650490.png";


                PATH.AppendFormat(@"https://eip.tkfood.com.tw/UOF/common/filecenter/v3/handler/downloadhandler.ashx?id={0}&path=ALBUM%5C2021%5C03&contentType=image%2Fpng&name={1}
                                ", row["RESIZE_FILE_ID"].ToString(), row["PHOTO_DESC"].ToString());
                
                img.ImageUrl = PATH.ToString();

                //img.ImageUrl  = Request.ApplicationPath + "/Common/FileCenter/ShowImage.aspx?id=" + row["THUMBNAIL_FILE_ID"].ToString();

                //  img.ImageUrl = string.Format("~/Common/FileCenter/Downloadfile.ashx?id={0}", row["THUMBNAIL_FILE_ID"].ToString());

                //e.Row.Cells[0].Text = row["THUMBNAIL_FILE_ID"].ToString();
                ////獲取當前行的圖片路徑
                //string ImgUrl = img.ImageUrl;
                ////給帶圖片的單元格添加點擊事件
                //e.Row.Cells[3].Attributes.Add("onclick", e.Row.Cells[3].ClientID.ToString()
                //    + ".checked=true;CellClick('" + ImgUrl + "')");

                //  img.ImageUrl = "https://eip.tkfood.com.tw/BM/upload/note/20200926112527.jpg";
            }


        }


    }

    public void OnBeforeExport1(object sender, Ede.Uof.Utility.Component.BeforeExportEventArgs e)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ERPconnectionstring"].ToString();
        Ede.Uof.Utility.Data.DatabaseHelper m_db = new Ede.Uof.Utility.Data.DatabaseHelper(connectionString);


        string cmdTxt = @" 
                       SELECT [PRODUCTS].[MB001],[PRODUCTSFEATURES],[SALESFOCUS],[COPYWRITINGS],[PICPATHS]
                        ,MB002,MB003,MB004,MA003,ISNULL(MD007,0) AS MD007,CONVERT(NVARCHAR,MB023)+(CASE WHEN MB198='1' THEN '天' ELSE (CASE WHEN MB198='2' THEN '月' ELSE '年' END ) END ) AS 'VALIDITYPERIOD',CONVERT(decimal(16,3),ISNULL(MB047,0)) AS MB047,MB013
                        ,[ALBUM_GUID], [PHOTO_GUID],[PHOTO_DESC],[FILE_ID],[RESIZE_FILE_ID],[THUMBNAIL_FILE_ID]
                        FROM [TKBUSINESS].[dbo].[PRODUCTS]
                        LEFT JOIN [TK].dbo.[INVMB] ON [PRODUCTS].[MB001]=[INVMB].[MB001]
                        LEFT JOIN [TK].dbo.INVMA ON MA001='9' AND MA002=MB115
                        LEFT JOIN [TK].dbo.BOMMD ON MD001=[INVMB].[MB001] AND MD003 LIKE '201%'
                        LEFT JOIN [192.168.1.223].[UOF].[dbo].[TB_EIP_ALBUM_PHOTO] ON [PHOTO_TOPIC]=[PRODUCTS].[MB001] COLLATE Chinese_Taiwan_Stroke_BIN
                        ORDER BY [PRODUCTS].[MB001]
                        ";



        DataTable dt = new DataTable();

        dt.Load(m_db.ExecuteReader(cmdTxt));

        if (dt.Rows.Count > 0)
        {
            dt.Columns[0].Caption = "ID";


            e.Datasource = dt;
        }
    }
    #endregion

    #region BUTTON
    protected void btn_Click(object sender, EventArgs e)
    {


        //開窗後回傳參數
        if (!string.IsNullOrEmpty(Dialog.GetReturnValue()))
        {
            //txtReturnValue.Text = Dialog.GetReturnValue();
        }


    }


    protected void btn1_Click(object sender, EventArgs e)
    {
        //this.Session["SDATE"] = txtDate1.Text.Trim();
        //this.Session["EDATE"] = txtDate2.Text.Trim();
    }
    protected void MyButtonClick(object sender, System.EventArgs e)
    {
      

    }
    #endregion
}