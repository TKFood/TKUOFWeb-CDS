<%@ Page Title="" Language="C#" MasterPageFile="~/Master/DefaultMasterPage.master" AutoEventWireup="true" CodeFile="TK_TB_COMPANY_PROJECTS.aspx.cs" Inherits="CDS_WebPage_TKBUSINESS_TK_TB_COMPANY_PROJECTSE" %>

<%@ Register Assembly="Ede.Uof.Utility.Component.Grid" Namespace="Ede.Uof.Utility.Component" TagPrefix="Fast" %>



<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script>          
        $(function () {
            $("#<%= txtDate1.ClientID %>").datepicker({ dateFormat: "yy/mm/dd", });
            $("#<%= txtDate2.ClientID %>").datepicker({ dateFormat: "yy/mm/dd", });
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
                        <td class="PopTableLeftTD">
                            <asp:Label ID="Label2" runat="server" Text="日期:" meta:resourcekey="Label4Resource1"></asp:Label>
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
                                        <asp:TemplateField HeaderText="是否結案" ItemStyle-Width="10%" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="是否結案" runat="server" Text='<%# Bind("ISCLOSED") %>' Style="word-break: break-all; white-space: pre-line;"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="專案名稱" ItemStyle-Width="25%" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="專案名稱" runat="server" Text='<%# Bind("PROJECTNAMES") %>' Style="word-break: break-all; white-space: pre-line;"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="專案內容" ItemStyle-Width="25%" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:TextBox ID="專案內容" runat="server" Text='<%# Bind("CONTENTS") %>' Style="word-break: break-all; white-space: pre-line;"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="專案日期" ItemStyle-Width="25%" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="專案日期" runat="server" Text='<%# Bind("PROJECTDATES") %>' Style="word-break: break-all; white-space: pre-line;"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="研發" ItemStyle-Width="25%" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                               <asp:HiddenField ID="HiddenDEPDEV" runat="server" Value='<%# Eval("DEPDEV") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="行銷設計" ItemStyle-Width="25%" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="行銷設計" runat="server" Text='<%# Bind("DEPMARKET") %>' Style="word-break: break-all; white-space: pre-line;"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="法務" ItemStyle-Width="25%" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="法務" runat="server" Text='<%# Bind("DEPLAWS") %>' Style="word-break: break-all; white-space: pre-line;"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="品保" ItemStyle-Width="25%" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="品保" runat="server" Text='<%# Bind("DEPQCS") %>' Style="word-break: break-all; white-space: pre-line;"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="財務" ItemStyle-Width="25%" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="財務" runat="server" Text='<%# Bind("DEPACCOUNTS") %>' Style="word-break: break-all; white-space: pre-line;"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="生產" ItemStyle-Width="25%" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="生產" runat="server" Text='<%# Bind("DEPMOC") %>' Style="word-break: break-all; white-space: pre-line;"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="業務" ItemStyle-Width="25%" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="業務" runat="server" Text='<%# Bind("DEPSALES") %>' Style="word-break: break-all; white-space: pre-line;"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="門市" ItemStyle-Width="25%" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="門市" runat="server" Text='<%# Bind("DEPSTORES") %>' Style="word-break: break-all; white-space: pre-line;"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="觀光工廠" ItemStyle-Width="25%" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="觀光工廠" runat="server" Text='<%# Bind("DEPFACTORYS") %>' Style="word-break: break-all; white-space: pre-line;"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="採購" ItemStyle-Width="25%" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="採購" runat="server" Text='<%# Bind("DEPPURS") %>' Style="word-break: break-all; white-space: pre-line;"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="研發最新回覆日" ItemStyle-Width="25%" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="研發最新回覆日" runat="server" Text='<%# Bind("DEPDEVREPLAYDATES") %>' Style="word-break: break-all; white-space: pre-line;"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                       <asp:TemplateField HeaderText="行銷設計最新回覆日" ItemStyle-Width="25%" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="行銷設計最新回覆日" runat="server" Text='<%# Bind("DEPMARKETREPLAYDATES") %>' Style="word-break: break-all; white-space: pre-line;"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="法務最新回覆日" ItemStyle-Width="25%" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="法務最新回覆日" runat="server" Text='<%# Bind("DEPLAWSREPLAYDATES") %>' Style="word-break: break-all; white-space: pre-line;"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="品保最新回覆日" ItemStyle-Width="25%" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="品保最新回覆日" runat="server" Text='<%# Bind("DEPQCSREPLAYDATES") %>' Style="word-break: break-all; white-space: pre-line;"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="財務最新回覆日" ItemStyle-Width="25%" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="財務最新回覆日" runat="server" Text='<%# Bind("DEPACCOUNTSREPLAYDATES") %>' Style="word-break: break-all; white-space: pre-line;"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                       <asp:TemplateField HeaderText="生產最新回覆日" ItemStyle-Width="25%" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="生產最新回覆日" runat="server" Text='<%# Bind("DEPMOCREPLAYDATES") %>' Style="word-break: break-all; white-space: pre-line;"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="業務最新回覆日" ItemStyle-Width="25%" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="業務最新回覆日" runat="server" Text='<%# Bind("DEPSALESREPLAYDATES") %>' Style="word-break: break-all; white-space: pre-line;"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="門市最新回覆日" ItemStyle-Width="25%" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="門市最新回覆日" runat="server" Text='<%# Bind("DEPSTORESREPLAYDATES") %>' Style="word-break: break-all; white-space: pre-line;"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="觀光工廠最新回覆日" ItemStyle-Width="25%" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="觀光工廠最新回覆日" runat="server" Text='<%# Bind("DEPFACTORYSREPLAYDATES") %>' Style="word-break: break-all; white-space: pre-line;"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="採購最新回覆日" ItemStyle-Width="25%" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="採購最新回覆日" runat="server" Text='<%# Bind("DEPPURSREPLAYDATES") %>' Style="word-break: break-all; white-space: pre-line;"></asp:Label>
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

