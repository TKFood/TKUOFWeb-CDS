<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Mobile_TEST2.aspx.cs" Inherits="CDS_WebPage_Mobile_Mobile_TEST2" %>

<!DOCTYPE html>
<html>
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta http-equiv="X-UA-Compatible" content="ie=edge">
    <title>instascan：純前端掃描 QR Code</title>
    <%--    <script src="https://rawgit.com/schmich/instascan-builds/master/instascan.min.js"></script>--%>
        
    <script src="../../instascan.min/instascan.js"></script>
<%--    <script src="../../instascan.min/instascan.min.js"></script>--%>
    <%--<script src="../../instascan.min/IOS15instascan.min.js"></script>--%>
</head>

<body>
    <input type="text" id="myText" value="">

    <input type="text" id="myTextcontent" value="">

    <button onclick="myFunction()">Try it</button>
    <button onclick="myFunctionQR()">Try 鏡頭</button>

    <video id="preview"  width="100" height="100"></video>

    <script type="text/javascript">
        function myFunction() {
            if (document.getElementById("myText").value == "後鏡頭") {
                document.getElementById("myText").value = "";
            }
            else {
                document.getElementById("myText").value = "後鏡頭";
            }


        }
       

        function myFunctionQR() {

            document.getElementById("myTextcontent").value = null;  

            let scanner = new Instascan.Scanner({
                scanPeriod: 4, 
                continuous: true, // 連續掃描
                mirror: false,
                captureImage: true,
                backgroundScan: false,
                video: document.getElementById('preview'), // 預覽
                facingMode: {
                    exact: "environment"
                }
            });

            scanner.addListener('scan', function (content) {

                //console.log(content);                
                //alert(content);
                document.getElementById("myTextcontent").value = content;  
                scanner.stop()

                //getConfirmation();

                //function getConfirmation() {

                //    document.getElementById("myTextcontent").value = "getConfirmation";  

                //    var retVal = confirm("掃描結果：" + content); // 掃描結果顯示
                //    document.getElementById("myTextcontent").value = retVal;

                //    //if (retVal == true) {
                //    //    var checkurl = /^((https?|http?|file):\/\/)?([\da-z\.-]+)\.([a-z\.]{2,6})([\/\w \.-]*)*\/?$/; // 檢查是否為網址
                //    //    if (checkurl.test(content)) {
                //    //        window.open(content, "_self"); // 開啟網址
                //    //    }
                //    //    return true;
                //    //} else {
                //    //    return false;
                //    //}
                //}


            });

            Instascan.Camera.getCameras().then(function (cameras) {

                //scanner.start(cameras[0]); // [0] 前鏡頭 [1] 後鏡頭 

                if (cameras.length > 0) {
                    //scanner.start(cameras[2]); // [0] 前鏡頭 [1] 後鏡頭 
                    document.getElementById("myText").value = "找到相機" + String(cameras.length);
                    scanner.start(cameras[0]);

                    //if (cameras.length >= 2)
                    //{
                    //    scanner.start(cameras[0]);
                    //}
                    //else
                    //{
                    //    scanner.start(cameras[0]);
                    //}
                    
                }
                else {
                    //console.error('沒有找到相機');
                    document.getElementById("myText").value = "沒有找到相機";
                }
            }).catch(function (e) {
                //console.error(e);
                document.getElementById("myText").value = "catch error";
                alert('No Back camera found!');
                //if (cameras.length > 0) {
                //    var selectedCam = cameras[0];
                //    $.each(cameras, (i, c) => {
                //        if (c.name.indexOf('back') != -1) {
                //            selectedCam = c;
                //            return false;
                //        }
                //    });
                //    scanner.start(selectedCam);
                //}
                //else
                //{
                //    console.error('No cameras found.');
                //}
            });
        }

    </script>



</body>

</html>
