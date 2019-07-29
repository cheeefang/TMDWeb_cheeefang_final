﻿using System;
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
        private string sortExpression;
        private SortDirection sortDirection;

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
            else
            {
                //if it is a postback, fetch SortExpression and Direction from viewstate
                //and store it in local variables
                if (ViewState["SortExpression"] != null)
                    sortExpression = ViewState["SortExpression"].ToString();
                else
                    sortExpression = String.Empty;

                if (ViewState["SortDirection"] != null)
                {
                    if (Convert.ToInt32(ViewState["SortDirection"]) == (int)SortDirection.Ascending)
                    {
                        sortDirection = SortDirection.Ascending;
                    }
                    else
                    {
                        sortDirection = SortDirection.Descending;
                    }
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
            if (ViewState["sorting"] == "descending" || ViewState["sorting"] == null)
            {
                dv.Sort = e.SortExpression + " ascending";
                ViewState["sorting"] = "ascending";
               
            }
            else if (ViewState["sorting"].ToString() == "ascending")
            {
                dv.Sort = e.SortExpression + " descending";
                ViewState["sorting"] = "descending";
              
            }
            GridView1.DataSource = dv;
            GridView1.DataBind();




        }
        protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
        {
            //check if it is a header row
            //since allowsorting is set to true, column names are added as command arguments to
            //the linkbuttons by DOTNET API
            if (e.Row.RowType == DataControlRowType.Header)
            {
                LinkButton btnSort;
                Image image;
                //iterate through all the header cells
                foreach (TableCell cell in e.Row.Cells)
                {
                    //check if the header cell has any child controls
                    if (cell.HasControls())
                    {
                        //get reference to the button column
                        btnSort = (LinkButton)cell.Controls[0];
                        image = new Image();
                        if (ViewState["SortExpression"] != null)
                        {
                            //see if the button user clicked on and the sortexpression in the viewstate are same
                            //this check is needed to figure out whether to add the image to this header column nor not
                            if (btnSort.CommandArgument == ViewState["SortExpression"].ToString())
                            {
                                //following snippet figure out whether to add the up or down arrow
                                //based on the sortdirection
                                if (Convert.ToInt32(ViewState["SortDirection"]) == (int)SortDirection.Ascending)
                                {
                                    image.ImageUrl = "~/Images/Ascending.png";
                                }
                                else
                                {
                                    image.ImageUrl = "~/Images/Descending.png";
                                }
                                cell.Controls.Add(image);
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