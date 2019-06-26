<%@ Page Title="" Language="C#" MasterPageFile="~/Template.Master" AutoEventWireup="true" CodeFile="OwnProfile.aspx.cs" Inherits="targeted_marketing_display.OwnProfile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <form runat="server">

        <div id="page-wrapper">

            <div runat="server" id="UserView" visible="false">

                <div class="row">
                    <div class="col-lg-12 col-md-12 col-sm-12">
                        <h1 class="page-header"> My Profile</h1>
                    </div>
                    <!-- /.col-lg-12 -->
                </div>
                <!-- /.row -->

                <div class="row">
                    <div class="col-lg-4 col-md-4 col-sm-4">

                        <div class="form-group">
                            <label>Name: </label>
                            &nbsp;
                            <asp:Label runat="server" ID="lbName"></asp:Label>&nbsp;
                        </div>
                    </div>

                    <div class="col-lg-4 col-md-4 col-sm-4">

                        <div class="form-group">
                            <label>E-mail: </label>
                            <asp:Label runat="server" ID="lbEmail"></asp:Label>&nbsp;
                        </div>
                    </div>

                    <div class="col-lg-4 col-md-4 col-sm-4">
                        <div class="form-group">
                            <label>Contact No.: </label>
                            <asp:Label runat="server" ID="lbContact"></asp:Label>&nbsp;
                        </div>
                    </div>
                </div>


                <div class="row">
                    <div class="col-lg-4 col-md-4 col-sm-4">
                        <div class="form-group">
                            <label>User Type: </label>
                            <asp:Label runat="server" ID="lbUserType"></asp:Label>&nbsp;

                        </div>
                    </div>

                    <div class="col-lg-4 col-md-4 col-sm-4">

                        <div class="form-group">
                            <label>Company: </label>
                            <asp:Label runat="server" ID="lbCompany"></asp:Label>&nbsp;
                        </div>
                    </div>

                    <div class="col-lg-4 col-md-4 col-sm-4">
                        <div class="form-group">
                            <label>Status: </label>
                            <asp:Label runat="server" ID="lbStatus"></asp:Label>&nbsp;

                        </div>
                    </div>

                </div>

                <div class="row">
                    <div class="col-lg-12 col-md-12 col-sm-12">
                        <asp:Button ID="btnEdit" class="btn btn-primary nextBtn pull-right" runat="server" Text="Edit" onclick="btnEdit_Click"/>
                    </div>
                </div>

            </div>



            <div runat="server" id="AdminView" visible="false">

                <div class="row">
                    <div class="col-lg-12 col-md-12 col-sm-12">
                        <h1 class="page-header">Profile</h1>
                    </div>
                    <!-- /.col-lg-12 -->
                </div>
                <!-- /.row -->

                <div class="row">
                    <div class="col-lg-4 col-md-4 col-sm-4">

                        <div class="form-group">
                            <label>Name: </label>
                            &nbsp;
                            <asp:Label runat="server" ID="lbAdminName"></asp:Label>&nbsp;
                        </div>
                    </div>

                    <div class="col-lg-4 col-md-4 col-sm-4">

                        <div class="form-group">
                            <label>E-mail: </label>
                            <asp:Label runat="server" ID="lbAdminEmail"></asp:Label>&nbsp;
                        </div>
                    </div>

                    <div class="col-lg-4 col-md-4 col-sm-4">
                        <div class="form-group">
                            <label>Contact No.: </label>
                            <asp:Label runat="server" ID="lbAdminContact"></asp:Label>&nbsp;
                        </div>
                    </div>
                </div>


                <div class="row">
                    <div class="col-lg-4 col-md-4 col-sm-4">
                        <div class="form-group">
                            <label>User Type: </label>
                            <asp:Label runat="server" ID="lbAdminType"></asp:Label>&nbsp;

                        </div>
                    </div>

                    <div class="col-lg-4 col-md-4 col-sm-4">
                        <div class="form-group">
                            <label>Status: </label>
                            <asp:Label runat="server" ID="lbAdminStatus"></asp:Label>&nbsp;

                        </div>
                    </div>

                </div>

                <div class="row">
                    <div class="col-lg-12 col-md-12 col-sm-12">
                        <asp:Button ID="Button1" class="btn btn-primary nextBtn pull-right" runat="server" Font-Bold="true" Text="Edit" onclick="btnEdit_Click"/>
                    </div>
                </div>

            </div>

        </div>

    </form>
</asp:Content>
