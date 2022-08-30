<%@ Page Title="" Language="C#" MasterPageFile="~/Master/DefaultMasterPage.master" AutoEventWireup="true" CodeFile="Mobile_TEST3.aspx.cs" Inherits="CDS_WebPage_Mobile_Mobile_TEST3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <html>
    <head>
        <title>Html-Qrcode Demo</title>
        <body>
            <div id="qr-reader" style="width: 500px"></div>
            <div id="qr-reader-results"></div>

            <div>
                <%--<input type="text" id="myTextcontent" runat="server" value="">--%>
                <asp:TextBox id="myTextcontent" runat="server"></asp:TextBox>
            </div>

            <div>
                <button onclick="myFunction()">重拍</button>
            </div>
            <div>
            </div>
        </body>

        <%--<script src="https://unpkg.com/html5-qrcode"></script>--%>
        <script src="HTML5/html5-qrcode.min.js"></script>

        <script>
            function myFunction() {
                var resultContainer = document.getElementById('qr-reader-results');
                var lastResult, countResults = 0;
                function onScanSuccess(decodedText, decodedResult) {
                    if (decodedText !== lastResult) {
                        ++countResults;
                        lastResult = decodedText;
                        // Handle on success condition with the decoded message.
                        //console.log(`Scan result ${decodedText}`, decodedResult);

                        //alert(decodedText);
                        document.getElementById('<%=myTextcontent.ClientID%>').value = decodedText;

                        html5QrcodeScanner.clear();
                    }
                }

                var html5QrcodeScanner = new Html5QrcodeScanner(
                    "qr-reader", { fps: 10, qrbox: 250 });
                html5QrcodeScanner.render(onScanSuccess);

                document.getElementById('<%=myTextcontent.ClientID%>').value = null;
            }

            function docReady(fn) {
                // see if DOM is already available
                if (document.readyState === "complete"
                    || document.readyState === "interactive") {
                    // call on next available tick
                    setTimeout(fn, 1);
                } else {

                    document.addEventListener("DOMContentLoaded", fn);

                }
            }

            docReady(function () {

                var resultContainer = document.getElementById('qr-reader-results');
                var lastResult, countResults = 0;
                function onScanSuccess(decodedText, decodedResult) {
                    if (decodedText !== lastResult) {
                        ++countResults;
                        lastResult = decodedText;
                        // Handle on success condition with the decoded message.
                        //console.log(`Scan result ${decodedText}`, decodedResult);

                        //alert(decodedText);
                        document.getElementById('<%=myTextcontent.ClientID%>').value = decodedText;

                        html5QrcodeScanner.clear();
                    }
                }

                var html5QrcodeScanner = new Html5QrcodeScanner(
                    "qr-reader", { fps: 10, qrbox: 250 });
                html5QrcodeScanner.render(onScanSuccess);

                document.getElementById('<%=myTextcontent.ClientID%>').value = null;
            });
        </script>
    </head>
    </html>



</asp:Content>

