<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportViewerRDLC.aspx.cs" Inherits="TransportManagement.ReportViewerRDLC" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    
    <form id="form1" runat="server">
        <asp:ScriptManager ID="scm" runat="server"></asp:ScriptManager>
        <div>
            <rsweb:ReportViewer ID="ReportViewer1" runat="server" Height="800px" Width="1200px"></rsweb:ReportViewer>
        </div>
    </form>
</body>
</html>
