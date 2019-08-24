using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace targeted_marketing_display
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
  
}