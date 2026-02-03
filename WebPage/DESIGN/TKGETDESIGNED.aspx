<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TKGETDESIGNED.aspx.cs" Inherits="CDS_WebPage_DESIGN_TKGETDESIGNED" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>產品圖瀏覽器</title>
    <style>
        body { font-family: "Microsoft JhengHei", sans-serif; background: #f4f7f6; margin: 20px; }
        .container { background: #fff; padding: 25px; border-radius: 12px; box-shadow: 0 4px 20px rgba(0,0,0,0.1); }
        .path-bar { background: #f8f9fa; padding: 12px; border-radius: 6px; margin-bottom: 20px; border: 1px solid #dee2e6; font-size: 14px; color: #555; }
        .item-list { display: flex; flex-wrap: wrap; gap: 20px; }
        .card { width: 200px; border: 1px solid #eee; border-radius: 8px; overflow: hidden; background: #fff; transition: 0.3s; }
        .card:hover { transform: translateY(-5px); box-shadow: 0 8px 25px rgba(0,0,0,0.1); }
        .card img { width: 100%; height: 140px; object-fit: cover; background: #fafafa; }
        .folder-icon { display: block; text-align: center; line-height: 140px; font-size: 60px; background: #fff9f1; color: #f39c12; text-decoration: none; }
        .info { padding: 10px; font-size: 13px; font-weight: bold; border-top: 1px solid #f9f9f9; text-align: center; }
        .back-btn { background: #34495e !important; color: white !important; }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h2>TKGETDESIGNED - 11.產品圖</h2>
            <div class="path-bar">
                <asp:Literal ID="litCurrentPath" runat="server"></asp:Literal>
            </div>
            <div class="item-list">
                <asp:PlaceHolder ID="phBack" runat="server" Visible="false">
                    <div class="card">
                        <asp:HyperLink ID="hlBack" runat="server" CssClass="folder-icon back-btn">⬅️</asp:HyperLink>
                        <div class="info">返回上一層</div>
                    </div>
                </asp:PlaceHolder>
                <asp:Repeater ID="rptFiles" runat="server">
                    <ItemTemplate>
                        <div class="card">
                            <a href='<%# Eval("LinkUrl") %>'>
                                <%# Eval("Type").ToString() == "Folder" ? "<span class='folder-icon'>📁</span>" : "<img src='TKGETDESIGNED_ImageHandler.ashx?relPath=" + HttpUtility.UrlEncode(Eval("RelativePath").ToString()) + "' />" %>
                            </a>
                            <div class="info"><%# Eval("Name") %></div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </form>
</body>
</html>