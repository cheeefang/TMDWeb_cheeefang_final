using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using targeted_marketing_display;

using System.Configuration;
namespace targeted_marketing_display
{
 

    public partial class AdvertListing : System.Web.UI.Page
    {
        SqlConnection vid = new
             SqlConnection(@"Data Source=L33527\CHEEEFANGSQL;Initial Catalog=Targeted_Marketing_Display;Persist Security Info=True;User ID=root;Password=passw8rd");
        private string sortExpression;
        private SortDirection sortDirection;
        SqlConnection con = new SqlConnection(@"Data Source=L33527\CHEEEFANGSQL;Initial Catalog=Targeted_Marketing_Display;Persist Security Info=True;User ID=root;Password=passw8rd");



        protected int adminint;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Convert.ToInt32(Session["AdvertUpdate"]) == 2)
                {
                    alertSuccessUpdate.Visible = true;
                    Session.Remove("AdvertUpdate");
                }

                if (Convert.ToInt32(Session["AdvertCreate"]) == 2)
                {
                    alertSuccessCreate.Visible = true;
                    Session.Remove("AdvertCreate");
                }

                this.BindGrid();


            }
          
        
            if (Session["userType"].ToString() == Reference.USR_ADM)
            {
                adminint = 1;
            }
            else
            {
                adminint = 0;
            }
        }
        public void BindGrid()
        {
                SqlConnection conn = null;
                SqlDataReader reader = null;

                // instantiate and open connection
                conn = new
                    SqlConnection(@"Data Source=L33527\CHEEEFANGSQL;Initial Catalog=Targeted_Marketing_Display;Persist Security Info=True;User ID=root;Password=passw8rd");
                conn.Open();
            if (Session["userType"].ToString()==Reference.USR_ADM)
            {
                // 1. declare command object with parameter
                SqlCommand cmd = new SqlCommand(
                  " SELECT [Advertisement].AdvID,[Company].Name as CompanyName, [Advertisement].Name as AdvertName, [Advertisement].Item, [Advertisement].ItemType,[StartDate], [EndDate]FROM " +
                  "[Advertisement] inner join [Company] on Company.CompanyID =[Advertisement].CompanyID where [Advertisement].status = 1 and[Company].status = 1", conn);
                SqlDataAdapter sda = new SqlDataAdapter();
                DataTable dt = new DataTable();


                cmd.Connection = conn;
                sda.SelectCommand = cmd;
                sda.Fill(dt);


                // get data stream



                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
            else
            {
                User uObj = new User();
                UserManagement uDao = new UserManagement();
                uObj = uDao.getUserByID(Session["userID"].ToString());
                // 1. declare command object with parameter
                SqlCommand cmd = new SqlCommand(
                  " SELECT [Advertisement].AdvID,[Company].Name as CompanyName, [Advertisement].Name as AdvertName, [Advertisement].Item, [Advertisement].ItemType,[StartDate], [EndDate] FROM " +
                  "[Advertisement] inner join [Company] on Company.CompanyID =[Advertisement].CompanyID where [Advertisement].status = 1 and [Company].status = 1 and [Company].CompanyID=@comID", conn);

                // 2. define parameters used in command object
                SqlParameter param = new SqlParameter();
                param.ParameterName = "@comID";
                param.Value = uObj.CompanyID.ToString();
                // 3. add new parameter to command object
                cmd.Parameters.Add(param);


                SqlDataAdapter sda = new SqlDataAdapter();
                DataTable dt = new DataTable();


                cmd.Connection = conn;
                sda.SelectCommand = cmd;
                sda.Fill(dt);


                // get data stream



                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
               

                if (GridView1.Rows.Count == 0)
                {
                    
                }                          
        }

        protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (GridView1.Attributes["CurrentSortField"] != null && GridView1.Attributes["CurrentSortDirection"] != null)
            {
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    foreach (TableCell tableCell in e.Row.Cells)
                    {
                        if (tableCell.HasControls())
                        {
                            LinkButton sortLinkButton = null;
                            if (tableCell.Controls[0] is LinkButton)
                            {
                                sortLinkButton = (LinkButton)tableCell.Controls[0];
                            }

                            if (sortLinkButton != null && GridView1.Attributes["CurrentSortField"] == sortLinkButton.CommandArgument)
                            {
                                Image image = new Image();
                                if (GridView1.Attributes["CurrentSortDirection"] == "ASC")
                                {
                                    image.ImageUrl = "~/webicons/Ascendingicon.png";

                                }
                                else
                                {
                                    image.ImageUrl = "~/webicons/Descendingicon.png";

                                }
                                tableCell.Controls.Add(new LiteralControl("&nbsp;"));
                                tableCell.Controls.Add(image);
                            }
                        }
                    }
                }
            }
        }
        protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
        {


            SortDirection sortDirection = SortDirection.Ascending;
            string sortField = string.Empty;

            SortGridview((GridView)sender, e, out sortDirection, out sortField);
            string strSortDirection = sortDirection == SortDirection.Ascending ? "ASC" : "DESC";





            SqlConnection conn = null;
            SqlDataReader reader = null;



            // instantiate and open connection
            conn = new
                SqlConnection(@"Data Source=L33527\CHEEEFANGSQL;Initial Catalog=Targeted_Marketing_Display;Persist Security Info=True;User ID=root;Password=passw8rd");
            conn.Open();


            //  " SELECT [Advertisement].AdvID,[Company].Name, [Advertisement].Name, [Advertisement].Item, [Advertisement].ItemType,[StartDate], [EndDate]FROM " +
            //"[Advertisement] inner join[Company] on Company.CompanyID =[Advertisement].CompanyID where[Advertisement].status = 1 and[Company].status = 1"
            if (Session["UserType"].ToString()==Reference.USR_ADM)
            {
                SqlCommand cmd = new SqlCommand(
               " select AdvID, [Company].Name as CompanyName ,[Advertisement].Name as AdvertName,[Advertisement].Item,[Advertisement].ItemType,[Advertisement].StartDate,[Advertisement].EndDate" +
               " from [Advertisement] inner join [Company] on [Advertisement].CompanyID =[Company].CompanyID " +
                       "where [Advertisement].status=1 order by " + e.SortExpression + "  " + strSortDirection, conn);
              
                SqlDataAdapter sda = new SqlDataAdapter();
                DataTable dt = new DataTable();
                cmd.Connection = conn;
                sda.SelectCommand = cmd;
                sda.Fill(dt);



                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
            else
            {
                User uObj = new User();
                UserManagement uDao = new UserManagement();
                uObj = uDao.getUserByID(Session["userID"].ToString());
                SqlCommand cmd = new SqlCommand(
            " select AdvID, [Company].Name as CompanyName ,[Advertisement].Name as AdvertName,[Advertisement].Item,[Advertisement].ItemType,[Advertisement].StartDate,[Advertisement].EndDate" +
            " from [Advertisement] inner join [Company] on [Advertisement].CompanyID =[Company].CompanyID " +
                    "where [Company].CompanyID=@ID and [Advertisement].status=1 order by " + e.SortExpression + "  " + strSortDirection, conn);

                // 2. define parameters used in command object
                SqlParameter param = new SqlParameter();
                param.ParameterName = "@ID";
                param.Value = uObj.CompanyID.ToString();

                // 3. add new parameter to command object
                cmd.Parameters.Add(param);
                SqlDataAdapter sda = new SqlDataAdapter();
                DataTable dt = new DataTable();
                cmd.Connection = conn;
                sda.SelectCommand = cmd;
                sda.Fill(dt);



                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
             
        }
        //nth to change
        private void SortGridview(GridView gridView, GridViewSortEventArgs e, out SortDirection sortDirection, out string sortField)
        {
            sortField = e.SortExpression;
            sortDirection = e.SortDirection;

            if (gridView.Attributes["CurrentSortField"] != null && gridView.Attributes["CurrentSortDirection"] != null)
            {
                if (sortField == gridView.Attributes["CurrentSortField"])
                {
                    if (gridView.Attributes["CurrentSortDirection"] == "ASC")
                    {
                        sortDirection = SortDirection.Descending;
                    }
                    else
                    {
                        sortDirection = SortDirection.Ascending;
                    }
                }

                gridView.Attributes["CurrentSortField"] = sortField;
                gridView.Attributes["CurrentSortDirection"] = (sortDirection == SortDirection.Ascending ? "ASC" : "DESC");
            }
        }

        protected void btnRun_Click(object sender, EventArgs e)
        {
         //   " select [Company].Name as CompanyName ,[Advertisement].Name as AdvertName,[Advertisement].Item,[Advertisement].ItemType,[Advertisement].StartDate,[Advertisement].EndDate" +
         //" from [Advertisement] inner join [Company] on [Advertisement].CompanyID =[Company].CompanyID " +
         //        "where [Advertisement].status=1 order by " + e.SortExpression + "  " + strSortDirection, conn);
            if (Session["userType"].ToString() == Reference.USR_ADM)
            {
                //admin input
                if (startDateTB.Text=="" && endDateTB.Text=="")
                {
                    string str = " select AdvID, [Company].Name as CompanyName,[Advertisement].Name as AdvertName" +
      ",[Advertisement].Item,[Advertisement].ItemType,[Advertisement].StartDate,[Advertisement].EndDate from [Advertisement] inner join [Company] on [Advertisement].CompanyID =[Company].CompanyID " +
              "where  [Advertisement].status=1 and ([Advertisement].Name like '%' + @search + '%' OR [Company].Name like '%' + @search + '%' OR ItemType like '%'" +
              " + @search + '%' OR StartDate like '%' + @search + '%' OR  EndDate like '%' + @search + '%') ";
                    SqlCommand xp = new SqlCommand(str, vid);
                    xp.Parameters.Add("@search", SqlDbType.NVarChar).Value = txtSearch.Text;
                    //xp.Parameters.Add("@search2", SqlDbType.NVarChar).Value = txtSearch.Text;
                    vid.Open();
                    xp.ExecuteNonQuery();
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = xp;
                    DataSet ds = new DataSet();
                    da.Fill(ds, "Name");
                    GridView1.DataSource = ds;
                    GridView1.DataBind();
                }
                else
                {
                    DateTime sdate = DateTime.Parse(startDateTB.Text);
                    DateTime edate = DateTime.Parse(endDateTB.Text);
                    string str = " select AdvID, [Company].Name as CompanyName,[Advertisement].Name as AdvertName" +
      ",[Advertisement].Item,[Advertisement].ItemType,[Advertisement].StartDate,[Advertisement].EndDate from [Advertisement] inner join [Company] on [Advertisement].CompanyID =[Company].CompanyID " +
              "where  [Advertisement].status=1 and [Advertisement].StartDate>=@sDate and [Advertisement].EndDate<=@eDate and" +
              " ([Advertisement].Name like '%' + @search + '%' OR [Company].Name like '%' + @search + '%' OR ItemType like '%'" +
              " + @search + '%' OR StartDate like '%' + @search + '%' OR  EndDate like '%' + @search + '%') ";
                    SqlCommand xp = new SqlCommand(str, vid);
                    xp.Parameters.Add("@search", SqlDbType.NVarChar).Value = txtSearch.Text;
                    xp.Parameters.Add("@sDate", SqlDbType.DateTime).Value = sdate;
                    xp.Parameters.Add("@eDate", SqlDbType.DateTime).Value = edate;
                    //xp.Parameters.Add("@search2", SqlDbType.NVarChar).Value = txtSearch.Text;
                    vid.Open();
                    xp.ExecuteNonQuery();
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = xp;
                    DataSet ds = new DataSet();
                    da.Fill(ds, "Name");
                    GridView1.DataSource = ds;
                    GridView1.DataBind();
                }
       
            }
            else
            {
                //user input
                if (startDateTB.Text == "" && endDateTB.Text == "")
                {
                    User uObj = new User();
                    UserManagement uDao = new UserManagement();
                    uObj = uDao.getUserByID(Session["userID"].ToString());
                    string str = " select AdvID, [Company].Name as CompanyName,[Advertisement].Name as AdvertName" +
                 ",[Advertisement].Item,[Advertisement].ItemType,[Advertisement].StartDate,[Advertisement].EndDate from [Advertisement] inner join [Company] on [Advertisement].CompanyID =[Company].CompanyID " +
                         "where [Company].CompanyID=@ID and [Advertisement].status=1 and ([Advertisement].Name like '%' + @search + '%' OR [Company].Name like '%' + @search + '%' OR ItemType like '%'" +
                         " + @search + '%' OR StartDate like '%' + @search + '%' OR  EndDate like '%' + @search + '%') ";
                    SqlCommand xp = new SqlCommand(str, vid);
                    xp.Parameters.Add("@ID", SqlDbType.NVarChar).Value = uObj.CompanyID.ToString();
                    xp.Parameters.Add("@search", SqlDbType.NVarChar).Value = txtSearch.Text;
                    //xp.Parameters.Add("@search2", SqlDbType.NVarChar).Value = txtSearch.Text;
                    vid.Open();
                    xp.ExecuteNonQuery();
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = xp;
                    DataSet ds = new DataSet();
                    da.Fill(ds, "Name");
                    GridView1.DataSource = ds;
                    GridView1.DataBind();
                }
                else
                {
                    DateTime sdate = DateTime.Parse(startDateTB.Text);
                    DateTime edate = DateTime.Parse(endDateTB.Text);
                    string str = " select AdvID, [Company].Name as CompanyName,[Advertisement].Name as AdvertName" +
      ",[Advertisement].Item,[Advertisement].ItemType,[Advertisement].StartDate,[Advertisement].EndDate from [Advertisement] inner join [Company] on [Advertisement].CompanyID =[Company].CompanyID " +
              "where  [Advertisement].status=1 and [Advertisement].StartDate>=@sDate and [Advertisement].EndDate<=@eDate and" +
              " ([Advertisement].Name like '%' + @search + '%' OR [Company].Name like '%' + @search + '%' OR ItemType like '%'" +
              " + @search + '%' OR StartDate like '%' + @search + '%' OR  EndDate like '%' + @search + '%') ";
                    SqlCommand xp = new SqlCommand(str, vid);
                    xp.Parameters.Add("@search", SqlDbType.NVarChar).Value = txtSearch.Text;
                    xp.Parameters.Add("@sDate", SqlDbType.DateTime).Value = sdate;
                    xp.Parameters.Add("@eDate", SqlDbType.DateTime).Value = edate;
                    //xp.Parameters.Add("@search2", SqlDbType.NVarChar).Value = txtSearch.Text;
                    vid.Open();
                    xp.ExecuteNonQuery();
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = xp;
                    DataSet ds = new DataSet();
                    da.Fill(ds, "Name");
                    GridView1.DataSource = ds;
                    GridView1.DataBind();
                }
            }
            //string str = " select [Company].Name as CompanyName,[Advertisement].Name as AdvertName" +
            //    ",[Advertisement].Item,[Advertisement].ItemType,[Advertisement].StartDate,[Advertisement].EndDate from [Advertisement] inner join [Company] on [Advertisement].CompanyID =[Company].CompanyID " +
            //            "where [Company].CompanyID=@ID and [Advertisement].status=1 and ([Advertisement].Name like '%' + @search + '%' OR ItemType like '%'" +
            //            " + @search + '%' OR StartDate like '%' + @search + '%' OR  EndDate like '%' + @search + '%') ";
            //SqlCommand xp = new SqlCommand(str, vid);
            //xp.Parameters.Add("@ID", SqlDbType.NVarChar).Value = Session["CompanyID"].ToString();
            //xp.Parameters.Add("@search", SqlDbType.NVarChar).Value = txtSearch.Text;
            ////xp.Parameters.Add("@search2", SqlDbType.NVarChar).Value = txtSearch.Text;
            //vid.Open();
            //xp.ExecuteNonQuery();
            //SqlDataAdapter da = new SqlDataAdapter();
            //da.SelectCommand = xp;
            //DataSet ds = new DataSet();
            //da.Fill(ds, "Name");
            //GridView1.DataSource = ds;
            //GridView1.DataBind();
        }



        protected void infoBtn_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "AdInfo")
            {

                int index = Convert.ToInt32(e.CommandArgument);


                // Retrieve the row that contains the link button from the Rows collection.
                GridViewRow row = GridView1.Rows[index];
                LinkButton lbButton1 = sender as LinkButton;
                GridViewRow gvRow1 = (GridViewRow)lbButton1.NamingContainer;

                //Need to Retrieve userID to display info of user
                Label lb_BillboardID = (Label)gvRow1.FindControl("lb_AdvertID");

                Session["AdvertID"] = lb_BillboardID.Text;

                Response.Redirect("AdvertInfo.aspx");
            }
        }
        protected void editBtn_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "AdvertUpdate")
            {

                int index = Convert.ToInt32(e.CommandArgument);


                // Retrieve the row that contains the link button from the Rows collection.
                GridViewRow row = GridView1.Rows[index];
                LinkButton lbButton1 = sender as LinkButton;
                GridViewRow gvRow1 = (GridViewRow)lbButton1.NamingContainer;

                //Need to Retrieve userID to edit user
                Label lb_AdvID = (Label)gvRow1.FindControl("lb_AdvertID");

                Session["AdvertID"] = lb_AdvID.Text;

                Response.Redirect("AdListingUpdate.aspx");
            }
        }


        protected void btnDelete_Command(object sender, CommandEventArgs e)
        {

            SqlConnection conn = null;
            SqlDataReader reader = null;



            // instantiate and open connection
            conn = new
                SqlConnection(@"Data Source=L33527\CHEEEFANGSQL;Initial Catalog=Targeted_Marketing_Display;Persist Security Info=True;User ID=root;Password=passw8rd");
            conn.Open();

            if (e.CommandName == "DeleteAdMessage")
            {
                int index = Convert.ToInt32(e.CommandArgument);


                // Retrieve the row that contains the button 
                // from the Rows collection.
                GridViewRow row = GridView1.Rows[index];
                LinkButton btnButton1 = sender as LinkButton;
                GridViewRow gvRow1 = (GridViewRow)btnButton1.NamingContainer;
                
                Advertisement aObj = new Advertisement();
                Advertisement_Management aDao = new Advertisement_Management();
             
               
                Label lb_msgId = (Label)gvRow1.FindControl("lb_AdvertID");
                aObj = aDao.getAdvByID(lb_msgId.Text);


        
                //   bObj = bDao.getBillboardByID(lb_msgId.Text);
                //    string BBCode = bObj.BillboardCode;
               



                //Boolean insCnt = bDao.deleteBillboard(lb_msgId.Text);
                Boolean DeleteAd = aDao.deleteAdvert(lb_msgId.Text);
                //VIC: never inform if the delete is successful or not?
                alertSuccessDelete.Visible = true;
                alertSuccessCreate.Visible = false;
                alertSuccessUpdate.Visible = false;
                Label3.Text = " Advert '" + aObj.Name + "' Has Been Deleted Successfully!";
                //" SELECT [Advertisement].AdvID,[Company].Name as CompanyName, [Advertisement].Name as AdvertName, [Advertisement].Item, [Advertisement].ItemType,[StartDate], [EndDate]FROM " +
                // "[Advertisement] inner join [Company] on Company.CompanyID =[Advertisement].CompanyID where [Advertisement].status = 1 and[Company].status = 1", conn);
                Database db = new Database();
                if (Session["userType"].ToString() == Reference.USR_ADM)
                {
                    SqlCommand cmd = new SqlCommand("SELECT [Advertisement].AdvID,[Company].Name as CompanyName, [Advertisement].Name as AdvertName, [Advertisement].Item, [Advertisement].ItemType,[StartDate], [EndDate]FROM " +
                        "[Advertisement] inner join [Company] on Company.CompanyID =[Advertisement].CompanyID where [Advertisement].status = 1 and[Company].status = 1",conn);
                    SqlDataAdapter sda = new SqlDataAdapter();
                    DataTable dt = new DataTable();
                    sda.SelectCommand = cmd;
                    sda.Fill(dt);
                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                }
                else
                {
                    User uObj = new User();
                    UserManagement uDao = new UserManagement();
                    uObj = uDao.getUserByID(Session["userID"].ToString());
                    SqlCommand cmd = new SqlCommand("SELECT [Advertisement].AdvID,[Company].Name as CompanyName, [Advertisement].Name as AdvertName, [Advertisement].Item, [Advertisement].ItemType,[StartDate], [EndDate]FROM " +
                        "[Advertisement] inner join [Company] on Company.CompanyID =[Advertisement].CompanyID where [Advertisement].status = 1 and[Company].status = 1 and [Advertisement].CompanyID=@comID",conn);
                    SqlParameter param = new SqlParameter();
                    param.ParameterName = "@comID";
                    param.Value = uObj.CompanyID.ToString();
                    cmd.Parameters.Add(param);
                    SqlDataAdapter sda = new SqlDataAdapter();
                    DataTable dt = new DataTable();
                    sda.SelectCommand = cmd;
                    sda.Fill(dt);
                    GridView1.DataSource = dt;
                    GridView1.DataBind();

                }
                

               

            }
        }





        protected void GridView1_PreRender(object sender, EventArgs e)
        {
            Label1.Text = "Displaying Page " + (GridView1.PageIndex + 1).ToString() + " of " + GridView1.PageCount.ToString();
            // Label lb_msgId = (Label)gvRow1.FindControl("AdvertItem");.jpeg
            //for (int i = 0; i < GridView1.Rows.Count; i++)
            //{
            //    Label lb_AdvertType = (Label)GridView1.Rows[i].FindControl("AdvertItem");
            //    if (lb_AdvertType.Text.EndsWith(".png") || lb_AdvertType.Text.EndsWith(".jpg") || lb_AdvertType.Text.EndsWith(".jpeg")||
            //         lb_AdvertType.Text.EndsWith(".PNG") || lb_AdvertType.Text.EndsWith(".JPG") || lb_AdvertType.Text.EndsWith(".JPEG") 
            //         || lb_AdvertType.Text.EndsWith(".GIF"))
            //    {


            //    }
            //}
        }
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            GridView1.PageIndex = e.NewPageIndex;
            GridView1.DataBind();
            BindGrid();
            alertSuccessCreate.Visible = false;
            alertSuccessDelete.Visible = false;
            alertSuccessUpdate.Visible = false;
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }
    }
}