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

public partial class CDS_WebPage_IT_UOF_FORMS : Ede.Uof.Utility.Page.BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {            

            BindGrid("");
        }
        else
        {

            BindGrid("");

        }

       


    }
    #region FUNCTION
  
    private void BindGrid(string SALESFOCUS)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
        Ede.Uof.Utility.Data.DatabaseHelper m_db = new Ede.Uof.Utility.Data.DatabaseHelper(connectionString);

        StringBuilder cmdTxt = new StringBuilder();
        StringBuilder QUERYS = new StringBuilder();

      

   
        //建議售價
        if (!string.IsNullOrEmpty(TextBox1.Text))
        {
            QUERYS.AppendFormat(@" AND DOC_NBR LIKE '%{0}%'", TextBox1.Text);
        }
   

        cmdTxt.AppendFormat(@" 
                            SELECT
                            usr2.NAME
                            ,(CASE WHEN  usr.IS_SUSPENDED = 1 THEN  usr.NAME + '(x)' WHEN  ISNULL(usr.ACCOUNT,'''') = '' THEN  'unknown user' ELSE usr.NAME END) AS APPLICANT_NAM
                            ,form.FORM_NAME
                            ,DOC_NBR
                            ,CONVERT(NVARCHAR,NODES.START_TIME,111) AS 'START_TIME'
                            ,DATEDIFF(HOUR,START_TIME,GETDATE()) AS 'HRS'
                            ,CONVERT(NVARCHAR,BEGIN_TIME,111) AS BEGIN_TIME

                            ,task.TASK_ID
                            ,END_TIME
                            ,TASK_RESULT
                            ,TASK_STATUS
                            ,task.USER_GUID
                            ,formVer.FORM_VERSION_ID
                            ,formVer.FORM_ID
                            ,CURRENT_SITE_ID
                            ,MESSAGE_CONTENT
                            ,LOCK_STATUS
                            ,ISNULL(formVer.DISPLAY_TITLE,'') AS VERSION_TITLE
                            ,ISNULL(task.JSON_DISPLAY,'') AS JSON_DISPLAY

                            FROM [UOF].dbo.TB_WKF_TASK task
                            INNER JOIN [UOF].dbo.TB_WKF_FORM_VERSION formVer ON task.FORM_VERSION_ID = formVer.FORM_VERSION_ID
                            INNER JOIN [UOF].dbo.TB_WKF_FORM form  ON  formVer.FORM_ID = form.FORM_ID 
                            LEFT JOIN [UOF].dbo.TB_EB_USER [usr]  ON task.USER_GUID = usr.USER_GUID
                            LEFT JOIN [UOF].dbo.TB_WKF_TASK_NODE [NODES] ON NODES.SITE_ID=task.CURRENT_SITE_ID
                            LEFT JOIN [UOF].dbo.TB_EB_USER [usr2]  ON NODES.ORIGINAL_SIGNER = [usr2].USER_GUID
                            WHERE
                            1=1  AND  TASK_STATUS NOT IN ('2')
                                {0}
                            ORDER BY HRS DESC,usr2.NAME,form.FORM_NAME,DOC_NBR
                               
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
        


    }

    public void OnBeforeExport1(object sender, Ede.Uof.Utility.Component.BeforeExportEventArgs e)
    {
       

        //string connectionString = ConfigurationManager.ConnectionStrings["ERPconnectionstring"].ToString();
        //Ede.Uof.Utility.Data.DatabaseHelper m_db = new Ede.Uof.Utility.Data.DatabaseHelper(connectionString);


        //string cmdTxt = @" 
        //               SELECT [PRODUCTS].[MB001],[PRODUCTSFEATURES],[SALESFOCUS],[COPYWRITINGS],[PICPATHS]
        //                ,MB002,MB003,MB004,MA003,ISNULL(MD007,0) AS MD007,CONVERT(NVARCHAR,MB023)+(CASE WHEN MB198='1' THEN '天' ELSE (CASE WHEN MB198='2' THEN '月' ELSE '年' END ) END ) AS 'VALIDITYPERIOD',CONVERT(decimal(16,3),ISNULL(MB047,0)) AS MB047,MB013
        //                ,[ALBUM_GUID], [PHOTO_GUID],[PHOTO_DESC],[FILE_ID],[RESIZE_FILE_ID],[THUMBNAIL_FILE_ID]
        //                FROM [TKBUSINESS].[dbo].[PRODUCTS]
        //                LEFT JOIN [TK].dbo.[INVMB] ON [PRODUCTS].[MB001]=[INVMB].[MB001]
        //                LEFT JOIN [TK].dbo.INVMA ON MA001='9' AND MA002=MB115
        //                LEFT JOIN [TK].dbo.BOMMD ON MD001=[INVMB].[MB001] AND MD003 LIKE '201%'
        //                LEFT JOIN [192.168.1.223].[UOF].[dbo].[TB_EIP_ALBUM_PHOTO] ON [PHOTO_TOPIC]=[PRODUCTS].[MB001] COLLATE Chinese_Taiwan_Stroke_BIN
        //                ORDER BY [PRODUCTS].[MB001]
        //                ";



        //DataTable dt = new DataTable();

        //dt.Load(m_db.ExecuteReader(cmdTxt));

        //if (dt.Rows.Count > 0)
        //{
        //    dt.Columns[0].Caption = "ID";


        //    e.Datasource = dt;
        //}
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

    protected void btn2_Click(object sender, EventArgs e)
    {
       
    }
    protected void btn3_Click(object sender, EventArgs e)
    {
       
    }
    protected void MyButtonClick(object sender, System.EventArgs e)
    {
      

    }

    protected void btn5_Click(object sender, EventArgs e)
    {

    }
    #endregion
}