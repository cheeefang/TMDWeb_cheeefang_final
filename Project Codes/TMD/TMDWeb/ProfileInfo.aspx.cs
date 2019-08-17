using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using targeted_marketing_display;
namespace targeted_marketing_display
{
    public partial class ProfileInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            diffUserView();
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            Response.Redirect("ProfileUpdate.aspx");
        }

        protected void diffUserView()
        {
            //VIC: the user of the session key username is misleading, it should be usertype right?
            if ((string)Session["userType"] == Reference.USR_ADM)
            {
                UserView.Visible = false;
                AdminView.Visible = true;

                User userObj = new User();
                UserManagement uDao = new UserManagement();

                userObj = uDao.getAdminByID(Session["userID"].ToString());

                lbAdminName.Text = userObj.Name;
                lbAdminEmail.Text = userObj.Email;
                lbAdminContact.Text = userObj.ContactNumber;
                lbAdminType.Text = uDao.getUserType(userObj.Type);
                lbAdminStatus.Text = uDao.getUserStatus(userObj.Status);
            }
            //VIC: the condition is redundant, if the above condition is false which already means username is not admin, there is no need for this statement as it will always be true
            else
            {
                UserView.Visible = true;
                AdminView.Visible = false;

                User userObj = new User();
                UserManagement uDao = new UserManagement();


                userObj = uDao.getUserByID(Session["userID"].ToString());

                lbName.Text = userObj.Name;
                lbEmail.Text = userObj.Email;
                lbContact.Text = userObj.ContactNumber;
                lbUserType.Text = uDao.getUserType(userObj.Type);
                lbCompany.Text = userObj.CompanyName;
                lbStatus.Text = uDao.getUserStatus(userObj.Status);
            }
        }
    }
}