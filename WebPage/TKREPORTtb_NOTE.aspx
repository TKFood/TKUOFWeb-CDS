<%@ Page Title="" Language="C#" MasterPageFile="~/Master/DefaultMasterPage.master" AutoEventWireup="true" CodeFile="TKREPORTtb_NOTE.aspx.cs" Inherits="CDS_WebPage_TKREPORTtb_NOTE" %>
<%@ Register Assembly="Ede.Uof.Utility.Component.Grid" Namespace="Ede.Uof.Utility.Component" TagPrefix="Fast" %>



<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script>    
   



</script>

    <table class="PopTable" >         
         <tr>
            <td class="PopTableLeftTD">
                <asp:Label ID="Label2" runat="server" Text="狀態:" meta:resourcekey="Label4Resource1"></asp:Label>             
            </td>
            <td class="PopTableRightTD"> 
                   <asp:DropDownList ID="DropDownList1" runat="server"></asp:DropDownList>
                <asp:Button ID="Button1" runat="server" Text=" 查詢 " OnClientClick="return btn5_Click(this)" 
                    onclick="btn6_Click" meta:resourcekey="btn4Resource1" />                
            </td>            
        </tr>
    </table>
    <table class="PopTable">
            <td colspan="2" class="PopTableRightTD" >
                <div style="overflow-x:auto;width:100%">
                    <Fast:Grid ID="Grid1" OnRowDataBound="Grid1_RowDataBound" runat="server" OnBeforeExport="OnBeforeExport" AllowPaging="true"  AutoGenerateCheckBoxColumn="False" AllowSorting="True" AutoGenerateColumns="False" CustomDropDownListPage="False" DataKeyOnClientWithCheckBox="False" DefaultSortDirection="Ascending" EmptyDataText="No data found" EnhancePager="True" KeepSelectedRows="False" PageSize="15" SelectedRowColor="" UnSelectedRowColor="" meta:resourcekey="Grid1Resource1" OnPageIndexChanging="grid_PageIndexChanging" >
                        <EnhancePagerSettings FirstImageUrl="" FirstAltImageUrl="" PreviousImageUrl="" NextImageUrl="" LastImageUrl="" LastAltImage="" PageNumberCssClass="" PageNumberCurrentCssClass="" PageInfoCssClass="" PageRedirectCssClass="" NextIAltImageUrl="" PreviousAltImageUrl="" ShowHeaderPager="True"></EnhancePagerSettings>
                 
                            <ExportExcelSettings AllowExportToExcel="true" ExportType="DataSource" ></ExportExcelSettings>
                            <Columns>
                                 <asp:BoundField HeaderText="客戶" DataField="COMPANY_NAME" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" >
                                    <ItemStyle HorizontalAlign="Center" Width="200px"></ItemStyle>
                                </asp:BoundField>   
                                 <asp:BoundField HeaderText="記錄" DataField="NOTE_CONTENT" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" >
                                    <ItemStyle HorizontalAlign="Left" Width="600px"></ItemStyle>
                                </asp:BoundField>   
                                 <asp:BoundField HeaderText="記錄日期" DataField="CREATE_DATETIME" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" >
                                    <ItemStyle HorizontalAlign="Center" Width="200px"></ItemStyle>
                                </asp:BoundField>   
                            </Columns>
                        </Fast:Grid>
                    </div>
            </td>
        </tr>
    </table>
</asp:Content>

