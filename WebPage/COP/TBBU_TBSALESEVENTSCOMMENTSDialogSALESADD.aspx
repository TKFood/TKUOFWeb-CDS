<%@ Page Title="" Language="C#" MasterPageFile="~/Master/DialogMasterPage.master" AutoEventWireup="true" CodeFile="TBBU_TBSALESEVENTSCOMMENTSDialogSALESADD.aspx.cs" Inherits="CDS_WebPage_TBBU_TBSALESEVENTSCOMMENTSDialogSALESADD" %>

<%@ Register Src="~/Common/HtmlEditor/UC_HtmlEditor.ascx" TagPrefix="uc1" TagName="UC_HtmlEditor" %>
<%@ Register Assembly="Ede.Uof.Utility.Component.Grid" Namespace="Ede.Uof.Utility.Component" TagPrefix="Fast" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <link rel="stylesheet" href="http://code.jquery.com/ui/1.10.4/themes/trontastic/jquery-ui.css">

    <script>

</script>

    <table class="PopTable">
        <tr>
            <td class="PopTableLeftTD">
                <asp:Label ID="Label1" runat="server" Text="TKRESEARCHTBSALESDEVDialogSALESADD"></asp:Label>
            </td>
            <td class="PopTableRightTD" colspan="2">
                <asp:Label ID="lblParam" runat="server" Text=""></asp:Label>
            </td>
        </tr>
    </table>
    <table class="PopTable">
        <tr>
            <td class="PopTableLeftTD">
                <asp:Label ID="Label3" runat="server" Text="待辦事件"></asp:Label>
            </td>
            <td class="PopTableRightTD">
                <asp:TextBox ID="TextBox2" runat="server" Text="" Width="100%" TextMode="MultiLine" Rows="10"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="PopTableLeftTD">
                <asp:Label ID="Label2" runat="server" Text="進度"></asp:Label>
            </td>
            <td class="PopTableRightTD">
                <asp:TextBox ID="TextBox1" runat="server" Text="" Width="100%" TextMode="MultiLine" Rows="10"></asp:TextBox>
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
                        <asp:BoundField HeaderText="填寫日期" DataField="ADDDATES" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" SortExpression="MEMODATES">
                            <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                        </asp:BoundField>

                        <asp:BoundField HeaderText="進度" DataField="COMMENTS" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                            <ItemStyle HorizontalAlign="Left" Width="600px"></ItemStyle>
                        </asp:BoundField>
                    </Columns>
                </Fast:Grid>
            </td>
        </tr>
    </table>

</asp:Content>

