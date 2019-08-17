using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using targeted_marketing_display;
using System.Text;
using System.Threading.Tasks;

using System.Configuration;


namespace targeted_marketing_display
   
{
    public partial class CompanyList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (Convert.ToInt32(Session["CoUpdate"]) == 2)
            {
                updateSuccess.Visible = true;
                Session.Remove("CoUpdate");
            }
            if (Convert.ToInt32(Session["CoCreate"]) == 2)
            {
                createSuccess.Visible = true;
                Session.Remove("CoCreate");
            }
            
            if (!IsPostBack)
            {
                
                    Company_Management coMgmt = new Company_Management();
                    DataTable dt = coMgmt.CoInfoRead();

                    //GridView3.DataSource = dt;
                    GridView3.DataBind();
                
             
            }
        }

        protected void adsBtn_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName== "AdvertInfo")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridView3.Rows[index];
                LinkButton lbButton1 = sender as LinkButton;
                GridViewRow gvRow1 = (GridViewRow)lbButton1.NamingContainer;
                //Need to Retrieve CompanyID to edit user
                Label lb_CompanyID = (Label)gvRow1.FindControl("lb_CompanyID");

                Session["CompanyID"] = lb_CompanyID.Text;


                Response.Redirect("CompanyInfo.aspx");

            }

        }
        protected void btnDelete_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "DeleteMessage")
            {
                SqlConnection conn = null;
                SqlDataReader reader = null;



                // instantiate and open connection
                conn = new
                    SqlConnection(@"Data Source=L33527\CHEEEFANGSQL;Initial Catalog=Targeted_Marketing_Display;Persist Security Info=True;User ID=root;Password=passw8rd");
                conn.Open();
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

                SqlCommand cmdCount = new SqlCommand("select count(*) as total from Advertisement as a inner join Company as c on a.companyID=c.CompanyID where c.CompanyID=@ID and a.status=1", conn);
                string CompanyName = Obj.Name;
                SqlParameter param1 = new SqlParameter();
                param1.ParameterName = "@ID";
                param1.Value = Obj.CompanyID.ToString();
                cmdCount.Parameters.Add(param1);
                SqlDataAdapter sda1 = new SqlDataAdapter();
                DataTable dt = new DataTable();
                cmdCount.Connection = conn;
                sda1.SelectCommand = cmdCount;
                sda1.Fill(dt);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    int count = (Convert.ToInt32(dt.Rows[i]["total"]));
                    if (count ==0)
                    {
                        Boolean insCnt = cDao.deleteCompany(lb_msgId.Text);
                        //VIC: never inform if the delete is successful or not?
                        alertSuccess.Visible = true;
                        DeleteError.Visible = false;
                        createSuccess.Visible = false;
                        updateSuccess.Visible = false;
                        msgSuccess.Text = CompanyName + " Has Been Deleted Successfully!";
                    }
                    else
                    {
                        DeleteError.Visible = true;
                        alertSuccess.Visible = false;
                        createSuccess.Visible = false;
                        updateSuccess.Visible = false;
                        DeleteLabel.Text = "Hi, there is/are " + count + " Advertisement(s) under " + CompanyName + ".Please Delete existing advertisement(s) first before proceeding to delete the company.";
                    }
                }
                

               

                Database db = new Database();

                SqlCommand cmd = new SqlCommand("Select * from [Company] where Status = 1");
                
              //  cmd.Parameters.AddWithValue("@paraType", (string)Session["userType"]);
                DataSet ds = db.getDataSet(cmd);

              
                GridView3.DataBind();

            }
        }


        protected void editBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("CompanyUpdate.aspx");

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

                Response.Redirect("CompanyUpdate.aspx");
            }
        }



        protected void searchBtn_Click(object sender, EventArgs e)
        {

        }


        protected void GridView3_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            GridView3.PageIndex = e.NewPageIndex;
            GridView3.DataBind();
            alertSuccess.Visible = false;
            updateSuccess.Visible = false;
            createSuccess.Visible = false;
            DeleteError.Visible = false;
        }

        protected void GridView3_PreRender(object sender, EventArgs e)
        {
            Label1.Text = "Displaying Page " + (GridView3.PageIndex + 1).ToString() + " of " + GridView3.PageCount.ToString();
        }

        protected void GridView3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}