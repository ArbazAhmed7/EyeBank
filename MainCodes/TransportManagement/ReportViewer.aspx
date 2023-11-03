<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportViewer.aspx.cs" Inherits="TransportManagement.ReportViewer" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" 
            AutoDataBind="True" EnableDatabaseLogonPrompt="true" Height="1202px" 
            ReportSourceID="CrystalReportSource1" ToolPanelView="None" ToolPanelWidth="200px" Width="903px"  />
            <CR:CrystalReportSource ID="CrystalReportSource1" runat="server"></CR:CrystalReportSource>
        </div>
    </form>
</body>
</html>
