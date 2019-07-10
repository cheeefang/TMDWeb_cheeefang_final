using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Text;
using System.Web;
using targeted_marketing_display;
using targeted_marketing_display.App_Code;


namespace targeted_marketing_display
{
    public class UserManagement
    {
        Database db = new Database();

        public User getUserByID(string userID)
        {
            SqlCommand cmd = new SqlCommand("Select *, Company.Name as CompanyName from [User] inner join Company on [User].CompanyID = Company.CompanyID where userID = @paraUserID and [User].Status = 1");
            cmd.Parameters.AddWithValue("@paraUserID", userID);
            DataTable dt = db.getDataTable(cmd);

            User obj = new User();

            int rec_cnt = dt.Rows.Count;
            if (rec_cnt > 0)
            {
                DataRow row = dt.Rows[0];
                obj.Email = row["Email"].ToString();
                obj.CompanyID = Convert.ToInt32(row["CompanyID"]);
                obj.CompanyName = row["CompanyName"].ToString();
                obj.PasswordHash = row["PasswordHash"].ToString();
                obj.PasswordSalt = row["PasswordSalt"].ToString();
                obj.Name = row["Name"].ToString();
                obj.ContactNumber = row["ContactNumber"].ToString();
                obj.Type = row["Type"].ToString();
                obj.Status = Convert.ToInt32(row["Status"]);
                obj.CreatedBy = Convert.ToInt32(row["CreatedBy"]);
                obj.CreatedOn = row["CreatedOn"].ToString();
                obj.LastUpdBy = row["LastUpdBy"].ToString();
                obj.LastUpdOn = row["LastUpdOn"].ToString();
            }
            else
            {
                obj = null;
            }

            return obj;
        }

        public User getAdminByID(string userID)
        {
            SqlCommand cmd = new SqlCommand("Select * from [User] where userID = @paraUserID and Status = 1");
            cmd.Parameters.AddWithValue("@paraUserID", userID);
            DataTable dt = db.getDataTable(cmd);

            User obj = new User();

            int rec_cnt = dt.Rows.Count;
            if (rec_cnt > 0)
            {
                DataRow row = dt.Rows[0];
                obj.Email = row["Email"].ToString();
                obj.PasswordHash = row["PasswordHash"].ToString();
                obj.PasswordSalt = row["PasswordSalt"].ToString();
                obj.Name = row["Name"].ToString();
                obj.ContactNumber = row["ContactNumber"].ToString();
                obj.Type = row["Type"].ToString();
                obj.Status = Convert.ToInt32(row["Status"]);
                obj.CreatedBy = Convert.ToInt32(row["CreatedBy"]);
                obj.CreatedOn = row["CreatedOn"].ToString();
                obj.LastUpdBy = row["LastUpdBy"].ToString();
                obj.LastUpdOn = row["LastUpdOn"].ToString();
            }
            else
            {
                obj = null;
            }

            return obj;
        }

        public User getUserByEmail(string Email)
        {
            SqlCommand cmd = new SqlCommand("Select * from [User] where Email = @paraEmail and Status = 1");
            cmd.Parameters.AddWithValue("@paraEmail", Email);
            DataTable dt = db.getDataTable(cmd);

            User obj = new User();

            int rec_cnt = dt.Rows.Count;
            if (rec_cnt > 0)
            {
                DataRow row = dt.Rows[0];
                obj.UserID = Convert.ToInt32(row["UserID"]);
                obj.Email = row["Email"].ToString();
                obj.PasswordHash = row["PasswordHash"].ToString();
                obj.PasswordSalt = row["PasswordSalt"].ToString();
                obj.Name = row["Name"].ToString();
                obj.ContactNumber = row["ContactNumber"].ToString();
                obj.Type = row["Type"].ToString();
                obj.Status = Convert.ToInt32(row["Status"]);
                obj.CreatedBy = Convert.ToInt32(row["CreatedBy"]);
                obj.CreatedOn = row["CreatedOn"].ToString();
                obj.LastUpdBy = row["LastUpdBy"].ToString();
                obj.LastUpdOn = row["LastUpdOn"].ToString();
            }
            else
            {
                obj = null;
            }

            return obj;
        }

        public User checkEmail(string Email)
        {
            SqlCommand cmd = new SqlCommand("Select * from [User] where Email = @paraEmail");
            cmd.Parameters.AddWithValue("@paraEmail", Email);
            DataTable dt = db.getDataTable(cmd);

            User obj = new User();

            int rec_cnt = dt.Rows.Count;
            if (rec_cnt > 0)
            {
                DataRow row = dt.Rows[0];
                obj.Email = row["Email"].ToString();
            }
            else
            {
                obj = null;
            }

            return obj;
        }

