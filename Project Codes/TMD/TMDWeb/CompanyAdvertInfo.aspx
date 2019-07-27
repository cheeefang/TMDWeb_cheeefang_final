<%@ Page Language="C#" MasterPageFile="~/Template.Master" AutoEventWireup="true" CodeFile="CompanyAdvertInfo.aspx.cs" Inherits="targeted_marketing_display.CompanyAdvertInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <style type="text/css">
        #divImage
{
    display: none;
    z-index: 1000;
    position: fixed;
    top: 0;
    left: 0;
    background-color: White;
    height: 550px;
    width: 600px;
    padding: 3px;
    border: solid 1px black;
}
#videoDog{
     object-fit: cover;
}
#vidDiv{

}
        </style>
    <form runat="server">       
            

 
          <div class="row">
                    <div class="col-lg-12">
                        <h1 class="page-header">Company Advertisement Listing<asp:label runat="server" id="company_name_label"></asp:label>
                        </h1>
                         <asp:label runat="server" id="labelIndustry" Font-Bold="true" text="Industry:" /><asp:literal runat="server" id="industrytext"></asp:literal>
                        </br>
                        <asp:label runat="server" id="rowCountLabel"  Font-Bold="true" text="Total:" /><asp:literal runat="server" id="counttext"></asp:literal>
                        <asp:label runat="server" id="ErrorHandler" visible="false"></asp:label>
                    </div>
                    <!-- /.col-lg-12 -->
                </div>


          <div class="row">
                    <div class="col-lg-6">
                   <div class="input-group custom-search-form" style="width: 50%">
                  <div style="padding: 20px; float: left; width:30%;">
                                          <p class="input-group" style="width:350px;margin-left:-20px;">
                                        <asp:TextBox ID="txtSearch" class="form-control" runat="server" placeholder="Search..." OnTextChanged="Search" AutoPostBack="true"></asp:TextBox>
                                        <%--<input type="submit" id="btSubmit" runat="server" />--%>
                                        <span class="input-group-btn" >
                                            <asp:LinkButton runat="server" class="btn btn-default" ID="btnRun" style="height:34px;" Text="<i class='fa fa-search'></i>" onclick="btnRun_click"/>
                                       </span>
                                            </p>
                                    </div>
                            </div>
               </div>
                
                </div>







        <asp:GridView ID="GridView1" runat="server" CssClass="table table-striped table-bordered table-hover"   CellPadding ="3" ForeColor="Black" GridLines="Vertical" Height="100%" Width="100%" OnPageIndexChanging="GridView1_PageIndexChanging" OnPreRender="GridView1_PreRender" AllowPaging="True" AutoGenerateColumns="False" DataKeyNames="Company" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" >
            <AlternatingRowStyle BackColor="#CCCCCC" />
            <Columns>
                

                                  
                                    <asp:TemplateField HeaderText="Advertisement">
                                       
                                        <ItemTemplate>
                                          
                                        
                                      <asp:ImageButton ID="Image1" runat="server" ImageUrl='<%# Eval("Item") %>' OnClientClick="return LoadDiv(this.src);" Visible='<%# Eval("ItemType").ToString() =="image" %>'  />
                                      <div id="vidDiv" runat="server">
                                        <video ClientIDMode="static" id="videoDog" width="200" height="200" runat="server" controls visible='<%# Eval("ItemType").ToString()!="image" %>'>  
                                            <source runat="server" src='<%#Eval("Item")%>' type="video/mp4" visible='<%# Eval("ItemType").ToString()!="image" %>' >  
                                        </video>  
                                           </div>
                                              
                                            </ItemTemplate>
                                        <controlstyle width="200px" height="200px"  />
                                        <ItemStyle Width="200px" height="200px" />
                                        </asp:TemplateField>
                                    <asp:BoundField DataField="Company" visible="false" HeaderText="Company Name" SortExpression="Name"></asp:BoundField>
                                    
                                     <asp:BoundField DataField="Name" HeaderText="Advert Name" SortExpression="Name1"></asp:BoundField>
                                    <asp:BoundField DataField="ItemType" HeaderText="ItemType" SortExpression="ItemType"></asp:BoundField>
                             
                               
                                    <asp:BoundField DataField="StartDate" HeaderText="StartDate" SortExpression="StartDate" DataFormatString="{0:D}"></asp:BoundField>
                                    <asp:BoundField DataField="EndDate" HeaderText="EndDate" SortExpression="EndDate" DataFormatString="{0:D}"></asp:BoundField>
                                   
               
                

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
        <FilterParameters>
                                            <asp:ControlParameter ControlID="txtSearch" Name="Name" PropertyName="Text" />
                                            <asp:ControlParameter ControlID="txtSearch" Name="Item" PropertyName="Text" />                                            
                                            <asp:ControlParameter ControlID="txtSearch" Name="Company" PropertyName="Text" />
                                            <asp:ControlParameter ControlID="txtSearch" Name="ItemType" PropertyName="Text" />
                                             <asp:ControlParameter ControlID="txtSearch" Name="StartDate" PropertyName="Text" />
                                            <asp:ControlParameter ControlID="txtSearch" Name="EndDate" PropertyName="Text" />
                                        </FilterParameters>        

          <p>
 
        
       
            

         
 
        
       
            

          </p>
 
        <asp:Label ID="LabelPaging" style="color:darkslateblue" runat="server" Text="Label"></asp:Label>
       
            


    </form>
</asp:Content>
