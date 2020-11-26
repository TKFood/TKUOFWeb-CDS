<%@ Page Title="" Language="C#" MasterPageFile="~/Master/DefaultMasterPage.master" AutoEventWireup="true" CodeFile="TBBU_COPCONDTIONS.aspx.cs" Inherits="CDS_WebPage_COP_TBBU_COPCONDTIONS" %>
<%@ Register Assembly="Ede.Uof.Utility.Component.Grid" Namespace="Ede.Uof.Utility.Component" TagPrefix="Fast" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

      <telerik:RadTabStrip ID="RadTabStrip1" runat="server"></telerik:RadTabStrip>
    <telerik:RadTabStrip ID="RadTabStrip2" runat="server" MultiPageID="RadMultiPage" SelectedIndex="0">
	    <Tabs>
		    <telerik:RadTab Text="國內商務出貨標準">
		    </telerik:RadTab>
		    <telerik:RadTab Text="">
		    </telerik:RadTab>
	    </Tabs>
    </telerik:RadTabStrip>
	<telerik:RadMultiPage ID="RadMultiPage" runat="server" SelectedIndex="0">
	    <telerik:RadPageView ID="RadPageView1" runat="server" Selected="true">
		   <div id="tabs-1">
             <table class="PopTable">
                 <tr>
                    <td colspan="2" class="PopTableRightTD" >
                        <div style="overflow-x:auto;width:100%">
                            <Fast:Grid ID="Grid1" OnRowDataBound="Grid1_RowDataBound" runat="server"  OnBeforeExport="OnBeforeExport1" AllowPaging="true"  AutoGenerateCheckBoxColumn="False" AllowSorting="True" AutoGenerateColumns="False" CustomDropDownListPage="False" DataKeyOnClientWithCheckBox="False" DefaultSortDirection="Ascending" EmptyDataText="No data found" EnhancePager="True" KeepSelectedRows="False" PageSize="100" SelectedRowColor="" UnSelectedRowColor="" meta:resourcekey="Grid2Resource1" OnPageIndexChanging="grid1_PageIndexChanging" >
                                <EnhancePagerSettings FirstImageUrl="" FirstAltImageUrl="" PreviousImageUrl="" NextImageUrl="" LastImageUrl="" LastAltImage="" PageNumberCssClass="" PageNumberCurrentCssClass="" PageInfoCssClass="" PageRedirectCssClass="" NextIAltImageUrl="" PreviousAltImageUrl="" ShowHeaderPager="True"></EnhancePagerSettings>                 
                                    <ExportExcelSettings AllowExportToExcel="true" ExportType="DataSource" ></ExportExcelSettings>
                                        <Columns>
                                             <asp:BoundField HeaderText="序號" DataField="SERNO" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" >
                                                <ItemStyle HorizontalAlign="Center" Width="10px"></ItemStyle>
                                            </asp:BoundField>  
                                              <asp:BoundField HeaderText="客戶代號" DataField="MA001" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" >
                                                <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                                            </asp:BoundField>  
                                              <asp:BoundField HeaderText="客戶名稱" DataField="MA002" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" >
                                                <ItemStyle HorizontalAlign="Left" Width="100px"></ItemStyle>
                                            </asp:BoundField>  
                                              <asp:BoundField HeaderText="連絡人" DataField="CONTACTPERSON" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" >
                                                <ItemStyle HorizontalAlign="Left" Width="100px"></ItemStyle>
                                            </asp:BoundField>  
                                              <asp:BoundField HeaderText="電話1" DataField="TEL1" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" >
                                                <ItemStyle HorizontalAlign="Left" Width="100px"></ItemStyle>
                                            </asp:BoundField>  
                                             <asp:BoundField HeaderText="電話2" DataField="TEL2" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" >
                                                <ItemStyle HorizontalAlign="Left" Width="100px"></ItemStyle>
                                            </asp:BoundField>  
                                              <asp:BoundField HeaderText="採購單(附單)" DataField="ISPURATTCH" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" >
                                                <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                                            </asp:BoundField>  
                                              <asp:BoundField HeaderText="銷貨單(附單)" DataField="ISCOPATTCH" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" >
                                                <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                                            </asp:BoundField>  
                                              <asp:BoundField HeaderText="是否顯$(附單)" DataField="ISSHOWMONEYS" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" >
                                                <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                                            </asp:BoundField>  
                                              <asp:BoundField HeaderText="發票(附單)" DataField="ISINVOICES" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" >
                                                <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                                            </asp:BoundField>  
                                              <asp:BoundField HeaderText="麥頭(附單)" DataField="ISSHIPMARK" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" >
                                                <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                                            </asp:BoundField>  
                                              <asp:BoundField HeaderText="允收期限" DataField="LIMITDAYS" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" >
                                                <ItemStyle HorizontalAlign="Left" Width="100px"></ItemStyle>
                                            </asp:BoundField>  
                                              <asp:BoundField HeaderText="收款條件" DataField="PAYMENT" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" >
                                                <ItemStyle HorizontalAlign="Left" Width="100px"></ItemStyle>
                                            </asp:BoundField>  
                                              <asp:BoundField HeaderText="寄送地址" DataField="SENDADDRESS" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" >
                                                <ItemStyle HorizontalAlign="Left" Width="100px"></ItemStyle>
                                            </asp:BoundField>  
                                             <asp:BoundField HeaderText="備註" DataField="COMMENT" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" >
                                                <ItemStyle HorizontalAlign="Left" Width="100px"></ItemStyle>
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
		    <div id="tabs-2">
              
          </div>
	    </telerik:RadPageView>
    </telerik:RadMultiPage>​
</asp:Content>

