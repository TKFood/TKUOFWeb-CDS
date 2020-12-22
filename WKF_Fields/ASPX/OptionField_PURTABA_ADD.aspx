<%@ Page Title="" Language="C#" MasterPageFile="~/Master/DialogMasterPage.master" AutoEventWireup="true" CodeFile="OptionField_PURTABA_ADD.aspx.cs" Inherits="CDS_WKF_Fields_ASPX_OptionField_PURTABA_ADD" %>
<%@ Register Assembly="Ede.Uof.Utility.Component.Grid" Namespace="Ede.Uof.Utility.Component" TagPrefix="Fast" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
<script>
    
</script>

<table class="" style="width:100%">
    <tr>
        <td  class="">
            <asp:Label ID="Label1" runat="server" Text="品號:"></asp:Label>
            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
            <asp:Button ID="Button2" runat="server" Text="查詢" OnClick="Button2_OnClick" />
        </td>
    </tr>
</table>
 <table class="PopTable">
         <tr >
            <td colspan="2" class="PopTableRightTD" >
                <div style="overflow-x:auto;width:100%">
                    <Fast:Grid ID="Grid1" OnRowDataBound="Grid1_RowDataBound" runat="server" OnBeforeExport="OnBeforeExport" AllowPaging="true"  AutoGenerateCheckBoxColumn="False" AllowSorting="True" AutoGenerateColumns="False" CustomDropDownListPage="False" DataKeyOnClientWithCheckBox="False" DefaultSortDirection="Ascending" EmptyDataText="No data found" EnhancePager="True" KeepSelectedRows="False" PageSize="100" SelectedRowColor="" UnSelectedRowColor="" meta:resourcekey="Grid1Resource1" >
                        <EnhancePagerSettings FirstImageUrl="" FirstAltImageUrl="" PreviousImageUrl="" NextImageUrl="" LastImageUrl="" LastAltImage="" PageNumberCssClass="" PageNumberCurrentCssClass="" PageInfoCssClass="" PageRedirectCssClass="" NextIAltImageUrl="" PreviousAltImageUrl="" ShowHeaderPager="True"></EnhancePagerSettings>
                 
                            <ExportExcelSettings AllowExportToExcel="true" ExportType="DataSource" ></ExportExcelSettings>
                            <Columns>
                                <asp:TemplateField HeaderText="選取" ItemStyle-Width="20px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" >                
                                    <ItemTemplate>
                                        <asp:Button ID="Button1" runat="server" Text="選取" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="20px"></ItemStyle>
                                </asp:TemplateField> 
                                  <asp:BoundField HeaderText="品號" DataField="MB001" ItemStyle-Width="200px" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" >
                                    <ItemStyle HorizontalAlign="Left" Width="200px"></ItemStyle>
                                </asp:BoundField> 
                                     <asp:BoundField HeaderText="品名" DataField="MB002" ItemStyle-Width="200px" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" >
                                    <ItemStyle HorizontalAlign="Left" Width="200px"></ItemStyle>
                                </asp:BoundField> 
                                
                            </Columns>
                        </Fast:Grid>
                    </div>
            </td>
        </tr>
    </table>

</asp:Content>

