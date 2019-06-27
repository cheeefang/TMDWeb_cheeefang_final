<%@ Page Title="" Language="C#" MasterPageFile="~/Template.Master" AutoEventWireup="true" CodeFile="FeedBackBillboard.aspx.cs" Inherits="targeted_marketing_display.FeedBackBillboard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .lbAdv
        {
            width:100% !important;
            height:100% !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form runat="server">
        <div id="adminDiv" runat="server">
            
                <!--button-->
                <div class="row">
                    <div class="col-lg-12">
                        <h1 class="page-header">Billboards</h1>
                    </div>
                    <!-- /.col-lg-12 -->
                </div>
                <!-- /.row -->

                <br />

            <div class="row">
                <div class="col-lg-12">
                                <p style="font-size:22pt">Select Billboards:</p>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-12">
                                <asp:ListBox ID="lbAdv" runat="server" CssClass="lbAdv" DataSourceID="SqlDataSource1" DataTextField="BillboardID" DataValueField="BillboardID">
                                    <asp:ListItem><--Select Billboard--></asp:ListItem>
                                </asp:ListBox>
                                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Targeted_Marketing_DisplayConnectionString %>" SelectCommand="SELECT [BillboardID], [BillboardCode], [latitude], [Longtitude], [AddressLn1], [AddressLn2], [Country], [City], [postalCode], [status], [CreatedBy], [CreatedOn] FROM [BillboardLocation]"></asp:SqlDataSource>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-12">
                    <asp:Button ID="btnAdd" runat="server" Text="Add" CssClass="btn btn-primary" OnClick="btnAdd_Click"/>
                </div>
            </div>
   
        </div>
    </form>
</asp:Content>
