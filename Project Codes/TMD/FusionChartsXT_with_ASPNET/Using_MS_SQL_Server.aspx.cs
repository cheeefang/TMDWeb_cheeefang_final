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


public partial class Using_MS_SQL_Server : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        /*******
         *
         * 
         * Uncomment this block to use the Data String method. 
         * You will also need to change the parameters of the RenderChart() method accordingly.
         *
         * 
 
        // Construct the connection string to interface with the SQL Server Database
        string connStr = @"Data Source=.\SQLEXPRESS;AttachDbFilename=C:\Users\Fusion Charts\Documents\Visual Studio 2010\WebSites\FusionChartsXT_with_ASPNET\App_Data\FusionChartsDB.mdf;Integrated Security=True;User Instance=True";

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
            SqlCommand query = new SqlCommand("SELECT SUM(FC_OrderDetails.UnitPrice * FC_OrderDetails.Quantity) AS AMOUNT, YEAR(FC_Orders.OrderDate) AS yr FROM FC_Orders INNER JOIN FC_OrderDetails ON FC_OrderDetails.OrderID = FC_Orders.OrderID GROUP BY YEAR(FC_Orders.OrderDate) ORDER BY YEAR(FC_Orders.OrderDate)", conn);

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
        */
            // Call the RenderChart method, pass the correct parameters, and write the return value to the Literal tag
            chart_from_db.Text = FusionCharts.RenderChart(
                "FusionChartsXT/Column2D.swf", // Path to chart's SWF
                Server.UrlEncode("DataProvider.aspx"), // Page which returns chart data
                "", // String containing the chart data. Leave blank when using Data URL.
                "annual_revenue",   // Unique chart ID
                "640", "340",       // Width & Height of chart
                false,              // Disable Debug Mode
                true);              // Register with JavaScript object
        
    }
}