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
   
    public class Billboard_Management
    {
        Database dbConnection = new Database();

        public Billboard getBillboardByID(string BillboardID)
        {
            SqlCommand cmd = new SqlCommand("Select * from [BillboardLocation] where BillboardID = @paraBillboardID and [BillboardLocation].Status = 1");
            cmd.Parameters.AddWithValue("@paraBillboardID", BillboardID);
            DataTable dt = dbConnection.getDataTable(cmd);

            Billboard obj = new Billboard();

            int rec_cnt = dt.Rows.Count;
            if (rec_cnt > 0)
            {
                DataRow row = dt.Rows[0];
                
                obj.BillboardID = Convert.ToInt32(row["BillboardID"]);
                obj.BillboardCode = row["BillboardCode"].ToString();
                obj.latitude = (row["latitude"].ToString());
                obj.Longtitude = (row["Longtitude"].ToString());
                obj.AddressLn1 = row["AddressLn1"].ToString();
                obj.AddressLn2 = row["AddressLn2"].ToString();
                obj.City = row["City"].ToString();
                obj.Country = row["Country"].ToString();
                obj.postalCode = row["postalCode"].ToString();
                obj.status = Convert.ToInt32(row["status"]);
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

        public Billboard getBillboardAdvertsByID(string BillboardID)
        {
            SqlCommand cmd = new SqlCommand("Select * from [Advertisement] inner join [AdvertisementLocation] on [Advertisement].AdvID=[AdvertisementLocation].AdvID  where [Advertisement].AdvID=@paraAdvID and Advertisement].Status = 1");
           
            cmd.Parameters.AddWithValue("@paraBillboardID", BillboardID);

            DataTable dt = dbConnection.getDataTable(cmd);

            Billboard obj = new Billboard();

            int rec_cnt = dt.Rows.Count;
            if (rec_cnt > 0)
            {
                DataRow row = dt.Rows[0];

                obj.BillboardID = Convert.ToInt32(row["BillboardID"]);
                obj.BillboardCode = row["BillboardCode"].ToString();
                obj.latitude = (row["latitude"].ToString());
                obj.Longtitude = (row["Longtitude"].ToString());
                obj.AddressLn1 = row["AddressLn1"].ToString();
                obj.AddressLn2 = row["AddressLn2"].ToString();
                obj.City = row["City"].ToString();
                obj.Country = row["Country"].ToString();
                obj.postalCode = row["postalCode"].ToString();
                obj.status = Convert.ToInt32(row["status"]);
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




        public Boolean BBInfoUpdate(string BillboardID, string AddressLn1, string AddressLn2, string City, string Country,string latitude,string Longtitude, string postalCode, string lastUpdBy, string lastUpdOn)
        {
            Boolean result;

            SqlCommand cmd = new SqlCommand("UPDATE [BillboardLocation] SET AddressLn1 = @paraAddressLn1 , AddressLn2 = @paraAddressLn2 ,City=@paraCity , Country=@paraCountry ,latitude=@paralatitude,Longtitude=@paraLongtitude," +
                "postalcode=@parapostalCode, LastUpdBy = @paraLastUpdBy, LastUpdOn = @paraLastUpdOn WHERE BillboardID=@paraBillboardID");
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



        public Boolean deleteBillboard(string BillboardID)
        {
            Boolean result;

            //sqlStr.AppendLine("DELETE FROM [User] where UserID = @paraUserId");
            SqlCommand cmd = new SqlCommand("Update [BillboardLocation] set Status = 0 where BillboardID = @paraBillboardID");

            cmd.Parameters.AddWithValue("@paraBillboardID", BillboardID);

            result = dbConnection.executeNonQuery(cmd);
            return result;

        }


        public Boolean BBinsert(String BillboardCode, String AddressLn1, String AddressLn2,
            String City, String Country, String postalCode, DateTime CreatedOn, int Status, String latitude, String Longtitude, int CreatedBy)
        {

            bool result = false;    // Execute NonQuery return boolean 


            //VIC: no checking if the billboard code has already been used before? it is supposed to be unique
            string sqlStatement = @"INSERT INTO BillboardLocation (BillboardCode,latitude,Longtitude,AddressLn1,AddressLn2,City,Country,postalCode,status,CreatedOn,CreatedBy)
                                    VALUES (@BillboardCode,@latitude,@Longtitude, @AddressLn1,@AddressLn2,@City,@Country,@postalCode,@Status,@CreatedOn,@CreatedBy)";

            SqlCommand sCmd = new SqlCommand(sqlStatement);

            // Add each parameterised query variable with value
            //          complete to add all parameterised queries
            sCmd.Parameters.AddWithValue("@BillboardCode", BillboardCode);
            sCmd.Parameters.AddWithValue("@AddressLn1", AddressLn1);
            sCmd.Parameters.AddWithValue("@AddressLn2", AddressLn2);
            sCmd.Parameters.AddWithValue("@City", City);
            sCmd.Parameters.AddWithValue("@Country", Country);
            sCmd.Parameters.AddWithValue("@postalCode", postalCode);
            sCmd.Parameters.AddWithValue("@CreatedOn", CreatedOn);
            sCmd.Parameters.AddWithValue("@CreatedBy", CreatedBy);
            sCmd.Parameters.AddWithValue("@Status", Status);
            sCmd.Parameters.AddWithValue("@latitude", latitude);
            sCmd.Parameters.AddWithValue("@Longtitude", Longtitude);


            result = dbConnection.executeNonQuery(sCmd);

            return result;
        }

    
        
        public DataTable BBread()
        {
            DataTable dt = new DataTable();

            string sqlStatement = @"Select BillboardCode, latitude, Longtitude, AddressLn1 + ' '+ AddressLn2 Address, City, Country, postalCode from BillboardLocation where status = 1";

            SqlCommand sCmd = new SqlCommand(sqlStatement);
            //dbConnection.getDataTable(sCmd);
            
            dt = dbConnection.getDataTable(sCmd);

            dt.Columns.Add(new DataColumn("Code"));
            dt.Columns.Add(new DataColumn("city"));
            dt.Columns.Add(new DataColumn("Postal Code"));
            dt.Columns.Add(new DataColumn("Latitude"));
            dt.Columns.Add(new DataColumn("Longitude"));
            dt.Columns.Add(new DataColumn("address"));
            dt.Columns.Add(new DataColumn("country"));



            DataRow dr = dt.NewRow();

            for (int q = 0; q < dt.Rows.Count; q++)
            {
                dr["BillboardCode"] = dr["Code"].ToString();
                dr["Address"] = dr["Address"].ToString();
                dr["City"] = dr["City"].ToString();
                dr["Country"] = dr["Country"].ToString();
                dr["Postal Code"] = dr["postalCode"].ToString();
                dr["Latitude"] = dr["Latitude"].ToString();
                dr["Longitude"] = dr["Longtitude"].ToString();

               // dt.Rows.Add(dr);
            }

            return dt;
            
        }
      

        public Boolean BBsearch()
        {
            bool result = false;

            string sqlStatement = "Name LIKE '%{0}%' OR Item LIKE '%{0}%' OR convert(CreatedBy,'System.String') LIKE '%{0}%'";

            SqlCommand sCmd = new SqlCommand(sqlStatement);

            result = dbConnection.executeNonQuery(sCmd);

            return result; 

        }
    }
}

