<%@ Page Title="" Language="C#" MasterPageFile="~/Master/DefaultMasterPage.master" AutoEventWireup="true" CodeFile="TK_AD_MANAGE.aspx.cs" Inherits="CDS_WebPage_MARKETING_TK_AD_MANAGE" %>

<%@ Register Assembly="Ede.Uof.Utility.Component.Grid" Namespace="Ede.Uof.Utility.Component" TagPrefix="Fast" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script>    


</script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table class="PopTable">
                <telerik:RadTabStrip ID="RadTabStrip1" runat="server"></telerik:RadTabStrip>
                <telerik:RadTabStrip ID="RadTabStrip2" runat="server" MultiPageID="RadMultiPage" SelectedIndex="0">
                    <Tabs>
                        <telerik:RadTab Text="資料">
                        </telerik:RadTab>
                        <telerik:RadTab Text="新增">
                        </telerik:RadTab>
                        <telerik:RadTab Text="其他">
                        </telerik:RadTab>
                    </Tabs>
                </telerik:RadTabStrip>

                <telerik:RadMultiPage ID="RadMultiPage" runat="server" SelectedIndex="0">
                    <telerik:RadPageView ID="RadPageView1" runat="server" Selected="true">
                        <div id="tabs-1">
                            <table class="PopTable">
                            </table>
                        </div>
                    </telerik:RadPageView>
                </telerik:RadMultiPage>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

