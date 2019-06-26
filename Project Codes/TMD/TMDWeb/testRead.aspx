<%@ Page Title="" Language="C#" MasterPageFile="~/Template.Master" AutoEventWireup="true" CodeFile="testRead.aspx.cs" Inherits="targeted_marketing_display.testRead" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form runat="server">
        <div id="page-wrapper">


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
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Height="200px" Width="100%" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3">
                            <Columns>
                                <asp:BoundField DataField="ID" HeaderText="Company Name" />
                                <asp:BoundField DataField="Billboard Code" HeaderText="Industry" />
                                <asp:BoundField DataField="Address" HeaderText="Status" />

                                <asp:TemplateField HeaderText="Update">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="editBtn" OnClick="editBtn_Click" runat="server">
                                        <i class="fa fa-edit"></i>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Ads">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="adsBtn" OnClick="adsBtn_Click" runat="server">
                                        <i class="fa fa-folder"></i>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Delete">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="delBtn" OnClick="delBtn_Click" runat="server">
                                        <i class="fa fa-trash"></i>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
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
                        <%--                            </table>--%>
                    </div>

                </div>
            </div>
        </div>
    </form>
</asp:Content>
