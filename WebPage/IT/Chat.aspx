﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Master/DefaultMasterPage.master" AutoEventWireup="true" CodeFile="Chat.aspx.cs" Inherits="CDS_WebPage_IT_Chat" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>ChatGPT 問答</h2>

    <asp:TextBox ID="txtPrompt" runat="server" TextMode="MultiLine" Rows="4" Columns="60" />
    <br /><br />
    <asp:Button ID="btnSend" runat="server" Text="送出" OnClick="btnSend_Click" />
    <br /><br />
    <asp:Label ID="Label1" runat="server" Text="以下回覆"/>
    <asp:Literal ID="txtResponse" runat="server" Mode="PassThrough" />

</asp:Content>
