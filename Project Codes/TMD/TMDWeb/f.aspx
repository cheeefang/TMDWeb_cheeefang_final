<%@ Page Language="C#" AutoEventWireup="true" CodeFile="f.aspx.cs" Inherits="targeted_marketing_display.f" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <style type="text/css">
  
    </style>

</head>
<body>
    <form id="form1" runat="server">
        <div>
       <asp:GridView ID="GridView1" runat="server" CssClass="tablesorter" CellPadding="4"
            ForeColor="#333333" GridLines="None" OnRowDataBound="GridView1_RowDataBound"   >
            
        </asp:GridView>
        </div>
    </form>
         <script type="text/javascript">
     $(document).ready(function() 
{ 
$("#Gridview1").tablesorter(); 
});
    
    </script>
</body>
</html>
