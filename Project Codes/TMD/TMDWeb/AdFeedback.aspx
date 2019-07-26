<%@ Page Title="" Language="C#" MasterPageFile="~/Template.Master" AutoEventWireup="true" CodeFile="AdFeedback.aspx.cs" Inherits="targeted_marketing_display.AdFeedback" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form runat="server">
        <div id="adminDiv" runat="server">
     
                <!--button-->
                <div class="row">
                    <div class="col-lg-12">
                        <h1 class="page-header">Advertisement Feedback</h1>
                    </div>
                    <!-- /.col-lg-12 -->
                </div>
                <!-- /.row -->

                <br />

            <div class="row">
                <div class="col-lg-6">
                    <p style="font-weight:bold">From:</p>
                </div>
                <div class="col-lg-6">
                    <p style="font-weight:bold">To:</p>
                </div>
            </div>
            <div class="row">
            <div class="col-lg-6">
                <div class="form-group">
                     <asp:TextBox ID="txtFrom" runat="server" CssClass="form-control pickdate"></asp:TextBox>
                     <asp:LinkButton ID="lBtnFrom" runat="server" CssClass="btn btn-default calendar pickdate"><span aria-hidden="false" class="glyphicon glyphicon-calendar"></span></asp:LinkButton>
                </div>   
            </div>
            <div class="col-lg-6">
                <div class="form-group">
                    <asp:TextBox ID="txtTo" runat="server" CssClass="form-control pickdate"></asp:TextBox>
                    <asp:LinkButton ID="lBtnTo" runat="server" CssClass="btn btn-default calendar pickdate"><span aria-hidden="false" class="glyphicon glyphicon-calendar"></span></asp:LinkButton>
                </div>
            </div>
            </div>
            <div class="row">
                <div class="col-lg-12">
                     <p style="font-weight:bold">Select Company:</p>
                </div>
            </div>
            <div class="row">
                <div class ="col-lg-6">
                    <div class="form-group">
                        <asp:DropDownList ID="ddlCom" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlCom_SelectedIndexChanged" AutoPostBack="True" DataTextField="Name" DataValueField="Name"></asp:DropDownList>
                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Targeted_Marketing_DisplayConnectionString %>" SelectCommand="SELECT [Name] FROM [Company]"></asp:SqlDataSource>
                    </div>
                </div>
                <div class="col-lg-6">
                    <div class="form-group">
                        <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#AdvModal" style="width:30.7%"> <b>Select Advertisements: </b></button>
                        <asp:Label runat="server" Text="OR" Font-Bold="true"></asp:Label>
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
                        <asp:RadioButton ID="rbTs" runat="server" OnCheckedChanged="rbTs_CheckedChanged" AutoPostBack="true"/>
                        <asp:Label ID="lblTs" runat="server" Text="Time Stamp"></asp:Label>
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
                        <asp:Panel runat="server" CssClass="panel">
                    <asp:Chart ID="chartFb" class="chartFb" runat="server" Visible="true">
                        <Series>
                            <asp:Series Name="Series1"></asp:Series>
                        </Series>
                                <Chartareas>
                                    <asp:ChartArea>
                                    </asp:ChartArea>
                                </Chartareas>
                            </asp:Chart>
                        </asp:Panel>
                    </div>
                </div>
                <div class="col-lg-3">

                </div>
            </div>
            <div class="row">
                <asp:GridView ID="gvCom" runat="server" Visible="true" Style="margin-top: 5px;" AutoGenerateColumns="False" CssClass="table table-bordered table-striped table-hover">
                            <Columns>
                                <asp:BoundField DataField="ID" HeaderText="Advertisement ID" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="100px">
                                        <ItemStyle HorizontalAlign="Center" Width="2%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="BillboardID" HeaderText="Billboard ID" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="100px">
                                        <ItemStyle HorizontalAlign="Center" Width="2%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="Name" HeaderText="Name" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="100px">
                                        <ItemStyle HorizontalAlign="Center" Width="2%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="Age" HeaderText="Age" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="100px">
                                        <ItemStyle HorizontalAlign="Center" Width="2%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="Gender" HeaderText="Gender" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="100px">
                                        <ItemStyle HorizontalAlign="Center" Width="2%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="Emotion" HeaderText="Emotion" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="100px">
                                        <ItemStyle HorizontalAlign="Center" Width="2%"></ItemStyle>
                                </asp:BoundField>
                            </Columns>
                        </asp:GridView>
            </div>
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
                            <asp:TextBox ID="txtAdv" class="form-control" runat="server" placeholder="Search..."></asp:TextBox>
                            <span class="input-group-btn">
                                <button class="btn btn-default" runat="server" type="button" onserverclick="btnAdvSearch_OnClick">
                                    <i class="fa fa-search"></i>
                                </button>
                            </span>
                        </div>
                                &nbsp
                                &nbsp
                        <asp:GridView ID="gvAdv" runat="server" Visible="true" Style="margin-top: 5px;" AutoGenerateColumns="False" CssClass="table table-bordered table-striped table-hover" OnRowDataBound="gvAdv_RowDataBound">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CheckBox1" runat="server" />
                                    </ItemTemplate>
                                    <ItemStyle Width="3%" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="AdvId" HeaderText="Advertisement ID" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="100px">
                                        <ItemStyle HorizontalAlign="Center" Width="2%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="Name" HeaderText="Advertisement" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="100px">
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="StartDate" HeaderText="Start Date" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="100px">
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="EndDate" HeaderText="End Date" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="100px">
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="Status" HeaderText="Status" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="100px">
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
                    <div class="modal-footer">
                    <asp:Button ID="addAdv" runat="server" CssClass="btn btn-primary" Text="Add" OnClick="addAdv_Click"/>
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
                        <asp:GridView ID="gvBb" runat="server" Visible="true" Style="margin-top: 5px;" AutoGenerateColumns="False" CssClass="table table-bordered table-striped table-hover" OnRowDataBound="gvBb_RowDataBound">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CheckBox1" runat="server" />
                                    </ItemTemplate>
                                    <ItemStyle Width="3%" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="BillboardID" HeaderText="Billboard ID" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="100px">
                                        <ItemStyle HorizontalAlign="Left" Width="7%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="Address" HeaderText="Address" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="200px">
                                        <ItemStyle HorizontalAlign="Center" Width="200px"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="Status" HeaderText="Status" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="100px">
                                        <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                                </asp:BoundField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
                    <div class="modal-footer">
                    <asp:Button ID="addBb" runat="server" CssClass="btn btn-primary" Text="Add" OnClick="addBb_Click"/>
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
