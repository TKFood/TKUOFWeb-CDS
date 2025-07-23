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
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public partial class CDS_WebPage_IT_Chat : Ede.Uof.Utility.Page.BasePage
{

    //{
    //  "message": "You exceeded your current quota, please check your plan and billing details. For more information on this error, read the docs: https://platform.openai.com/docs/guides/error-codes/api-errors."
    //}
    //尚未設定計費資訊（你雖有付費 ChatGPT，但 API 是分開計費的）
    //免費試用額度已用完
    //你付費的 ChatGPT 是 ChatGPT Plus（僅限 ChatGPT 網頁使用），不是 API 方案

    string ACCOUNT = null;
    string NAME = null;
    string ROLES = null;

    private string apiKey = ConfigurationManager.AppSettings["OpenAIApiKey"];

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    #region FUNCTION

    public void MsgBox(String ex, Page pg, Object obj)
    {
        string script = "alert('" + ex.Replace("\r\n", "\\n").Replace("'", "") + "');";
        ScriptManager.RegisterStartupScript(pg, obj.GetType(), "AlertScript", script, true);

        //string s = "<SCRIPT language='javascript'>alert('" + ex.Replace("\r\n", "\\n").Replace("'", "") + "'); </SCRIPT>";
        //Type cstype = obj.GetType();
        //ClientScriptManager cs = pg.ClientScript;
        //cs.RegisterClientScriptBlock(cstype, s, s.ToString());
    }
    #endregion

    #region BUTTON

    protected async void btnSend_Click(object sender, EventArgs e)
    {
        string prompt = txtPrompt.Text;

        var client = new HttpClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

        var requestBody = new
        {
            model = "gpt-3.5-turbo",
            messages = new[]
            {
                    new { role = "user", content = prompt }
                }
        };

        var content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");

        try
        {
            var response = await client.PostAsync("https://api.openai.com/v1/chat/completions", content);
            var json = await response.Content.ReadAsStringAsync();

            JObject result = JObject.Parse(json);

            string reply = "無法取得回覆";

            if (result["choices"] != null && result["choices"].HasValues)
            {
                var message = result["choices"][0]["message"];
                if (message != null && message["content"] != null)
                {
                    reply = message["content"].ToString();
                }
            }

            txtResponse.Text = "<b>ChatGPT 回答：</b><br/><pre>" + Server.HtmlEncode(reply) + "</pre>";
        }
        catch (Exception ex)
        {
            txtResponse.Text = "<span style='color:red;'>發生錯誤：" + ex.Message + "</span>";
        }
    }

    #endregion
}