using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using System.Data.SqlClient;
using BBMgmt;
using System.Text;
using PagedList;
using System.Threading.Tasks;
using targeted_marketing_display.App_Code;
namespace targeted_marketing_display
{
    public partial class BillboardAdInfo : System.Web.UI.Page
    {
    
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.BindGrid();

            }
        }

        protected void BindGrid()
        {
            SqlConnection conn = null;
            SqlDataReader reader = null;



            // instantiate and open connection
            conn = new
                SqlConnection(@"Data Source=L33527\CHEEEFANGSQL;Initial Catalog=Targeted_Marketing_Display;Persist Security Info=True;User ID=root;Password=passw8rd");
            conn.Open();



            // 1. declare command object with parameter
            SqlCommand cmd = new SqlCommand(
                " select [BillboardLocation].BillboardCode, [Advertisement].Name,[Advertisement].Item,[Advertisement].ItemType,[Advertisement].StartDate,[Advertisement].EndDate from [Advertisement] inner join" +
                " [AdvertisementLocation] on [Advertisement].AdvID=[AdvertisementLocation].AdvID join " +
                "[BillboardLocation] on[AdvertisementLocation].BillboardID =[BillboardLocation].BillboardID " +
                "where [Advertisement].status=1 and [BillboardLocation].BillboardID=@ID", conn);

            // 2. define parameters used in command object
            SqlParameter param = new SqlParameter();
            param.ParameterName = "@ID";
            param.Value = Session["BillboardID"].ToString();

            // 3. add new parameter to command object
            cmd.Parameters.Add(param);
            SqlDataAdapter sda = new SqlDataAdapter();
            DataTable dt = new DataTable();
            cmd.Connection = conn;
            sda.SelectCommand = cmd;
            sda.Fill(dt);
      


            GridView1.DataSource = dt;
            GridView1.DataBind();
            
            if (GridView1.Rows.Count == 0)
            {
                ErrorMessage.Visible = true;
            }
            SqlCommand cmdCount = new SqlCommand("select count(*) from [Advertisement] inner join [AdvertisementLocation] on [Advertisement].AdvID=[AdvertisementLocation].AdvID where BillboardID=@BillboardID and status=1", conn);
            SqlParameter paramCount = new SqlParameter();
            paramCount.ParameterName = "@BillboardID";
            paramCount.Value = Session["BillboardID"].ToString();
            cmdCount.Parameters.Add(paramCount);
            Int32 numberOfRows = Convert.ToInt32(cmdCount.ExecuteScalar());
            rowCountLabel.Text = "("+numberOfRows.ToString()+" Ads)";
            conn.Close();
        }


        protected void GridView1_PreRender(object sender, EventArgs e)
        {
            LabelPaging.Text = "Displaying Page " + (GridView1.PageIndex + 1).ToString() + " of " + GridView1.PageCount.ToString();
        }



        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
           
            BindGrid();
        }
    }
}