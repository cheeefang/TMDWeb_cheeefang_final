using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using targeted_marketing_display.App_Code;

namespace targeted_marketing_display
{
    public partial class UpdateOwnProfile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                User userObj = new User();
                UserManagement uDao = new UserManagement();

                if (Session["userType"].ToString() == "Admin")
                {
                    userObj = uDao.getAdminByID(Session["userID"].ToString());
                }
                else
                {
                    userObj = uDao.getUserByID(Session["userID"].ToString());
                }

                tbName.Text = userObj.Name;
                tbContact.Text = userObj.ContactNumber;
            }
        }

        protected void btnPswd_Click(object sender, EventArgs e)
        {
            divCPswd.Visible = true;
            divPswd.Visible = true;
            btnPswd.Visible = false;
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            UserManagement uDao = new UserManagement();
            User uObj = new User();

            if (Session["userType"].ToString() == "Admin")
            {
                uObj = uDao.getAdminByID(Session["userID"].ToString());
            }
            else
            {
                uObj = uDao.getUserByID(Session["userID"].ToString());
            }

            string uName = tbName.Text;
            string uContact = tbContact.Text;
            string lastUpdBy = Session["userID"].ToString();
            string lastUpdOn = DateTime.Now.ToString("MM/dd/yyyy h:mm tt");
            string uPswdHash = "";
            string uPswdSalt = "";

            if (tbPswd.Text == "" || tbCPswd.Text == "" || (tbPswd.Text == "" && tbCPswd.Text == ""))
            {
                uPswdHash = (string)uObj.PasswordHash;
                uPswdSalt = (string)uObj.PasswordSalt;
            }
            else if (tbPswd.Text == tbCPswd.Text)
            {
                // make a new byte array
                byte[] salt;

                // generate salt
                new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);

                // hash and salt using PBKDF2
                var pbkdf2 = new Rfc2898DeriveBytes(tbCPswd.Text, salt, 10000);

                // place string in byte array
                byte[] hash = pbkdf2.GetBytes(20);

                // make new byte array to store hashed password + salt
                // 36 --> 16(salt) + 20(hash)

                byte[] hashbytes = new byte[36];
                Array.Copy(salt, 0, hashbytes, 0, 16);
                Array.Copy(hash, 0, hashbytes, 16, 20);

                string PasswordHash = Convert.ToBase64String(hashbytes);
                string PasswordSalt = Convert.ToBase64String(salt);

                uPswdHash = PasswordHash;
                uPswdSalt = PasswordSalt;
            }

            Boolean insCnt = uDao.updateCurrentUser(Session["userID"].ToString(), uName, uContact, uPswdHash, uPswdSalt, lastUpdBy, lastUpdOn);

            tbName.Text = String.Empty;
            tbContact.Text = String.Empty;

            alertSuccess.Visible = true;
        }
    }
}