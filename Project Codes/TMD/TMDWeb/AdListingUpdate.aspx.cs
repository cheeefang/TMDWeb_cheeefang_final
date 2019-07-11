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
using BBMgmt;
using targeted_marketing_display;
using targeted_marketing_display.App_Code;
using System.Net;
using System.Xml.Linq;
namespace targeted_marketing_display
{
    public partial class AdListingUpdate : System.Web.UI.Page
    {
        string dbConnStr = ConfigurationManager.ConnectionStrings["Targeted_Marketing_DisplayConnectionString"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if ((string)Session["userType"] == "Admin")
                {
                    divCompany.Visible = true;
                    DropDownListCompany.Visible = true;
                    //  int companyID = Convert.ToInt32(DropDownListCompany.SelectedItem.Value);
                }
                else
                {
                    // User userObj = new User();
                    //  UserManagement uDao = new UserManagement();
                    divCompany.Visible = false;
                    DropDownListCompany.Visible = false;
                    // userObj = uDao.getUserByID(Session["userID"].ToString());
                    // int companyID = userObj.CompanyID;
                }
                Database db = new Database();
                string mainconn = ConfigurationManager.ConnectionStrings["Targeted_Marketing_DisplayConnectionString"].ConnectionString;
                SqlConnection sqlconn = new SqlConnection(dbConnStr);
                string sqlquery = "SELECT * FROM [CodeReferece] WHERE ([CodeType] = @CodeType)";
                SqlCommand cmd = new SqlCommand(sqlquery, sqlconn);
                cmd.Parameters.AddWithValue("@CodeType", "Category");
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                CheckBoxList1.DataSource = dt;
                CheckBoxList1.DataBind();
                Advertisement AdvertObj = new Advertisement();
                Advertisement_Management aDao = new Advertisement_Management();
                AdvertObj = aDao.getAdvByID(Session["AdvertID"].ToString());
                DropDownListCompany.SelectedValue = AdvertObj.CompanyID.ToString();
                videoDurationTB.Text = AdvertObj.Duration.ToString();
                adNameTB.Text = AdvertObj.Name.ToString();
                startDateTB.Text = AdvertObj.StartDate.ToString();
                endDateTB.Text = AdvertObj.EndDate.ToString();
                //string sqlquery2 = "Select CategoryID from [AdvertisementCategory] where AdvID=@ID";
                //SqlParameter param = new SqlParameter();
                //param.ParameterName = "@ID";
                //param.Value = Session["AdvertID"].ToString();
                //SqlCommand cmdCategory = new SqlCommand(sqlquery2, sqlconn);
                //cmdCategory.Parameters.Add(param);
            }
        }
    }
}