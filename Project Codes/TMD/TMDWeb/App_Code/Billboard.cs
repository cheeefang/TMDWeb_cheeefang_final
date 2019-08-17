using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace targeted_marketing_display
{
    public class Billboard
    {
        public int BillboardID { get; set; }
        public string BillboardCode { get; set; }
        public string latitude { get; set; }
        public string Longtitude { get; set; }
        public string AddressLn1 { get; set; }
        public string AddressLn2 { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string postalCode { get; set; }

        public int status { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedOn { get; set; }
        public string LastUpdBy { get; set; }
        public string LastUpdOn { get; set; }
        
    }

}
