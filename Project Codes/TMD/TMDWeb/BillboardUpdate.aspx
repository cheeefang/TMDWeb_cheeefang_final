<%@ Page Title="" Language="C#" MasterPageFile="~/Template.Master" AutoEventWireup="true" CodeFile="BillboardUpdate.aspx.cs" Inherits="targeted_marketing_display.BillboardUpdate" EnableViewState="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
       
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form runat="server">


    

            

            <div class="row">
                <div class="col-lg-12">
                    <h1 class="page-header">Update Billboard Location</h1>
                </div>
                <!-- /.col-lg-12 -->
            </div>
            <!-- /.row -->

     <div id='map' style="width:1550px;height:400px" align="center"></div>

    <script>
        mapboxgl.accessToken = 'pk.eyJ1IjoiY2hlZWVmYW5nIiwiYSI6ImNqeWdyd3ozeDAzejQzZGwzMjY0MzhzYzcifQ.BN7hdcRRbZT02s4h8QR-iw';
      

        var map = new mapboxgl.Map({
            container: 'map', // Container ID
            style: 'mapbox://styles/mapbox/streets-v11', // Map style to use
            center: ["<%= longtitude %>","<%= latitude %>"], // Starting position [lng, lat]
            zoom: 12, // Starting zoom level
        });
        var marker = new mapboxgl.Marker() // initialize a new marker
            .setLngLat(["<%= longtitude %>","<%= latitude %>"]) // Marker [lng, lat] coordinates
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

        var string = e.result.place_name,
            length = string.length,
            step = 50,
            array = [],
            i = 0,
            j;
        console.log(length);
        // j = string.lastIndexOf(" ", i + step);
        //run when i<address total length

        //find index of empty space after i+50;
        j = string.lastIndexOf(" ", i + step);
        console.log(j);
        //if cant find empty space after a certain index,return -1
        if (j === -1) {
            j = length;
        }
        //push value of addln1 first,then addln2
        array.push(string.slice(0, j));
        array.push(string.slice(j, length));


        if (array[1] == undefined) {
            array[1] = ' ';
        }
        console.log(array);
        var address = array[0];
        var address2 = array[1];
        var longtitude = e.result.geometry.coordinates[0];
        var latitude = e.result.geometry.coordinates[1];
        var pCode = e.result.context["0"].text;
        var City = e.result.context["3"].text;
        var Country = e.result.context["3"].short_code;
        var CapsCountry = Country.toUpperCase();
        console.log(e.result.geometry.coordinates);
        document.getElementById("BBLatitude").value = latitude;
        document.getElementById("BBLongtitude").value = longtitude;
        document.getElementById("BBPostalCode").value = pCode;
        document.getElementById("BBCity").value = City;
        document.getElementById("BBAddLn1").value = address;
        document.getElementById("BBAddLn2").value = address2;
        document.getElementById("BBCountry").value = CapsCountry;
    map.getSource('single-point').setData(e.result.geometry);
  });
});
    </script>  

            

                <div runat="server" class="alert alert-success" id="alertSuccess" visible="False">
                    <strong>Success!</strong> Location has been updated.
                </div>

                <div runat="server" class="alert alert-danger" id="alertWarning" visible="False">
                    <strong>Warning!</strong> Nothing was updated. 
                </div>

            
            <br />
         


            <div class="row">
               

                <div class="col-lg-6">

                   &nbsp;

                </div>
            </div>
            <div class="row">
                <div class="col-lg-6">

                    <div class="form-group">
                        <label>Address Line 1 </label>
                        
                        &nbsp;
                    <asp:TextBox class="form-control" ID="BBAddLn1" placeholder="Enter Address Line 1" runat="server" ClientIDMode="Static" MaxLength="50"></asp:TextBox>&nbsp;
                    </div>
                </div>

                <div class="col-lg-6">

                    <div class="form-group">
                        <label>Address Line 2(Optional)</label>
                        <asp:TextBox class="form-control" ID="BBAddLn2" placeholder="Enter Address Line 2" runat="server" ClientIDMode="Static" MaxLength="50"></asp:TextBox>
                        &nbsp;
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-lg-6">
                   <div class="form-group">

                        <label>City </label>
                        
                        <asp:TextBox class="form-control" ID="BBCity" placeholder="Enter Billboard City" runat="server" ClientIDMode="Static" MaxLength="20"></asp:TextBox>&nbsp;
                    </div>
                </div>

                <div class="col-lg-6">

                    <div class="form-group">
                        <label>Country </label>
                        
                        <asp:DropDownList Class="form-control" ID="BBCountry" runat="server" ClientIDMode="Static">
                        </asp:DropDownList>&nbsp;

                    </div>
                    
                </div>

            </div>


        <div class="row">
            <div class="col-lg-6">
                <div class="form-group">
                    <label>Latitude</label>

                    <asp:TextBox class="form-control" ID="BBLatitude" placeholder="Enter Billboard Latitude" runat="server" ClientIDMode="Static" MaxLength="50"></asp:TextBox>&nbsp;
                </div>
                

            </div>

            <div class="col-lg-6">

                <div class="form-group">
                    <label>Longtitude </label>

                    <asp:TextBox class="form-control" ID="BBLongtitude" placeholder="Enter Billboard Longtitude" runat="server" ClientIDMode="Static" MaxLength="50"></asp:TextBox>&nbsp;
                </div>
                



            </div>             

            </div>


            <div class="row">
                <div class="col-lg-6">

                    <div class="form-group">
                        <label>Postal Code </label>
                        
                        <asp:TextBox class="form-control" ID="BBPostalCode" placeholder="Enter Billboard Postal Code" runat="server" ClientIDMode="Static" MaxLength="12"></asp:TextBox>&nbsp;
                    </div>

                </div>

               
            </div>

            <div class="row">
                <div class="col-lg-12">

                    <asp:Button ID="updateBtn" class="btn btn-primary nextBtn pull-right" runat="server" Text="Update" OnClick="updateBtn_Click" />


                </div>
            </div>

    

    </form>


</asp:Content>
