<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Using_MS_SQL_Server.aspx.cs" Inherits="Using_MS_SQL_Server" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<script type="text/javascript" src="FusionChartsXT/FusionCharts.js"></script>
    <title>Using MS SQL Server data to create JavaScript charts with FusionCharts XT</title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Literal ID="chart_from_db" runat="server">        
        </asp:Literal>
    </form>
</body>
</html>
