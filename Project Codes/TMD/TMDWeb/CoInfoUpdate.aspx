<%@ Page Title="" Language="C#" MasterPageFile="~/Template.Master" AutoEventWireup="true" CodeFile="CoInfoUpdate.aspx.cs" Inherits="targeted_marketing_display.CoInfoUpdate" EnableViewState="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
       
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form runat="server">

      


            <div class="row">
                <div class="col-lg-12">
                    <h1 class="page-header">Update Company</h1>
                </div>
                <!-- /.col-lg-12 -->
                <!-- /.row -->
            </div>

             <div runat="server" class="alert alert-success" id="alertSuccess" visible="False">
                    <strong>Success!</strong> Company Information has been Updated.
                </div>
               
                <div runat="server" class="alert alert-danger" id="alertWarning" visible="False">
                    <strong>Warning!</strong> Update Unsuccessful
                </div>

            <br />

            <div class="row">
                <div class="col-lg-6">


                    <div class="form-group">
                        <label>Company Name</label>
                       
                         <asp:TextBox  class="form-control" ID="CoName" placeholder="Enter Company Name" runat="server" MaxLength="255"></asp:TextBox> &nbsp;

                    </div>
                </div>

                <div class="col-lg-6">
                    <div class="form-group">
                        <label>Industry</label>
                        <asp:DropDownList Class="form-control" ID="CoIndustry" runat="server">                         
                        </asp:DropDownList>
                    </div>
                </div>
                </div>

                <div class="row">
                    <div class="col-lg-12">
                        <asp:Button ID="updateBtn" class="btn btn-primary nextBtn pull-right" runat="server"   Font-Bold="true" Text="Update" OnClick="updateBtn_Click" />
                    </div>
             

            </div>
            <!-- /row -->


    

    </form>


</asp:Content>
