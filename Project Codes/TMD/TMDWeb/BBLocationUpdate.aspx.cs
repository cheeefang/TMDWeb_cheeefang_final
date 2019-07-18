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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Billboard BillboardObj = new Billboard();
                Billboard_Management bDao = new Billboard_Management();
                //Company CompanyObj = new Company();
                // Company_Management CDao = new Company_Management();

                BillboardObj = bDao.getBillboardByID(Session["BillboardID"].ToString());
                //CompanyObj = CDao.getCompanyByID(Session["CompanyID"].ToString());
          
                BbAddLn1.Text = BillboardObj.AddressLn1;
                BbAddLn2.Text = BillboardObj.AddressLn2;
                BbCity.Text = BillboardObj.City;
                BBCountry.SelectedValue = BillboardObj.Country;
                BBLatitude.Text = BillboardObj.latitude;
                BBLongtitude.Text = BillboardObj.Longtitude;

                BbPostalCode.Text = BillboardObj.postalCode;


            }


        }

        protected void SubmitBtn2_Click(object sender, EventArgs e)
        {

        }

        protected void updateBtn_Click(object sender, EventArgs e)
        {

            if ( BBCountry.SelectedValue == "" || BbAddLn1.Text == ""
                || BbCity.Text == "" || BbPostalCode.Text == "" || BbAddLn2.Text=="" || BBLatitude.Text=="" || BBLongtitude.Text=="")
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
                
                string Addr1 = BbAddLn1.Text;
                string Addr2 = BbAddLn2.Text;
                string City = BbCity.Text;
                string Country = BBCountry.SelectedValue;
                string latitude = BBLatitude.Text;
                string Longtitude = BBLongtitude.Text;
                string postalCode = BbPostalCode.Text;
                string lastUpdBy = Session["userID"].ToString();
                string lastUpdOn = DateTime.Now.ToString("MM/dd/yyyy h:mm tt");
                Boolean insCnt = bDAO.BBInfoUpdate(Session["BillboardID"].ToString(), Addr1, Addr2, City, Country,latitude,Longtitude,postalCode,lastUpdBy, lastUpdOn);
                alertWarning.Visible = false;
                alertSuccess.Visible = true;
             
                BbAddLn1.Text = String.Empty;
                BbAddLn2.Text = String.Empty;
                BBCountry.SelectedValue = "";
                BbCity.Text = String.Empty;
                BbPostalCode.Text = String.Empty;
                BBLatitude.Text = String.Empty;
                BBLongtitude.Text = String.Empty;

            }
            Session["BBUpdate"] =2;
            Response.Redirect("BBLocationRead");
        }
        

    }
}