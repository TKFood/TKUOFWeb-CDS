<%@ Page Title="" Language="C#" MasterPageFile="~/Master/DefaultMasterPage.master" AutoEventWireup="true" CodeFile="TKGETDESIGNED.aspx.cs" Inherits="CDS_WebPage_DESIGN_TKGETDESIGNED" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <html>
    <head runat="server">
        <title>美工檔案瀏覽器</title>
        <style>
            body {
                font-family: Microsoft JhengHei, sans-serif;
                background: #f4f4f4;
                margin: 20px;
            }

            .container {
                background: #fff;
                padding: 20px;
                border-radius: 8px;
                box-shadow: 0 2px 10px rgba(0,0,0,0.1);
            }

            .path-bar {
                background: #e9ecef;
                padding: 10px;
                border-radius: 4px;
                margin-bottom: 20px;
                color: #495057;
                font-weight: bold;
            }

            .item-list {
                display: flex;
                flex-wrap: wrap;
                gap: 15px;
            }

            .card {
                width: 220px;
                border: 1px solid #ddd;
                border-radius: 5px;
                overflow: hidden;
                background: #fff;
                transition: 0.3s;
            }

                .card:hover {
                    transform: translateY(-5px);
                    box-shadow: 0 5px 15px rgba(0,0,0,0.1);
                }

                .card img {
                    width: 100%;
                    height: 150px;
                    object-fit: cover;
                    background: #eee;
                }

                .card .info {
                    padding: 10px;
                    font-size: 13px;
                }

                    .card .info .name {
                        font-weight: bold;
                        display: block;
                        margin-bottom: 5px;
                        word-break: break-all;
                    }

                    .card .info .type {
                        color: #28a745;
                        font-size: 11px;
                    }

            .folder-icon {
                display: block;
                text-align: center;
                line-height: 150px;
                font-size: 50px;
                text-decoration: none;
                color: #f39c12;
                background: #fffbe6;
            }

            a {
                text-decoration: none;
                color: inherit;
            }
        </style>
    </head>
    <body>
        <form id="form1" runat="server">
            <div class="container">
                <h2>TKGETDESIGNED - 檔案瀏覽器</h2>
                <div class="path-bar">
                    <asp:Literal ID="litCurrentPath" runat="server"></asp:Literal>
                </div>

                <div class="item-list">
                    <asp:PlaceHolder ID="phBack" runat="server" Visible="false">
                        <div class="card">
                            <asp:HyperLink ID="hlBack" runat="server" CssClass="folder-icon">⬆️</asp:HyperLink>
                            <div class="info"><span class="name">返回上一層</span></div>
                        </div>
                    </asp:PlaceHolder>

                    <asp:Repeater ID="rptFiles" runat="server">
                        <ItemTemplate>
                            <div class="card">
                                <a href='<%# Eval("LinkUrl") %>'>
                                    <%# Eval("Type").ToString() == "Folder" ? 
                                    "<span class='folder-icon'>📁</span>" : 
                                    "<img src='TKGETDESIGNED_ImageHandler.ashx?relPath=" + HttpUtility.UrlEncode(Eval("RelativePath").ToString()) + "' />" %>
                            </a>
                                <div class="info">
                                    <span class="name"><%# Eval("Name") %></span>
                                    <span class="type"><%# Eval("Type").ToString() == "Folder" ? "[資料夾]" : "[圖片]" %></span>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
        </form>
    </body>
    </html>
</asp:Content>

