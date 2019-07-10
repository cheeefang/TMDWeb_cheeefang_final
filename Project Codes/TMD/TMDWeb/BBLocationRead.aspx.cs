using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data;
using System.Data.SqlClient;
using BBMgmt;
using System.Text;


using targeted_marketing_display.App_Code;
namespace targeted_marketing_display
{
    public partial class BBLocationRead : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            //ADM Or admin
            if (Session["userType"].ToString() == "Admin")
            {
                adminDiv.Visible = true;
                userDiv.Visible = false;
            }
            else
            {
                adminDiv.Visible = false;
                userDiv.Visible = true;
            }
            //SqlConnection conn = null;
            //SqlDataReader reader = null;

            //// instantiate and open connection
            //conn = new
            //    SqlConnection(@"Data Source=L33527\CHEEEFANGSQL;Initial Catalog=Targeted_Marketing_Display;Persist Security Info=True;User ID=root;Password=passw8rd");
            //conn.Open();
            Billboard_Management bbMgmt = new Billboard_Management();
                DataTable dt = bbMgmt.BBread();
            
            //GridView1.DataSource = dt;
            GridView1.DataBind();
            
                //GridView2.DataSource = dt;
                GridView2.DataBind();
                //foreach (GridViewRow row in GridView1.Rows)
                //{
                //    for (int i = 1; i <= GridView1.Rows.Count; i++)
                //    {
                        
                //        string BillboardID = GridView1.Rows[i].Cells[0].Text.ToString();
                //        SqlCommand cmd = new SqlCommand(
                //        "select count(*) from AdvertisementLocation where BillboardID=@ID ", conn);
                //        SqlParameter param = new SqlParameter();
                //        param.ParameterName = "@ID";
                //        param.Value = BillboardID;
                        

                //}

                //}

        }

        protected void editBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("BBLocationUpdate.aspx");

        }

        protected void adsBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("##");

        }


        protected void infoBtn_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "BillboardAdInfo")
            {

                int index = Convert.ToInt32(e.CommandArgument);


                // Retrieve the row that contains the link button from the Rows collection.
                GridViewRow row = GridView1.Rows[index];
                LinkButton lbButton1 = sender as LinkButton;
                GridViewRow gvRow1 = (GridViewRow)lbButton1.NamingContainer;

                //Need to Retrieve userID to display info of user
                Label lb_BillboardID = (Label)gvRow1.FindControl("lb_BillboardID");

                Session["BillboardID"] = lb_BillboardID.Text;

                Response.Redirect("BillboardAdInfo.aspx");
            }
        }

        protected void editBtn_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "BillboardUpdateInfo")
            {

                int index = Convert.ToInt32(e.CommandArgument);


                // Retrieve the row that contains the link button from the Rows collection.
                GridViewRow row = GridView1.Rows[index];
                LinkButton lbButton1 = sender as LinkButton;
                GridViewRow gvRow1 = (GridViewRow)lbButton1.NamingContainer;

                //Need to Retrieve userID to edit user
                Label lb_BillboardID = (Label)gvRow1.FindControl("lb_BillboardID");

                Session["BillboardID"] = lb_BillboardID.Text;

                Response.Redirect("BBLocationUpdate.aspx");
            }
        }

        protected void btnDelete_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "DeleteBBMessage")
            {
                int index = Convert.ToInt32(e.CommandArgument);


                // Retrieve the row that contains the button 
                // from the Rows collection.
                GridViewRow row = GridView1.Rows[index];
                LinkButton btnButton1 = sender as LinkButton;
                GridViewRow gvRow1 = (GridViewRow)btnButton1.NamingContainer;
                Billboard bObj = new Billboard();
                //User uObj = new User();
                //UserManagement uDao = new UserManagement();
                Billboard_Management bDao = new Billboard_Management();



                Label lb_msgId = (Label)gvRow1.FindControl("lb_BillboardID");

                bObj = bDao.getBillboardByID(lb_msgId.Text);
                string  BBCode= bObj.BillboardCode;




                Boolean insCnt = bDao.deleteBillboard(lb_msgId.Text);

                //VIC: never inform if the delete is successful or not?
                alertSuccess.Visible = true;
                msgSuccess.Text =" Billboard #"+ BBCode + " Has Been Deleted Successfully!";

                Database db = new Database();

                SqlCommand cmd = new SqlCommand("Select * from [BillboardLocation] WHERE Status = 1");             
                DataSet ds = db.getDataSet(cmd);

                //gvUser.DataSource = ds;
                GridView1.DataBind();

            }
        }











        protected void GridView1_PreRender(object sender, EventArgs e)
        {
            Label1.Text = "Displaying Page " + (GridView1.PageIndex + 1).ToString() + " of " + GridView1.PageCount.ToString();
        }
    }
}
