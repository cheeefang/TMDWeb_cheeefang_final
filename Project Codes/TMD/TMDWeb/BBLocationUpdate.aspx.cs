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
using BBMgmt;
namespace targeted_marketing_display
{
    public partial class BBLocationUpdate : System.Web.UI.Page
    {
        protected string latitude;
        protected string longtitude;
        protected void Page_Load(object sender, EventArgs e)
        {

          

            if (!IsPostBack)
            {
                Billboard BillboardObj = new Billboard();
                Billboard_Management bDao = new Billboard_Management();
                //Company CompanyObj = new Company();
                // Company_Management CDao = new Company_Management();

                BillboardObj = bDao.getBillboardByID(Session["BillboardID"].ToString());
                latitude = BillboardObj.latitude;
                longtitude = BillboardObj.Longtitude;
                //CompanyObj = CDao.getCompanyByID(Session["CompanyID"].ToString());
                Database db = new Database();
                //SqlCommand cmd = new SqlCommand("select CodeValue,CodeDesc from CodeReferece where CodeType=Industry ");
                //DataTable dt = db.getDataTable(cmd);
                //CoIndustry.DataSource = dt;
                //CoIndustry.DataValueField = "CodeValue";
                //CoIndustry.DataTextField = "CodeDesc";
                //CoIndustry.DataBind();
                //CoIndustry.Items.Insert(0, new ListItem("---Select An Industry---", "0"));
                SqlCommand cmd = new SqlCommand("select CodeValue,CodeDesc from CodeReferece where CodeType='Country' ");
                DataTable dt = db.getDataTable(cmd);
                BBCountry.DataSource = dt;
                BBCountry.DataValueField = "CodeValue";
                BBCountry.DataTextField = "CodeDesc";
                BBCountry.DataBind();
                BBCountry.Items.Insert(0, new ListItem("---Select A Country---", "0"));


                BBAddLn1.Text = BillboardObj.AddressLn1;
                BBAddLn2.Text = BillboardObj.AddressLn2;
                BBCity.Text = BillboardObj.City;
                BBCountry.SelectedValue = BillboardObj.Country;
                BBLatitude.Text = BillboardObj.latitude;
                BBLongtitude.Text = BillboardObj.Longtitude;

                BBPostalCode.Text = BillboardObj.postalCode;


            }


        }

        protected void SubmitBtn2_Click(object sender, EventArgs e)
        {

        }

        protected void updateBtn_Click(object sender, EventArgs e)
        {

            if ( BBCountry.SelectedValue == "" || BBAddLn1.Text == ""
                || BBCity.Text == "" || BBPostalCode.Text == ""|| BBLatitude.Text=="" || BBLongtitude.Text=="")
            {
                alertWarning.Visible = true;
                alertSuccess.Visible = false;
            }

            else
            {

                Billboard_Management bDAO = new Billboard_Management();
                //Company_Management cDAO = new Company_Management();
                // string companyName = CoName.Text;
                // string Industry = CoIndustry.SelectedValue;
                
                string Addr1 = BBAddLn1.Text.ToString();
                string Addr2 = BBAddLn2.Text.ToString();
                string City = BBCity.Text.ToString();
                string Country = BBCountry.SelectedItem.Value.ToString();
                string latitude = BBLatitude.Text.ToString();
                string Longtitude = BBLongtitude.Text.ToString();
                string postalCode = BBPostalCode.Text.ToString();
                string lastUpdBy = Session["userID"].ToString();
                string lastUpdOn = DateTime.Now.ToString("MM/dd/yyyy h:mm tt");
                //(string BillboardID, string AddressLn1, string AddressLn2, string City, string Country, string latitude, string Longtitude, string postalCode, string lastUpdBy, string lastUpdOn)
                Boolean insCnt = bDAO.BBInfoUpdate(Session["BillboardID"].ToString(), Addr1, Addr2, City, Country,latitude,Longtitude,postalCode,lastUpdBy, lastUpdOn);
                alertWarning.Visible = false;
                alertSuccess.Visible = true;
             
                BBAddLn1.Text = String.Empty;
                BBAddLn2.Text = String.Empty;
                BBCountry.SelectedItem.Value = "";
                BBCity.Text = String.Empty;
                BBPostalCode.Text = String.Empty;
                BBLatitude.Text = String.Empty;
                BBLongtitude.Text = String.Empty;
                Session["BBUpdate"] = 2;
                Response.Redirect("BBLocationRead.aspx");

            }
           
        }
        

    }
}