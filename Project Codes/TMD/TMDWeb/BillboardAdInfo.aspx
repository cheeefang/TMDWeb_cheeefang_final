<%@ Page Language="C#" MasterPageFile="~/Template.Master" AutoEventWireup="true" CodeFile="BillboardAdInfo.aspx.cs" Inherits="targeted_marketing_display.BillboardAdInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style>
        .auto-style2 {
            height: 42px;
            width: 150px;
        }

        .auto-style3 {
            width: 100px;
        }

        .auto-style4 {
            height: 42px;
            width: 65px;
        }

        .auto-style6 {
            height: 42px;
            width: 65px;
            text-align: center;
        }

        .auto-style10 {
            height: 30px;
            width: 5px;
            text-align: center;
        }
    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

                                     


        <form runat="server" id="deleteUser">

                <div class="row">

                    <div class="col-lg-12">
                        <h1 class="page-header">Billboard Ad Info</h1>
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

                <div class="row">
                    <div class="input-group custom-search-form">
                  <div style="padding: 20px; float: left; width:30%;">
                                          <p class="input-group" style="width:350px;margin-left:-20px;">
                                        <asp:TextBox ID="tbSearch" class="form-control" runat="server" placeholder="Search..."></asp:TextBox>
                                        <%--<input type="submit" id="btSubmit" runat="server" />--%>
                                        <span class="input-group-btn" >
                                            <asp:LinkButton runat="server" class="btn btn-default" ID="btnRun" style="height:34px;" Text="<i class='fa fa-search'></i>"/>
                                       </span>
                                            </p>
                                    </div>
            </div>
            

                            <asp:GridView ID="gvBillboard" runat="server" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" Height="100%" Width="100%" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" DataSourceID="SqlDataSource1"  >
                                <Columns>

                             



                                    <asp:BoundField HeaderText="AdvID" DataField="AdvID" InsertVisible="False" ReadOnly="True" SortExpression="AdvID">
                                    </asp:BoundField>

                                    <asp:BoundField DataField="BillboardID" HeaderText="BillboardID" SortExpression="BillboardID"></asp:BoundField>
                                    <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name"></asp:BoundField>
                                    <asp:BoundField DataField="Item" HeaderText="Item" SortExpression="Item"></asp:BoundField>
                                    
                                    
                                    

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

                

                <div class="row">

                    <div class="col-lg-12">

                        <div class="table-responsive">
                            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Targeted_Marketing_DisplayConnectionString %>" SelectCommand="SELECT Advertisement.[AdvID],AdvertisementLocation.[BillboardID],Advertisement.[Name],Advertisement.[Item] FROM [Advertisement] inner join [AdvertisementLocation] on Advertisement.AdvID=AdvertisementLocation.AdvID" FilterExpression="Name LIKE '%{0}%' OR Type LIKE '%{0}%' OR Expr1 LIKE '%{0}%' OR Email LIKE '%{0}%' ">
                                <FilterParameters>
                                            <asp:ControlParameter ControlID="tbSearch" Name="Name" PropertyName="Text" />
                                            <asp:ControlParameter ControlID="tbSearch" Name="AdvID" PropertyName="Text" />                                            
                                            <asp:ControlParameter ControlID="tbSearch" Name="BillboardID" PropertyName="Text" />
                                            <asp:ControlParameter ControlID="tbSearch" Name="Item" PropertyName="Text" />
                                        </FilterParameters>        
                            </asp:SqlDataSource>
                        </div>

                    
                    </div>

                </div>

        </form>


</asp:Content>
