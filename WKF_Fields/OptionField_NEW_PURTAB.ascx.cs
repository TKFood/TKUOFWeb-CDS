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

public partial class WKF_OptionalFields_OptionField_NEW_PURTAB : WKF_FormManagement_VersionFieldUserControl_VersionFieldUC
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

    protected void Page_Load(object sender, EventArgs e)
    {
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
            if (string.IsNullOrEmpty(txtFieldValue.Text) || txtFieldValue.Text == "<FieldValue/>")
            {
                return "<FieldValue/>";
            }
            return txtFieldValue.Text;
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
            if (string.IsNullOrEmpty(fieldOptional.FieldValue))
            {
                txtFieldValue.Text = "<FieldValue/>";
            }
            else
            {
                txtFieldValue.Text = fieldOptional.FieldValue;

                // 綁定單身 Grid
                BindGrid();
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

    public DataTable FIND_ERP_PURTA_PURTB(string TA002)
    {
        // 1.取得連線字串
        // 請將 "YourConnectionStringName" 替換為 Web.config 中定義的連線名稱
        string connectionString = ConfigurationManager.ConnectionStrings["ERPconnectionstring"].ConnectionString;
        Ede.Uof.Utility.Data.DatabaseHelper m_db = new Ede.Uof.Utility.Data.DatabaseHelper(connectionString);

        // 2. 定義 SQL 查詢字串           
        string cmdTxt = @"
                        SELECT *
                        FROM [TK].dbo.PURTA,[TK].dbo.PURTB
                        WHERE TA001=TB001 AND TA002=TB002 
                        AND TA002=@TA002
                        ";

        m_db.AddParameter("@TA002", TA002);


        DataTable dt = new DataTable();

        dt.Load(m_db.ExecuteReader(cmdTxt.ToString()));

        if(dt!=null && dt.Rows.Count>=1)
        {
            return dt;
        }
        else
        {
            return null;
        }
    }

    /// <summary>
    /// 解析 XML 並重新綁定 Grid
    /// </summary>
    private void BindGrid()
    {
        if (string.IsNullOrEmpty(txtFieldValue.Text)) return;

        XElement xe = XElement.Parse(txtFieldValue.Text);

        DataTable dt = new DataTable();
        dt.Columns.Add("ID");
        dt.Columns.Add("TA001");
        dt.Columns.Add("TA002");
        dt.Columns.Add("TB003");
        dt.Columns.Add("TB004");
        dt.Columns.Add("TB005");

        var items = xe.Elements("Item");
        foreach (var item in items)
        {
            XAttribute attrId = item.Attribute("id");
            XAttribute attrTA001 = item.Attribute("TA001");
            XAttribute attrTA002 = item.Attribute("TA002");
            XAttribute attrTB003 = item.Attribute("TB003");
            XAttribute attrTB004 = item.Attribute("TB004");
            XAttribute attrTB005 = item.Attribute("TB005");

            string id = (attrId != null) ? attrId.Value : string.Empty;
            string ta001 = (attrTA001 != null) ? attrTA001.Value : string.Empty;
            string ta002 = (attrTA002 != null) ? attrTA002.Value : string.Empty;
            string tb003 = (attrTB003 != null) ? attrTB003.Value : string.Empty;
            string tb004 = (attrTB004 != null) ? attrTB004.Value : string.Empty;
            string tb005 = (attrTB005 != null) ? attrTB005.Value : string.Empty;

            // ✅ 補齊 6 個欄位參數
            dt.Rows.Add(id, ta001, ta002, tb003, tb004, tb005);
        }

        Grid1.DataSource = dt;
        Grid1.DataBind();
    }

    /// <summary>
    /// 新增 TB005 至 XML 並更新 Grid
    /// </summary>
    protected void btnAddDetail_Click(object sender, EventArgs e)
    {
        lblMessage.Text = string.Empty;

        string tb005Value = txtTB005Input.Text.Trim();
        if (string.IsNullOrEmpty(tb005Value))
        {
            lblMessage.Text = "請輸入 TB005 資料！";
            return;
        }

        XElement xe = string.IsNullOrEmpty(txtFieldValue.Text) || txtFieldValue.Text == "<FieldValue/>"
            ? new XElement("FieldValue")
            : XElement.Parse(txtFieldValue.Text);

        // 建立單身 Item 節點 (僅記錄 TB005)
        XElement newItem = new XElement("Item",
            new XAttribute("id", Guid.NewGuid().ToString()),
            new XAttribute("TA001", tb005Value),
            new XAttribute("TA002", tb005Value),
            new XAttribute("TB003", tb005Value),
            new XAttribute("TB004", tb005Value),
            new XAttribute("TB005", tb005Value)
        );

        xe.Add(newItem);
        txtFieldValue.Text = xe.ToString();

        // 清空輸入欄位並重新綁定
        txtTB005Input.Text = string.Empty;
        BindGrid();
    }

    /// <summary>
    /// 刪除 Grid 勾選的列
    /// </summary>
    protected void btnDeleteDetail_Click(object sender, EventArgs e)
    {
        string[] selectedIds = Grid1.GetSelectedRowGUIDs();
        if (selectedIds == null || selectedIds.Length == 0) return;

        XElement xe = XElement.Parse(txtFieldValue.Text);

        foreach (string id in selectedIds)
        {
            XElement targetItem = xe.Elements("Item").FirstOrDefault(x => (string)x.Attribute("id") == id);
            if (targetItem != null)
            {
                targetItem.Remove();
            }
        }

        txtFieldValue.Text = xe.ToString();
        BindGrid();
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        lblMessage.Text = string.Empty;
        string TA002 = TextBox1.Text.Trim();

        if (!string.IsNullOrEmpty(TA002))
        {
            DataTable dt = FIND_ERP_PURTA_PURTB(TA002);

            if (dt != null && dt.Rows.Count > 0)
            {
                XElement xe;
                if (string.IsNullOrEmpty(txtFieldValue.Text) || txtFieldValue.Text == "<FieldValue/>")
                {
                    xe = new XElement("FieldValue");
                }
                else
                {
                    xe = XElement.Parse(txtFieldValue.Text);
                }

                int addedCount = 0;
                foreach (DataRow row in dt.Rows)
                {
                    // C# 5.0 安全取值 (轉換 DBNull 與 null)
                    string TA001Value = (row["TA001"] != DBNull.Value && row["TA001"] != null) ? row["TA001"].ToString().Trim() : string.Empty;
                    string TA002Value = (row["TA002"] != DBNull.Value && row["TA002"] != null) ? row["TA002"].ToString().Trim() : string.Empty;
                    string TB003Value = (row["TB003"] != DBNull.Value && row["TB003"] != null) ? row["TB003"].ToString().Trim() : string.Empty;
                    string TB004Value = (row["TB004"] != DBNull.Value && row["TB004"] != null) ? row["TB004"].ToString().Trim() : string.Empty;
                    string tb005Value = (row["TB005"] != DBNull.Value && row["TB005"] != null) ? row["TB005"].ToString().Trim() : string.Empty;

                    // 只要 TB005 有值（或改為判斷其他必填欄位）就匯入 XML
                    if (!string.IsNullOrEmpty(tb005Value))
                    {
                        XElement newItem = new XElement("Item",
                            new XAttribute("id", Guid.NewGuid().ToString()),
                            new XAttribute("TA001", TA001Value),
                            new XAttribute("TA002", TA002Value),
                            new XAttribute("TB003", TB003Value),
                            new XAttribute("TB004", TB004Value),
                            new XAttribute("TB005", tb005Value)
                        );

                        xe.Add(newItem);
                        addedCount++;
                    }
                }

                if (addedCount > 0)
                {
                    txtFieldValue.Text = xe.ToString();
                    BindGrid();
                    Label2.Text = string.Format("已成功匯入 {0} 筆明細", addedCount);
                }
                else
                {
                    lblMessage.Text = "查詢結果中的 TB005 欄位皆為空值！";
                }
            }
            else
            {
                lblMessage.Text = "查無對應的 ERP 單據資料！";
            }
        }
        else
        {
            lblMessage.Text = "請輸入單號！";
        }
    }
}