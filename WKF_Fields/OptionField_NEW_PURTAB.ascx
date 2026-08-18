<%@ Control Language="C#" AutoEventWireup="true" CodeFile="OptionField_NEW_PURTAB.ascx.cs" Inherits="WKF_OptionalFields_OptionField_NEW_PURTAB" %>
<%@ Reference Control="~/WKF/FormManagement/VersionFieldUserControl/VersionFieldUC.ascx" %>
<%@ Register Assembly="Ede.Uof.Utility.Component.Grid" Namespace="Ede.Uof.Utility.Component" TagPrefix="Ede" %>


<div style="width: 100%; height: 100%; border: 3px #cccccc dashed;">
    <table width="100%" class="" cellspacing="1">
        <tr class="">
            <td class="">
                <asp:Label ID="Label1" runat="server" Text="請填寫請購單號: "></asp:Label>
                <asp:TextBox ID="TextBox1" runat="server" Text="" AutoPostBack="true"></asp:TextBox>
            </td>

        </tr>
        <tr>
            <td>
                <asp:Button ID="Button1" runat="server" Style="width: 200px; height: 50px; background-color: palevioletred;" Text="查看" OnClick="Button1_Click" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label2" runat="server" Text="品名"></asp:Label>
            </td>
        </tr>
    </table>
</div>

<style type="text/css">
    .tb-box {
        width: 100%;
        border: 1px solid #ccc;
        padding: 10px;
        margin-bottom: 10px;
        border-radius: 4px;
    }

    .tb-header {
        background-color: #f5f5f5;
        font-weight: bold;
        padding: 5px;
        margin-bottom: 8px;
    }

    .tb-table {
        width: 100%;
        border-collapse: collapse;
    }

        .tb-table td {
            padding: 4px 8px;
        }
</style>

<!-- 明細編輯區 -->
<div class="tb-box">
    <div class="tb-header">明細編輯</div>
    <table class="tb-table">
        <tr>
            <td>
                <asp:Label ID="lblTB005Title" runat="server" Text="TB005 資料:"></asp:Label><br />
                <asp:TextBox ID="txtTB005Input" runat="server" Width="300px"></asp:TextBox>
                <asp:Button ID="btnAddDetail" runat="server" Text="新增至明細" OnClick="btnAddDetail_Click" />
                <asp:Button ID="btnDeleteDetail" runat="server" Text="刪除勾選列" OnClick="btnDeleteDetail_Click" />
            </td>
        </tr>
    </table>
</div>

<!-- Grid 列表區 -->
<div class="tb-box">
    <Ede:Grid ID="Grid1" runat="server" DataKeyNames="ID" AutoGenerateColumns="false" Width="100%">
        <Columns>
            <asp:BoundField HeaderText="ID" DataField="ID" Visible="false" />
            <asp:BoundField HeaderText="TA001" DataField="TA001" HeaderStyle-Width="10%" />
            <asp:BoundField HeaderText="TA002" DataField="TA002" HeaderStyle-Width="10%" />
            <asp:BoundField HeaderText="TB003" DataField="TB003" HeaderStyle-Width="10%" />
            <asp:BoundField HeaderText="TB004" DataField="TB004" HeaderStyle-Width="30%" />
            <asp:BoundField HeaderText="TB005" DataField="TB005" HeaderStyle-Width="40%" />
        </Columns>
    </Ede:Grid>
</div>

<!-- 訊息與隱藏狀態欄位 -->
<div id="MESSAGES">
    <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
</div>

<div style="display: none;">
    <!-- 存放 XML 結構的主要欄位 -->
    <asp:TextBox ID="txtFieldValue" runat="server" TextMode="MultiLine"></asp:TextBox>
</div>


<asp:Label ID="lblHasNoAuthority" runat="server" Text="無填寫權限" ForeColor="Red" Visible="False" meta:resourcekey="lblHasNoAuthorityResource1"></asp:Label>
<asp:Label ID="lblToolTipMsg" runat="server" Text="不允許修改(唯讀)" Visible="False" meta:resourcekey="lblToolTipMsgResource1"></asp:Label>
<asp:Label ID="lblModifier" runat="server" Visible="False" meta:resourcekey="lblModifierResource1"></asp:Label>
<asp:Label ID="lblMsgSigner" runat="server" Text="填寫者" Visible="False" meta:resourcekey="lblMsgSignerResource1"></asp:Label>
<asp:Label ID="lblAuthorityMsg" runat="server" Text="具填寫權限人員" Visible="False" meta:resourcekey="lblAuthorityMsgResource1"></asp:Label>
