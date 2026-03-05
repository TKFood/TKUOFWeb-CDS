<%@ Page Title="" Language="C#" MasterPageFile="~/Master/DefaultMasterPage.master" AutoEventWireup="true" CodeFile="TK_UOF_FROMS_1002_RECORDS.aspx.cs" Inherits="CDS_WebPage_RESEARCH_TK_UOF_FROMS_1002_RECORDS" %>

<%@ Register Assembly="Ede.Uof.Utility.Component.Grid" Namespace="Ede.Uof.Utility.Component" TagPrefix="Fast" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .big-bold-button {
            font-size: 20px;
            font-weight: bold;
            background-color: #007bff;
            color: white;
            padding: 10px 20px;
            border: none;
            border-radius: 8px;
            box-shadow: 2px 2px 5px rgba(0, 0, 0, 0.2);
            cursor: pointer;
        }

            .big-bold-button:hover {
                background-color: #0056b3;
            }
    </style>

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
                    <table class="PopTable">

                        <tr>
                            <td class="PopTableLeftTD"></td>
                            <td>
                                <asp:Button ID="Button1" runat="server" Text="查詢明細" OnClick="Button1_Click"
                                    meta:resourcekey="btn1Resource1" />
                            </td>
                        </tr>

                    </table>
                    <div id="tabs-1">
                        <table class="PopTable">
                            <tr>
                                <td colspan="2" class="PopTableRightTD">
                                    <div style="overflow-x: auto; width: 100%">
                                        <Fast:Grid ID="Grid1" OnRowDataBound="Grid1_RowDataBound" OnRowCommand="Grid1_RowCommand" runat="server" OnBeforeExport="OnBeforeExport1" AllowPaging="true" AutoGenerateCheckBoxColumn="False" AllowSorting="True" AutoGenerateColumns="False" CustomDropDownListPage="False" DataKeyOnClientWithCheckBox="False" DefaultSortDirection="Ascending" EmptyDataText="No data found" EnhancePager="True" KeepSelectedRows="False" PageSize="1000" SelectedRowColor="" UnSelectedRowColor="" meta:resourcekey="Grid1Resource1" OnPageIndexChanging="grid_PageIndexChanging1">
                                            <EnhancePagerSettings FirstImageUrl="" FirstAltImageUrl="" PreviousImageUrl="" NextImageUrl="" LastImageUrl="" LastAltImage="" PageNumberCssClass="" PageNumberCurrentCssClass="" PageInfoCssClass="" PageRedirectCssClass="" NextIAltImageUrl="" PreviousAltImageUrl="" ShowHeaderPager="True"></EnhancePagerSettings>
                                            <ExportExcelSettings AllowExportToExcel="true" ExportType="GridContent"></ExportExcelSettings>
                                              <Columns>
                                                <asp:TemplateField HeaderText="表單連結" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:HyperLink
                                                            ID="hlTask"
                                                            runat="server"
                                                            Text='點我開表單'
                                                            Target="_blank">
                                                        </asp:HyperLink>
                                                    </ItemTemplate>
                                                </asp:TemplateField>                                                   
                                                <asp:TemplateField HeaderText="表單編號" ItemStyle-Width="140px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label_表單編號" runat="server" Text='<%# Bind("表單編號") %>' Style="word-break: break-all; white-space: pre-line; width: 100%;"></asp:Label>
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
                    </div>
                </telerik:RadPageView>
            </telerik:RadMultiPage>

            ​
          
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

