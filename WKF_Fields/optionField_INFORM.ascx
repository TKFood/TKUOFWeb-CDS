<%@ Control Language="C#" AutoEventWireup="true" CodeFile="optionField_INFORM.ascx.cs" Inherits="WKF_OptionalFields_optionField_INFORM" %>
<%@ Reference Control="~/WKF/FormManagement/VersionFieldUserControl/VersionFieldUC.ascx" %>



<asp:Label ID="lblHasNoAuthority" runat="server" Text="無填寫權限" ForeColor="Red" Visible="False" meta:resourcekey="lblHasNoAuthorityResource1"></asp:Label>
<asp:Label ID="lblToolTipMsg" runat="server" Text="不允許修改(唯讀)" Visible="False" meta:resourcekey="lblToolTipMsgResource1"></asp:Label>
<asp:Label ID="lblModifier" runat="server" Visible="False" meta:resourcekey="lblModifierResource1"></asp:Label>
<asp:Label ID="lblMsgSigner" runat="server" Text="填寫者" Visible="False" meta:resourcekey="lblMsgSignerResource1"></asp:Label>
<asp:Label ID="lblAuthorityMsg" runat="server" Text="具填寫權限人員" Visible="False" meta:resourcekey="lblAuthorityMsgResource1"></asp:Label>

<div style="width: 100%; height: 100%; border: 3px #cccccc dashed;">
    <table  cellpadding="8"  width="100%" class="">
        <tr>
            <td style="padding:8px;">
                <asp:Button ID="Button1" runat="server"   style="width:200px;height:50px;background-color: palevioletred;"  Text="呼叫表單申請人 " OnClick="Button1_Click" OnClientClick="this.disabled = true; this.value = '已通知';" UseSubmitBehavior="false" />
            </td>
        </tr>  
          <tr>
            <td style="padding:8px;">
                 <asp:Button ID="Button2" runat="server"  style="width:200px;height:50px;background-color: palevioletred;" Text="呼叫 戴硯 執副 " OnClick="Button1_Click" OnClientClick="this.disabled = true; this.value = '已通知';" UseSubmitBehavior="false" />
            </td>
        </tr>
          <tr>
            <td style="padding:8px;">
                 <asp:Button ID="Button3" runat="server"  style="width:200px;height:50px;background-color: palevioletred;" Text="呼叫 林忠輝 副總 " OnClick="Button1_Click" OnClientClick="this.disabled = true; this.value = '已通知';" UseSubmitBehavior="false" />
            </td>
        </tr>
          <tr>
            <td style="padding:8px;">
                 <asp:Button ID="Button4" runat="server"  style="width:200px;height:50px;background-color: palevioletred;" Text="呼叫 張釋予 協理  " OnClick="Button1_Click" OnClientClick="this.disabled = true; this.value = '已通知';" UseSubmitBehavior="false" />
            </td>
        </tr>
          <tr>
            <td style="padding:8px;">
                 <asp:Button ID="Button5" runat="server"  style="width:200px;height:50px;background-color: palevioletred;" Text="呼叫 行企 盧香穎 經理 " OnClick="Button1_Click" OnClientClick="this.disabled = true; this.value = '已通知';" UseSubmitBehavior="false" />
            </td>
        </tr>
          <tr>
            <td style="padding:8px;">
                 <asp:Button ID="Button6" runat="server"  style="width:200px;height:50px;background-color: palevioletred;" Text="呼叫 採購 徐雅芳 經理 " OnClick="Button1_Click" OnClientClick="this.disabled = true; this.value = '已通知';" UseSubmitBehavior="false" />
            </td>
        </tr>
                </tr>
          <tr>
            <td style="padding:8px;">
                 <asp:Button ID="Button7" runat="server"  style="width:200px;height:50px;background-color: palevioletred;" Text="呼叫 財務 許文鴻 經理" OnClick="Button1_Click" OnClientClick="this.disabled = true; this.value = '已通知';" UseSubmitBehavior="false" />
            </td>
        </tr>
                </tr>
          <tr>
            <td style="padding:8px;">
                 <asp:Button ID="Button8" runat="server"  style="width:200px;height:50px;background-color: palevioletred;" Text="呼叫 業務 黃鈺涵 副理 " OnClick="Button1_Click" OnClientClick="this.disabled = true; this.value = '已通知';" UseSubmitBehavior="false" />
            </td>
        </tr>
                </tr>
          <tr>
            <td style="padding:8px;">
                 <asp:Button ID="Button9" runat="server"  style="width:200px;height:50px;background-color: palevioletred;" Text="呼叫 國外 洪櫻芬 經理 " OnClick="Button1_Click" OnClientClick="this.disabled = true; this.value = '已通知';" UseSubmitBehavior="false" />
            </td>
        </tr>
                </tr>
          <tr>
            <td style="padding:8px;">
                 <asp:Button ID="Button10" runat="server"  style="width:200px;height:50px;background-color: palevioletred;" Text="呼叫 何順誠 經理 " OnClick="Button1_Click" OnClientClick="this.disabled = true; this.value = '已通知';" UseSubmitBehavior="false" />
            </td>
        </tr>
                </tr>
          <tr>
            <td style="padding:8px;">
                 <asp:Button ID="Button11" runat="server"  style="width:200px;height:50px;background-color: palevioletred;" Text="呼叫 資訊 葉志剛 經理 " OnClick="Button1_Click" OnClientClick="this.disabled = true; this.value = '已通知';" UseSubmitBehavior="false" />
            </td>
        </tr>
                </tr>
          <tr>
            <td style="padding:8px;">
                 <asp:Button ID="Button12" runat="server"  style="width:200px;height:50px;background-color: palevioletred;" Text="呼叫 資訊 張健洲 經理 " OnClick="Button1_Click" OnClientClick="this.disabled = true; this.value = '已通知';" UseSubmitBehavior="false" />
            </td>
        </tr>

    </table>
</div>
