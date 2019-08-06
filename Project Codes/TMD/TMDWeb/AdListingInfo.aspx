<%@ Page Language="C#"  MasterPageFile="~/Template.Master" AutoEventWireup="true" CodeFile="AdListingInfo.aspx.cs" Inherits="targeted_marketing_display.AdListingInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        #imgLogo{
  float: left;
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
            
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <form runat="server">
<script>
        function deleteFunction() {

            if (!confirm('Confirm Deletion of Advertisement')) {

                return false;
            }

            else {
                return true;
            }
        }
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

         
       
                  <p>  <asp:image id="imgLogo" runat="server" imageurl="" style="width:700px;height:700px;margin-right:15px;" ClientIDMode="Static" ></asp:image>
                      <div id="vidDiv" runat="server">
                          <video clientidmode="static" id="videoThumbnail" width="250" height="250" runat="server" visible="true" controls class="img-fluid" alt="">
                              <source id="vidSource" runat="server" src="" type="video/mp4">
                          </video>
                      </div>

                      <div id="divText">
                 
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
                      </div>
                       
                      
          </p>  
              </div>







      </form>
</asp:Content>
