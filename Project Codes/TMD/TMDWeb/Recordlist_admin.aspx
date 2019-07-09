<%@ Page Title="" Language="C#" MasterPageFile="~/Template.Master" AutoEventWireup="true" CodeFile="Recordlist_admin.aspx.cs" Inherits="targeted_marketing_display.Recordlist_admin" %>
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
                    <a href="AdvCreate.aspx" class="btn btn-primary nextBtn pull-right" type="button"> <b> New Advertisement </b> </a>
                  
                </div>
                 </div>
        
            <div id="all" runat="server">
                <div class="row">
                    <div class="col-lg-12">

                        <div class="table-responsive">
                            <%--                        <table class="table table-striped table-bordered table-hover" style="width: 100%">--%>
                            <asp:GridView ID="GridView1" CssClass="table table-striped table-bordered table-hover" runat="server" AutoGenerateColumns="False" Width="100%" 
                                BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px"
                                CellPadding="3" DataSourceID="SqlDataSource1" AllowPaging="True" HorizontalAlign="Center" DataKeyNames="AdvID"  OnPreRender="GridView1_PreRender">
                                <AlternatingRowStyle HorizontalAlign="Center" />
                                <Columns>




                                    <%-- <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status" ReadOnly="True">
                                    </asp:BoundField>--%>



                                    <asp:BoundField DataField="AdvID" HeaderText="AdvID" SortExpression="AdvID" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" InsertVisible="False" ReadOnly="True">
<HeaderStyle CssClass="hiddencol"></HeaderStyle>

<ItemStyle CssClass="hiddencol"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:ImageField DataImageUrlField="Item" HeaderText="Advert Image" ControlStyle-Width="100" ControlStyle-Height = "100"> 
        
                                    </asp:ImageField>
                                    <asp:BoundField DataField="Name" HeaderText="Company Name" SortExpression="Name"></asp:BoundField>
                                    
                                     <asp:BoundField DataField="Name1" HeaderText="Advert Name" SortExpression="Name1"></asp:BoundField>
                                    <asp:BoundField DataField="ItemType" HeaderText="ItemType" SortExpression="ItemType"></asp:BoundField>
                             
                               
                                    <asp:BoundField DataField="StartDate" HeaderText="StartDate" SortExpression="StartDate"></asp:BoundField>
                                    <asp:BoundField DataField="EndDate" HeaderText="EndDate" SortExpression="EndDate"></asp:BoundField>
                                   


                                 


                                </Columns>
                                <EditRowStyle HorizontalAlign="Center" CssClass="GridViewEditRow" />
                                <EmptyDataRowStyle HorizontalAlign="Center" />
                                <FooterStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Center" />
                                <HeaderStyle BackColor="#848c8E" Font-Bold="True" ForeColor="#f1f2ee" HorizontalAlign="Center" />
                                <PagerStyle BackColor="White"  ForeColor="#85C1E9" HorizontalAlign="Left"  />
                                <RowStyle ForeColor="#435058" Height="20px" Width="30px" HorizontalAlign="Center" />
                                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                                <SortedAscendingCellStyle BackColor="#F1F1F1" HorizontalAlign="Center" />
                                <SortedAscendingHeaderStyle BackColor="#007DBB" HorizontalAlign="Center" />
                                <SortedDescendingCellStyle BackColor="#CAC9C9" HorizontalAlign="Center" />
                                <SortedDescendingHeaderStyle BackColor="#00547E" HorizontalAlign="Center" />
                            </asp:GridView>
                            <asp:Label ID="Label1" style="color:darkslateblue" runat="server" Text="Label"></asp:Label>
                            <%--                            </table>--%>
                        </div>
                        <!-- /.table-responsive -->
                    </div>
                </div>
            </div>
     




        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Targeted_Marketing_DisplayConnectionString %>" SelectCommand="SELECT [Advertisement].AdvID,[Company].Name, [Advertisement].Name, [Advertisement].Item, [Advertisement].ItemType,[StartDate], [EndDate]FROM [Advertisement] inner join [Company] on Company.CompanyID=[Advertisement].CompanyID where [Advertisement].status=1" FilterExpression="Name LIKE '%{0}%' OR Item LIKE '%{0}%' OR convert(CreatedBy,'System.String') LIKE '%{0}%'">
            <FilterParameters>
                                            <asp:ControlParameter ControlID="txtSearch" Name="Name" PropertyName="Text" />
                                            <asp:ControlParameter ControlID="txtSearch" Name="Item" PropertyName="Text" />                                            
                                            <asp:ControlParameter ControlID="txtSearch" Name="CreatedBy" PropertyName="Text" />

                                        </FilterParameters>        
            
       </asp:SqlDataSource>
      





        <%--  <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT DISTINCT [ItemType] FROM [Advertisement]"></asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT DISTINCT [Status] FROM [Advertisement]"></asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSource4" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT DISTINCT [CreatedBy] FROM [Advertisement]"></asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSource5" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT DISTINCT [LastUpdOn], [LastUpdBy] FROM [Advertisement]"></asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSource6" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT * FROM [Advertisement] WHERE ([ItemType] = @ItemType)">
            <SelectParameters>
                <asp:ControlParameter ControlID="DropDownList2" Name="ItemType" PropertyName="SelectedValue" Type="String" />
            </SelectParameters>
            </asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSource7" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT * FROM [Advertisement] WHERE ([Status] = @Status)">
            <SelectParameters>
                <asp:ControlParameter ControlID="DropDownList2" Name="Status" PropertyName="SelectedValue" Type="Int32" />
            </SelectParameters>
            </asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSource8" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT * FROM [Advertisement] WHERE ([CreatedBy] = @CreatedBy)">
            <SelectParameters>
                <asp:ControlParameter ControlID="DropDownList2" Name="CreatedBy" PropertyName="SelectedValue" Type="Int32" />
            </SelectParameters>
            </asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSource9" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT * FROM [Advertisement] WHERE ([LastUpdBy] = @LastUpdBy)">
            <SelectParameters>
                <asp:ControlParameter ControlID="DropDownList2" Name="LastUpdBy" PropertyName="SelectedValue" Type="Int32" />
            </SelectParameters>
            </asp:SqlDataSource>

       
            
      
               <div id="selected" runat="server">
                <div class="row">
                    <div class="col-lg-12">

                        <div class="table-responsive">--%>
        <%--                        <table class="table table-striped table-bordered table-hover" style="width: 100%">--%>
        <%-- <asp:GridView ID="GridView2" CssClass="table table-striped table-bordered table-hover" runat="server" AutoGenerateColumns="False" Height="200px" Width="100%" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3">
                                <FooterStyle BackColor="White" ForeColor="#000066" />
                                <HeaderStyle BackColor="#848c8E" Font-Bold="True" ForeColor="#f1f2ee" />
                                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                <RowStyle ForeColor="#435058" Height="20px" Width="30px" />
                                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                <SortedDescendingHeaderStyle BackColor="#00547E" />
                            </asp:GridView>--%>
        <%--                            </table>--%>
        <%--    </div>
                        <!-- /.table-responsive -->
                    </div>
                </div>
            </div>
        --%>
    </form>
</asp:Content>
