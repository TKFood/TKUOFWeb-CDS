<%@ Control Language="C#" AutoEventWireup="true" CodeFile="OptionField_PURTAB.ascx.cs" Inherits="WKF_OptionalFields_OptionField_PURTAB" %>
<%@ Reference Control="~/WKF/FormManagement/VersionFieldUserControl/VersionFieldUC.ascx" %>
<%@ Register Assembly="Ede.Uof.Utility.Component.Grid" Namespace="Ede.Uof.Utility.Component" TagPrefix="Ede" %>

<div  style="width:100%;height:100%;border:3px #cccccc dashed;">
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
</div>


<div  style="width:100%;height:100%;border:3px #cccccc dashed;">
    <table>
        <tr>
            <td>
                 <asp:Label ID="Label13" runat="server" Text="品號:<br/>"></asp:Label>
                <asp:TextBox ID="TextBox7" runat="server"></asp:TextBox>
                 <asp:Button ID="btnInert" runat="server" Text="查詢" OnClick="btnInert_Click" />
            </td>
             <td>
                 <asp:Label ID="Label14" runat="server" Text="品名:<br/>"></asp:Label>
                 <asp:TextBox ID="TextBox8" runat="server"></asp:TextBox>
            </td>
             <td>
                 <asp:Label ID="Label15" runat="server" Text="數量:<br/>"></asp:Label>
                 <asp:TextBox ID="TextBox9" runat="server"></asp:TextBox>
            </td>
             <td>
                 <asp:Label ID="Label16" runat="server" Text="需求日(年4碼+月2碼+日2碼):<br/>"></asp:Label>
                 <asp:TextBox ID="TextBox10" runat="server"></asp:TextBox>
            </td>
        </tr>          
      </table>
</div>

<div>
    <table>
        <tr  class="">
             <td  class="">
                  <asp:Label ID="Label17" runat="server" Text="明細"></asp:Label>
                    <asp:Button ID="Button3" runat="server" Text="新增" OnClick="btnADD_Click" /> 
                    <asp:Button ID="Button4" runat="server" Text="刪除" OnClick="btnDelete_Click" />               
             </td>        
            </tr>
    </table>
</div>

<div  style="width:100%;height:100%;border:3px #cccccc dashed;">
    <table>
        <tr>
            <td>
            
                     <Ede:Grid ID="Grid1" runat="server" DataKeyNames="ID" AutoGenerateColumns="false">
                        <Columns>
                            <asp:BoundField HeaderText="品號" DataField="品號" HeaderStyle-Width="100px" />
                            <asp:BoundField HeaderText="品名" DataField="品名" HeaderStyle-Width="400px"/>
                            <asp:BoundField HeaderText="數量" DataField="數量" HeaderStyle-Width="100px" />
                            <asp:BoundField HeaderText="需求日" DataField="需求日" HeaderStyle-Width="100px"/>                   
                   
                        </Columns>
                    </Ede:Grid>
            </td>
        </tr>
    </table>
</div>


<div id="MESSAGES">
    <asp:Label ID="LabelMESSAGES" runat="server" Text="" ForeColor="red"></asp:Label>
</div>

<div>
    <table> 
        <tr>
            <td>
                <asp:TextBox ID="txtFieldValue"  runat="server" TextMode="MultiLine" Width="1px" Height="1px" ></asp:TextBox>
            </td>
             <td>
                <asp:TextBox ID="txtFieldValue2" runat="server"  TextMode="MultiLine" Width="1px" Height="1px" ></asp:TextBox>
            </td>
        </tr>
    </table>
</div>





<asp:Label ID="lblHasNoAuthority" runat="server" Text="無填寫權限" ForeColor="Red" Visible="False" meta:resourcekey="lblHasNoAuthorityResource1"></asp:Label>
<asp:Label ID="lblToolTipMsg" runat="server" Text="不允許修改(唯讀)" Visible="False" meta:resourcekey="lblToolTipMsgResource1"></asp:Label>
<asp:Label ID="lblModifier" runat="server" Visible="False" meta:resourcekey="lblModifierResource1"></asp:Label>
<asp:Label ID="lblMsgSigner" runat="server" Text="填寫者" Visible="False" meta:resourcekey="lblMsgSignerResource1"></asp:Label>
<asp:Label ID="lblAuthorityMsg" runat="server" Text="具填寫權限人員" Visible="False" meta:resourcekey="lblAuthorityMsgResource1"></asp:Label>