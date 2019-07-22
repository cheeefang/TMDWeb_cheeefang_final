<%@ Page Title="" Language="C#" MasterPageFile="~/Template.Master" AutoEventWireup="true" CodeFile="AdListing.aspx.cs" Inherits="targeted_marketing_display.AdListing" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
      .GridViewEditRow input[type=text] {
            width: 90px;
        }
        /* size textboxes */
        .GridViewEditRow select {
            width: 90px;
        }
        /* size drop down lists */

           .hiddencol
  {
    display: none;
  }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <form runat="server">
<script>
        function deleteFunction() {

            if (!confirm('Confirm Deletion of Advertisement')) {

                return false;
            }

            else {
                return true;
            }
        }
</script>



        
     
            <!--button-->
            <div class="row">
                <div class="col-lg-12">
                    <h1 class="page-header">Advertisement Listing</h1>
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
                                        <asp:TextBox ID="txtSearch" class="form-control" runat="server" placeholder="Search..."></asp:TextBox>
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
                    <a href="AdCreate.aspx" class="btn btn-primary nextBtn pull-right" type="button"> <b> New Advertisement </b> </a>
                  
                </div>
                 </div>
         <div runat="server" class="alert alert-success" id="alertSuccessCreate" visible="False">
                    <strong>Successfully Created Advertisement</strong> 
                    <asp:Label runat="server" ID="Label2"></asp:Label>
                    <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>

          <div runat="server" class="alert alert-success" id="alertSuccessUpdate" visible="False">
                    <strong>Successfully Updated Advertisement</strong> 
                    <asp:Label runat="server" ID="msgSuccess"></asp:Label>
                    <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>

           <div runat="server" class="alert alert-success" id="alertSuccessDelete" visible="False">
                    <strong></strong> 
                    <asp:Label runat="server" ID="Label3"></asp:Label>
                    <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>



            <div id="all" runat="server">
                <div class="row">
                    <div class="col-lg-12">

                        <div class="table-responsive">
                            <%--                        <table class="table table-striped table-bordered table-hover" style="width: 100%">--%>
                            <asp:GridView ID="GridView1" CssClass="table table-striped table-bordered table-hover" runat="server" AutoGenerateColumns="False" Width="100%" 
                                BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px"
                                CellPadding="3" DataSourceID="SqlDataSource1" AllowPaging="True" HorizontalAlign="Center" DataKeyNames="AdvID"  OnPreRender="GridView1_PreRender" ForeColor="Black" GridLines="Vertical" OnRowDataBound="GridView1_RowDataBound">
                                <AlternatingRowStyle HorizontalAlign="Center" BackColor="#CCCCCC" />
                                <Columns>
                                    <asp:TemplateField visible="false">
                                        <ItemTemplate>
                                            <asp:Label runat="server" visible="false"  ID="lb_AdvertID" Text='<%# Bind("AdvID") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>



                                    <%-- <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status" ReadOnly="True">
                                    </asp:BoundField>--%>



                                    <asp:BoundField DataField="AdvID" HeaderText="AdvID" SortExpression="AdvID" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" InsertVisible="False" ReadOnly="True">
<HeaderStyle CssClass="hiddencol"></HeaderStyle>

<ItemStyle CssClass="hiddencol"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:ImageField DataImageUrlField="Item"  HeaderText="Advertisement" ControlStyle-Width="100" ControlStyle-Height = "100"> 
        
