<%@ Page Title="" Language="C#" MasterPageFile="~/Master/DefaultMasterPage.master" AutoEventWireup="true" CodeFile="TK_TEMP_HUMI_LOG.aspx.cs" Inherits="CDS_WebPage_QC_TK_TEMP_HUMI_LOG" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<!-- 只有在 UpdatePanel 內部的內容會被定期更新，整個頁面不會閃爍 -->
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <!-- 定時器：Interval 單位為毫秒 (5000 = 5秒) -->
        <asp:Timer ID="Timer1" runat="server" Interval="5000" OnTick="Timer1_Tick" />
        
        <asp:Label ID="lblStatus" runat="server" Text="等待更新中..." />
        <asp:GridView ID="GridView1" runat="server" />
    </ContentTemplate>
</asp:UpdatePanel>


</asp:Content>

