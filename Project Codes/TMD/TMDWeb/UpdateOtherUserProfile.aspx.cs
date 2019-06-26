using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using targeted_marketing_display.App_Code;

namespace targeted_marketing_display
{
    public partial class UpdateOtherUserProfile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                User userObj = new User();
                UserManagement uDao = new UserManagement();


                userObj = uDao.getUserByID(Session["SelectedID"].ToString());

                tbName.Text = userObj.Name;
                tbContact.Text = userObj.ContactNumber;
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            UserManagement uDAO = new UserManagement();

            string uName = tbName.Text;
            string uContact = tbContact.Text;
            string lastUpdBy = Session["userID"].ToString();
            string lastUpdOn = DateTime.Now.ToString("MM/dd/yyyy h:mm tt");

            Boolean insCnt = uDAO.updateUser(Session["SelectedID"].ToString(), uName, uContact, lastUpdBy, lastUpdOn);

            tbName.Text = String.Empty;
            tbContact.Text = String.Empty;

            alertSuccess.Visible = true;
            msgSuccess.Text = "Updated Successfully";
        }
    }
}