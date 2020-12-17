<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MSTEST.ascx.cs" Inherits="WKF_OptionalFields_MSTEST" %>
<%@ Register Assembly="Ede.Uof.Utility.Component.Grid" Namespace="Ede.Uof.Utility.Component" TagPrefix="Ede" %>
<%@ Reference Control="~/WKF/FormManagement/VersionFieldUserControl/VersionFieldUC.ascx" %>

<asp:Button ID="btnInert" runat="server" Text="新增" OnClick="btnInert_Click" />
<asp:Button ID="btnDelete" runat="server" Text="刪除" OnClick="btnDelete_Click" />


<asp:TextBox ID="txtTel" runat="server"></asp:TextBox>


<asp:TextBox ID="txtFieldValue" runat="server"   
    TextMode="MultiLine" Width="300px" Height="300px"
     ></asp:TextBox>


<Ede:Grid ID="Grid1" runat="server" DataKeyNames="ID" AutoGenerateColumns="false">
    <Columns>
        <asp:BoundField HeaderText="TXT1" DataField="TXT1" />
        <asp:BoundField HeaderText="TXT2" DataField="TXT2" />
        <asp:BoundField HeaderText="DDL" DataField="DDL" />
        <asp:TemplateField HeaderText="操作">
            <ItemTemplate>
                <asp:LinkButton ID="lbtnModify" runat="server">修改</asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>

</Ede:Grid>


<asp:Label ID="lblHasNoAuthority" runat="server" Text="無填寫權限" ForeColor="Red" Visible="False" meta:resourcekey="lblHasNoAuthorityResource1"></asp:Label>
<asp:Label ID="lblToolTipMsg" runat="server" Text="不允許修改(唯讀)" Visible="False" meta:resourcekey="lblToolTipMsgResource1"></asp:Label>
<asp:Label ID="lblModifier" runat="server" Visible="False" meta:resourcekey="lblModifierResource1"></asp:Label>
<asp:Label ID="lblMsgSigner" runat="server" Text="填寫者" Visible="False" meta:resourcekey="lblMsgSignerResource1"></asp:Label>
<asp:Label ID="lblAuthorityMsg" runat="server" Text="具填寫權限人員" Visible="False" meta:resourcekey="lblAuthorityMsgResource1"></asp:Label>