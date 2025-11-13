<%@ Page Title="" Language="C#" MasterPageFile="~/Master/DefaultMasterPage.master" AutoEventWireup="true" CodeFile="TK_QUERYS.aspx.cs" Inherits="CDS_WebPage_MARKETING_TK_QUERYS" %>

<%@ Register Assembly="Ede.Uof.Utility.Component.Grid" Namespace="Ede.Uof.Utility.Component" TagPrefix="Fast" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <style>
        .multiline-textbox {
            word-break: break-all;
            white-space: pre-line;
            height: 60px; /* 根据需要设置高度 */
        }

        .custom-button {
            width: 150px; /* 设置按钮宽度 */
            height: 40px; /* 设置按钮高度 */
            font-size: 20px; /* 设置按钮字体大小 */
            color: #ffffff; /* 设置按钮文本颜色 */
            background-color: #007bff; /* 设置按钮背景颜色 */
            border: none; /* 去除按钮边框 */
            border-radius: 5px; /* 设置按钮圆角 */
        }

            .custom-button:hover {
                background-color: #0056b3; /* 设置按钮鼠标悬停时的背景颜色 */
            }

        .file-preview {
            max-width: 400px; /* 限制圖片的最大寬度 */
            max-height: 400px; /* 限制圖片的最大高度 */
            height: auto; /* 保持圖片的寬高比 */
            width: auto; /* 保持圖片的寬高比 */
            border: 1px solid #ddd;
            border-radius: 4px;
            padding: 2px;
        }

        /* 確保圖片連結不會導致佈局錯亂 */
        .file-preview-container {
            display: inline-block;
        }
    </style>
    <script>    


