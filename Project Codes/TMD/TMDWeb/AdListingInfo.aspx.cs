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

                //SqlCommand cmd0 = new SqlCommand("select c.Name from [Advertisement] as a inner join [Company] as c on a.companyID=c.CompanyID where a.companyID=@ID");
                //SqlParameter param0 = new SqlParameter();
                //param0.ParameterName = "@ID";
                //param0.Value=
                Advertisement AdvertObj = new Advertisement();
                Advertisement_Management aDao = new Advertisement_Management();
                AdvertObj = aDao.getAdvByID(Session["AdvertID"].ToString());
                string previousimagepath = AdvertObj.Item.ToString();
                imgLogo.ImageUrl = ResolveUrl(previousimagepath);
               // CompanyNameLabel.Text="Company: "+AdvertObj.
                AdNameLabel.Text = "for " + AdvertObj.Name.ToString();
                AdName2.Text = "Advertisement Name: " + AdvertObj.Name.ToString();
                ItemTypeLabel.Text = "File Type: " + AdvertObj.ItemType.ToString();
                DateTime StartDateVar = Convert.ToDateTime(AdvertObj.StartDate);
                DateTime EndDateVar = Convert.ToDateTime(AdvertObj.EndDate);
                string niceStartDate = StartDateVar.ToString("dd MMM yyyy");
                string niceEndDate = EndDateVar.ToString("dd MMM yyyy");
                StartDateLabel.Text = StartDateLabel.Text + niceStartDate;
                EndDateLabel.Text = EndDateLabel.Text + niceEndDate;


                SqlCommand cmd1 = new SqlCommand("select * from [AdvertisementAudience] where AdvID=@ID ", conn);
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
                    int ageChecker = Convert.ToInt32(dt1.Rows[i]["AgeID"]);
                    string GenderChecker = dt1.Rows[i]["GenderID"].ToString();
                    if (ageChecker == 1)
                    {
                        if (GenderChecker == "M")
                        {
                            AudienceLabel.Text = AudienceLabel.Text + "Male Children(Age 0-15),";
                        }
                        else
                        {
                            AudienceLabel.Text = AudienceLabel.Text + "Female Children(Age 0-15),";
                        }
                    }
                    if (ageChecker == 2)
                    {
                        if (GenderChecker == "M")
                        {
                            AudienceLabel.Text = AudienceLabel.Text + "Male Young Adult(Age 16-30),";
                        }
                        else
                        {
                            AudienceLabel.Text = AudienceLabel.Text + "Female Young Adults(Age 16-30),";
                        }
                    }
                    if (ageChecker == 3)
                    {
                        if (GenderChecker == "M")
                        {
                            AudienceLabel.Text = AudienceLabel.Text + "Male Adults(Age 31-65),";
                        }
                        else
                        {
                            AudienceLabel.Text = AudienceLabel.Text + "Female Adults(Age 31-65),";
                        }
                    }
                    if (ageChecker == 4)
                    {
                        if (GenderChecker == "M")
                        {
                            AudienceLabel.Text = AudienceLabel.Text + "Male Seniors(Age 66+),";
                        }
                        else
                        {
                            AudienceLabel.Text = AudienceLabel.Text + "Female Seniors(Age 66+),";
                        }
                    }

                }
                SqlCommand cmd2 = new SqlCommand("Select * from [AdvertisementCategory] where AdvID=@ID", conn);
                SqlParameter param2 = new SqlParameter();
                param2.ParameterName = "@ID";
                param2.Value = Session["AdvertID"].ToString();
                cmd2.Parameters.Add(param2);
                SqlDataAdapter sda2 = new SqlDataAdapter();
                DataTable dt2 = new DataTable();
                cmd2.Connection = conn;
                sda2.SelectCommand = cmd2;
                sda2.Fill(dt2);
                cmd2.Parameters.Clear();
                for (int i = 0; i < dt2.Rows.Count; i++)
                {
                    //Auto,Bus,Career,Fin,Food,Gov,Health,Home,Ins,Int,Law,Mobile,Mother,Pets,Photo,Polit,
                    //Rec,Rest,Retail,Shop,Sport,Style,Tech,Tel,Travel,Wed,Women
                    //int ageChecker = Convert.ToInt32(datatable.Rows[i]["AgeID"]);
                    string catChecker = ((String)dt2.Rows[i]["CategoryID"]);

                    if (catChecker == " Auto")
                    {
                        CategoryLabel.Text = CategoryLabel.Text + "Automotive,";
                    }
                    if (catChecker == " Bus")
                    {
                        CategoryLabel.Text = CategoryLabel.Text + "Business,";
                    }
                    if (catChecker == " Career")
                    {
                        CategoryLabel.Text = CategoryLabel.Text + "Career,";
                    }
                    if (catChecker == " Fin")
                    {
                        CategoryLabel.Text = CategoryLabel.Text + "Financial,";
                    }
                    if (catChecker == " Food")
                    {
                        CategoryLabel.Text = CategoryLabel.Text + "Food,";
                    }
                    if (catChecker == " Gov")
                    {
                        CategoryLabel.Text = CategoryLabel.Text + "Government,";
                    }
                    if (catChecker == " Health")
                    {
                        CategoryLabel.Text = CategoryLabel.Text + "Health,";
                    }
                    if (catChecker == " Home")
                    {
                        CategoryLabel.Text = CategoryLabel.Text + "Home Garden,";
                    }
                    if (catChecker == " Ins")
                    {
                        CategoryLabel.Text = CategoryLabel.Text + "Insurance,";
                    }
                    if (catChecker == " Int")
                    {
                        CategoryLabel.Text = CategoryLabel.Text + "Internet,";
                    }

                    if (catChecker == " Law")
                    {
                        CategoryLabel.Text = CategoryLabel.Text + "Legal,";
                    }
                    if (catChecker == " Mobile")
                    {
                        CategoryLabel.Text = CategoryLabel.Text + "Mobile & wireless,";
                    }
                    if (catChecker == " Mother")
                    {
                        CategoryLabel.Text = CategoryLabel.Text + "Mothers,";
                    }
                    if (catChecker == " Pets")
                    {
                        CategoryLabel.Text = CategoryLabel.Text + "Pets,";
                    }
                    if (catChecker == " Photo")
                    {
                        CategoryLabel.Text = CategoryLabel.Text + "Photography,";
                    }
                    if (catChecker == " Polit")
                    {
                        CategoryLabel.Text = CategoryLabel.Text + "Political,";
                    }
                    if (catChecker == " Rec")
                    {
                        CategoryLabel.Text = CategoryLabel.Text + "Recreation,";
                    }
                    if (catChecker == " Rest")
                    {
                        CategoryLabel.Text = CategoryLabel.Text + "Restaurant,";
                    }
                    if (catChecker == " Retail")
                    {
                        CategoryLabel.Text = CategoryLabel.Text + "Retail,";
                    }
                    if (catChecker == " Shop")
                    {
                        CategoryLabel.Text = CategoryLabel.Text + "Shopping,";
                    }
                    if (catChecker == " Sport")
                    {
                        CategoryLabel.Text = CategoryLabel.Text + "Sports,";
                    }
                    if (catChecker == " Style")
                    {
                        CategoryLabel.Text = CategoryLabel.Text + "Lifestyle,";
                    }
                    if (catChecker == " Tech")
                    {
                        CategoryLabel.Text = CategoryLabel.Text + "Technology,";
                    }
                    if (catChecker == " Tel")
                    {
                        CategoryLabel.Text = CategoryLabel.Text + "Telecom,";
                    }
                    if (catChecker == " Travel")
                    {
                        CategoryLabel.Text = CategoryLabel.Text + "Travel & tourism,";
                    }
                    if (catChecker == " Wed")
                    {
                        CategoryLabel.Text = CategoryLabel.Text + "Wedding,";
                    }
                    if (catChecker == " Women")
                    {
                        CategoryLabel.Text = CategoryLabel.Text + "Women,";
                    }


                }
                //SqlCommand cmd2 = new SqlCommand("Select * from [AdvertisementCategory] where AdvID=@ID", conn);
                //SqlParameter param2 = new SqlParameter();
                //param2.ParameterName = "@ID";
                //param2.Value = Session["AdvertID"].ToString();
                //cmd2.Parameters.Add(param2);
                //SqlDataAdapter sda2 = new SqlDataAdapter();
                //DataTable dt2 = new DataTable();
                //cmd2.Connection = conn;
                //sda2.SelectCommand = cmd2;
                //sda2.Fill(dt2);
                //cmd2.Parameters.Clear();
                //for (int i = 0; i < dt2.Rows.Count; i++)
                //{

                //}
                SqlCommand cmd3 = new SqlCommand("select b.BillboardCode from [AdvertisementLocation] as a inner join [BillboardLocation] as b on a.BillboardID=b.BillboardID where AdvID=@ID", conn);
                SqlParameter param3 = new SqlParameter();
                param3.ParameterName = "@ID";
                param3.Value = Session["AdvertID"].ToString();
                cmd3.Parameters.Add(param3);
                SqlDataAdapter sda3 = new SqlDataAdapter();
                DataTable dt3 = new DataTable();
                cmd3.Connection = conn;
                sda3.SelectCommand = cmd3;
                sda3.Fill(dt3);
                cmd3.Parameters.Clear();
                for (int i = 0; i < dt3.Rows.Count; i++)
                {
                    string BillboardCodeChecker = ((String)dt3.Rows[i]["BillboardCode"] + ",");
                    LocationLabel.Text = LocationLabel.Text + BillboardCodeChecker;
                }
            }
        }
    }
}