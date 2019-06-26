using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using targeted_marketing_display.App_Code;

namespace targeted_marketing_display
{
    public partial class CreateNewUser : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                Database db = new Database();

                SqlCommand cmd = new SqlCommand("Select * from Company");
                DataTable dt = db.getDataTable(cmd);

                ddlCompany.DataSource = dt;
                ddlCompany.DataValueField = "CompanyID";
                ddlCompany.DataTextField = "Name";
                ddlCompany.DataBind();

                ddlCompany.Items.Insert(0, new ListItem("---Select A Company---", "0"));
            }
        }

        public string CreatePassword(int length)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }

        protected void sendMail(string subject, string body, string toEmail)
        {


            var fromAddress = "tmdboss2019@gmail.com";
            var toAddress = toEmail;
            const string fromPassword = "swqa1234";
            var smtp = new System.Net.Mail.SmtpClient();
            {
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                smtp.Credentials = new NetworkCredential(fromAddress,fromPassword);

            }
            smtp.Send(fromAddress, toAddress, subject, body);
        }

        protected void ddlUserType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlUserType.SelectedValue == "Member")
            {
                nUser.Visible = true;
            }
            else if (ddlUserType.SelectedValue == "Admin")
            {
                nUser.Visible = false;
            }
        }

        protected void btnCreate_User(object sender, EventArgs e)
        {
            string Name = tbName.Text;
            string Type = ddlUserType.SelectedValue;
            string Email = tbEmail.Text;
            string ContactNumber = tbConNo.Text;
            string Pswd = CreatePassword(8);
            int Status = 1;
            int CreatedBy = Convert.ToInt32(Session["userID"]);
            string CreatedOn = DateTime.Now.ToString("MM/dd/yyyy h:mm tt");
            int CompanyID = Convert.ToInt32(ddlCompany.SelectedValue);

            if (Type == "NULL")
            {
                alertWarning.Visible = true;
                msgWarning.Text = "Please Select User Type!";
            }
            else
            {
                if (Type == "Member" && CompanyID == 0)
                {
                    alertWarning.Visible = true;
                    msgWarning.Text = "Please Select Company!";
                }
                else
                {
                    // make a new byte array
                    byte[] salt;

                    // generate salt
                    new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);

                    // hash and salt using PBKDF2
                    var pbkdf2 = new Rfc2898DeriveBytes(Pswd, salt, 10000);

                    // place string in byte array
                    byte[] hash = pbkdf2.GetBytes(20);

                    // make new byte array to store hashed password + salt
                    // 36 --> 16(salt) + 20(hash)

                    byte[] hashbytes = new byte[36];
                    Array.Copy(salt, 0, hashbytes, 0, 16);
                    Array.Copy(hash, 0, hashbytes, 16, 20);

                    string PasswordHash = Convert.ToBase64String(hashbytes);
                    string PasswordSalt = Convert.ToBase64String(salt);

                    UserManagement uDao = new UserManagement();
                    User uObj = new User();

                    uObj = uDao.checkEmail(Email);

                    int EmailExist = 1;

                    if (uObj == null)
                    {
                        EmailExist = 0;
                    }

                    if (EmailExist == 0)
                    {
                        if (Type == "Admin")
                        {
                            Boolean insCnt = uDao.createAdmin(Name, Email, ContactNumber, Type, PasswordHash, PasswordSalt, Status, CreatedBy, CreatedOn);

                            System.Diagnostics.Debug.WriteLine("Working");
                        }
                        else
                        {
                            Boolean insCnt = uDao.createUser(Name, Email, ContactNumber, Type, PasswordHash, PasswordSalt, Status, CompanyID, CreatedBy, CreatedOn);
                        }

                        string body = "Dear " + Name + ", " + Environment.NewLine + Environment.NewLine + "Your Account Has Been Successfully Created! " + Environment.NewLine + "This Is Your First-Time Login Password: " + Pswd + " . Please Proceed To Change Your Password Upon Your First Login. Thank you. " + Environment.NewLine + Environment.NewLine + Environment.NewLine + "Regards, " + Environment.NewLine + "Targeted Marketing Admin Team";
                        string subject = "Account Successfully Created!";
                        string toEmail = Email;
                        sendMail(subject, body, toEmail);

                        //VIC: after successful creation, the fields should be cleared to min the risk of user clicking on the submit button again
                        ddlUserType.SelectedIndex = 0;
                        ddlCompany.SelectedIndex = 0;
                        tbName.Text = String.Empty;
                        tbEmail.Text = String.Empty;
                        tbConNo.Text = String.Empty;

                        alertSuccess.Visible = true;
                        msgSuccess.Text = Name + " Has Been Created Successfully!";
                    }
                    //VIC: do not need to check if contact already exist
                    else if (EmailExist > 0)
                    {
                        tbEmail.Text = String.Empty;

                        alertWarning.Visible = true;
                        msgWarning.Text = "Email Already In-Use. Please Try Again!";
                    }
                }
            }
        }
    }
}