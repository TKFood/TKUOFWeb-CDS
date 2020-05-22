<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TKUOFtb_COMPANYCOMPANY_ID.ascx.cs" Inherits="WKF_OptionalFields_TKUOFtb_COMPANYCOMPANY_ID" %>
<%@ Reference Control="~/WKF/FormManagement/VersionFieldUserControl/VersionFieldUC.ascx" %>

<table style="border-collapse:separate; border-spacing:0 10px; margin-top:-15px;">
    <tr>
        <td>
            <asp:Label ID="Label1" runat="server" Text="請輸入客戶代號: "></asp:Label>
        </td>      
        <td>
            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
            <asp:Button ID="Button1" runat="server" Text="查詢" OnClientClick="return btn_Click(this)"  onclick="btn_Click"/>
        </td>
        <td>
            
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
    <tr>
        <td>
            <asp:Label ID="Label3" runat="server" Text="資本額: "></asp:Label>
        </td>      
        <td>
            <asp:TextBox ID="TextBox2" runat="server" Text="0"></asp:TextBox>
        </td>    
    </tr>
    <tr>
        <td>
            交易幣別
        </td>
        <td>
            <asp:DropDownList ID="DropDownList1" runat="server" AppendDataBoundItems="false"></asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td>
            部門別
        </td>
        <td>
            <asp:DropDownList ID="DropDownList2" runat="server" AppendDataBoundItems="false"></asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td>
            通路別
        </td>
        <td>
            <asp:DropDownList ID="DropDownList3" runat="server" AppendDataBoundItems="false"></asp:DropDownList>
        </td>
    </tr>
        <tr>
        <td>
            地區別
        </td>
        <td>
            <asp:DropDownList ID="DropDownList4" runat="server" AppendDataBoundItems="false"></asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td>
            國家別
        </td>
        <td>
            <asp:DropDownList ID="DropDownList5" runat="server" AppendDataBoundItems="false"></asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="Label4" runat="server" Text="銷售評等: "></asp:Label>
        </td>      
        <td>
            <asp:TextBox ID="TextBox3" runat="server" Text="A"></asp:TextBox>
        </td>    
    </tr>
    <tr>
        <td>
            付款條件
        </td>
        <td>
            <asp:DropDownList ID="DropDownList6" runat="server" AppendDataBoundItems="false"></asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="Label5" runat="server" Text="信用額度: "></asp:Label>
        </td>      
        <td>
            <asp:TextBox ID="TextBox4" runat="server" Text="0"></asp:TextBox>
        </td>    
    </tr>
    <tr>
        <td>
            發票聯數
        </td>
        <td>
            <asp:DropDownList ID="DropDownList7" runat="server" AppendDataBoundItems="false"></asp:DropDownList>
        </td>
    </tr>
</table>

<asp:Label ID="lblHasNoAuthority" runat="server" Text="無填寫權限" ForeColor="Red" Visible="False" meta:resourcekey="lblHasNoAuthorityResource1"></asp:Label>
<asp:Label ID="lblToolTipMsg" runat="server" Text="不允許修改(唯讀)" Visible="False" meta:resourcekey="lblToolTipMsgResource1"></asp:Label>
<asp:Label ID="lblModifier" runat="server" Visible="False" meta:resourcekey="lblModifierResource1"></asp:Label>
<asp:Label ID="lblMsgSigner" runat="server" Text="填寫者" Visible="False" meta:resourcekey="lblMsgSignerResource1"></asp:Label>
<asp:Label ID="lblAuthorityMsg" runat="server" Text="具填寫權限人員" Visible="False" meta:resourcekey="lblAuthorityMsgResource1"></asp:Label>