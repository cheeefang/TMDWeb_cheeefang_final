<%@ Page Language="C#" MasterPageFile="~/Template.Master" AutoEventWireup="true" CodeFile="BillboardAdInfo.aspx.cs" Inherits="targeted_marketing_display.BillboardAdInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form runat="server">       
            

          <div class="row">
                    <div class="col-lg-12">
                        <h1 class="page-header">Billboard Advertisement Listing<asp:label runat="server" ID="BillboardCodelabel"></asp:label></h1>
                        <asp:label runat="server" id="rowCountLabel" Font-Bold="true" text="Total:" /><asp:literal runat="server" id="rowcounttext"></asp:literal>
                       
                    </div>
                    <!-- /.col-lg-12 -->
                </div>

        <asp:label id="labelMap" runat="server" font-bold="true">Map</asp:label>
         <div id='map' style="width:300px;height:300px" align="center"></div>

    <script>
        mapboxgl.accessToken = 'pk.eyJ1IjoiY2hlZWVmYW5nIiwiYSI6ImNqeWdyd3ozeDAzejQzZGwzMjY0MzhzYzcifQ.BN7hdcRRbZT02s4h8QR-iw';
      
     
        var map = new mapboxgl.Map({
            container: 'map', // Container ID
            style: 'mapbox://styles/mapbox/streets-v11', // Map style to use
            
            center: ["<%= longtitude %>","<%= latitude %>"], // Starting position [lng, lat]
            zoom: 12, // Starting zoom level
            
       
 
    //map.getSource('single-point').setData(center);
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
 
    map.getSource('single-point').setData(e.result.geometry);
  });
});
    </script>

          <div class="row">
                    <div class="col-lg-6">
                   <div class="input-group custom-search-form" style="width: 50%">
                  <div style="padding: 20px; float: left; width:30%;">
                                          <p class="input-group" style="width:350px;margin-left:-20px;">
                                        <asp:TextBox ID="txtSearch" class="form-control" runat="server" placeholder="Search..."></asp:TextBox>
                                        <%--<input type="submit" id="btSubmit" runat="server" />--%>
                                        <span class="input-group-btn" >
                                            <asp:LinkButton runat="server" class="btn btn-default" ID="btnRun" style="height:34px;" Text="<i class='fa fa-search'></i>"/>
                                       </span>
                                            </p>
                                    </div>
                            </div>
               </div>
                
                </div>

        <asp:GridView ID="GridView1" runat="server" CssClass="table table-striped table-bordered table-hover"   CellPadding ="3" ForeColor="Black" GridLines="Vertical" Height="100%" Width="100%" 
            OnPreRender="GridView1_PreRender" AllowPaging="True"  OnPageIndexChanging="GridView1_PageIndexChanging" AutoGenerateColumns="False" DataKeyNames="BillboardCode" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" >
            <AlternatingRowStyle BackColor="#CCCCCC" />
            <Columns>
                                    
    
                                    <asp:ImageField DataImageUrlField="Item" HeaderText="Advertisement" ControlStyle-Width="100" ControlStyle-Height = "100" > 
        
                                   
<ControlStyle Height="100px" Width="100px"></ControlStyle>
                                    </asp:ImageField>
        
                                   
                                    <asp:BoundField DataField="BillboardCode" HeaderText="Billboard Code" sortExpression="BillboardCode" Visible="false" ></asp:BoundField>
                                    <asp:BoundField DataField="Name" HeaderText="Advert Name" SortExpression="Name"></asp:BoundField>
                                    
                                     
                                    <asp:BoundField DataField="ItemType" HeaderText="ItemType" SortExpression="ItemType"></asp:BoundField>
                             
                               
                                    <asp:BoundField DataField="StartDate" HeaderText="StartDate" SortExpression="StartDate" DataFormatString="{0:D}"></asp:BoundField>
                                    <asp:BoundField DataField="EndDate" HeaderText="EndDate" SortExpression="EndDate" DataFormatString="{0:D}"> </asp:BoundField>
                                   

            </Columns>
           <EditRowStyle HorizontalAlign="Center" CssClass="GridViewEditRow" BackColor="#999999" />
                                <EmptyDataRowStyle HorizontalAlign="Center" />
                                <FooterStyle BackColor="#CCCCCC" HorizontalAlign="Center" Font-Bold="True" />
                                <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                                <PagerStyle BackColor="#999999"  ForeColor="Black" HorizontalAlign="left"  />
                                <RowStyle Height="20px" Width="30px" HorizontalAlign="Center" BackColor="#F7F6F3" />
                                <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                                <SortedAscendingCellStyle BackColor="#F1F1F1" HorizontalAlign="Center" />
                                <SortedAscendingHeaderStyle BackColor="#808080" HorizontalAlign="Center" />
                                <SortedDescendingCellStyle BackColor="#CAC9C9" HorizontalAlign="Center" />
                                <SortedDescendingHeaderStyle BackColor="#383838" HorizontalAlign="Center" />
        </asp:GridView>
        
 <!--       <asp:objectdatasource id="ObjectDataSource1" runat="server" FilterExpression="Name LIKE '%{0}%' OR Item LIKE '%{0}%' OR Name1  LIKE '%{0}%' OR ItemType LIKE '%{0}%' OR convert(StartDate,'System.String') LIKE '%{0}%' OR convert(EndDate,'System.String') LIKE '%{0}%' ">
            <Filterparameters>
               <asp:ControlParameter ControlID="txtSearch" Name="Name" PropertyName="Text" />
               <asp:ControlParameter ControlID="txtSearch" Name="Item" PropertyName="Text" />                                            
               <asp:ControlParameter ControlID="txtSearch" Name="BillboardCode" PropertyName="Text" />
               <asp:ControlParameter ControlID="txtSearch" Name="ItemType" PropertyName="Text" />
               <asp:ControlParameter ControlID="txtSearch" Name="StartDate" PropertyName="Text" />
               <asp:ControlParameter ControlID="txtSearch" Name="EndDate" PropertyName="Text" />
            </Filterparameters>
        </asp:objectdatasource>
 -->
        
    
            


          <p>
 
        
       
            

          <asp:Label ID="ErrorMessage" runat="server" visible="false" Text="No Advertisements are to be displayed in this Billboard for now."></asp:Label>
 
        
       
            

          </p>
 
        <asp:Label ID="LabelPaging" style="color:darkslateblue" runat="server" Text="Label"></asp:Label>
       
            


    </form>
</asp:Content>
