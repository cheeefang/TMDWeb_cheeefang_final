<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="newAdvCreate.aspx.cs" Inherits="targeted_marketing_display.newAdvCreate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
       
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form runat="server">
        <div id="page-wrapper">

            

            <div class="row">
                <div class="col-lg-12">
                    <h1 class="page-header">New Advertisement</h1>
                </div>
                <!-- /.col-lg-12 -->
            </div>
            <!-- /.row -->

          

              <center> 
                  
                  <div runat="server" class="alert alert-success" id="alertSuccess" visible="False">
                    <strong>Success!</strong> New Advertisement has been created.
                </div> 

                <div runat="server" class="alert alert-warning" id="alertWarning" visible="False">
                    <strong>Warning!</strong>
                    <asp:Label ID="warningLocation" runat="server"></asp:Label>
                </div>
                  <div runat="server" class="alert alert-danger" id="alertDanger" visible="False">
                    <strong>Danger!</strong>
                    <asp:Label ID="dangerLocation" runat="server"></asp:Label>
                </div>
                  
              </center>
            
           

            <br />


            <div class="row">
                <div class="col-lg-6">

                    <div class="form-group">
                        <label>Advertisement File Type: </label>
                        <label style="color: red">*</label>
                        <asp:TextBox class="form-control" ID="BbLocationCode" placeholder="Enter Address Line 1" runat="server"></asp:TextBox>&nbsp;
                    </div>

                </div>

                <div class="col-lg-6">

                    <div class="form-group">
                        
                        &nbsp;
                    </div>

                </div>

            
            </div>

            <div class="row">
                <div class="col-lg-6">

                    <div class="form-group">
                        <label>Advertisement Name</label>
                        <label style="color: red">*</label>
                        &nbsp;
                    <asp:TextBox class="form-control" ID="BbAddLn1" placeholder="Enter Advertisement Name" runat="server"></asp:TextBox>&nbsp;
                    </div>
                </div>

                <div class="col-lg-6">

                    <div class="form-group">
                        <label>Address File</label>
                        <asp:TextBox class="form-control" ID="BbAddLn2" placeholder="Enter Advertisement File" runat="server"></asp:TextBox>
                        &nbsp;
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-lg-6">
                   <div class="form-group">

                        <label>Advertisement Duration(Only for videos) </label>
                        <label style="color: red">*</label>
                        <asp:TextBox class="form-control" ID="BbCity" placeholder="Enter Advertisement Duration" runat="server"></asp:TextBox>&nbsp;
                    </div>
                </div>

               

            </div>


            <div class="row">
                <div class="col-lg-6">

                    <div class="form-group">
                        <label>Company </label>
                        <label style="color: red">*</label>
                        <asp:TextBox class="form-control" ID="BbPostalCode" placeholder="Enter Company " runat="server"></asp:TextBox>&nbsp;
                    </div>

                </div>

                <div class="row">
                <div class="col-lg-6">
                   <div class="form-group">

                        <label>Advertisement Start Date</label>
                        <label style="color: red">*</label>
                        <asp:TextBox class="form-control" ID="TextBox1" placeholder="Enter Start Date" runat="server"></asp:TextBox>&nbsp;
                    </div>
                </div>

               

            </div>



            <div class="row">
                <div class="col-lg-6">
                   <div class="form-group">

                        <label>Advertisement End Date</label>
                        <label style="color: red">*</label>
                        <asp:TextBox class="form-control" ID="TextBox2" placeholder="Enter End Date" runat="server"></asp:TextBox>&nbsp;
                    </div>
                </div>

               

            </div>


                <div class="row">
                <div class="col-lg-6">
                   <div class="form-group">

                        <label>Billboard to display in</label>
                        <label style="color: red">*</label>
                        <asp:TextBox class="form-control" ID="TextBox3" placeholder="Enter End Date" runat="server"></asp:TextBox>&nbsp;
                    </div>
                </div>

               

            </div>

                <div class="col-lg-6">

                    <div class="form-group">
                        &nbsp;
                    </div>

                </div>
            </div>

            <div class="row">
                <div class="col-lg-12">

                    <asp:Button ID="btnSubmit" class="btn btn-primary nextBtn pull-right" runat="server"   Font-Bold="true" Text="Submit" OnClick="SubmitBtn_Click" />


                </div>
            </div>

        </div>

    </form>
</asp:Content>

