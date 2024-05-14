﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Master/DefaultMasterPage.master" AutoEventWireup="true" CodeFile="TK_TB_EIP_FORUM_ARTICLE.aspx.cs" Inherits="CDS_WebPage_TKBUSINESS_TK_TB_EIP_FORUM_ARTICLE" %>

<%@ Register Assembly="Ede.Uof.Utility.Component.Grid" Namespace="Ede.Uof.Utility.Component" TagPrefix="Fast" %>



<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script>          
        $(function () {

        });
        //如果有設定回傳值則執行sender Event
        function OpenDialogResult(returnValue) {
            if (typeof (returnValue) == "undefined")
                return false;
            else
                return true;
        }
    </script>

    <telerik:RadTabStrip ID="RadTabStrip1" runat="server"></telerik:RadTabStrip>
    <telerik:RadTabStrip ID="RadTabStrip2" runat="server" MultiPageID="RadMultiPage" SelectedIndex="0">
        <Tabs>
            <telerik:RadTab Text="報表">
            </telerik:RadTab>
            <telerik:RadTab Text="資料">
            </telerik:RadTab>
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="RadMultiPage" runat="server" SelectedIndex="0">
        <telerik:RadPageView ID="RadPageView1" runat="server">
            <div id="tabs-1">
                <table class="PopTable">
                    <tr>
                        <td>
                            <asp:Label ID="Label14" runat="server" Text="查詢年度: "></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="DropDownListISCLOSE" runat="server" AutoPostBack="true" Style="width: 200px;"></asp:DropDownList>

                        </td>
                    </tr>
                    <tr>
                        <td class="PopTableLeftTD"></td>
                        <td>
                            <asp:Button ID="Button1" runat="server" Text=" 查詢 " OnClick="btn1_Click" meta:resourcekey="btn1_Resource1" />
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
                                        <asp:TemplateField HeaderText="討論區" ItemStyle-Width="10%" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="討論區" runat="server" Text='<%# Bind("BOARD_NAME") %>' Style="word-break: break-all; white-space: pre-line;"></asp:Label>
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
            <div id="tabs-3">
            </div>
        </telerik:RadPageView>
    </telerik:RadMultiPage>​
</asp:Content>

