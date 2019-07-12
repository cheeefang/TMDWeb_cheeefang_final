<%@ Page Language="C#"  MasterPageFile="~/Template.Master" AutoEventWireup="true" CodeFile="AdListingUpdate.aspx.cs" Inherits="targeted_marketing_display.AdListingUpdate" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.4.0/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.0/js/bootstrap.min.js"></script>
    <script src="jquery-1.4.3.js" type="text/javascript"></script>

   




    <style>
        .modal-dialog {
            padding: 70px 0;
            text-align: center;
        }

         .hiddencol
  {
    display: none;
  }
      
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form runat="server">
 
            <div class="container" style="height: 100%">            
                <div runat="server" class="alert alert-danger" id="alertWarning" visible="False" style="margin-top: 10px;">
                    
                    <strong>Warning!</strong>
                    <asp:Label ID="warningLocation" runat="server"></asp:Label>
                </div>
            </div>
        <div runat="server" class="alert alert-success" id="alertSuccess" visible="False">
                    <strong>Success!</strong> Advertisement Date has been updated.
                </div>

            <div class="row">
                <div class="col-lg-12">
                    <h1 class="page-header">Update Advertisement</h1>
                </div>
                <!-- /.col-lg-12 -->
            </div>
            <!-- /.row -->
            <div class="row">
                <div class="col-lg-6">
                    <div class="form-group">
                        <label>From </label>
                        <label style="color: red">*</label>
                        <asp:TextBox Class="form-control" ID="startDateTB" runat="server" TextMode="Date" AutoCompleteType="Disabled" autocomplete="off"></asp:TextBox>
                        <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="startDateTB" ErrorMessage="invalid start date (must be a day later)" Operator="GreaterThan" Type="Date" ForeColor="Red"></asp:CompareValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="startDateTB" Display="None" ErrorMessage="required start date"></asp:RequiredFieldValidator>
                    </div>
                </div>

                <div class="col-lg-6">

                    <div class="form-group">
                        <label>To: </label>
                        <label style="color: red">*</label>
                        <asp:TextBox class="form-control" ID="endDateTB" runat="server" TextMode="Date" AutoCompleteType="Disabled" autocomplete="off"></asp:TextBox>&nbsp;
                         <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToCompare="startDateTB" ControlToValidate="endDateTB" ErrorMessage="invalid end date" Operator="GreaterThan" Type="Date" ForeColor="Red"></asp:CompareValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="endDateTB" Display="None" ErrorMessage="required end date"></asp:RequiredFieldValidator>
                    </div>
                </div>
             
            </div>

            

                
          
                <div class="row">
                    <div class="col-lg-6">
                        <div class="form-group">
                            <p>
                               
                                <asp:CheckBox ID="CheckBox1" runat="server" />
                               Agree with the <a data-toggle="modal" data-target="#myModal">terms and conditions</a>
                            </p>

                            <!-- Modal -->
                            <div class="modal fade" id="myModal" role="dialog">
                                <div class="modal-dialog">

                                    <!-- Modal content-->
                                    <div class="modal-content">
                                        <div class="modal-header">
                                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                                            <h4 class="modal-title">Terms and Conditions</h4>
                                        </div>
                                        <div class="modal-body">
                                            <p>
                                                Welcome to our Targeted Marketing Display. These are our terms and conditions for use of our targeted marketing display system.
                                            </p>
                                            <p>
                                                1. As a condition of use, you promise not to use the services for any purpose that is unlawful.
                                            </p>
                                            <p>
                                                2. The client acknowledges that the content of the advertisement is subject to copyright and accordingly the Client shall ensure that the advertisement is used solely for the client's reference purposes and may not be copied, reproduced,rebroadcast or commercially exploited in all or any part.
                                            </p>

                                            <p>
                                                3. You cannot terminate your contract before end date.
                                            </p>
                                        </div>
                                        <div class="modal-footer">
                                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <asp:Button ID="Button1" class="btn btn-primary nextBtn pull-right" runat="server" Text="Confirm" OnClick="ButtonConfirm_Click" style="margin-right:25px;"/>

                </div>

    </form>
</asp:Content>
