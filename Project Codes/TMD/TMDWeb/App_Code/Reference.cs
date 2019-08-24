using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace targeted_marketing_display
{
    public class Reference
    {
        public static string USR_ADM = "ADM";
        public static string USR_MEM = "MEM";
        public static string Constr= ConfigurationManager.ConnectionStrings["Targeted_Marketing_DisplayConnectionString"].ConnectionString;
    }
}