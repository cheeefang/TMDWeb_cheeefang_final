using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoInfoMgmt;
using targeted_marketing_display.App_Code;
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
                CoIndustry.Text = CompanyObj.Industry;

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
                string Industry = CoIndustry.SelectedValue;
                string lastUpdBy = Session["userID"].ToString();
                string lastUpdOn = DateTime.Now.ToString("MM/dd/yyyy h:mm tt");
                Boolean insCnt = cDAO.CoInfoUpdate(Session["CompanyID"].ToString(), companyName,Industry,lastUpdBy,lastUpdOn);

                alertWarning.Visible = false;
                alertSuccess.Visible = true;
                CoName.Text = String.Empty;
                CoIndustry.SelectedValue = "";

            }
        }
    }
}