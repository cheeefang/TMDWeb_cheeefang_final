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
using targeted_marketing_display.App_Code;


namespace targeted_marketing_display.App_Code
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

        public Boolean AdvertUpdate(string BillboardID, string AddressLn1, string AddressLn2, string City, string Country, string latitude, string Longtitude, string postalCode, string lastUpdBy, string lastUpdOn)
        {
            Boolean result;

            SqlCommand cmd = new SqlCommand("UPDATE [Advertisement] SET AddressLn1 = @paraAddressLn1 , AddressLn2 = @paraAddressLn2 ,City=@paraCity , Country=@paraCountry ,latitude=@paralatitude,Longtitude=@paraLongtitude,@parapostalCode=@parapostalCode, LastUpdBy = @paraLastUpdBy, LastUpdOn = @paraLastUpdOn WHERE BillboardID=@paraBillboardID");
            cmd.Parameters.AddWithValue("@paraBillboardID", BillboardID);

            cmd.Parameters.AddWithValue("@paraAddressLn1", AddressLn1);
            cmd.Parameters.AddWithValue("@paraAddressLn2", AddressLn2);
            cmd.Parameters.AddWithValue("@paraCity", City);
            cmd.Parameters.AddWithValue("@paraCountry", Country);
            cmd.Parameters.AddWithValue("@paralatitude", latitude);
            cmd.Parameters.AddWithValue("@paraLongtitude", Longtitude);
            cmd.Parameters.AddWithValue("@parapostalCode", postalCode);


            cmd.Parameters.AddWithValue("@paraLastUpdBy", lastUpdBy);
            cmd.Parameters.AddWithValue("@paraLastUpdOn", lastUpdOn);

            result = dbConnection.executeNonQuery(cmd);
            return result;
        }

    }
}