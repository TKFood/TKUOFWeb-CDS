<%@ Page Title="" Language="C#" MasterPageFile="~/Master/DefaultMasterPage.master" AutoEventWireup="true" CodeFile="COPTCCOPTD.aspx.cs" Inherits="CDS_WebPage_SIGN_COPTCCOPTD" %>
<%@ Register Assembly="Ede.Uof.Utility.Component.Grid" Namespace="Ede.Uof.Utility.Component" TagPrefix="Fast" %>





<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script>    
<%--    $(function () {
        $("#<%= txtDate1.ClientID %>").datepicker({ dateFormat: "yymmdd", });       
    });--%>

   
</script>
     <table class="PopTable" >         
         <tr>
            <td class="PopTableLeftTD">
                <asp:Label ID="Label2" runat="server" Text="日期:" meta:resourcekey="Label1Resource1"></asp:Label>             
            </td>
            <td class="PopTableRightTD">                    
                <asp:TextBox ID="txtDate1"  runat="server" Width = "100px"></asp:TextBox>           
                <asp:Button ID="Button1" runat="server" Text=" 查詢 "
                    onclick="btn1_Click" meta:resourcekey="btn1Resource1" />                
            </td>            
        </tr>
    </table>
    <label>訂單</label>

    <table class="PopTable">
          <tr>
            <td colspan="2" class="PopTableRightTD" >
                <div style="overflow-x:auto;width:100%">
                    <Fast:Grid ID="Grid1" OnRowDataBound="Grid1_RowDataBound" runat="server"  OnBeforeExport="OnBeforeExport1" AllowPaging="true"  AutoGenerateCheckBoxColumn="False" AllowSorting="True" AutoGenerateColumns="False" CustomDropDownListPage="False" DataKeyOnClientWithCheckBox="False" DefaultSortDirection="Ascending" EmptyDataText="No data found" EnhancePager="True" KeepSelectedRows="False" PageSize="100" SelectedRowColor="" UnSelectedRowColor="" meta:resourcekey="Grid1Resource1" OnPageIndexChanging="grid_PageIndexChanging" >
                        <EnhancePagerSettings FirstImageUrl="" FirstAltImageUrl="" PreviousImageUrl="" NextImageUrl="" LastImageUrl="" LastAltImage="" PageNumberCssClass="" PageNumberCurrentCssClass="" PageInfoCssClass="" PageRedirectCssClass="" NextIAltImageUrl="" PreviousAltImageUrl="" ShowHeaderPager="True"></EnhancePagerSettings>
                 
                            <ExportExcelSettings AllowExportToExcel="true" ExportType="DataSource" ></ExportExcelSettings>
                            <Columns>
                                  <asp:TemplateField HeaderText="CheckBox" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CheckBox" AutoCallBack="true" runat="server"></asp:CheckBox>
                                    </ItemTemplate>
                                  </asp:TemplateField>                               
                                 <asp:BoundField HeaderText="訂單單別" DataField="TC001" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" >
                                    <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                                </asp:BoundField>  
                                 <asp:BoundField HeaderText="訂單單號" DataField="TC002" ItemStyle-Width="200px" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" >
                                    <ItemStyle HorizontalAlign="Center" Width="200px"></ItemStyle>
                                </asp:BoundField>  
                                   <asp:BoundField HeaderText="訂單金額" DataField="TC029" ItemStyle-Width="200px" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" >
                                    <ItemStyle HorizontalAlign="Center" Width="200px"></ItemStyle>
                                </asp:BoundField>  
                                   <asp:BoundField HeaderText="訂單稅額" DataField="TC030" ItemStyle-Width="200px" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" >
                                    <ItemStyle HorizontalAlign="Center" Width="200px"></ItemStyle>
                                </asp:BoundField>  
                                   <asp:BoundField HeaderText="訂單合計" DataField="MONEYS" ItemStyle-Width="200px" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" >
                                    <ItemStyle HorizontalAlign="Center" Width="200px"></ItemStyle>
                                </asp:BoundField>  
                                
                                
                            </Columns>
                        </Fast:Grid>
                    </div>
            </td>
        </tr>
    </table>
    <table class="PopTable" >         
         <tr>
            <td class="PopTableLeftTD">
                <asp:Label ID="Label1" runat="server" Text="簽核" meta:resourcekey="Label2Resource1"></asp:Label>             
            </td>
            <td class="PopTableRightTD">                                    
                <asp:Button ID="Button2" runat="server" Text=" 簽核 " onclick="btn2_Click" meta:resourcekey="btn2Resource1" Width="200px" BackColor="red" />                
            </td>            
        </tr>
    </table>
        <table class="PopTable" >         
         <tr>
            <td class="PopTableLeftTD">
                <asp:Label ID="Label3" runat="server" Text="" meta:resourcekey="Label2Resource1"></asp:Label>             
            </td>          
        </tr>
    </table>

</asp:Content>

