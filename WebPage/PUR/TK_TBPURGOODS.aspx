<%@ Page Title="" Language="C#" MasterPageFile="~/Master/DefaultMasterPage.master" AutoEventWireup="true" CodeFile="TK_TBPURGOODS.aspx.cs" Inherits="CDS_WebPage_PUR_TK_TBPURGOODS" %>

<%@ Register Assembly="Ede.Uof.Utility.Component.Grid" Namespace="Ede.Uof.Utility.Component" TagPrefix="Fast" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <telerik:RadTabStrip ID="RadTabStrip1" runat="server"></telerik:RadTabStrip>
            <telerik:RadTabStrip ID="RadTabStrip2" runat="server" MultiPageID="RadMultiPage" SelectedIndex="0">
                <Tabs>
                    <telerik:RadTab Text="資料">
                    </telerik:RadTab>
                    <telerik:RadTab Text="新增">
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
                                                <asp:TemplateField HeaderText="品項" ItemStyle-Width="400px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="TextBox_品項"
                                                            runat="server"
                                                            Text='<%# Bind("品項") %>'
                                                            Style="word-break: break-all; white-space: pre-line;"
                                                            Width="400px">
                                                        </asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="數量" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="TextBox_數量"
                                                            runat="server"
                                                            Text='<%# Bind("數量") %>'
                                                            Style="word-break: break-all; white-space: pre-line;"
                                                            Width="100px">
                                                        </asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="單價" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="TextBox_單價"
                                                            runat="server"
                                                            Text='<%# Bind("單價") %>'
                                                            Style="word-break: break-all; white-space: pre-line;"
                                                            Width="100px">
                                                        </asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="總計" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="TextBox_總計"
                                                            runat="server"
                                                            Text='<%# Bind("總計") %>'
                                                            Style="word-break: break-all; white-space: pre-line;"
                                                            Width="100px">
                                                        </asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="提供日期" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="TextBox_提供日期"
                                                            runat="server"
                                                            Text='<%# Bind("提供日期") %>'
                                                            Style="word-break: break-all; white-space: pre-line;"
                                                            Width="100px">
                                                        </asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="備註" ItemStyle-Width="200px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="TextBox_備註"
                                                            runat="server"
                                                            Text='<%# Bind("備註") %>'
                                                            Style="word-break: break-all; white-space: pre-line;"
                                                            Width="200px">
                                                        </asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="月叫貨量" ItemStyle-Width="200px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="TextBox_月叫貨量"
                                                            runat="server"
                                                            Text='<%# Bind("月叫貨量") %>'
                                                            Style="word-break: break-all; white-space: pre-line;"
                                                            Width="200px">
                                                        </asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnUpdate" runat="server" Text="更新" CommandName="MYUPDATE"
                                                            CommandArgument='<%# Eval("ID") %>'
                                                            OnClientClick="return confirm('是否更新？');"
                                                            CssClass="btn btn-danger btn-sm" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnDelete" runat="server" Text="刪除" CommandName="MYDELETE"
                                                            CommandArgument='<%# Eval("ID") %>'
                                                            OnClientClick="return confirm('是否刪除？');"
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
                <telerik:RadPageView ID="RadPageView2" runat="server">
                    <div id="tabs-2">
                        <h3>新增</h3>
                        <table class="PopTable">
                            <tr>
                                <td style="width: 15%; padding: 5px; font-weight: bold; vertical-align: top;">
                                    <asp:Label Text="廠商：" runat="server" />
                                </td>
                                <td style="width: 85%; padding: 5px;">
                                    <asp:TextBox ID="ADD_TextBox1" runat="server" Width="300px" />
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 15%; padding: 5px; font-weight: bold; vertical-align: top;">
                                    <asp:Label Text="品項：" runat="server" />
                                </td>
                                <td style="width: 85%; padding: 5px;">
                                    <asp:TextBox ID="ADD_TextBox2" runat="server" Width="300px" />
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 15%; padding: 5px; font-weight: bold; vertical-align: top;">
                                    <asp:Label Text="數量：" runat="server" />
                                </td>
                                <td style="width: 85%; padding: 5px;">
                                    <asp:TextBox ID="ADD_TextBox3" runat="server" Width="300px" />
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 15%; padding: 5px; font-weight: bold; vertical-align: top;">
                                    <asp:Label Text="單價：" runat="server" />
                                </td>
                                <td style="width: 85%; padding: 5px;">
                                    <asp:TextBox ID="ADD_TextBox4" runat="server" Width="300px" />
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 15%; padding: 5px; font-weight: bold; vertical-align: top;">
                                    <asp:Label Text="總計：" runat="server" />
                                </td>
                                <td style="width: 85%; padding: 5px;">
                                    <asp:TextBox ID="ADD_TextBox5" runat="server" Width="300px" />
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 15%; padding: 5px; font-weight: bold; vertical-align: top;">
                                    <asp:Label Text="提供日期：" runat="server" />
                                </td>
                                <td style="width: 85%; padding: 5px;">
                                    <asp:TextBox ID="ADD_TextBox6" runat="server" Width="300px" />
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 15%; padding: 5px; font-weight: bold; vertical-align: top;">
                                    <asp:Label Text="備註：" runat="server" />
                                </td>
                                <td style="width: 85%; padding: 5px;">
                                    <asp:TextBox ID="ADD_TextBox7" runat="server" Width="300px" />
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 15%; padding: 5px; font-weight: bold; vertical-align: top;">
                                    <asp:Label Text="月叫貨量：" runat="server" />
                                </td>
                                <td style="width: 85%; padding: 5px;">
                                    <asp:TextBox ID="ADD_TextBox8" runat="server" Width="300px" />
                                </td>
                            </tr>
                            <tr>
                                    <td></td>
                                    <td>
                                        <div style="padding: 10px 5px;">
                                            <asp:Button ID="btnADD"
                                                runat="server"
                                                Text="新增"
                                                OnClick="btnADD_Click"   
                                               />
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

