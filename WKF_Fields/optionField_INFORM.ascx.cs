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
using System.Text;
using System.Data.SqlClient;

public partial class WKF_OptionalFields_optionField_INFORM : WKF_FormManagement_VersionFieldUserControl_VersionFieldUC
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
            //回傳字串
			//取得表單欄位填寫的內容
			return String.Empty;
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

            //若有擴充屬性，可以用該屬性存取
            // fieldOptional.ExtensionSetting

            
            //草稿
            if(!fieldOptional.IsAudit)
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

    public void ADDToUOF_TB_EIP_PRIV_MESS(string MESSAGE_TO, string MESSAGE_FROM, string TOPIC, string CONTENT)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();

        Guid MESSAGE_GUID = Guid.NewGuid();
        Guid MASTER_GUID = Guid.NewGuid();
        Guid NOTIFY_ID = Guid.NewGuid();

        StringBuilder SBTEXT = new StringBuilder();

        //MESSAGE_TO = "b6f50a95-17ec-47f2-b842-4ad12512b431";
        //MESSAGE_FROM = "b6f50a95-17ec-47f2-b842-4ad12512b431";
        //TOPIC = "TEST";

        //SBTEXT.AppendFormat(@"
        //                    <p style=""font-size:160%;color:red;"">This is a paragraph.</p>
        //                    <br></br>
        //                    <p style=""font-size:160%;color:blue;"">This is a paragraph.</p>
        //                      ");
        //CONTENT = SBTEXT.ToString();

        string CREATOR = MESSAGE_FROM;
        string MODIFIER = MESSAGE_FROM;
        string MESSAGE_TOUSER = @"<UserSet><Element type=""user""><userId>b6f50a95-17ec-47f2-b842-4ad12512b431</userId></Element></UserSet>";
        string MESSAGE_CONTENT = CONTENT;
        string TB_EIP_PRIV_MESS_CREATE_TIME = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss ") + "+08:00";
        string CREATE_TIME = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
        string SENDER_TIME = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
        string CREATE_FROM = "192.168.1.57";
        string MODIFY_FROM = "192.168.1.57";
        string CREATE_DATE = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
        string MODIFY_DATE = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
        string USER_GUID = MESSAGE_FROM;
        string TITLE = TOPIC;

        try
        {
            StringBuilder queryString = new StringBuilder();
            queryString.AppendFormat(@"

                                    INSERT [UOFTEST].[dbo].TB_EIP_PRIV_MESS
                                    ( 
                                    MESSAGE_GUID
                                    , TOPIC
                                    , MESSAGE_CONTENT
                                    , MESSAGE_TO
                                    , MESSAGE_FROM
                                    , CREATE_TIME
                                    , FROM_DELETED
                                    , TO_DELETED
                                    , FILE_GROUP_ID
                                    , MASTER_GUID 
                                    ) 
                                    VALUES 
                                    ( 
                                    @MESSAGE_GUID
                                    , @TOPIC
                                    , @MESSAGE_CONTENT
                                    , @MESSAGE_TO
                                    , @MESSAGE_FROM
                                    , @TB_EIP_PRIV_MESS_CREATE_TIME
                                    , 0
                                    , 0
                                    , N''
                                    , @MASTER_GUID
                                    )

                                    INSERT [UOFTEST].[dbo].TB_EIP_PRIV_MESS_MASTER
                                    ( 
                                    MASTER_GUID
                                    , TOPIC
                                    , MESSAGE_FROM
                                    , MESSAGE_TO
                                    , SENDER_TIME
                                    , CREATOR
                                    , CREATE_FROM
                                    , CREATE_DATE
                                    , MODIFIER
                                    , MODIFY_FROM
                                    , MODIFY_DATE
                                    ) 
                                    VALUES 
                                    (  
                                    @MESSAGE_GUID
                                    ,@TOPIC
                                    ,@MESSAGE_FROM
                                    ,@MESSAGE_TOUSER
                                    ,@SENDER_TIME
                                    ,@CREATOR 
                                    ,@CREATE_FROM
                                    ,@CREATE_DATE
                                    ,@MODIFIER
                                    ,@MODIFY_FROM
                                    ,@MODIFY_DATE
                                    )

                                    INSERT [UOFTEST].[dbo].TB_EIP_PUSH_QUEUE
                                    ( 
                                    [NOTIFY_ID]
                                    , [USER_GUID]
                                    , [DESCRIPTION]
                                    , [TITLE]
                                    , [DISPLAY_NUMBER]
                                    , [MODULE_NAME]
                                    , [MODULE_KEY]
                                    , [CREATOR]
                                    , [CREATE_FROM]
                                    , [CREATE_DATE]
                                    , [MODIFIER]
                                    , [MODIFY_FROM]
                                    , [MODIFY_DATE]
                                    )
                                    VALUES
                                    (
                                    @NOTIFY_ID
                                    , @USER_GUID
                                    , N''
                                    ,@TITLE
                                    , 1
                                    , N'PrivateMessage'
                                    , N'PrivateMessage?id=@MESSAGE_GUID'
                                    ,@CREATOR
                                    , @CREATE_FROM
                                    , @CREATE_DATE
                                    , @MODIFIER
                                    , @MODIFY_FROM
                                    , @MODIFY_DATE
                                    )

                                        ");

          
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                SqlCommand command = new SqlCommand(queryString.ToString(), connection);
            
                command.Parameters.AddWithValue("@MESSAGE_GUID", MESSAGE_GUID);
                command.Parameters.AddWithValue("@MASTER_GUID", MASTER_GUID);
                command.Parameters.AddWithValue("@NOTIFY_ID", NOTIFY_ID);
                command.Parameters.AddWithValue("@MESSAGE_FROM", MESSAGE_FROM);
                command.Parameters.AddWithValue("@MESSAGE_TO", MESSAGE_TO);
                command.Parameters.AddWithValue("@MESSAGE_TOUSER", MESSAGE_TOUSER);
                command.Parameters.AddWithValue("@TOPIC", TOPIC);
                command.Parameters.AddWithValue("@MESSAGE_CONTENT", MESSAGE_CONTENT);
                command.Parameters.AddWithValue("@CREATE_TIME", CREATE_TIME);
                command.Parameters.AddWithValue("@SENDER_TIME", SENDER_TIME);
                command.Parameters.AddWithValue("@CREATOR", CREATOR);
                command.Parameters.AddWithValue("@MODIFIER", MODIFIER);
                command.Parameters.AddWithValue("@CREATE_FROM", CREATE_FROM);
                command.Parameters.AddWithValue("@MODIFY_FROM", MODIFY_FROM);
                command.Parameters.AddWithValue("@CREATE_DATE", CREATE_DATE);
                command.Parameters.AddWithValue("@MODIFY_DATE", MODIFY_DATE);
                command.Parameters.AddWithValue("@TITLE", TITLE);
                command.Parameters.AddWithValue("@USER_GUID", USER_GUID);
                command.Parameters.AddWithValue("@TB_EIP_PRIV_MESS_CREATE_TIME", TB_EIP_PRIV_MESS_CREATE_TIME);

                command.Connection.Open();

                int count = command.ExecuteNonQuery();

                connection.Close();
                connection.Dispose();

            }
          

        }
        catch
        {

        }

        finally
        {
          
        }

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        MsgBox("通知申請人", this.Page, this);
    }

    public void MsgBox(String ex, Page pg, Object obj)
    {
        ADDToUOF_TB_EIP_PRIV_MESS("c24e7a6a-3699-4e18-9e11-a90e09fd0ac1", "c24e7a6a-3699-4e18-9e11-a90e09fd0ac1", "總經理呼叫 表單 -測試通知", "總經理呼叫 表單-測試通知");
        
        string s = "<SCRIPT language='javascript'>alert('" + ex.Replace("\r\n", "\\n").Replace("'", "") + "'); </SCRIPT>";
        Type cstype = obj.GetType();
        ClientScriptManager cs = pg.ClientScript;
        cs.RegisterClientScriptBlock(cstype, s, s.ToString());
    }
}