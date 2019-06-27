using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;


namespace targeted_marketing_display
{
    public partial class testRead : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            

            DataTable dt = new DataTable();
        
            dt.Columns.Add(new DataColumn("Company Name"));
            dt.Columns.Add(new DataColumn("Industry"));
            dt.Columns.Add(new DataColumn("Status"));
         

           DataRow dr = dt.NewRow();
            dr["Company Name"] = "Samsung";
            dr["Industry"] = "Retail";
            dr["Status"] = "Active";
        
            dt.Rows.Add(dr);

            DataRow dr1 = dt.NewRow();
            dr["Company Name"] = "Mediacorp";
            dr["Industry"] = "Entertainment";
            dr["Status"] = "Inactive";
            dt.Rows.Add(dr1);

            DataRow dr2 = dt.NewRow();
            dr["Company Name"] = "Samsung";
            dr["Industry"] = "Retail";
            dr["Status"] = "Active";
            dt.Rows.Add(dr2);

            //GridView1.DataSource = dt;
            GridView1.DataBind();

        }
        protected void delBtn_Click(object sender,  EventArgs e)
        {

        }
        protected void adsBtn_Click(object sender, EventArgs e)
        {
           
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            
           
        }

        protected void editBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("BBLocationUpdate.aspx");

        }
    }
}