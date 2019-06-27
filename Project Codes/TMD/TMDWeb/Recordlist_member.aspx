<%@ Page Title="" Language="C#" MasterPageFile="~/Template.Master" AutoEventWireup="true" CodeFile="Recordlist_member.aspx.cs" Inherits="targeted_marketing_display.AdvrecordView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
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
                <div class="col-lg-12" style="margin-top: 15px;">
                    <h1 class="page-header">Advertisement Listing</h1>
                </div>
                <!-- /.col-lg-12 -->
            </div>
            <!-- /.row -->
            <br />
            <div class="row">
                <div class="col-lg-12">
                    <a href="AdvCreate.aspx" class="btn btn-primary nextBtn pull-right" type="button">New Advertisement </a>
                </div>
            </div>

            <br>

     

  

            <div class="input-group custom-search-form">
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

            <br>
            <div id="all" runat="server">
                <div class="row">
                    <div class="col-lg-12">

                        <div class="table-responsive">
                        
                            <asp:GridView ID="GridView1" CssClass="table table-striped table-bordered table-hover" runat="server" AutoGenerateColumns="False" Width="100%" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" DataSourceID="SqlDataSource2" AllowPaging="True" HorizontalAlign="Center" DataKeyNames="AdvID" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
                                <AlternatingRowStyle HorizontalAlign="Center" />
                                <Columns>






                                    <asp:BoundField DataField="AdvID" HeaderText="AdvID" SortExpression="AdvID" InsertVisible="False" ReadOnly="True"></asp:BoundField>
                                    <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name"></asp:BoundField>

                                    <asp:BoundField DataField="Item" HeaderText="Item" SortExpression="Item"></asp:BoundField>
                                    <asp:BoundField DataField="ItemType" HeaderText="ItemType" SortExpression="ItemType"></asp:BoundField>
                                    <asp:BoundField DataField="Duration" HeaderText="Duration" SortExpression="Duration"></asp:BoundField>
                                    <asp:BoundField DataField="companyID" HeaderText="companyID" SortExpression="companyID"  ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol">
                                    </asp:BoundField>




                          

                                     <asp:BoundField DataField="StartDate" HeaderText="StartDate" SortExpression="StartDate" />
                                    <asp:BoundField DataField="EndDate" HeaderText="EndDate" SortExpression="EndDate" />
                                    <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status" />
                                    <asp:BoundField DataField="CreatedBy" HeaderText="CreatedBy" SortExpression="CreatedBy" />
                                    <asp:BoundField DataField="CreatedOn" HeaderText="CreatedOn" SortExpression="CreatedOn" />




                                </Columns>
                                <EditRowStyle HorizontalAlign="Center" CssClass="GridViewEditRow" />
                                <EmptyDataRowStyle HorizontalAlign="Center" />
                                <FooterStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Center" />
                                <HeaderStyle BackColor="#848c8E" Font-Bold="True" ForeColor="#f1f2ee" HorizontalAlign="Center" />
                                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Center" />
                                <RowStyle ForeColor="#435058" Height="20px" Width="30px" HorizontalAlign="Center" />
                                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                                <SortedAscendingCellStyle BackColor="#F1F1F1" HorizontalAlign="Center" />
                                <SortedAscendingHeaderStyle BackColor="#007DBB" HorizontalAlign="Center" />
                                <SortedDescendingCellStyle BackColor="#CAC9C9" HorizontalAlign="Center" />
                                <SortedDescendingHeaderStyle BackColor="#00547E" HorizontalAlign="Center" />
                            </asp:GridView>
                            <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:Targeted_Marketing_DisplayConnectionString %>" SelectCommand="SELECT [AdvID], [Name], [Item], [ItemType], [Duration], [companyID], [StartDate], [EndDate], [Status], [CreatedBy], [CreatedOn] FROM [Advertisement]"></asp:SqlDataSource>
                            <%--                            </table>--%>
                        </div>
                        <!-- /.table-responsive -->
                    </div>
                </div>
            </div>
     




        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Targeted_Marketing_DisplayConnectionString %>"  SelectCommand="SELECT * FROM [Advertisement] WHERE ([Status] = @Status)" FilterExpression="Name LIKE '%{0}%' OR Item LIKE '%{0}%' OR convert(CreatedBy,'System.String') LIKE '%{0}%'" DeleteCommand="UPDATE [Advertisement] SET Status=0 where AdvID = @AdvID">
            <FilterParameters>
                                            <asp:ControlParameter ControlID="txtSearch" Name="Name" PropertyName="Text" />
                                            <asp:ControlParameter ControlID="txtSearch" Name="Item" PropertyName="Text" />                                            
                                            <asp:ControlParameter ControlID="txtSearch" Name="CreatedBy" PropertyName="Text" />

                                        </FilterParameters>        
            
             <DeleteParameters>
                    <asp:ControlParameter ControlID="GridView1" Name="AdvID" PropertyName="SelectedDataKey" />
                </DeleteParameters>
                <SelectParameters>
                    <asp:QueryStringParameter DefaultValue="1" Name="Status" QueryStringField="1" Type="Int32" />
                </SelectParameters>
       </asp:SqlDataSource>
      





    </form>
</asp:Content>