        public Boolean createUser(string Name, string Email, string ContactNumber, string Type, string PswdHash, string PswdSalt, int Status, int CompanyID, int CreatedBy, string CreatedOn)
        {
            Boolean result;

            SqlCommand cmd = new SqlCommand("INSERT INTO [User](Name, Email, ContactNumber, Type, PasswordHash, PasswordSalt, Status, CompanyID, CreatedBy, CreatedOn) VALUES (@paraName, @paraEmail, @paraContact, @paraType, @paraPswdHash, @paraPswdSalt, @paraStatus, @paraCompanyID, @paraCreatedBy, @paraCreatedOn)");

            cmd.Parameters.AddWithValue("@paraName", Name);
            cmd.Parameters.AddWithValue("@paraEmail", Email);
            cmd.Parameters.AddWithValue("@paraContact", ContactNumber);
            cmd.Parameters.AddWithValue("@paraType", Type);
            cmd.Parameters.AddWithValue("@paraPswdHash", PswdHash);
            cmd.Parameters.AddWithValue("@paraPswdSalt", PswdSalt);
            cmd.Parameters.AddWithValue("@paraStatus", Status);
            cmd.Parameters.AddWithValue("@paraCompanyID", CompanyID);
            cmd.Parameters.AddWithValue("@paraCreatedBy", CreatedBy);
            cmd.Parameters.AddWithValue("@paraCreatedOn", CreatedOn);

            result = db.executeNonQuery(cmd);
            return result;
        }

        public Boolean createAdmin(string Name, string Email, string ContactNumber, string Type, string PswdHash, string PswdSalt, int Status, int CreatedBy, string CreatedOn)
        {
            Boolean result;

            SqlCommand cmd = new SqlCommand("INSERT INTO [User](Name, Email, ContactNumber, Type, PasswordHash, PasswordSalt, Status, CompanyID, CreatedBy, CreatedOn) VALUES (@paraName, @paraEmail, @paraContact, @paraType, @paraPswdHash, @paraPswdSalt, @paraStatus, 1, @paraCreatedBy, @paraCreatedOn)");

            cmd.Parameters.AddWithValue("@paraName", Name);
            cmd.Parameters.AddWithValue("@paraEmail", Email);
            cmd.Parameters.AddWithValue("@paraContact", ContactNumber);
            cmd.Parameters.AddWithValue("@paraType", Type);
            cmd.Parameters.AddWithValue("@paraPswdHash", PswdHash);
            cmd.Parameters.AddWithValue("@paraPswdSalt", PswdSalt);
            cmd.Parameters.AddWithValue("@paraStatus", Status);
            cmd.Parameters.AddWithValue("@paraCreatedBy", CreatedBy);
            cmd.Parameters.AddWithValue("@paraCreatedOn", CreatedOn);

            result = db.executeNonQuery(cmd);
            return result;
        }
        //public Boolean CheckUserID(string userId, int currentUserID)
        //{
        //    Boolean result;
        //    if (userId = currentUserID)
        //    {
        //        return false;
        //    }
        //    else
        //    {
        //        return true;
        //    }
        //}
        
        public Boolean deleteQns(string userId)
        {
            Boolean result;

            //sqlStr.AppendLine("DELETE FROM [User] where UserID = @paraUserId");
            
            SqlCommand cmd = new SqlCommand("Update [User] set Status = 0 where UserID = @parauserId");

            cmd.Parameters.AddWithValue("@paraUserId", userId);

            result = db.executeNonQuery(cmd);
            return result;

        }

        public Boolean updateUser(string UserId, string uName, string uContact, string lastUpdBy, string lastUpdOn)
        {
            Boolean result;

            SqlCommand cmd = new SqlCommand("UPDATE [User] SET Name = @paraName , ContactNumber = @paraContact, LastUpdBy = @paraLastUpdBy, LastUpdOn = @paraLastUpdOn WHERE UserID = @paraUserId");

            cmd.Parameters.AddWithValue("@paraUserId", UserId);
            cmd.Parameters.AddWithValue("@paraName", uName);
            cmd.Parameters.AddWithValue("@paraContact", uContact);
            cmd.Parameters.AddWithValue("@paraLastUpdBy", lastUpdBy);
            cmd.Parameters.AddWithValue("@paraLastUpdOn", lastUpdOn);

            result = db.executeNonQuery(cmd);
            return result;
        }

        public Boolean updateCurrentUser(string UserId, string uName, string uContact, string uPswdHash, string uPswdSalt, string lastUpdBy, string lastUpdOn)
        {
            Boolean result;

            SqlCommand cmd = new SqlCommand("UPDATE [User] SET Name = @paraName, ContactNumber = @paraContact, PasswordHash = @paraPswdHash, PasswordSalt = @paraPswdSalt, LastUpdBy = @paraLastUpdBy, LastUpdOn = @paraLastUpdOn WHERE UserID = @paraUserId");

            cmd.Parameters.AddWithValue("@paraUserId", UserId);
            cmd.Parameters.AddWithValue("@paraName", uName);
            cmd.Parameters.AddWithValue("@paraContact", uContact);
            cmd.Parameters.AddWithValue("@paraPswdHash", uPswdHash);
            cmd.Parameters.AddWithValue("@paraPswdSalt", uPswdSalt);
            cmd.Parameters.AddWithValue("@paraLastUpdBy", lastUpdBy);
            cmd.Parameters.AddWithValue("@paraLastUpdOn", lastUpdOn);

            result = db.executeNonQuery(cmd);
            return result;
        }

        public string getUserType(string Type)
        {
            string UT = "";

            if (Type == "Admin")
            {
                UT = "Admin";
            }
            else if (Type == "Member")
            {
                UT = "Normal User";
            }

            return UT;
        }

        public string getUserStatus(int Status)
        {
            string Stat = "";

            if (Status == 0)
            {
                Stat = "Inactive";
            }
            else if (Status == 1)
            {
                Stat = "Active";
            }

            return Stat;
        }
    }
}