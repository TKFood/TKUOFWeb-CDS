<%@ Page Title="" Language="C#" MasterPageFile="~/Master/DefaultMasterPage.master" AutoEventWireup="true" CodeFile="TK_UOF_PUR_INVMB_DELIVERY.aspx.cs" Inherits="CDS_WebPage_PUR_TK_UOF_PUR_INVMB_DELIVERY" %>

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
                    <telerik:RadTab Text="新增">
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
                               <asp:DropDownList ID="ddlSearchKinds" runat="server" AppendDataBoundItems="true">                                   
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="PopTableLeftTD"></td>
                            <td>
                                <asp:Label ID="Label1" runat="server" Text="品號/品名"></asp:Label>
                                <asp:TextBox ID="FIND_TextBox1" runat="server"></asp:TextBox>
                            </td>
                        </tr>
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
                                                <asp:TemplateField HeaderText="ID" ItemStyle-Width="80px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label_ID" runat="server" Text='<%# Bind("ID") %>' Style="word-break: break-all; white-space: pre-line; width: 100%;"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="類別" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtNewField_GV1_類別" runat="server" Text='<%# Bind("KINDS") %>' Width="100%" TextMode="MultiLine" CssClass="multiline-textbox" Rows="3" onkeyup="autoResizeTextBox(this)"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="品號" ItemStyle-Width="200px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtNewField_GV1_品號" runat="server" Text='<%# Bind("MB001") %>' Width="100%" TextMode="MultiLine" CssClass="multiline-textbox" Rows="3" onkeyup="autoResizeTextBox(this)"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="品名" ItemStyle-Width="200px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtNewField_GV1_品名" runat="server" Text='<%# Bind("MB002") %>' Width="100%" TextMode="MultiLine" CssClass="multiline-textbox" Rows="3" onkeyup="autoResizeTextBox(this)"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="規格" ItemStyle-Width="200px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtNewField_GV1_規格" runat="server" Text='<%# Bind("MB003") %>' Width="100%" TextMode="MultiLine" CssClass="multiline-textbox" Rows="3" onkeyup="autoResizeTextBox(this)"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="最低採購量" ItemStyle-Width="200px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtNewField_GV1_最低採購量" runat="server" Text='<%# Bind("MOQ") %>' Width="100%" TextMode="MultiLine" CssClass="multiline-textbox" Rows="3" onkeyup="autoResizeTextBox(this)"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="單位" ItemStyle-Width="80px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtNewField_GV1_單位" runat="server" Text='<%# Bind("UNITS") %>' Width="100%" TextMode="MultiLine" CssClass="multiline-textbox" Rows="3" onkeyup="autoResizeTextBox(this)"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="交期" ItemStyle-Width="200px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtNewField_GV1_交期" runat="server" Text='<%# Bind("DELIVERYDATS") %>' Width="100%" TextMode="MultiLine" CssClass="multiline-textbox" Rows="3" onkeyup="autoResizeTextBox(this)"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="功能" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Button ID="Button2" runat="server" Text="更新" CommandName="Button2" ForeColor="Blue" CommandArgument='<%# Container.DataItemIndex %>' OnClientClick="return confirm('確定？');" />
                                                        <br />
                                                        <div style="height: 5px;"></div>
                                                        <asp:Button ID="Button3" runat="server" Text="刪除" CommandName="Button3" ForeColor="Red" CommandArgument='<%# Container.DataItemIndex %>' OnClientClick="return confirm('確定？');" />
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
                                    <asp:Label Text="類別：" runat="server" />
                                </td>
                                <td style="width: 85%; padding: 5px;">
                                    <asp:TextBox ID="ADD_TextBox1" runat="server" Width="300px" />
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 15%; padding: 5px; font-weight: bold; vertical-align: top;">
                                    <asp:Label Text="品號：" runat="server" />
                                </td>
                                <td style="width: 85%; padding: 5px;">
                                    <asp:TextBox ID="ADD_TextBox2" runat="server" Width="300px" />
                                 </td>
                            <tr>
                                <td style="width: 15%; padding: 5px; font-weight: bold; vertical-align: top;">
                                    <asp:Label Text="品名：" runat="server" />
                                </td>
                                <td style="width: 85%; padding: 5px;">
                                    <asp:TextBox ID="ADD_TextBox3" runat="server" Width="300px" />
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 15%; padding: 5px; font-weight: bold; vertical-align: top;">
                                    <asp:Label Text="規格：" runat="server" />
                                </td>
                                <td style="width: 85%; padding: 5px;">
                                    <asp:TextBox ID="ADD_TextBox4" runat="server" Width="300px" />
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 15%; padding: 5px; font-weight: bold; vertical-align: top;">
                                    <asp:Label Text="最低採購量：" runat="server" />
                                </td>
                                <td style="width: 85%; padding: 5px;">
                                    <asp:TextBox ID="ADD_TextBox5" runat="server" Width="300px" />
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 15%; padding: 5px; font-weight: bold; vertical-align: top;">
                                    <asp:Label Text="單位：" runat="server" />
                                </td>
                                <td style="width: 85%; padding: 5px;">
                                    <asp:TextBox ID="ADD_TextBox6" runat="server" Width="300px" />
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 15%; padding: 5px; font-weight: bold; vertical-align: top;">
                                    <asp:Label Text="交期：" runat="server" />
                                </td>
                                <td style="width: 85%; padding: 5px;">
                                    <asp:TextBox ID="ADD_TextBox7" runat="server" Width="300px" />
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
                                            CssClass="btn-ADD" />
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

