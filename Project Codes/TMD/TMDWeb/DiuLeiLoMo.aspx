<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DiuLeiLoMo.aspx.cs" Inherits="targeted_marketing_display.DiuLeiLoMo" %>

<!DOCTYPE html>
<html>
<head>
  <meta charset='utf-8' />
  <title>Local search app</title>
  <meta name='viewport' content='initial-scale=1,maximum-scale=1,user-scalable=no' />
    <script src='https://api.mapbox.com/mapbox-gl-js/plugins/mapbox-gl-geocoder/v4.2.0/mapbox-gl-geocoder.min.js'></script>
<link rel='stylesheet' href='https://api.mapbox.com/mapbox-gl-js/plugins/mapbox-gl-geocoder/v4.2.0/mapbox-gl-geocoder.css' type='text/css' />
  <script src='https://api.mapbox.com/mapbox-gl-js/v1.1.1/mapbox-gl.js'></script>
  <link href='https://api.mapbox.com/mapbox-gl-js/v1.1.1/mapbox-gl.css' rel='stylesheet' />
  <style>
    body {
      margin: 0;
      padding: 0;
    }

    #map {
      position: absolute;
      top: 0;
      bottom: 0;
      width: 100%;
    }
  </style>

</head>
<body>

    <div id='map' style="zoom:.5;-o-transform: scale(.5);-moz-transform: scale(.5)"></div>
    <script>
        mapboxgl.accessToken = 'pk.eyJ1IjoiY2hlZWVmYW5nIiwiYSI6ImNqeWdyd3ozeDAzejQzZGwzMjY0MzhzYzcifQ.BN7hdcRRbZT02s4h8QR-iw';
      

        var map = new mapboxgl.Map({
            container: 'map', // Container ID
            style: 'mapbox://styles/mapbox/streets-v11', // Map style to use
            center: [-122.25948, 37.87221], // Starting position [lng, lat]
            zoom: 12, // Starting zoom level
        });
        var marker = new mapboxgl.Marker() // initialize a new marker
            .setLngLat([-122.25948, 37.87221]) // Marker [lng, lat] coordinates
            .addTo(map); // Add the marker to the map
        var geocoder = new MapboxGeocoder({ // Initialize the geocoder
  accessToken: mapboxgl.accessToken, // Set the access token
  mapboxgl: mapboxgl, // Set the mapbox-gl instance
            marker: false, // Do not use the default marker style
              placeholder: 'Search for places',
  // bbox: [-122.30937, 37.84214, -122.23715, 37.89838], // Boundary for Berkeley
  //proximity: {
  //  longitude: -122.25948,
  //  latitude: 37.87221
  //} // Coordina
});

// Add the geocoder to the map
        map.addControl(geocoder);
        // After the map style has loaded on the page,
// add a source layer and default styling for a single point
map.on('load', function() {
  map.addSource('single-point', {
    type: 'geojson',
    data: {
      type: 'FeatureCollection',
      features: []
    }
  });

  map.addLayer({
    id: 'point',
    source: 'single-point',
    type: 'circle',
    paint: {
      'circle-radius': 10,
      'circle-color': '#448ee4'
    }
  });

  // Listen for the `result` event from the Geocoder
  // `result` event is triggered when a user makes a selection
  //  Add a marker at the result's coordinates
    geocoder.on('result', function (e) {
        console.log(e);
        console.log(e.result.context["0"].text);
        var longtitude = e.result.geometry.coordinates[0];
        var latitude = e.result.geometry.coordinates[1];
        var pCode = e.result.context["0"].text;
        
        console.log(e.result.geometry.coordinates);
        document.getElementById("LatTB").value = latitude;
        document.getElementById("LongTB").value = longtitude;
        document.getElementById("PostalCodeTB").value = pCode;
    map.getSource('single-point').setData(e.result.geometry);
  });
});
    </script>
    <form runat="server">
   Latitude <asp:TextBox ID="LatTB" runat="server" ClientIDMode="Static"></asp:TextBox>
    <br />
   Longtitude <asp:TextBox ID="LongTB" runat="server" ClientIDMode="Static"></asp:TextBox>
   Postal Code<asp:TextBox ID="PostalCodeTB" runat="server" ClientIDMode="Static"></asp:TextBox>
        </form>
</body>
</html>
