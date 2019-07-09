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
            // conn and reader declared outside try
            // block for visibility in finally block
            SqlConnection conn = null;
            SqlDataReader reader = null;



            // instantiate and open connection
            conn = new
                SqlConnection(@"Data Source=L33527\CHEEEFANGSQL;Initial Catalog=Targeted_Marketing_Display;Persist Security Info=True;User ID=root;Password=passw8rd");
            conn.Open();


      

            if (!IsPostBack)
            {
              
                  

                    // 1. declare command object with parameter
                    SqlCommand cmd = new SqlCommand(
                        "select [Company].Name,[Advertisement].Name,[Advertisement].Item,[Advertisement].ItemType,[Advertisement].StartDate,[Advertisement].EndDate from [Advertisement] inner join [Company] on [Advertisement].CompanyID =[Company].CompanyID " +
                        "where [ComPany].CompanyID=@ID and [Advertisement].status=1 ", conn);

                    // 2. define parameters used in command object
                    SqlParameter param = new SqlParameter();
                    param.ParameterName = "@ID";
                    param.Value = Session["CompanyID"].ToString();
                    List<DataList> newList = new List<DataList>();
                    // 3. add new parameter to command object
                    cmd.Parameters.Add(param);

                    // get data stream
                    reader = cmd.ExecuteReader();
                   // SqlDataAdapter da = new SqlDataAdapter(cmd);
                     //List<> ds = new ();
                    //da.Fill(ds);

                    GridView1.DataSource = reader;
                    GridView1.DataBind();
                    if (GridView1.Rows.Count == 0)
                    {
                        ErrorMessage.Visible = true;
                    }


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
        }
    }



}