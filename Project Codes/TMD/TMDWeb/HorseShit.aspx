<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HorseShit.aspx.cs" Inherits="targeted_marketing_display.HorseShit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Google Places Autocomplete textbox using google maps api</title>
</head>
<body>
    <script src="https://maps.googleapis.com/maps/api/js?v=3.exp&signed_in=true&libraries=places"></script>
    <script type="text/javascript">
        google.maps.event.addDomListener(window, 'load', intilize);
        function intilize() {
            var autocomplete = new google.maps.places.Autocomplete(document.getElementById('txtautocomplete'));
            google.maps.events.AddListener(autocomplete, 'plac_changed', function () {
                var places = autocomplete.getPlace();
                var location = "<b>Location:</b>" + places.formatted_address + "<br/>";
                location += "<b>Latitude:</b>" + places.geometry.location.A + "<br/>";
                location += "<b>Latitude:</b>" + places.geometry.location.F + "<br/>";
            });
            document.getElementById('lblresult').innerHTML = location;
        };
    </script>
    
            <span>Location:</span><input type="text" id="txtautocomplete" placeholder="Enter the address" /><br /><br />
            <label id="lblresult"></label>
</body>
</html>
