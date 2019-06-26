using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace targeted_marketing_display
{
    //VIC: this is an entity class, so the use of the word "Management" should not be used here. Change it to User
    public class User
    {
        public int UserID { get; set; }
        public string Email { get; set; }
        public int CompanyID { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public string Name { get; set; }
        public string ContactNumber { get; set; }
        public string Type { get; set; }
        public int Status { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedOn { get; set; }
        public string LastUpdBy { get; set; }
        public string LastUpdOn { get; set; }
        public string CompanyName { get; set; }
    }
}