<%@ Page Title="" Language="C#" MasterPageFile="~/Master/DefaultMasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="CDS_Choice_Default" %>

<%@ Register Src="../../Common/ChoiceCenter/UC_ChoiceList.ascx" TagName="UC_ChoiceList" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">



    <table class="PopTable">
        <tr>
            <td>
                <uc1:UC_ChoiceList ID="UC_ChoiceList1" ExpandToUser="false" runat="server" />

            </td>
            <td>

                <uc1:UC_ChoiceList ID="UC_ChoiceList2" ExpandToUser="true" runat="server" />

            </td>
         
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="TextBox1" TextMode="MultiLine" Width="300px"
                    Height="300px" runat="server"></asp:TextBox>

            </td>
            <td>
                <asp:TextBox ID="TextBox2" TextMode="MultiLine" Width="300px"
                    Height="300px" runat="server"></asp:TextBox>

            </td>

        </tr>
    </table>

</asp:Content>

