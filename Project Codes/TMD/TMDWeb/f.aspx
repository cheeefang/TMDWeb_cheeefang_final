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
//     $(document).ready(function() 
//{ 
//$("#Gridview1").tablesorter(); 
////});
//$(function() {
//  $("#Gridview1").tablesorter();
//});

             document.addEventListener('DOMContentLoaded', function () {
                 let x = document.getElementById("GridView1");
                 x.tablesorter();
   // ...
});
    </script>
</body>
<%--    <script type="text/javascript" src="js/jquery.tablesorter.js"></script>

<!-- tablesorter widgets (optional) -->
<script type="text/javascript" src="js/jquery.tablesorter.widgets.js"></script>--%>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js"></script>
<script src="https://cdnjs.cloudflare.com/ajax/libs/jquery.tablesorter/2.28.15/js/jquery.tablesorter.min.js"></script>
<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">
<script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
</html>
