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
using System.Net.Mail;
using System.Threading.Tasks;
using Ede.Uof.Utility.FileCenter.V3;

public partial class CDS_WebPage_QC_TK_TEMP_HUMI_LOG : Ede.Uof.Utility.Page.BasePage
{
    string ACCOUNT = null;
    string NAME = null;
    String ROLES = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // 第一次載入時先查詢一次
            BindData();
        }
    }

    #region FUNCTION
    // 每次 Timer 到期時觸發的事件
    protected void Timer1_Tick(object sender, EventArgs e)
    {
        BindData(); // 重新向資料庫查詢並繫結資料
    }

    private void BindData()
    {
        lblStatus.Text = "最後更新時間：" + DateTime.Now.ToString("HH:mm:ss");
        // GridView1.DataSource = ... 取得資料庫資料
        // GridView1.DataBind();
    }
    #endregion


    #region BUTTON

    #endregion

}