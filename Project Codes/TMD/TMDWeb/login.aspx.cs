using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using targeted_marketing_display;


namespace targeted_marketing_display
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
      
        }

        protected void login_onclick(object sender, EventArgs e)
        {
        

            string Email = unTB.Text;

            User userObj = new User();
            User userObj1 = new User();
            UserManagement uDao = new UserManagement();

            userObj1 = uDao.checkEmail(Email);

            int EmailMatch = 0;


            if (userObj1 != null)
            {
                EmailMatch = 1;
            }

            if (EmailMatch == 1)
            {
                userObj = uDao.getUserByEmail(Email);
                int pswdMatch = 1;

                //noted,CheEe(002):comment this to bypass the login!!!
               
                string pswdHash = userObj.PasswordHash;

                // convert into bytes
                byte[] hashbytes = Convert.FromBase64String(pswdHash);

                // take the salt out of the string
                byte[] salt = new byte[16];
                Array.Copy(hashbytes, 0, salt, 0, 16);

                // hash the entered password
                var pbkdf2 = new Rfc2898DeriveBytes(pwTB.Text, salt, 10000);

                byte[] hash = pbkdf2.GetBytes(20);

                for (int i = 0; i < 20; i++)
                {
                    if (hashbytes[i + 16] != hash[i])
                        pswdMatch = 0;
                }
          


                if (pswdMatch == 1)
                {
                    Session["userID"] = userObj.UserID;
                    //System.Diagnostics.Debug.Write(Session["userID"]);
                    Session["userType"] = userObj.Type;
                   
                    if ((string)Session["userType"] == Reference.USR_ADM)
                    {
                        Response.Redirect("BillboardList.aspx");
                    }
                    else if ((string)Session["userType"] == Reference.USR_MEM)
                    {
                        Response.Redirect("ProfileInfo.aspx");
                    }
                }

            }
            else
            {
                unTB.Text = String.Empty;
                pwTB.Text = String.Empty;

                Response.Redirect("login.aspx");
            }



        }
    }
}
 