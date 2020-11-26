<%@ Page Title="" Language="C#" MasterPageFile="~/Master/DefaultMasterPage.master" AutoEventWireup="true" CodeFile="TKREPORTtb_NOTE.aspx.cs" Inherits="CDS_WebPage_TKREPORTtb_NOTE" %>
<%@ Register Assembly="Ede.Uof.Utility.Component.Grid" Namespace="Ede.Uof.Utility.Component" TagPrefix="Fast" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<link href="CSS/NEWStyleSheet.css" type="text/css" rel="stylesheet" />


<script>    
    $(function () {
        $("#<%= txtDate1.ClientID %>").datepicker({ dateFormat: "yy/mm/dd", });
        $("#<%= txtDate2.ClientID %>").datepicker({ dateFormat: "yy/mm/dd", });      
    });

    function ImageClick(img) {
        //alert(img);
        var clientId = '#' + img.id

        var widthString = $(clientId).css("width");
        var heightString = $(clientId).css("height");
        var widthUnit = widthString.replace("px", "");
        var heightUnit = heightString.replace("px", "");
        var width = parseInt(widthUnit, 10);
        var height = parseInt(heightUnit, 10);



        if ($(clientId).hasClass("BigImage")) {
            width = (width / 10);
            height = (height / 10);
            $(clientId).css("width", width + "px");
            $(clientId).css("height", height + "px");
            $(clientId).removeClass("BigImage");
        }
        else {
            width = (width * 10);
            height = (height * 10);
            $(clientId).css("width", width + "px");
            $(clientId).css("height", height + "px");
            $(clientId).addClass("BigImage");
        }

    }

    function CellClick(ImgUrl) {
        //alert(ImgUrl);
        $(".Show_imgStyle").attr("src", ImgUrl);
        $("#Show_img_Max").css("display", "block");
       
    }
    function divClick() {
        //alert("Show_img_Max");
        $("#Show_img_Max").css("display", "none");
    }
