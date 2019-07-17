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
    public partial class AdListingInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SqlConnection conn = null;
            SqlDataReader reader = null;
            conn = new
            SqlConnection(@"Data Source=L33527\CHEEEFANGSQL;Initial Catalog=Targeted_Marketing_Display;Persist Security Info=True;User ID=root;Password=passw8rd");
            conn.Open();
            if (!IsPostBack)
            {
                Advertisement AdvertObj = new Advertisement();
                Advertisement_Management aDao = new Advertisement_Management();
                
                AdvertObj = aDao.getAdvByID(Session["AdvertID"].ToString());
                string previousimagepath = AdvertObj.Item.ToString();
                imgLogo.ImageUrl = ResolveUrl(previousimagepath);
                AdNameLabel.Text = "for " + AdvertObj.Name.ToString();
                AdName2.Text = "Advertisement Name: " + AdvertObj.Name.ToString();
                ItemTypeLabel.Text = "File Type: " + AdvertObj.ItemType.ToString();
                string niceStartDate = AdvertObj.StartDate.ToString("dd MMM  yyyy ");
                string niceEndDate = AdvertObj.EndDate.ToString("dd MMM yyyy");
                StartDateLabel.Text = niceStartDate;
                EndDateLabel.Text = niceEndDate;
                SqlCommand cmd1 = new SqlCommand("select * from [AdvertisementAudience] where AdvID=@ID ",conn);
                SqlParameter param1 = new SqlParameter();
                param1.ParameterName = "@ID";
                param1.Value = Session["AdvertID"].ToString();
                cmd1.Parameters.Add(param1);
                SqlDataAdapter sda1 = new SqlDataAdapter();
                DataTable dt1 = new DataTable();
                cmd1.Connection = conn;
                sda1.SelectCommand = cmd1;
                sda1.Fill(dt1);
                cmd1.Parameters.Clear();
                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    int ageChecker= Convert.ToInt32(dt1.Rows[i]["AgeID"]);
                    string GenderChecker = dt1.Rows[i]["GenderID"].ToString();
                    if (ageChecker == 1)
                    {
                        if (GenderChecker == "M")
                        {
                            AudienceLabel.Text = AudienceLabel.Text + "Age 0-15,Male Child";
                        }
                        else
                        {
                           
                        }
                    }
                    if (ageChecker == 2)
                    {
                        if (GenderChecker == "M")
                        {
                         
                        }
                        else
                        {
                          
                        }
                    }
                    if (ageChecker == 3)
                    {
                        if (GenderChecker == "M")
                        {
                           
                        }
                        else
                        {
                          
                        }
                    }
                    if (ageChecker == 4)
                    {
                        if (GenderChecker == "M")
                        {
                           
                        }
                        else
                        {
                            
                        }
                    }

                }

            }
        }
    }
}