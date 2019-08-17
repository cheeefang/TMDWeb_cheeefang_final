using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace targeted_marketing_display
{
    public class Advertisement
    {
        public int AdvID { get; set; }
        public string Name { get; set; }
        public string Item { get; set; }
        public string ItemType { get; set; }
        public int Duration { get; set; }
        public int CompanyID { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public int Status { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedOn { get; set; }
        public string LastUpdBy { get; set; }
        public string LastUpdOn { get; set; }

    }
    //public class AdvertDataAccessLayer
    //{
    //    public static List<Advertisement> GetAllAdverts(string sortColumn)
    //    {
    //        List<Advertisement> listAdverts = new List<Advertisement>();
    //        string CS = ConfigurationManager.ConnectionStrings["TMDdb"].ConnectionString;
    //        using (SqlConnection con=new SqlConnection(CS))
    //        {
    //            string 
    //        }
    //    }
    //}
}