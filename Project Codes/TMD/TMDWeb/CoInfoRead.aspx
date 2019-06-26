<%@ Page Title="" Language="C#" MasterPageFile="~/Template.Master" AutoEventWireup="true" CodeFile="CoInfoRead.aspx.cs" Inherits="targeted_marketing_display.CoInfoRead" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style16 {
            text-align: right;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form runat="server">

        <div id="adminDiv">
            <div id="page-wrapper">
                <div class="row">
                    <div class="col-lg-12">
                        <h1 class="page-header">Company Information</h1>
                    </div>
                    <!-- /.col-lg-12 -->
                </div>
                <!-- /.row -->

                <br />
                 <div class="row">
                       <div class="col-lg-6">
                   <div class="input-group custom-search-form" style="width: 50%">
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
               </div>
                     
                    <div class="col-lg-6" >      
                        <br />
                        <a href="CoInfoCreate.aspx" class="btn btn-primary nextBtn pull-right" type="button"> <b>New Company </b></a>                  
                    </div>               
            </div>

             

                 <div runat="server" class="alert alert-success" id="alertSuccess" visible="False">
                    <strong>Success!</strong> 
                    <asp:Label runat="server" ID="msgSuccess"></asp:Label>
                    <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="row">
                    <div class="col-lg-12">
                        <div class="table-responsive">                       

                          <%--                        <table class="table table-striped table-bordered table-hover" style="width: 100%">--%>
                            <asp:GridView ID="GridView3" CssClass="table table-striped table-bordered table-hover" runat="server" AutoGenerateColumns="False" Height="100%" Width="100%" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" DataSourceID="SqlDataSource1" >



                                <Columns>

                                      <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label runat="server" Visible="false" ID="lb_CompanyID" Text='<%# Bind("CompanyID") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:BoundField HeaderText="ID" DataField="CompanyID" Visible="false">
                                        <ItemStyle Width="100px" Wrap="False" HorizontalAlign="Left"/>
                                    </asp:BoundField>



                                    <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name">
                                    
                                    <ControlStyle Height="50%" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                     <ItemStyle Width="50%" Wrap="False" VerticalAlign="Middle" HorizontalAlign="Left" />

                                      </asp:BoundField>
                                    <asp:BoundField DataField="Industry" HeaderText="Industry" SortExpression="Industry" >

                                        <ControlStyle Height="50%" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <ItemStyle Width="25%" Wrap="False" VerticalAlign="Middle" HorizontalAlign="Center" />

                                    </asp:BoundField>

                                    <asp:TemplateField HeaderText="Update">
                                        <ItemTemplate>

                                         <asp:LinkButton ID="editBtn" OnCommand="editBtn_Command" runat="server" CommandName="editcompanyInfo" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'>
                                        <i class="fa fa-edit"></i>
                                            </asp:LinkButton>
                                       </ItemTemplate>
                                        <ControlStyle Height="50%" />
                                        <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                                        <ItemStyle Width="5%" HorizontalAlign="Center" Wrap="False" VerticalAlign="Middle" />

                                    </asp:TemplateField>


                               

                                      <asp:TemplateField HeaderText="Adverts">
                                        <ItemTemplate>

                                        <asp:LinkButton ID="adsBtn" OnClick="adsBtn_Click" runat="server">
                                        <i class="fa fa-folder"></i>
                                            </asp:LinkButton>
                                       </ItemTemplate>
                                           <ControlStyle Height="50%" />
                                        <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                                        <ItemStyle Width="5%" HorizontalAlign="Center" Wrap="True" VerticalAlign="Middle" />
                                          </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Delete">
                                        <ItemTemplate>
                                    <asp:LinkButton ID="delBtn" OnCommand="btnDelete_Command" runat="server" CommandName="DeleteMessage" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'>
                                        <i class="fa fa-trash"></i>
                                            </asp:LinkButton>
                                    </ItemTemplate>
                                        <ControlStyle Height="50%" />
                                        <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                                        <ItemStyle Width="5%" HorizontalAlign="Center" Wrap="True" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                   



                                </Columns>
                                <FooterStyle BackColor="White" ForeColor="#000066" />
                                <HeaderStyle BackColor="#848c8E" Font-Bold="True" ForeColor="#f1f2ee" HorizontalAlign="Center" Wrap="False" />
                                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                <RowStyle ForeColor="#435058" />
                                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                <SortedDescendingHeaderStyle BackColor="#00547E" />
                            </asp:GridView>
                            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Targeted_Marketing_DisplayConnectionString %>" SelectCommand="SELECT [Name], [Industry], [CompanyID] FROM [Company] where status=1" FilterExpression="Name LIKE '%{0}%' OR Industry LIKE '%{0}%'">
                                <FilterParameters>
                                            <asp:ControlParameter ControlID="tbSearch" Name="Name" PropertyName="Text" />
                                            <asp:ControlParameter ControlID="tbSearch" Name="Indusrry" PropertyName="Text" />                                            
                                           
                                        </FilterParameters>        

                            </asp:SqlDataSource>
                            <%--                            </table>--%>
                        </div>
                        <!-- /.table-responsive -->

                    </div>
                    <!--/.col-lg-12-->
                </div>
            </div>
        </div>


    </form>
</asp:Content>
