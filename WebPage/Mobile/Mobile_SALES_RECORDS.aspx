<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MobileMasterPage.master" AutoEventWireup="true" CodeFile="Mobile_SALES_RECORDS.aspx.cs" Inherits="CDS_WebPage_Mobile_SALES_RECORDS" %>

<%@ Register Assembly="Ede.Uof.Utility.Component.Grid" Namespace="Ede.Uof.Utility.Component" TagPrefix="Fast" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!-- 在CSS文件中定义样式 -->
    <style>
        .custom-button {
            background-color: #FF5733;
            color: #FFFFFF;
            width: 150px; /* 设置宽度 */
            height: 40px; /* 设置高度 */
            font-size: 16px; /* 设置字体大小 */
        }
    </style>
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
                                <h2>拜訪</h2>
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label3" runat="server" Text="業務員"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="DropDownListSALESNAMES" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownListSALESNAMES_SelectedIndexChanged" Style="width: 200px;"></asp:DropDownList>
                                <asp:Label ID="SALESID" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label1" runat="server" Text="舊客戶(有客代): "></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label6" runat="server" Text="關鍵字 "></asp:Label>
                                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                <asp:Button ID="Button1" runat="server" Text="查客戶 " OnClick="btn1_Click" meta:resourcekey="btn1_Resource1" />
                                <asp:DropDownList ID="DropDownListCLIENTSNAMES" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownListCLIENTSNAMES_SelectedIndexChanged" Style="width: 200px;"></asp:DropDownList>
                                <asp:Label ID="CLIENTSID" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label2" runat="server" Text="新客戶(無客代): "></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="NEWCLIENTSNAMES" runat="server" Style="width: 200px;"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label7" runat="server" Text="拜訪目的: "></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="DropDownListKINDS" runat="server" AutoPostBack="true" Style="width: 200px;"></asp:DropDownList>
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
                                <button type="button" id="btnUpload" class="custom-button">存檔</button>
                            </td>
                        </tr>
                    </table>

                </div>
            </telerik:RadPageView>
            <telerik:RadPageView ID="RadPageView2" runat="server" Selected="false">
                <div id="tabs-2">
                    <table class="PopTable">
                        <tr>
                            <td class="PopTableLeftTD">
                                <asp:Label ID="Label8" runat="server" Text="日期:" meta:resourcekey="Label4Resource1"></asp:Label>
                            </td>
                            <td class="PopTableRightTD">
                                <asp:TextBox ID="txtDate1" runat="server" Width="100px"></asp:TextBox>
                                <asp:Label ID="Label11" runat="server" Text="~"></asp:Label>
                                <asp:TextBox ID="txtDate2" runat="server" Width="100px"></asp:TextBox>
                                <asp:Label ID="Label12" runat="server" Text=" "></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="PopTableLeftTD"></td>
                            <td>
                                <asp:Button ID="Button2" runat="server" Text=" 查詢 " OnClick="btn2_Click" meta:resourcekey="btn2_Resource1" />
                            </td>
                        </tr>
                        <tr>
                            <td class="PopTableLeftTD"></td>
                            <td>
                                <asp:Button ID="Button3" runat="server" Text=" 匯出 " OnClick="btn3_Click" meta:resourcekey="btn3_Resource1" />
                            </td>
                        </tr>

                    </table>
                    <table class="PopTable">
                        <tr>
                            <td colspan="2" class="PopTableRightTD">
                                <div style="overflow-x: auto; width: 100%">
                                    <Fast:Grid ID="Grid1" OnRowDataBound="Grid1_RowDataBound" OnRowCommand="Grid1_OnRowCommand" runat="server" OnBeforeExport="OnBeforeExport1" AllowPaging="true" AutoGenerateCheckBoxColumn="False" AllowSorting="True" AutoGenerateColumns="False" CustomDropDownListPage="False" DataKeyOnClientWithCheckBox="False" DefaultSortDirection="Ascending" EmptyDataText="No data found" EnhancePager="True" KeepSelectedRows="False" PageSize="100" SelectedRowColor="" UnSelectedRowColor="" meta:resourcekey="Grid1Resource1" OnPageIndexChanging="grid_PageIndexChanging1">
                                        <EnhancePagerSettings FirstImageUrl="" FirstAltImageUrl="" PreviousImageUrl="" NextImageUrl="" LastImageUrl="" LastAltImage="" PageNumberCssClass="" PageNumberCurrentCssClass="" PageInfoCssClass="" PageRedirectCssClass="" NextIAltImageUrl="" PreviousAltImageUrl="" ShowHeaderPager="True"></EnhancePagerSettings>
                                        <ExportExcelSettings AllowExportToExcel="true" ExportType="DataSource"></ExportExcelSettings>
                                        <Columns>
                                            <asp:BoundField HeaderText="業務員" DataField="業務員" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="客戶" DataField="客戶" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="新客" DataField="新客" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="拜訪目的" DataField="拜訪目的" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="訪談內容" DataField="訪談內容" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle HorizontalAlign="Left" Width="300px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="訪談日期" DataField="訪談日期" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="建立日期" DataField="建立日期" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="Image">
                                                <ItemTemplate>
                                                    <asp:Image ID="Image1" runat="server" Width="100px" Height="100px" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </Fast:Grid>
                                </div>
                            </td>
                        </tr>
                    </table>
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
                // DropDownListSALESNAMES获取 DropDownList 的元素
                var SALESNAMES = document.getElementById('<%=DropDownListSALESNAMES.ClientID%>');
                // 获取选中项的索引
                var selectedIndex_SALESNAMES = SALESNAMES.selectedIndex;
                // 获取选中项的文本
                var selectedText_SALESNAMES = SALESNAMES.options[selectedIndex_SALESNAMES].text;

                //DropDownListCLIENTSNAMES 获取 DropDownList 的元素
                var CLIENTSNAMES = document.getElementById('<%=DropDownListCLIENTSNAMES.ClientID%>');
                // 获取选中项的索引
                var selectedIndex_CLIENTSNAMES = CLIENTSNAMES.selectedIndex;
                // 获取选中项的文本
                var selectedText_CLIENTSNAMES = CLIENTSNAMES.options[selectedIndex_CLIENTSNAMES].text;

                //DropDownListKINDS 获取 DropDownList 的元素
                var KINDS = document.getElementById('<%=DropDownListKINDS.ClientID%>');
                // 获取选中项的索引
                var selectedIndex_KINDS = KINDS.selectedIndex;
                // 获取选中项的文本
                var selectedText_KINDS = KINDS.options[selectedIndex_KINDS].text;

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
                            PageMethods.SaveCapturedImage(selectedText_SALESNAMES, CLIENTSID, selectedText_CLIENTSNAMES, NEWCLIENTSNAMES, selectedText_KINDS, RECORDS, RECORDSDATES, compressedBase64, Success, Failure);
                        };
                        reader.readAsDataURL(compressedBlob);
                    });
                }
                else {
                    PageMethods.SaveCapturedImage_NOIMAGE(selectedText_SALESNAMES, CLIENTSID, selectedText_CLIENTSNAMES, NEWCLIENTSNAMES, selectedText_KINDS, RECORDS, RECORDSDATES, Success, Failure);
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

