<%@ Page Title="" Language="C#" MasterPageFile="~/Master/DefaultMasterPage.master" AutoEventWireup="true" CodeFile="TK_TEMP_HUMI_LOG.aspx.cs" Inherits="CDS_WebPage_QC_TK_TEMP_HUMI_LOG" %>

<%@ Register Assembly="Ede.Uof.Utility.Component.Grid" Namespace="Ede.Uof.Utility.Component" TagPrefix="Fast" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <!-- 只有在 UpdatePanel 內部的內容會被定期更新，整個頁面不會閃爍 -->
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <!-- 定時器：Interval 單位為毫秒 (60000 = 60秒) -->
            <asp:Timer ID="Timer1" runat="server" Interval="60000" OnTick="Timer1_Tick" />

            <asp:Label ID="lblStatus" runat="server" Text="等待更新中..." />

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
                                    <asp:Button ID="Button1" runat="server" Text="即時查詢" OnClick="Button1_Click"
                                        meta:resourcekey="btn1Resource1" />
                                </td>
                            </tr>
                        </table>
                        <table class="PopTable">
                            <tr>
                                <td colspan="2" class="PopTableRightTD">
                                    <div style="overflow-x: auto; width: 100%">
                                        <Fast:Grid ID="Grid1" OnRowDataBound="Grid1_RowDataBound" OnRowCommand="Grid1_RowCommand" runat="server" OnBeforeExport="OnBeforeExport1" meta:resourcekey="Grid1Resource1" OnPageIndexChanging="grid_PageIndexChanging1" AllowPaging="true" AutoGenerateCheckBoxColumn="False" AllowSorting="True" AutoGenerateColumns="False" CustomDropDownListPage="False" DataKeyOnClientWithCheckBox="False" DefaultSortDirection="Ascending" EmptyDataText="No data found" EnhancePager="True" KeepSelectedRows="False" PageSize="1000" SelectedRowColor="" UnSelectedRowColor="" ShowFooter="True" EnableViewState="True">
                                            <EnhancePagerSettings FirstImageUrl="" FirstAltImageUrl="" PreviousImageUrl="" NextImageUrl="" LastImageUrl="" LastAltImage="" PageNumberCssClass="" PageNumberCurrentCssClass="" PageInfoCssClass="" PageRedirectCssClass="" NextIAltImageUrl="" PreviousAltImageUrl="" ShowHeaderPager="True"></EnhancePagerSettings>
                                            <ExportExcelSettings AllowExportToExcel="true" ExportType="GridContent"></ExportExcelSettings>
                                            <Columns>
                                                <asp:TemplateField HeaderText="機台名稱" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label_機台名稱" runat="server" Text='<%# Bind("機台名稱") %>' Style="word-break: break-all; white-space: pre-line;" Width="100%"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="區域" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label_區域" runat="server" Text='<%# Bind("區域") %>' Style="word-break: break-all; white-space: pre-line;" Width="100%"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="實際溫度" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label_實際溫度" runat="server" 
                                                               Text='<%# Eval("實際溫度", "{0:N2}") %>' 
                                                               Style="word-break: break-all; white-space: pre-line; text-align: right;" 
                                                               Width="100%">
                                                    </asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="溫度上限" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                               <asp:Label ID="Label_溫度上限" runat="server" 
                                                               Text='<%# Eval("溫度上限", "{0:N2}") %>' 
                                                               Style="word-break: break-all; white-space: pre-line; text-align: right;" 
                                                               Width="100%">
                                                    </asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="溫度下限" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                               <asp:Label ID="Label_溫度下限" runat="server" 
                                                               Text='<%# Eval("溫度下限", "{0:N2}") %>' 
                                                               Style="word-break: break-all; white-space: pre-line; text-align: right;" 
                                                               Width="100%">
                                                    </asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="實際溼度" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                               <asp:Label ID="Label_實際溼度" runat="server" 
                                                               Text='<%# Eval("實際溼度", "{0:N2}") %>' 
                                                               Style="word-break: break-all; white-space: pre-line; text-align: right;" 
                                                               Width="100%">
                                                    </asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="溼度上限" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                               <asp:Label ID="Label_溼度上限" runat="server" 
                                                               Text='<%# Eval("溼度上限", "{0:N2}") %>' 
                                                               Style="word-break: break-all; white-space: pre-line; text-align: right;" 
                                                               Width="100%">
                                                    </asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="溼度下限" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                               <asp:Label ID="Label_溼度下限" runat="server" 
                                                               Text='<%# Eval("溼度下限", "{0:N2}") %>' 
                                                               Style="word-break: break-all; white-space: pre-line; text-align: right;" 
                                                               Width="100%">
                                                    </asp:Label>
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
            <triggers>
            <%-- 注意：ControlID 必須對應到按鈕的 ID (Button3) --%>
            <asp:PostBackTrigger ControlID="Button3" />
        </triggers>
        </ContentTemplate>
    </asp:UpdatePanel>


</asp:Content>

