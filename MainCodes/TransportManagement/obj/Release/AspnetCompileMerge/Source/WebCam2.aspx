<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebCam2.aspx.cs" Inherits="TransportManagement.WebCam2" %>

<!DOCTYPE html>

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
                    $("#btnImport").removeAttr("disabled");
                });
            });

            $("#btnUpload").click(function () {
                $.ajax({
                    type: "POST",
                    url: "WebCam.aspx/SaveCapturedImage",
                    data: "{'data': '" + $("#imgCapture")[0].src + "'}",
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

            $("#btnImport").click(function () {
                $('input[name="hiddenCapturedImage"]').val($("#imgCapture")[0].src);
            });


        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
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
                    <asp:Button ID="btnImport" runat="server" OnClick="btnImport_Click" Text="Import" disabled = "disabled" ClientIDMode="Static" />
                    <asp:HiddenField ID="hiddenCapturedImage" runat="server" />
                </td>
            </tr>           
        </table>
    </form>
</body>
</html>
