<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ExportChartToPDF.aspx.cs" Inherits="ExportChartToPDF" %>

<%@ Register assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.DataVisualization.Charting" tagprefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div> 
        <asp:Chart ID="Chart1" runat="server">
            <series>
                <asp:Series Name="Series1" Legend="Legend1">
                <Points>
                    <asp:DataPoint AxisLabel="Article" YValues="90" />
                    <asp:DataPoint AxisLabel="Blogs" YValues="120" />
                    <asp:DataPoint AxisLabel="Questions" YValues="300" />
                    <asp:DataPoint AxisLabel="Videos" YValues="240" />
                    <asp:DataPoint AxisLabel="Training" YValues="100" />
                </Points>
                </asp:Series>
            </series>
            <chartareas>
                <asp:ChartArea Name="ChartArea1">
                </asp:ChartArea>
            </chartareas>
        <titles><asp:Title Name="Title1" Text="Website Stats"></asp:Title></titles></asp:Chart>
        <br />
 <asp:Button ID="btnExport" runat="server" Text="Export To PDF" OnClick="btnExport_Click" />
    </div>
       
    </form>
</body>
</html>
