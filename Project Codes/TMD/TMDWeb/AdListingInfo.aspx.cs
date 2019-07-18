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
using System.Web.UI.HtmlControls;

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
                SqlCommand cmd0 = new SqlCommand("select c.Name from [Advertisement] as a inner join [Company] as c on a.companyID=c.CompanyID where a.AdvID=@ID");
                SqlParameter param0 = new SqlParameter();
                param0.ParameterName = "@ID";
                param0.Value = Session["AdvertID"].ToString();
                cmd0.Parameters.Add(param0);
                SqlDataAdapter sda0 = new SqlDataAdapter();
                DataTable dt0 = new DataTable();
                cmd0.Connection = conn;
                sda0.SelectCommand = cmd0;
                sda0.Fill(dt0);
                cmd0.Parameters.Clear();



                for (int i = 0; i < dt0.Rows.Count; i++)
                {
                    string companynameChecker = dt0.Rows[i]["Name"].ToString();
                    CompanyNameLabel.Text = "Company: " + companynameChecker;
                }


                Advertisement AdvertObj = new Advertisement();
                Advertisement_Management aDao = new Advertisement_Management();
                AdvertObj = aDao.getAdvByID(Session["AdvertID"].ToString());
                string previousimagepath = AdvertObj.Item.ToString();
                imgLogo.ImageUrl = ResolveUrl(previousimagepath);
               
                AdNameLabel.Text = "for " + AdvertObj.Name.ToString();
                AdName2.Text = "Advertisement Name: " + AdvertObj.Name.ToString();
                ItemTypeLabel.Text = "File Type: " + AdvertObj.ItemType.ToString();
                DateTime StartDateVar = Convert.ToDateTime(AdvertObj.StartDate);
                DateTime EndDateVar = Convert.ToDateTime(AdvertObj.EndDate);
                string niceStartDate = StartDateVar.ToString("dd MMM yyyy");
                string niceEndDate = EndDateVar.ToString("dd MMM yyyy");
                StartDateLabel.Text = StartDateLabel.Text + niceStartDate;
                EndDateLabel.Text = EndDateLabel.Text + niceEndDate;


                SqlCommand cmd1 = new SqlCommand("select a.AdvID,a.AgeID,a.GenderID,coderefage.CodeDesc as agedesc,coderefgender.CodeDesc as genderdesc from AdvertisementAudience  a full outer join CodeReferece as coderefage on coderefage.CodeValue = a.AgeID  full outer join CodeReferece as coderefgender on coderefgender.CodeValue = a.GenderID where a.advid = 5003 and coderefage.CodeType = 'AgeID' and coderefgender.CodeType = 'GenderID' ", conn);
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



                for(int i = 0; i < dt1.Rows.Count; i++)
                {
                    string ageDesc = dt1.Rows[i]["agedesc"].ToString();
                    string genderDesc = dt1.Rows[i]["genderdesc"].ToString();
                    HtmlGenericControl li = new HtmlGenericControl("li");
                    //tabs.Controls.Add(li);
                    AudienceList.Controls.Add(li);

                    HtmlGenericControl anchor = new HtmlGenericControl();
                    anchor.Attributes.Add("href", "page.htm");
                    anchor.InnerText = "" + ageDesc + " (" + genderDesc + ")";
                    li.Controls.Add(anchor);
                }
            

                SqlCommand cmdCat = new SqlCommand("SELECT a.[AdvID], a.[Name], a.[Item], b.CategoryID, c.CodeDesc FROM [Advertisement] a INNER JOIN [AdvertisementCategory] b ON a.AdvID = b.AdvID FULL OUTER JOIN [CodeReferece] c ON b.CategoryID = c.CodeValue WHERE a.AdvID = @ID", conn);
                SqlParameter paramCat = new SqlParameter();
                paramCat.ParameterName = "@ID";
                paramCat.Value = Session["AdvertID"].ToString();
                cmdCat.Parameters.Add(paramCat);
                SqlDataAdapter sdaCat = new SqlDataAdapter();
                DataTable dtCat = new DataTable();
                cmdCat.Connection = conn;
                sdaCat.SelectCommand = cmdCat;
                sdaCat.Fill(dtCat);
                cmdCat.Parameters.Clear();

                for (int i = 0; i < dtCat.Rows.Count; i++)
                {
                    string codedesc = dtCat.Rows[i]["CodeDesc"].ToString();
                    HtmlGenericControl li = new HtmlGenericControl("li");
                    //tabs.Controls.Add(li);
                    //AudienceList.Controls.Add(li);
                    CategoryList.Controls.Add(li);
                    HtmlGenericControl anchor = new HtmlGenericControl();
                    anchor.Attributes.Add("href", "page.htm");
                    anchor.InnerText = "" + codedesc;
                    li.Controls.Add(anchor);
                }

              
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
                    string BillboardCode = ((String)dt3.Rows[i]["BillboardCode"] + ",");
                    HtmlGenericControl li = new HtmlGenericControl("li");
                    //tabs.Controls.Add(li);
                    //AudienceList.Controls.Add(li);
                    BBCodeList.Controls.Add(li);
                    HtmlGenericControl anchor = new HtmlGenericControl();
                    anchor.Attributes.Add("href", "page.htm");
                    anchor.InnerText = "" + BillboardCode;
                    li.Controls.Add(anchor);
                }

            }
        }
    }
}