<%@ Control Language="C#" AutoEventWireup="true" CodeFile="OptionField_PURTAB.ascx.cs" Inherits="WKF_OptionalFields_OptionField_PURTAB" %>
<%@ Reference Control="~/WKF/FormManagement/VersionFieldUserControl/VersionFieldUC.ascx" %>
<%@ Register Assembly="Ede.Uof.Utility.Component.Grid" Namespace="Ede.Uof.Utility.Component" TagPrefix="Ede" %>

<table width="100%" class="" cellspacing="1">
    
    <tr class="">
            <td class="">
                 <asp:Label ID="Label1" runat="server" Text="請購單" ></asp:Label>
            </td>       
        </tr>
    <tr  class="">
            <td  class="">
                <asp:Label ID="Label2" runat="server" Text="請購單單別:"></asp:Label>
                <asp:TextBox ID="TextBox1" runat="server" Text="請購單單別，不用填" ReadOnly="true"></asp:TextBox>
            </td>   
    </tr>
    <tr  class="">
         <td  class="">
              <asp:Label ID="Label3" runat="server" Text="請購單單號:"></asp:Label>
             <asp:TextBox ID="TextBox2" runat="server" Text="請購單單號，不用填" ReadOnly="true"></asp:TextBox>
         </td>        
        </tr>
    <tr  class="">
         <td  class="">
              <asp:Label ID="Label4" runat="server" Text="請購日期:"></asp:Label>
             <asp:TextBox ID="TextBox3" runat="server" Text="請購日期，不用填" ReadOnly="true"></asp:TextBox>
         </td>        
        </tr>
    <tr  class="">
         <td  class="">
              <asp:Label ID="Label5" runat="server" Text="請購人員:" ></asp:Label>
             <asp:TextBox ID="TextBox4" runat="server" Text="" ReadOnly="false" AutoPostBack="true" OnTextChanged="TextBox4_TextChanged"></asp:TextBox>
         </td>        
        </tr>
    <tr  class="">
         <td  class="">
              <asp:Label ID="Label6" runat="server" Text="請購部門:"></asp:Label>
             <asp:TextBox ID="TextBox5" runat="server" Text="" ReadOnly="false"></asp:TextBox>
         </td>        
        </tr>
     <tr  class="">
         <td  class="">
              <asp:Label ID="Label7" runat="server" Text="備註:"></asp:Label>
             <asp:TextBox ID="TextBox6" runat="server" Text="" ReadOnly="false"></asp:TextBox>
         </td>        
        </tr>
    </table>

<table>
      <tr  class="">
         <td  class="">
              <asp:Label ID="Label8" runat="server" Text="明細"></asp:Label>
                 <asp:Button ID="btnInert" runat="server" Text="新增" OnClick="btnInert_Click" />
                <asp:Button ID="btnDelete" runat="server" Text="刪除" OnClick="btnDelete_Click" />
         </td>        
        </tr>
    <tr>
        <td>
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
        </td>
    </tr>
</table>

<table> 
    <tr>
        <td>
            <asp:TextBox ID="txtFieldValue" runat="server"   
            TextMode="MultiLine" Width="300px" Height="300px"
             ></asp:TextBox>
        </td>
    </tr>
</table>




<asp:Label ID="lblHasNoAuthority" runat="server" Text="無填寫權限" ForeColor="Red" Visible="False" meta:resourcekey="lblHasNoAuthorityResource1"></asp:Label>
<asp:Label ID="lblToolTipMsg" runat="server" Text="不允許修改(唯讀)" Visible="False" meta:resourcekey="lblToolTipMsgResource1"></asp:Label>
<asp:Label ID="lblModifier" runat="server" Visible="False" meta:resourcekey="lblModifierResource1"></asp:Label>
<asp:Label ID="lblMsgSigner" runat="server" Text="填寫者" Visible="False" meta:resourcekey="lblMsgSignerResource1"></asp:Label>
<asp:Label ID="lblAuthorityMsg" runat="server" Text="具填寫權限人員" Visible="False" meta:resourcekey="lblAuthorityMsgResource1"></asp:Label>