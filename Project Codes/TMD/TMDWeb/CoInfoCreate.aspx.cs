using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using targeted_marketing_display.App_Code;
using CoInfoMgmt;

namespace targeted_marketing_display
{
    public partial class CoInfoCreate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void SubmitBtn_Click(object sender, EventArgs e)
        {

            String Industry = (CoIndustry.SelectedValue.ToString());
            String CompanyName = CoName.Text.ToString();
            DateTime CreatedOn = DateTime.Now;

            Company_Management coMgmt = new Company_Management();
            Boolean result = coMgmt.CoInfoInsert(CompanyName, Industry, CreatedOn);

            if (CoName.Text == "" || CoIndustry.SelectedValue == "")
            {
                alertWarning.Visible = true;
                alertSuccess.Visible = false;

                warningLocation.Text = "Please ensure you have filled in all required fields";
            }

            else
            {
                alertWarning.Visible = false;
                alertSuccess.Visible = true;
                CoName.Text = String.Empty;
                CoIndustry.SelectedValue = "";

            }
            Session["CoCreate"] = 2;
            Response.Redirect("CoInfoRead.aspx");
        }
    }
}