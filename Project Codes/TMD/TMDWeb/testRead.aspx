<%@ Page Title="" Language="C#" MasterPageFile="~/Template.Master" AutoEventWireup="true" CodeFile="testRead.aspx.cs" Inherits="targeted_marketing_display.testRead" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form runat="server">
       


            <div class="row">
                <div class="col-lg-12">
                    <h1 class="page-header">Billboard Locations</h1>
                </div>
                <!-- /.col-lg-12 -->
            </div>
            <!-- /.row -->

            <div class="row">
                <div class="col-lg-12">
                    <a href="BBLocationCreate.aspx" class="btn btn-primary nextBtn pull-right" type="button">New Location </a>
                </div>
            </div>

            <br />

            <div class="row">
                <div class="col-lg-12">
                    <div class="table-responsive">
                        <%--                        <table class="table table-striped table-bordered table-hover" style="width: 100%">--%>
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Height="200px" Width="100%" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" DataKeyNames="BillboardID" DataSourceID="SqlDataSource1">
                            <Columns>
                                <asp:BoundField DataField="BillboardID" HeaderText="BillboardID" InsertVisible="False" ReadOnly="True" SortExpression="BillboardID" />
                                <asp:BoundField DataField="BillboardCode" HeaderText="BillboardCode" SortExpression="BillboardCode" />
                                <asp:BoundField DataField="latitude" HeaderText="latitude" SortExpression="latitude" />

                                <asp:BoundField DataField="Longtitude" HeaderText="Longtitude" SortExpression="Longtitude"></asp:BoundField>
                                <asp:BoundField DataField="AddressLn1" HeaderText="AddressLn1" SortExpression="AddressLn1"></asp:BoundField>
                                <asp:BoundField DataField="AddressLn2" HeaderText="AddressLn2" SortExpression="AddressLn2"></asp:BoundField>
                                <asp:BoundField DataField="City" HeaderText="City" SortExpression="City"></asp:BoundField>
                                <asp:BoundField DataField="Country" HeaderText="Country" SortExpression="Country"></asp:BoundField>
                            </Columns>
                            <FooterStyle BackColor="White" ForeColor="#000066" />
                            <HeaderStyle BackColor="#848c8E" Font-Bold="True" ForeColor="#f1f2ee" />
                            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                            <RowStyle ForeColor="#435058" />
                            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                            <SortedAscendingCellStyle BackColor="#F1F1F1" />
                            <SortedAscendingHeaderStyle BackColor="#007DBB" />
                            <SortedDescendingCellStyle BackColor="#CAC9C9" />
                            <SortedDescendingHeaderStyle BackColor="#00547E" />
                        </asp:GridView>
                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Targeted_Marketing_DisplayConnectionString %>" SelectCommand="SELECT [BillboardID], [BillboardCode], [latitude], [Longtitude], [AddressLn1], [AddressLn2], [City], [Country] FROM [BillboardLocation]"></asp:SqlDataSource>
                        <%--                            </table>--%>
                    </div>

                </div>
            </div>
       
    </form>
</asp:Content>
