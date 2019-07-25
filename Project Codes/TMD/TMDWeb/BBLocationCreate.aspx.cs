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
    public partial class BBLocationCreate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

          
        }
        protected void SubmitBtn_Click(object sender, EventArgs e)
        {
            SqlConnection conn = null;
            SqlDataReader reader = null;



            // instantiate and open connection
            conn = new
                SqlConnection(@"Data Source=L33527\CHEEEFANGSQL;Initial Catalog=Targeted_Marketing_Display;Persist Security Info=True;User ID=root;Password=passw8rd");
            conn.Open();
            String BillboardCode = BBLocationCode.Text.ToString();
            String AddressLn1 = BBAddLn1.Text.ToString();
            String AddressLn2 = BBAddLn2.Text.ToString();
            String City = BBCity.Text.ToString();
            String Country = (BBCountry.SelectedValue.ToString());
            String PostalCode = BBPostalCode.Text.ToString();
            DateTime CreatedOn = DateTime.Now;
            int Status = 1;
            string latitude = BBLatitude.Text.ToString();
            int CreatedBy = 1;
            string Longtitude = BBLongtitude.Text.ToString();

            Billboard_Management bbMgmt = new Billboard_Management();
            //che ee was here
            // Boolean record = bbMgmt.BBcheck(BillboardCode);
            bool record = false;



            SqlCommand sCmd = new SqlCommand("SELECT count(*) as total FROM BillboardLocation WHERE BillboardCode ='@BCode' ", conn);
            SqlParameter param = new SqlParameter();
            param.ParameterName = "'@BCode'";
            param.Value = BillboardCode;
            sCmd.Parameters.AddWithValue("@BillboardCode", BillboardCode);
            SqlDataAdapter sda = new SqlDataAdapter();
            DataTable dt = new DataTable();
            sCmd.Connection = conn;
            sda.SelectCommand = sCmd;
            sda.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                int count = Convert.ToInt32(dt.Rows[i]["total"]);
                if (count == 0)
                {
                    record = false;
                }
                else
                {
                    record = true;
                }
            }




          

            if (BBLocationCode.Text != "" )
            {
                if (BBAddLn1.Text != "")
                {
                    if (BBCity.Text != "")
                    {
                        if (BBCountry.SelectedValue != "")
                        {
                            if (BBPostalCode.Text != "")
                            {
                                if (BBLatitude.Text != "")
                                {

                                    if (BBLongtitude.Text != "")
                                    {


                                        if (record == false)
                                        {
                                            testing123.Text = "Unique!";
                                            Boolean result = bbMgmt.BBinsert(BillboardCode, AddressLn1, AddressLn2, City, Country, PostalCode, CreatedOn, Status, latitude, Longtitude, CreatedBy);
                                            if (result == true)
                                            {
                                           
                                                alertWarning.Visible = false;
                                                alertSuccess.Visible = true;
                                                BBLocationCode.Text = String.Empty;
                                                BBAddLn1.Text = String.Empty;
                                                BBAddLn2.Text = String.Empty;
                                                BBCountry.SelectedValue = "";
                                                BBCity.Text = String.Empty;
                                                BBPostalCode.Text = String.Empty;

                                                Session["BBCreate"] = 2;
                                                Response.Redirect("BBLocationRead.aspx");
                                            }
                                        }

                                        if(record==true)
                                        {
                                            testing123.Text = " not Unique!";
                                            alertWarning.Visible = false;
                                            alertSuccess.Visible = false;
                                            alertDanger.Visible = true;
                                            dangerLocation.Text = "Billboard Code already exist";
                                        }
                                    }

                                    else
                                    {
                                        alertWarning.Visible = true;
                                        alertSuccess.Visible = false;
                                        alertDanger.Visible = false;
                                        warningLocation.Text = "Please enter Longtitude";
                                    }
                                }

                                else
                                {
                                    alertWarning.Visible = true;
                                    alertSuccess.Visible = false;
                                    alertDanger.Visible = false;
                                    warningLocation.Text = "Please enter Latitude";
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


