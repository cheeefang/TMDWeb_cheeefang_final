<%@ Page Title="" Language="C#" MasterPageFile="~/Template.Master" AutoEventWireup="true" CodeFile="AdrecordView.aspx.cs" Inherits="targeted_marketing_display.AdrecordView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style100 {
           border-radius:5px;
           border-style:ridge;
           height:30px;
        }

        .auto-style101 {
        border-radius:5px;
        border-style:none;
        height:30px;
        }
        .auto-style2 {
            height: 42px;
        }
    </style>
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form runat="server">



       

                <!--button-->
                <div class="row">
                    <div class="col-lg-12" style="margin-top:15px;">
                        <h1 class="page-header">View Advertisement</h1>
                    </div>
                    <!-- /.col-lg-12 -->
                </div>
                <!-- /.row -->
                <br/>
                <div class="row">
                    <div class="col-lg-12">
                        <a href="AdvCreate.aspx" class="btn btn-primary nextBtn pull-right" type="button"> <b>New Advertisement </b> </a>
                    </div>
                </div>

               <br>
                <div id="all0" runat="server">
                    <div id="all1" runat="server" class="auto-style2">
                        Filter By:&nbsp;
                        <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" CssClass="auto-style100" Height="30px" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                             <asp:ListItem>--Select--</asp:ListItem>
                            <asp:ListItem>ItemType</asp:ListItem>
                            <asp:ListItem>Status</asp:ListItem>
                            <asp:ListItem>CreatedBy</asp:ListItem>
                            <asp:ListItem>LastUpdBy</asp:ListItem>
                        </asp:DropDownList>
                        &nbsp;
                        <asp:DropDownList ID="DropDownList2" runat="server" CssClass="auto-style100" Height="30px" OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged" OnTextChanged="DropDownList2_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:Button ID="Button1" runat="server" BackColor="#DCF763" ForeColor="#435058" Text="Refresh" Font-Bold="true" BorderStyle="None" CssClass="auto-style101" />
                    </div>
                </div>
                <br>
                <div id="all" runat="server">
                <div class="row">
                    <div class="col-lg-12">

                        <div class="table-responsive">
                            <%--                        <table class="table table-striped table-bordered table-hover" style="width: 100%">--%>
                            <asp:GridView ID="GridView1" CssClass="table table-striped table-bordered table-hover" runat="server" AutoGenerateColumns="False"  Width="100%" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                                DataSourceID="SqlDataSource1" AllowPaging="True" HorizontalAlign="Center" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" DataKeyNames="AdvID" OnPreRender="GridView1_PreRender" PageSize="10">
                                 <AlternatingRowStyle HorizontalAlign="Center" />
                                 <Columns>


                                   
                                    <asp:BoundField DataField="AdvID" HeaderText="AdvID" ReadOnly="True" SortExpression="AdvID" InsertVisible="False">
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name">
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Item" HeaderText="Item" SortExpression="Item">
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ItemType" HeaderText="ItemType" SortExpression="ItemType">
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Duration" HeaderText="Duration" SortExpression="Duration">
                                    </asp:BoundField>
                                      <asp:BoundField DataField="companyID" HeaderText="companyID" SortExpression="companyID">
                                    </asp:BoundField>
                                      <asp:BoundField DataField="StartDate" HeaderText="StartDate" SortExpression="StartDate">
                                    </asp:BoundField>
                                      <asp:BoundField DataField="EndDate" HeaderText="EndDate" SortExpression="EndDate">
                                    </asp:BoundField>
                                     <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status">
                                    </asp:BoundField>
                                     <asp:BoundField DataField="CreatedBy" HeaderText="CreatedBy" SortExpression="CreatedBy">
                                    </asp:BoundField>
                                     <asp:BoundField DataField="CreatedOn" HeaderText="CreatedOn" SortExpression="CreatedOn">
                                    </asp:BoundField>

                                     <asp:BoundField DataField="LastUpdBy" HeaderText="LastUpdBy" SortExpression="LastUpdBy" />

                                     <asp:BoundField DataField="LastUpdOn" HeaderText="LastUpdOn" SortExpression="LastUpdOn" />

                                </Columns>
                                 <EditRowStyle HorizontalAlign="Center" />
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
                            <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                            <%--                            </table>--%>
                        </div>
                        <!-- /.table-responsive -->
                    </div>
                </div>
            </div>
        </div>

       
                 

        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Targeted_Marketing_DisplayConnectionString %>" SelectCommand="SELECT * FROM [Advertisement]"></asp:SqlDataSource>

       
            
        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:Targeted_Marketing_DisplayConnectionString %>" SelectCommand="SELECT DISTINCT [ItemType] FROM [Advertisement]"></asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:Targeted_Marketing_DisplayConnectionString %>" SelectCommand="SELECT DISTINCT [Status] FROM [Advertisement]"></asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSource4" runat="server" ConnectionString="<%$ ConnectionStrings:Targeted_Marketing_DisplayConnectionString %>" SelectCommand="SELECT DISTINCT [CreatedBy] FROM [Advertisement]"></asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSource5" runat="server" ConnectionString="<%$ ConnectionStrings:Targeted_Marketing_DisplayConnectionString %>" SelectCommand="SELECT DISTINCT [LastUpdOn], [LastUpdBy] FROM [Advertisement]"></asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSource6" runat="server" ConnectionString="<%$ ConnectionStrings:Targeted_Marketing_DisplayConnectionString %>" SelectCommand="SELECT * FROM [Advertisement] WHERE ([ItemType] = @ItemType)">
            <SelectParameters>
                <asp:ControlParameter ControlID="DropDownList2" Name="ItemType" PropertyName="SelectedValue" Type="String" />
            </SelectParameters>
            </asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSource7" runat="server" ConnectionString="<%$ ConnectionStrings:Targeted_Marketing_DisplayConnectionString %>" SelectCommand="SELECT * FROM [Advertisement] WHERE ([Status] = @Status)">
            <SelectParameters>
                <asp:ControlParameter ControlID="DropDownList2" Name="Status" PropertyName="SelectedValue" Type="Int32" />
            </SelectParameters>
            </asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSource8" runat="server" ConnectionString="<%$ ConnectionStrings:Targeted_Marketing_DisplayConnectionString %>" SelectCommand="SELECT * FROM [Advertisement] WHERE ([CreatedBy] = @CreatedBy)">
            <SelectParameters>
                <asp:ControlParameter ControlID="DropDownList2" Name="CreatedBy" PropertyName="SelectedValue" Type="Int32" />
            </SelectParameters>
            </asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSource9" runat="server" ConnectionString="<%$ ConnectionStrings:Targeted_Marketing_DisplayConnectionString %>" SelectCommand="SELECT * FROM [Advertisement] WHERE ([LastUpdBy] = @LastUpdBy)">
            <SelectParameters>
                <asp:ControlParameter ControlID="DropDownList2" Name="LastUpdBy" PropertyName="SelectedValue" Type="Int32" />
            </SelectParameters>
            </asp:SqlDataSource>

       
            
      
               <div id="selected" runat="server">
                <div class="row">
                    <div class="col-lg-12">

                        <div class="table-responsive">
                            <%--                        <table class="table table-striped table-bordered table-hover" style="width: 100%">--%>
                            <asp:GridView ID="GridView2" CssClass="table table-striped table-bordered table-hover" runat="server" AutoGenerateColumns="False" Height="200px" Width="100%" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3">
                                <FooterStyle BackColor="White" ForeColor="#000066" />
                                <HeaderStyle BackColor="#848c8E" Font-Bold="True" ForeColor="#f1f2ee" />
                                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                <RowStyle ForeColor="#435058" Height="20px" Width="30px" />
                                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                <SortedDescendingHeaderStyle BackColor="#00547E" />
                            </asp:GridView>
                            <%--                            </table>--%>
                        </div>
                        <!-- /.table-responsive -->
                    </div>
                </div>

        
     

    </form>
</asp:Content>
