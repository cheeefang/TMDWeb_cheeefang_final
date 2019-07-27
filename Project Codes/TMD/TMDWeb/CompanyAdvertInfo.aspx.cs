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

        Database dbConnection = new Database();

        protected void Page_Load(object sender, EventArgs e)
        {
            

      

            if (!IsPostBack)
            {

                this.BindGrid();
                this.SearchCompAds();



            }
            
        }


        protected void btnRun_click(object sender, EventArgs e)
        {
            this.SearchCompAds();
        }

        private void SearchCompAds()
        {
            string constr = ConfigurationManager.ConnectionStrings["Targeted_Marketing_DisplayConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd2 = new SqlCommand())
                {
                    string sql = "select [Company].Name as Company,[Advertisement].Name AS adName,[Advertisement].Item,[Advertisement].ItemType,[Advertisement].StartDate,[Advertisement].EndDate from [Advertisement] inner join [Company] on [Advertisement].CompanyID =[Company].CompanyID  " +
                "";
                    
                    if (!string.IsNullOrEmpty(txtSearch.Text.Trim()))
                    {
                        sql += "[Company].Name LIKE @CName OR Item LIKE @Item OR [Advertisement].Name LIKE @adName OR ItemType LIKE @ItemType OR convert(StartDate,'System.String') LIKE @sDate + '%' OR convert(EndDate,'System.String') LIKE @eDate + '%' and [Company].CompanyID=@ID and [Advertisement].status=1";
                        cmd2.Parameters.AddWithValue("@ID", Session["CompanyID"].ToString());
                        cmd2.Parameters.AddWithValue("@CName", txtSearch.Text.Trim());
                        cmd2.Parameters.AddWithValue("@adName", txtSearch.Text.Trim());
                        cmd2.Parameters.AddWithValue("@Item", txtSearch.Text.Trim());
                        cmd2.Parameters.AddWithValue("@ItemType", txtSearch.Text.Trim());
                        cmd2.Parameters.AddWithValue("@sDate", txtSearch.Text.Trim());
                        cmd2.Parameters.AddWithValue("@eDate", txtSearch.Text.Trim());
                    }
                    cmd2.CommandText = sql;
                    cmd2.Connection = con;
                    using (SqlDataAdapter sda2 = new SqlDataAdapter(cmd2))
                    {
                        DataTable dt2 = new DataTable();
                        sda2.Fill(dt2);
                        //gvCustomers.DataSource = dt;
                        //gvCustomers.DataBind();
                        GridView1.DataSource = dt2;
                        GridView1.DataBind();
                    }
                }
            }
        }
        public void BindGrid()
        {
            try
            {

                // conn and reader declared outside try
                // block for visibility in finally block
                SqlConnection conn = null;
            SqlDataReader reader = null;



            // instantiate and open connection
            conn = new
                SqlConnection(@"Data Source=L33527\CHEEEFANGSQL;Initial Catalog=Targeted_Marketing_Display;Persist Security Info=True;User ID=root;Password=passw8rd");
            conn.Open();

            // 1. declare command object with parameter
            SqlCommand cmd = new SqlCommand(
                "select [Company].Name as Company,[Advertisement].Name,[Advertisement].Item,[Advertisement].ItemType,[Advertisement].StartDate,[Advertisement].EndDate from [Advertisement] inner join [Company] on [Advertisement].CompanyID =[Company].CompanyID " +
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

            SqlCommand cmdIndustry = new SqlCommand("select c.Industry from Advertisement a inner join Company c on a.companyID=c.CompanyID where a.companyID=@ID and a.status=1",conn);
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
            //SqlConnection conn123 = null;
            //SqlDataReader reader123 = null;



            //// instantiate and open connection
            //conn123 = new
            //    SqlConnection(@"Data Source=L33527\CHEEEFANGSQL;Initial Catalog=Targeted_Marketing_Display;Persist Security Info=True;User ID=root;Password=passw8rd");
            //conn123.Open();
            

            //SqlCommand cmdIndustry123 = new SqlCommand("select c.Industry from Advertisement a inner join Company c on a.companyID=c.CompanyID where a.companyID=@ID and a.status=1", conn123);
            //SqlParameter paramIndustry123 = new SqlParameter();
            //paramIndustry123.ParameterName = "@ID";
            //paramIndustry123.Value = Session["CompanyID"].ToString();
            //cmdIndustry123.Parameters.Add(paramIndustry123);
            //SqlDataAdapter sdaIndustry123 = new SqlDataAdapter();
            //DataTable dtIndustry123 = new DataTable();
            //cmdIndustry123.Connection = conn123;
            //sdaIndustry123.SelectCommand = cmdIndustry123;
            //sdaIndustry123.Fill(dtIndustry123);
            //for (int x = 0; x < dtIndustry123.Rows.Count; x++)
            //{
            //    string IndustryCheck = (dtIndustry123.Rows[x]["Industry"]).ToString();

            //    industrytext.Text = IndustryCheck;
            //}

        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
           
            GridView1.PageIndex = e.NewPageIndex;
            GridView1.DataBind();
            BindGrid();
            this.SearchCompAds();
        }
    }



}