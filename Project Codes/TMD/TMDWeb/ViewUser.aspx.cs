using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;

using targeted_marketing_display.App_Code;


namespace targeted_marketing_display
{
    public partial class DeleteUser : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
               
                Database db = new Database();

                SqlCommand cmd = new SqlCommand("Select * from [User] WHERE Type != @paraType and [User].Status = 1");

                cmd.Parameters.AddWithValue("@paraType", (string)Session["userType"]);
                DataSet ds = db.getDataSet(cmd);
                
                //gvUser.DataSource = ds;
                gvUser.DataBind();

            }
        }

        protected void btnDelete_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "DeleteMessage")
            {
                int index = Convert.ToInt32(e.CommandArgument);


                // Retrieve the row that contains the button 
                // from the Rows collection.
                GridViewRow row = gvUser.Rows[index];
                LinkButton btnButton1 = sender as LinkButton;
                GridViewRow gvRow1 = (GridViewRow)btnButton1.NamingContainer;

                User uObj = new User();
                UserManagement uDao = new UserManagement();

                Label lb_msgId = (Label)gvRow1.FindControl("lb_UserID");
              
                uObj = uDao.getUserByID(lb_msgId.Text);
                string userName = uObj.Name;

                Boolean insCnt = uDao.deleteQns(lb_msgId.Text);

                //VIC: never inform if the delete is successful or not?
                alertSuccess.Visible = true;
                msgSuccess.Text = userName + " Has Been Deleted Successfully!";

                Database db = new Database();

                SqlCommand cmd = new SqlCommand("Select * from [User] WHERE Type != @paraType and Status = 1");

                cmd.Parameters.AddWithValue("@paraType", (string)Session["userType"]);
                DataSet ds = db.getDataSet(cmd);

                //gvUser.DataSource = ds;
                gvUser.DataBind();

            }
        }

        protected void infoBtn_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "userInfo")
            {

                  int index = Convert.ToInt32(e.CommandArgument);


                // Retrieve the row that contains the link button from the Rows collection.
                GridViewRow row = gvUser.Rows[index];
                LinkButton lbButton1 = sender as LinkButton;
                GridViewRow gvRow1 = (GridViewRow)lbButton1.NamingContainer;

                //Need to Retrieve userID to display info of user
                Label lb_UserID = (Label)gvRow1.FindControl("lb_UserID");

                Session["SelectedID"]= lb_UserID.Text;

                Response.Redirect("OtherUserProfile.aspx");
            }
        }



        protected void editBtn_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "editInfo")
            {

                int index = Convert.ToInt32(e.CommandArgument);


                // Retrieve the row that contains the link button from the Rows collection.
                GridViewRow row = gvUser.Rows[index];
                LinkButton lbButton1 = sender as LinkButton;
                GridViewRow gvRow1 = (GridViewRow)lbButton1.NamingContainer;

                //Need to Retrieve userID to edit user
                Label lb_UserID = (Label)gvRow1.FindControl("lb_UserID");

                Session["SelectedID"] = lb_UserID.Text;

                Response.Redirect("UpdateOtherUserProfile.aspx");
            }
        }

        protected void tbSearch_TextChanged(object sender, EventArgs e)
        {
            Database db = new Database();

            SqlCommand cmd = new SqlCommand("Select * from [User] WHERE Type != @paraType and Status = 1 and (Name like '%' + @paraSearch + '%' or UserID like '%' + @paraSearch + '%')");

            cmd.Parameters.AddWithValue("@paraSearch", tbSearch.Text);
            cmd.Parameters.AddWithValue("@paraType", (string)Session["userType"]);
            DataSet ds = db.getDataSet(cmd);

            //gvUser.DataSource = ds;
            gvUser.DataBind();
        }

        protected void gvUser_PreRender(object sender, EventArgs e)
        {
            LabelPaging.Text = "Displaying Page " + (gvUser.PageIndex + 1).ToString() + " of " + gvUser.PageCount.ToString();
        }
    }
}