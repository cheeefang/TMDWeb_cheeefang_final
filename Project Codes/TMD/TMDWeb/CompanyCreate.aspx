<%@ Page Title="" Language="C#" MasterPageFile="~/Template.Master" AutoEventWireup="true" CodeFile="CompanyCreate.aspx.cs" Inherits="targeted_marketing_display.CompanyCreate" EnableViewState="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <form runat="server">

    

            

            <div class="row">
                <div class="col-lg-12">
                    <h1 class="page-header">New Company </h1>
                </div>
                <!-- /.col-lg-12 -->
                <!-- /.row -->
            </div>

              <div runat="server" class="alert alert-success" id="alertSuccess" visible="False">
                    <strong>Success!</strong> New Company Information has been created.
                </div>
               
                <div runat="server" class="alert alert-danger" id="alertWarning" visible="False">
                    <strong>Warning!</strong>  <asp:Label ID="warningLocation" runat="server"></asp:Label>
                </div>
               
            <br />


            <div class="row">
                <div class="col-lg-6">


                    <div class="form-group">
                        <label>Company Name</label><label style="color:red">*</label>
                       
                         <asp:TextBox  class="form-control" ID="CoName" placeholder="Enter Company Name" runat="server" MaxLength="255"></asp:TextBox> &nbsp;

                    </div>
                </div>

                <div class="col-lg-6">
                    <div class="form-group">
                        <label>Industry</label><label style="color:red">*</label>
                        <asp:DropDownList Class="form-control" ID="CoIndustry" runat="server"></asp:DropDownList>
                    </div>
                </div>
                </div>

                <div class="row">
                    <div class="col-lg-12">
                        <asp:Button ID="SubmitBtn1" class="btn btn-primary nextBtn pull-right"   Font-Bold="true" runat="server" Text="Submit" OnClick="SubmitBtn_Click" />
                    </div>
                </div>

           
        
            <!-- /.row (nested) -->
        
    </form>

</asp:Content>
