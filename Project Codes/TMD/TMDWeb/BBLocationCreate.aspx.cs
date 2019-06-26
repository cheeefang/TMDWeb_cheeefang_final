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

using System.Net;
using System.Xml.Linq;


namespace targeted_marketing_display
{
    public partial class BBLocationCreate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {


        }
        protected void SubmitBtn_Click(object sender, EventArgs e)
        {

            String BillboardCode = BbLocationCode.Text.ToString();
            String AddressLn1 = BbAddLn1.Text.ToString();
            String AddressLn2 = BbAddLn2.Text.ToString();
            String City = BbCity.Text.ToString();
            String Country = (BBCountry.SelectedValue.ToString());
            String PostalCode = BbPostalCode.Text.ToString();
            DateTime CreatedOn = DateTime.Now;
            int Status = 1;
            string latitude = "0";
            int CreatedBy = 1;
            string Longtitude = "0";

            Billboard_Management bbMgmt = new Billboard_Management();
            Boolean record = bbMgmt.BBcheck(BillboardCode, PostalCode);
            
            if (BbLocationCode.Text != "")
            {
                if (BbAddLn1.Text != "")
                {
                    if (BbCity.Text != "")
                    {
                        if (BBCountry.SelectedValue != "")
                        {
                            if (BbPostalCode.Text != "")
                            {
                                if (record == false)
                                {
                                    Boolean result = bbMgmt.BBinsert(BillboardCode, AddressLn1, AddressLn2, City, Country, PostalCode, CreatedOn, Status, latitude, Longtitude, CreatedBy);
                                    if (result == true)
                                    {
                                        alertWarning.Visible = false;
                                        alertSuccess.Visible = true;
                                        BbLocationCode.Text = String.Empty;
                                        BbAddLn1.Text = String.Empty;
                                        BbAddLn2.Text = String.Empty;
                                        BBCountry.SelectedValue = "";
                                        BbCity.Text = String.Empty;
                                        BbPostalCode.Text = String.Empty;
                                    }
                                }

                                else
                                {
                                    alertWarning.Visible = false;
                                    alertSuccess.Visible = false;
                                    alertDanger.Visible = true;
                                    dangerLocation.Text = "Location already exist";
                                }
                            }

                            else
                            {
                                alertWarning.Visible = true;
                                alertSuccess.Visible = false;
                                alertDanger.Visible = false;
                                warningLocation.Text = "Please enter postal code";
                            }
                        }

                        else
                        {
                            alertWarning.Visible = true;
                            alertSuccess.Visible = false;
                            alertDanger.Visible = false;
                            warningLocation.Text = "Please select a country";
                        }
                    }

                    else
                    {
                        alertWarning.Visible = true;
                        alertSuccess.Visible = false;
                        alertDanger.Visible = false;
                        warningLocation.Text = "Please enter a city";
                    }
                }

                else
                {
                    alertWarning.Visible = true;
                    alertSuccess.Visible = false;
                    alertDanger.Visible = false;
                    warningLocation.Text = "Please enter an Address";
                }
            }
            else
            {
                alertWarning.Visible = true;
                alertSuccess.Visible = false;
                alertDanger.Visible = false;
                warningLocation.Text = "Please enter a unique Billboard code";
            }


        }
    }
}