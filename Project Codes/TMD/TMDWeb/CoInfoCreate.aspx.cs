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
            Database db = new Database();

            //SqlCommand cmd = new SqlCommand("Select * from Company where status=1");
            //DataTable dt = db.getDataTable(cmd);

            //ddlCompany.DataSource = dt;
            //ddlCompany.DataValueField = "CompanyID";
            //ddlCompany.DataTextField = "Name";
            //ddlCompany.DataBind();

            //ddlCompany.Items.Insert(0, new ListItem("---Select A Company---", "0"));
            //SqlCommand cmdUserType = new SqlCommand("select CodeValue,CodeDesc from CodeReferece where CodeType='UserType'");
            //DataTable dtUserType = db.getDataTable(cmdUserType);
            //ddlUserType.DataSource = dtUserType;
            //ddlUserType.DataValueField = "CodeValue";
            //ddlUserType.DataTextField = "CodeDesc";
            //ddlUserType.DataBind();
            //ddlUserType.Items.Insert(0, new ListItem("---Select A User Type---", "0"));
            SqlCommand cmd = new SqlCommand("select * from CodeReferece where CodeType='Industry' ");
            DataTable dt = db.getDataTable(cmd);
            CoIndustry.DataSource = dt;
            CoIndustry.DataValueField = "CodeValue";
            CoIndustry.DataTextField = "CodeValue";
            CoIndustry.DataBind();
            CoIndustry.Items.Insert(0, new ListItem("---Select An Industry---", "0"));


        }
        protected void SubmitBtn_Click(object sender, EventArgs e)
        {

            String Industry = (CoIndustry.SelectedItem.Value.ToString());
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
                CoIndustry.SelectedItem.Value = "";
                Session["CoCreate"] = 2;
                Response.Redirect("CoInfoRead.aspx");
            }
            
        }
    }
}