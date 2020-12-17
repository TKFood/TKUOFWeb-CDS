<%@ Page Title="" Language="C#" MasterPageFile="~/Master/DialogMasterPage.master" AutoEventWireup="true" CodeFile="MaintainItem.aspx.cs" Inherits="CDS_WKF_Fields_MaintainItem" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    
<script>
    
    function ValidateData(source, arguments) {
        var txt1 = $("#<%=txt1.ClientID%>").val();
        var txt2 = $("#<%=txt2.ClientID%>").val();
        //沒有公司統編或員編就不跑其他驗證
       
      
        //if (txt1 != txt2)
        //{
        //    alert('Error');
        //    arguments.IsValid = confirm('資料不正確，您確定要送出嗎?');
        //    return;
        //}
    }
</script>
<table class="PopTable" style="width:500px">
    <tr>
        <td>
            <asp:Label ID="Label1" runat="server" Text="文字1"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txt1" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" 
                ControlToValidate="txt1" Display="Dynamic"
                runat="server" ErrorMessage="TXT1必填!"></asp:RequiredFieldValidator>
        </td>
    </tr>
        <tr>
        <td>
            <asp:Label ID="Label2" runat="server" Text="文字2"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txt2" runat="server"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="Label3" runat="server" Text="下拉"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddl" runat="server">
                <asp:ListItem>A</asp:ListItem>
                <asp:ListItem>B</asp:ListItem>
                 <asp:ListItem>C</asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>

</table>
<asp:CustomValidator ID="CustomValidator1" 
    Display="Dynamic" ClientValidationFunction="ValidateData"
    runat="server" ErrorMessage=""></asp:CustomValidator>


    <asp:TextBox ID="txtFieldValue" runat="server" style="display:none" ></asp:TextBox>

</asp:Content>

