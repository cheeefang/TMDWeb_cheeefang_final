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
                // conn and reader declared outside try
                // block for visibility in finally block
                SqlConnection conn = null;
                SqlDataReader reader = null;

                string inputCity = "London";
              
                    // instantiate and open connection
                    conn = new
                        SqlConnection(@"Data Source=L33527\CHEEEFANGSQL;Initial Catalog=Targeted_Marketing_Display;Persist Security Info=True;User ID=root;Password=passw8rd");
                    conn.Open();

                  

                    // 1. declare command object with parameter
                    SqlCommand cmd = new SqlCommand(
                        "select [Company].Name,[Advertisement].Name,[Advertisement].ItemType,[Advertisement].StartDate,[Advertisement].EndDate from [Advertisement] inner join [Company] on [Advertisement].CompanyID =[Company].CompanyID " +
                        "where [ComPany].CompanyID=@ID ", conn);

                    // 2. define parameters used in command object
                    SqlParameter param = new SqlParameter();
                    param.ParameterName = "@ID";
                    param.Value = Session["CompanyID"].ToString();

                    // 3. add new parameter to command object
                    cmd.Parameters.Add(param);

                    // get data stream
                    reader = cmd.ExecuteReader();

                   
                    GridView1.DataSource = reader;
                    GridView1.DataBind();



            }
        }





    }


     
}