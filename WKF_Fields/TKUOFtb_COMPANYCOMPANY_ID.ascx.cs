using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Ede.Uof.WKF.Design;
using System.Collections.Generic;
using Ede.Uof.WKF.Utility;
using Ede.Uof.EIP.Organization.Util;
using Ede.Uof.WKF.Design.Data;
using Ede.Uof.WKF.VersionFields;
using System.Xml;
using System.Linq;
using System.Data.SqlClient;
using Ede.Uof.Utility.Data;
using System.Xml.Linq;

public partial class WKF_OptionalFields_TKUOFtb_COMPANYCOMPANY_ID : WKF_FormManagement_VersionFieldUserControl_VersionFieldUC
{

	#region ==============公開方法及屬性==============
    //表單設計時
	//如果為False時,表示是在表單設計時
    private bool m_ShowGetValueButton = true;
    public bool ShowGetValueButton
    {
        get { return this.m_ShowGetValueButton; }
        set { this.m_ShowGetValueButton = value; }
    }

    #endregion

    string MA001;
    string MA002;

    protected void Page_Load(object sender, EventArgs e)
    {
		//這裡不用修改
		//欄位的初始化資料都到SetField Method去做
        SetField(m_versionField);

        if (!IsPostBack)
        {
            BindDropDownList();
            BindDropDownList2();
            BindDropDownList3();
            BindDropDownList4();
            BindDropDownList5();
            BindDropDownList6();
            BindDropDownList7();
        }
    }    

    /// <summary>
    /// 外掛欄位的條件值
    /// </summary>
    public override string ConditionValue
    {
        get
        {
            //回傳字串
            //此字串的內容將會被表單拿來當做條件判斷的值
            //return String.Empty;
            return TextBox1.Text;
        }
    }

    /// <summary>
    /// 是否被修改
    /// </summary>
    public override bool IsModified
    {
        get
        {
			//請自行判斷欄位內容是否有被修改
			//有修改回傳True
			//沒有修改回傳False
            //若實作產品標準的控制修改權限必需實作
            //一般是用 m_versionField.FieldValue (表單開啟前的值)
            //      和this.FieldValue (當前的值) 作比對
			return false;
        }
    }

    /// <summary>
    /// 查詢顯示的標題
    /// </summary>
    public override string DisplayTitle
    {
        get
        {
            //表單查詢或WebPart顯示的標題
            //回傳字串
            return TextBox1.Text;
        }
    }

    /// <summary>
    /// 訊息通知的內容
    /// </summary>
    public override string Message
    {
        get
        {
            //表單訊息通知顯示的內容
            //回傳字串
            return TextBox1.Text;
        }
    }


    /// <summary>
    /// 真實的值
    /// </summary>
    public override string RealValue
    {
        get
        {
            //回傳字串
            //取得表單欄位簽核者的UsetSet字串
            //內容必須符合EB UserSet的格式
            return TextBox1.Text;
        }
        set
        {
			//這個屬性不用修改
            base.m_fieldValue = value;
        }
    }


    /// <summary>
    /// 欄位的內容
    /// </summary>
    public override string FieldValue
    {
        get
        {
            //回傳字串
            //取得表單欄位填寫的內容

            //<FieldValue MA001='' MA002=''>
            XElement xe = new XElement("FieldValue"
                ,new XAttribute("MA001", TextBox1.Text.Trim())
                ,new XAttribute("MA002", LabelNAME.Text.Trim())
                ,new XAttribute("MA011", TextBox2.Text.Trim())
                ,new XAttribute("MA014", DropDownList1.SelectedValue.ToString().Trim())
                ,new XAttribute("MA015", DropDownList2.SelectedValue.ToString().Trim())
                ,new XAttribute("MA017", DropDownList3.SelectedValue.ToString().Trim())
                ,new XAttribute("MA018", DropDownList4.SelectedValue.ToString().Trim())
                ,new XAttribute("MA019", DropDownList5.SelectedValue.ToString().Trim())
                ,new XAttribute("MA028", TextBox3.Text.Trim())
                ,new XAttribute("MA031", DropDownList6.SelectedValue.ToString().Trim())
                ,new XAttribute("MA033", TextBox4.Text.Trim())
                ,new XAttribute("MA037", DropDownList7.SelectedValue.ToString().Trim())
                );

            return xe.ToString();
        }
        set
        {
			//這個屬性不用修改
            base.m_fieldValue = value;
        }
    }


  
    /// <summary>
    /// 是否為第一次填寫
    /// </summary>
    public override bool IsFirstTimeWrite
    {
        get
        {
            //這裡請自行判斷是否為第一次填寫
            //若實作產品標準的控制修改權限必需實作
            //實作此屬性填寫者可修改也才會生效
            //一般是用 m_versionField.Filler == null(沒有記錄填寫者代表沒填過)
            //      和this.FieldValue (當前的值是否為預設的空白) 作比對
            return false;
        }
        set
        {
            //這個屬性不用修改
            base.IsFirstTimeWrite = value;
        }
    }

