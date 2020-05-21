<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TKUOFtb_COMPANYCOMPANY_ID.ascx.cs" Inherits="WKF_OptionalFields_TKUOFtb_COMPANYCOMPANY_ID" %>
<%@ Reference Control="~/WKF/FormManagement/VersionFieldUserControl/VersionFieldUC.ascx" %>

<table>
    <tr>
        <td>
            <asp:Label ID="Label1" runat="server" Text="請輸入客戶代號:"></asp:Label>
        </td>      
        <td>
            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        </td>
        <td>
            <asp:Button ID="Button1" runat="server" Text="查詢" OnClientClick="return btn_Click(this)"  onclick="btn_Click"/>
        </td>
    </tr>
   <tr>
        <td>
            <asp:Label ID="Label2" runat="server" Text=" 客戶名稱: "></asp:Label>
        </td>
        <td>
            <asp:Label ID="LabelNAME" runat="server" Text=""></asp:Label>
        </td>
   </tr>
</table>

<asp:Label ID="lblHasNoAuthority" runat="server" Text="無填寫權限" ForeColor="Red" Visible="False" meta:resourcekey="lblHasNoAuthorityResource1"></asp:Label>
<asp:Label ID="lblToolTipMsg" runat="server" Text="不允許修改(唯讀)" Visible="False" meta:resourcekey="lblToolTipMsgResource1"></asp:Label>
<asp:Label ID="lblModifier" runat="server" Visible="False" meta:resourcekey="lblModifierResource1"></asp:Label>
<asp:Label ID="lblMsgSigner" runat="server" Text="填寫者" Visible="False" meta:resourcekey="lblMsgSignerResource1"></asp:Label>
<asp:Label ID="lblAuthorityMsg" runat="server" Text="具填寫權限人員" Visible="False" meta:resourcekey="lblAuthorityMsgResource1"></asp:Label>