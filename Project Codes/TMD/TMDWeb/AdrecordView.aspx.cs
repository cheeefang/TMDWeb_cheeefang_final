using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
namespace targeted_marketing_display
{
    public partial class AdrecordView : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            selected.Visible = false;
            all.Visible = true;
        }
        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DropDownList1.Text == "ItemType")
            {
                DropDownList2.Items.Clear();
                DropDownList2.DataSource = SqlDataSource2;
                DropDownList2.DataTextField = "ItemType";
                DropDownList2.DataValueField = "ItemType";
                DropDownList2.DataBind();

                DropDownList2.Items.Insert(0, "--Select--");

            }

            if (DropDownList1.Text == "Status")
            {
                DropDownList2.Items.Clear();
                DropDownList2.DataSource = SqlDataSource3;
                DropDownList2.DataTextField = "Status";
                DropDownList2.DataValueField = "Status";
                DropDownList2.DataBind();

                DropDownList2.Items.Insert(0, "--Select--");
            }

            else if (DropDownList1.Text == "CreatedBy")
            {
                DropDownList2.Items.Clear();
                DropDownList2.DataSource = SqlDataSource4;
                DropDownList2.DataTextField = "CreatedBy";
                DropDownList2.DataValueField = "CreatedBy";
                DropDownList2.DataBind();

                DropDownList2.Items.Insert(0, "--Select--");
            }

            else if (DropDownList1.Text == "LastUpdBy")
            {
                DropDownList2.Items.Clear();
                DropDownList2.DataSource = SqlDataSource5;
                DropDownList2.DataTextField = "LastUpdBy";
                DropDownList2.DataValueField = "LastUpdBy";
                DropDownList2.DataBind();

                DropDownList2.Items.Insert(0, "--Select--");
            }

        }
        protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (DropDownList1.Text == "ItemType")
            {
                GridView2.DataSource = SqlDataSource6;
                GridView2.DataBind();
                all.Visible = false;
                selected.Visible = true;

            }
            else if (DropDownList1.Text == "Status")
            {
                GridView2.DataSource = SqlDataSource7;
                GridView2.DataBind();
                all.Visible = false;
                selected.Visible = true;

            }
            else if (DropDownList1.Text == "CreatedBy")
            {
                GridView2.DataSource = SqlDataSource8;
                GridView2.DataBind();
                all.Visible = false;
                selected.Visible = true;

            }
            else if (DropDownList1.Text == "LastUpdBy")
            {
                GridView2.DataSource = SqlDataSource9;
                GridView2.DataBind();
                all.Visible = false;
                selected.Visible = true;

            }


        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            all.Visible = true;
            selected.Visible = false;
        }

        protected void GridView1_PreRender(object sender, EventArgs e)
        {
            Label1.Text = "Displaying Page " + (GridView1.PageIndex + 1).ToString() + " of " + GridView1.PageCount.ToString();
        }
    }
}