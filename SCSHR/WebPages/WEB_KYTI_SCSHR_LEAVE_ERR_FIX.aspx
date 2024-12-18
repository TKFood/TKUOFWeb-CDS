﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WEB_KYTI_SCSHR_LEAVE_ERR_FIX.aspx.cs" MasterPageFile="~/Master/DefaultMasterPage.master" Inherits="CDS_SCSHR_WebPages_WEB_KYTI_SCSHR_LEAVE_ERR_FIX" %>

<%@ Register Src="~/Common/ChoiceCenter/UC_BtnChoiceOnce.ascx" TagPrefix="uc1" TagName="UC_BtnChoiceOnce" %>
<%@ Register Src="~/KYTControl/KYTTextBox.ascx" TagPrefix="uc1" TagName="KYTTextBox" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="<%=Page.ResolveUrl("~/KYTControl/css/gemps.ui.css")%>" rel="stylesheet" />
    <link href="<%=Page.ResolveUrl("~/KYTControl/css/font-awesome.min.css")%>" rel="stylesheet" />
    <script src="<%=Page.ResolveUrl("~/KYTControl/js/gemps.ui.js")%>"></script>
    <link href="<%=Page.ResolveUrl("~/CDS/SCSHR/Assets/css/SCSHR.css")%>" rel="stylesheet" />
    <!-- 引用bootstrap -->
    <link href="<%=Page.ResolveUrl("~/CDS/bootstrap/css/bootstrap.min.css")%>" rel="stylesheet" />
    <script src="<%=Page.ResolveUrl("~/CDS/bootstrap/js/popper.min.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/CDS/bootstrap/js/bootstrap.min.js")%>"></script>
    <style>
        .hidden {
            display: none;
        }

        div.RowNormal:first-child {
            margin-top: 12px;
        }

        div.RowNormal:not(:first-child) {
            margin-top: 5px;
        }

        div.RowNormal > div:first-child {
            flex-basis: 135px;
        }

        div.RowNormal > div:not(:first-child) {
            padding: 0;
        }

        div.RowNormal.special {
            margin-top: -2px;
        }

            div.RowNormal.special > div:first-child {
                margin-top: 5px;
            }

            div.RowNormal.special > div:nth-child(2) > span {
                margin-top: 5px;
            }

        div.inputBorder .RadPicker input {
            border-color: #A9A9A9;
        }

        div.GridViewNormal {
            padding: 0;
            overflow-x: inherit;
        }

            div.GridViewNormal td {
                background-color: white;
            }

            div.GridViewNormal table tr > th {
                text-align: center;
            }

            div.GridViewNormal table > tbody > tr:nth-child(odd) > td {
                background-color: #ffffff;
            }

            div.GridViewNormal table > tbody > tr:nth-child(even) > td {
                background-color: #f7ffe7;
            }

        div.selectHidden > div > div {
            display: none;
        }

        html {
            overflow: hidden;
        }

        .limitHeight {
            overflow: auto;
        }
    </style>
    <script>
        $(document).ready(function () {
            LimitHeight()
        });
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(LimitHeight);
        function LimitHeight() {
            $(".limitHeight").css("height", "calc(100vh - " + $("#MasterHeader").css("height") + " - " + $("#MasterFooter").css("height"));
        }
    </script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="col-md-12 limitHeight" style="width: 100vw;">
                <div class="row">
                    <div class="col-md-12 divheader2">
                        請假單處理
                    </div>
                </div>
                <div class="row RowNormal">
                    <div class="col-sm-2">
                        請假單號:
                    </div>
                    <div class="col-sm-10">
                        <asp:TextBox ID="txtDOC_NBR" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="row RowNormal">
                    <div class="col-sm-2">
                        請假單申請時間:
                    </div>
                    <div class="col-sm-10 inputBorder">
                        <telerik:RadDateTimePicker runat="server" ID="dtpStart" />
                        ~
                        <telerik:RadDateTimePicker runat="server" ID="dtpEnd" />
                    </div>
                </div>
                <div class="row RowNormal special">
                    <div class="col-sm-2">
                        申請人:
                    </div>
                    <div class="col-sm-10">
                        <asp:TextBox ID="txtAPPLICANT" runat="server" ReadOnly="true"></asp:TextBox>
                        <asp:HiddenField ID="hidAPPLICANTGUID" runat="server" />
                        <uc1:UC_BtnChoiceOnce runat="server" ID="bcoMan" ButtonText="選人" ChoiceOnceKind="Employee" OnEditButtonOnClick="bcoMan_EditButtonOnClick" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12" style="margin: 12px 0;">
                        <asp:Button ID="btnSearch" runat="server" Text="搜尋" OnClick="btnSearch_Click" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12" style="margin: 12px 0;">
                        <asp:Button ID="btnPartReBuild" runat="server" Text="失敗單號重轉" OnClick="btnPartReBuild_Click" />
                        <asp:Button ID="btnReBuild" runat="server" Text="重轉" OnClick="btnReBuild_Click" />
                    </div>
                </div>
                <div>
                    <div class="row">
                        <div class="col-md-12 GridViewNormal">
                            <asp:GridView runat="server" ID="gvLVH"
                                CssClass="tsGridView2 horzFull"
                                AutoGenerateColumns="false"
                                ShowHeader="true"
                                ShowHeaderWhenEmpty="false"
                                OnRowDataBound="gvLVH_RowDataBound">
                                <EmptyDataRowStyle HorizontalAlign="Center" />
                                <EmptyDataTemplate>
                                    <asp:Label ID="Label01" runat="server" ForeColor="Red" Font-Bold="true" Text="目前沒有明細資料"></asp:Label>
                                </EmptyDataTemplate>
                                <Columns>
                                    <asp:TemplateField HeaderText="中繼表更新狀態">
                                        <ItemTemplate>
                                            <asp:Label ID="lblZTABLE_STATUS" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="飛騰更新狀態">
                                        <ItemTemplate>
                                            <asp:Label ID="lblWS_STATUS" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="請假單單單號">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDOC_NBR" runat="server" Text='<%#Bind("DOC_NBR")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="請假人">
                                        <ItemTemplate>
                                            <asp:Label ID="lblLEAEMP" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="申請日期">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAPPLICANTDATE" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="代理人">
                                        <ItemTemplate>
                                            <asp:Label ID="lblLEAAGENT" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="請假代碼">
                                        <ItemTemplate>
                                            <asp:Label ID="lblLEACODE" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="請假名稱">
                                        <ItemTemplate>
                                            <asp:Label ID="lblLEACODENAME" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="請假起">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSTARTTIME" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="請假迄">
                                        <ItemTemplate>
                                            <asp:Label ID="lblENDTIME" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="請假時數">
                                        <ItemTemplate>
                                            <asp:Label ID="lblLEAHOURS" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="請假天數">
                                        <ItemTemplate>
                                            <asp:Label ID="lblLEADAYS" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="訊息">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMSG" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
