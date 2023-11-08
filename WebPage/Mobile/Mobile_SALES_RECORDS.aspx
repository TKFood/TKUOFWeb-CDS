<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MobileMasterPage.master" AutoEventWireup="true" CodeFile="Mobile_SALES_RECORDS.aspx.cs" Inherits="CDS_WebPage_Mobile_SALES_RECORDS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
  
    <html>

    <head>
        <title>業務</title>
    </head>
    <body>
        <div>
            <table>
                <tr>
                    <td>
                        <h2>拜訪拍照</h2>
                    </td>
                </tr>
                 <tr>
                    <td>
                        <asp:Label ID="Label3" runat="server" Text="業務"></asp:Label>
                        <asp:TextBox ID="SALESNAMES" runat="server"></asp:TextBox>
                    </td>
                </tr>
                 <tr>
                    <td>
                        <asp:Label ID="Label1" runat="server" Text="舊客戶(有客代): "></asp:Label>
                        <asp:TextBox ID="CLIENTSNAMES" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label2" runat="server" Text="新客戶(無客代): "></asp:Label>
                        <asp:TextBox ID="NEWCLIENTSNAMES" runat="server"></asp:TextBox>
                    </td>
                </tr>
                  <tr>
                    <td>
                        <asp:Label ID="Label4" runat="server" Text="客戶拜訪內容: "></asp:Label>
                        <asp:TextBox ID="RECORDS" runat="server"></asp:TextBox>
                    </td>
                </tr>
                  <tr>
                    <td>
                        <asp:Label ID="Label5" runat="server" Text="客戶拜訪日期 "></asp:Label>
                        <asp:TextBox ID="RECORDSDATES"  runat="server" Width = "100px"></asp:TextBox>
                       
                    </td>
                </tr>
                
                <tr>
                    <td>
                        <input type="file" accept="image/*" capture="camera" id="photoInput" style="display: none" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <button type="button" id="takePhotoButton">拍照</button>
                    </td>
                </tr>
                <tr>
                    <td>
                        <h2>照片</h2>
                    </td>
                </tr>
                <tr>
                    <td>
                        <img id="previewImage" style="max-width: 50%;" />
                    </td>
                </tr>
               
                <tr>
                    <td>
                        <button type="button" id="btnUpload">上傳存檔</button>
                    </td>
                </tr>
            </table>



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
                var SALESNAMES = document.getElementById('<%=SALESNAMES.ClientID%>').value;
                var CLIENTSNAMES = document.getElementById('<%=CLIENTSNAMES.ClientID%>').value;
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

                // 壓縮圖片並使用 PageMethods.SaveCapturedImage 上傳
                compressImage(previewImage, 0.5, function (compressedBlob) {
                    // 將壓縮後的圖片轉換為Base64字串
                    const reader = new FileReader();
                    reader.onload = function () {
                        const compressedBase64 = reader.result;

                        // 使用 PageMethods.SaveCapturedImage 上傳壓縮後的圖片
                        PageMethods.SaveCapturedImage(SALESNAMES, CLIENTSNAMES, NEWCLIENTSNAMES, RECORDS, RECORDSDATES, compressedBase64, Success, Failure);
                    };
                    reader.readAsDataURL(compressedBlob);
                });

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

