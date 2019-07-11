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
    public partial class Recordlist_admin : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(@"Data Source=L33527\CHEEEFANGSQL;Initial Catalog=Targeted_Marketing_Display;Persist Security Info=True;User ID=root;Password=passw8rd");




        protected void Page_Load(object sender, EventArgs e)
        {

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
                string AdvName = aObj.Name;



                //Boolean insCnt = bDao.deleteBillboard(lb_msgId.Text);
                Boolean DeleteAd = aDao.deleteAdvert(lb_msgId.Text);
                //VIC: never inform if the delete is successful or not?
                alertSuccess.Visible = true;
                msgSuccess.Text = " Advert " + AdvName + " Has Been Deleted Successfully!";

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
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}