using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using targeted_marketing_display.App_Code;
using System.Configuration;
namespace targeted_marketing_display
{
 

    public partial class AdListing : System.Web.UI.Page
    {
        private string sortExpression;
        private SortDirection sortDirection;
        SqlConnection con = new SqlConnection(@"Data Source=L33527\CHEEEFANGSQL;Initial Catalog=Targeted_Marketing_Display;Persist Security Info=True;User ID=root;Password=passw8rd");




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
            }
            else
            {
                if (ViewState["SortExpression"] != null)
                    sortExpression = ViewState["SortExpression"].ToString();
                else
                    sortExpression = String.Empty;

                if (ViewState["SortDirection"] != null)
                {
                    if (Convert.ToInt32(ViewState["SortDirection"]) == (int)SortDirection.Ascending)
                    {
                        sortDirection = SortDirection.Ascending;
                    }
                    else
                    {
                        sortDirection = SortDirection.Descending;
                    }
                }
            }

        }
        //DataGridColumn dgcolumn = new DataGridColumn();

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

                Response.Redirect("AdListingInfo.aspx");
            }
        }
        protected void editBtn_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "AdUpdateInfo")
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
                Label3.Text = " Advert# " + aObj.AdvID + " Has Been Deleted Successfully!";

                Database db = new Database();

                SqlCommand cmd = new SqlCommand("Select * from [Advertisement] WHERE Status = 1");
                DataSet ds = db.getDataSet(cmd);

                //gvUser.DataSource = ds;
                GridView1.DataBind();

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

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }
    }
}