<%@ Page Title="" Language="C#" MasterPageFile="~/Template.Master" AutoEventWireup="true" CodeFile="BBLocationRead.aspx.cs" Inherits="targeted_marketing_display.BBLocationRead" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server" align="left">
    <style type="text/css">
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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server" align="left">
    <form runat="server">

    <script>
        function deleteFunction() {

            if (!confirm('Confirm Deletion of Billboard?')) {

                return false;
            }

            else {
                return true;
            }
        }
</script>

        <div id="adminDiv" runat="server" align="left">

       
                <!--button-->
                <div class="row">
                    <div class="col-lg-12">
                        <h1 class="page-header">Billboard Locations Listing</h1>
                    </div>
                    <!-- /.col-lg-12 -->
                </div>
                <!-- /.row -->

        

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

                    <div class="col-lg-6">
                        </br>
                        <a href="BBLocationCreate.aspx" class="btn btn-primary nextBtn pull-right" type="button"> <b>New Billboard </b> </a>
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

                            <asp:GridView ID="GridView1" CssClass="table table-striped table-bordered table-hover" runat="server" AutoGenerateColumns="False" Height="100%" Width="100%" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" DataSourceID="SqlDataSource4" AllowPaging="True" OnPreRender="GridView1_PreRender" PageSize="10">
                                <Columns>

                                    <asp:TemplateField visible="false">
                                        <ItemTemplate>
                                            <asp:Label runat="server" visible="false"  ID="lb_BillboardID" Text='<%# Bind("BillboardID") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:BoundField HeaderText="ID" DataField="BillboardID" visible="false">
                                        <ItemStyle Width="100px" Wrap="False" HorizontalAlign="Left"/>
                                    </asp:BoundField>


                                    <asp:BoundField DataField="BillboardCode" HeaderText="BillboardCode" SortExpression="BillboardCode">
                                    </asp:BoundField>

                                    <asp:BoundField DataField="Latitude" HeaderText="Latitude" SortExpression="Latitude">
                                         <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>

                                    <asp:BoundField DataField="Longtitude" HeaderText="Longtitude" SortExpression="Longtitude">
                                          <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>

                                    <asp:BoundField DataField="Address" HeaderText="Address" SortExpression="Address" ReadOnly="True">
                                    </asp:BoundField>


                                     <asp:templatefield headertext="View">
                                        <itemtemplate>

                                            <asp:LinkButton ID="viewBtn" OnCommand="infoBtn_Command" runat="server" CommandName="BillboardAdInfo" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'>


                                        <i class="fas fa-eye"></i>

                                            


                                        </asp:LinkButton>
                                      
                                    </itemtemplate>
                                        <ControlStyle Height="50%" />
                                        <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                                        <ItemStyle Width="5%" HorizontalAlign="Center" Wrap="True" VerticalAlign="Middle" />
                                  </asp:templatefield>


                                    <asp:templatefield headertext="Update">
                                        <itemtemplate>
                                 
                                        <asp:LinkButton ID="editBtn" OnCommand="editBtn_Command" runat="server" CommandName="BillboardUpdateInfo" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'>

                                        <i class="fa fa-edit"></i>
                                            

                                            </asp:LinkButton>
                                  
                                         </itemtemplate>
                                        <ControlStyle Height="50%" />
                                        <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                                        <ItemStyle Width="5%" HorizontalAlign="Center" Wrap="True" VerticalAlign="Middle" />
                                  </asp:templatefield>
                                    

                                    

                                     <asp:templatefield headertext="Delete">

                                         
                                        <itemtemplate>

                                            
                                        <asp:LinkButton ID="DeleteBtn"  OnClientClick="return deleteFunction();"  OnCommand="btnDelete_Command" runat="server" CommandName="DeleteBBMessage" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'>

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
                                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                <RowStyle ForeColor="#435058" />
                                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                <SortedDescendingHeaderStyle BackColor="#00547E" />
                            </asp:GridView>

                            <asp:Label ID="Label1" style="color:darkslateblue" runat="server" Text="Label"></asp:Label>

                            <asp:SqlDataSource ID="SqlDataSource4" runat="server" ConnectionString="<%$ ConnectionStrings:Targeted_Marketing_DisplayConnectionString %>" SelectCommand="SELECT BillboardID,BillboardCode, Latitude ,Longtitude ,(( AddressLn1) + ' '+( AddressLn2 )+  ' '+(City)+  ', '+(Country)+ ' '+(postalCode)) AS Address FROM BillboardLocation where status=1 " FilterExpression="BillboardCode LIKE '%{0}%' OR Address LIKE '%{0}%' OR convert(Latitude,'System.String') LIKE '%{0}%' OR convert(Longtitude,'System.String') LIKE '%{0}%'">
                                 <FilterParameters>
                                            <asp:ControlParameter ControlID="tbSearch" Name="BillboardCode" PropertyName="Text" />
                                            <asp:ControlParameter ControlID="tbSearch" Name="Latitude" PropertyName="Text" />                                            
                                            <asp:ControlParameter ControlID="tbSearch" Name="Longtitude" PropertyName="Text" />  
                                            <asp:ControlParameter ControlID="tbSearch" Name="Address" PropertyName="Text" />  
                                        </FilterParameters> 

                            </asp:SqlDataSource>

                            <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:Targeted_Marketing_DisplayConnectionString %>" SelectCommand="SELECT * FROM [Advertisement]"></asp:SqlDataSource>

                            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Targeted_Marketing_DisplayConnectionString %>" SelectCommand="SELECT * FROM [Advertisement]"></asp:SqlDataSource>

                        </div>
                        <!-- /.table-responsive -->
                    </div>
                </div>
         
        </div>

        <div id="userDiv" runat="server">
          
                <div class="row">

                    <div class="col-lg-12">
                        <h1 class="page-header">Billboard Locations</h1>
                    </div>
                    <!-- /.col-lg-12 -->
                </div>
                <!-- /.row -->

                <br />
                <div class="row">

                    <div class="col-lg-6">

                        <div class="input-group custom-search-form" style="width: 50%">
                            <input type="text" class="form-control" runat="server" placeholder="Search...">
                            <span class="input-group-btn">
                                <button class="btn btn-default" runat="server" type="button">
                                    <i class="fa fa-search"></i>
                                </button>
                            </span>
                        </div>
                    </div>

                    <div class="col-lg-6">
                        &nbsp; 
                    </div>
                    <!-- /.col-lg-6 -->

                </div>


                <br />

                <br />

                <div class="row">
                    <div class="col-lg-12">

                        <div class="table-responsive">
                            <%--                        <table class="table table-striped table-bordered table-hover" style="width: 100%">--%>

                            <asp:GridView ID="GridView2" CssClass="table table-striped table-bordered table-hover" runat="server" AutoGenerateColumns="False" Height="100%" Width="100%" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" DataKeyNames="BillboardID" DataSourceID="SqlDataSource5">
                                <Columns>


                                    <asp:BoundField DataField="BillboardID" HeaderText="BillboardID" InsertVisible="False" ReadOnly="True" SortExpression="BillboardID">
                                    </asp:BoundField>

                                    <asp:BoundField DataField="BillboardCode" HeaderText="BillboardCode" SortExpression="BillboardCode">
                                    </asp:BoundField>

                                    <asp:BoundField DataField="latitude" HeaderText="latitude" SortExpression="latitude">
                                    </asp:BoundField>

                                    <asp:BoundField DataField="Longtitude" HeaderText="Longtitude" SortExpression="Longtitude">
                                    </asp:BoundField>

                                    <asp:BoundField DataField="AddressLn1" HeaderText="AddressLn1" SortExpression="AddressLn1">
                                    </asp:BoundField>

                                    <asp:BoundField DataField="AddressLn2" HeaderText="AddressLn2" SortExpression="AddressLn2">
                                    </asp:BoundField>

                                    <asp:BoundField DataField="City" HeaderText="City" SortExpression="City">
                                    </asp:BoundField>

                                 

                                    <asp:BoundField DataField="Country" HeaderText="Country" SortExpression="Country" />
                                    <asp:BoundField DataField="postalCode" HeaderText="postalCode" SortExpression="postalCode" />
                                    <asp:BoundField DataField="status" HeaderText="status" SortExpression="status" />
                                    <asp:BoundField DataField="CreatedBy" HeaderText="CreatedBy" SortExpression="CreatedBy" />
                                    <asp:BoundField DataField="CreatedOn" HeaderText="CreatedOn" SortExpression="CreatedOn" />

                                 

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

                            <asp:SqlDataSource ID="SqlDataSource5" runat="server" ConnectionString="<%$ ConnectionStrings:Targeted_Marketing_DisplayConnectionString %>" SelectCommand="SELECT [BillboardID], [BillboardCode], [latitude], [Longtitude], [AddressLn1], [AddressLn2], [City], [Country], [postalCode], [status], [CreatedBy], [CreatedOn] FROM [BillboardLocation]"></asp:SqlDataSource>

                            <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:Targeted_Marketing_DisplayConnectionString %>" SelectCommand="SELECT * FROM [Advertisement]"></asp:SqlDataSource>

                        </div>
                        <!-- /.table-responsive -->
                    </div>
                </div>
     
        </div>

    </form>
</asp:Content>
