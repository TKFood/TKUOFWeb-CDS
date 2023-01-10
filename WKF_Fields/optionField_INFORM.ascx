<%@ Control Language="C#" AutoEventWireup="true" CodeFile="optionField_INFORM.ascx.cs" Inherits="WKF_OptionalFields_optionField_INFORM" %>
<%@ Reference Control="~/WKF/FormManagement/VersionFieldUserControl/VersionFieldUC.ascx" %>



<asp:Label ID="lblHasNoAuthority" runat="server" Text="無填寫權限" ForeColor="Red" Visible="False" meta:resourcekey="lblHasNoAuthorityResource1"></asp:Label>
<asp:Label ID="lblToolTipMsg" runat="server" Text="不允許修改(唯讀)" Visible="False" meta:resourcekey="lblToolTipMsgResource1"></asp:Label>
<asp:Label ID="lblModifier" runat="server" Visible="False" meta:resourcekey="lblModifierResource1"></asp:Label>
<asp:Label ID="lblMsgSigner" runat="server" Text="填寫者" Visible="False" meta:resourcekey="lblMsgSignerResource1"></asp:Label>
<asp:Label ID="lblAuthorityMsg" runat="server" Text="具填寫權限人員" Visible="False" meta:resourcekey="lblAuthorityMsgResource1"></asp:Label>

<div style="width: 100%; height: 100%; border: 3px #cccccc dashed;">
    <table width="100%" class="" cellspacing="1">
        <tr>
            <td>
                <asp:Button ID="Button1" runat="server" Text="通知申請人" OnClick="Button1_Click" />
            </td>
        </tr>
    </table>
</div>
