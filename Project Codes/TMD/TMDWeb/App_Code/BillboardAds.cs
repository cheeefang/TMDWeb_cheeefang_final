using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace targeted_marketing_display.App_Code
{
    public class BillboardAds
    {
        public int AdvID { get; set; }
        public string Name { get; set; }
        public string Item { get; set; }
        public string ItemType { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }
    //public class BillboardDataAccessLayer
    //{
    //    public static List<BillboardAds> GetAllAds(string sortColumn)
    //    {
    //        //List<BillboardAds> listAds = new List<BillboardAds>();
    //        //string CS = ConfigurationManager.ConnectionStrings["TMDdb"].ConnectionString;
    //        //SqlConnection conn = null;
    //        //SqlDataReader reader = null;



    //        //// instantiate and open connection
    //        //conn = new
    //        //    SqlConnection(@"Data Source=L33527\CHEEEFANGSQL;Initial Catalog=Targeted_Marketing_Display;Persist Security Info=True;User ID=root;Password=passw8rd");
    //        //conn.Open();
    //        //SqlCommand cmd = new SqlCommand("select [BillboardLocation].BillboardCode, [Advertisement].Name,[Advertisement].Item,[Advertisement].ItemType,[Advertisement].StartDate,[Advertisement].EndDate from [Advertisement] inner join" +
    //        //    " [AdvertisementLocation] on [Advertisement].AdvID=[AdvertisementLocation].AdvID join " +
    //        //    "[BillboardLocation] on[AdvertisementLocation].BillboardID =[BillboardLocation].BillboardID " +
    //        //    "where [Advertisement].status=1 and [BillboardLocation].BillboardID=@ID", conn);
    //        //SqlParameter param = new SqlParameter();
    //        //param.ParameterName = "@ID";
    //        //param.Value = HttpContext.Current.Session("BillboardID")


    //        //}

    //}

    //public class EmployeeDataAccessLayer
    //{
    //    public static List<Employee> GetAllEmployees(string sortColumn)
    //    {
    //        List<Employee> listEmployees = new List<Employee>();

    //        string CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
    //        using (SqlConnection con = new SqlConnection(CS))
    //        {
    //            string sqlQuery = "Select * from tblEmployee";

    //            if (!string.IsNullOrEmpty(sortColumn))
    //            {
    //                sqlQuery += " order by " + sortColumn;
    //            }

    //            SqlCommand cmd = new SqlCommand(sqlQuery, con);

    //            con.Open();
    //            SqlDataReader rdr = cmd.ExecuteReader();
    //            while (rdr.Read())
    //            {
    //                Employee employee = new Employee();
    //                employee.EmployeeId = Convert.ToInt32(rdr["EmployeeId"]);
    //                employee.Name = rdr["Name"].ToString();
    //                employee.Gender = rdr["Gender"].ToString();
    //                employee.City = rdr["City"].ToString();

    //                listEmployees.Add(employee);
    //            }
    //        }

    //        return listEmployees;
    //    }
    //}
}