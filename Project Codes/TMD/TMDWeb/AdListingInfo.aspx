﻿<%@ Page Language="C#"  MasterPageFile="~/Template.Master" AutoEventWireup="true" CodeFile="AdListingInfo.aspx.cs" Inherits="targeted_marketing_display.AdListingInfo" %>

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
              
           

  <div class="row">
            <div class="col-lg-6">
                <div class="form-group">
                    <asp:label  runat="server"><b>Advertisement Image </b></asp:label>
                    <br />
                   
                    
                    <asp:image id="imgLogo" runat="server" ImageUrl="" Width="200" Height="200"></asp:image>
                       
                </div>
            </div>
        </div>

           <div class="row">
            <div class="col-lg-6">
                <div class="form-group">
                    <asp:label id="AdName2"  runat="server"> </asp:label>
                    
                </div>
            </div>
        </div>

           <div class="row">
            <div class="col-lg-6">
                <div class="form-group">
                    <asp:label id="ItemTypeLabel"  runat="server"> </asp:label>
                    
                </div>
            </div>
        </div>

           <div class="row">
            <div class="col-lg-6">
                <div class="form-group">
                    <asp:label id="StartDateLabel"  runat="server"> </asp:label>
                    
                </div>
            </div>
        </div>
           <div class="row">
            <div class="col-lg-6">
                <div class="form-group">
                    <asp:label id="EndDateLabel"  runat="server"> </asp:label>
                    
                </div>
            </div>
        </div>

          <div class="row">
            <div class="col-lg-6">
                <div class="form-group">
                    <asp:label id="AudienceLabel"  runat="server" text="Targeted Audience:"> </asp:label>
                    
                </div>
            </div>
        </div>
          
          
    </form>
</asp:Content>