    /// <summary>
    /// 設定元件狀態
    /// </summary>
    /// <param name="Enabled">是否啟用輸入元件</param>
    public void EnabledControl(bool Enabled)
    {
        Button1.Visible = Enabled;
    }

    /// <summary>
    /// 顯示時欄位初始值
    /// </summary>
    /// <param name="versionField">欄位集合</param>
    public override void SetField(Ede.Uof.WKF.Design.VersionField versionField)
    {
        FieldOptional fieldOptional = versionField as FieldOptional;

        if (fieldOptional != null)
        {
            //若有擴充屬性，可以用該屬性存取
            // fieldOptional.ExtensionSetting

            //TextBox1.Text= fieldOptional.FieldValue;
            //LabelNAME.Text = COMPANY_NAME;

            if(!string.IsNullOrEmpty(fieldOptional.FieldValue))
            {
                XElement xe = XElement.Parse(fieldOptional.FieldValue);

                TextBox1.Text = xe.Attribute("MA001").Value;
                LabelNAME.Text = xe.Attribute("MA002").Value;
                TextBox2.Text = xe.Attribute("MA011").Value;
                DropDownList1.SelectedValue = xe.Attribute("MA014").Value;
                DropDownList2.SelectedValue = xe.Attribute("MA015").Value;
                DropDownList3.SelectedValue = xe.Attribute("MA017").Value;
                DropDownList4.SelectedValue = xe.Attribute("MA018").Value;
                DropDownList5.SelectedValue = xe.Attribute("MA019").Value;
                TextBox3.Text = xe.Attribute("MA028").Value;
                DropDownList6.SelectedValue = xe.Attribute("MA031").Value;
                TextBox4.Text = xe.Attribute("MA033").Value;
                DropDownList7.SelectedValue = xe.Attribute("MA037").Value;
            }

            switch (fieldOptional.FieldMode)
            {
                case FieldMode.Print:
                case FieldMode.View:
                    //觀看和列印都需作沒有權限的處理
                    EnabledControl(false);
                    break;

            }
            
            #region ==============屬性說明==============『』
			//fieldOptional.IsRequiredField『是否為必填欄位,如果是必填(True),如果不是必填(False)』
			//fieldOptional.DisplayOnly『是否為純顯示,如果是(True),如果不是(False),一般在觀看表單及列印表單時,屬性為True』
			//fieldOptional.HasAuthority『是否有填寫權限,如果有填寫權限(True),如果沒有填寫權限(False)』
			//fieldOptional.FieldValue『如果已有人填寫過欄位,則此屬性為記錄其內容』
			//fieldOptional.FieldDefault『如果欄位有預設值,則此屬性為記錄其內容』
			//fieldOptional.FieldModify『是否允許修改,如果允許(fieldOptional.FieldModify=FieldModifyType.yes),如果不允許(fieldOptional.FieldModify=FieldModifyType.no)』
			//fieldOptional.Modifier『如果欄位有被修改過,則Modifier的內容為EBUser,如果沒有被修改過,則會等於Null』
            #endregion

            #region ==============如果有修改，要顯示修改者資訊==============
            if (fieldOptional.Modifier != null)
            {
                lblModifier.Visible = true;
                lblModifier.ForeColor = System.Drawing.Color.FromArgb(0x52, 0x52, 0x52);
                lblModifier.Text = System.Web.Security.AntiXss.AntiXssEncoder.HtmlEncode(fieldOptional.Modifier.Name, true);
            } 
            #endregion
        }
    }

