<%@ Page Title="" Language="C#" MasterPageFile="~/Master/DefaultMasterPage.master" AutoEventWireup="true" CodeFile="TB_SALES_PROMOTIONS.aspx.cs" Inherits="CDS_WebPage_COP_TB_SALES_PROMOTIONS" %>

<%@ Register Assembly="Ede.Uof.Utility.Component.Grid" Namespace="Ede.Uof.Utility.Component" TagPrefix="Fast" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .multiline-textbox {
            word-break: break-all;
            white-space: pre-line;
            /*width: 100px;*/ /* 根据需要设置宽度 */
            height: 60px; /* 根据需要设置高度 */
        }
    </style>
    <script>    
        function btn4_Click(sender) {
            //從前端開始視窗
            //sender為註冊是由哪個視窗開啟，作為事後要觸發哪個元件的依據
            //OpenDialogResult為關閉視後會執行的JS Function
            //參數使用JSON格式傳遞
            $uof.dialog.open2("", sender, "", 800, 600, OpenDialogResult, {});

            return false;

        }

        //如果有設定回傳值則執行sender Event
        function OpenDialogResult(returnValue) {
            if (typeof (returnValue) == "undefined")
                return false;
            else
                return true;
        }



    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <telerik:RadTabStrip ID="RadTabStrip1" runat="server"></telerik:RadTabStrip>
            <telerik:RadTabStrip ID="RadTabStrip2" runat="server" MultiPageID="RadMultiPage" SelectedIndex="0">
                <Tabs>
                    <telerik:RadTab Text="資料">
                    </telerik:RadTab>
                    <telerik:RadTab Text="新增資料">
                    </telerik:RadTab>
                </Tabs>
            </telerik:RadTabStrip>
            <telerik:RadMultiPage ID="RadMultiPage" runat="server" SelectedIndex="0">
                <telerik:RadPageView ID="RadPageView1" runat="server" Selected="true">
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
                                <td class="PopTableLeftTD">
                                    <asp:Label ID="Label2" runat="server" Text="通路名稱: "></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="PopTableLeftTD">
                                    <asp:Label ID="Label3" runat="server" Text="產品規格: "></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="PopTableLeftTD">
                                    <asp:Label ID="Label4" runat="server" Text="活動類型: "></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="DropDownListKINDS" runat="server" AutoPostBack="true" Style="width: 200px;"></asp:DropDownList>
                                </td>
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
                                        <Fast:Grid ID="Grid1" OnRowDataBound="Grid1_RowDataBound" OnRowCommand="Grid1_OnRowCommand" runat="server" OnBeforeExport="OnBeforeExport1" AllowPaging="true" AutoGenerateCheckBoxColumn="False" AllowSorting="True" AutoGenerateColumns="False" CustomDropDownListPage="False" DataKeyOnClientWithCheckBox="False" DefaultSortDirection="Ascending" EmptyDataText="No data found" EnhancePager="True" KeepSelectedRows="False" PageSize="100" SelectedRowColor="" UnSelectedRowColor="" meta:resourcekey="Grid1Resource1" OnPageIndexChanging="grid1_PageIndexChanging">
                                            <EnhancePagerSettings FirstImageUrl="" FirstAltImageUrl="" PreviousImageUrl="" NextImageUrl="" LastImageUrl="" LastAltImage="" PageNumberCssClass="" PageNumberCurrentCssClass="" PageInfoCssClass="" PageRedirectCssClass="" NextIAltImageUrl="" PreviousAltImageUrl="" ShowHeaderPager="True"></EnhancePagerSettings>
                                            <ExportExcelSettings AllowExportToExcel="true" ExportType="DataSource"></ExportExcelSettings>
                                            <Columns>
                                                <asp:TemplateField HeaderText="ID" ItemStyle-Width="10%" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="ID" runat="server" Text='<%# Bind("ID") %>' Style="word-break: break-all; white-space: pre-line;"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="是否結案" ItemStyle-Width="10%" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <%--<asp:Label ID="是否結案" runat="server" Text='<%# Bind("ISCLOSES") %>' Style="word-break: break-all; white-space: pre-line;"></asp:Label>--%>
                                                        <asp:DropDownList ID="GW1DropDownListISCLOSED" runat="server" Style="word-wrap: break-word; min-width: 100%;"></asp:DropDownList>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="通路" ItemStyle-Width="10%" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <%--<asp:Label ID="通路" runat="server" Text='<%# Bind("SALESTO") %>' Style="word-break: break-all; white-space: pre-line;"></asp:Label>--%>
                                                        <asp:TextBox ID="通路" runat="server" Text='<%# Bind("SALESTO") %>' CssClass="multiline-textbox"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="活動時間" ItemStyle-Width="20%" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="活動時間" runat="server" Text='<%# Bind("SDATES") %>' CssClass="multiline-textbox"></asp:TextBox>                                                        
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="產品規格" ItemStyle-Width="10%" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>                                                        
                                                        <asp:TextBox ID="產品規格" runat="server" Text='<%# Bind("PRODUCTS") %>' CssClass="multiline-textbox"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="出貨日" ItemStyle-Width="10%" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>                                                        
                                                        <asp:TextBox ID="出貨日" runat="server" Text='<%# Bind("SHIPDATES") %>' CssClass="multiline-textbox"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="活動類型" ItemStyle-Width="10%" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <%--<asp:Label ID="活動類型" runat="server" Text='<%# Bind("KINIDS") %>' Style="word-break: break-all; white-space: pre-line;"></asp:Label>--%>
                                                        <asp:DropDownList ID="GW1DropDownListKINDS" runat="server" Style="word-wrap: break-word; min-width: 100%;"></asp:DropDownList>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="活動內容及價格" ItemStyle-Width="20%" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>                                                        
                                                        <asp:TextBox ID="活動內容及價格" runat="server" Text='<%# Bind("CONTEXTS") %>' CssClass="multiline-textbox"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="功能" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Button ID="GW1Button1" runat="server" Text="修改" ForeColor="Red" CommandName="GW1Button1" CommandArgument='<%# Eval("ID") %>' />
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
                        <table class="PopTable">
                            <tr>
                                <td class="PopTableLeftTD">
                                    <asp:Label ID="Label1" runat="server" Text="新增資料" meta:resourcekey="Label4Resource1"></asp:Label>
                                </td>
                                <td class="PopTableRightTD">
                                    <asp:Button ID="btn4" runat="server" Text="新增資料" ForeColor="red" OnClientClick="return btn4_Click(this)" meta:resourcekey="btn4Resource1" />

                                </td>
                            </tr>
                        </table>
                    </div>
                </telerik:RadPageView>
            </telerik:RadMultiPage>​
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

