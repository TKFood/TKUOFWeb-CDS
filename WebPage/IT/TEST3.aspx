<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TEST3.aspx.cs" Inherits="CDS_WebPage_IT_TEST3" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">


    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
           <input type="text" id="textbox" />
            <button onclick="startDictation()">开始</button>
             <input type="text" id="textbox2" />
        </div>
    </form>
</body>
<script>
    var recognition = new webkitSpeechRecognition();
    recognition.lang = "zh-TW"; // 设置语言为繁体中文
    recognition.continuous = true; // 设置语音识别为连续模式

    recognition.onresult = function (event) {
        var result = event.results[event.resultIndex];

       

        if (result.isFinal) { // 只处理最终结果
            document.getElementById('textbox').value = result[0].transcript;

          
        }
    };

    function startDictation() {
        

        document.getElementById('textbox2').value ="OK";
    }
</script>

</html>
