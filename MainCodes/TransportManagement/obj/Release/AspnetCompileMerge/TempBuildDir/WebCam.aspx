<%@ Page Title="Web Cam" Language="C#" AutoEventWireup="true" CodeBehind="WebCam.aspx.cs" Inherits="TransportManagement.WebCam" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        body
        {
            font-family: Arial;
            font-size: 10pt;
        }
        table
        {
            border: 1px solid #ccc;
            border-collapse: collapse;
        }
        table th
        {
            background-color: #F7F7F7;
            color: #333;
            font-weight: bold;
        }
        table th, table td
        {
            padding: 5px;
            width: 300px;
            border: 1px solid #ccc;
        }
    </style>
</head>
<body>
    <table border="0" cellpadding="0" cellspacing="0">
    <tr>
        <th align="center"><u>Live Camera</u></th>
        <th align="center"><u>Captured Picture</u></th>
    </tr>
    <tr>
        <td><div id="webcam"></div></td>
        <td><img id = "imgCapture" /></td>
    </tr>
    <tr>
        <td align = "center">
            <input type="button" id="btnCapture" value="Capture" />
        </td>
        <td align = "center">
            <input type="button" id="btnUpload" value="Upload" disabled = "disabled" />
        </td>
    </tr>
    </table>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript" src="Scripts/FormsScripts/WebCam.js"></script>
    <script type="text/javascript">
 

        $(function () {
            Webcam.set({
                width: 320,
                height: 240,
                image_format: 'jpeg',
                jpeg_quality: 90
            });
            Webcam.attach('#webcam');
            $("#btnCapture").click(function () {
                Webcam.snap(function (data_uri) {
                    $("#imgCapture")[0].src = data_uri;
                    $("#btnUpload").removeAttr("disabled");
                });
            });
            $("#btnUpload").click(function () {                
                $.ajax({
                    type: "POST",
                    url: "WebCam.aspx/SaveCapturedImage",
                    data: "{'data': '" + $("#imgCapture")[0].src +  "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        alert("Picture saved successfully.");
                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        debugger;
                        console.log(errorThrown);
                    }
                    
                });
            });
        });
    </script>
</body>
</html>

