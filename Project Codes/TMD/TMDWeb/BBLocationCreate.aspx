﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Template.Master" AutoEventWireup="true" CodeFile="BBLocationCreate.aspx.cs" Inherits="targeted_marketing_display.BBLocationCreate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
       
    </style>
    <script type="text/javascript">
       //google.maps.event.addDomListener(window, 'load', initialize);
       // function initialize() {
       //     var autocomplete = new google.maps.places.Autocomplete(document.getElementById('LocationTB'));
       //     google.maps.event.addListerner(autocomplete, 'place_changed', function () {
       //         var place = autocomplete.getPlace();
       //         var location = "<b>Address</b>:" + place.formatted_address + "<br/>";
       //         location += "<b>Latitude</b>:" + place.geometry.location.A + "<br/>";
       //         location += "<b>Longtitude</b>:" + place.geometry.location.F;
       //         document.getElementById('lblResult').innerHTML = location
       //     });
       // }
            
        
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <form runat="server">
    

            

            <div class="row">
                <div class="col-lg-12">
                    <h1 class="page-header">New Billboard Location</h1>
                </div>
                <!-- /.col-lg-12 -->
            </div>
            <!-- /.row -->

       <div id='map' style="width:300px;height:300px" align="center"></div>

    <script>
        mapboxgl.accessToken = 'pk.eyJ1IjoiY2hlZWVmYW5nIiwiYSI6ImNqeWdyd3ozeDAzejQzZGwzMjY0MzhzYzcifQ.BN7hdcRRbZT02s4h8QR-iw';
      

        var map = new mapboxgl.Map({
            container: 'map', // Container ID
            style: 'mapbox://styles/mapbox/streets-v11', // Map style to use
            center: [103.819839,1.352083], // Starting position [lng, lat]
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
        var string = e.result.place_name,
            length = string.length,
            step = 50,
            array = [],
            i = 0,
            j;

        while (i < length) {
            j = string.indexOf(" ", i + step);
            if (j === -1) {
                j = length;
            }
    
            array.push(string.slice(i, j));
            i = j;
        }
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

              <center> 
               <asp:label ID="testing123" runat="server"></asp:label>
                  <div runat="server" class="alert alert-success" id="alertSuccess" visible="False">
                    <strong>Success!</strong> New Location has been created.
                </div> 

                <div runat="server" class="alert alert-danger" id="alertWarning" visible="False">
                    <strong>Warning!</strong>
                    <asp:Label ID="warningLocation" runat="server"></asp:Label>
                </div>
                  <div runat="server" class="alert alert-danger" id="alertDanger" visible="False">
                    <strong>Danger!</strong>
                    <asp:Label ID="dangerLocation" runat="server"></asp:Label>
                </div>
                  
              </center>
            
           

            <br />


            <div class="row">
                <div class="col-lg-6">

                    <div class="form-group">
                        <label>Billboard Code </label>
                        <label style="color: red">*</label>
                        <asp:TextBox class="form-control" ID="BBLocationCode" placeholder="Enter Location Code" runat="server" ClientIDMode="Static" MaxLength="10"></asp:TextBox>&nbsp;
                    </div>

                </div>
            
            </div>

            <div class="row">
                <div class="col-lg-6">

                    <div class="form-group">
                        <label>Address Line 1 </label>
                        <label style="color: red">*</label>
                        &nbsp;
                    <asp:TextBox class="form-control" ID="BBAddLn1" placeholder="Enter Address Line 1" runat="server" ClientIDMode="Static"></asp:TextBox>&nbsp;
                    </div>
                </div>

                <div class="col-lg-6">

                    <div class="form-group">
                        <label>Address Line 2(Optional)</label>
                      
                        <asp:TextBox class="form-control" ID="BBAddLn2" placeholder="Enter Address Line 2" runat="server" ClientIDMode="Static"></asp:TextBox>
                        &nbsp;
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-lg-6">
                   <div class="form-group">

                        <label>City </label>
                        <label style="color: red">*</label>
                        <asp:TextBox class="form-control" ID="BBCity" placeholder="Enter City" runat="server" ClientIDMode="Static"></asp:TextBox>&nbsp;
                    </div>
                </div>

                <div class="col-lg-6">

                    <div class="form-group">
                        <label>Country </label>
                        <label style="color: red">*</label>
                        <asp:DropDownList Class="form-control" ID="BBCountry" runat="server" ClientIDMode="Static">

                            <asp:ListItem Value="">Select Country</asp:ListItem>
                            <asp:ListItem Value="AF">Afghanistan</asp:ListItem>
                            <asp:ListItem Value="AX">Åland Islands</asp:ListItem>
                            <asp:ListItem Value="AL">Albania</asp:ListItem>
                            <asp:ListItem Value="DZ">Algeria</asp:ListItem>
                            <asp:ListItem Value="AS">American Samoa</asp:ListItem>
                            <asp:ListItem Value="AD">Andorra</asp:ListItem>
                            <asp:ListItem Value="AO">Angola</asp:ListItem>
                            <asp:ListItem Value="AI">Anguilla</asp:ListItem>
                            <asp:ListItem Value="AQ">Antarctica</asp:ListItem>
                            <asp:ListItem Value="AG">Antigua and Barbuda</asp:ListItem>
                            <asp:ListItem Value="AR">Argentina</asp:ListItem>
                            <asp:ListItem Value="AM">Armenia</asp:ListItem>
                            <asp:ListItem Value="AW">Aruba</asp:ListItem>
                            <asp:ListItem Value="AU">Australia</asp:ListItem>
                            <asp:ListItem Value="AT">Austria</asp:ListItem>
                            <asp:ListItem Value="AZ">Azerbaijan</asp:ListItem>
                            <asp:ListItem Value="BS">Bahamas</asp:ListItem>
                            <asp:ListItem Value="BH">Bahrain</asp:ListItem>
                            <asp:ListItem Value="BD">Bangladesh</asp:ListItem>
                            <asp:ListItem Value="BB">Barbados</asp:ListItem>
                            <asp:ListItem Value="BY">Belarus</asp:ListItem>
                            <asp:ListItem Value="BE">Belgium</asp:ListItem>
                            <asp:ListItem Value="BZ">Belize</asp:ListItem>
                            <asp:ListItem Value="BJ">Benin</asp:ListItem>
                            <asp:ListItem Value="BM">Bermuda</asp:ListItem>
                            <asp:ListItem Value="BT">Bhutan</asp:ListItem>
                            <asp:ListItem Value="BO">Bolivia, Plurinational State of</asp:ListItem>
                            <asp:ListItem Value="BQ">Bonaire, Sint Eustatius and Saba</asp:ListItem>
                            <asp:ListItem Value="BA">Bosnia and Herzegovina</asp:ListItem>
                            <asp:ListItem Value="BW">Botswana</asp:ListItem>
                            <asp:ListItem Value="BV">Bouvet Island</asp:ListItem>
                            <asp:ListItem Value="BR">Brazil</asp:ListItem>
                            <asp:ListItem Value="IO">British Indian Ocean Territory</asp:ListItem>
                            <asp:ListItem Value="BN">Brunei Darussalam</asp:ListItem>
                            <asp:ListItem Value="BG">Bulgaria</asp:ListItem>
                            <asp:ListItem Value="BF">Burkina Faso</asp:ListItem>
                            <asp:ListItem Value="BI">Burundi</asp:ListItem>
                            <asp:ListItem Value="KH">Cambodia</asp:ListItem>
                            <asp:ListItem Value="CM">Cameroon</asp:ListItem>
                            <asp:ListItem Value="CA">Canada</asp:ListItem>
                            <asp:ListItem Value="CV">Cape Verde</asp:ListItem>
                            <asp:ListItem Value="KY">Cayman Islands</asp:ListItem>
                            <asp:ListItem Value="CF">Central African Republic</asp:ListItem>
                            <asp:ListItem Value="TD">Chad</asp:ListItem>
                            <asp:ListItem Value="CL">Chile</asp:ListItem>
                            <asp:ListItem Value="CN">China</asp:ListItem>
                            <asp:ListItem Value="CX">Christmas Island</asp:ListItem>
                            <asp:ListItem Value="CC">Cocos (Keeling) Islands</asp:ListItem>
                            <asp:ListItem Value="CO">Colombia</asp:ListItem>
                            <asp:ListItem Value="KM">Comoros</asp:ListItem>
                            <asp:ListItem Value="CG">Congo</asp:ListItem>
                            <asp:ListItem Value="CD">Congo, the Democratic Republic of the</asp:ListItem>
                            <asp:ListItem Value="CK">Cook Islands</asp:ListItem>
                            <asp:ListItem Value="CR">Costa Rica</asp:ListItem>
                            <asp:ListItem Value="CI">Côte d'Ivoire</asp:ListItem>
                            <asp:ListItem Value="HR">Croatia</asp:ListItem>
                            <asp:ListItem Value="CU">Cuba</asp:ListItem>
                            <asp:ListItem Value="CW">Curaçao</asp:ListItem>
                            <asp:ListItem Value="CY">Cyprus</asp:ListItem>
                            <asp:ListItem Value="CZ">Czech Republic</asp:ListItem>
                            <asp:ListItem Value="DK">Denmark</asp:ListItem>
                            <asp:ListItem Value="DJ">Djibouti</asp:ListItem>
                            <asp:ListItem Value="DM">Dominica</asp:ListItem>
                            <asp:ListItem Value="DO">Dominican Republic</asp:ListItem>
                            <asp:ListItem Value="EC">Ecuador</asp:ListItem>
                            <asp:ListItem Value="EG">Egypt</asp:ListItem>
                            <asp:ListItem Value="SV">El Salvador</asp:ListItem>
                            <asp:ListItem Value="GQ">Equatorial Guinea</asp:ListItem>
                            <asp:ListItem Value="ER">Eritrea</asp:ListItem>
                            <asp:ListItem Value="EE">Estonia</asp:ListItem>
                            <asp:ListItem Value="ET">Ethiopia</asp:ListItem>
                            <asp:ListItem Value="FK">Falkland Islands (Malvinas)</asp:ListItem>
                            <asp:ListItem Value="FO">Faroe Islands</asp:ListItem>
                            <asp:ListItem Value="FJ">Fiji</asp:ListItem>
                            <asp:ListItem Value="FI">Finland</asp:ListItem>
                            <asp:ListItem Value="FR">France</asp:ListItem>
                            <asp:ListItem Value="GF">French Guiana</asp:ListItem>
                            <asp:ListItem Value="PF">French Polynesia</asp:ListItem>
                            <asp:ListItem Value="TF">French Southern Territories</asp:ListItem>
                            <asp:ListItem Value="GA">Gabon</asp:ListItem>
                            <asp:ListItem Value="GM">Gambia</asp:ListItem>
                            <asp:ListItem Value="GE">Georgia</asp:ListItem>
                            <asp:ListItem Value="DE">Germany</asp:ListItem>
                            <asp:ListItem Value="GH">Ghana</asp:ListItem>
                            <asp:ListItem Value="GI">Gibraltar</asp:ListItem>
                            <asp:ListItem Value="GR">Greece</asp:ListItem>
                            <asp:ListItem Value="GL">Greenland</asp:ListItem>
                            <asp:ListItem Value="GD">Grenada</asp:ListItem>
                            <asp:ListItem Value="GP">Guadeloupe</asp:ListItem>
                            <asp:ListItem Value="GU">Guam</asp:ListItem>
                            <asp:ListItem Value="GT">Guatemala</asp:ListItem>
                            <asp:ListItem Value="GG">Guernsey</asp:ListItem>
                            <asp:ListItem Value="GN">Guinea</asp:ListItem>
                            <asp:ListItem Value="GW">Guinea-Bissau</asp:ListItem>
                            <asp:ListItem Value="GY">Guyana</asp:ListItem>
                            <asp:ListItem Value="HT">Haiti</asp:ListItem>
                            <asp:ListItem Value="HM">Heard Island and McDonald Islands</asp:ListItem>
                            <asp:ListItem Value="VA">Holy See (Vatican City State)</asp:ListItem>
                            <asp:ListItem Value="HN">Honduras</asp:ListItem>
                            <asp:ListItem Value="HK">Hong Kong</asp:ListItem>
                            <asp:ListItem Value="HU">Hungary</asp:ListItem>
                            <asp:ListItem Value="IS">Iceland</asp:ListItem>
                            <asp:ListItem Value="IN">India</asp:ListItem>
                            <asp:ListItem Value="ID">Indonesia</asp:ListItem>
                            <asp:ListItem Value="IR">Iran, Islamic Republic of</asp:ListItem>
                            <asp:ListItem Value="IQ">Iraq</asp:ListItem>
                            <asp:ListItem Value="IE">Ireland</asp:ListItem>
                            <asp:ListItem Value="IM">Isle of Man</asp:ListItem>
                            <asp:ListItem Value="IL">Israel</asp:ListItem>
                            <asp:ListItem Value="IT">Italy</asp:ListItem>
                            <asp:ListItem Value="JM">Jamaica</asp:ListItem>
                            <asp:ListItem Value="JP">Japan</asp:ListItem>
                            <asp:ListItem Value="JE">Jersey</asp:ListItem>
                            <asp:ListItem Value="JO">Jordan</asp:ListItem>
                            <asp:ListItem Value="KZ">Kazakhstan</asp:ListItem>
                            <asp:ListItem Value="KE">Kenya</asp:ListItem>
                            <asp:ListItem Value="KI">Kiribati</asp:ListItem>
                            <asp:ListItem Value="KP">Korea, Democratic People's Republic of</asp:ListItem>
                            <asp:ListItem Value="KR">Korea, Republic of</asp:ListItem>
                            <asp:ListItem Value="KW">Kuwait</asp:ListItem>
                            <asp:ListItem Value="KG">Kyrgyzstan</asp:ListItem>
                            <asp:ListItem Value="LA">Lao People's Democratic Republic</asp:ListItem>
                            <asp:ListItem Value="LV">Latvia</asp:ListItem>
                            <asp:ListItem Value="LB">Lebanon</asp:ListItem>
                            <asp:ListItem Value="LS">Lesotho</asp:ListItem>
                            <asp:ListItem Value="LR">Liberia</asp:ListItem>
                            <asp:ListItem Value="LY">Libya</asp:ListItem>
                            <asp:ListItem Value="LI">Liechtenstein</asp:ListItem>
                            <asp:ListItem Value="LT">Lithuania</asp:ListItem>
                            <asp:ListItem Value="LU">Luxembourg</asp:ListItem>
                            <asp:ListItem Value="MO">Macao</asp:ListItem>
                            <asp:ListItem Value="MK">Macedonia, the former Yugoslav Republic of</asp:ListItem>
                            <asp:ListItem Value="MG">Madagascar</asp:ListItem>
                            <asp:ListItem Value="MW">Malawi</asp:ListItem>
                            <asp:ListItem Value="MY">Malaysia</asp:ListItem>
                            <asp:ListItem Value="MV">Maldives</asp:ListItem>
                            <asp:ListItem Value="ML">Mali</asp:ListItem>
                            <asp:ListItem Value="MT">Malta</asp:ListItem>
                            <asp:ListItem Value="MH">Marshall Islands</asp:ListItem>
                            <asp:ListItem Value="MQ">Martinique</asp:ListItem>
                            <asp:ListItem Value="MR">Mauritania</asp:ListItem>
                            <asp:ListItem Value="MU">Mauritius</asp:ListItem>
                            <asp:ListItem Value="YT">Mayotte</asp:ListItem>
                            <asp:ListItem Value="MX">Mexico</asp:ListItem>
                            <asp:ListItem Value="FM">Micronesia, Federated States of</asp:ListItem>
                            <asp:ListItem Value="MD">Moldova, Republic of</asp:ListItem>
                            <asp:ListItem Value="MC">Monaco</asp:ListItem>
                            <asp:ListItem Value="MN">Mongolia</asp:ListItem>
                            <asp:ListItem Value="ME">Montenegro</asp:ListItem>
                            <asp:ListItem Value="MS">Montserrat</asp:ListItem>
                            <asp:ListItem Value="MA">Morocco</asp:ListItem>
                            <asp:ListItem Value="MZ">Mozambique</asp:ListItem>
                            <asp:ListItem Value="MM">Myanmar</asp:ListItem>
                            <asp:ListItem Value="NA">Namibia</asp:ListItem>
                            <asp:ListItem Value="NR">Nauru</asp:ListItem>
                            <asp:ListItem Value="NP">Nepal</asp:ListItem>
                            <asp:ListItem Value="NL">Netherlands</asp:ListItem>
                            <asp:ListItem Value="NC">New Caledonia</asp:ListItem>
                            <asp:ListItem Value="NZ">New Zealand</asp:ListItem>
                            <asp:ListItem Value="NI">Nicaragua</asp:ListItem>
                            <asp:ListItem Value="NE">Niger</asp:ListItem>
                            <asp:ListItem Value="NG">Nigeria</asp:ListItem>
                            <asp:ListItem Value="NU">Niue</asp:ListItem>
                            <asp:ListItem Value="NF">Norfolk Island</asp:ListItem>
                            <asp:ListItem Value="MP">Northern Mariana Islands</asp:ListItem>
                            <asp:ListItem Value="NO">Norway</asp:ListItem>
                            <asp:ListItem Value="OM">Oman</asp:ListItem>
                            <asp:ListItem Value="PK">Pakistan</asp:ListItem>
                            <asp:ListItem Value="PW">Palau</asp:ListItem>
                            <asp:ListItem Value="PS">Palestinian Territory, Occupied</asp:ListItem>
                            <asp:ListItem Value="PA">Panama</asp:ListItem>
                            <asp:ListItem Value="PG">Papua New Guinea</asp:ListItem>
                            <asp:ListItem Value="PY">Paraguay</asp:ListItem>
                            <asp:ListItem Value="PE">Peru</asp:ListItem>
                            <asp:ListItem Value="PH">Philippines</asp:ListItem>
                            <asp:ListItem Value="PN">Pitcairn</asp:ListItem>
                            <asp:ListItem Value="PL">Poland</asp:ListItem>
                            <asp:ListItem Value="PT">Portugal</asp:ListItem>
                            <asp:ListItem Value="PR">Puerto Rico</asp:ListItem>
                            <asp:ListItem Value="QA">Qatar</asp:ListItem>
                            <asp:ListItem Value="RE">Réunion</asp:ListItem>
                            <asp:ListItem Value="RO">Romania</asp:ListItem>
                            <asp:ListItem Value="RU">Russian Federation</asp:ListItem>
                            <asp:ListItem Value="RW">Rwanda</asp:ListItem>
                            <asp:ListItem Value="BL">Saint Barthélemy</asp:ListItem>
                            <asp:ListItem Value="SH">Saint Helena, Ascension and Tristan da Cunha</asp:ListItem>
                            <asp:ListItem Value="KN">Saint Kitts and Nevis</asp:ListItem>
                            <asp:ListItem Value="LC">Saint Lucia</asp:ListItem>
                            <asp:ListItem Value="MF">Saint Martin (French part)</asp:ListItem>
                            <asp:ListItem Value="PM">Saint Pierre and Miquelon</asp:ListItem>
                            <asp:ListItem Value="VC">Saint Vincent and the Grenadines</asp:ListItem>
                            <asp:ListItem Value="WS">Samoa</asp:ListItem>
                            <asp:ListItem Value="SM">San Marino</asp:ListItem>
                            <asp:ListItem Value="ST">Sao Tome and Principe</asp:ListItem>
                            <asp:ListItem Value="SA">Saudi Arabia</asp:ListItem>
                            <asp:ListItem Value="SN">Senegal</asp:ListItem>
                            <asp:ListItem Value="RS">Serbia</asp:ListItem>
                            <asp:ListItem Value="SC">Seychelles</asp:ListItem>
                            <asp:ListItem Value="SL">Sierra Leone</asp:ListItem>
                            <asp:ListItem Value="SG">Singapore</asp:ListItem>
                            <asp:ListItem Value="SX">Sint Maarten (Dutch part)</asp:ListItem>
                            <asp:ListItem Value="SK">Slovakia</asp:ListItem>
                            <asp:ListItem Value="SI">Slovenia</asp:ListItem>
                            <asp:ListItem Value="SB">Solomon Islands</asp:ListItem>
                            <asp:ListItem Value="SO">Somalia</asp:ListItem>
                            <asp:ListItem Value="ZA">South Africa</asp:ListItem>
                            <asp:ListItem Value="GS">South Georgia and the South Sandwich Islands</asp:ListItem>
                            <asp:ListItem Value="SS">South Sudan</asp:ListItem>
                            <asp:ListItem Value="ES">Spain</asp:ListItem>
                            <asp:ListItem Value="LK">Sri Lanka</asp:ListItem>
                            <asp:ListItem Value="SD">Sudan</asp:ListItem>
                            <asp:ListItem Value="SR">Suriname</asp:ListItem>
                            <asp:ListItem Value="SJ">Svalbard and Jan Mayen</asp:ListItem>
                            <asp:ListItem Value="SZ">Swaziland</asp:ListItem>
                            <asp:ListItem Value="SE">Sweden</asp:ListItem>
                            <asp:ListItem Value="CH">Switzerland</asp:ListItem>
                            <asp:ListItem Value="SY">Syrian Arab Republic</asp:ListItem>
                            <asp:ListItem Value="TW">Taiwan, Province of China</asp:ListItem>
                            <asp:ListItem Value="TJ">Tajikistan</asp:ListItem>
                            <asp:ListItem Value="TZ">Tanzania, United Republic of</asp:ListItem>
                            <asp:ListItem Value="TH">Thailand</asp:ListItem>
                            <asp:ListItem Value="TL">Timor-Leste</asp:ListItem>
                            <asp:ListItem Value="TG">Togo</asp:ListItem>
                            <asp:ListItem Value="TK">Tokelau</asp:ListItem>
                            <asp:ListItem Value="TO">Tonga</asp:ListItem>
                            <asp:ListItem Value="TT">Trinidad and Tobago</asp:ListItem>
                            <asp:ListItem Value="TN">Tunisia</asp:ListItem>
                            <asp:ListItem Value="TR">Turkey</asp:ListItem>
                            <asp:ListItem Value="TM">Turkmenistan</asp:ListItem>
                            <asp:ListItem Value="TC">Turks and Caicos Islands</asp:ListItem>
                            <asp:ListItem Value="TV">Tuvalu</asp:ListItem>
                            <asp:ListItem Value="UG">Uganda</asp:ListItem>
                            <asp:ListItem Value="UA">Ukraine</asp:ListItem>
                            <asp:ListItem Value="AE">United Arab Emirates</asp:ListItem>
                            <asp:ListItem Value="GB">United Kingdom</asp:ListItem>
                            <asp:ListItem Value="US">United States</asp:ListItem>
                            <asp:ListItem Value="UM">United States Minor Outlying Islands</asp:ListItem>
                            <asp:ListItem Value="UY">Uruguay</asp:ListItem>
                            <asp:ListItem Value="UZ">Uzbekistan</asp:ListItem>
                            <asp:ListItem Value="VU">Vanuatu</asp:ListItem>
                            <asp:ListItem Value="VE">Venezuela, Bolivarian Republic of</asp:ListItem>
                            <asp:ListItem Value="VN">Viet Nam</asp:ListItem>
                            <asp:ListItem Value="VG">Virgin Islands, British</asp:ListItem>
                            <asp:ListItem Value="VI">Virgin Islands, U.S.</asp:ListItem>
                            <asp:ListItem Value="WF">Wallis and Futuna</asp:ListItem>
                            <asp:ListItem Value="EH">Western Sahara</asp:ListItem>
                            <asp:ListItem Value="YE">Yemen</asp:ListItem>
                            <asp:ListItem Value="ZM">Zambia</asp:ListItem>
                            <asp:ListItem Value="ZW">Zimbabwe</asp:ListItem>
                        </asp:DropDownList>&nbsp;

                    </div>
                    
                </div>

            </div>


            <div class="row">
                <div class="col-lg-6">
                     <div class="form-group">
                        <label>Latitude </label>
                        <label style="color: red">*</label>
                        <asp:TextBox class="form-control" ID="BBLatitude" placeholder="Enter Latitude" runat="server" ClientIDMode="Static"></asp:TextBox>&nbsp;
                    </div>
                     
                   

                </div>
                <div class="col-lg-6">

                   <div class="form-group">
                        <label>Longtitude </label>
                        <label style="color: red">*</label>
                        <asp:TextBox class="form-control" ID="BBLongtitude" placeholder="Enter Longtitude" runat="server" ClientIDMode="Static"></asp:TextBox>&nbsp;
                    </div>

                </div>
                </div>
                  <div class="row">
                <div class="col-lg-6">

                    <div class="form-group">
                        <label>Postal Code </label>
                        <label style="color: red">*</label>
                        <asp:TextBox class="form-control" ID="BBPostalCode" placeholder="Enter Postal Code" runat="server" ClientIDMode="Static"></asp:TextBox>&nbsp;
                    </div>

                </div>
            </div>

            <div class="row">
                <div class="col-lg-12">

                    <asp:Button ID="btnSubmit" class="btn btn-primary nextBtn pull-right" runat="server"   Font-Bold="true" Text="Submit" OnClick="SubmitBtn_Click" />


                </div>
            </div>

   

    </form>
</asp:Content>

