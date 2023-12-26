<%@ Page Title="" Language="C#" MasterPageFile="~/Master/DefaultMasterPage.master" AutoEventWireup="true" CodeFile="TK_POS_SETS.aspx.cs" Inherits="CDS_WebPage_COWORK_TK_POS_SETS" %>

<%@ Register Assembly="Ede.Uof.Utility.Component.Grid" Namespace="Ede.Uof.Utility.Component" TagPrefix="Fast" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <telerik:RadTabStrip ID="RadTabStrip1" runat="server"></telerik:RadTabStrip>
    <telerik:RadTabStrip ID="RadTabStrip2" runat="server" MultiPageID="RadMultiPage" SelectedIndex="0">
        <Tabs>
            <telerik:RadTab Text="商品特價/折扣">
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
                            <asp:Label ID="Label1" runat="server" Text="年度: "></asp:Label>
                            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="PopTableLeftTD"></td>
                        <td>
                            <asp:Label ID="Label3" runat="server" Text="特價代號/名稱: "></asp:Label>
                            <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="PopTableLeftTD"></td>
                        <td>
                            <asp:Label ID="Label2" runat="server" Text="是否確認: "></asp:Label>
                            <asp:DropDownList ID="DropDownList1" runat="server"></asp:DropDownList>
                        </td>
                    </tr>

                    <tr>
                        <td class="PopTableLeftTD"></td>
                        <td>
                            <asp:Button ID="Button1" runat="server" Text=" 查詢 " OnClick="Button1_Click"
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
                                        <asp:BoundField HeaderText="活動代號" DataField="MB001" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                            <ItemStyle HorizontalAlign="Left" Width="100px"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="特價代號" DataField="MB003" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                            <ItemStyle HorizontalAlign="Left" Width="100px"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="特價名稱" DataField="MB004" ItemStyle-Width="300px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                            <ItemStyle HorizontalAlign="Left" Width="300px"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="核價日期" DataField="MB005" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                            <ItemStyle HorizontalAlign="Left" Width="100px"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="活動商品" ItemStyle-Width="300px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="Label_All_MC004" runat="server" Text='<%# Bind("All_MC004") %>' Style="word-break: break-all; white-space: pre-line;" Width="300px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="適用門市" ItemStyle-Width="160px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="Label_All_MF004" runat="server" Text='<%# Bind("All_MF004") %>' Style="word-break: break-all; white-space: pre-line;" Width="160px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="適用會員" ItemStyle-Width="200px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="Label_All_NI002" runat="server" Text='<%# Bind("All_NI002") %>' Style="word-break: break-all; white-space: pre-line;" Width="200px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="是否送簽" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Button ID="Grid1_Button2" runat="server" Text="送簽" CommandName="Grid1_Button2" ForeColor="Red" CommandArgument='<%# Eval("MB003") %>' />
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
</asp:Content>

