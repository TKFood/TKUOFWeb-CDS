using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security.AntiXss;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ede.Uof.Utility.Data;
using Ede.Uof.Utility.Page.Common;

public partial class CDS_WebPage_SIGN_COPTCCOPTD : Ede.Uof.Utility.Page.BasePage
{


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindGrid(DateTime.Now.ToString("yyyyMMdd"));
            txtDate1.Text = DateTime.Now.ToString("yyyyMMdd");
          

        }
        else
        {
            //BindGrid(txtDate1.Text);

        }
    }

    #region FUNCTION
    private void BindGrid(string SDATE)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ERPconnectionstring"].ToString();
        Ede.Uof.Utility.Data.DatabaseHelper m_db = new Ede.Uof.Utility.Data.DatabaseHelper(connectionString);

        string cmdTxt = @" 
                        SELECT TC001,TC002,CONVERT(INT,TC029) TC029,CONVERT(INT,TC030) TC030,CONVERT(INT,(TC029+TC030)) AS MONEYS
                        FROM [TK].dbo.COPTC
                        WHERE TC027='N' 
                        AND  TC003=@SDATE
                        ORDER BY TC001,TC002
                        ";

        m_db.AddParameter("@SDATE", SDATE);

        DataTable dt = new DataTable();

        dt.Load(m_db.ExecuteReader(cmdTxt));

        Grid1.DataSource = dt;
        Grid1.DataBind();
    }

    protected void grid_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        Grid1.PageIndex = e.NewPageIndex;
        BindGrid(this.Session["SDATE"].ToString());
    }
    protected void Grid1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
       
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        DataRowView row = (DataRowView)e.Row.DataItem;
        //        LinkButton lbtnName = (LinkButton)e.Row.FindControl("lbtnName");

        //        ExpandoObject param = new { ID = row["ID"].ToString() }.ToExpando();

        //        //Grid開窗是用RowDataBound事件再開窗
        //        Dialog.Open2(lbtnName, "~/CDS/WebPage/TKRESEARCHTBDEVMEMODialogEDITDEL.aspx", "", 800, 600, Dialog.PostBackType.AfterReturn, param);
        //    }


    }

    public void OnBeforeExport1(object sender, Ede.Uof.Utility.Component.BeforeExportEventArgs e)
    {
        
    }
    public void UPDATECOPTCCOPTD(string SQL)
    {
        string TC027 = "Y";
        string TC048 = "N";
        string TD021 = "Y";
        string COMPANY = "TK";
        string MODIFIER = "160115";
        string MODI_DATE = DateTime.Now.ToString("yyyyMMdd");
        string MODI_TIME = DateTime.Now.ToString("HH:mm:dd");

        string connectionString = ConfigurationManager.ConnectionStrings["ERPconnectionstring"].ToString();

        var SQLCOMMAND = SQL;

        try
        {
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(SQLCOMMAND, cnn))
                {

                    cmd.Parameters.AddWithValue("@TC027", TC027);
                    cmd.Parameters.AddWithValue("@TC048", TC048);
                    cmd.Parameters.AddWithValue("@TD021", TD021);

                    cmd.Parameters.AddWithValue("@COMPANY", COMPANY);
                    cmd.Parameters.AddWithValue("@MODIFIER", MODIFIER);
                    cmd.Parameters.AddWithValue("@MODI_DATE", MODI_DATE);
                    cmd.Parameters.AddWithValue("@MODI_TIME", MODI_TIME);

                    cnn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();


                    if (rowsAffected>=1)
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

    public string SETSQL(string TC001TC002)
    {
        StringBuilder SQL = new StringBuilder();

        SQL.AppendFormat(@"
                        UPDATE [TK].dbo.COPTC
                        SET TC027=@TC027,TC048=@TC048, FLAG=FLAG+1,COMPANY=@COMPANY,MODIFIER=@MODIFIER ,MODI_DATE=@MODI_DATE, MODI_TIME=@MODI_TIME 
                        WHERE TC001+TC002 IN ({0})

                        UPDATE [TK].dbo.COPTD 
                        SET TD021=@TD021, FLAG=FLAG+1,COMPANY=@COMPANY,MODIFIER=@MODIFIER ,MODI_DATE=@MODI_DATE, MODI_TIME=@MODI_TIME 
                        WHERE TD001+TD002 IN ({0})
                        ", TC001TC002);

        return SQL.ToString();
    }

    #endregion


    #region BUTTON
    protected void btn1_Click(object sender, EventArgs e)
    {
        BindGrid(txtDate1.Text);
    }

    protected void btn2_Click(object sender, EventArgs e)
    {
        string TC001TC002 = "";
        Grid1.EditIndex = -1;

        foreach (GridViewRow gvr in this.Grid1.Rows)
        {
            Control ctl = gvr.FindControl("CheckBox");
            CheckBox ck = (CheckBox)ctl;
            if (ck.Checked)
            {
                TableCellCollection cell = gvr.Cells;
                TC001TC002 += "'"+cell[1].Text+ cell[2].Text + "',";
            }
        }
        TC001TC002 += "''";
        Label3.Text = TC001TC002.ToString();

        string SQL= SETSQL(Label3.Text);

        UPDATECOPTCCOPTD(SQL);

        BindGrid(txtDate1.Text);
    }
    #endregion
}