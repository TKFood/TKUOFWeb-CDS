<%@ Page Title="" Language="C#" MasterPageFile="~/Master/DefaultMasterPage.master" AutoEventWireup="true" CodeFile="TBBU_TBCOPTFCHECK.aspx.cs" Inherits="CDS_WebPage_COP_TBBU_TBCOPTFCHECK" %>

<%@ Register Assembly="Ede.Uof.Utility.Component.Grid" Namespace="Ede.Uof.Utility.Component" TagPrefix="Fast" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script>    
        function btn4_Click(sender) {
            //從前端開始視窗
            //sender為註冊是由哪個視窗開啟，作為事後要觸發哪個元件的依據
            //OpenDialogResult為關閉視後會執行的JS Function
            //參數使用JSON格式傳遞
            //$uof.dialog.open2("~/CDS/WebPage/COP/TBBU_PRODUCTSDialogADD.aspx", sender, "", 800, 600, OpenDialogResult, {});

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

    <telerik:RadTabStrip ID="RadTabStrip1" runat="server"></telerik:RadTabStrip>
    <telerik:RadTabStrip ID="RadTabStrip2" runat="server" MultiPageID="RadMultiPage" SelectedIndex="0">
        <Tabs>
            <telerik:RadTab Text="訂單 變更單協調">
            </telerik:RadTab>
            <telerik:RadTab Text="資料">
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
                            <asp:Label ID="Label3" runat="server" Text="月份: "></asp:Label>
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
                            <asp:Label ID="Label4" runat="server" Text="是否生產: "></asp:Label>
                            <asp:DropDownList ID="DropDownList2" runat="server"></asp:DropDownList>
                        </td>

                    </tr>
                    <tr>
                        <td class="PopTableLeftTD"></td>
                        <td>
                            <asp:Button ID="Button1" runat="server" Text=" 查詢 " OnClick="btn5_Click"
                                meta:resourcekey="btn5Resource1" />
                        </td>

                    </tr>
                </table>
                <table class="PopTable">
                    <tr>
                        <td colspan="2" class="PopTableRightTD">
                            <div style="overflow-x: auto; width: 100%">
                                <Fast:Grid ID="Grid1" OnRowDataBound="Grid1_RowDataBound" OnRowCommand="Grid1_RowCommand" runat="server" OnBeforeExport="OnBeforeExport1" AllowPaging="true" AutoGenerateCheckBoxColumn="False" AllowSorting="True" AutoGenerateColumns="False" CustomDropDownListPage="False" DataKeyOnClientWithCheckBox="False" DefaultSortDirection="Ascending" EmptyDataText="No data found" EnhancePager="True" KeepSelectedRows="False" PageSize="100" SelectedRowColor="" UnSelectedRowColor="" meta:resourcekey="Grid1Resource1" OnPageIndexChanging="grid_PageIndexChanging1">
                                    <EnhancePagerSettings FirstImageUrl="" FirstAltImageUrl="" PreviousImageUrl="" NextImageUrl="" LastImageUrl="" LastAltImage="" PageNumberCssClass="" PageNumberCurrentCssClass="" PageInfoCssClass="" PageRedirectCssClass="" NextIAltImageUrl="" PreviousAltImageUrl="" ShowHeaderPager="True"></EnhancePagerSettings>
                                    <ExportExcelSettings AllowExportToExcel="true" ExportType="GridContent"></ExportExcelSettings>
                                    <Columns>
                                        <asp:BoundField HeaderText="客戶" DataField="TE055" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                            <ItemStyle HorizontalAlign="Left" Width="100px"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="單別" DataField="TF001" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                            <ItemStyle HorizontalAlign="Center" Width="10px"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="單號" DataField="TF002" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                            <ItemStyle HorizontalAlign="Center" Width="10px"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="變更版次" DataField="TF003" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                            <ItemStyle HorizontalAlign="Center" Width="10px"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="序號" DataField="TF004" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                            <ItemStyle HorizontalAlign="Center" Width="10px"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="單頭備註" DataField="TE050" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                            <ItemStyle HorizontalAlign="Left" Width="100px"></ItemStyle>
                                        </asp:BoundField>

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
                    <%-- <tr>
                        <td class="PopTableLeftTD">
                            <asp:Label ID="Label1" runat="server" Text="新增資料" meta:resourcekey="Label4Resource1"></asp:Label>
                        </td>
                        <td class="PopTableRightTD">
                            <asp:Button ID="btn4" runat="server" Text="新增資料" ForeColor="red" OnClientClick="return btn4_Click(this)" meta:resourcekey="btn4Resource1" />

                        </td>
                    </tr>--%>
                </table>
            </div>
        </telerik:RadPageView>
    </telerik:RadMultiPage>​
</asp:Content>

