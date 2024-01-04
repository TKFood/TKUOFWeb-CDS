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
    string MB003 = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        string ACCOUNT = Current.Account;
        string NAME = Current.User.Name;

        if (!IsPostBack)
        {
            SETDATES();
            BindDropDownList();
            BindDropDownList2();
            BindDropDownList3();
            BindDropDownList4();
            BindDropDownList5();
            BindDropDownList6();
            BindDropDownList7();

            BindGrid();
            BindGrid2();
            BindGrid3();
            BindGrid4();
            BindGrid5();
        }
    }
    #region FUNCTION
    public void SETDATES()
    {
        TextBox1.Text = DateTime.Now.ToString("yyyy");
        TextBox3.Text = DateTime.Now.ToString("yyyy");
        TextBox5.Text = DateTime.Now.ToString("yyyy");
        TextBox7.Text = DateTime.Now.ToString("yyyy");
        TextBox9.Text = DateTime.Now.ToString("yyyy");
        TextBox11.Text = DateTime.Now.ToString("yyyy");
        TextBox13.Text = DateTime.Now.ToString("yyyy");
    

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
    private void BindDropDownList2()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("STATUS", typeof(String));


        string connectionString = ConfigurationManager.ConnectionStrings["ERPconnectionstring"].ToString();
        Ede.Uof.Utility.Data.DatabaseHelper m_db = new Ede.Uof.Utility.Data.DatabaseHelper(connectionString);

        string cmdTxt = @" SELECT '未核單' AS 'STATUS' UNION ALL SELECT '已核單' AS 'STATUS' ";

        dt.Load(m_db.ExecuteReader(cmdTxt));

        if (dt.Rows.Count > 0)
        {
            DropDownList2.DataSource = dt;
            DropDownList2.DataTextField = "STATUS";
            DropDownList2.DataValueField = "STATUS";
            DropDownList2.DataBind();

        }
        else
        {

        }



    }
    private void BindDropDownList3()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("STATUS", typeof(String));


        string connectionString = ConfigurationManager.ConnectionStrings["ERPconnectionstring"].ToString();
        Ede.Uof.Utility.Data.DatabaseHelper m_db = new Ede.Uof.Utility.Data.DatabaseHelper(connectionString);

        string cmdTxt = @" SELECT '未核單' AS 'STATUS' UNION ALL SELECT '已核單' AS 'STATUS' ";

        dt.Load(m_db.ExecuteReader(cmdTxt));

        if (dt.Rows.Count > 0)
        {
            DropDownList3.DataSource = dt;
            DropDownList3.DataTextField = "STATUS";
            DropDownList3.DataValueField = "STATUS";
            DropDownList3.DataBind();

        }
        else
        {

        }



    }
    private void BindDropDownList4()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("STATUS", typeof(String));


        string connectionString = ConfigurationManager.ConnectionStrings["ERPconnectionstring"].ToString();
        Ede.Uof.Utility.Data.DatabaseHelper m_db = new Ede.Uof.Utility.Data.DatabaseHelper(connectionString);

        string cmdTxt = @" SELECT '未核單' AS 'STATUS' UNION ALL SELECT '已核單' AS 'STATUS' ";

        dt.Load(m_db.ExecuteReader(cmdTxt));

        if (dt.Rows.Count > 0)
        {
            DropDownList4.DataSource = dt;
            DropDownList4.DataTextField = "STATUS";
            DropDownList4.DataValueField = "STATUS";
            DropDownList4.DataBind();

        }
        else
        {

        }



    }
    private void BindDropDownList5()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("STATUS", typeof(String));


        string connectionString = ConfigurationManager.ConnectionStrings["ERPconnectionstring"].ToString();
        Ede.Uof.Utility.Data.DatabaseHelper m_db = new Ede.Uof.Utility.Data.DatabaseHelper(connectionString);

        string cmdTxt = @" SELECT '未核單' AS 'STATUS' UNION ALL SELECT '已核單' AS 'STATUS' ";

        dt.Load(m_db.ExecuteReader(cmdTxt));

        if (dt.Rows.Count > 0)
        {
            DropDownList5.DataSource = dt;
            DropDownList5.DataTextField = "STATUS";
            DropDownList5.DataValueField = "STATUS";
            DropDownList5.DataBind();

        }
        else
        {

        }



    }
    private void BindDropDownList6()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("STATUS", typeof(String));


        string connectionString = ConfigurationManager.ConnectionStrings["ERPconnectionstring"].ToString();
        Ede.Uof.Utility.Data.DatabaseHelper m_db = new Ede.Uof.Utility.Data.DatabaseHelper(connectionString);

        string cmdTxt = @" SELECT '未核單' AS 'STATUS' UNION ALL SELECT '已核單' AS 'STATUS' ";

        dt.Load(m_db.ExecuteReader(cmdTxt));

        if (dt.Rows.Count > 0)
        {
            DropDownList6.DataSource = dt;
            DropDownList6.DataTextField = "STATUS";
            DropDownList6.DataValueField = "STATUS";
            DropDownList6.DataBind();

        }
        else
        {

        }



    }
    private void BindDropDownList7()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("STATUS", typeof(String));


        string connectionString = ConfigurationManager.ConnectionStrings["ERPconnectionstring"].ToString();
        Ede.Uof.Utility.Data.DatabaseHelper m_db = new Ede.Uof.Utility.Data.DatabaseHelper(connectionString);

        string cmdTxt = @" SELECT '未核單' AS 'STATUS' UNION ALL SELECT '已核單' AS 'STATUS' ";

        dt.Load(m_db.ExecuteReader(cmdTxt));

        if (dt.Rows.Count > 0)
        {
            DropDownList7.DataSource = dt;
            DropDownList7.DataTextField = "STATUS";
            DropDownList7.DataValueField = "STATUS";
            DropDownList7.DataBind();

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
            QUERYS3.AppendFormat(@" AND (MB004 LIKE '%{0}%' OR MB003 LIKE '%{0}%') ", TextBox2.Text.Trim());
        }




        cmdTxt.AppendFormat(@" 
                            SELECT *
                            ,(
                                       SELECT  LTRIM(RTRIM(MF004))+LTRIM(RTRIM(MA002))+ CHAR(13) + CHAR(10) 
                                       FROM [TK].dbo.POSMF,[TK].dbo.WSCMA
                                       WHERE MF004=MA001 AND MF003 = MB003
                                       FOR XML PATH('')) AS All_MF004
                            ,(
                                       SELECT  LTRIM(RTRIM(NI002))+ CHAR(13) + CHAR(10) 
                                       FROM [TK].dbo.POSMG,[TK].dbo.WSCNI
                                       WHERE MG005=NI001 AND MG003 = MB003
                                       FOR XML PATH('')) AS All_NI002
                            ,(
                                        SELECT  LTRIM(RTRIM(MC004))+ CHAR(13) + CHAR(10) +LTRIM(RTRIM(MB002))+ CHAR(13) + CHAR(10) +'非會員特價'+CONVERT(NVARCHAR,CONVERT(INT,MC005))+ CHAR(13) + CHAR(10) +' 會員特價'+CONVERT(NVARCHAR,CONVERT(INT,MC006))+ CHAR(13) + CHAR(10) + CHAR(13) + CHAR(10) 
                                        FROM [TK].dbo.POSMC,[TK].dbo.INVMB
                                        WHERE MC004=INVMB.MB001 AND MC003 = POSMB.MB003
                                        FOR XML PATH('')) AS All_MC004
                            ,(MB012+'~'+MB013) AS 'MB012MB013'

                            FROM [TK].dbo.POSMB
                            WHERE 1=1  
                            AND MB002 IN ('1')
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
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //Grid1_Button1
            //Get the button that raised the event
            Button Grid1_Button1 = (Button)e.Row.FindControl("Grid1_Button1");
            //Get the row that contains this button
            GridViewRow gvr1 = (GridViewRow)Grid1_Button1.NamingContainer;
            //string cellvalue = gvr.Cells[2].Text.Trim();
            string Cellvalue1 = Grid1_Button1.CommandArgument;
            DataRowView row1 = (DataRowView)e.Row.DataItem;
            Button lbtnName1 = (Button)e.Row.FindControl("Grid1_Button1");
            ExpandoObject param1 = new { ID = Cellvalue1 }.ToExpando();

        }
    }

    protected void Grid1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int rowIndex = -1;

        if (e.CommandName == "Grid1_Button1")
        {
            //MsgBox(e.CommandArgument.ToString() + "", this.Page, this);

            MB003 = e.CommandArgument.ToString();      

            ADDTB_WKF_EXTERNAL_TASK_POSSET("商品特價折扣",MB003);
        }

    }


    public void OnBeforeExport1(object sender, Ede.Uof.Utility.Component.BeforeExportEventArgs e)
    {
        //SETEXCEL();

    }

    private void BindGrid2()
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ERPconnectionstring"].ToString();
        Ede.Uof.Utility.Data.DatabaseHelper m_db = new Ede.Uof.Utility.Data.DatabaseHelper(connectionString);

        StringBuilder cmdTxt = new StringBuilder();
        StringBuilder QUERYS = new StringBuilder();
        StringBuilder QUERYS2 = new StringBuilder();
        StringBuilder QUERYS3 = new StringBuilder();

        //日期
        if (!string.IsNullOrEmpty(TextBox3.Text))
        {
            QUERYS.AppendFormat(@" AND MB001 LIKE '{0}%' ", TextBox3.Text.Trim());
        }

        //核單
        if (!string.IsNullOrEmpty(DropDownList2.Text))
        {
            if (DropDownList2.Text.Equals("未核單"))
            {
                QUERYS2.AppendFormat(@" AND MB008='N' ");
            }
            else if (DropDownList2.Text.Equals("已核單"))
            {
                QUERYS2.AppendFormat(@"  AND MB008='Y' ");
            }
        }
        //特價名稱
        if (!string.IsNullOrEmpty(TextBox4.Text))
        {
            QUERYS3.AppendFormat(@" AND (MB004 LIKE '%{0}%' OR MB003 LIKE '%{0}%') ", TextBox4.Text.Trim());
        }




        cmdTxt.AppendFormat(@" 
                           SELECT *
                            ,(
                                        SELECT  LTRIM(RTRIM(MF004))+LTRIM(RTRIM(MA002))+ CHAR(13) + CHAR(10) 
                                        FROM [TK].dbo.POSMF,[TK].dbo.WSCMA
                                        WHERE MF004=MA001 AND MF003 = MB003
                                        FOR XML PATH('')) AS All_MF004
                            ,(
                                        SELECT  LTRIM(RTRIM(NI002))+ CHAR(13) + CHAR(10) 
                                        FROM [TK].dbo.POSMG,[TK].dbo.WSCNI
                                        WHERE MG005=NI001 AND MG003 = MB003
                                        FOR XML PATH('')) AS All_NI002
                            ,(
                                        SELECT  LTRIM(RTRIM(MD004))+ CHAR(13) + CHAR(10) +LTRIM(RTRIM(MA003))+ CHAR(13) + CHAR(10) +'非會員特價'+CONVERT(NVARCHAR,CONVERT(INT,MD005))+ CHAR(13) + CHAR(10) +' 會員特價'+CONVERT(NVARCHAR,CONVERT(INT,MD006))+ CHAR(13) + CHAR(10) + CHAR(13) + CHAR(10) 
                                        FROM [TK].dbo.POSMD,[TK].dbo.INVMA
                                        WHERE MD004=INVMA.MA002 AND INVMA.MA001='3' AND MD003 = POSMB.MB003
                                        FOR XML PATH('')) AS All_MC004
                            ,(MB012+'~'+MB013) AS 'MB012MB013'

                            FROM [TK].dbo.POSMB
                            WHERE 1=1  
                            AND MB002 IN ('2')
                            {0}
                            {1}   
                            {2}    

                                ", QUERYS.ToString(), QUERYS2.ToString(), QUERYS3.ToString());



        DataTable dt = new DataTable();

        dt.Load(m_db.ExecuteReader(cmdTxt.ToString()));



        Grid2.DataSource = dt;
        Grid2.DataBind();
    }

    protected void grid_PageIndexChanging2(object sender, GridViewPageEventArgs e)
    {

    }
    protected void Grid2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //Grid1_Button1
            //Get the button that raised the event
            Button Grid1_Button1 = (Button)e.Row.FindControl("Grid2_Button1");
            //Get the row that contains this button
            GridViewRow gvr1 = (GridViewRow)Grid1_Button1.NamingContainer;
            //string cellvalue = gvr.Cells[2].Text.Trim();
            string Cellvalue1 = Grid1_Button1.CommandArgument;
            DataRowView row1 = (DataRowView)e.Row.DataItem;
            Button lbtnName1 = (Button)e.Row.FindControl("Grid2_Button1");
            ExpandoObject param1 = new { ID = Cellvalue1 }.ToExpando();

        }
    }

    protected void Grid2_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int rowIndex = -1;

        if (e.CommandName == "Grid2_Button1")
        {
            //MsgBox(e.CommandArgument.ToString() + "", this.Page, this);

            MB003 = e.CommandArgument.ToString();

            //ADDTB_WKF_EXTERNAL_TASK_POSSET("商品特價折扣", MB003);
        }

    }


    public void OnBeforeExport2(object sender, Ede.Uof.Utility.Component.BeforeExportEventArgs e)
    {
        //SETEXCEL();

    }

    private void BindGrid3()
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ERPconnectionstring"].ToString();
        Ede.Uof.Utility.Data.DatabaseHelper m_db = new Ede.Uof.Utility.Data.DatabaseHelper(connectionString);

        StringBuilder cmdTxt = new StringBuilder();
        StringBuilder QUERYS = new StringBuilder();
        StringBuilder QUERYS2 = new StringBuilder();
        StringBuilder QUERYS3 = new StringBuilder();

        //日期
        if (!string.IsNullOrEmpty(TextBox5.Text))
        {
            QUERYS.AppendFormat(@" AND MB001 LIKE '{0}%' ", TextBox5.Text.Trim());
        }

        //核單
        if (!string.IsNullOrEmpty(DropDownList3.Text))
        {
            if (DropDownList3.Text.Equals("未核單"))
            {
                QUERYS2.AppendFormat(@" AND MB008='N' ");
            }
            else if (DropDownList3.Text.Equals("已核單"))
            {
                QUERYS2.AppendFormat(@"  AND MB008='Y' ");
            }
        }
        //特價名稱
        if (!string.IsNullOrEmpty(TextBox6.Text))
        {
            QUERYS3.AppendFormat(@" AND (MB004 LIKE '%{0}%' OR MB003 LIKE '%{0}%') ", TextBox6.Text.Trim());
        }




        cmdTxt.AppendFormat(@" 
                           SELECT *
                            ,(
                                        SELECT  LTRIM(RTRIM(MF004))+LTRIM(RTRIM(MA002))+ CHAR(13) + CHAR(10) 
                                        FROM [TK].dbo.POSMF,[TK].dbo.WSCMA
                                        WHERE MF004=MA001 AND MF003 = MB003
                                        FOR XML PATH('')) AS All_MF004
                            ,(
                                        SELECT  LTRIM(RTRIM(NI002))+ CHAR(13) + CHAR(10) 
                                        FROM [TK].dbo.POSMG,[TK].dbo.WSCNI
                                        WHERE MG005=NI001 AND MG003 = MB003
                                        FOR XML PATH('')) AS All_NI002
                            ,(
                                        SELECT  LTRIM(RTRIM(CONVERT(INT,ME005)))+ '~' +LTRIM(RTRIM(CONVERT(INT,ME006)))+ CHAR(13) + CHAR(10) +'非會員特價'+CONVERT(NVARCHAR,CONVERT(INT,ME007))+ CHAR(13) + CHAR(10) +' 會員特價'+CONVERT(NVARCHAR,CONVERT(INT,ME008))+ CHAR(13) + CHAR(10) + CHAR(13) + CHAR(10) 
                                        FROM [TK].dbo.POSME
                                        WHERE  ME003 = POSMB.MB003
                                        FOR XML PATH('')) AS All_MC004
                            ,(MB012+'~'+MB013) AS 'MB012MB013'

                            FROM [TK].dbo.POSMB
                            WHERE 1=1      
                            AND MB002 IN ('3')
                            {0}
                            {1}   
                            {2}    

                                ", QUERYS.ToString(), QUERYS2.ToString(), QUERYS3.ToString());



        DataTable dt = new DataTable();

        dt.Load(m_db.ExecuteReader(cmdTxt.ToString()));



        Grid3.DataSource = dt;
        Grid3.DataBind();
    }

    protected void grid_PageIndexChanging3(object sender, GridViewPageEventArgs e)
    {

    }
    protected void Grid3_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //Grid1_Button1
            //Get the button that raised the event
            Button Grid1_Button1 = (Button)e.Row.FindControl("Grid3_Button1");
            //Get the row that contains this button
            GridViewRow gvr1 = (GridViewRow)Grid1_Button1.NamingContainer;
            //string cellvalue = gvr.Cells[2].Text.Trim();
            string Cellvalue1 = Grid1_Button1.CommandArgument;
            DataRowView row1 = (DataRowView)e.Row.DataItem;
            Button lbtnName1 = (Button)e.Row.FindControl("Grid3_Button1");
            ExpandoObject param1 = new { ID = Cellvalue1 }.ToExpando();

        }
    }

    protected void Grid3_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int rowIndex = -1;

        if (e.CommandName == "Grid3_Button1")
        {
            //MsgBox(e.CommandArgument.ToString() + "", this.Page, this);

            MB003 = e.CommandArgument.ToString();

            //ADDTB_WKF_EXTERNAL_TASK_POSSET("商品特價折扣", MB003);
        }

    }


    public void OnBeforeExport3(object sender, Ede.Uof.Utility.Component.BeforeExportEventArgs e)
    {
        //SETEXCEL();

    }

    private void BindGrid4()
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ERPconnectionstring"].ToString();
        Ede.Uof.Utility.Data.DatabaseHelper m_db = new Ede.Uof.Utility.Data.DatabaseHelper(connectionString);

        StringBuilder cmdTxt = new StringBuilder();
        StringBuilder QUERYS = new StringBuilder();
        StringBuilder QUERYS2 = new StringBuilder();
        StringBuilder QUERYS3 = new StringBuilder();

        //日期
        if (!string.IsNullOrEmpty(TextBox7.Text))
        {
            QUERYS.AppendFormat(@" AND MI001 LIKE '{0}%' ", TextBox7.Text.Trim());
        }

        //核單
        if (!string.IsNullOrEmpty(DropDownList4.Text))
        {
            if (DropDownList4.Text.Equals("未核單"))
            {
                QUERYS2.AppendFormat(@" AND MI015='N' ");
            }
            else if (DropDownList4.Text.Equals("已核單"))
            {
                QUERYS2.AppendFormat(@"  AND MI015='Y' ");
            }
        }
        //特價名稱
        if (!string.IsNullOrEmpty(TextBox8.Text))
        {
            QUERYS3.AppendFormat(@" AND (MI003 LIKE '%{0}%' OR MI004 LIKE '%{0}%') ", TextBox8.Text.Trim());
        }




        cmdTxt.AppendFormat(@" 
                            SELECT *
                            ,(
                                        SELECT  LTRIM(RTRIM(MF004))+LTRIM(RTRIM(MA002))+ CHAR(13) + CHAR(10) 
                                        FROM [TK].dbo.POSMF,[TK].dbo.WSCMA
                                        WHERE MF004=MA001 AND MF003 = MI003
                                        FOR XML PATH('')) AS All_MF004                                     
                            ,(
                                        SELECT  LTRIM(RTRIM(NI002))+ CHAR(13) + CHAR(10) 
                                        FROM [TK].dbo.POSMG,[TK].dbo.WSCNI
                                        WHERE MG005=NI001 AND MG003 = MI003
                                        FOR XML PATH('')) AS All_NI002
                            ,(
                                        SELECT  LTRIM(RTRIM(MJ004))+ CHAR(13) + CHAR(10)+LTRIM(RTRIM(MB002))+ CHAR(13) + CHAR(10) +'非會員特價'+CONVERT(NVARCHAR,CONVERT(INT,MI017))+ CHAR(13) + CHAR(10) +' 會員特價'+CONVERT(NVARCHAR,CONVERT(INT,MI018))+ CHAR(13) + CHAR(10) + CHAR(13) + CHAR(10) 
                                        FROM [TK].dbo.POSMJ,[TK].dbo.INVMB
                                        WHERE MJ004=MB001 AND  MJ003 = MI003
                                        FOR XML PATH('')) AS All_MC004
                            ,(MI005+'~'+MI006) AS 'MB012MB013'

                            FROM [TK].dbo.POSMI
                            WHERE 1=1                             
                            {0}
                            {1}   
                            {2}    

                                ", QUERYS.ToString(), QUERYS2.ToString(), QUERYS3.ToString());



        DataTable dt = new DataTable();

        dt.Load(m_db.ExecuteReader(cmdTxt.ToString()));



        Grid4.DataSource = dt;
        Grid4.DataBind();
    }

    protected void grid_PageIndexChanging4(object sender, GridViewPageEventArgs e)
    {

    }
    protected void Grid4_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //Grid1_Button1
            //Get the button that raised the event
            Button Grid1_Button1 = (Button)e.Row.FindControl("Grid4_Button1");
            //Get the row that contains this button
            GridViewRow gvr1 = (GridViewRow)Grid1_Button1.NamingContainer;
            //string cellvalue = gvr.Cells[2].Text.Trim();
            string Cellvalue1 = Grid1_Button1.CommandArgument;
            DataRowView row1 = (DataRowView)e.Row.DataItem;
            Button lbtnName1 = (Button)e.Row.FindControl("Grid4_Button1");
            ExpandoObject param1 = new { ID = Cellvalue1 }.ToExpando();

        }
    }

    protected void Grid4_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int rowIndex = -1;

        if (e.CommandName == "Grid4_Button1")
        {
            //MsgBox(e.CommandArgument.ToString() + "", this.Page, this);

            MB003 = e.CommandArgument.ToString();

            //ADDTB_WKF_EXTERNAL_TASK_POSSET("商品特價折扣", MB003);
        }

    }


    public void OnBeforeExport4(object sender, Ede.Uof.Utility.Component.BeforeExportEventArgs e)
    {
        //SETEXCEL();

    }

    private void BindGrid5()
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ERPconnectionstring"].ToString();
        Ede.Uof.Utility.Data.DatabaseHelper m_db = new Ede.Uof.Utility.Data.DatabaseHelper(connectionString);

        StringBuilder cmdTxt = new StringBuilder();
        StringBuilder QUERYS = new StringBuilder();
        StringBuilder QUERYS2 = new StringBuilder();
        StringBuilder QUERYS3 = new StringBuilder();

        //日期
        if (!string.IsNullOrEmpty(TextBox9.Text))
        {
            QUERYS.AppendFormat(@" AND MO001 LIKE '{0}%' ", TextBox9.Text.Trim());
        }

        //核單
        if (!string.IsNullOrEmpty(DropDownList5.Text))
        {
            if (DropDownList5.Text.Equals("未核單"))
            {
                QUERYS2.AppendFormat(@" AND MO008='N' ");
            }
            else if (DropDownList5.Text.Equals("已核單"))
            {
                QUERYS2.AppendFormat(@"  AND MO008='Y' ");
            }
        }
        //特價名稱
        if (!string.IsNullOrEmpty(TextBox10.Text))
        {
            QUERYS3.AppendFormat(@" AND (MO003 LIKE '%{0}%' OR MO004 LIKE '%{0}%') ", TextBox10.Text.Trim());
        }




        cmdTxt.AppendFormat(@" 
                           SELECT *
                            ,(
                                        SELECT  LTRIM(RTRIM(MF004))+LTRIM(RTRIM(MA002))+ CHAR(13) + CHAR(10) 
                                        FROM [TK].dbo.POSMF,[TK].dbo.WSCMA
                                        WHERE MF004=MA001 AND MF003 = MO003
                                        FOR XML PATH('')) AS All_MF004                                     
                            ,(
                                        SELECT  LTRIM(RTRIM(NI002))+ CHAR(13) + CHAR(10) 
                                        FROM [TK].dbo.POSMG,[TK].dbo.WSCNI
                                        WHERE MG005=NI001 AND MG003 = MO003
                                        FOR XML PATH('')) AS All_NI002
                            ,(
                                        SELECT  LTRIM(RTRIM(MP005))+ CHAR(13) + CHAR(10)+LTRIM(RTRIM(MB002))+ CHAR(13) + CHAR(10) +'非會員特價'+CONVERT(NVARCHAR,CONVERT(INT,MP006))+ CHAR(13) + CHAR(10) +' 會員特價'+CONVERT(NVARCHAR,CONVERT(INT,MP007))+ CHAR(13) + CHAR(10) + CHAR(13) + CHAR(10) 
                                        FROM [TK].dbo.POSMP,[TK].dbo.INVMB
                                        WHERE MP005=MB001 AND  MP003 = MO003
                                        FOR XML PATH('')) AS All_MC004
                            ,(MO011+'~'+MO012) AS 'MB012MB013'

                            FROM [TK].dbo.POSMO
                            WHERE 1=1  
                            AND MO003='520240101001'
                            {0}
                            {1}   
                            {2}    

                                ", QUERYS.ToString(), QUERYS2.ToString(), QUERYS3.ToString());



        DataTable dt = new DataTable();

        dt.Load(m_db.ExecuteReader(cmdTxt.ToString()));



        Grid5.DataSource = dt;
        Grid5.DataBind();
    }

    protected void grid_PageIndexChanging5(object sender, GridViewPageEventArgs e)
    {

    }
    protected void Grid5_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //Grid1_Button1
            //Get the button that raised the event
            Button Grid1_Button1 = (Button)e.Row.FindControl("Grid5_Button1");
            //Get the row that contains this button
            GridViewRow gvr1 = (GridViewRow)Grid1_Button1.NamingContainer;
            //string cellvalue = gvr.Cells[2].Text.Trim();
            string Cellvalue1 = Grid1_Button1.CommandArgument;
            DataRowView row1 = (DataRowView)e.Row.DataItem;
            Button lbtnName1 = (Button)e.Row.FindControl("Grid5_Button1");
            ExpandoObject param1 = new { ID = Cellvalue1 }.ToExpando();

        }
    }

    protected void Grid5_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int rowIndex = -1;

        if (e.CommandName == "Grid5_Button1")
        {
            //MsgBox(e.CommandArgument.ToString() + "", this.Page, this);

            MB003 = e.CommandArgument.ToString();

            //ADDTB_WKF_EXTERNAL_TASK_POSSET("商品特價折扣", MB003);
        }

    }


    public void OnBeforeExport5(object sender, Ede.Uof.Utility.Component.BeforeExportEventArgs e)
    {
        //SETEXCEL();

    }

    //
    public void ADDTB_WKF_EXTERNAL_TASK_POSSET(string KINDS,string MB003)
    {
        DataTable DT = SEARCH_POSM(MB003);
        DataTable DTUPFDEP = SEARCHUOFDEP(DT.Rows[0]["CREATOR"].ToString());

        string account = DT.Rows[0]["CREATOR"].ToString();
        string groupId = DT.Rows[0]["GROUP_ID"].ToString();
        string jobTitleId = DT.Rows[0]["TITLE_ID"].ToString();
        string fillerName = DT.Rows[0]["NAME"].ToString();
        string fillerUserGuid = DT.Rows[0]["USER_GUID"].ToString();

        string DEPNAME = DTUPFDEP.Rows[0]["DEPNAME"].ToString();
        string DEPNO = DTUPFDEP.Rows[0]["DEPNO"].ToString();

        string EXTERNAL_FORM_NBR = DT.Rows[0]["MB003"].ToString().Trim() ;

        int rowscounts = 0;

        XmlDocument xmlDoc = new XmlDocument();
        //建立根節點
        XmlElement Form = xmlDoc.CreateElement("Form");

        //正式的id
        string ID = SEARCHFORM_VERSION_ID("POS,商品活動設定");

        if (!string.IsNullOrEmpty(ID))
        {
            Form.SetAttribute("formVersionId", ID);
        }


        Form.SetAttribute("urgentLevel", "2");
        //加入節點底下
        xmlDoc.AppendChild(Form);

        ////建立節點Applicant
        XmlElement Applicant = xmlDoc.CreateElement("Applicant");
        Applicant.SetAttribute("account", account);
        Applicant.SetAttribute("groupId", groupId);
        Applicant.SetAttribute("jobTitleId", jobTitleId);
        //加入節點底下
        Form.AppendChild(Applicant);

        //建立節點 Comment
        XmlElement Comment = xmlDoc.CreateElement("Comment");
        Comment.InnerText = "申請者意見";
        //加入至節點底下
        Applicant.AppendChild(Comment);

        //建立節點 FormFieldValue
        XmlElement FormFieldValue = xmlDoc.CreateElement("FormFieldValue");
        //加入至節點底下
        Form.AppendChild(FormFieldValue);

        //建立節點FieldItem
        //ID 表單編號	
        XmlElement FieldItem = xmlDoc.CreateElement("FieldItem");
        FieldItem.SetAttribute("fieldId", "ID");
        FieldItem.SetAttribute("fieldValue", "");
        FieldItem.SetAttribute("realValue", "");
        FieldItem.SetAttribute("enableSearch", "True");
        FieldItem.SetAttribute("fillerName", fillerName);
        FieldItem.SetAttribute("fillerUserGuid", fillerUserGuid);
        FieldItem.SetAttribute("fillerAccount", account);
        FieldItem.SetAttribute("fillSiteId", "");
        //加入至members節點底下
        FormFieldValue.AppendChild(FieldItem);

        //建立節點FieldItem
        //FIELD001	
        FieldItem = xmlDoc.CreateElement("FieldItem");
        FieldItem.SetAttribute("fieldId", "FIELD001");
        FieldItem.SetAttribute("fieldValue", DT.Rows[0]["MB001"].ToString());
        FieldItem.SetAttribute("realValue", "");
        FieldItem.SetAttribute("enableSearch", "True");
        FieldItem.SetAttribute("fillerName", fillerName);
        FieldItem.SetAttribute("fillerUserGuid", fillerUserGuid);
        FieldItem.SetAttribute("fillerAccount", account);
        FieldItem.SetAttribute("fillSiteId", "");
        //加入至members節點底下
        FormFieldValue.AppendChild(FieldItem);
        //FIELD002	
        FieldItem = xmlDoc.CreateElement("FieldItem");
        FieldItem.SetAttribute("fieldId", "FIELD002");
        FieldItem.SetAttribute("fieldValue", DT.Rows[0]["MB003"].ToString());
        FieldItem.SetAttribute("realValue", "");
        FieldItem.SetAttribute("enableSearch", "True");
        FieldItem.SetAttribute("fillerName", fillerName);
        FieldItem.SetAttribute("fillerUserGuid", fillerUserGuid);
        FieldItem.SetAttribute("fillerAccount", account);
        FieldItem.SetAttribute("fillSiteId", "");
        //加入至members節點底下
        FormFieldValue.AppendChild(FieldItem);
        //FIELD003	
        FieldItem = xmlDoc.CreateElement("FieldItem");
        FieldItem.SetAttribute("fieldId", "FIELD003");
        FieldItem.SetAttribute("fieldValue", DT.Rows[0]["MB004"].ToString());
        FieldItem.SetAttribute("realValue", "");
        FieldItem.SetAttribute("enableSearch", "True");
        FieldItem.SetAttribute("fillerName", fillerName);
        FieldItem.SetAttribute("fillerUserGuid", fillerUserGuid);
        FieldItem.SetAttribute("fillerAccount", account);
        FieldItem.SetAttribute("fillSiteId", "");
        //加入至members節點底下
        FormFieldValue.AppendChild(FieldItem);
        //FIELD004	
        FieldItem = xmlDoc.CreateElement("FieldItem");
        FieldItem.SetAttribute("fieldId", "FIELD004");
        FieldItem.SetAttribute("fieldValue", DT.Rows[0]["All_MC004"].ToString());
        FieldItem.SetAttribute("realValue", "");
        FieldItem.SetAttribute("enableSearch", "True");
        FieldItem.SetAttribute("fillerName", fillerName);
        FieldItem.SetAttribute("fillerUserGuid", fillerUserGuid);
        FieldItem.SetAttribute("fillerAccount", account);
        FieldItem.SetAttribute("fillSiteId", "");
        //加入至members節點底下
        FormFieldValue.AppendChild(FieldItem);
        //FIELD005	
        FieldItem = xmlDoc.CreateElement("FieldItem");
        FieldItem.SetAttribute("fieldId", "FIELD005");
        FieldItem.SetAttribute("fieldValue", DT.Rows[0]["All_MF004"].ToString());
        FieldItem.SetAttribute("realValue", "");
        FieldItem.SetAttribute("enableSearch", "True");
        FieldItem.SetAttribute("fillerName", fillerName);
        FieldItem.SetAttribute("fillerUserGuid", fillerUserGuid);
        FieldItem.SetAttribute("fillerAccount", account);
        FieldItem.SetAttribute("fillSiteId", "");
        //加入至members節點底下
        FormFieldValue.AppendChild(FieldItem);
        //FIELD006	
        FieldItem = xmlDoc.CreateElement("FieldItem");
        FieldItem.SetAttribute("fieldId", "FIELD006");
        FieldItem.SetAttribute("fieldValue", DT.Rows[0]["All_NI002"].ToString());
        FieldItem.SetAttribute("realValue", "");
        FieldItem.SetAttribute("enableSearch", "True");
        FieldItem.SetAttribute("fillerName", fillerName);
        FieldItem.SetAttribute("fillerUserGuid", fillerUserGuid);
        FieldItem.SetAttribute("fillerAccount", account);
        FieldItem.SetAttribute("fillSiteId", "");
        //加入至members節點底下
        FormFieldValue.AppendChild(FieldItem);
        //FIELD007	
        FieldItem = xmlDoc.CreateElement("FieldItem");
        FieldItem.SetAttribute("fieldId", "FIELD007");
        FieldItem.SetAttribute("fieldValue", KINDS);
        FieldItem.SetAttribute("realValue", "");
        FieldItem.SetAttribute("enableSearch", "True");
        FieldItem.SetAttribute("fillerName", fillerName);
        FieldItem.SetAttribute("fillerUserGuid", fillerUserGuid);
        FieldItem.SetAttribute("fillerAccount", account);
        FieldItem.SetAttribute("fillSiteId", "");
        //加入至members節點底下
        FormFieldValue.AppendChild(FieldItem);
        //FIELD008	
        FieldItem = xmlDoc.CreateElement("FieldItem");
        FieldItem.SetAttribute("fieldId", "FIELD008");
        FieldItem.SetAttribute("fieldValue", DT.Rows[0]["MB012MB013"].ToString());
        FieldItem.SetAttribute("realValue", "");
        FieldItem.SetAttribute("enableSearch", "True");
        FieldItem.SetAttribute("fillerName", fillerName);
        FieldItem.SetAttribute("fillerUserGuid", fillerUserGuid);
        FieldItem.SetAttribute("fillerAccount", account);
        FieldItem.SetAttribute("fillSiteId", "");
        //加入至members節點底下
        FormFieldValue.AppendChild(FieldItem);

        //用ADDTACK，直接啟動起單
        ADDTACK(Form);

    }
    public string SEARCHFORM_VERSION_ID(string FORM_NAME)
    {
        try
        {

            string connectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
            Ede.Uof.Utility.Data.DatabaseHelper m_db = new Ede.Uof.Utility.Data.DatabaseHelper(connectionString);

            StringBuilder cmdTxt = new StringBuilder();
            StringBuilder QUERYS = new StringBuilder();



            cmdTxt.AppendFormat(@" 
                                 SELECT TOP 1 RTRIM(LTRIM(TB_WKF_FORM_VERSION.FORM_VERSION_ID)) FORM_VERSION_ID,TB_WKF_FORM_VERSION.FORM_ID,TB_WKF_FORM_VERSION.VERSION,TB_WKF_FORM_VERSION.ISSUE_CTL
                                    ,TB_WKF_FORM.FORM_NAME
                                    FROM [UOF].dbo.TB_WKF_FORM_VERSION,[UOF].dbo.TB_WKF_FORM
                                    WHERE 1=1
                                    AND TB_WKF_FORM_VERSION.FORM_ID=TB_WKF_FORM.FORM_ID
                                    AND TB_WKF_FORM_VERSION.ISSUE_CTL=1
                                    AND FORM_NAME='{0}'
                                    ORDER BY TB_WKF_FORM_VERSION.FORM_ID,TB_WKF_FORM_VERSION.VERSION DESC

                                   ", FORM_NAME);




            //m_db.AddParameter("@SDATE", SDATE);
            //m_db.AddParameter("@EDATE", EDATE);

            DataTable dt = new DataTable();

            dt.Load(m_db.ExecuteReader(cmdTxt.ToString()));

            if (dt.Rows.Count >= 1)
            {
                return dt.Rows[0]["FORM_VERSION_ID"].ToString();
            }
            else
            {
                return "";
            }

        }
        catch
        {
            return "";
        }
        finally
        {

        }
    }

    public DataTable SEARCH_POSM(string MB003)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ERPconnectionstring"].ToString();
        Ede.Uof.Utility.Data.DatabaseHelper m_db = new Ede.Uof.Utility.Data.DatabaseHelper(connectionString);

        StringBuilder cmdTxt = new StringBuilder();
        StringBuilder QUERYS = new StringBuilder();
        try
        {

            cmdTxt.AppendFormat(@"  
                                   SELECT TEMP.*
                                    ,(SELECT TOP 1 GROUP_ID FROM [192.168.1.223].[UOF].[dbo].[TB_EB_EMPL_DEP] WHERE [TB_EB_EMPL_DEP].USER_GUID=TEMP.USER_GUID) AS 'GROUP_ID'
                                    ,(SELECT TOP 1 TITLE_ID FROM [192.168.1.223].[UOF].[dbo].[TB_EB_EMPL_DEP] WHERE [TB_EB_EMPL_DEP].USER_GUID=TEMP.USER_GUID) AS 'TITLE_ID'

                                    FROM 
                                    (
                                    SELECT
                                    POSMB.[CREATOR]
                                    ,POSMB.[MB001]
                                    ,POSMB.[MB002]
                                    ,POSMB.[MB003]
                                    ,POSMB.[MB004]
                                    ,POSMB.[MB005]
                                    ,POSMB.[MB006]
                                    ,POSMB.[MB007]
                                    ,POSMB.[MB008]
                                    ,POSMB.[MB009]
                                    ,POSMB.[MB010]
                                    ,POSMB.[MB011]
                                    ,POSMB.[MB012]
                                    ,POSMB.[MB013]
                                    ,POSMB.[MB014]
                                    ,POSMB.[MB015]
                                    ,POSMB.[MB016]
                                    ,POSMB.[MB017]
                                    ,POSMB.[MB018]
                                    ,POSMB.[MB019]
                                    ,POSMB.[MB020]
                                    ,POSMB.[MB021]
                                    ,POSMB.[MB022]
                                    ,POSMB.[MB023]
                                    ,POSMB.[MB024]
                                    ,POSMB.[MB025]
                                    ,POSMB.[MB026]
                                    ,POSMB.[MB027]
                                    ,POSMB.[MB028]
                                    ,POSMB.[MB029]
                                    ,POSMB.[MB030]
                                    ,POSMB.[MB031]
                                    ,POSMB.[MB032]
                                    ,POSMB.[MB033]
                                    ,POSMB.[MB034]
                                    ,POSMB.[MB035]
                                    ,POSMB.[MB036]
                                    ,POSMB.[MB037]
                                    ,POSMB.[MB038]
                                    ,POSMB.[MB039]
                                    ,POSMB.[MB040]
                                    ,POSMB.[MB041]
                                    ,POSMB.[MB042]
                                    ,POSMB.[MB043]
                                    ,POSMB.[MB044]
                                    ,POSMB.[MB045]
                                    ,POSMB.[MB046]
                                    ,POSMB.[MB200]
                                    ,POSMB.[UDF01]
                                    ,POSMB.[UDF02]
                                    ,POSMB.[UDF03]
                                    ,POSMB.[UDF04]
                                    ,POSMB.[UDF05]
                                    ,POSMB.[UDF06]
                                    ,POSMB.[UDF07]
                                    ,POSMB.[UDF08]
                                    ,POSMB.[UDF09]
                                    ,POSMB.[UDF10]
                                    ,STUFF((
                                    SELECT  LTRIM(RTRIM(MF004))+LTRIM(RTRIM(MA002))+ CHAR(10) 
                                    FROM [TK].dbo.POSMF,[TK].dbo.WSCMA
                                    WHERE MF004=MA001 AND MF003 = MB003
                                    FOR XML PATH('')), 1, 1, '1') AS All_MF004
                                    ,STUFF((
                                    SELECT  LTRIM(RTRIM(NI002)) + CHAR(10)
                                    FROM [TK].dbo.POSMG,[TK].dbo.WSCNI
                                    WHERE MG005=NI001 AND MG003 = MB003
                                    FOR XML PATH('')), 1, 1, '1') AS All_NI002
                                    ,STUFF((
                                    SELECT  LTRIM(RTRIM(MC004))+LTRIM(RTRIM(MB002))+'非會員特價'+CONVERT(NVARCHAR,CONVERT(INT,MC005))+' 會員特價'+CONVERT(NVARCHAR,CONVERT(INT,MC006))+ CHAR(10)
                                    FROM [TK].dbo.POSMC,[TK].dbo.INVMB
                                    WHERE MC004=INVMB.MB001 AND MC003 = POSMB.MB003
                                    FOR XML PATH('')), 1, 1, '1') AS All_MC004
                                     ,[TB_EB_USER].USER_GUID,NAME
                                    ,(MB012+'~'+MB013) AS 'MB012MB013'

                                    FROM [TK].dbo.POSMB
                                    LEFT JOIN [192.168.1.223].[UOF].[dbo].[TB_EB_USER] ON [TB_EB_USER].ACCOUNT= POSMB.CREATOR COLLATE Chinese_Taiwan_Stroke_BIN
                                    WHERE 1=1    
                                    AND MB003='{0}'
                                    ) AS TEMP
                              
                                    ", MB003);




            //m_db.AddParameter("@SDATE", SDATE);
            //m_db.AddParameter("@EDATE", EDATE);

            DataTable dt = new DataTable();

            dt.Load(m_db.ExecuteReader(cmdTxt.ToString()));

            if (dt != null && dt.Rows.Count >= 1)
            {
                return dt;
            }
            else
            {
                return null;
            }
        }
        catch
        {
            return null;
        }
        finally
        { }
    }
    public DataTable SEARCHUOFDEP(string ACCOUNT)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ERPconnectionstring"].ToString();
        Ede.Uof.Utility.Data.DatabaseHelper m_db = new Ede.Uof.Utility.Data.DatabaseHelper(connectionString);

        StringBuilder cmdTxt = new StringBuilder();
        StringBuilder QUERYS = new StringBuilder();
        try
        {

            cmdTxt.AppendFormat(@"  
                                    SELECT 
                                    [GROUP_NAME] AS 'DEPNAME'
                                    ,[TB_EB_EMPL_DEP].[GROUP_ID]+','+[GROUP_NAME]+',False' AS 'DEPNO'
                                    ,[TB_EB_USER].[USER_GUID]
                                    ,[ACCOUNT]
                                    ,[NAME]
                                    ,[TB_EB_EMPL_DEP].[GROUP_ID]
                                    ,[TITLE_ID]     
                                    ,[GROUP_NAME]
                                    ,[GROUP_CODE]
                                    FROM [192.168.1.223].[UOF].[dbo].[TB_EB_USER],[192.168.1.223].[UOF].[dbo].[TB_EB_EMPL_DEP],[192.168.1.223].[UOF].[dbo].[TB_EB_GROUP]
                                    WHERE [TB_EB_USER].[USER_GUID]=[TB_EB_EMPL_DEP].[USER_GUID]
                                    AND [TB_EB_EMPL_DEP].[GROUP_ID]=[TB_EB_GROUP].[GROUP_ID]
                                    AND ISNULL([TB_EB_GROUP].[GROUP_CODE],'')<>''
                                    AND [ACCOUNT]='{0}'
                              
                                    ", ACCOUNT);




            //m_db.AddParameter("@SDATE", SDATE);
            //m_db.AddParameter("@EDATE", EDATE);

            DataTable dt = new DataTable();

            dt.Load(m_db.ExecuteReader(cmdTxt.ToString()));

            if (dt != null && dt.Rows.Count >= 1)
            {
                return dt;
            }
            else
            {
                return null;
            }
        }
        catch
        {
            return null;
        }
        finally
        { }
    }
    public void ADDTACK(XmlElement Form)
    {
        Ede.Uof.WKF.Utility.TaskUtilityUCO taskUCO = new Ede.Uof.WKF.Utility.TaskUtilityUCO();

        string result = taskUCO.WebService_CreateTask(Form.OuterXml);

        XElement resultXE = XElement.Parse(result);

        string status = "";
        string formNBR = "";
        string error = "";

        string NEWTASK_ID = "";

        if (resultXE.Element("Status").Value == "1")
        {
            status = "送單成功!";
            formNBR = resultXE.Element("FormNumber").Value;

            NEWTASK_ID = formNBR;

            Logger.Write("起單", status + formNBR);
            MsgBox("送單成功 \r\n" + MB003 + " > " + formNBR, this.Page, this);

        }
        else
        {
            status = "送單失敗!";
            error = resultXE.Element("Exception").Element("Message").Value;

            Logger.Write("起單", status + error + "\r\n" + Form.OuterXml);

            MsgBox("失敗了，無法送單!!!!    " + error + "\r\n" + Form.OuterXml, this.Page, this);

            throw new Exception(status + error + "\r\n" + Form.OuterXml);

        }
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
    protected void Button1_Click(object sender, EventArgs e)
    {
        BindGrid();       

    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        BindGrid2();

    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        BindGrid3();

    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        BindGrid4();

    }
    protected void Button5_Click(object sender, EventArgs e)
    {
        BindGrid5();

    }
    protected void Button6_Click(object sender, EventArgs e)
    {


    }
    protected void Button7_Click(object sender, EventArgs e)
    {


    }
    #endregion
}