using System;
using System.Collections;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using System.Collections.Generic;
using CoInfoMgmt;
using targeted_marketing_display.App_Code;
using targeted_marketing_display;
using System.Web.UI.WebControls;

namespace targeted_marketing_display
{
    public partial class CompanyAdvertInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                CompanyAdverts CompanyObj = new CompanyAdverts();
                Company_Management cDao = new Company_Management();

                CompanyObj = cDao.getCompanyAdvertsByCompanyID(Session["CompanyID"].ToString());
                foreach (GridViewRow gr in GridView1.Rows)
                {

                    GridView1.Rows[gr.RowIndex].Cells[0].Text = CompanyObj.AdvName.ToString();
                    GridView1.Rows[gr.RowIndex].Cells[1].Text = CompanyObj.ItemType.ToString();
                    GridView1.Rows[gr.RowIndex].Cells[2].Text = CompanyObj.StartDate.ToString();
                    GridView1.Rows[gr.RowIndex].Cells[3].Text =CompanyObj.EndDate.ToString();
                        
                }





            }


        }

        protected void GridView1_PreRender(object sender, EventArgs e)
        {
           
        }
    }
}