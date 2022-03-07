﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Master/DefaultMasterPage.master" AutoEventWireup="true" CodeFile="TBBU_TBCOPTFCHECK.aspx.cs" Inherits="CDS_WebPage_COP_TBBU_TBCOPTFCHECK" %>

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
            <telerik:RadTab Text="訂單協調">
            </telerik:RadTab>
            <telerik:RadTab Text="訂單協調-生管用">
            </telerik:RadTab>
            <telerik:RadTab Text="訂單協調-採購用">
            </telerik:RadTab>
            <telerik:RadTab Text="訂單協調-業務用">
            </telerik:RadTab>
            <telerik:RadTab Text="訂單不需生產庫存出貨">
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
                            <asp:Label ID="Label17" runat="server" Text="訂單單號: "></asp:Label>
                            <asp:TextBox ID="TextBox9" runat="server"></asp:TextBox>
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
                                <Fast:Grid ID="Grid1" OnRowDataBound="Grid1_RowDataBound" OnRowCommand="Grid1_RowCommand" runat="server" OnBeforeExport="OnBeforeExport1" AllowPaging="true" AutoGenerateCheckBoxColumn="False" AllowSorting="True" AutoGenerateColumns="False" CustomDropDownListPage="False" DataKeyOnClientWithCheckBox="False" DefaultSortDirection="Ascending" EmptyDataText="No data found" EnhancePager="True" KeepSelectedRows="False" PageSize="100" SelectedRowColor="" UnSelectedRowColor="" meta:resourcekey="Grid1Resource1" OnPageIndexChanging="grid_PageIndexChanging1">
                                    <EnhancePagerSettings FirstImageUrl="" FirstAltImageUrl="" PreviousImageUrl="" NextImageUrl="" LastImageUrl="" LastAltImage="" PageNumberCssClass="" PageNumberCurrentCssClass="" PageInfoCssClass="" PageRedirectCssClass="" NextIAltImageUrl="" PreviousAltImageUrl="" ShowHeaderPager="True"></EnhancePagerSettings>
                                    <ExportExcelSettings AllowExportToExcel="true" ExportType="GridContent"></ExportExcelSettings>
                                    <Columns>
                                        <asp:BoundField HeaderText="客戶" DataField="TC053" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                            <ItemStyle HorizontalAlign="Left" Width="100px"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="單別" DataField="TD001" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                            <ItemStyle HorizontalAlign="Center" Width="10px"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="單號" DataField="TD002" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                            <ItemStyle HorizontalAlign="Center" Width="10px"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="序號" DataField="TD003" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                            <ItemStyle HorizontalAlign="Center" Width="10px"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="品號" DataField="TD004" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                            <ItemStyle HorizontalAlign="Left" Width="10px"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="品名" DataField="TD005" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                            <ItemStyle HorizontalAlign="Left" Width="100px"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="訂單數量" DataField="TD008" DataFormatString="{0:N2}" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                            <ItemStyle HorizontalAlign="Center" Width="10px"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="贈品量" DataField="TD024" DataFormatString="{0:N2}" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                            <ItemStyle HorizontalAlign="Center" Width="10px"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="已交數量" DataField="TD009" DataFormatString="{0:N2}" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                            <ItemStyle HorizontalAlign="Center" Width="10px"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="贈品已交" DataField="TD025" DataFormatString="{0:N2}" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                            <ItemStyle HorizontalAlign="Center" Width="10px"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="單位" DataField="TD010" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                            <ItemStyle HorizontalAlign="Center" Width="10px"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="預交日" DataField="TD013" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                            <ItemStyle HorizontalAlign="Center" Width="10px"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="單頭備註" DataField="TC015" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                            <ItemStyle HorizontalAlign="Left" Width="200px"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="單身備註" DataField="TD020" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                            <ItemStyle HorizontalAlign="Left" Width="200px"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="生管核準" DataField="MOCCHECKS" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                            <ItemStyle HorizontalAlign="Center" Width="20px"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="生管更新日期" DataField="MOCCHECKDATES" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                            <ItemStyle HorizontalAlign="Center" Width="10px"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="生管備註" DataField="MOCCHECKSCOMMENTS" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                            <ItemStyle HorizontalAlign="Left" Width="200px"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="採購核準" DataField="PURCHECKS" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                            <ItemStyle HorizontalAlign="Center" Width="20px"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="採購更新日期" DataField="PURCHECKDATES" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                            <ItemStyle HorizontalAlign="Center" Width="10px"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="採購備註" DataField="PURCHECKSCOMMENTS" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                            <ItemStyle HorizontalAlign="Left" Width="200px"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="業務更新日期" DataField="SALESCHECKDATES" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                            <ItemStyle HorizontalAlign="Center" Width="10px"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="業務備註" DataField="SALESCHECKSCOMMENTS" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                            <ItemStyle HorizontalAlign="Left" Width="200px"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="更新進度" ItemStyle-Width="80px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Button ID="Button1" runat="server" Text="更新" ForeColor="Red" CommandName="Button1" CommandArgument='<%# Eval("TD123") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="是否送簽" ItemStyle-Width="60px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Button ID="Button2" runat="server" Text="送簽" CommandName="Button2" ForeColor="Red" CommandArgument='<%# Eval("TD12") %>' />
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
                    <table class="PopTable">
                        <tr>
                            <td class="PopTableLeftTD"></td>
                            <td>
                                <asp:Label ID="Label5" runat="server" Text="年度: "></asp:Label>
                                <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                            </td>

                        </tr>
                        <tr>
                            <td class="PopTableLeftTD"></td>
                            <td>
                                <asp:Label ID="Label6" runat="server" Text="月份: "></asp:Label>
                                <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
                            </td>

                        </tr>
                        <tr>
                            <td class="PopTableLeftTD"></td>
                            <td>
                                <asp:Label ID="Label7" runat="server" Text="是否確認: "></asp:Label>
                                <asp:DropDownList ID="DropDownList3" runat="server"></asp:DropDownList>
                            </td>

                        </tr>
                        <tr>
                            <td class="PopTableLeftTD"></td>
                            <td>
                                <asp:Label ID="Label8" runat="server" Text="是否生產: "></asp:Label>
                                <asp:DropDownList ID="DropDownList4" runat="server"></asp:DropDownList>
                            </td>

                        </tr>
                        <tr>
                            <td class="PopTableLeftTD"></td>
                            <td>
                                <asp:Label ID="Label18" runat="server" Text="訂單單號: "></asp:Label>
                                <asp:TextBox ID="TextBox10" runat="server"></asp:TextBox>
                            </td>

                        </tr>
                        <tr>
                            <td class="PopTableLeftTD"></td>
                            <td>
                                <asp:Button ID="Button3" runat="server" Text=" 查詢 " OnClick="Button3_Click"
                                    meta:resourcekey="btn5Resource1" />
                            </td>

                        </tr>
                    </table>
                    <table class="PopTable">
                        <tr>
                            <td class="PopTableLeftTD"></td>
                            <td>
                                <asp:Button ID="Button4" runat="server" Text=" 生管批次更新 " OnClick="Button4_Click"
                                    meta:resourcekey="btn4Resource1" />
                            </td>

                        </tr>
                    </table>
                    <table class="PopTable">
                        <tr>
                            <td colspan="2" class="PopTableRightTD">
                                <div style="overflow-x: auto; width: 100%">
                                    <Fast:Grid ID="Grid2" OnRowDataBound="Grid2_RowDataBound" OnRowCommand="Grid2_RowCommand" runat="server" OnBeforeExport="OnBeforeExport2" AllowPaging="true" AutoGenerateCheckBoxColumn="False" AllowSorting="True" AutoGenerateColumns="False" CustomDropDownListPage="False" DataKeyOnClientWithCheckBox="False" DefaultSortDirection="Ascending" EmptyDataText="No data found" EnhancePager="True" KeepSelectedRows="False" PageSize="100" SelectedRowColor="" UnSelectedRowColor="" meta:resourcekey="Grid1Resource2" OnPageIndexChanging="grid_PageIndexChanging2">
                                        <EnhancePagerSettings FirstImageUrl="" FirstAltImageUrl="" PreviousImageUrl="" NextImageUrl="" LastImageUrl="" LastAltImage="" PageNumberCssClass="" PageNumberCurrentCssClass="" PageInfoCssClass="" PageRedirectCssClass="" NextIAltImageUrl="" PreviousAltImageUrl="" ShowHeaderPager="True"></EnhancePagerSettings>
                                        <ExportExcelSettings AllowExportToExcel="true" ExportType="GridContent"></ExportExcelSettings>
                                        <Columns>
                                            <asp:BoundField HeaderText="客戶" DataField="TC053" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle HorizontalAlign="Left" Width="100px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="單別" DataField="TD001" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle HorizontalAlign="Center" Width="10px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="單號" DataField="TD002" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle HorizontalAlign="Center" Width="10px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="序號" DataField="TD003" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle HorizontalAlign="Center" Width="10px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="品號" DataField="TD004" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle HorizontalAlign="Left" Width="10px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="品名" DataField="TD005" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle HorizontalAlign="Left" Width="100px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="訂單數量" DataField="TD008" DataFormatString="{0:N2}" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle HorizontalAlign="Center" Width="10px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="贈品量" DataField="TD024" DataFormatString="{0:N2}" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle HorizontalAlign="Center" Width="10px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="已交數量" DataField="TD009" DataFormatString="{0:N2}" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle HorizontalAlign="Center" Width="10px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="贈品已交" DataField="TD025" DataFormatString="{0:N2}" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle HorizontalAlign="Center" Width="10px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="單位" DataField="TD010" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle HorizontalAlign="Center" Width="10px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="預交日" DataField="TD013" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle HorizontalAlign="Center" Width="10px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="單頭備註" DataField="TC015" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle HorizontalAlign="Left" Width="200px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="單身備註" DataField="TD020" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle HorizontalAlign="Left" Width="200px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="生管核準" DataField="MOCCHECKS" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle HorizontalAlign="Center" Width="20px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="生管更新日期" DataField="MOCCHECKDATES" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle HorizontalAlign="Center" Width="10px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="生管備註" DataField="MOCCHECKSCOMMENTS" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle HorizontalAlign="Left" Width="200px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="採購核準" DataField="PURCHECKS" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle HorizontalAlign="Center" Width="20px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="採購更新日期" DataField="PURCHECKDATES" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle HorizontalAlign="Center" Width="10px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="採購備註" DataField="PURCHECKSCOMMENTS" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle HorizontalAlign="Left" Width="200px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="業務更新日期" DataField="SALESCHECKDATES" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle HorizontalAlign="Center" Width="10px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="業務備註" DataField="SALESCHECKSCOMMENTS" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle HorizontalAlign="Left" Width="200px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="生管備註填寫" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="GRIDVIEWTextBox1" runat="server" Text="" TextMode="MultiLine" Rows="3"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="生管核準填寫" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="GRIDVIEWDropDownList1" runat="server">
                                                        <asp:ListItem>N</asp:ListItem>
                                                        <asp:ListItem>Y</asp:ListItem>


                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%--  <asp:TemplateField HeaderText="更新進度" ItemStyle-Width="80px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Button ID="Button3" runat="server" Text="更新" ForeColor="Red" CommandName="Button3" CommandArgument='<%# Eval("TD123") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="是否送簽" ItemStyle-Width="60px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Button ID="Button4" runat="server" Text="送簽" CommandName="Button4" ForeColor="Red" CommandArgument='<%# Eval("TD12") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                        </Columns>
                                    </Fast:Grid>
                                </div>
                            </td>
                        </tr>
                    </table>
                </table>
            </div>
        </telerik:RadPageView>
        <telerik:RadPageView ID="RadPageView3" runat="server">
            <div id="tabs-2">
                <table class="PopTable">
                    <table class="PopTable">
                        <tr>
                            <td class="PopTableLeftTD"></td>
                            <td>
                                <asp:Label ID="Label9" runat="server" Text="年度: "></asp:Label>
                                <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
                            </td>

                        </tr>
                        <tr>
                            <td class="PopTableLeftTD"></td>
                            <td>
                                <asp:Label ID="Label10" runat="server" Text="月份: "></asp:Label>
                                <asp:TextBox ID="TextBox6" runat="server"></asp:TextBox>
                            </td>

                        </tr>
                        <tr>
                            <td class="PopTableLeftTD"></td>
                            <td>
                                <asp:Label ID="Label11" runat="server" Text="是否確認: "></asp:Label>
                                <asp:DropDownList ID="DropDownList5" runat="server"></asp:DropDownList>
                            </td>

                        </tr>
                        <tr>
                            <td class="PopTableLeftTD"></td>
                            <td>
                                <asp:Label ID="Label12" runat="server" Text="是否生產: "></asp:Label>
                                <asp:DropDownList ID="DropDownList6" runat="server"></asp:DropDownList>
                            </td>

                        </tr>
                        <tr>
                            <td class="PopTableLeftTD"></td>
                            <td>
                                <asp:Label ID="Label19" runat="server" Text="訂單單號: "></asp:Label>
                                <asp:TextBox ID="TextBox11" runat="server"></asp:TextBox>
                            </td>

                        </tr>
                        <tr>
                            <td class="PopTableLeftTD"></td>
                            <td>
                                <asp:Button ID="Button5" runat="server" Text=" 查詢 " OnClick="Button5_Click"
                                    meta:resourcekey="btn5Resource1" />
                            </td>

                        </tr>
                    </table>
                    <table class="PopTable">
                        <tr>
                            <td class="PopTableLeftTD"></td>
                            <td>
                                <asp:Button ID="Button6" runat="server" Text=" 採購批次更新 " OnClick="Button6_Click"
                                    meta:resourcekey="btn6Resource1" />
                            </td>

                        </tr>
                    </table>
                    <table class="PopTable">
                        <tr>
                            <td colspan="2" class="PopTableRightTD">
                                <div style="overflow-x: auto; width: 100%">
                                    <Fast:Grid ID="Grid3" OnRowDataBound="Grid3_RowDataBound" OnRowCommand="Grid3_RowCommand" runat="server" OnBeforeExport="OnBeforeExport3" AllowPaging="true" AutoGenerateCheckBoxColumn="False" AllowSorting="True" AutoGenerateColumns="False" CustomDropDownListPage="False" DataKeyOnClientWithCheckBox="False" DefaultSortDirection="Ascending" EmptyDataText="No data found" EnhancePager="True" KeepSelectedRows="False" PageSize="100" SelectedRowColor="" UnSelectedRowColor="" meta:resourcekey="Grid1Resource3" OnPageIndexChanging="grid_PageIndexChanging3">
                                        <EnhancePagerSettings FirstImageUrl="" FirstAltImageUrl="" PreviousImageUrl="" NextImageUrl="" LastImageUrl="" LastAltImage="" PageNumberCssClass="" PageNumberCurrentCssClass="" PageInfoCssClass="" PageRedirectCssClass="" NextIAltImageUrl="" PreviousAltImageUrl="" ShowHeaderPager="True"></EnhancePagerSettings>
                                        <ExportExcelSettings AllowExportToExcel="true" ExportType="GridContent"></ExportExcelSettings>
                                        <Columns>
                                            <asp:BoundField HeaderText="客戶" DataField="TC053" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle HorizontalAlign="Left" Width="100px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="單別" DataField="TD001" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle HorizontalAlign="Center" Width="10px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="單號" DataField="TD002" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle HorizontalAlign="Center" Width="10px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="序號" DataField="TD003" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle HorizontalAlign="Center" Width="10px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="品號" DataField="TD004" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle HorizontalAlign="Left" Width="10px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="品名" DataField="TD005" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle HorizontalAlign="Left" Width="100px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="訂單數量" DataField="TD008" DataFormatString="{0:N2}" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle HorizontalAlign="Center" Width="10px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="贈品量" DataField="TD024" DataFormatString="{0:N2}" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle HorizontalAlign="Center" Width="10px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="已交數量" DataField="TD009" DataFormatString="{0:N2}" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle HorizontalAlign="Center" Width="10px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="贈品已交" DataField="TD025" DataFormatString="{0:N2}" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle HorizontalAlign="Center" Width="10px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="單位" DataField="TD010" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle HorizontalAlign="Center" Width="10px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="預交日" DataField="TD013" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle HorizontalAlign="Center" Width="10px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="單頭備註" DataField="TC015" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle HorizontalAlign="Left" Width="200px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="單身備註" DataField="TD020" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle HorizontalAlign="Left" Width="200px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="生管核準" DataField="MOCCHECKS" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle HorizontalAlign="Center" Width="20px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="生管更新日期" DataField="MOCCHECKDATES" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle HorizontalAlign="Center" Width="10px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="生管備註" DataField="MOCCHECKSCOMMENTS" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle HorizontalAlign="Left" Width="200px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="採購核準" DataField="PURCHECKS" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle HorizontalAlign="Center" Width="20px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="採購更新日期" DataField="PURCHECKDATES" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle HorizontalAlign="Center" Width="10px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="採購備註" DataField="PURCHECKSCOMMENTS" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle HorizontalAlign="Left" Width="200px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="業務更新日期" DataField="SALESCHECKDATES" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle HorizontalAlign="Center" Width="10px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="業務備註" DataField="SALESCHECKSCOMMENTS" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle HorizontalAlign="Left" Width="200px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="採購備註填寫" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="GRIDVIEW2TextBox1" runat="server" Text="" TextMode="MultiLine" Rows="3"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="採購核準填寫" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="GRIDVIEW2DropDownList1" runat="server">
                                                        <asp:ListItem>N</asp:ListItem>
                                                        <asp:ListItem>Y</asp:ListItem>


                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%--  <asp:TemplateField HeaderText="更新進度" ItemStyle-Width="80px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Button ID="Button3" runat="server" Text="更新" ForeColor="Red" CommandName="Button3" CommandArgument='<%# Eval("TD123") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="是否送簽" ItemStyle-Width="60px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Button ID="Button4" runat="server" Text="送簽" CommandName="Button4" ForeColor="Red" CommandArgument='<%# Eval("TD12") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                        </Columns>
                                    </Fast:Grid>
                                </div>
                            </td>
                        </tr>
                    </table>
                </table>
            </div>
        </telerik:RadPageView>
        <telerik:RadPageView ID="RadPageView4" runat="server">
            <div id="tabs-2">
                <table class="PopTable">
                    <table class="PopTable">
                        <tr>
                            <td class="PopTableLeftTD"></td>
                            <td>
                                <asp:Label ID="Label13" runat="server" Text="年度: "></asp:Label>
                                <asp:TextBox ID="TextBox7" runat="server"></asp:TextBox>
                            </td>

                        </tr>
                        <tr>
                            <td class="PopTableLeftTD"></td>
                            <td>
                                <asp:Label ID="Label14" runat="server" Text="月份: "></asp:Label>
                                <asp:TextBox ID="TextBox8" runat="server"></asp:TextBox>
                            </td>

                        </tr>
                        <tr>
                            <td class="PopTableLeftTD"></td>
                            <td>
                                <asp:Label ID="Label15" runat="server" Text="是否確認: "></asp:Label>
                                <asp:DropDownList ID="DropDownList7" runat="server"></asp:DropDownList>
                            </td>

                        </tr>
                        <tr>
                            <td class="PopTableLeftTD"></td>
                            <td>
                                <asp:Label ID="Label16" runat="server" Text="是否生產: "></asp:Label>
                                <asp:DropDownList ID="DropDownList8" runat="server"></asp:DropDownList>
                            </td>

                        </tr>
                        <tr>
                            <td class="PopTableLeftTD"></td>
                            <td>
                                <asp:Label ID="Label20" runat="server" Text="訂單單號: "></asp:Label>
                                <asp:TextBox ID="TextBox12" runat="server"></asp:TextBox>
                            </td>

                        </tr>
                        <tr>
                            <td class="PopTableLeftTD"></td>
                            <td>
                                <asp:Button ID="Button7" runat="server" Text=" 查詢 " OnClick="Button7_Click"
                                    meta:resourcekey="btn7Resource1" />
                            </td>

                        </tr>
                    </table>
                    <table class="PopTable">
                        <tr>
                            <td class="PopTableLeftTD"></td>
                            <td>
                                <asp:Button ID="Button8" runat="server" Text=" 業務批次更新 " OnClick="Button8_Click"
                                    meta:resourcekey="btn8Resource1" />
                            </td>

                        </tr>
                    </table>
                    <table class="PopTable">
                        <tr>
                            <td colspan="2" class="PopTableRightTD">
                                <div style="overflow-x: auto; width: 100%">
                                    <Fast:Grid ID="Grid4" OnRowDataBound="Grid4_RowDataBound" OnRowCommand="Grid4_RowCommand" runat="server" OnBeforeExport="OnBeforeExport4" AllowPaging="true" AutoGenerateCheckBoxColumn="False" AllowSorting="True" AutoGenerateColumns="False" CustomDropDownListPage="False" DataKeyOnClientWithCheckBox="False" DefaultSortDirection="Ascending" EmptyDataText="No data found" EnhancePager="True" KeepSelectedRows="False" PageSize="100" SelectedRowColor="" UnSelectedRowColor="" meta:resourcekey="Grid1Resource4" OnPageIndexChanging="grid_PageIndexChanging4">
                                        <EnhancePagerSettings FirstImageUrl="" FirstAltImageUrl="" PreviousImageUrl="" NextImageUrl="" LastImageUrl="" LastAltImage="" PageNumberCssClass="" PageNumberCurrentCssClass="" PageInfoCssClass="" PageRedirectCssClass="" NextIAltImageUrl="" PreviousAltImageUrl="" ShowHeaderPager="True"></EnhancePagerSettings>
                                        <ExportExcelSettings AllowExportToExcel="true" ExportType="GridContent"></ExportExcelSettings>
                                        <Columns>
                                            <asp:BoundField HeaderText="客戶" DataField="TC053" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle HorizontalAlign="Left" Width="100px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="單別" DataField="TD001" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle HorizontalAlign="Center" Width="10px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="單號" DataField="TD002" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle HorizontalAlign="Center" Width="10px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="序號" DataField="TD003" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle HorizontalAlign="Center" Width="10px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="品號" DataField="TD004" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle HorizontalAlign="Left" Width="10px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="品名" DataField="TD005" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle HorizontalAlign="Left" Width="100px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="訂單數量" DataField="TD008" DataFormatString="{0:N2}" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle HorizontalAlign="Center" Width="10px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="贈品量" DataField="TD024" DataFormatString="{0:N2}" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle HorizontalAlign="Center" Width="10px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="已交數量" DataField="TD009" DataFormatString="{0:N2}" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle HorizontalAlign="Center" Width="10px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="贈品已交" DataField="TD025" DataFormatString="{0:N2}" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle HorizontalAlign="Center" Width="10px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="單位" DataField="TD010" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle HorizontalAlign="Center" Width="10px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="預交日" DataField="TD013" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle HorizontalAlign="Center" Width="10px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="單頭備註" DataField="TC015" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle HorizontalAlign="Left" Width="200px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="單身備註" DataField="TD020" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle HorizontalAlign="Left" Width="200px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="生管核準" DataField="MOCCHECKS" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle HorizontalAlign="Center" Width="20px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="生管更新日期" DataField="MOCCHECKDATES" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle HorizontalAlign="Center" Width="10px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="生管備註" DataField="MOCCHECKSCOMMENTS" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle HorizontalAlign="Left" Width="200px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="採購核準" DataField="PURCHECKS" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle HorizontalAlign="Center" Width="20px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="採購更新日期" DataField="PURCHECKDATES" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle HorizontalAlign="Center" Width="10px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="採購備註" DataField="PURCHECKSCOMMENTS" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle HorizontalAlign="Left" Width="200px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="業務更新日期" DataField="SALESCHECKDATES" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle HorizontalAlign="Center" Width="10px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="業務備註" DataField="SALESCHECKSCOMMENTS" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle HorizontalAlign="Left" Width="200px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="業務備註填寫" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="GRIDVIEW4TextBox1" runat="server" Text="" TextMode="MultiLine" Rows="3"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="是否送簽" ItemStyle-Width="60px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Button ID="GRIDVIEW4Button1" runat="server" Text="送簽" CommandName="GRIDVIEW4Button1" ForeColor="Red" CommandArgument='<%# Eval("TD12") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%--  <asp:TemplateField HeaderText="更新進度" ItemStyle-Width="80px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Button ID="Button3" runat="server" Text="更新" ForeColor="Red" CommandName="Button3" CommandArgument='<%# Eval("TD123") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="是否送簽" ItemStyle-Width="60px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Button ID="Button4" runat="server" Text="送簽" CommandName="Button4" ForeColor="Red" CommandArgument='<%# Eval("TD12") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                        </Columns>
                                    </Fast:Grid>
                                </div>
                            </td>
                        </tr>
                    </table>
                </table>
            </div>
        </telerik:RadPageView>
         <telerik:RadPageView ID="RadPageView5" runat="server">
            <div id="tabs-2">
                <table class="PopTable">
                    <table class="PopTable">
                        <tr>
                            <td class="PopTableLeftTD"></td>
                            <td>
                                <asp:Label ID="Label21" runat="server" Text="年度: "></asp:Label>
                                <asp:TextBox ID="TextBox13" runat="server"></asp:TextBox>
                            </td>

                        </tr>
                        <tr>
                            <td class="PopTableLeftTD"></td>
                            <td>
                                <asp:Label ID="Label22" runat="server" Text="月份: "></asp:Label>
                                <asp:TextBox ID="TextBox14" runat="server"></asp:TextBox>
                            </td>

                        </tr>
                        <tr>
                            <td class="PopTableLeftTD"></td>
                            <td>
                                <asp:Label ID="Label23" runat="server" Text="是否確認: "></asp:Label>
                                <asp:DropDownList ID="DropDownList9" runat="server"></asp:DropDownList>
                            </td>

                        </tr>
                        <tr>
                            <td class="PopTableLeftTD"></td>
                            <td>
                                <asp:Label ID="Label24" runat="server" Text="是否生產: "></asp:Label>
                                <asp:DropDownList ID="DropDownList10" runat="server"></asp:DropDownList>
                            </td>

                        </tr>
                        <tr>
                            <td class="PopTableLeftTD"></td>
                            <td>
                                <asp:Label ID="Label25" runat="server" Text="訂單單號: "></asp:Label>
                                <asp:TextBox ID="TextBox15" runat="server"></asp:TextBox>
                            </td>

                        </tr>
                        <tr>
                            <td class="PopTableLeftTD"></td>
                            <td>
                                <asp:Button ID="Button9" runat="server" Text=" 查詢 " OnClick="Button7_Click"
                                    meta:resourcekey="btn7Resource1" />
                            </td>

                        </tr>
                    </table>
                 
                    <table class="PopTable">
                        <tr>
                            <td colspan="2" class="PopTableRightTD">
                                <div style="overflow-x: auto; width: 100%">
                                    <Fast:Grid ID="Grid5" OnRowDataBound="Grid5_RowDataBound" OnRowCommand="Grid5_RowCommand" runat="server" OnBeforeExport="OnBeforeExport5" AllowPaging="true" AutoGenerateCheckBoxColumn="False" AllowSorting="True" AutoGenerateColumns="False" CustomDropDownListPage="False" DataKeyOnClientWithCheckBox="False" DefaultSortDirection="Ascending" EmptyDataText="No data found" EnhancePager="True" KeepSelectedRows="False" PageSize="100" SelectedRowColor="" UnSelectedRowColor="" meta:resourcekey="Grid1Resource5" OnPageIndexChanging="grid_PageIndexChanging5">
                                        <EnhancePagerSettings FirstImageUrl="" FirstAltImageUrl="" PreviousImageUrl="" NextImageUrl="" LastImageUrl="" LastAltImage="" PageNumberCssClass="" PageNumberCurrentCssClass="" PageInfoCssClass="" PageRedirectCssClass="" NextIAltImageUrl="" PreviousAltImageUrl="" ShowHeaderPager="True"></EnhancePagerSettings>
                                        <ExportExcelSettings AllowExportToExcel="true" ExportType="GridContent"></ExportExcelSettings>
                                        <Columns>
                                            <asp:BoundField HeaderText="客戶" DataField="TC053" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle HorizontalAlign="Left" Width="100px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="單別" DataField="TD001" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle HorizontalAlign="Center" Width="10px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="單號" DataField="TD002" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle HorizontalAlign="Center" Width="10px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="序號" DataField="TD003" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle HorizontalAlign="Center" Width="10px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="品號" DataField="TD004" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle HorizontalAlign="Left" Width="10px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="品名" DataField="TD005" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle HorizontalAlign="Left" Width="100px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="訂單數量" DataField="TD008" DataFormatString="{0:N2}" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle HorizontalAlign="Center" Width="10px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="贈品量" DataField="TD024" DataFormatString="{0:N2}" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle HorizontalAlign="Center" Width="10px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="已交數量" DataField="TD009" DataFormatString="{0:N2}" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle HorizontalAlign="Center" Width="10px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="贈品已交" DataField="TD025" DataFormatString="{0:N2}" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle HorizontalAlign="Center" Width="10px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="單位" DataField="TD010" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle HorizontalAlign="Center" Width="10px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="預交日" DataField="TD013" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle HorizontalAlign="Center" Width="10px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="單頭備註" DataField="TC015" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle HorizontalAlign="Left" Width="200px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="單身備註" DataField="TD020" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle HorizontalAlign="Left" Width="200px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="生管核準" DataField="MOCCHECKS" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle HorizontalAlign="Center" Width="20px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="生管更新日期" DataField="MOCCHECKDATES" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle HorizontalAlign="Center" Width="10px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="生管備註" DataField="MOCCHECKSCOMMENTS" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle HorizontalAlign="Left" Width="200px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="採購核準" DataField="PURCHECKS" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle HorizontalAlign="Center" Width="20px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="採購更新日期" DataField="PURCHECKDATES" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle HorizontalAlign="Center" Width="10px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="採購備註" DataField="PURCHECKSCOMMENTS" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle HorizontalAlign="Left" Width="200px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="業務更新日期" DataField="SALESCHECKDATES" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle HorizontalAlign="Center" Width="10px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="業務備註" DataField="SALESCHECKSCOMMENTS" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle HorizontalAlign="Left" Width="200px"></ItemStyle>
                                            </asp:BoundField>                                   
                                            <asp:TemplateField HeaderText="是否送簽" ItemStyle-Width="60px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Button ID="GRIDVIEW5Button1" runat="server" Text="送簽" CommandName="GRIDVIEW5Button1" ForeColor="Red" CommandArgument='<%# Eval("TD12") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%--  <asp:TemplateField HeaderText="更新進度" ItemStyle-Width="80px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Button ID="Button3" runat="server" Text="更新" ForeColor="Red" CommandName="Button3" CommandArgument='<%# Eval("TD123") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="是否送簽" ItemStyle-Width="60px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Button ID="Button4" runat="server" Text="送簽" CommandName="Button4" ForeColor="Red" CommandArgument='<%# Eval("TD12") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                        </Columns>
                                    </Fast:Grid>
                                </div>
                            </td>
                        </tr>
                    </table>
                </table>
            </div>
        </telerik:RadPageView>
        <telerik:RadPageView ID="RadPageView99" runat="server">
            <div id="tabs-99">
            </div>
        </telerik:RadPageView>
    </telerik:RadMultiPage>​
</asp:Content>