<ControlStyle Height="100px" Width="100px"></ControlStyle>

        
                                    </asp:ImageField>
                                    <asp:BoundField DataField="Name" HeaderText="Company Name" SortExpression="Name"></asp:BoundField>
                                    
                                     <asp:BoundField DataField="Name1" HeaderText="Advert Name" SortExpression="Name1"></asp:BoundField>
                                    <asp:BoundField DataField="ItemType" HeaderText="ItemType" SortExpression="ItemType" ></asp:BoundField>
                             
                               
                                    <asp:BoundField DataField="StartDate" HeaderText="StartDate" SortExpression="StartDate" DataFormatString="{0:D}"></asp:BoundField>
                                    <asp:BoundField DataField="EndDate" HeaderText="EndDate" SortExpression="EndDate" DataFormatString="{0:D}"></asp:BoundField>
                                    

                                     <asp:templatefield headertext="View">
                                        <itemtemplate>

                                            <asp:LinkButton ID="viewBtn" OnCommand="infoBtn_Command" runat="server" CommandName="AdInfo" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'>


                                        <i class="fas fa-eye"></i>

                                            


                                        </asp:LinkButton>
                                      
                                    </itemtemplate>
                                        <ControlStyle Height="50%" />
                                        <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                                        <ItemStyle Width="5%" HorizontalAlign="Center" Wrap="True" VerticalAlign="Middle" />
                                  </asp:templatefield>


                                    <asp:templatefield headertext="Update">
                                        <itemtemplate>
                                 
                                        <asp:LinkButton ID="editBtn" OnCommand="editBtn_Command" runat="server" CommandName="AdUpdateInfo" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'>

                                        <i class="fa fa-edit"></i>
                                            

                                            </asp:LinkButton>
                                  
                                         </itemtemplate>
                                        <ControlStyle Height="50%" />
                                        <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                                        <ItemStyle Width="5%" HorizontalAlign="Center" Wrap="True" VerticalAlign="Middle" />
                                  </asp:templatefield>
                                    

                                    <asp:templatefield headertext="Delete">

                                         
                                        <itemtemplate>

                                            
                                        <asp:LinkButton ID="DeleteBtn"  OnClientClick="return deleteFunction();"  OnCommand="btnDelete_Command" runat="server" CommandName="DeleteAdMessage" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'>

                                             <i class="fa fa-trash"></i>
                                             

                                             </asp:LinkButton>
                                      

                                                                                                             
                                         </itemtemplate>
                                        <ControlStyle Height="50%" />
                                        <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                                        <ItemStyle Width="5%" HorizontalAlign="Center" Wrap="True" VerticalAlign="Middle" />
                                  </asp:templatefield>

                                 


                                </Columns>
                                <EditRowStyle HorizontalAlign="Center" CssClass="GridViewEditRow" />
                                <EmptyDataRowStyle HorizontalAlign="Center" />
                                <FooterStyle BackColor="#CCCCCC" HorizontalAlign="Center" />
                                <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                                <PagerStyle BackColor="#999999"  ForeColor="Black" HorizontalAlign="left"  />
                                <RowStyle Height="20px" Width="30px" HorizontalAlign="Center" />
                                <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                                <SortedAscendingCellStyle BackColor="#F1F1F1" HorizontalAlign="Center" />
                                <SortedAscendingHeaderStyle BackColor="#808080" HorizontalAlign="Center" />
                                <SortedDescendingCellStyle BackColor="#CAC9C9" HorizontalAlign="Center" />
                                <SortedDescendingHeaderStyle BackColor="#383838" HorizontalAlign="Center" />
                            </asp:GridView>
                            <asp:Label ID="Label1" style="color:darkslateblue" runat="server" Text="Label"></asp:Label>
                            <%--                            </table>--%>
                        </div>
                        <!-- /.table-responsive -->
                    </div>
                </div>
            </div>
     





        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Targeted_Marketing_DisplayConnectionString %>" SelectCommand="SELECT [Advertisement].AdvID,[Company].Name, [Advertisement].Name, [Advertisement].Item, [Advertisement].ItemType,[StartDate], [EndDate]FROM [Advertisement] inner join [Company] on Company.CompanyID=[Advertisement].CompanyID where [Advertisement].status=1 and [Company].status=1" FilterExpression="Name LIKE '%{0}%' OR Item LIKE '%{0}%' OR Name1  LIKE '%{0}%' OR ItemType LIKE '%{0}%' OR convert(StartDate,'System.String') LIKE '%{0}%' OR convert(EndDate,'System.String') LIKE '%{0}%' ">
            <FilterParameters>
                                            <asp:ControlParameter ControlID="txtSearch" Name="Name" PropertyName="Text" />
                                            <asp:ControlParameter ControlID="txtSearch" Name="Item" PropertyName="Text" />                                            
                                            <asp:ControlParameter ControlID="txtSearch" Name="Name1" PropertyName="Text" />
                                            <asp:ControlParameter ControlID="txtSearch" Name="ItemType" PropertyName="Text" />
                                             <asp:ControlParameter ControlID="txtSearch" Name="StartDate" PropertyName="Text" />
                                            <asp:ControlParameter ControlID="txtSearch" Name="EndDate" PropertyName="Text" />
                                        </FilterParameters>        
            
       </asp:SqlDataSource>
      

    </form>
</asp:Content>
