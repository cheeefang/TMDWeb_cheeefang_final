using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using targeted_marketing_display;
namespace targeted_marketing_display
{
    public partial class CoInfoUpdate : System.Web.UI.Page
    {
        //CoName=companynametextboxID
        //CoIndustry=companyIndustrytextboxID
        protected void Page_Load(object sender, EventArgs e)
        {
         
                
            if (!IsPostBack)
            {


                Company CompanyObj = new Company();
                Company_Management CDao = new Company_Management();
               

                CompanyObj=CDao.getCompanyByID(Session["CompanyID"].ToString());

                CoName.Text = CompanyObj.Name;
                CoIndustry.SelectedValue = CompanyObj.Industry;
                Database db = new Database();
                SqlCommand cmd = new SqlCommand("select * from CodeReferece where CodeType='Industry' ");
                DataTable dt = db.getDataTable(cmd);
                CoIndustry.DataSource = dt;
                CoIndustry.DataValueField = "CodeValue";
                CoIndustry.DataTextField = "CodeValue";
                CoIndustry.DataBind();
                CoIndustry.Items.Insert(0, new ListItem("---Select An Industry---", "0"));

            }
        }

        protected void updateBtn_Click(object sender, EventArgs e)
        {
            if (CoName.Text == "" || CoIndustry.SelectedValue == "")
            {
                alertWarning.Visible = true;
                alertSuccess.Visible = false;
            }

            else
            {
                Company_Management cDAO = new Company_Management();
                string companyName = CoName.Text;
                string Industry = CoIndustry.SelectedItem.Value.ToString();
                string lastUpdBy = Session["userID"].ToString();
                string lastUpdOn = DateTime.Now.ToString("MM/dd/yyyy h:mm tt");
                Boolean insCnt = cDAO.CoInfoUpdate(Session["CompanyID"].ToString(), companyName,Industry,lastUpdBy,lastUpdOn);

                alertWarning.Visible = false;
                alertSuccess.Visible = true;
                CoName.Text = String.Empty;
                CoIndustry.SelectedItem.Value = "";
                Session["CoUpdate"] = 2;
                Response.Redirect("CoInfoRead.aspx");

            }
          
        }
    }
}