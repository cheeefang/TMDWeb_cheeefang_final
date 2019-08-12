<%@ Page Title="" Language="C#" MasterPageFile="~/Template.Master" AutoEventWireup="true" CodeFile="AdFeedback.aspx.cs" Inherits="targeted_marketing_display.AdFeedback" %>
<%@ Register assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.DataVisualization.Charting" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .panel{
            width:100% !important;
            height:100% !important;
        }
        .chartFb
        {
            width:100% !important;
            height:50% !important;
        }
        .calendar{
            position:relative;
            left:741px;
            top:-34px;
        }
        .pickdate{

        }
        .fontfont{
            position:relative;
            left:710px;
            top:-20px
        }
        #videoDog{
     object-fit: cover;
}
#vidDiv{

}
    </style>  
    <script>  
        $(function ()
        {
            $('.pickdate').datepicker(
                {
                    dateFormat: 'dd/mm/yy',
                    changeMonth: true,
                    changeYear: true,
                    yearRange: '1950:2100'
                });
        })
        function showComModal() {
            $('#ComModal').modal('show');
        }
        function showAdvModal() {
            $('#AdvModal').modal('show');
        }
        function showBbModal() {
            $('#BbModal').modal('show');
        }
        function showVadDateModal() {
            $('#VadDateModal').modal('show');
        }
        function showVadModal() {
            $('#VadModal').modal('show');
        }
        function showVadModal2() {
            $('#VadModal2').modal('show');
        }
        
    </script>
  <script type="text/javascript">
      function RadioCheckAdvert(rb) {

        var gv = document.getElementById("<%=gvAdv.ClientID%>");

        var rbs = gv.getElementsByTagName("input");

 

        var row = rb.parentNode.parentNode;

        for (var i = 0; i < rbs.length; i++) {

            if (rbs[i].type == "radio") {

                if (rbs[i].checked && rbs[i] != rb) {

                    rbs[i].checked = false;

                    break;

                }

            }

        }

      }    
      function RadioCheckBillboard(rb) {

        var gv = document.getElementById("<%=gvBb.ClientID%>");

        var rbs = gv.getElementsByTagName("input");

 

        var row = rb.parentNode.parentNode;

        for (var i = 0; i < rbs.length; i++) {

            if (rbs[i].type == "radio") {

                if (rbs[i].checked && rbs[i] != rb) {

                    rbs[i].checked = false;

                    break;

                }

            }

        }

    }    


