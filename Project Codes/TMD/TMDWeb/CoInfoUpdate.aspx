<%@ Page Title="" Language="C#" MasterPageFile="~/Template.Master" AutoEventWireup="true" CodeFile="CoInfoUpdate.aspx.cs" Inherits="targeted_marketing_display.CoInfoUpdate" %>

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
                            <asp:ListItem value="">Select Industry</asp:ListItem>
                            <asp:ListItem value="F&B">F&B</asp:ListItem>
                            <asp:ListItem value="Fin.">Finance</asp:ListItem>
                            <asp:ListItem value="Agro.">Agriculture</asp:ListItem>
                            <asp:ListItem value="Mine">Mine</asp:ListItem>
                            <asp:ListItem value="Utility">Utility</asp:ListItem>
                            <asp:ListItem value="Manu.">Manufacturing</asp:ListItem>
                            <asp:ListItem value="Sale">Sale</asp:ListItem>
                            <asp:ListItem value="Retail">Retail</asp:ListItem>
                            <asp:ListItem value="Tran.">Transport</asp:ListItem>
                            <asp:ListItem value="House">House</asp:ListItem>
                            <asp:ListItem value="Rental">Rental</asp:ListItem>
                            <asp:ListItem value="Service">Service</asp:ListItem>
                            <asp:ListItem value="Manage">Management</asp:ListItem>
                            <asp:ListItem value="Adm.">Administration</asp:ListItem>
                            <asp:ListItem value="Edu.">Education</asp:ListItem>
                            <asp:ListItem value="Ware.">Warehouse</asp:ListItem>
                            <asp:ListItem value="Media">Media</asp:ListItem>
                            <asp:ListItem value="Tel.">Telecommunication</asp:ListItem>
                            <asp:ListItem value="Health">Health</asp:ListItem>
                            <asp:ListItem value="Care">Care</asp:ListItem>
                            <asp:ListItem value="Ent.">Entertainment</asp:ListItem>
                            <asp:ListItem value="Central">Central Administrative</asp:ListItem>
                            <asp:ListItem value="Cons">Construction</asp:ListItem>
                            <asp:ListItem value="Other">Other</asp:ListItem>
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
