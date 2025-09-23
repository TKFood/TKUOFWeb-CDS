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

public partial class CDS_WebPage_RESEARCH_RESEARCH_UOF_FORMS : Ede.Uof.Utility.Page.BasePage
{
    string ACCOUNTS = null;
    string NAMES = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindDropDownList1();         

            BindGrid();
        }
    }


    #region FUNCTION
    private void BindDropDownList(DropDownList ddl, string sql, string textField, string valueField)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ERPconnectionstring"].ToString();
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
        SELECT '全部' AS FORM_NAME
        UNION ALL
        SELECT  [FORM_NAME]  FROM [TKRESEARCH].[dbo].[TB_UOF_RESEARCH_FORM_NAME]
        ";

        BindDropDownList(DropDownList1, sql, "FORM_NAME", "FORM_NAME");
    }

  
    private void BindGrid()
    {
        string connectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
        Ede.Uof.Utility.Data.DatabaseHelper m_db = new Ede.Uof.Utility.Data.DatabaseHelper(connectionString);

        StringBuilder filterUser = new StringBuilder();
        StringBuilder filterForm = new StringBuilder();

        // 申請者
        string String_DropDownList1 = DropDownList1.SelectedValue;
        if (!string.IsNullOrEmpty(String_DropDownList1) && String_DropDownList1 != "全部")
        {
            filterForm.Append(" AND f.FORM_NAME=@FormName ");
            m_db.AddParameter("@FormName", String_DropDownList1);
        }

        string sql = string.Format(@"
                                    -- 20250923 查所有未結案的研發表單
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
	                                     (
                                            SELECT STUFF(
                                                (
                                                    SELECT ',' + u2.NAME
                                                    FROM [UOF].dbo.TB_WKF_TASK_NODE AS TN
                                                    LEFT JOIN [UOF].dbo.TB_EB_USER AS u2
                                                        ON u2.USER_GUID = TN.ORIGINAL_SIGNER
                                                    WHERE TN.SITE_ID <> t.CURRENT_SITE_ID
				                                    AND TN.TASK_ID = t.TASK_ID
				                                    AND ISNULL(FINISH_TIME,'')=''
				                                    ORDER BY NODE_SEQ
                                                    FOR XML PATH(''), TYPE
                                                ).value('.', 'nvarchar(max)')
                                            ,1,1,'')
                                        ) AS '下關的簽核者',
                                        CASE         
                                            WHEN f.FORM_NAME = '1001.品號申請單' 
			                                    THEN STUFF((
					                                    SELECT N',' + C.value('@fieldValue','nvarchar(200)')
					                                    FROM CURRENT_DOC.nodes('/Form/FormFieldValue/FieldItem[@fieldId=""RDFrm001IT""]/DataGrid/Row/Cell[@fieldId=""RDFrm001IN""]')  AS T(C)
					                                    FOR XML PATH(''), TYPE
				                                    ).value('.', 'nvarchar(max)'), 1, 1, '')  
		                                    WHEN f.FORM_NAME = '1002.商品變更及設計' 
                                                THEN t.CURRENT_DOC.value('(/Form/FormFieldValue/FieldItem[@fieldId=""RDFrm1002DS""]/@fieldValue)[1]', 'nvarchar(max)')  
		                                    WHEN f.FORM_NAME = '1003.BOM表變更申請單' 
                                                THEN t.CURRENT_DOC.value('(/Form/FormFieldValue/FieldItem[@fieldId=""RDFrm1003RS""]/@fieldValue)[1]', 'nvarchar(max)')  
		                                    WHEN f.FORM_NAME = '1004.無品號試吃製作申請單' 
                                                THEN t.CURRENT_DOC.value('(/Form/FormFieldValue/FieldItem[@fieldId=""DV09""]/@fieldValue)[1]', 'nvarchar(max)')  
		                                    WHEN f.FORM_NAME = '1005 研發業務工作申請單' 
                                                THEN t.CURRENT_DOC.value('(/Form/FormFieldValue/FieldItem[@fieldId=""RDFrm1003RS""]/@fieldValue)[1]', 'nvarchar(max)')  
		                                    WHEN f.FORM_NAME = '1006.樣品試吃回覆單' 
                                                THEN t.CURRENT_DOC.value('(/Form/FormFieldValue/FieldItem[@fieldId=""F02""]/@fieldValue)[1]', 'nvarchar(max)')  
		                                    WHEN f.FORM_NAME = '1007.自主研發申請書' 
                                                THEN t.CURRENT_DOC.value('(/Form/FormFieldValue/FieldItem[@fieldId=""DV09""]/@fieldValue)[1]', 'nvarchar(max)')  
		                                    WHEN f.FORM_NAME = '1008.無品號-烘培試吃製作申請單' 
                                                THEN t.CURRENT_DOC.value('(/Form/FormFieldValue/FieldItem[@fieldId=""DV09""]/@fieldValue)[1]', 'nvarchar(max)')  
		                                    WHEN f.FORM_NAME = '2001.產品開發+包裝設計申請單' 
                                                THEN t.CURRENT_DOC.value('(/Form/FormFieldValue/FieldItem[@fieldId=""FIELD3""]/@fieldValue)[1]', 'nvarchar(max)')  
		                                    WHEN f.FORM_NAME = '2001A.產品開發+包裝設計申請單(行企專用)' 
                                                THEN t.CURRENT_DOC.value('(/Form/FormFieldValue/FieldItem[@fieldId=""FIELD3""]/@fieldValue)[1]', 'nvarchar(max)')  
		                                    WHEN f.FORM_NAME = '9001.新品號通知單' 
                                                THEN t.CURRENT_DOC.value('(/Form/FormFieldValue/FieldItem[@fieldId=""MB002""]/@fieldValue)[1]', 'nvarchar(max)')  
		                                    WHEN f.FORM_NAME = '9002.品號變更通知' 
                                                THEN STUFF((
					                                    SELECT N',' + C.value('@fieldValue','nvarchar(200)')
					                                    FROM CURRENT_DOC.nodes('/Form/FormFieldValue/FieldItem[@fieldId=""DETAILS""]/DataGrid/Row/Cell[@fieldId=""TL007""]')  AS T(C)
					                                    FOR XML PATH(''), TYPE
				                                    ).value('.', 'nvarchar(max)'), 1, 1, '')  
		                                    WHEN f.FORM_NAME = '9003.新品號明細的通知' 
                                                THEN t.CURRENT_DOC.value('(/Form/FormFieldValue/FieldItem[@fieldId=""MB002""]/@fieldValue)[1]', 'nvarchar(max)')  
                                            WHEN f.FORM_NAME = '1005.舊品變更申請單'                                                        
                                                THEN t.CURRENT_DOC.value('(/Form/FormFieldValue/FieldItem[@fieldId=""RDFrm1002PD""]/@fieldValue)[1]', 'nvarchar(max)')     

                                            ELSE NULL
                                        END AS '表單標題',
                                        t.CURRENT_SITE_ID,
	                                    t.TASK_ID,
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
                                            SELECT  [FORM_NAME]
		                                    FROM [192.168.1.105].[TKRESEARCH].[dbo].[TB_UOF_RESEARCH_FORM_NAME]
	                                       )
                                    {0}
                                    ORDER BY f.FORM_NAME,t.DOC_NBR;

                                ",  filterForm);

        DataTable dt = new DataTable();
        using (IDataReader reader = m_db.ExecuteReader(sql))
        {
            dt.Load(reader);
        }

        Grid1.DataSource = dt.Rows.Count > 0 ? dt : null;
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
            string taskId = DataBinder.Eval(e.Row.DataItem, "TASK_ID") as string;
            HyperLink hlTask = (HyperLink)e.Row.FindControl("hlTask");

            if (!string.IsNullOrEmpty(taskId))
            {
                hlTask.NavigateUrl = string.Format("https://eip.tkfood.com.tw/UOF/wkf/formuse/viewform.aspx?TASK_ID={0}", taskId);
            }
            else
            {
                hlTask.Visible = false; // 或改成顯示文字 Label
            }
        }
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