</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server" ClientIDMode="Static">
    <form runat="server">
          <asp:ScriptManager ID="ScriptManager2" runat="server"></asp:ScriptManager>

     
                <!--button-->
                <div class="row">
                    <div class="col-lg-12">
                        <h1 class="page-header">Advertisement Feedback</h1>
                    </div>
                    <!-- /.col-lg-12 -->
                </div>
                <!-- /.row -->

                <br />
         <div runat="server" class="alert alert-danger" id="RadioButtonNull" visible="False">
                     <strong>Error!</strong> 
                    <asp:Label runat="server" ID="Label1">Please select a Radio Button</asp:Label>
                    <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
        
           <div runat="server" class="alert alert-danger" id="dateNull" visible="False">
                     <strong>Error!</strong> 
                    <asp:Label runat="server" ID="LabelError">Please Key in a Start Date and End Date</asp:Label>
                    <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
        
             <div class="row">
                <div class="col-lg-6">
                    <div class="form-group">
                        <label>From: </label>
                        <label style="color: red">*</label>
                        <asp:TextBox Class="form-control" ID="startDateTB" runat="server" TextMode="Date" AutoCompleteType="Disabled" autocomplete="off"></asp:TextBox>
                      
                       
                    </div>
                </div>

                <div class="col-lg-6">

                    <div class="form-group">
                        <label>To: </label>
                        <label style="color: red">*</label>
                        <asp:TextBox class="form-control" ID="endDateTB" runat="server" TextMode="Date" AutoCompleteType="Disabled" autocomplete="off"></asp:TextBox>&nbsp;
                         <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToCompare="startDateTB" ControlToValidate="endDateTB" ErrorMessage="invalid end date" Operator="GreaterThan" Type="Date" ForeColor="Red"></asp:CompareValidator>
                        
                    </div>
                </div>
             
            </div>
            <div class="row">
                <div class="col-lg-12">
                     <p style="font-weight:bold">Select Company:</p>
                </div>
            </div>
            <div class="row">
                <%--<asp:UpdatePanel ID="updatePanelCompany" runat="server">
                    <ContentTemplate>--%>

                  
                <div class ="col-lg-6">
                    <div class="form-group">
                        <asp:DropDownList ID="ddlCom" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlCom_SelectedIndexChanged" AutoPostBack="True" DataTextField="Name" DataValueField="Name"></asp:DropDownList>
                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Targeted_Marketing_DisplayConnectionString %>" SelectCommand="SELECT [Name] FROM [Company] where status=1"></asp:SqlDataSource>
                    </div>
                </div>
                         <%-- </ContentTemplate>
                                  <Triggers>
                <asp:AsyncPostBackTrigger ControlID="addAdv" EventName="Click" />
            </Triggers>
                </asp:UpdatePanel>--%>
                
                <div class="col-lg-6">
                    <div class="form-group">
                       
                        <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#AdvModal" style="width:30.7%"> <b>Select Advertisements: </b></button>

                        <asp:Label runat="server" Text="OR/AND" Font-Bold="true"></asp:Label>
                        <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#BbModal" style="width:30.7%"><b>Select Billboards:</b></button>
                    </div>
                </div>
                          
            </div>
            <div class="row">
                <div class="col-lg-6">
                    <div class="form-group">
                        <asp:Label runat="server" Text="Chart Type:" Font-Bold="true"></asp:Label>
                        <asp:RadioButton ID="rbNo" runat="server" OnCheckedChanged="rbNo_CheckedChanged" AutoPostBack="true"/> 
                        <asp:Label ID="lblNo" runat="server" Text="No. Of Pax"></asp:Label>
                        &nbsp
                       
                        <asp:RadioButton ID="rbAge" runat="server" OnCheckedChanged="rbAge_CheckedChanged" AutoPostBack="true"/> 
                        <asp:Label ID="lblAge" runat="server" Text="Age"></asp:Label>
                        &nbsp
                        <asp:RadioButton ID="rbGender" runat="server" OnCheckedChanged="rbGender_CheckedChanged" AutoPostBack="true"/>
                        <asp:Label ID="lblGender" runat="server" Text="Gender"></asp:Label>
                        &nbsp
                        <asp:RadioButton ID="rbEmotion" runat="server" OnCheckedChanged="rbEmotion_CheckedChanged" AutoPostBack="true"/> 
                        <asp:Label ID="lblEmotion" runat="server" Text="Emotion"></asp:Label>
                    </div>
                </div>
            </div>
                <div class="row">
                    <div class="col-lg-6">
                        <asp:Button ID="btnGen" runat="server"  Font-Bold="true" Text="Generate Chart" CssClass="btn btn-primary" OnClick="btnGen_Click" />
                    </div>
                </div>
        
            <div class="row" style="margin-top:30px">
                <div class="col-lg-12">
                    <div class="form-group">
                    <asp:Label ID="lblFbc" runat="server" Visible="false" Font-Size="X-Large" Font-Bold="true" CssClass="fontfont">Feedback Chart</asp:Label>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-3">

                </div>
                <div class="col-lg-6">
                    <div class="form-group">
                     
                    <asp:Chart ID="chartFb" class="chartFb" runat="server" ClientIDMode="Static" Palette="Bright">
                        <series>
                          
                            <asp:Series Name="Series1" Legend="Legend1">
                
                </asp:Series>
                        </series>
                                <chartareas>
                                    <asp:ChartArea Name="ChartArea1">
                                    </asp:ChartArea>
                                </chartareas>
                            </asp:Chart>
            
                    </div>
                </div>
                
                  <div class="col-lg-6">
                    <div class="form-group">
                        <asp:Label id="TitleDetailsLabel" text="Chart Data Details" runat="server" visible="false" Font-Bold="true"></asp:Label>
                        <br />
                        <asp:Label id="AdvertNameLabel" runat="server" visible="false"></asp:Label>
                        <asp:Label id="CompanyNameLabel" runat="server" visible="false"></asp:Label>
                        <asp:Label id="BillboardCodeLabel" runat="server" visible="false"></asp:Label>
                        </div>
                      </div>
                </div>
                <br />
            <div id="NoDataDiv" runat="server" visible="false" align="center">
                <asp:Label id="NoDataText" Font-Bold="true" runat="server"></asp:Label>
                <br />
                <img src="~/webicons/NoChartData.png" runat="server" id="NoDataYet" />
        </div>
        <div id="AdvModal" class="modal fade" role="dialog">
            <div class="modal-dialog modal-lg">

                <!-- Modal content-->
                <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h2 class="modal-title">Advertisements</h2>
                </div>
                <div class="modal-body">
                    <div class="row">

                    <div class="col-lg-12">

                        <div class="input-group custom-search-form" style="width: 50%">
                            <asp:textbox id="txtAdv" class="form-control" runat="server" placeholder="Search..."></asp:textbox>
                            <span class="input-group-btn">
                                <button class="btn btn-default" runat="server" type="button" onserverclick="btnAdvSearch_OnClick">
                                    <i class="fa fa-search"></i>
                                </button>
                            </span>
                        </div>
                        <asp:updatepanel id="updatepanel20" runat="server" updatemode="Conditional" childrenastriggers="true">

                            <ContentTemplate>
                        <asp:GridView ID="gvAdv" runat="server" Visible="true" Style="margin-top: 5px;" AutoGenerateColumns="False" CssClass="table table-bordered table-striped table-hover" OnRowDataBound="gvAdv_RowDataBound"
                            AllowPaging="true" PageSize="3" ForeColor="Black" GridLines="Vertical" Height="100%" Width="100%"
                                BackColor="White" BorderColor="#999999" BorderStyle="Solid"
                                BorderWidth="1px" CellPadding="3" OnPageIndexChanging="gvAdv_PageIndexChanging" OnSorting="gvAdv_Sorting"  >
                              <AlternatingRowStyle BackColor="#CCCCCC" />
                            <Columns>
                              <asp:TemplateField HeaderText="Select">
                <ItemTemplate>
                    <asp:RadioButton ID="RowSelectorADV" runat="server" onclick="RadioCheckAdvert(this);" />

                </ItemTemplate>
                                 <ItemStyle Width="3%" HorizontalAlign="Center" />
            </asp:TemplateField>
                                <asp:TemplateField visible="false">
                                        <ItemTemplate>
                                            <asp:Label runat="server" visible="false"  ID="lb_AdvertID" Text='<%# Bind("AdvID") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Advertisement">
                                       
                                        <ItemTemplate>
                                          
                                        
                                      <asp:Image ID="Image1" runat="server" ImageUrl='<%# Eval("Item") %>'
                                          Visible='<%# Eval("ItemType").ToString() =="image" %>' ClientIDMode="static" style="display:block;object-fit: cover;" ItemStyle-HorizontalAlign="Center"   />
                                      <div id="vidDiv" runat="server">
                                        <video ClientIDMode="static" id="videoDog" width="200" height="200" runat="server" controls visible='<%# Eval("ItemType").ToString()!="image" %>'>  
                                            <source runat="server" src='<%#Eval("Item")%>' type="video/mp4" visible='<%# Eval("ItemType").ToString()!="image" %>' ItemStyle-HorizontalAlign="Center" />  
                                        </video>  
                                           </div>
                                              
                                            </ItemTemplate>
                                        <controlstyle width="200px" height="200px"  />
                                        <ItemStyle Width="200px" height="200px" />
                                        </asp:TemplateField>
                                <asp:BoundField DataField="Name" HeaderText="Name" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100px">
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:BoundField>
                                   <asp:BoundField DataField="ItemType" HeaderText="Type" SortExpression="ItemType" ItemStyle-HorizontalAlign="Center">

                                   </asp:BoundField>
                                <asp:BoundField DataField="StartDate" HeaderText="Start Date" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100px" DataFormatString="{0:D}">
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="EndDate" HeaderText="End Date" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100px" DataFormatString="{0:D}">
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundField>
                                
                            </Columns>
                                <FooterStyle BackColor="#CCCCCC" />
                                <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" Wrap="False" />
                                <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Left" />
                                <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                                <SortedAscendingCellStyle BackColor="#F1F1F1" HorizontalAlign="Center" />
                                <SortedAscendingHeaderStyle BackColor="#808080" HorizontalAlign="Center" />
                                <SortedDescendingCellStyle BackColor="#CAC9C9" HorizontalAlign="Center"/>
                                <SortedDescendingHeaderStyle BackColor="#383838" HorizontalAlign="Center"/>
                        </asp:GridView>
                                </ContentTemplate>
                      
                         </asp:updatepanel>
                    </div>
                    </div>
                   
                    <div class="modal-footer">
                       
                    <asp:Button ID="addAdv" runat="server" CssClass="btn btn-primary" Text="Add" OnClick="addAdv_Click" />
                       
                    </div>
                     
                    </div>
                </div>
            </div>
        </div>
        <div id="BbModal" class="modal fade" role="dialog">
            <div class="modal-dialog modal-lg">

                <!-- Modal content-->
                <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h2 class="modal-title">Billboards</h2>
                </div>
                <div class="modal-body">
                    <div class="row">

                    <div class="col-lg-12">

                        <div class="input-group custom-search-form" style="width: 50%">
                            <asp:TextBox ID="txtBb" class="form-control" runat="server" placeholder="Search..."></asp:TextBox>
                            <span class="input-group-btn">
                                <button class="btn btn-default" runat="server" type="button" onserverclick="btnBbSearch_OnClick">
                                    <i class="fa fa-search"></i>
                                </button>
                            </span>
                        </div>
                                &nbsp
                                &nbsp 
                        <asp:GridView ID="gvBb" runat="server" Visible="true" Style="margin-top: 5px;" CssClass="table table-bordered table-striped table-hover"
                            OnRowDataBound="gvBb_RowDataBound" AllowPaging="true" PageSize="10" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3"
                            AutoGenerateColumns="False" Height="100%" Width="100%" OnPageIndexChanging="gvBb_PageIndexChanging" OnSorting="gvBb_Sorting">
                             <AlternatingRowStyle BackColor="#CCCCCC" />
                            <Columns>
                                <asp:TemplateField HeaderText="Select">
                <ItemTemplate><asp:RadioButton ID="RowSelectorBB" runat="server" onclick="RadioCheckBillboard(this);" />

                </ItemTemplate>
                                    <ItemStyle Width="3%" HorizontalAlign="Center" />
            </asp:TemplateField>
                                  <asp:TemplateField visible="false">
                                        <ItemTemplate>
                                            <asp:Label runat="server" visible="false"  ID="lb_BillboardID" Text='<%# Bind("BillboardID") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                <asp:BoundField DataField="BillboardCode" HeaderText="Billboard Code" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="100px">
                                        <ItemStyle HorizontalAlign="Left" Width="7%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="Address" HeaderText="Address" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="200px">
                                        <ItemStyle HorizontalAlign="Center" Width="200px"></ItemStyle>
                                </asp:BoundField>
                            
                            </Columns>
                               <FooterStyle BackColor="#CCCCCC" />
                                <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" Wrap="False" />
                                <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="left" />
                                <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                <SortedAscendingHeaderStyle BackColor="#808080" />
                                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                <SortedDescendingHeaderStyle BackColor="#383838" />
                        </asp:GridView>
                    </div>
                </div>
                    <div class="modal-footer">
                       <%-- <asp:UpdatePanel ID="updatepanel2" runat="server">

                            <ContentTemplate>--%>
                    <asp:Button ID="addBb" runat="server" CssClass="btn btn-primary" Text="Add" OnClick="addBb_Click"/>
                    <%--            </ContentTemplate>
                            </asp:UpdatePanel>--%>
                    </div>
                    </div>
                </div>
            </div>
        </div>
            <div id="VadDateModal" class="modal fade" role="dialog">
            <div class="modal-dialog">

                <!-- Modal content-->
                <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h2 class="modal-title">Validation</h2>
                </div>
                <div class="modal-body">
                    <div class="row">

                    <div class="col-lg-12">
                        <asp:Label runat ="server" Text="Please input start/end dates" Font-Size="Large"></asp:Label>
                    </div>
                        &nbsp
                </div>
                    <div class="modal-footer">
                    </div>
                    </div>
                </div>
            </div>
        </div>
            <div id="VadModal" class="modal fade" role="dialog">
            <div class="modal-dialog">

                <!-- Modal content-->
                <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h2 class="modal-title">Validation</h2>
                </div>
                <div class="modal-body">
                    <div class="row">

                    <div class="col-lg-12">
                        <asp:Label runat ="server" Text="Please select a chart type" Font-Size="Large"></asp:Label>
                    </div>
                        &nbsp
                </div>
                    <div class="modal-footer">
                    </div>
                    </div>
                </div>
            </div>
        </div>
            <div id="VadModal2" class="modal fade" role="dialog">
            <div class="modal-dialog">

                <!-- Modal content-->
                <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h2 class="modal-title">Validation</h2>
                </div>
                <div class="modal-body">
                    <div class="row">

                    <div class="col-lg-12">
                        <asp:Label runat ="server" Text="Please select either a company/advertisement/billboard" Font-Size="Large"></asp:Label>
                    </div>
                        &nbsp
                </div>
                    <div class="modal-footer">
                    </div>
                    </div>
                </div>
            </div>

        </div> 
    </form>
</asp:Content>
