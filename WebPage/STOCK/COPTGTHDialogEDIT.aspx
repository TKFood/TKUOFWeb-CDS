<%@ Page Title="" Language="C#" MasterPageFile="~/Master/DialogMasterPage.master" AutoEventWireup="true" CodeFile="COPTGTHDialogEDIT.aspx.cs" Inherits="CDS_WebPage_STOCK_COPTGTHDialogEDIT" %>

<%@ Register Src="~/Common/HtmlEditor/UC_HtmlEditor.ascx" TagPrefix="uc1" TagName="UC_HtmlEditor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <link rel="stylesheet" href="http://code.jquery.com/ui/1.10.4/themes/trontastic/jquery-ui.css">

    <script>
        $(function () {

        });

    </script>
    <div style="overflow-x: auto; width: 800px">
    </div>
    <table class="PopTable">
        <tr>
            <td class="PopTableLeftTD">
                <asp:Label ID="Label1" runat="server" Text="銷貨單別-單號"></asp:Label>
            </td>
            <td class="PopTableRightTD" colspan="2">
                <asp:Label ID="lblParam" runat="server" Text=""></asp:Label>
            </td>
        </tr>

    </table>
    <table>
        <div>

            <tr>
                <th align="center"><u>Live Camera</u></th>
                <th align="center"><u>Captured Picture</u></th>
            </tr>
            <tr>
                <td>
                    <div id="webcam"></div>
                </td>
                <td>
                    <img id="imgCapture" /></td>
            </tr>
            <tr>
                <td align="center">
                    <input type="button" id="btnCapture" value="Capture" />
                </td>
                <td align="center">
                    <input type="button" id="btnUpload" value="Upload" />
                </td>
            </tr>

        </div>
    </table>
    <script src="WebCam/WebCam.js"></script>

     <script>    
         //JS GetName
         function GetName() {
             PageMethods.Name(Success, Failure);
         }

         //JS執行成功Success
         function Success(result) {
             alert(result);
         }
         //JS執行失敗
         function Failure(error) {
             alert(error);
         }

         $(function () {
             Webcam.set({
                 width: 160,
                 height: 120,
                 image_format: 'jpeg',
                 jpeg_quality: 90
             });
             Webcam.attach('#webcam');
             $("#btnCapture").click(function () {
                 Webcam.snap(function (data_uri) {
                     $("#imgCapture")[0].src = data_uri;

                 });
             });
             $("#btnUpload").click(function () {              
                    var imgCapture = $("#imgCapture")[0].src;
                    PageMethods.SaveCapturedImage(myTextcontent, imgCapture, Success, Failure);

                    //$.ajax({
                    //    type: "POST",
                    //    url: "Mobile_Test3.aspx/SaveCapturedImage",
                    //    data: "{data: '" + $("#imgCapture")[0].src + "'}",
                    //    contentType: "application/json; charset=utf-8",
                    //    dataType: "json",
                    //    success: function (r) { }
                    //});
                });
            });
        </script>
     


</asp:Content>


