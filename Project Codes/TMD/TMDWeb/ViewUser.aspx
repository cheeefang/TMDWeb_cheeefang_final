<%@ Page Title="" Language="C#" MasterPageFile="~/Template.Master" AutoEventWireup="true" CodeFile="ViewUser.aspx.cs" Inherits="targeted_marketing_display.DeleteUser" %>
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


     <script>
        function deleteFunction() {

            if (!confirm('Confirm Deletion of User?')) {

                return false;
            }

            else {
                return true;
            }
        }
</script>



                <div class="row">

                    <div class="col-lg-12">
                        <h1 class="page-header">User Listing</h1>
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
                     <div class="col-lg-6">
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
                         </div>
            

                                        <div class="col-lg-6">
                        </br>
                        <a href="CreateNewUser.aspx" class="btn btn-primary nextBtn pull-right" type="button"> <b>New User </b> </a>
                    </div>
                </div>


                

                 <div runat="server" class="alert alert-success" id="Div1" visible="False">
                    <strong>Success!</strong> 
                    <asp:Label runat="server" ID="Label1"></asp:Label>
                    <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>






                            <asp:GridView ID="gvUser" runat="server" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" Height="100%" Width="100%" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" DataSourceID="SqlDataSource1"
                                AllowPaging="true"  PageSize="10" OnPreRender="gvUser_PreRender">
                               
                                <Columns>

                                     <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label runat="server" Visible="false" ID="lb_UserID" Text='<%# Bind("UserID") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:BoundField HeaderText="ID" DataField="UserID" Visible="false">
                                        <ItemStyle Width="100px" Wrap="False" HorizontalAlign="Left"/>
                                    </asp:BoundField>

                                    <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name"></asp:BoundField>
                                    <asp:BoundField DataField="Type" HeaderText="UserType" SortExpression="Type"></asp:BoundField>
                                    <asp:BoundField DataField="Expr1" HeaderText="Company Name" SortExpression="Expr1"></asp:BoundField>
                                    <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email"></asp:BoundField>

                                    <asp:templatefield headertext="View">
                                        <itemtemplate>

                                            <asp:LinkButton ID="viewBtn" OnCommand="infoBtn_Command" runat="server" CommandName="userInfo" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'>

                                        <i class="fas fa-eye"></i>

                                        </asp:LinkButton>
                                      
                                    </itemtemplate>
                                        <ControlStyle Height="50%" />
                                        <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                                        <ItemStyle Width="5%" HorizontalAlign="Center" Wrap="True" VerticalAlign="Middle" />
                                  </asp:templatefield>


                                    <asp:templatefield headertext="Update">
                                        <itemtemplate>
                                 
                                        <asp:LinkButton ID="editBtn" OnCommand="editBtn_Command" runat="server" CommandName="editInfo" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'>
                                        <i class="fa fa-edit"></i>
                                            </asp:LinkButton>
                                  
                                         </itemtemplate>
                                        <ControlStyle Height="50%" />
                                        <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                                        <ItemStyle Width="5%" HorizontalAlign="Center" Wrap="True" VerticalAlign="Middle" />
                                  </asp:templatefield>
                                    

                                    

                                     <asp:templatefield headertext="Delete">

                                         
                                        <itemtemplate>

                                            
                                        <asp:LinkButton ID="DeleteBtn" OnCommand="btnDelete_Command"   OnClientClick="return deleteFunction();" runat="server" CommandName="DeleteMessage" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'>
                                             <i class="fa fa-trash"></i>
                                             </asp:LinkButton>
                                      

                                                                                                             
                                         </itemtemplate>
                                        <ControlStyle Height="50%" />
                                        <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                                        <ItemStyle Width="5%" HorizontalAlign="Center" Wrap="True" VerticalAlign="Middle" />
                                  </asp:templatefield>                                    

                                </Columns>
                                <FooterStyle BackColor="White" ForeColor="#000066" />
                                <HeaderStyle BackColor="#848c8E" Font-Bold="True" ForeColor="#f1f2ee" />
                                <PagerStyle BackColor="White" ForeColor="#85C1E9" HorizontalAlign="Left" />
                                <RowStyle ForeColor="#435058" />
                                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                <SortedDescendingHeaderStyle BackColor="#00547E" />
                            </asp:GridView>

                <asp:Label ID="LabelPaging" runat="server" Text="Label"></asp:Label>

                <div class="row">

                    <div class="col-lg-12">

                        <div class="table-responsive">
                            <div class="alert alert-success" runat="server" id="panel_success" visible="false">
       <asp:Label ID="lbl_success" runat="server"></asp:Label>
   </div>
   <div class="alert alert-danger" runat="server" id="panel_error" visible="false">
       <asp:Label ID="lbl_error" runat="server"></asp:Label>
   </div>
                            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Targeted_Marketing_DisplayConnectionString %>" SelectCommand="SELECT [User].Name, [User].Type, Company.Name AS Expr1, [User].Email, [User].UserID FROM [User] INNER JOIN Company ON [User].CompanyID = Company.CompanyID where [User].Status=1" FilterExpression="Name LIKE '%{0}%' OR Type LIKE '%{0}%' OR Expr1 LIKE '%{0}%' OR Email LIKE '%{0}%' ">
                                <FilterParameters>
                                            <asp:ControlParameter ControlID="tbSearch" Name="Name" PropertyName="Text" />
                                            <asp:ControlParameter ControlID="tbSearch" Name="Type" PropertyName="Text" />                                            
                                            <asp:ControlParameter ControlID="tbSearch" Name="Expr1" PropertyName="Text" />
                                            <asp:ControlParameter ControlID="tbSearch" Name="Email" PropertyName="Text" />
                                        </FilterParameters>        
                            </asp:SqlDataSource>
                        </div>

                    
                    </div>

                </div>

        </form>


</asp:Content>
