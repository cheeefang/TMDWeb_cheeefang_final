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
            if (!IsPostBack)
            {
                SqlConnection conn = null;
                SqlDataReader reader = null;
                Database db = new Database();
                string mainconn = ConfigurationManager.ConnectionStrings["Targeted_Marketing_DisplayConnectionString"].ConnectionString;
                conn = new
                SqlConnection(@"Data Source=L33527\CHEEEFANGSQL;Initial Catalog=Targeted_Marketing_Display;Persist Security Info=True;User ID=root;Password=passw8rd");
                conn.Open();
                //SqlConnection sqlconn = new SqlConnection(dbConnStr);
                //string sqlquery = "SELECT * FROM [CodeReferece] WHERE ([CodeType] = @CodeType)";
                //SqlCommand cmd = new SqlCommand(sqlquery, sqlconn);
                //cmd.Parameters.AddWithValue("@CodeType", "Category");
                //SqlDataAdapter sda = new SqlDataAdapter(cmd);
                //DataTable dt = new DataTable();
                //sda.Fill(dt);

                Advertisement AdvertObj = new Advertisement();
                Advertisement_Management aDao = new Advertisement_Management();
                AdvertObj = aDao.getAdvByID(Session["AdvertID"].ToString());
                DateTime dt = Convert.ToDateTime(AdvertObj.StartDate);
                String ConvertDate = dt.ToString("yyyy-MM-dd");
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
                SqlCommand cmdCat = new SqlCommand("select * from [AdvertisementLocation] where AdvID=@ID", conn);
                SqlParameter paramCat = new SqlParameter();
                paramCat.ParameterName = "@ID";
                paramCat.Value = Session["AdvertID"].ToString();
                cmdCat.Parameters.Add(paramCat);
                SqlDataAdapter sdaCat = new SqlDataAdapter();
                DataTable datatableCat = new DataTable();
                cmdCat.Connection = conn;
                sdaCat.SelectCommand = cmdCat;
                sdaCat.Fill(datatableCat);
                for (int i = 0; i < datatableCat.Rows.Count; i++)
                {
                    int BillboardCheckID = Convert.ToInt32(datatableCat.Rows[i]["BillboardID"]);
                    foreach (GridViewRow gvr in GridView1.Rows)

                    {

                        if (gvr.RowType == DataControlRowType.DataRow)
                        {
                            CheckBox cb = (CheckBox)(gvr.FindControl("CheckBoxSelector"));
                            if (Convert.ToInt32(gvr.Cells[1].Text) == BillboardCheckID)
                            {
                                cb.Checked = true;
                                billboardDisplayTB.Text = billboardDisplayTB.Text + "," + gvr.Cells[1].Text;
                            }
                        }
                    }
                    CompareValidator1.ValueToCompare = DateTime.Now.ToShortDateString();
                    CompareValidator2.ValueToCompare = DateTime.Now.ToShortDateString();


                }
            }
        }
        protected void ButtonConfirm_Click(object sender, EventArgs e)
        {
            Advertisement AdvertObj = new Advertisement();
            Advertisement_Management aDao = new Advertisement_Management();
            AdvertObj = aDao.getAdvByID(Session["AdvertID"].ToString());
            if (startDateTB.Text == "" || endDateTB.Text == "")
            {
                warningLocation.Visible = true;
               
            }
           
            else
            {
                string startdate = startDateTB.Text.ToString();
                string enddate = endDateTB.Text.ToString();
                string lastUpdBy = Session["userID"].ToString();
                string lastUpdOn = DateTime.Now.ToString("MM/dd/yyyy h:mm tt");
                aDao.AdvertUpdate(Session["AdvertID"].ToString(), startdate, enddate,lastUpdBy,lastUpdOn);
                startDateTB.Text = string.Empty;
                endDateTB.Text = string.Empty;
                
                alertWarning.Visible = false;
                alertSuccess.Visible = true;
            }
           
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
                        billboardDisplayTB.Text = billboardDisplayTB.Text + "," + gvr.Cells[1].Text;

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
