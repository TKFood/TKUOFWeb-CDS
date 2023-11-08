<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MobileMasterPage.master" AutoEventWireup="true" CodeFile="Mobile_SALES_RECORDS.aspx.cs" Inherits="CDS_WebPage_Mobile_SALES_RECORDS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <html>

    <head>
        <title>業務</title>
    </head>
    <body>
        <telerik:RadTabStrip ID="RadTabStrip1" runat="server"></telerik:RadTabStrip>
        <telerik:RadTabStrip ID="RadTabStrip2" runat="server" MultiPageID="RadMultiPage" SelectedIndex="0">
            <Tabs>
                <telerik:RadTab Text="業務">
                </telerik:RadTab>
                <telerik:RadTab Text="資料">
                </telerik:RadTab>
            </Tabs>
        </telerik:RadTabStrip>
        <telerik:RadMultiPage ID="RadMultiPage" runat="server" SelectedIndex="0">
            <telerik:RadPageView ID="RadPageView1" runat="server" Selected="true">
                <div id="tabs-1">
                    <table class="PopTable">
                        <tr>
                            <td>
                                <h2>拜訪拍照</h2>
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label3" runat="server" Text="業務"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="SALESNAMES" runat="server" AutoPostBack="true" OnSelectedIndexChanged="SALESNAMES_SelectedIndexChanged" style="width: 200px;"></asp:DropDownList>
                                <asp:Label ID="SALESID" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label1" runat="server" Text="舊客戶(有客代): "></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="CLIENTSNAMES" runat="server" AutoPostBack="true" OnSelectedIndexChanged="CLIENTSNAMES_SelectedIndexChanged" style="width: 200px;"></asp:DropDownList>
                                <asp:Label ID="CLIENTSID" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label2" runat="server" Text="新客戶(無客代): " ></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="NEWCLIENTSNAMES" runat="server" style="width: 200px;"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label4" runat="server" Text="客戶拜訪內容: "></asp:Label>
                            </td>
                            <td>
                               <asp:TextBox ID="RECORDS" runat="server" TextMode="MultiLine" Rows="4" Columns="40"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label5" runat="server" Text="客戶拜訪日期 "></asp:Label>

                            </td>
                            <td>
                                <asp:TextBox ID="RECORDSDATES" runat="server" Width="100px"></asp:TextBox>
                            </td>
                        </tr>

                        <tr>
                            <td></td>
                            <td>
                                <input type="file" accept="image/*" capture="camera" id="photoInput" style="display: none" />
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <button type="button" id="takePhotoButton">拍照</button>
                            </td>                   
                        </tr>
                        <tr>
                            <td>照片
                            </td>
                            <td>
                                <img id="previewImage" style="max-width: 50%;" />
                            </td>
                        </tr>

                        <tr>
                            <td></td>
                            <td>
                                <button type="button" id="btnUpload">上傳存檔</button>
                            </td>
                        </tr>
                    </table>

                </div>
            </telerik:RadPageView>
            <telerik:RadPageView ID="RadPageView2" runat="server" Selected="false">
                <div id="tabs-1">
                </div>
            </telerik:RadPageView>
        </telerik:RadMultiPage>​
            <div>
            </div>
    </body>


    <script>
        window.onload = function () {
            var photoInput = document.getElementById("photoInput");
            var takePhotoButton = document.getElementById("takePhotoButton");
            var previewImage = document.getElementById("previewImage");

            takePhotoButton.onclick = function () {
                photoInput.click();
            };

            photoInput.onchange = function () {
                if (photoInput.files && photoInput.files[0]) {
                    var reader = new FileReader();
                    reader.onload = function (e) {
                        previewImage.src = e.target.result;
                    };
                    reader.readAsDataURL(photoInput.files[0]);
                }
            };
        };

        //JS執行成功Success
        function Success(result) {
            alert(result);
        }
        //JS執行失敗
        function Failure(error) {
            alert(error);
        }

        $(function () {
            $("#btnUpload").click(function () {
                // 获取 DropDownList 的元素
                var SALESNAMES = document.getElementById('<%=SALESNAMES.ClientID%>');            
                // 获取选中项的索引
                var selectedIndex_SALESNAMES = SALESNAMES.selectedIndex;
                // 获取选中项的文本
                var selectedText_SALESNAMES = SALESNAMES.options[selectedIndex_SALESNAMES].text;
                // 获取 DropDownList 的元素
                var CLIENTSNAMES = document.getElementById('<%=CLIENTSNAMES.ClientID%>');
                // 获取选中项的索引
                var selectedIndex_CLIENTSNAMES = CLIENTSNAMES.selectedIndex;
                // 获取选中项的文本
                var selectedText_CLIENTSNAMES = CLIENTSNAMES.options[selectedIndex_CLIENTSNAMES].text;
               
                var CLIENTSID = document.getElementById('<%=CLIENTSID.ClientID%>').innerHTML;
                var NEWCLIENTSNAMES = document.getElementById('<%=NEWCLIENTSNAMES.ClientID%>').value;
                var RECORDS = document.getElementById('<%=RECORDS.ClientID%>').value;
                var RECORDSDATES = document.getElementById('<%=RECORDSDATES.ClientID%>').value;

                var previewImage = document.getElementById("previewImage");
                var imgCapture = $("#previewImage")[0].src;


                // 壓縮圖片的函數
                function compressImage(image, quality, callback) {
                    const canvas = document.createElement('canvas');
                    const context = canvas.getContext('2d');
                    canvas.width = image.width;
                    canvas.height = image.height;
                    context.drawImage(image, 0, 0, canvas.width, canvas.height);

                    canvas.toBlob(function (blob) {
                        callback(blob);
                    }, 'image/jpeg', quality);
                }

                //圖片!== ""
                if (imgCapture !== "" && imgCapture !== undefined) {
                    // 壓縮圖片並使用 PageMethods.SaveCapturedImage 上傳
                    compressImage(previewImage, 0.5, function (compressedBlob) {
                        // 將壓縮後的圖片轉換為Base64字串
                        const reader = new FileReader();
                        reader.onload = function () {
                            const compressedBase64 = reader.result;
                            // 使用 PageMethods.SaveCapturedImage 上傳壓縮後的圖片
                            PageMethods.SaveCapturedImage(selectedText_SALESNAMES, CLIENTSID, selectedText_CLIENTSNAMES, NEWCLIENTSNAMES, RECORDS, RECORDSDATES, compressedBase64, Success, Failure);
                        };
                        reader.readAsDataURL(compressedBlob);
                    });
                }
                else {
                    PageMethods.SaveCapturedImage_NOIMAGE(selectedText_SALESNAMES, CLIENTSID, selectedText_CLIENTSNAMES, NEWCLIENTSNAMES, RECORDS, RECORDSDATES, Success, Failure);
                    reader.readAsDataURL(compressedBlob);
                }
               

                //PageMethods.SaveCapturedImage(myTextcontent, imgCapture, Success, Failure);
                //PageMethods.TEST(Success, Failure);

            });
        });
        $(function () {
            $("#<%= RECORDSDATES.ClientID %>").datepicker({ dateFormat: "yy/mm/dd", });

        });
    </script>

    </html>



</asp:Content>

