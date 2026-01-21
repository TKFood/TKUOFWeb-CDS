<%@ Page Title="" Language="C#" MasterPageFile="~/Master/DefaultMasterPage.master" AutoEventWireup="true" CodeFile="TK_TBPURGOODS.aspx.cs" Inherits="CDS_WebPage_PUR_TK_TBPURGOODS" %>
<%@ Register Assembly="Ede.Uof.Utility.Component.Grid" Namespace="Ede.Uof.Utility.Component" TagPrefix="Fast" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <telerik:RadTabStrip ID="RadTabStrip1" runat="server"></telerik:RadTabStrip>
            <telerik:RadTabStrip ID="RadTabStrip2" runat="server" MultiPageID="RadMultiPage" SelectedIndex="0">
                <Tabs>
                    <telerik:RadTab Text="資料">
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
                                    <asp:Label ID="Label7" runat="server" Text="廠商: "></asp:Label>
                                    <asp:TextBox ID="QUERY_TextBox1" Text="" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="PopTableLeftTD"></td>
                                <td>
                                    <asp:Label ID="Label3" runat="server" Text="品項: "></asp:Label>
                                    <asp:TextBox ID="QUERY_TextBox2" Text="" runat="server"></asp:TextBox>
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
                                        <Fast:Grid ID="Grid1" OnRowUpdating="Grid1_RowUpdating" OnRowDataBound="Grid1_RowDataBound" OnRowCommand="Grid1_RowCommand" runat="server" OnBeforeExport="OnBeforeExport1" AllowPaging="true" AutoGenerateCheckBoxColumn="False" AllowSorting="True" AutoGenerateColumns="False" CustomDropDownListPage="False" DataKeyOnClientWithCheckBox="False" DefaultSortDirection="Ascending" EmptyDataText="No data found" EnhancePager="True" KeepSelectedRows="False" PageSize="1000" SelectedRowColor="" UnSelectedRowColor="" meta:resourcekey="Grid1Resource1" OnPageIndexChanging="grid_PageIndexChanging1">
                                            <EnhancePagerSettings FirstImageUrl="" FirstAltImageUrl="" PreviousImageUrl="" NextImageUrl="" LastImageUrl="" LastAltImage="" PageNumberCssClass="" PageNumberCurrentCssClass="" PageInfoCssClass="" PageRedirectCssClass="" NextIAltImageUrl="" PreviousAltImageUrl="" ShowHeaderPager="True"></EnhancePagerSettings>
                                            <ExportExcelSettings AllowExportToExcel="true" ExportType="GridContent"></ExportExcelSettings>
                                            <Columns>
                                                <asp:TemplateField HeaderText="廠商" ItemStyle-Width="200px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="TextBox_廠商"
                                                            runat="server"
                                                            Text='<%# Bind("廠商") %>'
                                                            Style="word-break: break-all; white-space: pre-line;"
                                                            Width="200px">
                                                        </asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>                                               
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnUpdate" runat="server" Text="更新" CommandName="UPDATE"
                                                            CommandArgument='<%# Eval("ID") %>'
                                                            OnClientClick="return confirm('是否更新？');"
                                                            CssClass="btn btn-danger btn-sm" />
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

