<%@ Page Title="" Language="C#" MasterPageFile="~/Master/DialogMasterPage.master" AutoEventWireup="true" CodeFile="OptionField_PURTABA_ADD.aspx.cs" Inherits="CDS_WKF_Fields_ASPX_OptionField_PURTABA_ADD" %>
<%@ Register Assembly="Ede.Uof.Utility.Component.Grid" Namespace="Ede.Uof.Utility.Component" TagPrefix="Fast" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
<script>
    
</script>

<table class="" style="width:500px">
    <tr>
        <td  class="">
            <asp:Label ID="Label1" runat="server" Text="品號:"></asp:Label>
            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        </td>
    </tr>
</table>
 <table class="PopTable">
         <tr >
            <td colspan="2" class="PopTableRightTD" >
                <div style="overflow-x:auto;width:1400px">
                    <Fast:Grid ID="Grid1" OnRowDataBound="Grid1_RowDataBound" runat="server" OnBeforeExport="OnBeforeExport" AllowPaging="true"  AutoGenerateCheckBoxColumn="False" AllowSorting="True" AutoGenerateColumns="False" CustomDropDownListPage="False" DataKeyOnClientWithCheckBox="False" DefaultSortDirection="Ascending" EmptyDataText="No data found" EnhancePager="True" KeepSelectedRows="False" PageSize="100" SelectedRowColor="" UnSelectedRowColor="" meta:resourcekey="Grid1Resource1" >
                        <EnhancePagerSettings FirstImageUrl="" FirstAltImageUrl="" PreviousImageUrl="" NextImageUrl="" LastImageUrl="" LastAltImage="" PageNumberCssClass="" PageNumberCurrentCssClass="" PageInfoCssClass="" PageRedirectCssClass="" NextIAltImageUrl="" PreviousAltImageUrl="" ShowHeaderPager="True"></EnhancePagerSettings>
                 
                            <ExportExcelSettings AllowExportToExcel="true" ExportType="DataSource" ></ExportExcelSettings>
                            <Columns>
                                <asp:TemplateField HeaderText="選取" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" >                
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtnName" runat="server" Text='<%# Bind("MB001") %>' meta:resourcekey="lbtnNameResource1"></asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                                </asp:TemplateField> 
                                  <asp:BoundField HeaderText="品號" DataField="MB001" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" >
                                    <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                                </asp:BoundField> 
                                
                            </Columns>
                        </Fast:Grid>
                    </div>
            </td>
        </tr>
    </table>

</asp:Content>

