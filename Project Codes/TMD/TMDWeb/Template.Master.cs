using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using targeted_marketing_display;
namespace TMDWeb
{
    public partial class Template : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                User userObj = new User();
                UserManagement uDao = new UserManagement();

                ////noted,CheEe(001):add this to pass the create user page!!!
                //if (Session["userType"] == null)
                //{
                //    Session["userType"] = "Admin";
                //    Session["userID"] = 18;// "zshiyun98@gmail.com";
                //}
                if (Session["userID"]==null||Session["userType"] ==null)
                {
                    Response.Redirect("login.aspx");
                }

                if (Session["userType"].ToString() == Reference.USR_ADM)
                {
                    userObj = uDao.getAdminByID(Session["userID"].ToString());

                    adminDiv.Visible = true;
                    userDiv.Visible = false;
                    lbAdminName.Text = userObj.Name;
                }
                else if (Session["userType"].ToString() == Reference.USR_MEM)
                {
                    userObj = uDao.getUserByID(Session["userID"].ToString());

                    adminDiv.Visible = false;
                    userDiv.Visible = true;
                    lbUserName.Text = userObj.Name;
                }
            }

        }
        protected void logout_click(object sender,EventArgs e)
        {
            Session.RemoveAll();
            Response.Redirect("login.aspx");
        }
    }
}