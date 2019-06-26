using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using CoInfoMgmt;
using targeted_marketing_display.App_Code;

namespace targeted_marketing_display
   
{
    public partial class CoInfoRead : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
              
                Company_Management coMgmt = new Company_Management();
                DataTable dt = coMgmt.CoInfoRead();

                //GridView3.DataSource = dt;
                GridView3.DataBind();
            }
        }

        protected void adsBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("##");

        }
        protected void btnDelete_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "DeleteMessage")
            {
                int index = Convert.ToInt32(e.CommandArgument);


                // Retrieve the row that contains the button 
                // from the Rows collection.
                GridViewRow row = GridView3.Rows[index];
                LinkButton btnButton1 = sender as LinkButton;
                GridViewRow gvRow1 = (GridViewRow)btnButton1.NamingContainer;

                Company Obj = new Company();
                Company_Management cDao = new Company_Management();

                Label lb_msgId = (Label)gvRow1.FindControl("lb_CompanyID");
              
                Obj = cDao.getCompanyByID(lb_msgId.Text);
                string CompanyName = Obj.Name;

                Boolean insCnt = cDao.deleteCompany(lb_msgId.Text);

                //VIC: never inform if the delete is successful or not?
                alertSuccess.Visible = true;
                msgSuccess.Text = CompanyName + " Has Been Deleted Successfully!";

                Database db = new Database();

                SqlCommand cmd = new SqlCommand("Select * from [Company] where Status = 1");

              //  cmd.Parameters.AddWithValue("@paraType", (string)Session["userType"]);
                DataSet ds = db.getDataSet(cmd);

              
                GridView3.DataBind();

            }
        }


        protected void editBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("CoInfoUpdate.aspx");

        }

        protected void editBtn_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "editcompanyInfo")
            {

                int index = Convert.ToInt32(e.CommandArgument);


                // Retrieve the row that contains the link button from the Rows collection.
                GridViewRow row = GridView3.Rows[index];
                LinkButton lbButton1 = sender as LinkButton;
                GridViewRow gvRow1 = (GridViewRow)lbButton1.NamingContainer;

                //Need to Retrieve CompanyID to edit user
                Label lb_CompanyID = (Label)gvRow1.FindControl("lb_CompanyID");

                Session["CompanyID"] = lb_CompanyID.Text;

                Response.Redirect("CoInfoUpdate.aspx");
            }
        }



        protected void searchBtn_Click(object sender, EventArgs e)
        {

        }

        

    }
}