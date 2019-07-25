<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OneMapTest.aspx.cs" Inherits="targeted_marketing_display.OneMapTest" %>

 <!DOCTYPE html>
          <html>
      <head>
        <title>OneMap2 XYZ (Default)</title>
        <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/leaflet/0.7.3/leaflet.css" />
        <script src="https://cdn.onemap.sg/leaflet/onemap-leaflet.js"></script>
      </head>
                 var basemap = L.tileLayer('https://maps-{s}.onemap.sg/v3/Default/{z}/{x}/{y}.png', {
          detectRetina: true,
          maxZoom: 18,
          minZoom: 11,
          //Do not remove this attribution
          attribution: '<img src="https://docs.onemap.sg/maps/images/oneMap64-01.png" style="height:20px;width:20px;"/> New OneMap | Map data &copy; contributors, <a href="http://SLA.gov.sg">Singapore Land Authority</a>'
        });
      <body>
        <h1>Singapore Map</h1>
        <div id='mapdiv' style='height:800px;'></div>
      </body>
      <script>
        var center = L.bounds([1.56073, 104.11475], [1.16, 103.502]).getCenter();
        var map = L.map('mapdiv').setView([center.x, center.y], 12);

        var basemap = L.tileLayer('https://maps-{s}.onemap.sg/v3/Default/{z}/{x}/{y}.png', {
          detectRetina: true,
          maxZoom: 18,
          minZoom: 11
        });

        map.setMaxBounds([[1.56073, 104.1147], [1.16, 103.502]]);

        basemap.addTo(map);

        function getLocation() {
          if (navigator.geolocation) {
            navigator.geolocation.getCurrentPosition(showPosition);
          } 
        }

        function showPosition(position) {           
          marker = new L.Marker([position.coords.latitude, position.coords.longitude], {bounceOnAdd: false}).addTo(map);             
          var popup = L.popup()
          .setLatLng([position.coords.latitude, position.coords.longitude]) 
          .setContent('You are here!')
          .openOn(map);         
        }
      </script>
      </html>
