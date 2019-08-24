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
    public class Company_Management
    {
        Database dbConnection = new Database();
        //SqlConnection conn = null;
        //SqlDataReader reader = null;
        //conn = new
       
        //conn.Open();
        public Boolean CoInfoInsert(String CompanyName, String Industry, DateTime CreatedOn)
        {
            bool result = false;    // Execute NonQuery return boolean 

            // Instantiate SqlConnection instance and SqlCommand instance
            dbConnection.getDBConnection();

            string sqlStatement = @"INSERT INTO Company (Name,Industry,status,CreatedOn,CreatedBy)
                                    VALUES (@CompanyName,@Industry,1,@CreatedOn,1)";

            SqlCommand sCmd = new SqlCommand(sqlStatement);

            // Add each parameterised query variable with value
            //          complete to add all parameterised queries
            sCmd.Parameters.AddWithValue("@CompanyName", CompanyName);
            sCmd.Parameters.AddWithValue("@Industry", Industry);
            sCmd.Parameters.AddWithValue("@CreatedOn", CreatedOn);

         

            result = dbConnection.executeNonQuery(sCmd);

            return result;

        }
        public Boolean CoInfoCheck()
        {
            bool record = false;

            string sqlStatement = @"SELECT Name, Industry,  (case Status when 1 then 'Active' else 'Inactive' end) as Status FROM Company WHERE status = 1";

            SqlCommand sCmd = new SqlCommand(sqlStatement);

            dbConnection.executeNonQuery(sCmd);
            record = dbConnection.executeScalar(sCmd);

            return record;

        }

        public Boolean CoInfoUpdate(string CompanyID, string companyName, string Industry, string lastUpdBy, string lastUpdOn)
        {
            Boolean result;

            SqlCommand cmd = new SqlCommand("UPDATE [COMPANY] SET Name = @paraName, Industry = @paraIndustry ,LastUpdBy = @paraLastUpdBy, LastUpdOn = @paraLastUpdOn WHERE CompanyID=@paraCompanyID");
            cmd.Parameters.AddWithValue("@paraCompanyID",CompanyID);
            cmd.Parameters.AddWithValue("@paraName", companyName);
            cmd.Parameters.AddWithValue("@paraIndustry", Industry);
            cmd.Parameters.AddWithValue("@paraLastUpdBy", lastUpdBy);
            cmd.Parameters.AddWithValue("@paraLastUpdOn", lastUpdOn);

            result = dbConnection.executeNonQuery(cmd);
            return result;
        }




        public DataTable CoInfoRead()
        {
            DataTable dt = new DataTable();

            string sqlStatement = @"SELECT Industry, Name, (case Status when 1 then 'Active' else 'Inactive' end) as Status FROM Company WHERE status = 1";

            SqlCommand sCmd = new SqlCommand(sqlStatement);
            //dbConnection.getDataTable(sCmd);

            dt = dbConnection.getDataTable(sCmd);

            dt.Columns.Add(new DataColumn("Company Name"));
            dt.Columns.Add(new DataColumn("industry"));
            dt.Columns.Add(new DataColumn("status"));

            DataRow dr = dt.NewRow();

            for (int q = 0; q < dt.Rows.Count; q++)
            {
                dr["Company Name"] = dr["Company Name"].ToString();
                dr["Industry"] = dr["Industry"].ToString();
                dr["Status"] = dr["Status"].ToString();

            }

            return dt;
            

        }

        public Boolean deleteCompany(string CompanyID)
        {
            Boolean result;
            //SqlCommand cmdCount = new SqlCommand("select count(*) from Advertisement as a inner join Company as c on a.companyID=c.CompanyID where c.CompanyID=@ID");
            //SqlParameter param1 = new SqlParameter();
            //param1.ParameterName = "@ID";
            //param1.Value = CompanyID;
            //cmdCount.Parameters.Add(param1);
            //SqlDataAdapter sda1 = new SqlDataAdapter();
            //DataTable dt = new DataTable();
            //cmdCount.Connection = conn;
            //sda1.SelectCommand = cmdCount;

            //sda1.Fill(dt);
            
            //sqlStr.AppendLine("DELETE FROM [User] where UserID = @paraUserId");
            SqlCommand cmd = new SqlCommand("Update [Company] set Status = 0 where CompanyID = @paraCompanyID");

            cmd.Parameters.AddWithValue("@paraCompanyID", CompanyID);

            result = dbConnection.executeNonQuery(cmd);
            return result;

        }




        public Company getCompanyByID(string CompanyID)
        {
            SqlCommand cmd = new SqlCommand("Select * from [Company] where companyID=@ParaCompanyID and status =1 ");
            cmd.Parameters.AddWithValue("@paraCompanyID", CompanyID);
            DataTable dt = dbConnection.getDataTable(cmd);

            Company obj = new Company();

            int rec_cnt = dt.Rows.Count;
            if (rec_cnt > 0)
            {
                DataRow row = dt.Rows[0];       
                obj.CompanyID = Convert.ToInt32(row["CompanyID"]);
                obj.Industry = row["Industry"].ToString();
                obj.Name = row["Name"].ToString();
    
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


       



    }
}

