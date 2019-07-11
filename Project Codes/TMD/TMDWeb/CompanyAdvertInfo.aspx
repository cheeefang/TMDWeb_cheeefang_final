<%@ Page Language="C#" MasterPageFile="~/Template.Master" AutoEventWireup="true" CodeFile="CompanyAdvertInfo.aspx.cs" Inherits="targeted_marketing_display.CompanyAdvertInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form runat="server">       
            

 
          <div class="row">
                    <div class="col-lg-12">
                        <h1 class="page-header">Company Advertisement Listing<asp:label runat="server" id="rowCountLabel"></asp:label>
                        </h1>
                    </div>
                    <!-- /.col-lg-12 -->
                </div>

        <asp:GridView ID="GridView1" runat="server" CssClass="table table-striped table-bordered table-hover"   CellPadding ="3" ForeColor="#333333" GridLines="None" Height="100%" Width="100%" OnPageIndexChanging="GridView1_PageIndexChanging" OnPreRender="GridView1_PreRender" AllowPaging="True" AutoGenerateColumns="False" >
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
                

                                  
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
   
          <p>
 
        
       
            

          <asp:Label ID="ErrorMessage" runat="server" visible="false" Text="This Company has yet to create an Advertisement."></asp:Label>
 
        
       
            

          </p>
 
        <asp:Label ID="LabelPaging" style="color:darkslateblue" runat="server" Text="Label"></asp:Label>
       
            


    </form>
</asp:Content>
