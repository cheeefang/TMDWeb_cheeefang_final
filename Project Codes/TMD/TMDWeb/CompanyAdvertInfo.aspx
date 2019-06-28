<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CompanyAdvertInfo.aspx.cs" Inherits="targeted_marketing_display.CompanyAdvertInfo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
        </div>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Height="280px" OnPreRender="GridView1_PreRender" Width="1010px">
            <Columns>
            
                <asp:BoundField HeaderText="Advert Name"></asp:BoundField>


                <asp:BoundField HeaderText="Advert Type"></asp:BoundField>
          
                <asp:BoundField HeaderText="Start Date"></asp:BoundField>
                <asp:BoundField HeaderText="End Date"></asp:BoundField>

            </Columns>
        </asp:GridView>
    </form>
</body>
</html>
