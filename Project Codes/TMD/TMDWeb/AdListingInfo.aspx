<%@ Page Language="C#"  MasterPageFile="~/Template.Master" AutoEventWireup="true" CodeFile="AdListingInfo.aspx.cs" Inherits="targeted_marketing_display.AdListingInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
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
                    <h1 class="page-header">Advertisement Listing info <asp:Label ID="AdNameLabel" runat="server"></asp:Label></h1>
                </div>
                <!-- /.col-lg-12 -->
            </div>
            <!-- /.row -->
        
                <br />
              
           
         <div class="card">
        <div class="row no-gutters">
            <div class="col-auto">
                        
                    <asp:image id="imgLogo" runat="server" ImageUrl="" Width="250" Height="250" visible="false" ClientIDMode="static" class="img-fluid" alt=""></asp:image>
                    <div id="vidDiv" runat="server">
                     <video ClientIDMode="static" id="videoThumbnail" width="250" height="250" runat="server" visible="true" controls class="img-fluid" alt="">  
                                            <source id="vidSource" runat="server" src="" type="video/mp4">  
                                        </video>  
                        </div>
                  
                    </div>
                    <div class="col">
                                        <div class="card-block px-2">
                        <asp:label id="CompanyNameLabel"  runat="server" Font-Bold="true" class="card-title" text="Company:" />
                           <asp:literal runat="server" id="CompanyNameLiteral" ></asp:literal>
                        <br />
                         <asp:label id="AdName2"  runat="server" Font-Bold="true" text="Advertisement Name:" class="card-title"  />
                    <asp:literal runat="server" id="AdNameLiteral"></asp:literal>
                        <br />
                          <asp:label id="ItemTypeLabel"  runat="server" Font-Bold="true" text="File Type:" class="card-title"  />
                    <asp:literal runat="server" id="ItemTypeLiteral"></asp:literal>
                        <br />
                              <asp:label id="StartDateLabel"  runat="server" text="Start Date:" Font-Bold="true" class="card-title" />
                    <asp:literal runat="server" id="StartDateLiteral"></asp:literal>
                        <br />
                          <asp:label id="EndDateLabel"  runat="server" text="End Date:" Font-Bold="true" class="card-title"  />
                    <asp:literal runat="server" id="EndDateLiteral"></asp:literal>
                        <br />
                             <asp:label id="AudienceLabel"  runat="server" text="Targeted Audience(s):" Font-Bold="true" class="card-title"  />
                      <ul runat="server" id="AudienceList" align="center"> </ul>
                        <br />
                                                  <asp:label id="CategoryLabel"  runat="server" text="Advertisement Category(s):" Font-Bold="true" class="card-title"  />
                    <ul runat="server" id="CategoryList"></ul>
                        <br />
                         <asp:label id="LocationLabel"  runat="server" text="Connected Billboard(s):" Font-Bold="true" class="card-title"  />
                    <ul runat="server" id="BBCodeList"></ul>
                        <br />
                    </div>
                        </div>
                </div>
                <div class="card-footer">
                    <small class="text-muted">Targeted Marketing Display</small>
                </div>
            </div>
          
      

    
    

     
          
          
    </form>
</asp:Content>
