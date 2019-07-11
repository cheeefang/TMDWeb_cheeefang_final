using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace targeted_marketing_display.App_Code
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
}