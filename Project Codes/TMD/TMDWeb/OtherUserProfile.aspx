<%@ Page Title="" Language="C#" MasterPageFile="~/Template.Master" AutoEventWireup="true" CodeFile="OtherUserProfile.aspx.cs" Inherits="targeted_marketing_display.OtherUserProfile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form runat="server">

    

            <div class="row">
                <div class="col-lg-12">
                    <h1 class="page-header">Profile Listing </h1>
                </div>
                <!-- /.col-lg-12 -->
            </div>
            <!-- /.row -->

            <div class="row">
                <div class="col-lg-4">
                    <div class="form-group">
                        <label>Name: </label>
                        &nbsp;
                        <asp:Label runat="server" ID="lbName"></asp:Label>&nbsp;
                    </div>
                </div>

                <div class="col-lg-4">
                    <div class="form-group">
                        <label>E-mail: </label>
                        <asp:Label runat="server" ID="lbEmail"></asp:Label>&nbsp;
                    </div>
                </div>

                <div class="col-lg-4">
                    <div class="form-group">
                        <label>Contact No.: </label>
                        <asp:Label runat="server" ID="lbContact"></asp:Label>&nbsp;
                    </div>
                </div>
            </div>


            <div class="row">
                <div class="col-lg-4">
                    <div class="form-group">
                        <label>User Type: </label>
                        <asp:Label runat="server" ID="lbUserType"></asp:Label>&nbsp;

                    </div>
                </div>

                <div class="col-lg-4">
                    <div class="form-group">
                        <label>Company: </label>
                        <asp:Label runat="server" ID="lbCompany"></asp:Label>&nbsp;
                    </div>
                </div>

                <div class="col-lg-4">
                    <div class="form-group">
                        <label>Status: </label>
                        <asp:Label runat="server" ID="lbStatus"></asp:Label>&nbsp;
                    </div>
                </div>

            </div>


    </form>
</asp:Content>
