<%@ Page Language="C#" MasterPageFile="~/Template.Master" AutoEventWireup="true" CodeFile="BillboardAdInfo.aspx.cs" Inherits="targeted_marketing_display.BillboardAdInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form runat="server">       
            

          <div class="row">
                    <div class="col-lg-12">
                        <h1 class="page-header">Billboard Advertisement Listing<asp:label runat="server" id="rowCountLabel"></asp:label></h1>
                    </div>
                    <!-- /.col-lg-12 -->
                </div>


        

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
                
                </div>

        <asp:GridView ID="GridView1" runat="server" CssClass="table table-striped table-bordered table-hover"   CellPadding ="3" ForeColor="#333333" GridLines="None" Height="100%" Width="100%" 
            OnPreRender="GridView1_PreRender" AllowPaging="True"  OnPageIndexChanging="GridView1_PageIndexChanging" AutoGenerateColumns="False" >
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
               
    
                                    <asp:ImageField DataImageUrlField="Item" HeaderText="Advert Image" ControlStyle-Width="100" ControlStyle-Height = "100" /> 
        
                            
                                    <asp:BoundField DataField="BillboardCode" HeaderText="Billboard Code" sortExpression="BillboardCode" ></asp:BoundField>
                                    <asp:BoundField DataField="Name" HeaderText="Advert Name" SortExpression="Name"></asp:BoundField>
                                    
                                     
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
 
        
       
            

          <asp:Label ID="ErrorMessage" runat="server" visible="false" Text="No Advertisements are to be displayed in this Billboard for now."></asp:Label>
 
        
       
            

          </p>
 
        <asp:Label ID="LabelPaging" style="color:darkslateblue" runat="server" Text="Label"></asp:Label>
       
            


    </form>
</asp:Content>
