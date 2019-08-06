<%@ Page Title="" Language="C#" MasterPageFile="~/Template.Master" AutoEventWireup="true" CodeFile="UpdateOwnProfile.aspx.cs" Inherits="targeted_marketing_display.UpdateOwnProfile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .btn {
            margin:8px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form runat="server">
      

            <div class="row">
                <div class="col-lg-12">
                    <h1 class="page-header">Update Profile</h1>
                </div>
                <!-- /.col-lg-12 -->
            </div>
            <!-- /.row -->
         <div runat="server" class="alert alert-danger" id="alertDanger" visible="False">
                    <strong>Error!</strong> Your Current Password is Incorrect
                    <button type="button" class="close" onclick="window.location.href='OwnProfile.aspx';">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div runat="server" class="alert alert-success" id="alertSuccess" visible="False">
                    <strong>Success!</strong> Your profile has been updated.
                    <button type="button" class="close" onclick="window.location.href='OwnProfile.aspx';">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>

            <div class="row">
                <div class="col-lg-6">

                    <div class="form-group">
                        <label>Name </label>
                    <asp:TextBox class="form-control" ID="tbName" placeholder="Enter Name" runat="server"></asp:TextBox>&nbsp;
                    <asp:RequiredFieldValidator ID="rfvName" runat="server" ControlToValidate="tbName" Display="Dynamic" ErrorMessage="Please Enter Name" ForeColor="Red"></asp:RequiredFieldValidator>
                    </div>

                </div>

                <div class="col-lg-6">

                    <div class="form-group">
                        <label>Contact No. </label>
                        <asp:TextBox class="form-control" ID="tbContact" placeholder="Enter Contact No." runat="server"></asp:TextBox>&nbsp;
                        <asp:RequiredFieldValidator ID="rfvConNo" runat="server" ControlToValidate="tbContact" Display="Dynamic" ErrorMessage="Please Enter Contact Number." ForeColor="Red"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="revConNo" runat="server" ControlToValidate="tbContact" Display="Dynamic" ErrorMessage="Please Enter A Valid 8 Digits Contact Number & Begins With The Number 8 or 9." ValidationExpression="^[89]\d{7}$" ForeColor="Red"></asp:RegularExpressionValidator>
                    </div>

                </div>

            </div>

        <div class="row">

                <div class="col-lg-6" runat="server" id="divCurrentPassword" visible="false">

                    <div class="form-group">
                        <label>Current Password </label>
                        <asp:TextBox class="form-control" ID="CurrentPassword" placeholder="Enter Password" runat="server" TextMode="Password"></asp:TextBox>&nbsp;
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="CurrentPassword" Display="Dynamic" ErrorMessage="Please Enter Password." ForeColor="Red"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="CurrentPassword" Display="Dynamic" ErrorMessage="Please Enter A Password With Length Between 8-12 Characters." ValidationExpression="^.{8,12}$" ForeColor="Red"></asp:RegularExpressionValidator>
                    </div>

                </div>
            </div>


            <div class="row">

                <div class="col-lg-6" runat="server" id="divPswd" visible="false">

                    <div class="form-group">
                        <label>New Password </label>
                        <asp:TextBox class="form-control" ID="tbPswd" placeholder="Enter Password" runat="server" TextMode="Password"></asp:TextBox>&nbsp;
                        <asp:RequiredFieldValidator ID="rfvPswd" runat="server" ControlToValidate="tbPswd" Display="Dynamic" ErrorMessage="Please Enter Password." ForeColor="Red"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="revPswd" runat="server" ControlToValidate="tbPswd" Display="Dynamic" ErrorMessage="Please Enter A Password With Length Between 8-12 Characters." ValidationExpression="^.{8,12}$" ForeColor="Red"></asp:RegularExpressionValidator>
                    </div>

                </div>

                <div class="col-lg-6" runat="server" id="divCPswd" Visible="false">

                    <div class="form-group">
                        <label>Confirm New Password </label>
                        <asp:TextBox class="form-control" ID="tbCPswd" placeholder="Confirm Password" runat="server" TextMode="Password"></asp:TextBox>&nbsp;
                        <asp:RequiredFieldValidator ID="rfvCpswd" runat="server" ControlToValidate="tbCPswd" Display="Dynamic" ErrorMessage="Please Confirm Password." ForeColor="Red"></asp:RequiredFieldValidator>
                    </div>

                </div>

            </div>

            <div class="row">
                <div class="col-lg-12">
                                                         
                    <asp:Button ID="btnUpdate" class="btn btn-primary nextBtn pull-right" runat="server" Text="Update" onclick="btnUpdate_Click"/>

                    <asp:Button ID="btnPswd" class="btn btn-primary nextBtn pull-right" runat="server" Text="Change Password?" onclick="btnPswd_Click"/>


                </div>
            </div> 

 

    </form>
</asp:Content>
