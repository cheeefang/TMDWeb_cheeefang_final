using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace targeted_marketing_display
{
    public partial class f : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BindGrid();
        }
        private void BindGrid()
        {
            DataTable table = new DataTable();
            table.Columns.Add("Dosage", typeof(int));
            table.Columns.Add("Drug", typeof(string));
            table.Columns.Add("Patient", typeof(string));
            table.Columns.Add("Date", typeof(DateTime));

            // Here we add five DataRows.  
            table.Rows.Add(10, "Hydralazine", "Christoff", DateTime.Now);
            table.Rows.Add(21, "Combivent", "Janet", DateTime.Now);
            table.Rows.Add(100, "Dilantin", "Melanie", DateTime.Now);
            GridView1.DataSource = table;
            GridView1.DataBind();
        }
        protected void GridView1_PreRender(object sender, EventArgs e)
        {

            // You only need the following 2 lines of code if you are not 
            // using an ObjectDataSource of SqlDataSource
 

            if (GridView1.Rows.Count > 0)
            {
                //This replaces <td> with <th> and adds the scope attribute
                GridView1.UseAccessibleHeader = true;

                //This will add the <thead> and <tbody> elements
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;

                //This adds the <tfoot> element. 
                //Remove if you don't have a footer row
              //  GridView1.FooterRow.TableSection = TableRowSection.TableFooter;
            }

        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.TableSection = TableRowSection.TableHeader;
            }
        }
    }
}