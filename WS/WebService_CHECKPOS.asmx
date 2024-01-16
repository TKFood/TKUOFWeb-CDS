<%@ WebService Language="C#" Class="WebService_CHECKPOS" %>

using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Data;
using System.Xml;

[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// 若要允許使用 ASP.NET AJAX 從指令碼呼叫此 Web 服務，請取消註解下列一行。
// [System.Web.Script.Services.ScriptService]
public class WebService_CHECKPOS : System.Web.Services.WebService
{

    //[WebMethod]
    //public string GetFieldValue(string formInfo)
    //{
    //}



    //[WebMethod]
    //public string GetFieldValueByDDL(string formInfo)
    //{
    //}


    [WebMethod]
    public string CheckedForm(string formInfo)
    {

        // - <Form formVersionId="684034be-3cf5-4d47-9710-2ae8a090875c">
        //- <FormFieldValue>
        //  <FieldItem fieldId="NO" fieldValue="" realValue="" /> 
        //  <FieldItem fieldId="A01" fieldValue="1" realValue="" fillerName="湯尼" fillerUserGuid="cb7b9862-6591-4b28-a6e6-c54d57eb00c7" fillerAccount="Tony" fillSiteId="" /> 
        //  <FieldItem fieldId="A02" fieldValue="1" realValue="" fillerName="湯尼" fillerUserGuid="cb7b9862-6591-4b28-a6e6-c54d57eb00c7" fillerAccount="Tony" fillSiteId="" /> 
        //  <FieldItem fieldId="A03" fieldValue="1" realValue="" fillerName="湯尼" fillerUserGuid="cb7b9862-6591-4b28-a6e6-c54d57eb00c7" fillerAccount="Tony" fillSiteId="" /> 
        //  <FieldItem fieldId="A04" fieldValue="" realValue="" /> 
        //  </FormFieldValue>
        //  </Form>
        formInfo = HttpUtility.UrlDecode(formInfo);

        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(formInfo);

        XmlDocument returnXmlDoc = new XmlDocument();
        XmlElement returnValueElement = returnXmlDoc.CreateElement("ReturnValue");
        XmlElement statusElement = returnXmlDoc.CreateElement("Status");
        XmlElement exceptionElement = returnXmlDoc.CreateElement("Exception");
        XmlElement messageElement = returnXmlDoc.CreateElement("Message");

        returnValueElement.AppendChild(statusElement);
        exceptionElement.AppendChild(messageElement);
        returnValueElement.AppendChild(exceptionElement);
        returnXmlDoc.AppendChild(returnValueElement);

        try
        {
            returnValueElement.SelectSingleNode("/ReturnValue/Status").InnerText = "0";
            returnValueElement.SelectSingleNode("/ReturnValue/Exception/Message").InnerText = "POS折扣有相同的品號，出現在重疊的日期";

        }
        catch (Exception ce)
        {
            returnValueElement.SelectSingleNode("/ReturnValue/Status").InnerText = "0";
            returnValueElement.SelectSingleNode("/ReturnValue/Exception/Message").InnerText = ce.Message;
        }

        return returnValueElement.OuterXml;
    }


    //[WebMethod]
    //public string FormSignEvent(string formInfo)
    //{
    //}


    //[WebMethod]
    //public string EndFormEvent(string formInfo)
    //{
    //}
}

