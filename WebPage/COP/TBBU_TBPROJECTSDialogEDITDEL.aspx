﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Master/DialogMasterPage.master" AutoEventWireup="true" CodeFile="TBBU_TBPROJECTSDialogEDITDEL.aspx.cs" Inherits="CDS_WebPage_TBBU_TBPROJECTSDialogEDITDEL" %>

<%@ Register Src="~/Common/HtmlEditor/UC_HtmlEditor.ascx" TagPrefix="uc1" TagName="UC_HtmlEditor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <link rel="stylesheet" href="http://code.jquery.com/ui/1.10.4/themes/trontastic/jquery-ui.css">

    <script>
        $(function () {

        });

    </script>
    <div style="overflow-x: auto; width: 800px">
    </div>
    <table class="PopTable">
        <tr>
            <td class="PopTableLeftTD">
                <asp:Label ID="Label1" runat="server" Text="CDS_WebPage_TBBU_COPCOPMACLIENTDialogEDITDEL"></asp:Label>
            </td>
            <td class="PopTableRightTD" colspan="2">
                <asp:Label ID="lblParam" runat="server" Text=""></asp:Label>
            </td>
        </tr>

    </table>
    <table>
        <tr>
            <td class="PopTableLeftTD">
                <asp:Label ID="Label2" runat="server" Text="年度"></asp:Label>
            </td>
            <td class="PopTableRightTD">
                <asp:TextBox ID="TextBox1" runat="server" Text="" Width="200%" TextMode="MultiLine" Row="1" Style="height: 20px;"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="PopTableLeftTD">
                <asp:Label ID="Label3" runat="server" Text="週次"></asp:Label>
            </td>
            <td class="PopTableRightTD">
                <asp:TextBox ID="TextBox2" runat="server" Text="" Width="200%" TextMode="MultiLine" Row="1" Style="height: 20px;"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="PopTableLeftTD">
                <asp:Label ID="Label5" runat="server" Text="通路"></asp:Label>
            </td>
            <td class="PopTableRightTD">
                <%--<asp:TextBox ID="TextBox3" runat="server" Text="" Width="200%" TextMode="MultiLine" Row="5" Style="height: 120px;"></asp:TextBox>--%>

                <asp:DropDownList ID="DropDownList1" runat="server"></asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="PopTableLeftTD">
                <asp:Label ID="Label6" runat="server" Text="活動品項"></asp:Label>
            </td>
            <td class="PopTableRightTD">
                <asp:TextBox ID="TextBox4" runat="server" Text="" Width="200%" TextMode="MultiLine" Row="5" Style="height: 120px;"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="PopTableLeftTD">
                <asp:Label ID="Label7" runat="server" Text="活動內容"></asp:Label>
            </td>
            <td class="PopTableRightTD">
                <asp:TextBox ID="TextBox5" runat="server" Text="" Width="200%" TextMode="MultiLine" Row="5" Style="height: 120px;"></asp:TextBox>
            </td>
        </tr>




    </table>

    <table>
        <tr>
            <td class="PopTableLeftTD">
                <asp:Label ID="Label4" runat="server" Text="是否刪除"></asp:Label>
            </td>
            <td class="PopTableRightTD">
                <asp:Button ID="Button1" runat="server" Text="刪除" ForeColor="red"
                    OnClick="btn1_Click" meta:resourcekey="btn1Resource1" />
            </td>
        </tr>
    </table>

</asp:Content>

