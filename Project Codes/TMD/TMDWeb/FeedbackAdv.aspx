<%@ Page Title="" Language="C#" MasterPageFile="~/Template.Master" AutoEventWireup="true" CodeFile="FeedbackAdv.aspx.cs" Inherits="targeted_marketing_display.FeedbackAdv" %>
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
            <div id="page-wrapper">
                <!--button-->
                <div class="row">
                    <div class="col-lg-12">
                        <h1 class="page-header">Advertisements</h1>
                    </div>
                    <!-- /.col-lg-12 -->
                </div>
                <!-- /.row -->

                <br />

            <div class="row">
                <div class="col-lg-12">
                                <p style="font-size:22pt">Select Advertisements:</p>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-12">
                                <asp:ListBox ID="lbAdv" runat="server" CssClass="lbAdv" DataSourceID="SqlDataSource1" DataTextField="Name" DataValueField="Name">
                                    <asp:ListItem><--Select Advertisement--></asp:ListItem>
                                </asp:ListBox>
                                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Targeted_Marketing_DisplayConnectionString %>" SelectCommand="SELECT [Name], [Item], [ItemType], [StartDate], [EndDate] FROM [Advertisement]"></asp:SqlDataSource>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-12">
                    <asp:Button ID="btnAdd" runat="server" Text="Add" CssClass="btn btn-primary" OnClick="btnAdd_Click"/>
                </div>
            </div>
        </div>
        </div>
    </form>
</asp:Content>
