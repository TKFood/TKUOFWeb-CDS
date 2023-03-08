using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CDS_WebPart_UC_TB_EIP_SCH_WORK : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindDropDownList();

            BindGrid2("");

            DateTime FirstDay = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DateTime LastDay = new DateTime(DateTime.Now.AddMonths(1).Year, DateTime.Now.AddMonths(1).Month, 1).AddDays(-1);
         //   DateTime.DaysInMonth()
            BindGrid(FirstDay.ToString("yyyy/MM/dd"), LastDay.ToString("yyyy/MM/dd"));

            txtDate1.SelectedDate = FirstDay;
            txtDate2.SelectedDate = LastDay;
        }
        //else
        //{
        //    BindGrid(txtDate1.SelectedDate.Value.ToString("yyyy/MM/dd"), txtDate2.SelectedDate.Value.ToString("yyyy/MM/dd"));
        //}

           
    }
    #region FUNCTION
    private void BindDropDownList()
    {
        DataTable dt = new DataTable();
        DataRow row;
        dt.Columns.Add("KINDS", typeof(String));

        row = dt.NewRow();
        row["KINDS"] = "N";
        dt.Rows.Add(row);

        row = dt.NewRow();
        row["KINDS"] = "Y";
        dt.Rows.Add(row);



        //string connectionString = ConfigurationManager.ConnectionStrings["ERPconnectionstring"].ToString();
        //Ede.Uof.Utility.Data.DatabaseHelper m_db = new Ede.Uof.Utility.Data.DatabaseHelper(connectionString);

        //string cmdTxt = @" SELECT '全部' AS SALESFOCUS UNION ALL SELECT SALESFOCUS  FROM  [TKBUSINESS].[dbo].[PRODUCTS]  GROUP BY SALESFOCUS ";

        //dt.Load(m_db.ExecuteReader(cmdTxt));

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

    private void BindGrid(string SDATE, string EDATE)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
        Ede.Uof.Utility.Data.DatabaseHelper m_db = new Ede.Uof.Utility.Data.DatabaseHelper(connectionString);

        string cmdTxt = @"
                        SELECT 
                        USER1.[NAME] AS 'NAME1',REPLACE([TB_EIP_SCH_WORK].[SUBJECT],char(10),'<br/>') AS   SUBJECT
                        ,(SELECT TOP 1 ISNULL([DESCRIPTION],'') FROM [UOF].[dbo].[TB_EIP_SCH_WORK_RECORD] WHERE [TB_EIP_SCH_WORK_RECORD].[WORK_GUID]=[TB_EIP_SCH_WORK].[WORK_GUID] ORDER BY CREATE_TIME DESC) AS 'DESCRIPTION'
                        ,CONVERT(NVARCHAR,[TB_EIP_SCH_WORK].[END_TIME],111)  AS END_TIME,DATEDIFF(day, [TB_EIP_SCH_WORK].[END_TIME],GETDATE()) AS DIFFDATES,USER2.[NAME]  AS 'NAME2'
                        ,CASE WHEN [TB_EIP_SCH_WORK].[WORK_STATE]='NotYetBegin' THEN '當未開始' ELSE '進行中' END AS 'STATUS'
                        ,[TB_EIP_SCH_WORK].[WORK_STATE],[TB_EIP_SCH_WORK].[EXECUTE_USER],[TB_EIP_SCH_WORK].[SOURCE_USER]
                        ,[TB_EIP_SCH_WORK].WORK_GUID
                        FROM [UOF].[dbo].[TB_EIP_SCH_WORK]
                        LEFT JOIN [UOF].[dbo].[TB_EB_USER] USER1 ON USER1.USER_GUID=[TB_EIP_SCH_WORK].[EXECUTE_USER]
                        LEFT JOIN [UOF].[dbo].[TB_EB_USER] USER2 ON USER2.USER_GUID=[TB_EIP_SCH_WORK].[SOURCE_USER]
                        WHERE [WORK_STATE] IN ('NotYetBegin','Proceeding','Audit')
                        AND USER1.[NAME] IN ('洪櫻芬','王琇平','葉枋俐','何姍怡','林琪琪','林杏育','張釋予','蔡顏鴻','陳帟靜','黃鈺涵')
                        AND CONVERT(NVARCHAR,[TB_EIP_SCH_WORK].[END_TIME],111) >=@SDATE AND CONVERT(NVARCHAR,[TB_EIP_SCH_WORK].[END_TIME],111) <=@EDATE
                        ORDER BY [EXECUTE_USER],[TB_EIP_SCH_WORK].[END_TIME],[SUBJECT]

                        ";

        //string cmdTxt = @"
        //                SELECT TOP 1 
        //                'NAME1'AS 'NAME1','SUBJECT' AS SUBJECT
        //                ,'DESCRIPTION' AS 'DESCRIPTION'
        //                ,''  AS END_TIME
        //                ,''  AS DIFFDATES
        //                ,'NAME2'  AS 'NAME2'
        //                ,'STATUS' AS 'STATUS'
        //                ,'1' 'WORK_STATE','' 'EXECUTE_USER','' 'SOURCE_USER'
        //                ,'' 'WORK_GUID'
        //                FROM [UOF].[dbo].TB_DMS_AGENCY_SUBSCRIBE

        //                ";

        m_db.AddParameter("@SDATE", SDATE);
        m_db.AddParameter("@EDATE", EDATE);

        DataTable dt = new DataTable();

        dt.Load(m_db.ExecuteReader(cmdTxt));

        Grid1.DataSource = dt;
        Grid1.DataBind();
    }



   
    protected void grid1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
       
    }



  
    protected void Grid1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
       

    }

   

    public void OnBeforeExport1(object sender, Ede.Uof.Utility.Component.BeforeExportEventArgs e)
    {
        
    }

    private void BindGrid2(string SALESFOCUS)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
        Ede.Uof.Utility.Data.DatabaseHelper m_db = new Ede.Uof.Utility.Data.DatabaseHelper(connectionString);

        StringBuilder cmdTxt = new StringBuilder();
        StringBuilder QUERYS = new StringBuilder();



        //狀態
        if (!string.IsNullOrEmpty(DropDownList1.Text))
        {
            if (DropDownList1.Text.Equals("Y"))
            {
                QUERYS.AppendFormat(@" AND WORK_STATE  IN ('Completed') ");
            }
            else if (DropDownList1.Text.Equals("N"))
            {
                QUERYS.AppendFormat(@"  AND WORK_STATE NOT IN ('Completed') ");
            }
        }

        //校稿名稱
        if (!string.IsNullOrEmpty(TextBox1.Text))
        {
            QUERYS.AppendFormat(@" AND SUBJECT LIKE  '%'+@SUBJECT+'%' ");

        }

        //執行者
        if (!string.IsNullOrEmpty(TextBox2.Text))
        {
            QUERYS.AppendFormat(@" AND USER2.NAME LIKE  '%{0}%' ", TextBox2.Text);

        }

        //交付者
        if (!string.IsNullOrEmpty(TextBox3.Text))
        {
            QUERYS.AppendFormat(@" AND USER1.NAME LIKE  '%{0}%' ", TextBox3.Text);

        }

        cmdTxt.AppendFormat(@" 
                            SELECT SUBJECT,CASE WHEN WORK_STATE='Completed' THEN '已完成'  WHEN WORK_STATE='NotYetBegin' THEN '尚未開始' WHEN WORK_STATE='Audit' THEN '交付人審查中' WHEN WORK_STATE='Proceeding' THEN '進行中' ELSE WORK_STATE END  WORK_STATE
                            ,USER1.NAME AS '交付者',USER2.NAME AS '執行者',CONVERT(nvarchar,CREATE_TIME,112 ) CREATE_TIME,CONVERT(nvarchar,END_TIME,112 ) END_TIME
                            ,(SELECT TOP 1 DESCRIPTION FROM  [UOF].dbo.TB_EIP_SCH_WORK_RECORD WHERE TB_EIP_SCH_WORK_RECORD.WORK_GUID=TB_EIP_SCH_WORK.WORK_GUID ORDER BY CREATE_TIME DESC) AS 'DESCRIPTION'
                            ,WORK_GUID,CREATE_USER,EXECUTE_USER
                            FROM [UOF].dbo.TB_EIP_SCH_WORK
                            LEFT JOIN [UOF].dbo.TB_EB_USER USER1 ON USER1.USER_GUID=CREATE_USER
                            LEFT JOIN [UOF].dbo.TB_EB_USER USER2 ON USER2.USER_GUID=EXECUTE_USER
                            WHERE 1=1
                                {0}
                            ORDER BY SUBJECT,CREATE_TIME DESC
                             
                              
                                ", QUERYS.ToString());




        //m_db.AddParameter("@SDATE", SDATE);
        //m_db.AddParameter("@EDATE", EDATE);

        m_db.AddParameter("@SUBJECT", TextBox1.Text);

        DataTable dt = new DataTable();

        dt.Load(m_db.ExecuteReader(cmdTxt.ToString()));

        Grid2.DataSource = dt;
        Grid2.DataBind();
    }

    protected void grid2_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
    protected void Grid2_RowDataBound(object sender, GridViewRowEventArgs e)
    {



    }

    public void OnBeforeExport2(object sender, Ede.Uof.Utility.Component.BeforeExportEventArgs e)
    {

    }
    #endregion

    #region BUTTON
    protected void btn_Click(object sender, EventArgs e)
    {

    }
    protected void btn1_Click(object sender, EventArgs e)
    {
        BindGrid(txtDate1.SelectedDate.Value.ToString("yyyy/MM/dd"), txtDate2.SelectedDate.Value.ToString("yyyy/MM/dd"));
    }

    protected void btn5_Click(object sender, EventArgs e)
    {
        BindGrid2("");


        //if (!string.IsNullOrEmpty(Dialog.GetReturnValue()))
        //{
        //    if (Dialog.GetReturnValue().Equals("NeedPostBack"))
        //    {

        //    }

        //}
    }

    #endregion
}