<%@ Page Title="" Language="C#" MasterPageFile="~/Master/DefaultMasterPage.master" AutoEventWireup="true" CodeFile="Mobile_SALES_RECORDS_ADMIN.aspx.cs" Inherits="CDS_WebPage_Mobile_Mobile_SALES_RECORDS_ADMIN" %>

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

        .fast-grid {
        }

            /* 表头样式 */
            .fast-grid th {
                background-color: dodgerblue; /* 背景颜色 */
                color: white; /* 字体颜色 */
                font-weight: bold; /* 字体粗体 */
                border: 2px solid white; /* 边框粗线 */
                padding: 5px;
                text-align: center;
            }

            /* 单元格框线 */
            .fast-grid td {
                border: 1px solid #000;
                padding: 5px;
                text-align: left; /* 根据需要更改文本对齐方式 */
            }
    </style>
    <html>

    <head>
        <title>業務</title>
    </head>
    <body>
        <telerik:RadTabStrip ID="RadTabStrip1" runat="server"></telerik:RadTabStrip>
        <telerik:RadTabStrip ID="RadTabStrip2" runat="server" MultiPageID="RadMultiPage">
            <Tabs>
                <telerik:RadTab Text="交辨回覆" PageViewID="RadPageView1">
                </telerik:RadTab>
                <telerik:RadTab Text="交辨新增修改" PageViewID="RadPageView2">
                </telerik:RadTab>
                <telerik:RadTab Text="資料" PageViewID="RadPageView3">
                </telerik:RadTab>
            </Tabs>
        </telerik:RadTabStrip>
        <telerik:RadMultiPage ID="RadMultiPage" runat="server" SelectedIndex="0">
            <telerik:RadPageView ID="RadPageView1" runat="server" Selected="true" PageViewID="0">
                <div id="tabs-1">
                    <table class="PopTable">
                        <tr>
                            <td>
                                <asp:Label ID="Label9" runat="server" Text="客戶: "></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox_CLIENTS" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label14" runat="server" Text="是否結案: "></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="DropDownListISCLOSE" runat="server" AutoPostBack="true" Style="width: 200px;"></asp:DropDownList>

                            </td>
                        </tr>
                        <tr>
                            <td class="PopTableLeftTD"></td>
                            <td>
                                <asp:Button ID="Button1" runat="server" Text="查詢 " OnClick="btn1_Click" meta:resourcekey="btn1_Resource1" />
                            </td>
                        </tr>


                    </table>
                    <table style="width: 100%">
                        <tr style="width: 100%">
                            <td colspan="2" style="width: 100%">
                                <div style="overflow-x: auto; width: 100%">
                                    <Fast:Grid ID="Grid1" OnRowDataBound="Grid1_RowDataBound" OnRowCommand="Grid1_OnRowCommand" runat="server" OnBeforeExport="OnBeforeExport1" AllowPaging="true" AutoGenerateCheckBoxColumn="False" AllowSorting="True" AutoGenerateColumns="False" CustomDropDownListPage="False" DataKeyOnClientWithCheckBox="False" DefaultSortDirection="Ascending" EmptyDataText="No data found" EnhancePager="True" KeepSelectedRows="False" PageSize="100" SelectedRowColor="" UnSelectedRowColor="" meta:resourcekey="Grid1Resource1" OnPageIndexChanging="grid_PageIndexChanging1" CssClass="fast-grid">
                                        <EnhancePagerSettings FirstImageUrl="" FirstAltImageUrl="" PreviousImageUrl="" NextImageUrl="" LastImageUrl="" LastAltImage="" PageNumberCssClass="" PageNumberCurrentCssClass="" PageInfoCssClass="" PageRedirectCssClass="" NextIAltImageUrl="" PreviousAltImageUrl="" ShowHeaderPager="True"></EnhancePagerSettings>
                                        <ExportExcelSettings AllowExportToExcel="true" ExportType="DataSource"></ExportExcelSettings>
                                        <Columns>
                                            <asp:TemplateField HeaderText="業務員" ItemStyle-Width="200px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="SALES" runat="server" Text='<%# Bind("SALES") %>' Style="word-break: break-all; white-space: pre-line" Width="100px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField HeaderText="客戶" DataField="CLIENTS" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="回覆期限" DataField="EDAYS" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="交辨內容" ItemStyle-Width="200px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="交辨內容" runat="server" Text='<%# Bind("EVENTS") %>' Style="word-break: break-all; white-space: pre-line;" Width="200px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField HeaderText="是否結案" DataField="ISCLOSE" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="輸入回覆" ItemStyle-Width="200px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtNewField" runat="server" TextMode="MultiLine" Rows="3"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="確定回覆" ItemStyle-Width="60px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Button ID="Grid1Button1" runat="server" Text="確定回覆" CommandName="Grid1Button1" ForeColor="Red" CommandArgument='<%# Container.DataItemIndex %>' OnClientClick="return confirm('確定？');" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="確定結案" ItemStyle-Width="60px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Button ID="Grid1Button2" runat="server" Text="確定結案" CommandName="Grid1Button2" ForeColor="Red" CommandArgument='<%# Container.DataItemIndex %>' OnClientClick="return confirm('確定？');" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="改未結案" ItemStyle-Width="60px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Button ID="Grid1Button3" runat="server" Text="改未結案" CommandName="Grid1Button3" ForeColor="Red" CommandArgument='<%# Container.DataItemIndex %>' OnClientClick="return confirm('確定？');" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ID" ItemStyle-Width="200px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="ID" runat="server" Text='<%# Bind("ID") %>' Style="word-break: break-all; white-space: pre-line" Width="100px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="DID" ItemStyle-Width="200px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="DID" runat="server" Text='<%# Bind("DID") %>' Style="word-break: break-all; white-space: pre-line" Width="100px"></asp:Label>
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
            <telerik:RadPageView ID="RadPageView2" runat="server" Selected="false" PageViewID="1">
                <div id="tabs-2">
                </div>
            </telerik:RadPageView>
            <telerik:RadPageView ID="RadPageView3" runat="server" Selected="false" PageViewID="3">
                <div id="tabs-3">
                </div>
            </telerik:RadPageView>

        </telerik:RadMultiPage>​
    </html>
</asp:Content>

