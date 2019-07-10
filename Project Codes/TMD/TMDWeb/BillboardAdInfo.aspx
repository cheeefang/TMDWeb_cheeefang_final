<%@ Page Language="C#" MasterPageFile="~/Template.Master" AutoEventWireup="true" CodeFile="BillboardAdInfo.aspx.cs" Inherits="targeted_marketing_display.BillboardAdInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form runat="server">       
            

 
          <div class="row">
                    <div class="col-lg-12">
                        <h1 class="page-header">Billboard Advertisement Listing</h1>
                    </div>
                    <!-- /.col-lg-12 -->
                </div>
        <asp:GridView ID="GridView1" runat="server" CssClass="table table-striped table-bordered table-hover"   CellPadding ="3" ForeColor="#333333" GridLines="None" Height="100%" Width="100%" OnPreRender="GridView1_PreRender" AllowPaging="True"  OnPageIndexChanging="GridView1_PageIndexChanging" >
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
                <asp:ImageField DataImageUrlField="Item"  HeaderText="Advert Image" ControlStyle-Width="100" ControlStyle-Height = "100">
                </asp:ImageField>
            </Columns>
            <EditRowStyle BackColor="#999999" />
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#848C8E" Font-Bold="True" ForeColor="#F1F2EE"  />
            <PagerStyle BackColor="white" ForeColor="White" HorizontalAlign="left" />
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#E9E7E2" />
            <SortedAscendingHeaderStyle BackColor="#506C8C" />
            <SortedDescendingCellStyle BackColor="#FFFDF8" />
            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
        </asp:GridView>
 
        
    
            


          <p>
 
        
       
            

          <asp:Label ID="ErrorMessage" runat="server" visible="false" Text="No Advertisements are to be displayed in this Billboard for now."></asp:Label>
 
        
       
            

          </p>
 
        <asp:Label ID="LabelPaging" style="color:darkslateblue" runat="server" Text="Label"></asp:Label>
       
            


    </form>
</asp:Content>
