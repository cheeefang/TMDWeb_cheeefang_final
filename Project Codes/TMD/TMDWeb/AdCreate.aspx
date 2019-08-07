<%@ Page Title="" Language="C#" MasterPageFile="~/Template.Master"  EnableEventValidation="true" AutoEventWireup="true" CodeFile="AdCreate.aspx.cs" Inherits="targeted_marketing_display.AdCreate"  %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.4.0/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.0/js/bootstrap.min.js"></script>
    <script src="jquery-1.4.3.js" type="text/javascript"></script>

    <script>
        function showModal() {
            $('#myModal2').modal('show');
        }
        function hideModal() {
            $('#myModal2').modal('close');
        }
    </script>

    <script type="text/javascript" language="javascript">

        function CheckCheck() {

            var chkBoxList = document.getElementById('<%=CheckBoxList1.ClientID %>');
            var chkBoxCount = chkBoxList.getElementsByTagName("input");
            var btn = document.getElementById('<%=CategoryButton.ClientID %>');
            var i = 0;
            var tot = 0;

            for (i = 0; i < chkBoxCount.length; i++) {
                if (chkBoxCount[i].checked) {
                    tot = tot + 1;
                }
            }

            if (tot > 8) {
                alert('Cannot check more than 8 categpries');
                btn.disabled = true;
            }

            else {
                btn.disabled = false;
            }

        }
        function HeaderCheckBoxClick(checkbox) {
            var gridview = document.getElementById("GridView1");
            for (var i = 1; i<gridview.rows.length; i++) {
                gridview.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = checkbox.checked;
            }
        }

        function ChildCheckBoxClick(checkbox) {
            var atleastOneCheckBoxUnchecked = false;
            var gridview = document.getElementById("GridView1");

            for (var i = 1; i<gridview.rows.length; i++) {
                if (gridview.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked == false) {
                    atleastOneCheckBoxUnchecked = true;
                    break;
                }
            }

            gridview.rows[0].cells[0].getElementsByTagName("INPUT")[0].checked = !atleastOneCheckBoxUnchecked;
        }


      
    </script>

 <script type="text/javascript">
        function checkAll(objRef) {
            var GridView = objRef.parentNode.parentNode.parentNode;
            var inputList = GridView.getElementsByTagName("input");
            for (var i = 0; i < inputList.length; i++) {
                 var row = inputList[i].parentNode.parentNode;
                if (inputList[i].type == "checkbox" && objRef != inputList[i]) {
                     if (objRef.checked) {
                       inputList[i].checked = true;
                     }
                     else {
                         inputList[i].checked = false;
                     }
                 }
             }
         }

     function Check_Click(objRef) {
         var row = objRef.parentNode.parentNode;
         var GridView = row.parentNode;
         var inputList = GridView.getElementsByTagName("input");
         for (var i = 0; i < inputList.length; i++) {
             var headerCheckBox = inputList[0];
             var checked = true;
             if (inputList[i].type == "checkbox" && inputList[i] != headerCheckBox) {
                 if (!inputList[i].checked) {
                     checked = false;
                     break;
                 }
             }
         }
         headerCheckBox.checked = checked;
     }


     function ImagePreview(input) {
         const file = input.files[0];
         const fileType = file['type'];
         const validImageTypes = ['image/gif', 'image/jpeg', 'image/png','image/jpg','image/PNG','image/JPEG','image/JPG'];
         if (!validImageTypes.includes(fileType)) {
              
             // invalid file type code goes here.
    
             
             if (input.files && input.files[0]) {

             var reader = new FileReader();
             reader.onload = function (e) {
                 $('#<%=videoDog.ClientID%>').prop('src', e.target.result)
                     .width(200)
                     .height(200);
             };
             reader.readAsDataURL(input.files[0]);
                             
             }
             document.getElementById("videoDog").style.display = "block";
             document.getElementById("imgLogo").style.display = "none";
         }
         else {

             if (input.files && input.files[0]) {
                  
             var reader = new FileReader();
             reader.onload = function (e) {
                 $('#<%=imgLogo.ClientID%>').prop('src', e.target.result)
                     .width(200)
                     .height(200);
             };
             reader.readAsDataURL(input.files[0]);
                
         }

             document.getElementById("videoDog").style.display = "none";
             document.getElementById("imgLogo").style.display = "block";
          
             
         }
         
     }
 </script>


    <style>
        .modal-dialog {
            padding: 70px 0;
            text-align: center;
        }

         .hiddencol
  {
    display: none;
  }
      #videoDog{
          display:none;
             object-fit: cover;
      }
      #vidDiv{
       
      }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form runat="server">  
            <div class="container" style="height: 100%">            
                <div runat="server" class="alert alert-danger" id="alertWarning" visible="False" style="margin-top: 10px;">
                    
                    <strong>Warning!</strong>
                    <asp:Label ID="warningLocation" runat="server"></asp:Label>
                </div>
            </div>
        <br />
        <div runat="server" class="alert alert-success" id="alertSuccess" visible="False">

                    <strong>Success!</strong> Advertisement has been Created.
                </div>
            <div class="row">
                <div class="col-lg-12">
                    <h1 class="page-header">New Advertisement</h1>
                </div>
                <!-- /.col-lg-12 -->
            </div>
            <!-- /.row -->
        

         <div class="row">
            <div class="col-lg-6">
                <div class="form-group">
                    <label>Advertisement Image/Video</label>
                      <label style="color: red">*</label>
               <br />
                   
                 
                    <asp:image id="imgLogo" runat="server" ImageUrl="Images/NoImageAvailable.png" Width="200" Height="200" visible="true" ClientIDMode="static"></asp:image>
                     
                 <div id="vidDiv" runat="server">
                       <video ClientIDMode="static" id="videoDog" width="200" height="200" runat="server" controls visible="true"  >  
                                            <source  id="vidSource" runat="server" src="" type="video/mp4">  
                                        </video>  
                       </div>
                </div>
            </div>
        </div>



             <div class="row">
                
                <div class="col-lg-6">
                   <div class="form-group">
                 
                        
                      
                        &nbsp;
                       
                                       
                               
                              
                    

                        <asp:FileUpload ID="FileUpload1" runat="server" onchange="ImagePreview(this);" />
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationExpression='(.*?)\.(jpg|jpeg|png|gif|avi|flv|wmv|mp4|mov|JPG|JPEG|PNG|GIF|AVI|FLV|WMV|MOV|MP4)$'
                            ControlToValidate="FileUpload1" runat="server" ForeColor="Red" ErrorMessage="Please select valid image/video file."
                            Display="Dynamic" />
                        <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                        <asp:Literal ID="Literal2" runat="server"></asp:Literal>
                        <br />
                               
                                   
                    </div>
                </div>

                  <div class="col-lg-6" id="divCompany" runat="server" visible="false">
                   <div class="form-group">
                 <label>Company</label>
                       <label style="color: red">*</label>
                       </br>
                 <asp:DropDownList class="form-control" ID="DropDownListCompany" runat="server" visible="false" >
                     

                    </asp:DropDownList>
                     
   <br />
                      
                    </div>
                </div>
            
       
            </div>
           
            <div class="row">
                
               
                <div class="col-lg-6">

                    <div class="form-group">
                        <label>Duration</label> (only video file required)&nbsp;
                        <asp:TextBox Class="form-control" ID="videoDurationTB" runat="server" />                        
                        <br />
                    </div>
                </div>



          <div class="col-lg-6">
                    <div class="form-group">
                        <label>Name </label>
                        <label style="color: red">*</label>
                        <asp:TextBox Class="form-control" ID="adNameTB" runat="server" AutoCompleteType="Disabled" autocomplete="off" MaxLength="255"></asp:TextBox>
                                     <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator3" controltovalidate="adNameTB" errormessage="Please Enter Advertisement Name" ForeColor="Red"/>
    <br /><br />
                    </div>
                </div>
            </div>



            <div class="row">
                <div class="col-lg-6">
                    <div class="form-group">
                        <label>From </label>
                        <label style="color: red">*</label>
                        <asp:TextBox Class="form-control" ID="startDateTB" runat="server" TextMode="Date" AutoCompleteType="Disabled" autocomplete="off"></asp:TextBox>
                        <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="startDateTB" ErrorMessage="invalid start date (must be a day later)" Operator="GreaterThan" Type="Date" ForeColor="Red"></asp:CompareValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="startDateTB" Display="None" ErrorMessage="required start date"></asp:RequiredFieldValidator>
                    </div>
                </div>

                <div class="col-lg-6">

                    <div class="form-group">
                        <label>To: </label>
                        <label style="color: red">*</label>
                        <asp:TextBox class="form-control" ID="endDateTB" runat="server" TextMode="Date" AutoCompleteType="Disabled" autocomplete="off"></asp:TextBox>&nbsp;
                         <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToCompare="startDateTB" ControlToValidate="endDateTB" ErrorMessage="invalid end date" Operator="GreaterThan" Type="Date" ForeColor="Red"></asp:CompareValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="endDateTB" Display="None" ErrorMessage="required end date"></asp:RequiredFieldValidator>
                    </div>
                </div>
             
            </div>

            <div class="row">
                <div class="col-lg-6">
                    <div class="form-group">
                        <label>Category </label>
                        <label style="color: red">*</label>
                        &nbsp;
                        <asp:ScriptManager ID="ScriptManager2" runat="server"></asp:ScriptManager>

                        <asp:UpdatePanel ID="updatepanel1" runat="server">

                            <ContentTemplate>
                                <p class="input-group">
                                    <asp:TextBox ID="adCategoryTB" class="form-control" runat="server" AutoCompleteType="Disabled" autocomplete="off"></asp:TextBox>
                                    <span class="input-group-btn">
                                        <asp:LinkButton runat="server" class="btn btn-default" ID="CategoryButton" Style="height: 34px;" Text="<i class='fa fa-caret-square-o-down'></i>" OnClick="CategoryButton_Click" />
                                    </span>
                                </p>
                                <ajaxToolkit:PopupControlExtender ID="PopupControlExtender1" runat="server"
                                    Enabled="True" ExtenderControlID="" TargetControlID="adCategoryTB"
                                    PopupControlID="Panel1" OffsetY="22">
                                </ajaxToolkit:PopupControlExtender>
                                <asp:Panel ID="Panel1" Class="form-control" runat="server" Height="116px" Width="450px"
                                    BorderStyle="Solid" BorderColor="#435058" BorderWidth="1px" Direction="LeftToRight"
                                    ScrollBars="Auto" BackColor="whitesmoke" Style="display: none; margin-top: 10px;">

                                    <asp:CheckBoxList ID="CheckBoxList1" runat="server" DataTextField="CodeDesc" DataValueField="CodeValue" onclick="javascript:CheckCheck();"></asp:CheckBoxList>
                                </asp:Panel>
                            </ContentTemplate>

                        </asp:UpdatePanel>



                    </div>
                </div>
            

                          
                <div class="col-lg-6">

                    <div class="form-group">
                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Targeted_Marketing_DisplayConnectionString %>" SelectCommand="SELECT [CompanyID], [Name] FROM [Company] where status=1"></asp:SqlDataSource>
                        
                        <label>Display Billboard </label>
                        <label style="color: red">*</label>
                        <asp:UpdatePanel ID="updatepanel3" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">

                            <ContentTemplate>
                            <asp:TextBox ID="billboardDisplayTB" class="form-control" runat="server"   placeholder="Search..." data-toggle="modal" data-target="#myModal2" AutoCompleteType="Disabled" autocomplete="off"></asp:TextBox>
                          
    <br /><br />
                      </ContentTemplate>
                                             <Triggers>
                <asp:AsyncPostBackTrigger ControlID="BillboardSearch" EventName="Click" />
            </Triggers>
                     </asp:UpdatePanel>
                           
                    </div>
                    
                     <div id="myModal2" class="modal fade" role="dialog">
                            <div class="modal-dialog modal-lg" >

                                <!-- Modal content-->
                                <div class="modal-content">
                                    <div class="modal-header">
                                       
                                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                                        <h4 class="modal-title" style="font-size: xx-large;">Billboard Locations</h4>
                                    </div>
                                    <div class="modal-body">
                                    
                                        <p class="input-group" style="float:left; width:300px">
                                            <asp:TextBox ID="txtSearch" class="form-control" runat="server" placeholder="Search..." AutoCompleteType="Disabled" autocomplete="off"></asp:TextBox>
                                      
                                            <span class="input-group-btn">
                                                <asp:LinkButton runat="server" class="btn btn-default" ID="btnRun" Style="height: 34px;" Text="<i class='fa fa-search'></i>" OnClick="btnRun_Click"  />
                                            </span>


                                        </p>

                                    <!-- Billboard Gridview-->
 <asp:UpdatePanel ID="updatepanel20" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">

                            <ContentTemplate>
                                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                                        CssClass="table table-striped table-bordered table-hover" DataKeyNames="BillboardID" 
                                        DataSourceID="SqlDataSource2" AllowPaging="True" Width="100%" BackColor="White" 
                                        BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3">
                                         <AlternatingRowStyle BackColor="#CCCCCC" />
                                        <Columns>                                         
                                           <asp:TemplateField>
                                               <HeaderTemplate>
                                                   <asp:CheckBox ID="checkboxSelectAll"  onclick="checkAll(this);" runat="server" />
                                               </HeaderTemplate>
                                                <ItemTemplate>
                                                   <asp:Checkbox ID="CheckBoxSelector" onclick="Check_Click(this);"  runat ="server" />
                                               </ItemTemplate>                                            
                                           </asp:TemplateField>
                                              <asp:TemplateField visible="false">
                                        <ItemTemplate>
                                            <asp:Label runat="server" visible="false"  ID="lb_BillboardID" Text='<%# Bind("BillboardID") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                 
                                          
                                            <asp:BoundField DataField="BillboardCode" HeaderText="BillboardCode" SortExpression="BillboardCode" />
                                            <asp:BoundField DataField="Latitude" HeaderText="Latitude" SortExpression="Latitude" />
                                            <asp:BoundField DataField="Longtitude" HeaderText="Longtitude" SortExpression="Longtitude" />
                                            <asp:BoundField DataField="Address" HeaderText="Address" SortExpression="Address" ReadOnly="True" />

                                        </Columns>
                                                    <FooterStyle BackColor="#CCCCCC" />

                               <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />

                               <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="left" />
	
                               <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />

                               <SortedAscendingCellStyle BackColor="#F1F1F1" />

                               <SortedAscendingHeaderStyle BackColor="#808080" />

                               <SortedDescendingCellStyle BackColor="#CAC9C9" />

                               <SortedDescendingHeaderStyle BackColor="#383838" />
	
                           </asp:GridView>
                                   </ContentTemplate>
     </asp:UpdatePanel>
                                    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:Targeted_Marketing_DisplayConnectionString %>" 
                               FilterExpression="BillboardCode LIKE '%{0}%' OR AddressLn LIKE '%{0}%' OR City LIKE '%{0}%' OR Country LIKE '%{0}%' OR postalCode LIKE '%{0}%'" 
                                        SelectCommand="SELECT BillboardID,BillboardCode, Latitude ,Longtitude ,(( AddressLn1) + ' '+( AddressLn2 )+  ' '+(City)+  ', '+(Country)+ ' '+(postalCode)) AS Address FROM BillboardLocation where status=1 ">
                                        <FilterParameters>
                                            <asp:ControlParameter ControlID="txtSearch" Name="City" PropertyName="Text" />
                                            <asp:ControlParameter ControlID="txtSearch" Name="AddressLn" PropertyName="Text" />
                                            <asp:ControlParameter ControlID="txtSearch" Name="BillboardCode" PropertyName="Text" />
                                            <asp:ControlParameter ControlID="txtSearch" Name="Country" PropertyName="Text" />
                                            <asp:ControlParameter ControlID="txtSearch" Name="postalCode" PropertyName="Text" />

                                        </FilterParameters>
                                    </asp:SqlDataSource>
                                     

                                   
                                    </div>
                                    <div class="modal-footer">
                                     
                                <asp:LinkButton runat="server" class="btn btn-default" ID="BillboardSearch" Style="height: 34px;" Text="Confirm" OnClick="BillboardSearch_Click" autopostback="true"  />

                                        <asp:Button ID="Button3" class="btn btn-default" runat="server" Text="Close" data-dismiss="modal" />
                  
                                    </div>
                                </div>

                            </div>
                        </div>
                </div>
                </div>
                                                   
          
                <div class="row">
                    <div class="col-lg-6">
                        <div class="form-group" style="margin-top:20px;">
                            <label>Targeted Audience </label>
                            <label style="color: red">*</label>
                            <asp:CheckBoxList ID="CheckBoxList2" runat="server" Width="822px" Font-Size="small" Font-Bold="false" Height="92px" RepeatDirection="Horizontal" Style="margin-right: 10px; margin-left:18px; margin-top: 3px; margin-bottom: 10px;" RepeatColumns="4" >
                                <asp:ListItem Value="1">Male Child(Age 0-15)</asp:ListItem>
                                <asp:ListItem Value="2">Male Young Adult(Age 16-30)</asp:ListItem>
                                <asp:ListItem Value="3">Male Adult(Age 31-65)</asp:ListItem>
                                <asp:ListItem Value="4">Male Senior(Age 66+)</asp:ListItem>
                                <asp:ListItem Value="5">Female Child(Age 0-15)</asp:ListItem>
                                <asp:ListItem Value="6">Female Young Adult(Age 16-30)</asp:ListItem>
                                <asp:ListItem Value="7">Female Adult(Age 31-65)</asp:ListItem>
                                <asp:ListItem Value="8">Female Senior(Age 66+)</asp:ListItem>
                            </asp:CheckBoxList>


                            


                        </div>

                    </div>
                    </div>

                
          
                <div class="row">
                    <div class="col-lg-6">
                        <div class="form-group">
                            <p>
                               
                                <asp:CheckBox ID="CheckBox1" runat="server" />
                               Agree with the <a data-toggle="modal" data-target="#myModal">terms and conditions</a>
                            </p>

                            <!-- Modal -->
                            <div class="modal fade" id="myModal" role="dialog">
                                <div class="modal-dialog">

                                    <!-- Modal content-->
                                    <div class="modal-content">
                                        <div class="modal-header">
                                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                                            <h4 class="modal-title">Terms and Conditions</h4>
                                        </div>
                                        <div class="modal-body">
                                            <p>
                                                Welcome to our Targeted Marketing Display. These are our terms and conditions for use of our targeted marketing display system.
                                            </p>
                                            <p>
                                                1. As a condition of use, you promise not to use the services for any purpose that is unlawful.
                                            </p>
                                            <p>
                                                2. The client acknowledges that the content of the advertisement is subject to copyright and accordingly the Client shall ensure that the advertisement is used solely for the client's reference purposes and may not be copied, reproduced,rebroadcast or commercially exploited in all or any part.
                                            </p>

                                            <p>
                                                3. You cannot terminate your contract before end date.
                                            </p>
                                        </div>
                                        <div class="modal-footer">
                                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <asp:Button ID="Button1" class="btn btn-primary nextBtn pull-right" runat="server" Text="Confirm" OnClick="ButtonConfirm_Click" style="margin-right:25px;"/>



                   


                   



                </div>

        









    </form>


</asp:Content>
