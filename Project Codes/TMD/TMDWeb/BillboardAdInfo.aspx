<%@ Page Language="C#" MasterPageFile="~/Template.Master" AutoEventWireup="true" CodeFile="BillboardAdInfo.aspx.cs" Inherits="targeted_marketing_display.BillboardAdInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
    function LoadDiv(url) {
    var img = new Image();
    var bcgDiv = document.getElementById("divBackground");
    var imgDiv = document.getElementById("divImage");
    var imgFull = document.getElementById("imgFull");
    var imgLoader = document.getElementById("imgLoader");
    imgLoader.style.display = "block";
    img.onload = function () {
        imgFull.src = img.src;
        imgFull.style.display = "block";
        imgLoader.style.display = "none";
   };
    img.src = url;
    var width = document.body.clientWidth;
    if (document.body.clientHeight > document.body.scrollHeight) {
        bcgDiv.style.height = document.body.clientHeight + "px";
    }
    else {
        bcgDiv.style.height = document.body.scrollHeight + "px";
    }
    imgDiv.style.left = (width - 650) / 2 + "px";
    imgDiv.style.top = "20px";
    bcgDiv.style.width = "100%";
 
    bcgDiv.style.display = "block";
    imgDiv.style.display = "block";
    return false;
}
function HideDiv() {
    var bcgDiv = document.getElementById("divBackground");
    var imgDiv = document.getElementById("divImage");
    var imgFull = document.getElementById("imgFull");
    if (bcgDiv != null) {
        bcgDiv.style.display = "none";
        imgDiv.style.display = "none";
        imgFull.style.display = "none";
    }
}
</script>
    <form runat="server">       
            

          <div class="row">
                    <div class="col-lg-12">
                        <h1 class="page-header">Billboard Advertisement Listing<asp:label runat="server" ID="BillboardCodelabel"></asp:label></h1>
                        <asp:label runat="server" id="rowCountLabel" Font-Bold="true" text="Total:" /><asp:literal runat="server" id="rowcounttext"></asp:literal>
                       
                    </div>
                    <!-- /.col-lg-12 -->
                </div>


        

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
                
                </div>

        <asp:GridView ID="GridView1" runat="server" CssClass="table table-striped table-bordered table-hover"   CellPadding ="3" ForeColor="Black" GridLines="Vertical" Height="100%" Width="100%" 
            OnPreRender="GridView1_PreRender" AllowPaging="True"  OnPageIndexChanging="GridView1_PageIndexChanging" AutoGenerateColumns="False" DataKeyNames="BillboardCode" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" >
            <AlternatingRowStyle BackColor="#CCCCCC" />
            <Columns>
                                    
    
                                  <asp:TemplateField HeaderText="Advertisement">
                                       
                                        <ItemTemplate>
                                          
                                        
                                      <asp:ImageButton ID="Image1" runat="server" ImageUrl='<%# Eval("Item") %>' OnClientClick="return LoadDiv(this.src);" Visible='<%# Eval("ItemType").ToString() =="image" %>'  />
                                      
                                        <video width="150" height="150" runat="server" controls visible='<%# Eval("ItemType").ToString()!="image" %>'>  
                                            <source runat="server" src='<%#Eval("Item")%>' type="video/mp4" visible='<%# Eval("ItemType").ToString()!="image" %>' >  
                                        </video>  
                                           
                                              
                                            </ItemTemplate>
                                        <controlstyle height="150px" width="150px"  />
                                        </asp:TemplateField>
        
                                   
                                    <asp:BoundField DataField="BillboardCode" HeaderText="Billboard Code" sortExpression="BillboardCode" Visible="false" ></asp:BoundField>
                                    <asp:BoundField DataField="Name" HeaderText="Advert Name" SortExpression="Name"></asp:BoundField>
                                    
                                     
                                    <asp:BoundField DataField="ItemType" HeaderText="ItemType" SortExpression="ItemType"></asp:BoundField>
                             
                               
                                    <asp:BoundField DataField="StartDate" HeaderText="StartDate" SortExpression="StartDate" DataFormatString="{0:D}"></asp:BoundField>
                                    <asp:BoundField DataField="EndDate" HeaderText="EndDate" SortExpression="EndDate" DataFormatString="{0:D}"> </asp:BoundField>
                                   

            </Columns>
           <EditRowStyle HorizontalAlign="Center" CssClass="GridViewEditRow" BackColor="#999999" />
                                <EmptyDataRowStyle HorizontalAlign="Center" />
                                <FooterStyle BackColor="#CCCCCC" HorizontalAlign="Center" Font-Bold="True" />
                                <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                                <PagerStyle BackColor="#999999"  ForeColor="Black" HorizontalAlign="left"  />
                                <RowStyle Height="20px" Width="30px" HorizontalAlign="Center" BackColor="#F7F6F3" />
                                <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                                <SortedAscendingCellStyle BackColor="#F1F1F1" HorizontalAlign="Center" />
                                <SortedAscendingHeaderStyle BackColor="#808080" HorizontalAlign="Center" />
                                <SortedDescendingCellStyle BackColor="#CAC9C9" HorizontalAlign="Center" />
                                <SortedDescendingHeaderStyle BackColor="#383838" HorizontalAlign="Center" />
        </asp:GridView>
        
 <!--       <asp:objectdatasource id="ObjectDataSource1" runat="server" FilterExpression="Name LIKE '%{0}%' OR Item LIKE '%{0}%' OR Name1  LIKE '%{0}%' OR ItemType LIKE '%{0}%' OR convert(StartDate,'System.String') LIKE '%{0}%' OR convert(EndDate,'System.String') LIKE '%{0}%' ">
            <Filterparameters>
               <asp:ControlParameter ControlID="txtSearch" Name="Name" PropertyName="Text" />
               <asp:ControlParameter ControlID="txtSearch" Name="Item" PropertyName="Text" />                                            
               <asp:ControlParameter ControlID="txtSearch" Name="BillboardCode" PropertyName="Text" />
               <asp:ControlParameter ControlID="txtSearch" Name="ItemType" PropertyName="Text" />
               <asp:ControlParameter ControlID="txtSearch" Name="StartDate" PropertyName="Text" />
               <asp:ControlParameter ControlID="txtSearch" Name="EndDate" PropertyName="Text" />
            </Filterparameters>
        </asp:objectdatasource>
 -->
        
    
            


          <p>
 
        
       
            

          <asp:Label ID="ErrorMessage" runat="server" visible="false" Text="No Advertisements are to be displayed in this Billboard for now."></asp:Label>
 
        
       
            

          </p>
 
        <asp:Label ID="LabelPaging" style="color:darkslateblue" runat="server" Text="Label"></asp:Label>
       
            


    </form>
</asp:Content>
