<%@ Page Title="" Language="C#" MasterPageFile="~/Master/DialogMasterPage.master" AutoEventWireup="true" CodeFile="TKRESEARCHTBSALESDEVMEMODialogADD.aspx.cs" Inherits="CDS_WebPage_TKRESEARCHTBSALESDEVMEMODialogADD" %>

<%@ Register Src="~/Common/HtmlEditor/UC_HtmlEditor.ascx" TagPrefix="uc1" TagName="UC_HtmlEditor" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<script>
        
</script>

    <table class="PopTable" >
        <tr>
            <td class="PopTableLeftTD" >
                <asp:Label ID="Label1" runat="server" Text="CDS_WebPage_TKRESEARCHTBSALESDEVMEMODialogADD"></asp:Label>
            </td>
            <td class="PopTableRightTD" colspan=2>
                <asp:Label ID="lblParam" runat="server" Text=""></asp:Label>
            </td>            
        </tr>        
          <tr>
            <td class="PopTableLeftTD" >
                <asp:Label ID="Label8" runat="server" Text="金額"></asp:Label>
            </td>
            <td class="PopTableRightTD">
                <asp:TextBox ID="txtTextBox6" runat="server"  Text="10000"></asp:TextBox>
            </td> 
       </tr>

    </table>

 
</asp:Content>

