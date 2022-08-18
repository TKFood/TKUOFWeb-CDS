<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Mobile_Test3.aspx.cs" Inherits="CDS_WebPage_Mobile_Mobile_Test3" %>

<!doctype html>
<html lang="en">
<head>
    <!-- Required meta tags -->
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
    <title>QR Code Scanner Example</title>


</head>

<body class="cont">

    <h3 class="txt-cntr mar-top-2">Simple QR Code Scanner Example</h3>

    <article class="pad-2">
        <section>
            <div>
                <input type="text" id="myTextcontent" value="">
            </div>
            <div class="frm-grp txt-cntr">
                <label class="disp-blck">Choose camera:</label>
                <select id="webcameraChanger" onchange="cameraChange($(this).val());" class="frm-ctrl mar-btm-2"></select>
            </div>
            <!-- webcamera view component -->
            <video id="webcameraPreview" playsinline autoplay controls muted loop style="width: 100px; height: 100px"></video>
        </section>
    </article>

    <script src="https://code.jquery.com/jquery-3.3.1.min.js" integrity="sha256-FgpCb/KJQlLNfOu91ta32o/NMZxltwRo8QtmkMRdAu8=" crossorigin="anonymous"></script>
    <script src="IOS11SRC/adapter.min.js"></script>
    <script src="IOS11SRC/instascan.js"></script>
    <script src="IOS11SRC/jquery.min.js"></script>
    <script src="IOS11SRC/QrCodeScanner.js"></script>
    <script type="text/javascript">
        document.getElementById("myTextcontent").value = null;
        //HTML video component for web camera
        var videoComponent = $("#webcameraPreview");
        //HTML select component for cameras change
        var webcameraChanger = $("#webcameraChanger");
        var options = {};
        //init options for scanner
        options = initVideoObjectOptions("webcameraPreview");
        var cameraId = 0;

        initScanner(options);

        initAvaliableCameras(
            webcameraChanger,
            function () {
                cameraId = parseInt(getSelectedCamera(webcameraChanger));
            }
        );

        initCamera(cameraId);


        scanStart(function (data) {
           

            document.getElementById("myTextcontent").value = data;
            scanner.stop();

            //alert(data);
        });

    </script>

</body>
</html>
