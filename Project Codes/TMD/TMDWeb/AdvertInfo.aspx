<%@ Page Language="C#"  MasterPageFile="~/Template.Master" AutoEventWireup="true" CodeFile="AdvertInfo.aspx.cs" Inherits="targeted_marketing_display.AdvertInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        #imgLogo{
  float: left;
}
        #videoThumbnail{
            float:left;
        }
      .GridViewEditRow input[type=text] {
            width: 90px;
        }
        /* size textboxes */
        .GridViewEditRow select {
            width: 90px;
        }
        /* size drop down lists */

           .hiddencol
  {
    display: none;
  }
           #videoThumbnail{
                 object-fit: cover;
           }
            #vidDiv{
       
      }
       #divText{
           overflow:auto;
       }
       div.a {
  border: 1px solid black;
  
}
       .w3-container{
  padding: 0px;

}
       #imgLogo{
             border: 1px solid black;
       }
     
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <form runat="server">
<script>
     
</script>



        
     
          <!--button-->
          <div class="row">
              <div class="col-lg-12">
                  <h1 class="page-header">Advertisement Listing info
                      <asp:label id="AdNameLabel" runat="server"></asp:label>
                  </h1>
              </div>
              <!-- /.col-lg-12 -->
          </div>
          <!-- /.row -->

          <br />
          <div class="a">
         <div class="w3-container w3-dark-grey">
  <h1 id="headerName" runat="server" style="font-size:25px;padding-left:10px"></h1>
</div>
       <div class="w3-container">
                 <asp:image id="imgLogo" runat="server" imageurl="" style="width:800px;height:700px;margin-right:15px;" ClientIDMode="Static"  ></asp:image>
                      <div id="vidDiv" runat="server">
                          <video clientidmode="static" id="videoThumbnail" style="width:800px;height:700px;margin-right:15px;" runat="server" visible="true" controls class="img-fluid" alt="">
                              <source id="vidSource" runat="server" src="" type="video/mp4">
                          </video>
                      </div>
           
                      <div id="divText" style="padding-top:20px">
                 <p> 
                      <asp:label id="CompanyNameLabel" runat="server" font-bold="true" class="card-title" text="Company:" />
                      <asp:literal runat="server" id="CompanyNameLiteral"></asp:literal>
                
                       <br />

                      <asp:label id="AdName2" runat="server" font-bold="true" text="Advertisement Name:" class="card-title" />
                      <asp:literal runat="server" id="AdNameLiteral"></asp:literal>
                   
                       <br />

                      <asp:label id="ItemTypeLabel" runat="server" font-bold="true" text="File Type:" class="card-title" />
                      <asp:literal runat="server" id="ItemTypeLiteral"></asp:literal>
                    
                      <br />

                      <asp:label id="StartDateLabel" runat="server" text="Start Date:" font-bold="true" class="card-title" />
                      <asp:literal runat="server" id="StartDateLiteral"></asp:literal>
                  
                       <br />
        
                      <asp:label id="EndDateLabel" runat="server" text="End Date:" font-bold="true" class="card-title" />
                      <asp:literal runat="server" id="EndDateLiteral"></asp:literal>
                       
                      <br />

                      <asp:label id="AudienceLabel" runat="server" text="Targeted Audience(s):" font-bold="true"  />
                      <ul runat="server" id="AudienceList" ></ul>
                      
                      <br /> 


                      <asp:label id="CategoryLabel" runat="server" text="Advertisement Category(s):" font-bold="true" class="card-title" />
                      <ul runat="server" id="CategoryList"></ul>
                 
                       <br />
         
                      <asp:label id="LocationLabel" runat="server" text="Connected Billboard(s):" font-bold="true" class="card-title" />
                      <ul runat="server" id="BBCodeList"></ul>
                     </p>
                     </div>
                    
                       
                      
         
              </div>
           <div class="w3-container w3-dark-grey">
               <h1 id="footer" style="font-size:25px;padding-left:10px">Targeted Marketing Display &#169;</h1>
               </div>
       </div>

          <br />
          <br />



      </form>
</asp:Content>
