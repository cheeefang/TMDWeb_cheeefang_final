using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using targeted_marketing_display;

namespace targeted_marketing_display
{
    public partial class ProfileUpdate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                User userObj = new User();
                UserManagement uDao = new UserManagement();

                if (Session["userType"].ToString() == Reference.USR_ADM)
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
            divCurrentPassword.Visible = true;
            divCPswd.Visible = true;
            divPswd.Visible = true;
            btnPswd.Visible = false;
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            UserManagement uDao = new UserManagement();
            User uObj = new User();

            if (Session["userType"].ToString() == Reference.USR_ADM)
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
            //initialise hash password
            string uPswdHash = "";
            //initalise salted password
            string uPswdSalt = "";
            int testing = 1;
            //pswdmatch=1
            int pswdMatch = 1;
            //if empty make password hash and salt same
            if (CurrentPassword.Text == "" && CurrentPassword.Visible == false || tbPswd.Text == "" || tbCPswd.Text == "" || (tbPswd.Text == "" && tbCPswd.Text == ""))
            {
                uPswdHash = (string)uObj.PasswordHash;
                uPswdSalt = (string)uObj.PasswordSalt;
                Boolean insCnt = uDao.updateCurrentUser(Session["userID"].ToString(), uName, uContact, uPswdHash, uPswdSalt, lastUpdBy, lastUpdOn);

                tbName.Text = String.Empty;
                tbContact.Text = String.Empty;
                alertSuccess.Visible = true;
            }
            else
            {
                    string passwordhashlol = uObj.PasswordHash;
                    // convert into bytes
                    byte[] hashbyteslol = Convert.FromBase64String(passwordhashlol);

                    // take the salt out of the string
                    byte[] saltlol = new byte[16];
                    Array.Copy(hashbyteslol, 0, saltlol, 0, 16);

                    // hash the entered Current password
                    var pbkdf2lol = new Rfc2898DeriveBytes(CurrentPassword.Text, saltlol, 10000);


                    byte[] hashlol = pbkdf2lol.GetBytes(20);

                    for (int i = 0; i < 20; i++)
                    {
                        if (hashbyteslol[i + 16] != hashlol[i])
                        {
                            pswdMatch = 0;
                        }
                    }


                    if (pswdMatch == 1)
                    {
                        if (tbPswd.Text == tbCPswd.Text)
                        {
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
                            Boolean insCnt = uDao.updateCurrentUser(Session["userID"].ToString(), uName, uContact, uPswdHash, uPswdSalt, lastUpdBy, lastUpdOn);

                            tbName.Text = String.Empty;
                            tbContact.Text = String.Empty;

                            alertSuccess.Visible = true;
                            alertDanger.Visible = false;
                        }
                    }
                    else
                    {
                        alertDanger.Visible = true;
                        alertSuccess.Visible = false;
                    }
                
            }


         

           
        }
    }
}