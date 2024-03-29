﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Dynamic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security.AntiXss;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ede.Uof.Utility.Data;
using Ede.Uof.Utility.Page.Common;

public partial class CDS_WebPage_Mobile_Mobile_TB_EIP_SCH_WORK : Ede.Uof.Utility.Page.BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {

        //不顯示子視窗的按鈕
        ((Master_MobileMasterPage)this.Master).Button1Text = string.Empty;
        ((Master_MobileMasterPage)this.Master).Button2Text = string.Empty;
        ((Master_MobileMasterPage)this.Master).Button3Text = string.Empty;

        if (!IsPostBack)
        {
            DateTime FirstDay = DateTime.Now.AddDays(-DateTime.Now.Day + 1);
            DateTime LastDay = DateTime.Now.AddDays(1 - DateTime.Now.Day).AddMonths(1).AddDays(-1);

            BindGrid(FirstDay.ToString("yyyy/MM/dd"), LastDay.ToString("yyyy/MM/dd"));
            BindGrid2(FirstDay.ToString("yyyy/MM/dd"), LastDay.ToString("yyyy/MM/dd"));

            txtDate1.Text = FirstDay.ToString("yyyy/MM/dd");
            txtDate2.Text = LastDay.ToString("yyyy/MM/dd");
        }
      
    }


    #region FUNCTION

    private void BindGrid(string SDATE, string EDATE)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
        Ede.Uof.Utility.Data.DatabaseHelper m_db = new Ede.Uof.Utility.Data.DatabaseHelper(connectionString);

        string cmdTxt = @"
                        SELECT [TB_EB_USER].[USER_GUID],[tb_USER].[USER_ID],[ACCOUNT],[NAME],CONVERT(NVARCHAR,[START_TIME],111) AS START_TIME,SUBJECT,DESCRIPTION
                        FROM [UOF].[dbo].[TB_EB_USER],[UOF].dbo.[TB_EIP_SCH_WORK],[HJ_BM_DB].[dbo].[tb_USER]
                        WHERE [TB_EB_USER].[USER_GUID]=[TB_EIP_SCH_WORK].EXECUTE_USER
                        AND [tb_USER].[USER_GUID]=[TB_EB_USER].[USER_GUID]
                        AND [SOURCE_TYPE]='Self'
                        AND [NAME] IN ('洪櫻芬','王琇平','葉枋俐','何姍怡','林琪琪','林杏育','張釋予','蔡顏鴻','陳帟靜','黃鈺涵')
                        AND CONVERT(NVARCHAR,[START_TIME],111)>=@SDATE AND CONVERT(NVARCHAR,[START_TIME],111)<=@EDATE
                        ORDER BY [NAME],CONVERT(NVARCHAR,[START_TIME],112)

                        ";

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

    private void BindGrid2(string SDATE, string EDATE)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
        Ede.Uof.Utility.Data.DatabaseHelper m_db = new Ede.Uof.Utility.Data.DatabaseHelper(connectionString);

        string cmdTxt = @"
                        SELECT 
                        USER1.[NAME] AS 'NAME1',REPLACE([TB_EIP_SCH_WORK].[SUBJECT],char(10),'<br/>')  SUBJECT
                        ,(SELECT TOP 1 ISNULL([DESCRIPTION],'') FROM [UOF].[dbo].[TB_EIP_SCH_WORK_RECORD] WHERE [TB_EIP_SCH_WORK_RECORD].[WORK_GUID]=[TB_EIP_SCH_WORK].[WORK_GUID] ORDER BY CREATE_TIME DESC) AS 'DESCRIPTION'
                        ,CONVERT(NVARCHAR,[TB_EIP_SCH_WORK].[END_TIME],111)  AS END_TIME,DATEDIFF(day, [TB_EIP_SCH_WORK].[END_TIME],GETDATE()) AS DIFFDATES,USER2.[NAME]  AS 'NAME2'
                        ,CASE WHEN [TB_EIP_SCH_WORK].[WORK_STATE]='NotYetBegin' THEN '當未開始' ELSE '進行中' END AS 'STATUS'
                        ,[TB_EIP_SCH_WORK].[WORK_STATE],[TB_EIP_SCH_WORK].[EXECUTE_USER],[TB_EIP_SCH_WORK].[SOURCE_USER]
                        ,[TB_EIP_SCH_WORK].WORK_GUID
                        FROM [UOF].[dbo].[TB_EIP_SCH_WORK]
                        LEFT JOIN [UOF].[dbo].[TB_EB_USER] USER1 ON USER1.USER_GUID=[TB_EIP_SCH_WORK].[EXECUTE_USER]
                        LEFT JOIN [UOF].[dbo].[TB_EB_USER] USER2 ON USER2.USER_GUID=[TB_EIP_SCH_WORK].[SOURCE_USER]
                        WHERE [WORK_STATE] IN ('NotYetBegin','Proceeding')
                        AND EXECUTE_USER IN 
                        (
                        SELECT
                        UserId.value('(.)', 'nvarchar(50)') AS UserId
                        FROM
                        [UOF].dbo.TB_EIP_SCH_DEVOLVE
                        CROSS APPLY
                        USER_SET.nodes('/UserSet/Element') AS UserSet(Element)
                        CROSS APPLY
                        Element.nodes('userId') AS UserId(UserId)
                        WHERE
                        TB_EIP_SCH_DEVOLVE.DEVOLVE_GUID=TB_EIP_SCH_WORK.DEVOLVE_GUID
                        )

                        AND USER1.[NAME] IN (SELECT [NAMES] FROM [UOF].[dbo].[Z_SALES_NAMES])
                        AND CONVERT(NVARCHAR,[TB_EIP_SCH_WORK].[END_TIME],111) >=@SDATE AND CONVERT(NVARCHAR,[TB_EIP_SCH_WORK].[END_TIME],111) <=@EDATE
                        ORDER BY [EXECUTE_USER],[TB_EIP_SCH_WORK].[END_TIME],[SUBJECT]

                        ";

        m_db.AddParameter("@SDATE", SDATE);
        m_db.AddParameter("@EDATE", EDATE);

        DataTable dt = new DataTable();

        dt.Load(m_db.ExecuteReader(cmdTxt));

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

    protected void MyButtonClick(object sender, System.EventArgs e)
    {
        //Get the button that raised the event
        Button btn = (Button)sender;

        //Get the row that contains this button
        GridViewRow gvr = (GridViewRow)btn.NamingContainer;

        //string cellvalue = gvr.Cells[2].Text.Trim();
        string Cellvalue = btn.CommandArgument;

        Label3.Text = Cellvalue;

        UPDATETB_EIP_SCH_WORK(Cellvalue);

        BindGrid2(txtDate1.Text, txtDate2.Text);
    }

    public void UPDATETB_EIP_SCH_WORK(string WORK_GUID)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();

        var SQLCOMMAND = @"
                            UPDATE [UOF].[dbo].[TB_EIP_SCH_WORK]
                            SET [WORK_STATE]='Completed'
                            WHERE WORK_GUID=@WORK_GUID    
                            ";

        try
        {
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(SQLCOMMAND, cnn))
                {

                    cmd.Parameters.AddWithValue("@WORK_GUID", WORK_GUID);

                    cnn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();


                    if (rowsAffected >= 1)
                    {
                        Label3.Text = "完成";
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

    #endregion

    #region BUTTON
    protected void btn1_Click(object sender, EventArgs e)
    {
        BindGrid(txtDate1.Text, txtDate2.Text);
        BindGrid2(txtDate1.Text, txtDate2.Text);
    }



    #endregion
}