</script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <telerik:RadTabStrip ID="RadTabStrip1" runat="server"></telerik:RadTabStrip>
            <telerik:RadTabStrip ID="RadTabStrip2" runat="server" MultiPageID="RadMultiPage" SelectedIndex="0">
                <Tabs>
                    <telerik:RadTab Text="門市滿額">
                    </telerik:RadTab>
                    <telerik:RadTab Text="商品銷售">
                    </telerik:RadTab>
                    <telerik:RadTab Text="其他">
                    </telerik:RadTab>
                </Tabs>
            </telerik:RadTabStrip>
            <telerik:RadMultiPage ID="RadMultiPage" runat="server" SelectedIndex="0">
                <telerik:RadPageView ID="RadPageView1" runat="server" Selected="true">
                    <div id="tabs-1">
                        <table class="PopTable">
                            <tr>
                                <td class="PopTableLeftTD"></td>
                                <td>
                                    <asp:Label ID="Label1" runat="server" Text="日期起:(格式yyyyMMdd) "></asp:Label>
                                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="PopTableLeftTD"></td>
                                <td>
                                    <asp:Label ID="Label3" runat="server" Text="日期迄:(格式yyyyMMdd) "></asp:Label>
                                    <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="PopTableLeftTD"></td>
                                <td>
                                    <asp:Label ID="Label4" runat="server" Text="滿額金額含稅: "></asp:Label>
                                    <asp:TextBox ID="TextBox3" Text="1" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="PopTableLeftTD"></td>
                                <td>
                                    <asp:Button ID="Button1" runat="server" Text="查詢" OnClick="Button1_Click"
                                        meta:resourcekey="btn1Resource1" />
                                </td>
                            </tr>
                        </table>
                        <table class="PopTable">
                            <tr>
                                <td colspan="2" class="PopTableRightTD">
                                    <div style="overflow-x: auto; width: 100%">
                                        <Fast:Grid ID="Grid1" OnRowDataBound="Grid1_RowDataBound" OnRowCommand="Grid1_RowCommand" runat="server" OnBeforeExport="OnBeforeExport1" AllowPaging="true" AutoGenerateCheckBoxColumn="False" AllowSorting="True" AutoGenerateColumns="False" CustomDropDownListPage="False" DataKeyOnClientWithCheckBox="False" DefaultSortDirection="Ascending" EmptyDataText="No data found" EnhancePager="True" KeepSelectedRows="False" PageSize="1000" SelectedRowColor="" UnSelectedRowColor="" meta:resourcekey="Grid1Resource1" OnPageIndexChanging="grid_PageIndexChanging1">
                                            <EnhancePagerSettings FirstImageUrl="" FirstAltImageUrl="" PreviousImageUrl="" NextImageUrl="" LastImageUrl="" LastAltImage="" PageNumberCssClass="" PageNumberCurrentCssClass="" PageInfoCssClass="" PageRedirectCssClass="" NextIAltImageUrl="" PreviousAltImageUrl="" ShowHeaderPager="True"></EnhancePagerSettings>
                                            <ExportExcelSettings AllowExportToExcel="true" ExportType="GridContent"></ExportExcelSettings>
                                            <Columns>
                                                <asp:TemplateField HeaderText="日期起" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label_日期起" runat="server" Text='<%# Bind("日期起") %>' Style="word-break: break-all; white-space: pre-line;" Width="100px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="日期迄" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label_日期迄" runat="server" Text='<%# Bind("日期迄") %>' Style="word-break: break-all; white-space: pre-line;" Width="100px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="門市代" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label_門市代" runat="server" Text='<%# Bind("門市代") %>' Style="word-break: break-all; white-space: pre-line;" Width="100px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="門市" ItemStyle-Width="300px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label_門市" runat="server" Text='<%# Bind("門市") %>' Style="word-break: break-all; white-space: pre-line;" Width="300px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="銷售總筆數" ItemStyle-Width="160px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label_銷售總筆數" runat="server" Text='<%# Eval("銷售總筆數", "{0:N0}") %>' Style="word-break: break-all; white-space: pre-line; text-align: right;" Width="160px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="銷售總金額含稅" ItemStyle-Width="160px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label_銷售總金額含稅" runat="server" Text='<%# Eval("銷售總金額含稅", "{0:N0}") %>' Style="word-break: break-all; white-space: pre-line; text-align: right;" Width="160px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="滿額總筆數" ItemStyle-Width="160px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label_滿額總筆數" runat="server" Text='<%# Eval("滿額總筆數", "{0:N0}") %>' Style="word-break: break-all; white-space: pre-line; text-align: right;" Width="160px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="滿額金額含稅" ItemStyle-Width="160px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label_滿額金額含稅" runat="server" Text='<%# Eval("滿額金額含稅", "{0:N0}") %>' Style="word-break: break-all; white-space: pre-line; text-align: right;" Width="160px"></asp:Label>
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
                <telerik:RadPageView ID="RadPageView2" runat="server">
                    <div id="tabs-2">
                        <table class="PopTable">
                            <tr>
                                <td class="PopTableLeftTD"></td>
                                <td>
                                    <asp:Label ID="Label5" runat="server" Text="日期起:(格式yyyyMMdd) "></asp:Label>
                                    <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="PopTableLeftTD"></td>
                                <td>
                                    <asp:Label ID="Label6" runat="server" Text="日期迄:(格式yyyyMMdd) "></asp:Label>
                                    <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="PopTableLeftTD"></td>
                                <td>
                                    <asp:Label ID="Label7" runat="server" Text="商品品號/品名: "></asp:Label>
                                    <asp:TextBox ID="TextBox6" Text="" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="PopTableLeftTD"></td>
                                <td>
                                    <asp:Button ID="Button2" runat="server" Text="查詢" OnClick="Button2_Click"
                                        meta:resourcekey="btn1Resource1" />
                                </td>
                            </tr>
                        </table>
                        <table class="PopTable">
                            <tr>
                                <td class="PopTableLeftTD"></td>
                                <td>
                                    <asp:Label ID="Label_query_dates" runat="server" Text=""></asp:Label>                                  
                                </td>
                            </tr>
                        </table>
                        <table class="PopTable">
                            <tr>
                                <td colspan="2" class="PopTableRightTD">
                                    <div style="overflow-x: auto; width: 100%">
                                        <Fast:Grid ID="Grid2" OnRowDataBound="Grid2_RowDataBound" OnRowCommand="Grid2_RowCommand" runat="server" OnBeforeExport="OnBeforeExport2" AllowPaging="true" AutoGenerateCheckBoxColumn="False" AllowSorting="True" AutoGenerateColumns="False" CustomDropDownListPage="False" DataKeyOnClientWithCheckBox="False" DefaultSortDirection="Ascending" EmptyDataText="No data found" EnhancePager="True" KeepSelectedRows="False" PageSize="1000" SelectedRowColor="" UnSelectedRowColor="" meta:resourcekey="Grid1Resource2" OnPageIndexChanging="grid_PageIndexChanging2" ShowFooter="True" EnableViewState="False">
                                            <EnhancePagerSettings FirstImageUrl="" FirstAltImageUrl="" PreviousImageUrl="" NextImageUrl="" LastImageUrl="" LastAltImage="" PageNumberCssClass="" PageNumberCurrentCssClass="" PageInfoCssClass="" PageRedirectCssClass="" NextIAltImageUrl="" PreviousAltImageUrl="" ShowHeaderPager="True"></EnhancePagerSettings>
                                            <ExportExcelSettings AllowExportToExcel="true" ExportType="GridContent"></ExportExcelSettings>
                                            <Columns>
                                                <asp:TemplateField HeaderText="部門代" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label_部門代" runat="server" Text='<%# Bind("TG005") %>' Style="word-break: break-all; white-space: pre-line;" Width="100px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="部門" ItemStyle-Width="200px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label_部門" runat="server" Text='<%# Bind("ME002") %>' Style="word-break: break-all; white-space: pre-line;" Width="200px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="品號" ItemStyle-Width="200px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label_品號" runat="server" Text='<%# Bind("TH004") %>' Style="word-break: break-all; white-space: pre-line;" Width="200px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="品名" ItemStyle-Width="300px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label_品名" runat="server" Text='<%# Bind("MB002") %>' Style="word-break: break-all; white-space: pre-line;" Width="300px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="銷售總數量" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label_銷售總數量" runat="server" Text='<%# Eval("TOTALNUMS", "{0:N0}") %>'  Style="word-break: break-all; white-space: pre-line; text-align: right;" Width="100px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="銷售總金額(含稅)" ItemStyle-Width="200px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label_銷售總金額含稅" runat="server" Text='<%# Eval("TOTALMONEYS", "{0:N0}") %>'  Style="word-break: break-all; white-space: pre-line; text-align: right;" Width="200px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="成本總金額" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label_成本總金額" runat="server" Text='<%# Eval("TOTALCOSTS", "{0:N0}") %>' Style="word-break: break-all; white-space: pre-line; text-align: right;" Width="100px"></asp:Label>
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

                <telerik:RadPageView ID="RadPageView99" runat="server">
                    <div id="tabs-99">
                        <asp:Label ID="Label2" runat="server" Text="Labe2"></asp:Label>
                    </div>
                </telerik:RadPageView>
            </telerik:RadMultiPage>​
        </ContentTemplate>

    </asp:UpdatePanel>
</asp:Content>

