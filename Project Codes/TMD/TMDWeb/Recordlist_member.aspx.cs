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
    public partial class AdvrecordView : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
          
        }
    

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string AdvID = GridView1.SelectedRow.Cells[5].Text;
            Response.Redirect("Recordviw.aspx?AdvID=" + AdvID);

        }
    }
}