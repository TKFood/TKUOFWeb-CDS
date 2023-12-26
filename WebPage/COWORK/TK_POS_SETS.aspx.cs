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
using System.Xml;
using System.Xml.Linq;
using Ede.Uof.EIP.SystemInfo;
using Ede.Uof.Utility.Data;
using Ede.Uof.Utility.Log;
using Ede.Uof.Utility.Page.Common;
using OfficeOpenXml;
using OfficeOpenXml.Drawing;
using OfficeOpenXml.Style;

public partial class CDS_WebPage_COWORK_TK_POS_SETS : Ede.Uof.Utility.Page.BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string ACCOUNT = Current.Account;
        string NAME = Current.User.Name;

        if (!IsPostBack)
        {
            SETDATES();
            BindDropDownList();
            BindGrid();
        }
    }
    #region FUNCTION
    public void SETDATES()
    {
        TextBox1.Text = DateTime.Now.ToString("yyyy");       

    }
    private void BindDropDownList()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("STATUS", typeof(String));


        string connectionString = ConfigurationManager.ConnectionStrings["ERPconnectionstring"].ToString();
        Ede.Uof.Utility.Data.DatabaseHelper m_db = new Ede.Uof.Utility.Data.DatabaseHelper(connectionString);

        string cmdTxt = @" SELECT '未核單' AS 'STATUS' UNION ALL SELECT '已核單' AS 'STATUS' ";

        dt.Load(m_db.ExecuteReader(cmdTxt));

        if (dt.Rows.Count > 0)
        {
            DropDownList1.DataSource = dt;
            DropDownList1.DataTextField = "STATUS";
            DropDownList1.DataValueField = "STATUS";
            DropDownList1.DataBind();

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

        //日期
        if (!string.IsNullOrEmpty(TextBox1.Text))
        {
            QUERYS.AppendFormat(@" AND MB001 LIKE '{0}%' ", TextBox1.Text.Trim());
        }

        //核單
        if (!string.IsNullOrEmpty(DropDownList1.Text))
        {
            if (DropDownList1.Text.Equals("未核單"))
            {
                QUERYS2.AppendFormat(@" AND MB008='N' ");
            }
            else if (DropDownList1.Text.Equals("已核單"))
            {
                QUERYS2.AppendFormat(@"  AND MB008='Y' ");
            }
        }
        //特價名稱
        if (!string.IsNullOrEmpty(TextBox2.Text))
        {
            QUERYS3.AppendFormat(@" AND MB004 LIKE '{0}%' ", TextBox2.Text.Trim());
        }




        cmdTxt.AppendFormat(@" 
                            SELECT *
                            ,STUFF((
                                       SELECT  LTRIM(RTRIM(MF004))+LTRIM(RTRIM(MA002))+ CHAR(13) + CHAR(10) 
                                       FROM [TK].dbo.POSMF,[TK].dbo.WSCMA
                                       WHERE MF004=MA001 AND MF003 = MB003
                                       FOR XML PATH('')), 1, 1, '1') AS All_MF004
                            ,STUFF((
                                       SELECT  LTRIM(RTRIM(NI002))+ CHAR(13) + CHAR(10) 
                                       FROM [TK].dbo.POSMG,[TK].dbo.WSCNI
                                       WHERE MG005=NI001 AND MG003 = MB003
                                       FOR XML PATH('')), 1, 1, '1') AS All_NI002
                            ,STUFF((
                                        SELECT  LTRIM(RTRIM(MC004))+ CHAR(13) + CHAR(10) +LTRIM(RTRIM(MB002))+ CHAR(13) + CHAR(10) +'非會員特價'+CONVERT(NVARCHAR,CONVERT(INT,MC005))+ CHAR(13) + CHAR(10) +' 會員特價'+CONVERT(NVARCHAR,CONVERT(INT,MC006))+ CHAR(13) + CHAR(10) + CHAR(13) + CHAR(10) 
                                        FROM [TK].dbo.POSMC,[TK].dbo.INVMB
                                        WHERE MC004=INVMB.MB001 AND MC003 = POSMB.MB003
                                        FOR XML PATH('')), 1, 1, '1') AS All_MC004

                            FROM [TK].dbo.POSMB
                            WHERE 1=1                            
                            {0}
                            {1}   
                            {2}    

                                ", QUERYS.ToString(), QUERYS2.ToString(), QUERYS3.ToString());



        DataTable dt = new DataTable();

        dt.Load(m_db.ExecuteReader(cmdTxt.ToString()));



        Grid1.DataSource = dt;
        Grid1.DataBind();
    }

    protected void grid_PageIndexChanging1(object sender, GridViewPageEventArgs e)
    {
       
    }
    protected void Grid1_RowDataBound(object sender, GridViewRowEventArgs e)
    {        

    }

    protected void Grid1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int rowIndex = -1;

        if (e.CommandName == "Button1")
        {           
            BindGrid();
        }
        else if (e.CommandName == "Button2")
        {
        }

    }


    public void OnBeforeExport1(object sender, Ede.Uof.Utility.Component.BeforeExportEventArgs e)
    {
        //SETEXCEL();

    }

    #endregion

    #region BUTTON
    protected void Button1_Click(object sender, EventArgs e)
    {
        BindGrid();       

    }

    #endregion
}