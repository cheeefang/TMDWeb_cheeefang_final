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
         
            }
            
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
            
        }
    }



}