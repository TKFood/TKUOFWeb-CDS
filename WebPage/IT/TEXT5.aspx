<%@ Page Title="" Language="C#" MasterPageFile="~/Master/DefaultMasterPage.master" AutoEventWireup="true" CodeFile="TEXT5.aspx.cs" Inherits="CDS_WebPage_IT_TEXT5" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jspdf/1.5.3/jspdf.debug.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.6.0/jquery.min.js"></script>

    <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" />

    <html>
    <head>
        <meta charset="UTF-8">
        <title>HTML to PDF Converter</title>
    </head>
    <body>
        <h1>HTML to PDF Converter</h1>
        <button id="generate-pdf-button">Generate PDF File</button>

        <h1>Click</h1>
        <button id="myButton">Click Me</button>
        <script>
            $(function () {
                var name = "";
                console.log("click1 " + name + "!"); // 在控制台中輸出 "Hello, John!"

                console.log("click2 " + name + "!"); // 在控制台中輸出 "Hello, John!"


                $('#generate-pdf-button').click(function () {
                    console.log("click button " + name + "!"); // 在控制台中輸出 "Hello, John!"

                    $.get('http://localhost/TKUOF/CDS/WebPage/IT/TEXT5.aspx', function (html) {
                        console.log("get " + name + "!"); // 在控制台中輸出 "Hello, John!"
                        // Create PDF document
                        var pdf = new jsPDF();
                        // Add HTML content to PDF document
                        pdf.fromHTML(html);
                        console.log("fromHTML " + name + "!"); // 在控制台中輸出 "Hello, John!"
                        // Download PDF document
                        pdf.save('my-pdf-file.pdf');
                        console.log("save " + name + "!"); // 在控制台中輸出 "Hello, John!"
                    });
                });
            });

            // 獲取按鈕元素
            var button = document.getElementById("myButton");

            // 添加 click 事件監聽器
            button.addEventListener("click", function () {
                console.log("Button clicked!");
            });

        </script>
    </body>
    </html>

</asp:Content>