    #region
    protected void btn_Click(object sender, EventArgs e)
    {
        MA001 = TextBox1.Text;
        MA002 = SERACHtb_COMPANY(MA001);

        LabelNAME.Text = MA002;
    }

    public string SERACHtb_COMPANY(string COMPANY_ID)
    {
        DataSet ds = new DataSet();
        DatabaseHelper DbQuery = new DatabaseHelper();
        DataTable dt = new DataTable();

        string connectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            SqlCommand command = new SqlCommand(@" SELECT [COMPANY_NAME],[ERPNO] FROM [HJ_BM_DB].[dbo].[tb_COMPANY] WHERE [ERPNO]=@ERPNO", conn);
            command.Parameters.AddWithValue("@ERPNO", COMPANY_ID);

            ds.Clear();

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            conn.Open();

            adapter.Fill(ds, command.ToString());

            if(ds.Tables[0].Rows.Count>0)
            {
                return ds.Tables[0].Rows[0]["COMPANY_NAME"].ToString();
            }
            else
            {
                return "查無資料";
            }
        }
    }

    private void BindDropDownList()
    {
        DataSet ds = new DataSet();
        DatabaseHelper DbQuery = new DatabaseHelper();
        DataTable dt = new DataTable();     
        DataRow ndr = dt.NewRow();

        dt.Columns.Add("MF001", typeof(String));


        string connectionString = ConfigurationManager.ConnectionStrings["ERPconnectionstring"].ToString();
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            SqlCommand command = new SqlCommand(@"SELECT LTRIM(RTRIM(MF001)) AS MF001 FROM [TK].dbo.CMSMF ORDER BY MF001", conn);

            ds.Clear();

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            conn.Open();

            adapter.Fill(ds, command.ToString());

            if (ds.Tables[0].Rows.Count > 0)
            {
                DropDownList1.DataSource = ds.Tables[0];
                DropDownList1.DataTextField = "MF001";
                DropDownList1.DataValueField = "MF001";
                DropDownList1.DataBind();

            }
            else
            {

            }
        }


    }

    private void BindDropDownList2()
    {
        DataSet ds = new DataSet();
        DatabaseHelper DbQuery = new DatabaseHelper();
        DataTable dt = new DataTable();
        DataRow ndr = dt.NewRow();

        dt.Columns.Add("ME001", typeof(String));
        dt.Columns.Add("ME002", typeof(String));

        string connectionString = ConfigurationManager.ConnectionStrings["ERPconnectionstring"].ToString();
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            SqlCommand command = new SqlCommand(@"SELECT LTRIM(RTRIM(ME001)) AS ME001,ME002 FROM [TK].dbo.CMSME WHERE ME002 NOT LIKE '%停用%' ORDER BY ME001 ", conn);

            ds.Clear();

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            conn.Open();

            adapter.Fill(ds, command.ToString());

            if (ds.Tables[0].Rows.Count > 0)
            {
                DropDownList2.DataSource = ds.Tables[0];
                DropDownList2.DataTextField = "ME002";
                DropDownList2.DataValueField = "ME001";
                DropDownList2.DataBind();

            }
            else
            {

            }
        }
    }

    private void BindDropDownList3()
    {
        DataSet ds = new DataSet();
        DatabaseHelper DbQuery = new DatabaseHelper();
        DataTable dt = new DataTable();
        DataRow ndr = dt.NewRow();

        dt.Columns.Add("MR002", typeof(String));
        dt.Columns.Add("MR003", typeof(String));

        string connectionString = ConfigurationManager.ConnectionStrings["ERPconnectionstring"].ToString();
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            SqlCommand command = new SqlCommand(@"SELECT LTRIM(RTRIM(MR002)) AS MR002,MR003 FROM [TK].dbo.CMSMR WHERE MR001='1' ORDER BY MR002 ", conn);

            ds.Clear();

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            conn.Open();

            adapter.Fill(ds, command.ToString());

            if (ds.Tables[0].Rows.Count > 0)
            {
                DropDownList3.DataSource = ds.Tables[0];
                DropDownList3.DataTextField = "MR003";
                DropDownList3.DataValueField = "MR002";
                DropDownList3.DataBind();

            }
            else
            {

            }
        }
    }

    private void BindDropDownList4()
    {
        DataSet ds = new DataSet();
        DatabaseHelper DbQuery = new DatabaseHelper();
        DataTable dt = new DataTable();
        DataRow ndr = dt.NewRow();

        dt.Columns.Add("MR002", typeof(String));
        dt.Columns.Add("MR003", typeof(String));

        string connectionString = ConfigurationManager.ConnectionStrings["ERPconnectionstring"].ToString();
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            SqlCommand command = new SqlCommand(@"SELECT LTRIM(RTRIM(MR002)) AS MR002,MR003 FROM [TK].dbo.CMSMR WHERE MR001='3' ORDER BY MR002", conn);

            ds.Clear();

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            conn.Open();

            adapter.Fill(ds, command.ToString());

            if (ds.Tables[0].Rows.Count > 0)
            {
                DropDownList4.DataSource = ds.Tables[0];
                DropDownList4.DataTextField = "MR003";
                DropDownList4.DataValueField = "MR002";
                DropDownList4.DataBind();

            }
            else
            {

            }
        }
    }
    private void BindDropDownList5()
    {
        DataSet ds = new DataSet();
        DatabaseHelper DbQuery = new DatabaseHelper();
        DataTable dt = new DataTable();
        DataRow ndr = dt.NewRow();

        dt.Columns.Add("MR002", typeof(String));
        dt.Columns.Add("MR003", typeof(String));

        string connectionString = ConfigurationManager.ConnectionStrings["ERPconnectionstring"].ToString();
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            SqlCommand command = new SqlCommand(@"SELECT LTRIM(RTRIM(MR002)) AS MR002,MR003 FROM [TK].dbo.CMSMR WHERE MR001='4' ORDER BY MR002", conn);

            ds.Clear();

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            conn.Open();

            adapter.Fill(ds, command.ToString());

            if (ds.Tables[0].Rows.Count > 0)
            {
                DropDownList5.DataSource = ds.Tables[0];
                DropDownList5.DataTextField = "MR003";
                DropDownList5.DataValueField = "MR002";
                DropDownList5.DataBind();

            }
            else
            {

            }
        }
    }
    private void BindDropDownList6()
    {
        DataSet ds = new DataSet();
        DatabaseHelper DbQuery = new DatabaseHelper();
        DataTable dt = new DataTable();
        DataRow ndr = dt.NewRow();

        dt.Columns.Add("NA002", typeof(String));
        dt.Columns.Add("NA003", typeof(String));

        string connectionString = ConfigurationManager.ConnectionStrings["ERPconnectionstring"].ToString();
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            SqlCommand command = new SqlCommand(@"SELECT LTRIM(RTRIM(NA002)) AS NA002,NA003 FROM [TK].dbo.CMSNA WHERE COMPANY='TK' AND NA001='2' ORDER BY NA002", conn);

            ds.Clear();

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            conn.Open();

            adapter.Fill(ds, command.ToString());

            if (ds.Tables[0].Rows.Count > 0)
            {
                DropDownList6.DataSource = ds.Tables[0];
                DropDownList6.DataTextField = "NA003";
                DropDownList6.DataValueField = "NA002";
                DropDownList6.DataBind();

            }
            else
            {

            }
        }
    }

    private void BindDropDownList7()
    {
        DataSet ds = new DataSet();
        DatabaseHelper DbQuery = new DatabaseHelper();
        DataTable dt = new DataTable();
        DataRow ndr = dt.NewRow();

        dt.Columns.Add("Filed1", typeof(String));
        dt.Columns.Add("Filed2", typeof(String));
        //1.二聯式、2.三聯式、3.二聯式收銀機發票、4.三聯式收銀機發票、5.電子計算機發票、6.免用統一發票、7.電子發票

        dt.Rows.Add(new Object[] {"7", "7電子發票" });
        dt.Rows.Add(new Object[] {"1", "1二聯式" });
        dt.Rows.Add(new Object[] { "2", "2三聯式" });
        dt.Rows.Add(new Object[] { "3", "3二聯式收銀機發票" });
        dt.Rows.Add(new Object[] { "4", "4三聯式收銀機發票" });
        dt.Rows.Add(new Object[] { "5", "5電子計算機發票" });
        dt.Rows.Add(new Object[] { "6", "6免用統一發票" });



        DropDownList7.DataSource = dt;
        DropDownList7.DataTextField = "Filed2";
        DropDownList7.DataValueField = "Filed1";
        DropDownList7.DataBind();
    }

    #endregion

}