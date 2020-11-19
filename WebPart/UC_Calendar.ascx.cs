using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CDS_WebPart_UC_Calendar : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindGrid(DateTime.Now.ToString("yyyy/MM/dd"), DateTime.Now.AddDays(7).ToString("yyyy/MM/dd"));

            txtDate1.Text = DateTime.Now.ToString("yyyy/MM/dd");
            txtDate2.Text = DateTime.Now.AddDays(7).ToString("yyyy/MM/dd");
        }
        else
        {
            BindGrid(txtDate1.Text, txtDate2.Text);
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
    }

    #endregion
}