<%@ Page Title="" Language="C#" MasterPageFile="~/Master/DefaultMasterPage.master" AutoEventWireup="true" CodeFile="TKRESEARCH_COST.aspx.cs" Inherits="CDS_WebPage_RESEARCH_TKRESEARCH_COST" %>

<%@ Register Assembly="Ede.Uof.Utility.Component.Grid" Namespace="Ede.Uof.Utility.Component" TagPrefix="Fast" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <script>    
        function btn4_Click(sender) {
            //從前端開始視窗
            //sender為註冊是由哪個視窗開啟，作為事後要觸發哪個元件的依據
            //OpenDialogResult為關閉視後會執行的JS Function
            //參數使用JSON格式傳遞
            //$uof.dialog.open2("~/CDS/WebPage/COP/TBBU_PRODUCTSDialogADD.aspx", sender, "", 800, 600, OpenDialogResult, {});

            //return false;

        }

        //如果有設定回傳值則執行sender Event
        function OpenDialogResult(returnValue) {
            if (typeof (returnValue) == "undefined")
                return false;
            else
                return true;
        }

        function myFunction() {
            //document.getElementById("demo").innerHTML = "Hello World";

        }



    </script>



    <telerik:RadTabStrip ID="RadTabStrip1" runat="server"></telerik:RadTabStrip>
    <telerik:RadTabStrip ID="RadTabStrip2" runat="server" MultiPageID="RadMultiPage" SelectedIndex="0">
        <Tabs>
            <telerik:RadTab Text="每年平均成本">
            </telerik:RadTab>
            <telerik:RadTab Text="分攤的每年平均成本">
            </telerik:RadTab>
            <telerik:RadTab Text="空白">
            </telerik:RadTab>
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="RadMultiPage" runat="server" SelectedIndex="0">
        <telerik:RadPageView ID="RadPageView1" runat="server" Selected="true">
            <div id="tabs-1">
                <table class="PopTable">
                    <tr>
                        <td class="PopTableLeftTD">
                            <asp:Label ID="Label2" runat="server" Text="日期:" meta:resourcekey="Label4Resource1"></asp:Label>
                        </td>
                        <td class="PopTableRightTD">
                            <asp:TextBox ID="txtDate1" runat="server" Width="100px"></asp:TextBox>

                        </td>
                    </tr>

                    <tr>
                        <td class="PopTableLeftTD">
                            <asp:Label ID="Label3" runat="server" Text="品號:" meta:resourcekey="Label4Resource1"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                        </td>

                    </tr>
                    <tr>
                        <td class="PopTableLeftTD">
                            <asp:Label ID="Label1" runat="server" Text="品名:" meta:resourcekey="Label4Resource1"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                        </td>

                    </tr>

                    <tr>
                        <td class="PopTableLeftTD"></td>
                        <td>
                            <p id="demo"></p>
                            <asp:Button ID="Button1" runat="server" Text=" 查詢 " OnClick="btn1_Click"
                                meta:resourcekey="btn1Resource1" />
                        </td>

                    </tr>


                    <%--<tr>
                        <td class="PopTableLeftTD"></td>
                        <td>
                             <button onclick="myFunction()">Click me</button>
                        </td>

                    </tr>--%>
                </table>
                <table class="PopTable">
                    <tr>
                        <td colspan="2" class="PopTableRightTD">
                            <div style="overflow-x: auto; width: 100%">
                                <Fast:Grid ID="Grid1" Style="overflow-x: auto; width: 100%" OnRowDataBound="Grid1_RowDataBound" OnRowCommand="Grid1_RowCommand" runat="server" OnBeforeExport="OnBeforeExport1" AllowPaging="true" AutoGenerateCheckBoxColumn="False" AllowSorting="True" AutoGenerateColumns="False" CustomDropDownListPage="False" DataKeyOnClientWithCheckBox="False" DefaultSortDirection="Ascending" EmptyDataText="No data found" EnhancePager="True" KeepSelectedRows="False" PageSize="1000" SelectedRowColor="" UnSelectedRowColor="" meta:resourcekey="GridResource1" OnPageIndexChanging="grid1_PageIndexChanging">
                                    <EnhancePagerSettings FirstImageUrl="" FirstAltImageUrl="" PreviousImageUrl="" NextImageUrl="" LastImageUrl="" LastAltImage="" PageNumberCssClass="" PageNumberCurrentCssClass="" PageInfoCssClass="" PageRedirectCssClass="" NextIAltImageUrl="" PreviousAltImageUrl="" ShowHeaderPager="True"></EnhancePagerSettings>
                                    <ExportExcelSettings AllowExportToExcel="true" ExportType="GridContent"></ExportExcelSettings>
                                    <Columns>
                                        <asp:BoundField HeaderText="年月" DataField="年月" ItemStyle-Width="40px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                            <ItemStyle HorizontalAlign="Center" Width="40px"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="品號" DataField="品號" ItemStyle-Width="40px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                            <ItemStyle HorizontalAlign="left" Width="40px"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="品名" DataField="品名" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                            <ItemStyle HorizontalAlign="left" Width="100px"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="規格" DataField="規格" ItemStyle-Width="40px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                            <ItemStyle HorizontalAlign="left" Width="40px"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="單位材料成本" DataField="單位材料成本" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                            <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="單位人工成本" DataField="單位人工成本" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                            <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="單位製造成本" DataField="單位製造成本" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                            <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="單位加工成本" DataField="單位加工成本" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                            <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="單位成本" DataField="單位成本" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                            <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
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
             <table class="PopTable">
                    <tr>
                        <td colspan="2" class="PopTableRightTD">
                            <div style="overflow-x: auto; width: 100%">
                                <Fast:Grid ID="Grid2" Style="overflow-x: auto; width: 100%" OnRowDataBound="Grid2_RowDataBound" OnRowCommand="Grid2_RowCommand" runat="server" OnBeforeExport="OnBeforeExport2" AllowPaging="true" AutoGenerateCheckBoxColumn="False" AllowSorting="True" AutoGenerateColumns="False" CustomDropDownListPage="False" DataKeyOnClientWithCheckBox="False" DefaultSortDirection="Ascending" EmptyDataText="No data found" EnhancePager="True" KeepSelectedRows="False" PageSize="1000" SelectedRowColor="" UnSelectedRowColor="" meta:resourcekey="GridResource2" OnPageIndexChanging="grid2_PageIndexChanging">
                                    <EnhancePagerSettings FirstImageUrl="" FirstAltImageUrl="" PreviousImageUrl="" NextImageUrl="" LastImageUrl="" LastAltImage="" PageNumberCssClass="" PageNumberCurrentCssClass="" PageInfoCssClass="" PageRedirectCssClass="" NextIAltImageUrl="" PreviousAltImageUrl="" ShowHeaderPager="True"></EnhancePagerSettings>
                                    <ExportExcelSettings AllowExportToExcel="true" ExportType="GridContent"></ExportExcelSettings>
                                    <Columns>
                                        <asp:BoundField HeaderText="年度" DataField="年度" ItemStyle-Width="40px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                            <ItemStyle HorizontalAlign="Center" Width="40px"></ItemStyle>
                                        </asp:BoundField>
                                         <asp:BoundField HeaderText="成品品號" DataField="成品品號" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                            <ItemStyle HorizontalAlign="left" Width="100px"></ItemStyle>
                                        </asp:BoundField>
                                      
                                         <asp:BoundField HeaderText="成品品名" DataField="成品品名" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                            <ItemStyle HorizontalAlign="left" Width="100px"></ItemStyle>
                                        </asp:BoundField>
                                      
                                         <asp:BoundField HeaderText="使用品號" DataField="使用品號" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                            <ItemStyle HorizontalAlign="left" Width="100px"></ItemStyle>
                                        </asp:BoundField>
                                      
                                         <asp:BoundField HeaderText="使用品名" DataField="使用品名" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                            <ItemStyle HorizontalAlign="left" Width="100px"></ItemStyle>
                                        </asp:BoundField>
                                      
                                         <asp:BoundField HeaderText="分類" DataField="分類" ItemStyle-Width="40px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                            <ItemStyle HorizontalAlign="Center" Width="40px"></ItemStyle>
                                        </asp:BoundField>
                                      
                                         <asp:BoundField HeaderText="分攤成本" DataField="分攤成本" ItemStyle-Width="40px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                            <ItemStyle HorizontalAlign="Center" Width="40px"></ItemStyle>
                                        </asp:BoundField>
                                      
                                         <asp:BoundField HeaderText="各百分比" DataField="各百分比" ItemStyle-Width="40px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                            <ItemStyle HorizontalAlign="Center" Width="40px"></ItemStyle>
                                        </asp:BoundField>
                                      
                                      

                                    </Columns>
                                </Fast:Grid>
                            </div>
                        </td>
                    </tr>
                </table>
        </telerik:RadPageView>

        <telerik:RadPageView ID="RadPageView3" runat="server">
        </telerik:RadPageView>
    </telerik:RadMultiPage>​
</asp:Content>

