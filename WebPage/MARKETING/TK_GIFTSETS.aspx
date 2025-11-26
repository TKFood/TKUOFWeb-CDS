<%@ Page Title="" Language="C#" MasterPageFile="~/Master/DefaultMasterPage.master" AutoEventWireup="true" CodeFile="TK_GIFTSETS.aspx.cs" Inherits="CDS_WebPage_MARKETING_TK_GIFTSETS" %>

<%@ Register Assembly="Ede.Uof.Utility.Component.Grid" Namespace="Ede.Uof.Utility.Component" TagPrefix="Fast" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <style>
        .multiline-textbox {
            word-break: break-all;
            white-space: pre-line;
            height: 60px; /* 根据需要设置高度 */
        }

        .custom-button {
            width: 150px; /* 设置按钮宽度 */
            height: 40px; /* 设置按钮高度 */
            font-size: 20px; /* 设置按钮字体大小 */
            color: #ffffff; /* 设置按钮文本颜色 */
            background-color: #007bff; /* 设置按钮背景颜色 */
            border: none; /* 去除按钮边框 */
            border-radius: 5px; /* 设置按钮圆角 */
        }

            .custom-button:hover {
                background-color: #0056b3; /* 设置按钮鼠标悬停时的背景颜色 */
            }

        .file-preview {
            max-width: 400px; /* 限制圖片的最大寬度 */
            max-height: 400px; /* 限制圖片的最大高度 */
            height: auto; /* 保持圖片的寬高比 */
            width: auto; /* 保持圖片的寬高比 */
            border: 1px solid #ddd;
            border-radius: 4px;
            padding: 2px;
        }

        /* 確保圖片連結不會導致佈局錯亂 */
        .file-preview-container {
            display: inline-block;
        }
    </style>
    <script>    


</script>

     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <telerik:RadTabStrip ID="RadTabStrip1" runat="server"></telerik:RadTabStrip>
            <telerik:RadTabStrip ID="RadTabStrip2" runat="server" MultiPageID="RadMultiPage" SelectedIndex="0">
                <Tabs>
                    <telerik:RadTab Text="資料">
                    </telerik:RadTab>                 
                    <telerik:RadTab Text="其他">
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
                                    <asp:Button ID="Button1" runat="server" Text="查詢" OnClick="Button1_Click"
                                        meta:resourcekey="btn1Resource1" />
                                </td>
                            </tr>
                        </table>
                        <table class="PopTable">
                            <tr>
                                <td colspan="2" class="PopTableRightTD">
                                    <div style="overflow-x: auto; width: 100%">
                                        <Fast:Grid ID="Grid1" OnRowDataBound="Grid1_RowDataBound" OnRowCommand="Grid1_RowCommand" runat="server" OnBeforeExport="OnBeforeExport1" AllowPaging="true" AutoGenerateCheckBoxColumn="False" AllowSorting="True" AutoGenerateColumns="False" CustomDropDownListPage="False" DataKeyOnClientWithCheckBox="False" DefaultSortDirection="Ascending" EmptyDataText="No data found" EnhancePager="True" KeepSelectedRows="False" PageSize="1000" SelectedRowColor="" UnSelectedRowColor="" meta:resourcekey="Grid1Resource1" OnPageIndexChanging="grid_PageIndexChanging1">
                                            <EnhancePagerSettings FirstImageUrl="" FirstAltImageUrl="" PreviousImageUrl="" NextImageUrl="" LastImageUrl="" LastAltImage="" PageNumberCssClass="" PageNumberCurrentCssClass="" PageInfoCssClass="" PageRedirectCssClass="" NextIAltImageUrl="" PreviousAltImageUrl="" ShowHeaderPager="True"></EnhancePagerSettings>
                                            <ExportExcelSettings AllowExportToExcel="true" ExportType="GridContent"></ExportExcelSettings>
                                            <Columns>
                                                <asp:TemplateField HeaderText="品名" ItemStyle-Width="200px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label_品名" runat="server" Text='<%# Bind("MB002") %>' Style="word-break: break-all; white-space: pre-line;" Width="100px"></asp:Label>
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

                <telerik:RadPageView ID="RadPageView99" runat="server">
                    <div id="tabs-99">
                        <asp:Label ID="Label2" runat="server" Text="Labe2"></asp:Label>
                    </div>
                </telerik:RadPageView>
            </telerik:RadMultiPage>​
        </ContentTemplate>

    </asp:UpdatePanel>
</asp:Content>

