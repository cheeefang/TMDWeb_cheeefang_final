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
using System.Web.UI;

namespace targeted_marketing_display
{
    public partial class CompanyAdvertInfo : System.Web.UI.Page
    {
        SqlConnection vid = new
              SqlConnection(@"Data Source=L33527\CHEEEFANGSQL;Initial Catalog=Targeted_Marketing_Display;Persist Security Info=True;User ID=root;Password=passw8rd");
        Database dbConnection = new Database();

        
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                
                this.BindGrid();
         
            }

        }
        protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (GridView1.Attributes["CurrentSortField"] != null && GridView1.Attributes["CurrentSortDirection"] != null)
            {
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    foreach (TableCell tableCell in e.Row.Cells)
                    {
                        if (tableCell.HasControls())
                        {
                            LinkButton sortLinkButton = null;
                            if (tableCell.Controls[0] is LinkButton)
                            {
                                sortLinkButton = (LinkButton)tableCell.Controls[0];
                            }

                            if (sortLinkButton != null && GridView1.Attributes["CurrentSortField"] == sortLinkButton.CommandArgument)
                            {
                                Image image = new Image();
                                if (GridView1.Attributes["CurrentSortDirection"] == "ASC")
                                {
                                    image.ImageUrl = "~/Images/Ascending.png";
                                    image.Width = 50;
                                    image.Height = 50;
                                }
                                else
                                {
                                    image.ImageUrl = "~/Images/Descending.png";
                                    image.Width = 50;
                                    image.Height = 50;
                                }
                                tableCell.Controls.Add(new LiteralControl("&nbsp;"));
                                tableCell.Controls.Add(image);
                            }
                        }
                    }
                }
            }
        }
        protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
        {
            SqlConnection mycon = null;
            mycon = new SqlConnection(@"Data Source=L33527\CHEEEFANGSQL;Initial Catalog=Targeted_Marketing_Display;Persist Security Info=True;User ID=root;Password=passw8rd");
            mycon.Open();
                SqlCommand cmdSort = new SqlCommand("select [Company].Name as Company,[Advertisement].Name as adname,[Advertisement].Item,[Advertisement].ItemType,[Advertisement].StartDate,[Advertisement].EndDate from [Advertisement] inner join [Company] on [Advertisement].CompanyID =[Company].CompanyID " +
                        "where [Company].CompanyID=@ID and [Advertisement].status=1", mycon);
            SqlParameter paramSort = new SqlParameter();
            paramSort.ParameterName = "@ID";
                paramSort.Value = Session["CompanyID"].ToString();
            cmdSort.Parameters.Add(paramSort);
                SqlDataAdapter sdaSort = new SqlDataAdapter();
            DataSet ds = new DataSet();
            cmdSort.Connection = mycon;
                sdaSort.SelectCommand = cmdSort;
                sdaSort.Fill(ds);
                mycon.Close();
                DataTable dtSort = ds.Tables[0];

            DataView dv = new DataView(dtSort);
            if (ViewState["sorting"] == "DESC" || ViewState["sorting"] == null)
            {
                dv.Sort = e.SortExpression + " ASC";
                ViewState["sorting"] = "ASC";
               
            }
            else if (ViewState["sorting"].ToString() == "ASC")
            {
                dv.Sort = e.SortExpression + " DESC";
                ViewState["sorting"] = "DESC";
              
            }
            GridView1.DataSource = dv;
            GridView1.DataBind();




        }
        public void BindGrid()
        {
            try
            {

              
                SqlConnection conn = null;
                    SqlDataReader reader = null;



                    // instantiate and open connection
                    conn = new
                        SqlConnection(@"Data Source=L33527\CHEEEFANGSQL;Initial Catalog=Targeted_Marketing_Display;Persist Security Info=True;User ID=root;Password=passw8rd");
                    conn.Open();

                    // 1. declare command object with parameter
                    SqlCommand cmd = new SqlCommand(
                        "select [Company].Name as Company,[Advertisement].Name as adname,[Advertisement].Item,[Advertisement].ItemType,[Advertisement].StartDate,[Advertisement].EndDate from [Advertisement] inner join [Company] on [Advertisement].CompanyID =[Company].CompanyID " +
                        "where [ComPany].CompanyID=@ID and [Advertisement].status=1 ", conn);

                    // 2. define parameters used in command object
                    SqlParameter param = new SqlParameter();
                    param.ParameterName = "@ID";
                    param.Value = Session["CompanyID"].ToString();

                    // 3. add new parameter to command object
                    cmd.Parameters.Add(param);


                    SqlDataAdapter sda = new SqlDataAdapter();
                    DataTable dt = new DataTable();


                    cmd.Connection = conn;
                    sda.SelectCommand = cmd;
                    sda.Fill(dt);


                    // get data stream



                    GridView1.DataSource = dt;
                    GridView1.DataBind();

                    if (GridView1.Rows.Count == 0)
                    {

                    }
                    SqlCommand cmdCount = new SqlCommand("select count(*) from Advertisement where companyID=@CompanyID and status=1", conn);
                    SqlParameter paramCount = new SqlParameter();
                    paramCount.ParameterName = "@CompanyID";
                    paramCount.Value = Session["CompanyID"].ToString();
                    cmdCount.Parameters.Add(paramCount);
                    Int32 numberOfRows = Convert.ToInt32(cmdCount.ExecuteScalar());
                    counttext.Text = numberOfRows.ToString() + " Advertisement(s)";

                    SqlCommand cmdIndustry = new SqlCommand("select c.Industry from Advertisement a inner join Company c on a.companyID=c.CompanyID where a.companyID=@ID and a.status=1", conn);
                    SqlParameter paramIndustry = new SqlParameter();
                    paramIndustry.ParameterName = "@ID";
                    paramIndustry.Value = Session["CompanyID"].ToString();
                    cmdIndustry.Parameters.Add(paramIndustry);
                    SqlDataAdapter sdaIndustry = new SqlDataAdapter();
                    DataTable dtIndustry = new DataTable();
                    cmdIndustry.Connection = conn;
                    sdaIndustry.SelectCommand = cmdIndustry;
                    sdaIndustry.Fill(dtIndustry);
                    for (int x = 0; x < dtIndustry.Rows.Count; x++)
                    {
                        string IndustryCheck = (dtIndustry.Rows[x]["Industry"]).ToString();

                        industrytext.Text = IndustryCheck;
                    }




                    }
                    catch (System.ArgumentOutOfRangeException ArgumentOutOfRangeException)
                {
                    //ErrorHandler.Text = "This Company does not have any Advertisements";
                    labelIndustry.Visible = false;
                }

            }
            


        protected void GridView1_PreRender(object sender, EventArgs e)
        {
            LabelPaging.Text = "Displaying Page " + (GridView1.PageIndex + 1).ToString() + " of " + GridView1.PageCount.ToString();
         
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
           
            GridView1.PageIndex = e.NewPageIndex;
            GridView1.DataBind();
            BindGrid();
            
        }

        protected void btnRun_Click(object sender, EventArgs e)
        {
            string str = " select [Company].Name as Company,[Advertisement].Name as adname,[Advertisement].Item,[Advertisement].ItemType,[Advertisement].StartDate,[Advertisement].EndDate from [Advertisement] inner join [Company] on [Advertisement].CompanyID =[Company].CompanyID " +
                        "where [Company].CompanyID=@ID and [Advertisement].status=1 and ([Advertisement].Name like '%' + @search + '%' OR ItemType like '%'" +
                        " + @search + '%' OR StartDate like '%' + @search + '%' OR  EndDate like '%' + @search + '%') ";
            SqlCommand xp = new SqlCommand(str, vid);
            xp.Parameters.Add("@ID", SqlDbType.NVarChar).Value = Session["CompanyID"].ToString();
            xp.Parameters.Add("@search", SqlDbType.NVarChar).Value = txtSearch.Text;
            //xp.Parameters.Add("@search2", SqlDbType.NVarChar).Value = txtSearch.Text;
            vid.Open();
            xp.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = xp;
            DataSet ds = new DataSet();
            da.Fill(ds, "Name");
            GridView1.DataSource = ds;
            GridView1.DataBind();



        }
    }
}