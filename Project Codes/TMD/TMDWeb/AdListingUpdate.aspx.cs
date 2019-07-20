﻿using System;
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
using System.Globalization;

namespace targeted_marketing_display
{
    public partial class AdListingUpdate : System.Web.UI.Page
    {
        string dbConnStr = ConfigurationManager.ConnectionStrings["Targeted_Marketing_DisplayConnectionString"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            Advertisement adObject = new Advertisement();
            Advertisement_Management adObjectClass = new Advertisement_Management();
            adObject = adObjectClass.getAdvByID(Session["AdvertID"].ToString());
            string previousimagepath = adObject.Item.ToString();
            imgLogo.ImageUrl = ResolveUrl(previousimagepath) ;   
            SqlConnection conn = null;
            SqlDataReader reader = null;
            conn = new
            SqlConnection(@"Data Source=L33527\CHEEEFANGSQL;Initial Catalog=Targeted_Marketing_Display;Persist Security Info=True;User ID=root;Password=passw8rd");
            conn.Open();
            
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
            if (FileUpload1.HasFile)
            {
                Literal1.Text = Convert.ToString(FileUpload1.PostedFile.FileName);
                if (Literal1.Text.EndsWith(".png") || Literal1.Text.EndsWith(".jpg") || Literal1.Text.EndsWith(".jpeg") || Literal1.Text.EndsWith(".gif") || Literal1.Text.EndsWith(".PNG") || Literal1.Text.EndsWith(".JPG") || Literal1.Text.EndsWith(".JPEG") || Literal1.Text.EndsWith(".GIF"))
                {
                    Literal2.Text = "image";
                }
                else
                {
                    Literal2.Text = "video";




                }



            }
            if (!Page.IsPostBack)
            {
            
                Database db = new Database();
                string mainconn = ConfigurationManager.ConnectionStrings["Targeted_Marketing_DisplayConnectionString"].ConnectionString;
                SqlConnection sqlconn = new SqlConnection(dbConnStr);
                string sqlquery = "SELECT * FROM [CodeReferece] WHERE ([CodeType] = @CodeType)";
                SqlCommand cmdCodeRef = new SqlCommand(sqlquery, sqlconn);
                cmdCodeRef.Parameters.AddWithValue("@CodeType", "Category");
                SqlDataAdapter sdaCodeRef = new SqlDataAdapter(cmdCodeRef);
                DataTable dt = new DataTable();
                sdaCodeRef.Fill(dt);

                CheckBoxList1.DataSource = dt;
                CheckBoxList1.DataBind();
                SqlCommand cmdCompany = new SqlCommand("Select * from Company where status=1");
                DataTable dtCompany = db.getDataTable(cmdCompany);
                DropDownListCompany.DataSource = dtCompany;
                DropDownListCompany.DataValueField = "CompanyID";
                DropDownListCompany.DataTextField = "Name";
                DropDownListCompany.DataBind();
                DropDownListCompany.Items.Insert(0, new ListItem("---Select A Company---", "0"));
     
                Advertisement AdvertObj = new Advertisement();
                Advertisement_Management aDao = new Advertisement_Management();
                AdvertObj = aDao.getAdvByID(Session["AdvertID"].ToString());
                DateTime startDatevar = Convert.ToDateTime(AdvertObj.StartDate);
                String ConvertDate = startDatevar.ToString("yyyy-MM-dd");
                DateTime dt2 = Convert.ToDateTime(AdvertObj.EndDate);
                String ConvertEndDate = dt2.ToString("yyyy-MM-dd");
                AdvIDLabel.Text = " for " + ' ' + AdvertObj.Name.ToString();

                startDateTB.Text = ConvertDate;
                endDateTB.Text = ConvertEndDate;
                DropDownListCompany.SelectedValue = AdvertObj.CompanyID.ToString();
                adNameTB.Text = AdvertObj.Name.ToString();
                videoDurationTB.Text = AdvertObj.Duration.ToString();

                SqlCommand cmd = new SqlCommand("select * from [AdvertisementAudience] where AdvID=@ID", conn);
                SqlParameter param = new SqlParameter();
                param.ParameterName = "@ID";
                param.Value = Session["AdvertID"].ToString();
                cmd.Parameters.Add(param);

                SqlDataAdapter sda = new SqlDataAdapter();

                DataTable datatable = new DataTable();
                cmd.Connection = conn;
                sda.SelectCommand = cmd;
                sda.Fill(datatable);
                cmd.Parameters.Clear();
                //  string userName = dtLoginTable.Rows[0]["UserName"].ToString();
                string AgeID = datatable.Rows[0]["AgeID"].ToString();
                for (int i = 0; i < datatable.Rows.Count; i++)
                {
                    int ageChecker = Convert.ToInt32(datatable.Rows[i]["AgeID"]);
                    string GenderChecker = datatable.Rows[i]["GenderID"].ToString();
                    if (ageChecker == 1)
                    {
                        if (GenderChecker == "M")
                        {
                            CheckBoxList2.Items[0].Selected = true;
                        }
                        else
                        {
                            CheckBoxList2.Items[4].Selected = true;
                        }
                    }
                    if (ageChecker == 2)
                    {
                        if (GenderChecker == "M")
                        {
                            CheckBoxList2.Items[1].Selected = true;
                        }
                        else
                        {
                            CheckBoxList2.Items[5].Selected = true;
                        }
                    }
                    if (ageChecker == 3)
                    {
                        if (GenderChecker == "M")
                        {
                            CheckBoxList2.Items[2].Selected = true;
                        }
                        else
                        {
                            CheckBoxList2.Items[6].Selected = true;
                        }
                    }
                    if (ageChecker == 4)
                    {
                        if (GenderChecker == "M")
                        {
                            CheckBoxList2.Items[3].Selected = true;
                        }
                        else
                        {
                            CheckBoxList2.Items[7].Selected = true;
                        }
                    }

                }
                CompareValidator2.ValueToCompare = DateTime.Now.ToShortDateString();
                SqlCommand cmdLoc = new SqlCommand("select a.AdvID,a.BillboardID,b.BillboardCode from AdvertisementLocation a inner join BillboardLocation b on a.BillboardID = b.BillboardID where a.AdvID = @ID", conn);
                SqlParameter paramLoc = new SqlParameter();
                paramLoc.ParameterName = "@ID";
                paramLoc.Value = Session["AdvertID"].ToString();
                cmdLoc.Parameters.Add(paramLoc);
                SqlDataAdapter sdaLoc = new SqlDataAdapter();
                DataTable datatableLoc = new DataTable();
                cmdLoc.Connection = conn;
                sdaLoc.SelectCommand = cmdLoc;
                sdaLoc.Fill(datatableLoc);
                cmdLoc.Parameters.Clear();
                for (int i = 0; i < datatableLoc.Rows.Count; i++)
                {
                    string BillboardCodefromdb = datatableLoc.Rows[i]["BillboardCode"].ToString();
                    foreach (GridViewRow gvr in GridView1.Rows)

                    {

                        if (gvr.RowType == DataControlRowType.DataRow)
                        {
                            CheckBox cb = (CheckBox)(gvr.FindControl("CheckBoxSelector"));
                            if (gvr.Cells[2].Text.ToString() == BillboardCodefromdb)
                            {
                                cb.Checked = true;
                                billboardDisplayTB.Text = billboardDisplayTB.Text + "," + gvr.Cells[2].Text;
                               
                            }
                        }
                    }
                    
                    // CompareValidator1.ValueToCompare = DateTime.Now.ToShortDateString();
                    


                }
                billboardDisplayTB.Text = (billboardDisplayTB.Text).Substring(1);
                SqlCommand cmdCat = new SqlCommand("select * from [AdvertisementCategory] where AdvID=@ID", conn);
                SqlParameter paramCat = new SqlParameter();
                paramCat.ParameterName = "@ID";
                paramCat.Value = Session["AdvertID"].ToString();
                cmdCat.Parameters.Add(paramCat);
                SqlDataAdapter sdaCat = new SqlDataAdapter();
                DataTable datatableCat = new DataTable();
                cmdCat.Connection = conn;
                sdaCat.SelectCommand = cmdCat;
                sdaCat.Fill(datatableCat);
                string name = "";
                for (int i = 0; i < datatableCat.Rows.Count; i++)
                {
                    //Auto,Bus,Career,Fin,Food,Gov,Health,Home,Ins,Int,Law,Mobile,Mother,Pets,Photo,Polit,
                    //Rec,Rest,Retail,Shop,Sport,Style,Tech,Tel,Travel,Wed,Women
                    //int ageChecker = Convert.ToInt32(datatable.Rows[i]["AgeID"]);
                    string catChecker = ((String)datatableCat.Rows[i]["CategoryID"]);
                    
                        if (catChecker == "Auto")
                        {
                            CheckBoxList1.Items[0].Selected = true;
                            //CheckBoxList1.Items[0].Selected = true;
                            name = name + ", " + CheckBoxList1.Items[0].Value;
                            //adCategoryTB.Text = (name).Substring(1);
                        }
                        if (catChecker == "Bus")
                        {
                            CheckBoxList1.Items[1].Selected = true;
                            name = name + ", " + CheckBoxList1.Items[1].Value;
                           // adCategoryTB.Text = (name).Substring(1);
                        }
                        if (catChecker == "Career")
                        {
                            CheckBoxList1.Items[2].Selected = true;
                            name = name + ", " + CheckBoxList1.Items[2].Value;
                           // adCategoryTB.Text = (name).Substring(1);
                        }
                        if (catChecker == "Fin")
                        {
                            CheckBoxList1.Items[3].Selected = true;
                            name = name + ", " + CheckBoxList1.Items[3].Value;
                            //adCategoryTB.Text = (name).Substring(1);
                        }
                        if (catChecker == "Food")
                        {
                           
                            CheckBoxList1.Items[4].Selected = true;
                            name = name + ", " + CheckBoxList1.Items[4].Value;
                           // adCategoryTB.Text = (name).Substring(1);
                        }
                        if (catChecker == "Gov")
                        {
                            CheckBoxList1.Items[5].Selected = true;
                            name = name + ", " + CheckBoxList1.Items[5].Value;
                           // adCategoryTB.Text = (name).Substring(1);
                        }
                        if (catChecker == "Health")
                        {
                            CheckBoxList1.Items[6].Selected = true;
                            name = name + ", " + CheckBoxList1.Items[6].Value;
                           // adCategoryTB.Text = (name).Substring(1);
                        }
                        if (catChecker == "Home")
                        {
                            CheckBoxList1.Items[7].Selected = true;
                            name = name + ", " + CheckBoxList1.Items[7].Value;
                           // adCategoryTB.Text = (name).Substring(1);
                        }
                        if (catChecker == "Ins")
                        {
                            CheckBoxList1.Items[8].Selected = true;
                            name = name + ", " + CheckBoxList1.Items[8].Value;
                          //  adCategoryTB.Text = (name).Substring(1);
                        }
                        if (catChecker=="Int")
                        {
                            CheckBoxList1.Items[9].Selected = true;
                            name = name + ", " + CheckBoxList1.Items[9].Value;
                        }
                        if (catChecker == "Law")
                        {
                            CheckBoxList1.Items[10].Selected = true;
                            name = name + ", " + CheckBoxList1.Items[10].Value;
                          //  adCategoryTB.Text = (name).Substring(1);
                        }
                        if (catChecker == "Mobile")
                        {
                            CheckBoxList1.Items[11].Selected = true;
                            name = name + ", " + CheckBoxList1.Items[11].Value;
                           // adCategoryTB.Text = (name).Substring(1);
                        }
                        if (catChecker == "Mother")
                        {
                            CheckBoxList1.Items[12].Selected = true;
                            name = name + ", " + CheckBoxList1.Items[12].Value;
                           // adCategoryTB.Text = (name).Substring(1);
                        }
                        if (catChecker == "Pets")
                        {
                            CheckBoxList1.Items[13].Selected = true;
                            name = name + ", " + CheckBoxList1.Items[13].Value;
                          //  adCategoryTB.Text = (name).Substring(1);
                        }
                        if (catChecker == "Photo")
                        {
                            CheckBoxList1.Items[14].Selected = true;
                            name = name + ", " + CheckBoxList1.Items[14].Value;
                          //  adCategoryTB.Text = (name).Substring(1);
                        }
                        if (catChecker == "Polit")
                        {
                            CheckBoxList1.Items[15].Selected = true;
                            name = name + ", " + CheckBoxList1.Items[15].Value;
                           // adCategoryTB.Text = (name).Substring(1);
                        }
                        if (catChecker == "Rec")
                        {
                            CheckBoxList1.Items[16].Selected = true;
                            name = name + ", " + CheckBoxList1.Items[16].Value;
                          //  adCategoryTB.Text = (name).Substring(1);
                        }
                        if (catChecker == "Rest")
                        {
                            CheckBoxList1.Items[17].Selected = true;
                            name = name + ", " + CheckBoxList1.Items[17].Value;
                         //   adCategoryTB.Text = (name).Substring(1);
                        }
                        if (catChecker == "Retail")
                        {
                            CheckBoxList1.Items[18].Selected = true;
                            name = name + ", " + CheckBoxList1.Items[18].Value;
                            //adCategoryTB.Text = (name).Substring(1);
                        }
                        if (catChecker == "Shop")
                        {
                            CheckBoxList1.Items[19].Selected = true;
                            name = name + ", " + CheckBoxList1.Items[19].Value;
                           // adCategoryTB.Text = (name).Substring(1);
                        }
                        if (catChecker == "Sport")
                        {
                            CheckBoxList1.Items[20].Selected = true;
                            name = name + ", " + CheckBoxList1.Items[20].Value;
                            //adCategoryTB.Text = (name).Substring(1);
                        }
                        if (catChecker == "Style")
                        {
                            CheckBoxList1.Items[21].Selected = true;
                            name = name + ", " + CheckBoxList1.Items[21].Value;
                           // adCategoryTB.Text = (name).Substring(1);
                        }
                        if (catChecker == "Tech")
                        {
                            CheckBoxList1.Items[22].Selected = true;
                            name = name + ", " + CheckBoxList1.Items[22].Value;
                          //  adCategoryTB.Text = (name).Substring(1);
                        }
                        if (catChecker == "Tel")
                        {
                            CheckBoxList1.Items[23].Selected = true;
                            name = name + ", " + CheckBoxList1.Items[23].Value;
                          //  adCategoryTB.Text = (name).Substring(1);
                        }
                        if (catChecker == "Travel")
                        {
                            CheckBoxList1.Items[24].Selected = true;
                            name = name + ", " + CheckBoxList1.Items[24].Value;
                           // adCategoryTB.Text = (name).Substring(1);
                        }
                        if (catChecker == "Wed")
                        {
                            CheckBoxList1.Items[25].Selected = true;
                            name = name + ", " + CheckBoxList1.Items[25].Value;
                          //  adCategoryTB.Text = (name).Substring(1);
                        }
                        if (catChecker == "Women")
                        {
                            CheckBoxList1.Items[26].Selected = true;
                            name = name + ", " + CheckBoxList1.Items[26].Value;
                           // adCategoryTB.Text = (name).Substring(1);
                        }
                        
                    // adCategoryTB.Text = (name).Substring(1);
                }
                adCategoryTB.Text = name.Substring(1);        
            }
        }
        protected void ButtonConfirm_Click(object sender, EventArgs e)
        {
            Advertisement AdvertObj = new Advertisement();
            Advertisement_Management aDao = new Advertisement_Management();
            AdvertObj = aDao.getAdvByID(Session["AdvertID"].ToString());
            //initialise imagelink and getvalue
            string imagelink = "";
            string getvalue = "";

            for (int i = 0; i < CheckBoxList2.Items.Count; i++)
            {
                if (CheckBoxList2.Items[i].Selected)
                {
                    getvalue += CheckBoxList2.Items[i].Text + ",";
                    getvalue = getvalue.TrimEnd();
                }
            }

            //if uploaded file then save
            if (FileUpload1.HasFile)
            {
                string fileExt = System.IO.Path.GetExtension(FileUpload1.FileName);
                FileUpload1.SaveAs(Server.MapPath("~/Images/") + FileUpload1.FileName);

            }

            //if any field missing give warning!
            if (Literal1.Text == "" || startDateTB.Text == ""
                   || endDateTB.Text == "" || adCategoryTB.Text == "" || billboardDisplayTB.Text == "" || getvalue == "")
            {
                alertWarning.Visible = true;

                warningLocation.Text = "Please ensure you have filled in all required fields";
            }
            //if never agree to terms and conditions,display warning
            else if (CheckBox1.Checked == false)
            {
                alertWarning.Visible = true;

                warningLocation.Text = "Please agree with T&C";
            }

            if (startDateTB.Text == "" || endDateTB.Text == "")
            {
                warningLocation.Visible = true;
               
            }
           
            else
            {

                imagelink = "Images/" + Literal1.Text;
                string NewAdvertName = adNameTB.Text.ToString();
                int NewCompanyID = Convert.ToInt32(DropDownListCompany.SelectedValue);
                int NewDuration = Convert.ToInt32(videoDurationTB.Text);

                string startdate = startDateTB.Text.ToString();
                string enddate = endDateTB.Text.ToString();
                
                string lastUpdBy = Session["userID"].ToString();
                string lastUpdOn = DateTime.Now.ToString("MM/dd/yyyy h:mm tt");
                SqlConnection sqlcn = new SqlConnection(dbConnStr);
                
                aDao.AdvertUpdate(Session["AdvertID"].ToString(),imagelink,NewCompanyID,NewAdvertName,NewDuration, startdate, enddate,lastUpdBy,lastUpdOn);
                //SqlCommand cmd = new SqlCommand("update [AdvertisementAudience] set AgeID=@newAgeID,GenderID=@newGenderID where AdvID=@paraAdvID " +
                // "if @@rowcount=0 insert into [AdvertisementLocation] (AdvID,AgeID,GenderID) values (@newAdvID,@newAgeID,@newGenderID)", sqlcn);
                // sqlcn.Open();
                aDao.AdvertAudienceDeleteExisting(Session["AdvertID"].ToString());
                aDao.AdvertCategoryDeleteExisting(Session["AdvertID"].ToString());
                aDao.AdvertLocationDeleteExisting(Session["AdvertID"].ToString());
                SqlConnection sqlcon = new SqlConnection(dbConnStr);
                string sqlquery = "Insert into [AdvertisementCategory](AdvID,CategoryID) values(@AdvID,@CategoryID)";
                SqlCommand sqlcom = new SqlCommand(sqlquery, sqlcon);
                sqlcon.Open();
                string str = adCategoryTB.Text;
                string[] splitstr = str.Split(',');
                //int id = GetMaxIDAdvertisement();


                foreach (string s in splitstr)
                {
                    //trim the string, i.e. remove the space if any
                    string _s = s;
                    _s = _s.Trim();
                    sqlcom.Parameters.AddWithValue("@AdvID", Session["AdvertID"]);
                    sqlcom.Parameters.AddWithValue("@CategoryID", _s);
                    sqlcom.ExecuteNonQuery();
                    sqlcom.Parameters.Clear();
                }


                sqlcon.Close();


                SqlConnection sqlconnn = new SqlConnection(dbConnStr);
                string sqlqueryy = "Insert into [AdvertisementLocation](AdvID,BillboardID) values(@AdvID,@BillboardID)";
                SqlCommand sqlcommm = new SqlCommand(sqlqueryy, sqlconnn);
                sqlconnn.Open();

                //int AdvId = GetMaxIDAdvertisement();

                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    GridViewRow row = GridView1.Rows[i];
                    bool chkbx = ((CheckBox)row.FindControl("CheckBoxSelector")).Checked;
                    if (chkbx)
                    {
                        Label bblabel = (Label)GridView1.Rows[i].FindControl("lb_BillboardID");
                        sqlcommm.Parameters.AddWithValue("@BillboardID", Convert.ToInt32(bblabel.Text));
                        sqlcommm.Parameters.AddWithValue("@AdvID", Session["AdvertID"]);
                        sqlcommm.ExecuteNonQuery();
                        sqlcommm.Parameters.Clear();
                    }
                }
                sqlconnn.Close();




                SqlConnection sqlcnAudience = new SqlConnection(dbConnStr);
                string sqlque = "Insert into [AdvertisementAudience](AdvID,AgeID,GenderID) values(@AdvID,@AgeID,@GenderID)";
                SqlCommand sqlcm = new SqlCommand(sqlque, sqlcnAudience);
                sqlcnAudience.Open();

                //int ID_audience = GetMaxIDAdvertisement();


                for (int i = 0; i < CheckBoxList2.Items.Count; i++)
                {
                    if (CheckBoxList2.Items[i].Selected == true)
                    {

                        string stri = string.Empty;
                        stri = CheckBoxList2.Items[i].ToString();

                        if (stri.Contains("Male") & stri.Contains("Child"))
                        {
                            sqlcm.Parameters.AddWithValue("@GenderID", "M");
                            sqlcm.Parameters.AddWithValue("@AgeID", "1");
                        }
                        else if (stri.Contains("Male") & stri.Contains("Young Adult"))
                        {
                            sqlcm.Parameters.AddWithValue("@GenderID", "M");
                            sqlcm.Parameters.AddWithValue("@AgeID", "2");
                        }
                        else if (stri.Contains("Male") & stri.Contains("Age 31-65"))
                        {
                            sqlcm.Parameters.AddWithValue("@GenderID", "M");
                            sqlcm.Parameters.AddWithValue("@AgeID", "3");
                        }
                        else if (stri.Contains("Male") & stri.Contains("Senior"))
                        {
                            sqlcm.Parameters.AddWithValue("@GenderID", "M");
                            sqlcm.Parameters.AddWithValue("@AgeID", "4");
                        }
                        else if (stri.Contains("Female") & stri.Contains("Child"))
                        {
                            sqlcm.Parameters.AddWithValue("@GenderID", "F");
                            sqlcm.Parameters.AddWithValue("@AgeID", "1");
                        }
                        else if (stri.Contains("Female") & stri.Contains("Young Adult"))
                        {
                            sqlcm.Parameters.AddWithValue("@GenderID", "F");
                            sqlcm.Parameters.AddWithValue("@AgeID", "2");
                        }
                        else if (stri.Contains("Female") & stri.Contains("Age 31-65"))
                        {
                            sqlcm.Parameters.AddWithValue("@GenderID", "F");
                            sqlcm.Parameters.AddWithValue("@AgeID", "3");
                        }
                        else if (stri.Contains("Female") & stri.Contains("Senior"))
                        {
                            sqlcm.Parameters.AddWithValue("@GenderID", "F");
                            sqlcm.Parameters.AddWithValue("@AgeID", "4");
                        }


                        sqlcm.Parameters.AddWithValue("@AdvID", Session["AdvertID"]);
                        sqlcm.ExecuteNonQuery();
                        sqlcm.Parameters.Clear();

                    }



                }

                sqlcnAudience.Close();
                adNameTB.Text = string.Empty;
                DropDownListCompany.SelectedIndex=0;
                startDateTB.Text = string.Empty;
                endDateTB.Text = string.Empty;
                videoDurationTB.Text = string.Empty;
                adCategoryTB.Text = string.Empty;
                billboardDisplayTB.Text = string.Empty;
                for (int i = 0; i < CheckBoxList1.Items.Count; i++)
                {
                    CheckBoxList1.Items[i].Selected = false;
                }
                for(int i = 0; i > CheckBoxList2.Items.Count; i++)
                {
                    CheckBoxList2.Items[i].Selected = false;
                }
                alertWarning.Visible = false;
                alertSuccess.Visible = true;

            }

