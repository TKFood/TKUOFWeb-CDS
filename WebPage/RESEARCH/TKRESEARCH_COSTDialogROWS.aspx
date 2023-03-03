<%@ Page Title="" Language="C#" MasterPageFile="~/Master/DialogMasterPage.master" AutoEventWireup="true" CodeFile="TKRESEARCH_COSTDialogROWS.aspx.cs" Inherits="CDS_WebPage_TKRESEARCH_COSTDialogROWS" %>

<%@ Register Src="~/Common/HtmlEditor/UC_HtmlEditor.ascx" TagPrefix="uc1" TagName="UC_HtmlEditor" %>
<%@ Register Assembly="Ede.Uof.Utility.Component.Grid" Namespace="Ede.Uof.Utility.Component" TagPrefix="Fast" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <link rel="stylesheet" href="http://code.jquery.com/ui/1.10.4/themes/trontastic/jquery-ui.css">

    <script>
        $(function () {


        });

    </script>
    <div style="overflow-x: auto; width: 800px">

        <table class="PopTable">
            <tr>
                <td class="PopTableLeftTD">
                    <asp:Label ID="Label1" runat="server" Text="TKRESEARCH_COSTDialogROWS"></asp:Label>
                </td>
                <td class="PopTableRightTD" colspan="2">
                    <asp:Label ID="lblParam" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </table>
        <table class="PopTable">
            <tr>
                <td colspan="2" class="PopTableRightTD">
                    <Fast:Grid ID="Grid1" runat="server" AutoGenerateCheckBoxColumn="False" AllowSorting="True" AutoGenerateColumns="False" CustomDropDownListPage="False" DataKeyOnClientWithCheckBox="False" DefaultSortDirection="Ascending" EmptyDataText="No data found" EnhancePager="True" KeepSelectedRows="False" PageSize="15" SelectedRowColor="" UnSelectedRowColor="" meta:resourcekey="Grid1Resource1">
                        <EnhancePagerSettings FirstImageUrl="" FirstAltImageUrl="" PreviousImageUrl="" NextImageUrl="" LastImageUrl="" LastAltImage="" PageNumberCssClass="" PageNumberCurrentCssClass="" PageInfoCssClass="" PageRedirectCssClass="" NextIAltImageUrl="" PreviousAltImageUrl="" ShowHeaderPager="True"></EnhancePagerSettings>
                        <ExportExcelSettings AllowExportToExcel="False"></ExportExcelSettings>
                        <Columns>
                            <asp:BoundField HeaderText="原料品名" DataField="MB002" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" SortExpression="MEMODATES">
                                <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField HeaderText="原料規格" DataField="MB003" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                <ItemStyle HorizontalAlign="Left" Width="100px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField HeaderText="原料單價" DataField="COSTROW" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                <ItemStyle HorizontalAlign="Left" Width="100px"></ItemStyle>
                            </asp:BoundField>
                        </Columns>
                    </Fast:Grid>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

