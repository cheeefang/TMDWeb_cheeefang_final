using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace targeted_marketing_display
{
    public class Company
    {
        public int CompanyID { get; set; }
        public string Industry { get; set; }
        public string Name { get; set; }
        public int Status { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedOn { get; set; }
        public string LastUpdBy { get; set; }
        public string LastUpdOn { get; set; }
    }
}
