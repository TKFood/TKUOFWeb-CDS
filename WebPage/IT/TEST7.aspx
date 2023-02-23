<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TEST7.aspx.cs" Inherits="CDS_WebPage_IT_TEST7" %>

<!DOCTYPE html>
<html>
<head>
  <meta charset="UTF-8">
  <title>語音輸入測試</title>
  <script>
      function setupInput() {
          var myInput = document.getElementById("myInput");

          if (/Android|webOS|iPhone|iPad|iPod|BlackBerry|IEMobile|Opera Mini/i.test(navigator.userAgent)) {
              myInput.addEventListener("input", function () {
                  if (myInput.value.trim() === "") {
                      myInput.setAttribute("inputmode", "voice");
                  } else {
                      myInput.setAttribute("inputmode", "text");
                  }
              });
          }
      }
  </script>
</head>
<body onload="setupInput();">
  <h1>語音輸入測試</h1>
  <p>在手機上輸入下面的文字框：</p>
  <input type="text" id="myInput">
</body>
</html>