            //ScriptManager.RegisterStartupScript(this, this.GetType(),
            //"alert",
            //"alert('Advertisement Successfully Updated');window.location ='AdListing.aspx';",
            // true);
            //Response.Write("<script language='javascript'>window.alert('Advertisement Successfully Updated');window.location='AdListingUpdate.aspx';</script>");
            //string adNamenew = AdvertObj.Name;
            //Response.Write("<script language='javascript'>alert('Successfully Updated Advertisement');</script>");
            //Server.Transfer("AdListing.aspx", true);
            Session["AdvertUpdate"] = 2;
            Response.Redirect("AdListing.aspx");
            // Response.Redirect(Request.RawUrl);
           // alertSuccess.Visible = true;
           // Response.Redirect("AdListing.aspx?showSuccessMessage = 1");

       
        }

      

        protected void CategoryButton_Click(object sender, EventArgs e)
        {
            string name = "";
            for (int i = 0; i < CheckBoxList1.Items.Count; i++)
            {

                if (CheckBoxList1.Items[i].Selected)
                {

                    name = name + ", " + CheckBoxList1.Items[i].Value;

                }


            }

            adCategoryTB.Text = (name).Substring(1);
        }
        protected void BillboardSearch_Click(object sender, EventArgs e)
        {
            List<int> lstBillboardID = new List<int>();

            billboardDisplayTB.Text = "";
            foreach (GridViewRow gvr in GridView1.Rows)

            {
                if (gvr.RowType == DataControlRowType.DataRow)
                {
                    CheckBox cb = (CheckBox)(gvr.FindControl("CheckBoxSelector"));
                    if (cb.Checked == true)
                    {
                        billboardDisplayTB.Text = billboardDisplayTB.Text + "," + gvr.Cells[2].Text;

                    }
                }
            }

            billboardDisplayTB.Text = (billboardDisplayTB.Text).Substring(1);
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "  hideModal();", true);

        }

        protected void btnRun_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
        }
    }
}