</script>

    <table class="PopTable" >         
         <tr>
            <td class="PopTableLeftTD">
                <asp:Label ID="Label2" runat="server" Text="日期:" meta:resourcekey="Label4Resource1"></asp:Label>             
            </td>
            <td class="PopTableRightTD">                    
                <asp:TextBox ID="txtDate1"  runat="server" Width = "100px"></asp:TextBox>
                <asp:Label ID="Label11" runat="server" Text="~"></asp:Label>
                <asp:TextBox ID="txtDate2"  runat="server" Width = "100px"></asp:TextBox>
                <asp:Label ID="Label12" runat="server" Text=" "></asp:Label>
                <asp:Button ID="Button1" runat="server" Text=" 查詢 "
                    onclick="btn1_Click" meta:resourcekey="btn4Resource1" />                
            </td>            
        </tr>
    </table>
    <label>客戶記錄-主管決議:是</label>
    <table class="PopTable">
          <tr>
            <td colspan="2" class="PopTableRightTD" >
                <div style="overflow-x:auto;width:100%">
                    <Fast:Grid ID="Grid1" OnRowDataBound="Grid1_RowDataBound" runat="server"  OnBeforeExport="OnBeforeExport" AllowPaging="true"  AutoGenerateCheckBoxColumn="False" AllowSorting="True" AutoGenerateColumns="False" CustomDropDownListPage="False" DataKeyOnClientWithCheckBox="False" DefaultSortDirection="Ascending" EmptyDataText="No data found" EnhancePager="True" KeepSelectedRows="False" PageSize="100" SelectedRowColor="" UnSelectedRowColor="" meta:resourcekey="Grid1Resource1" OnPageIndexChanging="grid_PageIndexChanging" >
                        <EnhancePagerSettings FirstImageUrl="" FirstAltImageUrl="" PreviousImageUrl="" NextImageUrl="" LastImageUrl="" LastAltImage="" PageNumberCssClass="" PageNumberCurrentCssClass="" PageInfoCssClass="" PageRedirectCssClass="" NextIAltImageUrl="" PreviousAltImageUrl="" ShowHeaderPager="True"></EnhancePagerSettings>
                 
                            <ExportExcelSettings AllowExportToExcel="true" ExportType="DataSource" ></ExportExcelSettings>
                            <Columns>
                                 <asp:BoundField HeaderText="業務" DataField="USER_NAME" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" >
                                    <ItemStyle HorizontalAlign="Center" Width="140px"></ItemStyle>
                                </asp:BoundField>   
                                 <asp:BoundField HeaderText="客戶" DataField="COMPANY_NAME" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" >
                                    <ItemStyle HorizontalAlign="Center" Width="200px"></ItemStyle>
                                </asp:BoundField>   
                                <asp:TemplateField HeaderText="記錄" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" >
                                    <ItemTemplate>
                                        <asp:Label ID="CONTENT" runat="server" Text='<%# Bind("NOTE_CONTENT") %>' style="text-align:left" HorizontalAlign="Left" Width="600px" ItemStyle-HorizontalAlign="Left"></asp:Label>
                                        <ItemStyle HorizontalAlign="Left" Width="600px"></ItemStyle>
                                    </ItemTemplate>                                        
                                 </asp:TemplateField>
                               <%--  <asp:BoundField HeaderText="記錄" DataField="NOTE_CONTENT" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" >
                                    <ItemStyle HorizontalAlign="Left" Width="600px"></ItemStyle>
                                </asp:BoundField> --%>  
                                 <asp:BoundField HeaderText="記錄日期" DataField="CREATE_DATETIME" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" >
                                    <ItemStyle HorizontalAlign="Center" Width="200px"></ItemStyle>
                                </asp:BoundField> 
                               
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Image ID="Image1" runat="server"  HorizontalAlign="Center"  Length="100px" Width="100px" onclick="ImageClick(this)"/>
                                    </ItemTemplate >
                                </asp:TemplateField>
                                
                            </Columns>
                        </Fast:Grid>
                    </div>
            </td>
        </tr>
    </table>
    <label>專案記錄-主管決議:是</label>
      <table class="PopTable">
            <tr>
            <td colspan="2" class="PopTableRightTD" >
                <div style="overflow-x:auto;width:100%">
                    <Fast:Grid ID="Grid3" OnRowDataBound="Grid3_RowDataBound" runat="server"  OnBeforeExport="OnBeforeExport" AllowPaging="true"  AutoGenerateCheckBoxColumn="False" AllowSorting="True" AutoGenerateColumns="False" CustomDropDownListPage="False" DataKeyOnClientWithCheckBox="False" DefaultSortDirection="Ascending" EmptyDataText="No data found" EnhancePager="True" KeepSelectedRows="False" PageSize="100" SelectedRowColor="" UnSelectedRowColor="" meta:resourcekey="Grid1Resource3" OnPageIndexChanging="grid3_PageIndexChanging" >
                        <EnhancePagerSettings FirstImageUrl="" FirstAltImageUrl="" PreviousImageUrl="" NextImageUrl="" LastImageUrl="" LastAltImage="" PageNumberCssClass="" PageNumberCurrentCssClass="" PageInfoCssClass="" PageRedirectCssClass="" NextIAltImageUrl="" PreviousAltImageUrl="" ShowHeaderPager="True"></EnhancePagerSettings>
                 
                            <ExportExcelSettings AllowExportToExcel="true" ExportType="DataSource" ></ExportExcelSettings>
                            <Columns>
                                <asp:BoundField HeaderText="業務" DataField="USER_NAME" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" >
                                    <ItemStyle HorizontalAlign="Center" Width="140px"></ItemStyle>
                                </asp:BoundField>   
                                 <asp:BoundField HeaderText="客戶名稱" DataField="COMPANY_NAME" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" >
                                    <ItemStyle HorizontalAlign="Center" Width="200px"></ItemStyle>
                                </asp:BoundField>  
                                 <asp:BoundField HeaderText="專案名稱" DataField="OPPORTUNITY_NAME" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" >
                                    <ItemStyle HorizontalAlign="Center" Width="200px"></ItemStyle>
                                </asp:BoundField>  
                                 <asp:BoundField HeaderText="商品" DataField="PRODUCT" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" >
                                    <ItemStyle HorizontalAlign="Left" Width="200px"></ItemStyle>
                                </asp:BoundField>   
                                <asp:BoundField HeaderText="專案金額" DataField="AMOUNT" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" >
                                    <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                                </asp:BoundField> 
                                 <asp:TemplateField HeaderText="記錄" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" >
                                    <ItemTemplate>
                                        <asp:Label ID="CONTENT" runat="server" Text='<%# Bind("NOTE_CONTENT") %>' style="text-align:left" HorizontalAlign="Left" Width="600px" ItemStyle-HorizontalAlign="Left"></asp:Label>
                                        <ItemStyle HorizontalAlign="Left" Width="600px"></ItemStyle>
                                    </ItemTemplate>                                        
                                 </asp:TemplateField>

                               <%--  <asp:BoundField HeaderText="記錄" DataField="NOTE_CONTENT" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" >
                                    <ItemStyle HorizontalAlign="Left" Width="600px"></ItemStyle>
                                </asp:BoundField>   --%>
                                 <asp:BoundField HeaderText="記錄日期" DataField="CREATE_DATETIME" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" >
                                    <ItemStyle HorizontalAlign="Center" Width="200px"></ItemStyle>
                                </asp:BoundField>   
                            </Columns>
                        </Fast:Grid>
                    </div>
            </td>
        </tr>
    </table>

    <label>客戶記錄-主管決議:否</label>
     <table class="PopTable">
           <tr>
            <td colspan="2" class="PopTableRightTD" >
                <div style="overflow-x:auto;width:100%">
                    <Fast:Grid ID="Grid2" OnRowDataBound="Grid2_RowDataBound" runat="server"  OnBeforeExport="OnBeforeExport" AllowPaging="true"  AutoGenerateCheckBoxColumn="False" AllowSorting="True" AutoGenerateColumns="False" CustomDropDownListPage="False" DataKeyOnClientWithCheckBox="False" DefaultSortDirection="Ascending" EmptyDataText="No data found" EnhancePager="True" KeepSelectedRows="False" PageSize="100" SelectedRowColor="" UnSelectedRowColor="" meta:resourcekey="Grid1Resource2" OnPageIndexChanging="grid2_PageIndexChanging" >
                        <EnhancePagerSettings FirstImageUrl="" FirstAltImageUrl="" PreviousImageUrl="" NextImageUrl="" LastImageUrl="" LastAltImage="" PageNumberCssClass="" PageNumberCurrentCssClass="" PageInfoCssClass="" PageRedirectCssClass="" NextIAltImageUrl="" PreviousAltImageUrl="" ShowHeaderPager="True"></EnhancePagerSettings>
                 
                            <ExportExcelSettings AllowExportToExcel="true" ExportType="DataSource" ></ExportExcelSettings>
                            <Columns>
                                 <asp:BoundField HeaderText="業務" DataField="USER_NAME" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" >
                                    <ItemStyle HorizontalAlign="Center" Width="140px"></ItemStyle>
                                </asp:BoundField>   
                                 <asp:BoundField HeaderText="客戶" DataField="COMPANY_NAME" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" >
                                    <ItemStyle HorizontalAlign="Center" Width="200px"></ItemStyle>
                                </asp:BoundField>   
                                <asp:TemplateField HeaderText="記錄" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" >
                                    <ItemTemplate>
                                        <asp:Label ID="CONTENT" runat="server" Text='<%# Bind("NOTE_CONTENT") %>' style="text-align:left" HorizontalAlign="Left" Width="600px" ItemStyle-HorizontalAlign="Left"></asp:Label>
                                        <ItemStyle HorizontalAlign="Left" Width="600px"></ItemStyle>
                                    </ItemTemplate>                                        
                                 </asp:TemplateField>
                                 <asp:BoundField HeaderText="記錄日期" DataField="CREATE_DATETIME" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" >
                                    <ItemStyle HorizontalAlign="Center" Width="200px"></ItemStyle>
                                </asp:BoundField> 
                               
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <%--<asp:Image ID="Image2" runat="server"  HorizontalAlign="Center"  Length="100px" Width="100px" onclick="ImageClick(this)"/>--%>
                                        <asp:Image ID="Image2" runat="server"  HorizontalAlign="Center"  Length="100px" Width="100px"/>                                  
                                    </ItemTemplate >
                                </asp:TemplateField>
                                
                            </Columns>
                        </Fast:Grid>
                    </div>
            </td>
        </tr>
    </table>

     <label>專案記錄-主管決議:否</label>
      <table class="PopTable">
          <tr>
            <td colspan="2" class="PopTableRightTD" >
                <div style="overflow-x:auto;width:100%">
                    <Fast:Grid ID="Grid4" OnRowDataBound="Grid4_RowDataBound" runat="server"  OnBeforeExport="OnBeforeExport" AllowPaging="true"  AutoGenerateCheckBoxColumn="False" AllowSorting="True" AutoGenerateColumns="False" CustomDropDownListPage="False" DataKeyOnClientWithCheckBox="False" DefaultSortDirection="Ascending" EmptyDataText="No data found" EnhancePager="True" KeepSelectedRows="False" PageSize="100" SelectedRowColor="" UnSelectedRowColor="" meta:resourcekey="Grid1Resource4" OnPageIndexChanging="grid4_PageIndexChanging" >
                        <EnhancePagerSettings FirstImageUrl="" FirstAltImageUrl="" PreviousImageUrl="" NextImageUrl="" LastImageUrl="" LastAltImage="" PageNumberCssClass="" PageNumberCurrentCssClass="" PageInfoCssClass="" PageRedirectCssClass="" NextIAltImageUrl="" PreviousAltImageUrl="" ShowHeaderPager="True"></EnhancePagerSettings>
                 
                            <ExportExcelSettings AllowExportToExcel="true" ExportType="DataSource" ></ExportExcelSettings>
                            <Columns>
                                <asp:BoundField HeaderText="業務" DataField="USER_NAME" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" >
                                    <ItemStyle HorizontalAlign="Center" Width="140px"></ItemStyle>
                                </asp:BoundField>   
                                 <asp:BoundField HeaderText="客戶名稱" DataField="COMPANY_NAME" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" >
                                    <ItemStyle HorizontalAlign="Center" Width="200px"></ItemStyle>
                                </asp:BoundField>  
                                 <asp:BoundField HeaderText="專案名稱" DataField="OPPORTUNITY_NAME" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" >
                                    <ItemStyle HorizontalAlign="Center" Width="200px"></ItemStyle>
                                </asp:BoundField>  
                                 <asp:BoundField HeaderText="商品" DataField="PRODUCT" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" >
                                    <ItemStyle HorizontalAlign="Left" Width="200px"></ItemStyle>
                                </asp:BoundField>   
                                <asp:BoundField HeaderText="專案金額" DataField="AMOUNT" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" >
                                    <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                                </asp:BoundField> 
                                 <asp:TemplateField HeaderText="記錄" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" >
                                    <ItemTemplate>
                                        <asp:Label ID="CONTENT" runat="server" Text='<%# Bind("NOTE_CONTENT") %>' style="text-align:left" HorizontalAlign="Left" Width="600px" ItemStyle-HorizontalAlign="Left"></asp:Label>
                                        <ItemStyle HorizontalAlign="Left" Width="600px"></ItemStyle>
                                    </ItemTemplate>                                        
                                 </asp:TemplateField>

                               <%--  <asp:BoundField HeaderText="記錄" DataField="NOTE_CONTENT" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" >
                                    <ItemStyle HorizontalAlign="Left" Width="600px"></ItemStyle>
                                </asp:BoundField>   --%>
                                 <asp:BoundField HeaderText="記錄日期" DataField="CREATE_DATETIME" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center" >
                                    <ItemStyle HorizontalAlign="Center" Width="200px"></ItemStyle>
                                </asp:BoundField>   
                            </Columns>
                        </Fast:Grid>
                    </div>
            </td>
        </tr>
    </table>

    <table>
        <div id="Show_img_Max" onclick="divClick()">
            <div>                
                <img src="" class="Show_imgStyle" />
            </div>
        </div>

    </table>     
  
</asp:Content>

