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
using Ede.Uof.EIP.SystemInfo;
using Ede.Uof.Utility.Data;
using Ede.Uof.Utility.Page.Common;
using Telerik.Web.UI;

public partial class CDS_WebPage_COP_SALES_UOF_FORMS : Ede.Uof.Utility.Page.BasePage
{
    string ACCOUNTS = null;
    string NAMES = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

        }
    }


    #region FUNCTION
    private void BindGrid()
    {
        string connectionString = ConfigurationManager.ConnectionStrings["connectionstringUOF"].ToString();
        Ede.Uof.Utility.Data.DatabaseHelper m_db = new Ede.Uof.Utility.Data.DatabaseHelper(connectionString);

        StringBuilder cmdTxt = new StringBuilder();
        StringBuilder QUERYS1 = new StringBuilder();
        StringBuilder QUERYS2 = new StringBuilder();
        StringBuilder QUERYS3 = new StringBuilder();
        StringBuilder QUERYS4 = new StringBuilder();

        ////
        //if (!string.IsNullOrEmpty(TextBox1.Text) && !string.IsNullOrEmpty(TextBox1.Text))
        //{
        //    QUERYS1.AppendFormat(@" AND [SALESTO] LIKE '%{0}%'", TextBox1.Text.Trim());
        //}
        //else
        //{
        //    QUERYS1.AppendFormat(@" ");
        //}


        cmdTxt.AppendFormat(@"                            
                            SELECT 
                                f.FORM_NAME       AS '表單名稱',
                                u.NAME            AS '申請者',
                                CONVERT(nvarchar,t.BEGIN_TIME,111)      AS '申請時間',
                                t.DOC_NBR         AS '表單編號',   
                                (
                                    SELECT STUFF(
                                        (
                                            SELECT ',' + u2.NAME
                                            FROM [UOF].dbo.TB_WKF_TASK_NODE AS TN
                                            LEFT JOIN [UOF].dbo.TB_EB_USER AS u2
                                                ON u2.USER_GUID = TN.ORIGINAL_SIGNER
                                            WHERE TN.SITE_ID = t.CURRENT_SITE_ID
                                            FOR XML PATH(''), TYPE
                                        ).value('.', 'nvarchar(max)')
                                    ,1,1,'')
                                ) AS '目前簽核者',
                                CASE 
                                    WHEN f.FORM_NAME = '1001.文宣樣品申請' 
                                        THEN t.CURRENT_DOC.value('(/Form/FormFieldValue/FieldItem[@fieldId=""00010""]/@fieldValue)[1]', 'nvarchar(max)')  
                                    WHEN f.FORM_NAME = '1001.品號申請單' 
                               THEN STUFF((
    	                            SELECT N',' + C.value('@fieldValue','nvarchar(200)')
    	                            FROM CURRENT_DOC.nodes('/Form/FormFieldValue/FieldItem[@fieldId=""RDFrm001IT""]/DataGrid/Row/Cell[@fieldId=""RDFrm001IN""]') AS T(C)
    	                            FOR XML PATH(''), TYPE
                                ).value('.', 'nvarchar(max)'), 1, 1, '')           
                              WHEN f.FORM_NAME = '1001.工作交付單' 
                               THEN [UOF].dbo.StripHTML(t.CURRENT_DOC.value('(/Form/FormFieldValue/FieldItem[@fieldId=""00010""]/@fieldValue)[1]', 'nvarchar(max)'))
                                 WHEN f.FORM_NAME = '1002.客訴異常處理單' 
                                        THEN t.CURRENT_DOC.value('(/Form/FormFieldValue/FieldItem[@fieldId=""QCFrm002PRD""]/@fieldValue)[1]', 'nvarchar(max)') 
                              WHEN f.FORM_NAME = '1004.無品號試吃製作申請單' 
                               THEN STUFF((
                                SELECT N',' + C.value('@fieldValue','nvarchar(200)')
                                FROM CURRENT_DOC.nodes('/Form/FormFieldValue/FieldItem[@fieldId=""DETAILS""]/DataGrid/Row/Cell[@fieldId=""DVV01""]') AS T(C)
                                FOR XML PATH(''), TYPE
                               ).value('.', 'nvarchar(max)'), 1, 1, '')
                                    WHEN f.FORM_NAME = '1004.品保業務工作申請單' 
                                        THEN [UOF].dbo.StripHTML(t.CURRENT_DOC.value('(/Form/FormFieldValue/FieldItem[@fieldId=""QCFrm004Process""]/@fieldValue)[1]', 'nvarchar(max)'))
                              WHEN f.FORM_NAME = '1005 研發業務工作申請單' 
                               THEN  [UOF].dbo.StripHTML(t.CURRENT_DOC.value('(/Form/FormFieldValue/FieldItem[@fieldId=""RDFrm1003RS""]/@fieldValue)[1]', 'nvarchar(max)'))
                              WHEN f.FORM_NAME = '1005.舊品變更申請單' 
                                        THEN t.CURRENT_DOC.value('(/Form/FormFieldValue/FieldItem[@fieldId=""RDFrm1002PD""]/@fieldValue)[1]', 'nvarchar(max)')  
                              WHEN f.FORM_NAME = '1006.委外送驗申請單' 
                                        THEN t.CURRENT_DOC.value('(/Form/FormFieldValue/FieldItem[@fieldId=""QC6007""]/DataGrid/Row[@order=""0""]/Cell[@fieldId=""QC60071""]/@fieldValue)[1]', 'nvarchar(200)')
                              WHEN f.FORM_NAME = '1008.無品號-烘培試吃製作申請單' 
                               THEN  [UOF].dbo.StripHTML(t.CURRENT_DOC.value('(/Form/FormFieldValue/FieldItem[@fieldId=""DV09""]/@fieldValue)[1]', 'nvarchar(max)'))
                              WHEN f.FORM_NAME = '2001.產品開發+包裝設計申請單' 
                                        THEN t.CURRENT_DOC.value('(/Form/FormFieldValue/FieldItem[@fieldId=""FIELD3""]/@fieldValue)[1]', 'nvarchar(max)')  
                                    ELSE NULL
                                END AS '表單標題',
                                t.CURRENT_SITE_ID,
                                t.CURRENT_DOC
                            FROM [UOF].dbo.TB_WKF_TASK AS t WITH (NOLOCK)
                            LEFT JOIN [UOF].dbo.TB_EB_USER AS u
                                ON u.USER_GUID = t.USER_GUID
                            LEFT JOIN [UOF].dbo.TB_EB_EMPL_DEP AS ed
                                ON ed.USER_GUID = u.USER_GUID AND ed.ORDERS = '0'
                            JOIN [UOF].dbo.TB_WKF_FORM_VERSION AS fv
                                ON t.FORM_VERSION_ID = fv.FORM_VERSION_ID
                            JOIN [UOF].dbo.TB_WKF_FORM AS f
                                ON f.FORM_ID = fv.FORM_ID
                            WHERE 1=1
                              AND ISNULL(t.TASK_STATUS,'') NOT IN ('2','3')
                              AND t.BEGIN_TIME >= '2025-01-01'
                              AND f.FORM_NAME COLLATE Chinese_Taiwan_Stroke_BIN IN (
                                    SELECT [FORM_NAME]
                                    FROM [192.168.1.105].[TKMQ].[dbo].[TB_UOF_SALES_FORM_NAME]
                              )
                              AND u.ACCOUNT COLLATE Chinese_Taiwan_Stroke_BIN IN (
                                    SELECT [ID]
                                    FROM [192.168.1.105].[TKMQ].[dbo].[TB_UOF_SALES_NAMES]
                              )
                            ORDER BY f.FORM_NAME, u.NAME,t.DOC_NBR;

                        ", QUERYS1.ToString(), QUERYS2.ToString(), QUERYS3.ToString(), QUERYS4.ToString());


        //m_db.AddParameter("@SDATE", SDATE);
        //m_db.AddParameter("@EDATE", EDATE);

        DataTable dt = new DataTable();

        dt.Load(m_db.ExecuteReader(cmdTxt.ToString()));

        Grid1.DataSource = dt;
        Grid1.DataBind();

        MsgBox("完成 \r\n", this.Page, this);
    }

    protected void grid1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //Grid1.PageIndex = e.NewPageIndex;
        //BindGrid();
    }
    protected void Grid1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    protected void Grid1_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        int rowIndex = -1;
    }

    public void OnBeforeExport1(object sender, Ede.Uof.Utility.Component.BeforeExportEventArgs e)
    {

    }
    public void MsgBox(string ex, Page pg, object obj)
    {
        string script = "alert('" + ex.Replace("\r\n", "\\n").Replace("'", "") + "');";
        ScriptManager.RegisterStartupScript(pg, obj.GetType(), "AlertScript", script, true);
    }

    #endregion

    #region BUTTON
    protected void Button1_Click(object sender, EventArgs e)
    {
        BindGrid();
    }
    #endregion
}