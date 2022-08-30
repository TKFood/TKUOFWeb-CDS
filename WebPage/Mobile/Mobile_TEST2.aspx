<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Mobile_TEST2.aspx.cs" Inherits="CDS_WebPage_Mobile_Mobile_TEST2" %>



<html>
<head>
    <title>Html-Qrcode Demo</title>
    <body>
        <div id="qr-reader" style="width: 500px"></div>
        <div id="qr-reader-results"></div>

        <div>
            <input type="text" id="myTextcontent"   runat="server"  value="">
            <%--<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>--%>
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
                    document.getElementById("myTextcontent").value = decodedText;

                    html5QrcodeScanner.clear();
                }
            }

            var html5QrcodeScanner = new Html5QrcodeScanner(
                "qr-reader", { fps: 10, qrbox: 250 });
            html5QrcodeScanner.render(onScanSuccess);

            document.getElementById("myTextcontent").value = null;
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
                    document.getElementById("myTextcontent").value = decodedText;

                    html5QrcodeScanner.clear();
                }
            }

            var html5QrcodeScanner = new Html5QrcodeScanner(
                "qr-reader", { fps: 10, qrbox: 250 });
            html5QrcodeScanner.render(onScanSuccess);

            document.getElementById("myTextcontent").value = null;
        });
    </script>
</head>
