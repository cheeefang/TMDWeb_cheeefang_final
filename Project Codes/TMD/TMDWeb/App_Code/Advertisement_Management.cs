using System;
using System.Collections;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using System.Collections.Generic;
using targeted_marketing_display;



namespace targeted_marketing_display
{
    public class Advertisement_Management
    {
        Database dbConnection = new Database();
        public Advertisement getAdvByID(string AdvID)
        {
            SqlCommand cmd = new SqlCommand("Select * from [Advertisement] where AdvID = @ID and Status = 1");
            cmd.Parameters.AddWithValue("@ID", AdvID);
            DataTable dt = dbConnection.getDataTable(cmd);
            Advertisement obj = new Advertisement();
            int rec_cnt = dt.Rows.Count;
            if (rec_cnt > 0)
            {
                DataRow row = dt.Rows[0];

                obj.AdvID = Convert.ToInt32(row["AdvID"]);
                obj.Name = row["Name"].ToString();
                obj.Item = (row["Item"].ToString());
                obj.ItemType = (row["ItemType"].ToString());
                obj.Duration = Convert.ToInt32(row["Duration"]);
                obj.CompanyID = Convert.ToInt32(row["CompanyID"]);
                obj.StartDate = row["StartDate"].ToString();
                obj.EndDate = row["EndDate"].ToString();
                
                obj.Status = Convert.ToInt32(row["status"]);
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

        public Boolean deleteAdvert(string AdvID)
        {
            Boolean result;

            //sqlStr.AppendLine("DELETE FROM [User] where UserID = @paraUserId");
            SqlCommand cmd = new SqlCommand("Update [Advertisement] set Status = 0 where AdvID = @ID");

            cmd.Parameters.AddWithValue("@ID", AdvID);

            result = dbConnection.executeNonQuery(cmd);
            return result;

        }

        public Boolean AdvertUpdate(string AdvID,string itemUpload,string itemType,int companyID,string Name,int duration, string StartDate, string EndDate, string LastUpdBy, string LastUpdOn)
        {
            Boolean result;

            SqlCommand cmd = new SqlCommand("UPDATE [Advertisement] SET item=@newItem,itemType=@newitemType, CompanyID=@newCompID, Name=@AdName,Duration=@newduration, StartDate=@paraStartDate,EndDate=@paraEndDate, LastUpdBy = @paraLastUpdBy, LastUpdOn = @paraLastUpdOn WHERE AdvID=@paraAdvID");
            cmd.Parameters.AddWithValue("@paraAdvID", AdvID);
            cmd.Parameters.AddWithValue("@newItem", itemUpload);
            cmd.Parameters.AddWithValue("@newitemType", itemType);
            cmd.Parameters.AddWithValue("@newCompID", companyID);
            cmd.Parameters.AddWithValue("@AdName", Name);
            cmd.Parameters.AddWithValue("@newduration", duration);
            cmd.Parameters.AddWithValue("@paraStartDate", StartDate);
            cmd.Parameters.AddWithValue("@paraEndDate", EndDate);
            
            cmd.Parameters.AddWithValue("@paraLastUpdBy", LastUpdBy);
            cmd.Parameters.AddWithValue("@paraLastUpdOn", LastUpdOn);

            result = dbConnection.executeNonQuery(cmd);
            return result;
        }

        public Boolean AdvertAudienceUpdate(string AdvID, int AgeID, string GenderID)
        {
            Boolean result;
            SqlCommand cmd = new SqlCommand("update [AdvertisementAudience] set AgeID=@newAgeID,GenderID=@newGenderID where AdvID=@paraAdvID " +
             "if @@rowcount=0 insert into [AdvertisementLocation] (AdvID,AgeID,GenderID) values (@newAdvID,@newAgeID,@newGenderID) ");
            cmd.Parameters.AddWithValue("@newAgeID", AgeID);
            cmd.Parameters.AddWithValue("@paraAdvID", AdvID);
            cmd.Parameters.AddWithValue("@newGenderID", GenderID);
            result = dbConnection.executeNonQuery(cmd);
            return result;
        }

        public Boolean AdvertAudienceDeleteExisting(string AdvID)
        {
            Boolean result;
            SqlCommand cmd = new SqlCommand("Delete from [AdvertisementAudience] where AdvID=@ID");
            cmd.Parameters.AddWithValue("@ID", AdvID);
            result = dbConnection.executeNonQuery(cmd);
            return result;
        }


        public Boolean AdvertCategoryUpdate(string AdvID, string CategoryID)
        {
            Boolean result;
            SqlCommand cmd = new SqlCommand("update [AdvertisementLocation] set CategoryID=@newCategoryID where AdvID=@paraAdvID " +
              "if @@rowcount=0 insert into [AdvertisementLocation] (AdvID,CategoryID) values (@AdvID,@newCategoryID) ");
            cmd.Parameters.AddWithValue("@newCategoryID", CategoryID);
            cmd.Parameters.AddWithValue("@paraAdvID", AdvID);
            result = dbConnection.executeNonQuery(cmd);
            return result;
        }

        public Boolean AdvertCategoryDeleteExisting(string AdvID)
        {
            Boolean result;
            SqlCommand cmd = new SqlCommand("Delete from [AdvertisementCategory] where AdvID=@ID");
            cmd.Parameters.AddWithValue("@ID", AdvID);
            result = dbConnection.executeNonQuery(cmd);
            return result;
        }

        public Boolean AdvertLocationUpdate(string AdvID,int BillboardID)
        {
            Boolean result;
            SqlCommand cmd = new SqlCommand("update [AdvertisementLocation] set BillboardID=@newBillboardID where AdvID=@paraAdvID " +
               "if @@rowcount=0 insert into [AdvertisementLocation] (AdvID,BillboardID) values (@AdvID,@newBillboardID) ");
            cmd.Parameters.AddWithValue("@newBillboardID", BillboardID);
            cmd.Parameters.AddWithValue("@paraAdvID", AdvID);
            result = dbConnection.executeNonQuery(cmd);
            return result;
        }

        public Boolean AdvertLocationDeleteExisting(string AdvID)
        {
            Boolean result;
            SqlCommand cmd = new SqlCommand("Delete from [AdvertisementLocation] where AdvID=@ID");
            cmd.Parameters.AddWithValue("@ID", AdvID);
            result = dbConnection.executeNonQuery(cmd);
            return result;
        }



    }
}