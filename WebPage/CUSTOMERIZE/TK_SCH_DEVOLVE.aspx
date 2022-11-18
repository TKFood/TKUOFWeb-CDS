<%@ Page Title="" Language="C#" MasterPageFile="~/Master/DefaultMasterPage.master" AutoEventWireup="true" CodeFile="TK_SCH_DEVOLVE.aspx.cs" Inherits="CDS_WebPage_CUSTOMERIZE_TK_SCH_DEVOLVE" %>

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
            <telerik:RadTab Text="校稿">
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
                            <asp:Label ID="Label3" runat="server" Text="校稿項目:" meta:resourcekey="Label4Resource1"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                        </td>

                    </tr>
                    <tr>
                        <td class="PopTableLeftTD">
                            <asp:Label ID="Label9" runat="server" Text="校稿 是否完成:" meta:resourcekey="Label4Resource1"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="DropDownList1" runat="server"></asp:DropDownList>
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

                    <tr>
                        <td class="PopTableLeftTD">
                            <asp:Label ID="Label1" runat="server" Text="交辨狀態:" meta:resourcekey="Label4Resource1"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Label2" runat="server" Text="未開始>處理中>交辨完成>審稿完成>校稿才完成 " meta:resourcekey="Label4Resource1"></asp:Label>
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
                                        <asp:BoundField HeaderText="交辨開始時間" DataField="交辨開始時間" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                            <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="交辨項目" DataField="交辨項目" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                            <ItemStyle HorizontalAlign="Left" Width="200px"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="交辨人" DataField="交辨人" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                            <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="被交辨人" DataField="被交辨人" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                            <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="交辨狀態" DataField="交辨狀態" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                            <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="交辨回覆" DataField="交辨回覆" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                            <ItemStyle HorizontalAlign="Left" Width="100px"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="回覆時間" DataField="回覆時間" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                            <ItemStyle HorizontalAlign="Left" Width="100px"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="填寫交辨" ItemStyle-Width="30px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Button ID="GWButton1" runat="server" Text="填寫交辨" CommandName="GWButton1" ForeColor="Red" CommandArgument='<%# Eval("DEVOLVE_GUID") %>' />
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
        </telerik:RadPageView>
    </telerik:RadMultiPage>​
</asp:Content>

