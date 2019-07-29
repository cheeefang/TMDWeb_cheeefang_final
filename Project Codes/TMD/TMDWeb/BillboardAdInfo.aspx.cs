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

using System.Threading.Tasks;
using targeted_marketing_display.App_Code;
namespace targeted_marketing_display
{
    public partial class BillboardAdInfo : System.Web.UI.Page
    {
        SqlConnection vid= new
                SqlConnection(@"Data Source=L33527\CHEEEFANGSQL;Initial Catalog=Targeted_Marketing_Display;Persist Security Info=True;User ID=root;Password=passw8rd");
        
        protected string latitude;
        protected string longtitude;
        protected void Page_Load(object sender, EventArgs e)
        {
          
            
            if (!IsPostBack)
            {
              
                Billboard BillboardObj = new Billboard();
                Billboard_Management bDao = new Billboard_Management();

                BillboardObj = bDao.getBillboardByID(Session["BillboardID"].ToString());
                latitude = BillboardObj.latitude;
                longtitude = BillboardObj.Longtitude;

                if (ViewState["sorting"] == null)
                {
                    this.BindGrid();
                }
            }
            try
            {


                var rowIndex = 0;
                var hiddenvalue = (string)GridView1.DataKeys[rowIndex]["BillboardCode"];
                BillboardCodelabel.Text = " for " + hiddenvalue.ToString();

            }
            catch (System.ArgumentOutOfRangeException ArgumentOutOfRangeException)
            {
                
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
            rowcounttext.Text = numberOfRows.ToString() + " Advertisement(s)";
            

            
          

        }

        protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
        {
            SqlConnection mycon = null;
            mycon = new SqlConnection(@"Data Source=L33527\CHEEEFANGSQL;Initial Catalog=Targeted_Marketing_Display;Persist Security Info=True;User ID=root;Password=passw8rd");
            mycon.Open();
            SqlCommand cmdSort = new SqlCommand("select [BillboardLocation].BillboardCode, [Advertisement].Name,[Advertisement].Item,[Advertisement].ItemType,[Advertisement].StartDate,[Advertisement].EndDate from [Advertisement] inner join" +
                " [AdvertisementLocation] on [Advertisement].AdvID=[AdvertisementLocation].AdvID join " +
                "[BillboardLocation] on[AdvertisementLocation].BillboardID =[BillboardLocation].BillboardID " +
                "where [Advertisement].status=1 and [BillboardLocation].BillboardID=@ID", mycon);
            SqlParameter paramSort = new SqlParameter();
            paramSort.ParameterName = "@ID";
            paramSort.Value = Session["BillboardID"].ToString();
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
                                    image.Width=50;
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
        protected void GridView1_PreRender(object sender, EventArgs e)
        {
            LabelPaging.Text = "Displaying Page " + (GridView1.PageIndex + 1).ToString() + " of " + GridView1.PageCount.ToString();

        }



        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
           
            BindGrid();
        }



        protected void btnRun_Click(object sender, EventArgs e)
        {
            
            string str = " select [BillboardLocation].BillboardCode, [Advertisement].Name,[Advertisement].Item,[Advertisement].ItemType,[Advertisement].StartDate,[Advertisement].EndDate from [Advertisement] inner join" +
                " [AdvertisementLocation] on [Advertisement].AdvID=[AdvertisementLocation].AdvID join " +
                "[BillboardLocation] on[AdvertisementLocation].BillboardID =[BillboardLocation].BillboardID " +
                "where [Advertisement].status=1 and [BillboardLocation].BillboardID=@ID and (Name like '%' + @search + '%' OR ItemType like '%' + @search + '%' ) ";
            SqlCommand xp = new SqlCommand(str, vid);
            xp.Parameters.Add("@ID", SqlDbType.NVarChar).Value = Session["BillboardID"].ToString();
            xp.Parameters.Add("@search", SqlDbType.NVarChar).Value = txtSearch.Text;
      
            vid.Open();
            xp.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = xp;
            DataSet ds = new DataSet();
            da.Fill(ds);
            GridView1.DataSource = ds;
            GridView1.DataBind();
            


        }
    }
}