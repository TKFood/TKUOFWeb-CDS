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
using System.Xml.Linq;
using Ede.Uof.Utility.Page.Common;
using System.Linq;
using Kendo.Mvc.Extensions;

public partial class WKF_OptionalFields_OptionField_PURTAB : WKF_FormManagement_VersionFieldUserControl_VersionFieldUC
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

    string DGXML = "";
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        TextBox3.Text = DateTime.Now.ToString("yyyyMMdd");

        //這裡不用修改
        //欄位的初始化資料都到SetField Method去做
        SetField(m_versionField);

        
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
			return String.Empty;
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
            return String.Empty;
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
            return String.Empty;
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
			return String.Empty;
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
            XElement xe = XElement.Parse(txtFieldValue.Text);
            xe.SetAttributeValue("TA001", TextBox1.Text);
            xe.SetAttributeValue("TA002", TextBox2.Text);
            xe.SetAttributeValue("TA003", TextBox3.Text);
            xe.SetAttributeValue("NAME", TextBox4.Text);
            xe.SetAttributeValue("DEP", TextBox5.Text);
            xe.SetAttributeValue("COMMENT", TextBox6.Text);

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
            Dialog.Open2(btnInert, "~/CDS/WKF_Fields/ASPX/OptionField_PURTABA_ADD.aspx", "", 700, 600, Dialog.PostBackType.AfterReturn);

            if (string.IsNullOrEmpty(fieldOptional.FieldValue))
            {
                txtFieldValue.Text = "<FieldValue/>";
            }
            else
            {
                
                txtFieldValue.Text = fieldOptional.FieldValue;
                BindGrid();
            }

            //<FieldValue txt1=''  txt2='' />

            if (!string.IsNullOrEmpty(fieldOptional.FieldValue))
            {

                XElement xe = XElement.Parse(fieldOptional.FieldValue);
                TextBox1.Text = xe.Attribute("TA001").Value;
                TextBox2.Text = xe.Attribute("TA002").Value;
                TextBox3.Text = xe.Attribute("TA003").Value;
                TextBox4.Text = xe.Attribute("NAME").Value;
                TextBox5.Text = xe.Attribute("DEP").Value;
                TextBox6.Text = xe.Attribute("COMMENT").Value;
            }

            //若有擴充屬性，可以用該屬性存取
            // fieldOptional.ExtensionSetting


            //草稿
            if (!fieldOptional.IsAudit)
            {
                if(fieldOptional.HasAuthority)
                {
                    //有填寫權限的處理
                    EnabledControl(true);
                }
                else
                {
                    //沒填寫權限的處理
                    EnabledControl(false);
                }
            }
            else
            {
                //己送出

                //有填過
                if(fieldOptional.Filler != null)
                {
                    //判斷填寫的站點和當前是否相同
                    if(base.taskObj != null && base.taskObj.CurrentSite != null &&
                        base.taskObj.CurrentSite.SiteId == fieldOptional.FillSiteId && fieldOptional.Filler.UserGUID == Ede.Uof.EIP.SystemInfo.Current.UserGUID)
                    {
                        //判斷填寫權限
                        if (fieldOptional.HasAuthority)
                        {
                            //有填寫權限的處理
                            EnabledControl(true);
                        }
                        else
                        {
                            //沒填寫權限的處理
                            EnabledControl(false);
                        }
                    }
                    else
                    {
                        //判斷修改權限
                        if (fieldOptional.AllowModify)
                        {
                            //有修改權限的處理
                            EnabledControl(true);
                        }
                        else
                        {
                            //沒修改權限的處理
                            EnabledControl(false);
                        }

                    }
                }
                else
                {
                    //判斷填寫權限
                    if (fieldOptional.HasAuthority)
                    {
                        //有填寫權限的處理
                        EnabledControl(true);
                    }
                    else
                    {
                        //沒填寫權限的處理
                        EnabledControl(false);
                    }

                }
            }



            switch(fieldOptional.FieldMode)
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
    #region CODE
    private void BindGrid()
    {
        XElement xe = XElement.Parse(txtFieldValue.Text);

        DataTable dt = new DataTable();
        dt.Columns.Add("ID");
        dt.Columns.Add("品號");
        dt.Columns.Add("品名");
        dt.Columns.Add("數量");
        dt.Columns.Add("需求日");
        dt.Columns.Add("單身備註");

        var nodes = (from xl in xe.Elements("Item")
                     select xl);
        ////<FieldValue txt1=''  txt2='' />
        foreach (var node in nodes)
        {
            dt.Rows.Add(node.Attribute("id").Value,
               node.Attribute("品號").Value,
               node.Attribute("品名").Value,
               node.Attribute("數量").Value,
               node.Attribute("需求日").Value,
               node.Attribute("單身備註").Value);

        }

        Grid1.DataSource = dt;
        Grid1.DataBind();
    }


    protected void TextBox4_TextChanged(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(TextBox4.Text))
        {
            TextBox5.Text = SEARCHCMSMV(TextBox4.Text);
        }
        else
        {
            TextBox5.Text = "";
        }
    }
    protected void TextBox9_TextChanged(object sender, EventArgs e)
    {
        int n;

        if (!int.TryParse(TextBox9.Text, out n))
        {
            TextBox9.Text = "";
            LabelMESSAGES.Text = "不是正確的數字！";
        }
        else
        {
            SETLabelMESSAGES();
        }
    }


    public string  SEARCHCMSMV(string MV001)
    {
        try
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ERPconnectionstring"].ToString();
            Ede.Uof.Utility.Data.DatabaseHelper m_db = new Ede.Uof.Utility.Data.DatabaseHelper(connectionString);

            string cmdTxt = @" 
                        SELECT MV001,MV004
                        FROM [TK].[dbo].[CMSMV]
                        WHERE MV001=@MV001
                       
                        ";

            m_db.AddParameter("@MV001", MV001);
            //m_db.AddParameter("@EDATE", EDATE);

            DataTable dt = new DataTable();

            dt.Load(m_db.ExecuteReader(cmdTxt));

            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0]["MV004"].ToString();
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

    public string GetXML(string id)
    {
        XElement xe = new XElement("Item",
            new XAttribute("id", id),
             new XAttribute("品號", TextBox7.Text),
              new XAttribute("品名", TextBox8.Text),
               new XAttribute("數量", TextBox9.Text),
                new XAttribute("需求日", TextBox10.Text),
                 new XAttribute("單身備註", TextBox11.Text)

                );
        return xe.ToString();
    }

    public void SETLabelMESSAGES()
    {
        LabelMESSAGES.Text = "";
    }
    #endregion

    #region ENEVNTS
    protected void btnInert_Click(object sender, EventArgs e)
    {
        string returnValue = string.Format("<Return>{0}</Return>", Dialog.GetReturnValue());
     
        XElement returnXe = XElement.Parse(returnValue);
        var nodes = (from xl in returnXe.Elements("Item")
                     select xl);

        foreach (var node in nodes)
        {
            TextBox7.Text = node.Attribute("MB001").Value;
            TextBox8.Text = node.Attribute("MB002").Value;
     
        }
    }



    protected void btnDelete_Click(object sender, EventArgs e)
    {
        string[] ids = Grid1.GetSelectedRowGUIDs();

        XmlDocument xmlDoc = new XmlDocument();

        xmlDoc.LoadXml(txtFieldValue.Text);

        foreach (string id in ids)
        {
            string xpath = string.Format("./FieldValue/Item[@id='{0}']", id);
            XmlNode node = xmlDoc.SelectSingleNode(xpath);

            xmlDoc.SelectSingleNode("FieldValue").RemoveChild(node);

        }

        txtFieldValue.Text = xmlDoc.OuterXml;

        BindGrid();
    }

    protected void btnADD_Click(object sender, EventArgs e)
    {
        SETLabelMESSAGES();
        
        if (!string.IsNullOrEmpty(TextBox10.Text)&& TextBox10.Text.Length==8)
        {
            string yyyy = TextBox10.Text.Substring(0, 4);
            string MM = TextBox10.Text.Substring(4, 2);
            string dd = TextBox10.Text.Substring(6, 2);

            string strDate = yyyy + "/" + MM + "/" + dd;
            DateTime dtDate;

            if (DateTime.TryParse(strDate, out dtDate))
            {
                
            }
            else
            {
                LabelMESSAGES.Text = "不是正確的日期格式型別！";

            }
        }
        else
        {
            LabelMESSAGES.Text = "不是正確的日期格式型別！";

        }

        if (string.IsNullOrEmpty(LabelMESSAGES.Text) && !string.IsNullOrEmpty(TextBox7.Text)&& !string.IsNullOrEmpty(TextBox8.Text) && !string.IsNullOrEmpty(TextBox9.Text) && !string.IsNullOrEmpty(TextBox10.Text))
        {
            txtFieldValue2.Text += GetXML(Guid.NewGuid().ToString());

            string returnValue = string.Format("<Return>{0}</Return>", txtFieldValue2.Text);

            XElement xe = XElement.Parse(txtFieldValue.Text);
            XElement returnXe = XElement.Parse(returnValue);
            var nodes = (from xl in returnXe.Elements("Item")
                         select xl);

            xe.Add(nodes);


            // < FieldValue tel='' >
            // <Item id=xx txt1='' txt2='' txt3='' />
            // <Item id=xx txt1='' txt2='' txt3='' />
            //      <Item id=xx txt1='' txt2='' txt3='' />
            //      <Item id=xx txt1='' txt2='' txt3=''/>
            //</ FieldValue >

            txtFieldValue.Text = xe.ToString();

            txtFieldValue2.Text = "";
            TextBox7.Text = "";
            TextBox8.Text = "";
            TextBox9.Text = "";
            TextBox10.Text = "";
            TextBox11.Text = "";

            BindGrid();
        }
        

    }
    #endregion


}