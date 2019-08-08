using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Text;
using InfoSoftGlobal;

public partial class DataProvider : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Construct the connection string to interface with the SQL Server Database
        string connStr = @"Data Source=L33527\CHEEEFANGSQL;Initial Catalog=Targeted_Marketing_Display;Persist Security Info=True;User ID=root;Password=passw8rd";
   
        // Initialize the string which would contain the chart data in XML format
        StringBuilder xmlStr = new StringBuilder();

        // Provide the relevant customization attributes to the chart
        xmlStr.Append("<chart caption='Total Revenue' palette='3' showValues='0' numberPrefix='$' useRoundEdges='1'>");

        // Create a SQLConnection object 
        using (SqlConnection conn = new SqlConnection(connStr))
        {
            // Establish the connection with the database
            conn.Open();

            // Construct and execute SQL query which would return the total amount of sales for each year
            SqlCommand query = new SqlCommand("SELECT sum(noofpax) from advertisementfeedback where advid=1", conn);

            // Begin iterating through the result set
            SqlDataReader rst = query.ExecuteReader();


            while (rst.Read())
            {
                // Construct the chart data in XML format
                xmlStr.AppendFormat("<set label='{0}' value='{1}'/>", rst["yr"].ToString(), rst["AMOUNT"].ToString());
            }

            // End the XML string
            xmlStr.Append("</chart>");

            // Close the result set Reader object and the Connection object
            rst.Close();
            conn.Close();

            // This page should return only XML content
            Response.ContentType = "text/xml";
            Response.Write(xmlStr.ToString());
        }
    }
}