using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EightElements.Web.SupportTool.Domain.Models
{
    public class Transaction
    {
        public int ID { get; set; }
        
        public DateTime Timestamp { get; set; }
        
        public string Msisdn { get; set; }
        
        public string ContentName { get; set; }
        
        public double FileSize { get; set; }
        
        public double Price { get; set; }
        
        public string InstallStatus { get; set; } 

        public string PhoneModel { get; set; }        
    }
}
