using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace EightElements.Web.SupportTool
{
    public class Settings
    {
        public static string DatabaseConnectionString
        {
            get
            {
                return WebConfigurationManager.ConnectionStrings["8ePingDb"].ConnectionString;
            }
        }
        
        public static string AdminUsername
        {
            get
            {
                return WebConfigurationManager.AppSettings["AdminUsername"];
            }
        }

        public static int PageSize
        {
            get
            {
                int pageSize = 0;
                int.TryParse(WebConfigurationManager.AppSettings["PageSize"], out pageSize);
                return pageSize;
            }
        }
    }
}
