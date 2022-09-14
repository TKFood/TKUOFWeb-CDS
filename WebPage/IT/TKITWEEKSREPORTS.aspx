<%@ Page Title="" Language="C#" MasterPageFile="~/Master/DefaultMasterPage.master" AutoEventWireup="true" CodeFile="TKITWEEKSREPORTS.aspx.cs" Inherits="CDS_WebPage_IT_TKITWEEKSREPORTS" %>

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
            <telerik:RadTab Text="週報">
            </telerik:RadTab>
            <telerik:RadTab Text="新增週報">
            </telerik:RadTab>
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="RadMultiPage" runat="server" SelectedIndex="0">
        <telerik:RadPageView ID="RadPageView1" runat="server" Selected="true">
            <div id="tabs-1">
                <table class="PopTable">
                    <tr>
                        <td class="PopTableLeftTD">
                            <asp:Label ID="Label3" runat="server" Text="年度:" meta:resourcekey="Label4Resource1"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                        </td>

                    </tr>
                    <tr>
                        <td class="PopTableLeftTD">
                            <asp:Label ID="Label1" runat="server" Text="週別:" meta:resourcekey="Label4Resource1"></asp:Label>
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
                                <Fast:Grid ID="Grid1" Style="overflow-x: auto; width: 100%" OnRowDataBound="Grid1_RowDataBound" runat="server" OnBeforeExport="OnBeforeExport1" AllowPaging="true" AutoGenerateCheckBoxColumn="False" AllowSorting="True" AutoGenerateColumns="False" CustomDropDownListPage="False" DataKeyOnClientWithCheckBox="False" DefaultSortDirection="Ascending" EmptyDataText="No data found" EnhancePager="True" KeepSelectedRows="False" PageSize="1000" SelectedRowColor="" UnSelectedRowColor="" meta:resourcekey="GridResource1" OnPageIndexChanging="grid1_PageIndexChanging">
                                    <EnhancePagerSettings FirstImageUrl="" FirstAltImageUrl="" PreviousImageUrl="" NextImageUrl="" LastImageUrl="" LastAltImage="" PageNumberCssClass="" PageNumberCurrentCssClass="" PageInfoCssClass="" PageRedirectCssClass="" NextIAltImageUrl="" PreviousAltImageUrl="" ShowHeaderPager="True"></EnhancePagerSettings>
                                    <ExportExcelSettings AllowExportToExcel="true" ExportType="GridContent"></ExportExcelSettings>
                                    <Columns>
                                        <asp:BoundField HeaderText="姓名" DataField="NAMES" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                            <ItemStyle HorizontalAlign="Center" Width="40px"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="年度" DataField="WYEARS" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                            <ItemStyle HorizontalAlign="Center" Width="40px"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="週別" DataField="WEEKS" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                            <ItemStyle HorizontalAlign="Center" Width="40px"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="開始日" DataField="SDATES" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                            <ItemStyle HorizontalAlign="Center" Width="40px"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="結束日" DataField="EDATES" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                            <ItemStyle HorizontalAlign="Center" Width="40px"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="週報" DataField="COMMENTS" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                            <ItemStyle HorizontalAlign="Left" Width="200px"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="ID" DataField="ID" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                            <ItemStyle HorizontalAlign="Center" Width="40px"></ItemStyle>
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
            <div id="tabs-1">
                <table class="PopTable">
                    <tr>
                        <td class="PopTableLeftTD">
                            <asp:Label ID="Label2" runat="server" Text="年度:" meta:resourcekey="Label4Resource1"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="PopTableLeftTD">
                            <asp:Label ID="Label4" runat="server" Text="週別:" meta:resourcekey="Label4Resource1"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
                        </td>

                    </tr>
                    <tr>
                        <td class="PopTableLeftTD">
                            <asp:Label ID="Label5" runat="server" Text="姓名:" meta:resourcekey="Label4Resource1"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="DropDownList1" runat="server"></asp:DropDownList>
                        </td>

                    </tr>
                    <tr>
                        <td class="PopTableLeftTD">
                            <asp:Label ID="Label6" runat="server" Text="週報:" meta:resourcekey="Label4Resource1"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="TextBox5" runat="server" TextMode="MultiLine" Rows="20" Width="100%"></asp:TextBox>
                        </td>

                    </tr>
                    <tr>
                        <td class="PopTableLeftTD">
                            <asp:Label ID="Label7" runat="server" Text="日期:" meta:resourcekey="Label4Resource1"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="TextBox6" runat="server"></asp:TextBox>
                            <asp:TextBox ID="TextBox7" runat="server"></asp:TextBox>
                        </td>                    

                    </tr>
                    <tr>
                    <tr>
                        <td class="PopTableLeftTD"></td>
                        <td>
                            <p id="demo"></p>
                            <asp:Button ID="Button2" runat="server" Text=" 新增 " OnClick="btn2_Click"
                                meta:resourcekey="btn2Resource1" />
                        </td>

                    </tr>
                </table>
            </div>
        </telerik:RadPageView>
    </telerik:RadMultiPage>​
</asp:Content>

