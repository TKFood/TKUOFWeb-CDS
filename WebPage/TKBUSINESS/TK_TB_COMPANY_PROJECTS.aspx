<%@ Page Title="" Language="C#" MasterPageFile="~/Master/DefaultMasterPage.master" AutoEventWireup="true" CodeFile="TK_TB_COMPANY_PROJECTS.aspx.cs" Inherits="CDS_WebPage_TKBUSINESS_TK_TB_COMPANY_PROJECTSE" %>

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
            <telerik:RadTab Text="專案進度">
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
                            <asp:Label ID="Label14" runat="server" Text="是否結案: "></asp:Label>
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
                                        <asp:TemplateField HeaderText="是否結案" ItemStyle-Width="7%" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="是否結案" runat="server" Text='<%# Bind("ISCLOSED") %>' Style="word-break: break-all; white-space: pre-line;"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="專案編號" ItemStyle-Width="7%" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="專案編號" runat="server" Text='<%# Bind("NO") %>' Style="word-break: break-all; white-space: pre-line;"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="專案屬性" ItemStyle-Width="7%" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <%--<asp:Label ID="專案屬性" runat="server" Text='<%# Bind("KINDS") %>' Style="word-break: break-all; white-space: pre-line;"></asp:Label>--%>
                                                <asp:DropDownList ID="DropDownListKINDS" runat="server" Style="width: 100%; word-break: break-all;"></asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="專案名稱" ItemStyle-Width="7%" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="專案名稱" runat="server" Text='<%# Bind("PROJECTNAMES") %>' Style="word-break: break-all; white-space: pre-line;"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="需求單位" ItemStyle-Width="7%" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <%--<asp:Label ID="需求單位" runat="server" Text='<%# Bind("DEPNAMES") %>' Style="word-break: break-all; white-space: pre-line;"></asp:Label>--%>
                                                <asp:DropDownList ID="DropDownListDEPNAMES" runat="server" Style="word-wrap: break-word; min-width: 100%;"></asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="產品開發申請書" ItemStyle-Width="7%" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <%--<asp:Label ID="產品開發申請書" runat="server" Text='<%# Bind("PRODUCTAPPLYS") %>' Style="word-break: break-all; white-space: pre-line;"></asp:Label>--%>
                                                <asp:DropDownList ID="DropDownListPRODUCTAPPLYS" runat="server" Style="width: 100%; word-break: break-all;"></asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="包材暨包裝設計及變更申請書" ItemStyle-Width="7%" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <%--<asp:Label ID="包材暨包裝設計及變更申請書" runat="server" Text='<%# Bind("PACKAPPLYS") %>' Style="word-break: break-all; white-space: pre-line;"></asp:Label>--%>
                                                <asp:DropDownList ID="DropDownListPACKAPPLYS" runat="server" Style="width: 100%; word-break: break-all;"></asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="需求單位預計上市時間" ItemStyle-Width="7%" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="需求單位預計上市時間" runat="server" Text='<%# Bind("SALEDATES") %>' Style="word-break: break-all; white-space: pre-line;"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="專案進度" ItemStyle-Width="7%" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <%--<asp:Label ID="專案進度" runat="server" Text='<%# Bind("STATUS") %>' Style="word-break: break-all; white-space: pre-line;"></asp:Label>--%>
                                                <asp:DropDownList ID="DropDownListSTATUS" runat="server" Style="word-wrap: break-word; min-width: 100%;"></asp:DropDownList>

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="回饋進度與內容" ItemStyle-Width="7%" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="回饋進度與內容" runat="server" Text='<%# Bind("COMMENTS") %>' Style="word-break: break-all; white-space: pre-line;"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="追蹤日" ItemStyle-Width="7%" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="追蹤日" runat="server" Text='<%# Bind("TRACEDATES") %>' Style="word-break: break-all; white-space: pre-line;"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="輸入回覆" ItemStyle-Width="7%" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtNewField" runat="server" Text='' TextMode="MultiLine" Rows="3" Width="100%"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="新增回覆" ItemStyle-Width="7%" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Button ID="Grid1Button1" runat="server" Text="新增回覆" CommandName="Grid1Button1" ForeColor="Red" CommandArgument='<%# Container.DataItemIndex %>' OnClientClick="return confirm('新增回覆 確定？');" />
                                                <asp:Button ID="Grid1Button2" runat="server" Text="修改專案" CommandName="Grid1Button2" ForeColor="Red" CommandArgument='<%# Container.DataItemIndex %>' OnClientClick="return confirm('修改專案 確定？');" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="ID" ItemStyle-Width="5%" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="ID" runat="server" Text='<%# Bind("ID") %>' Style="word-break: break-all; white-space: pre-line;"></asp:Label>
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
            </div>
        </telerik:RadPageView>

    </telerik:RadMultiPage>​
</asp:Content>

