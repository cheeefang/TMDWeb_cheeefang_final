<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CompanyAdvertInfo.aspx.cs" Inherits="targeted_marketing_display.CompanyAdvertInfo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Company Adverts Listing</title>
</head>
<body>
 
        
       
            

    <form id="form1" runat="server">
          <div class="row">
                    <div class="col-lg-12">
                        <h1 class="page-header">Company Adverts Listing</h1>
                    </div>
                    <!-- /.col-lg-12 -->
                </div>
        <asp:GridView ID="GridView1" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <EditRowStyle BackColor="#999999" />
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#E9E7E2" />
            <SortedAscendingHeaderStyle BackColor="#506C8C" />
            <SortedDescendingCellStyle BackColor="#FFFDF8" />
            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
        </asp:GridView>
 
        
       
            

    </form>
          <p>
 
        
       
            

          <asp:Label ID="ErrorMessage" runat="server" visible="false" Text="This Company has yet to create an Advertisement."></asp:Label>
 
        
       
            

          </p>
 
        
       
            

    </form>
    </body>
</html>
