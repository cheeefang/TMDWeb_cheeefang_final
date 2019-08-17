<%@ Page Title="" Language="C#" MasterPageFile="~/Template.Master" AutoEventWireup="true" CodeFile="UserCreate.aspx.cs" Inherits="targeted_marketing_display.UserCreate" EnableViewState="true"%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form runat="server">
     

            <div class="row">
                <div class="col-lg-12">
                    <h1 class="page-header">New User</h1>
                </div>
                <!-- /.col-lg-12 -->
            </div>
            <!-- /.row -->


                <div runat="server" class="alert alert-success" id="alertSuccess" visible="False">
                    <strong>Success!</strong> 
                    <asp:Label runat="server" ID="msgSuccess"></asp:Label>
                    <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
        
                


                <div runat="server" class="alert alert-danger" id="alertWarning" visible="False">
                    <strong>Warning!</strong>
                    <asp:Label ID="msgWarning" runat="server"></asp:Label>
                    <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>


            <div class="row">
                <div class="col-lg-12">

                    <label style="color:red">* Mandatory Information</label>                

                </div>
            </div>

            <br />

            <div class="row">
                <div class="col-lg-6">
                    <div class="form-group">
                        <label>User Type </label>
                        <label style="color: red">*</label>
                        <asp:DropDownList Class="form-control" ID="ddlUserType" runat="server" OnSelectedIndexChanged="ddlUserType_SelectedIndexChanged" AutoPostBack="true">

                        </asp:DropDownList>&nbsp;
                        <asp:RequiredFieldValidator ID="rfvUser" runat="server" ControlToValidate="ddlUserType" Display="Dynamic" ErrorMessage="Please Select User Type"></asp:RequiredFieldValidator>

                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-lg-6">

                    <div class="form-group">
                        <label>Name </label>
                        <label style="color: red">*</label>
                        &nbsp;
                    <asp:TextBox class="form-control" ID="tbName" placeholder="Enter Name" runat="server"></asp:TextBox>&nbsp;
                    <asp:RequiredFieldValidator ID="rfvName" runat="server" ControlToValidate="tbName" Display="Dynamic" ErrorMessage="Please Enter Name" ForeColor="Red"></asp:RequiredFieldValidator>
                    </div>
                </div>

                <div class="col-lg-6">

                    <div class="form-group">
                        <label>E-mail</label>
                        <label style="color: red">*</label>
                        <asp:TextBox class="form-control" ID="tbEmail" placeholder="Enter E-mail" runat="server"></asp:TextBox>
                        &nbsp;
                        <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="tbEmail" Display="Dynamic" ErrorMessage="Please Enter Email" ForeColor="Red"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="tbEmail" Display="Dynamic" ErrorMessage="Please enter a valid email Address" ValidationExpression="^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$" ForeColor="Red"></asp:RegularExpressionValidator>
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-lg-6">
                    <div class="form-group">
                        <label>Contact No. </label>
                        <label style="color: red">*</label>
                        <asp:TextBox class="form-control" ID="tbConNo" placeholder="Enter Contact No." runat="server"></asp:TextBox>&nbsp;
                        <asp:RequiredFieldValidator ID="rfvConNo" runat="server" ControlToValidate="tbConNo" Display="Dynamic" ErrorMessage="Please Enter Contact Number." ForeColor="Red"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="revConNo" runat="server" ControlToValidate="tbConNo" Display="Dynamic" ErrorMessage="Please Enter A Valid 8 Digits Contact Number & Begins With The Number 8 or 9." ValidationExpression="^[89]\d{7}$" ForeColor="Red"></asp:RegularExpressionValidator>
                    </div>
                </div>

                <div runat="server" id="nUser" visible="false">
                    <div class="col-lg-6">

                        <div class="form-group">
                            <label>Company </label>
                            <label style="color: red">*</label>
                            <asp:DropDownList Class="form-control" ID="ddlCompany" runat="server" >
                  
                            </asp:DropDownList>&nbsp;
                        </div>

                    </div>
                </div>

            </div>

            <div class="row">
                <div class="col-lg-12">

                    <asp:Button ID="btnCreate" class="btn btn-primary nextBtn pull-right" runat="server"   Font-Bold="true" Text="Create" OnClick="btnCreate_User" />


                </div>
            </div>


    </form>
</asp:Content>
