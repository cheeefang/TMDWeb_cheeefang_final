using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data;
using System.Data.SqlClient;
using BBMgmt;
using System.Text;


using targeted_marketing_display.App_Code;
namespace targeted_marketing_display
{
    public partial class BillboardAdInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Billboard BillboardObj = new Billboard();
                Billboard_Management bDao = new Billboard_Management();
                //Company CompanyObj = new Company();
                // Company_Management CDao = new Company_Management();

                BillboardObj = bDao.getBillboardByID(Session["BillboardID"].ToString());
            }
        }
    }
}